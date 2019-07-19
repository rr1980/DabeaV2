using Newtonsoft.Json;

namespace DabeaV2.Web.Middleware.ExceptionHandling
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Stack { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}