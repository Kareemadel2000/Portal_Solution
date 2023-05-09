namespace Portal.Api.SeedWork.Custom_Problem_Details
{
    public class ApiException : ApiResponse
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public ApiException(string details, string title) : base(500)
        {
            this.Details = details;
            this.Title = title;
        }
    }
}
