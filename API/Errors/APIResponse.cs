namespace API.Errors
{
    public class APIResponse
    {
        public APIResponse(int statusCode , string errorMessage = null!)
        {
            StatusCode=statusCode;
            ErrorMessage=GetDefaultMessageForStatusCode(statusCode) ?? errorMessage;
        }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, It was not",
                500 => "Error are the path in dark side. Errors lead to anger. anger leds to hate." +
                "Hate leads to career change",
                _ =>null!
            };
        }
    }
}
