using System;

namespace DabeaV2.Services
{
    public class DabeaV2ServicesException : Exception
    {
        public DabeaV2ServicesException(string message) : base(message)
        {

        }

        public DabeaV2ServicesException(string message, Exception ex) : base("ServicesException: " + message, ex)
        {

        }
    }
}
