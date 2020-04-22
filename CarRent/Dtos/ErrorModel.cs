using System;

namespace CarRent.Dtos
{
    public class ErrorModel
    {
        public ErrorModel()
        {
            TimeStamp = DateTime.Now;
            CauseValueType = null;
        }

        public string Message { get; set; }
        public string CauseValueType { get; set; }
        public string CauseValueName { get; set; }
        public string CauseValue { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
