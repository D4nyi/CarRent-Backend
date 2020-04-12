using System.Text;
using CarRent.Contexts.Interfaces;
using CarRent.Contexts.SQLiteContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CarRent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SQLiteDbContext>(
                options =>
                {
                    options.UseSqlite("Data Source=CarRentDB.db;", o =>
                    {
                        o.MigrationsAssembly("CarRent.Contexts.SQLiteContext");
                    });
                }
            );

            services.AddScoped<IUserRepository, UserRepositroy>();
            services.AddScoped<IPremiseRepository, PremiseRepository>();
            services.AddScoped<ICarRepository, CarRepositroy>();

            services.AddAuthentication("AuthToken")
                .AddJwtBearer("AuthToken", config =>
                {
                    byte[] secretBytes = Encoding.UTF8.GetBytes("pepsjvxyjcvpsdjvélyxvéléá");

                    // getting jwt token form query params
                    /**
                    config.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.ContainsKey("token"))
                            {
                                context.Token = context.Request.Query["token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                    */

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                        ValidIssuer = "https://localhost:5001/",
                        ValidAudience = "https://localhost:5001/"
                    };
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication()
               .UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
