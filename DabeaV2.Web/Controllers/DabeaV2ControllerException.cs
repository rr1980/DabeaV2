using System;

namespace DabeaV2.Web.Controllers
{
    public class DabeaV2ControllerException : Exception
    {
        public DabeaV2ControllerException(string message) : base(message)
        {

        }

        public DabeaV2ControllerException(string message, Exception ex) : base("ControllerException: " + message, ex)
        {

        }
    }
}
