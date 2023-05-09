namespace Portal.Api.SeedWork.Custom_Problem_Details
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse(IEnumerable<string> errors) : base(400)
        {
            this.Errors = errors;
        }
    }
}
