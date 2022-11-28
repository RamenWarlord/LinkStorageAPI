namespace LinkStorageAPI.Models
{
    public class Response
    {
        public Response(int statusCode, string statusDescription, object result)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Result = result;
        }
        public Response() { }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public object Result { get; set; }
    }
}
