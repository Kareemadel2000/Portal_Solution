namespace Portal.Api.SeedWork.Custom_Problem_Details
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }        
        public string Message { get; private set; }
        public ApiResponse(int statusCode, string message = null!)
        {
            this.StatusCode = statusCode;            
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        private string GetDefaultMessageForStatusCode(int statusCode)
            => statusCode switch
            {
                400 => "A Bad Request Happen.",
                401 => "Un Authorized.",
                403 => "Confilect Happen.",
                404 => "Resource Was Not Found.",
                500 => "Internal Server Error.",
                _ => null!
            };
    }
}
