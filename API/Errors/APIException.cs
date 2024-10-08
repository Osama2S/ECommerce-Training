namespace API.Errors
{
    public class APIException : APIResponse
    {
        public APIException(int statusCode, string errorMessage = null!,string details=null!) : base(statusCode, errorMessage)
        {
            Details=details;
        }
        public string Details { get; set; }

    }
}
