using System.Net;

namespace MagicVillaAPI.Models
{

    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public List<string> Message { get; set; }
        public object Result { get; set; }

    }

}