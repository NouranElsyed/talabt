namespace talabt.Error
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessageForStatusCode(statusCode);
        }
        private string GetDefaultMessageForStatusCode(int statusCode) 
        {
            var message = statusCode switch
            {
                400 => "Hmm, something's not quite right with your request. Please try again or contact us if the problem persists.",
                401 => "You're not logged in! Please sign in to continue.",
                403 => "It looks like you don't have permission to access this page. If you think this is a mistake, please contact support.",
                404 => "Oops! We can't seem to find what you're looking for. Please check the URL or return to the homepage.",
                500 => "Something went wrong on our end. We're working on it! Please try again later.",
                503 => "Our servers are currently busy or down for maintenance. Please check back soon!"
            };
            return message;
        }
    }
}
