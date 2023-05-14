using Azure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using Portal.Api.SeedWork.Custom_Problem_Details;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;

namespace Portal.Api.Custom_Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {            

            try
            {                
                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    await GetUnAuthorizedProblemDetails(context);
                    return;
                }
            }
            catch (InvalidRequestException ex)
            {
                ApiValidationErrorResponse problemDetails = GetBadRequestProblemDetails(ex);

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;                     
                                
                await response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }
            catch (ApiErrorResponse ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = ex.StatusCode;

                ApiResponse apiResponse = new ApiResponse(ex.StatusCode, ex.Message);
                await response.WriteAsync(JsonSerializer.Serialize(apiResponse));
            }
            catch (Exception ex) 
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ProblemDetails problemDetails = GetProblemDetails(ex);

                ApiException apiException = new ApiException(problemDetails.Detail!, problemDetails.Title!);

                await response.WriteAsync(JsonSerializer.Serialize(apiException));
            }
        }

        private ApiValidationErrorResponse GetBadRequestProblemDetails(InvalidRequestException ex)
        {            
            var invalidRequestProblemDetails = new ApiValidationErrorResponse(ex.Errors);          

            return invalidRequestProblemDetails;
        }

        private ProblemDetails GetProblemDetails(Exception ex)
        {
            string traceId = Guid.NewGuid().ToString(); // For Idenetity Server Errors In Log.

            if (_env.IsDevelopment())
                return new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "https://httpstatuses.com/500",
                    Title = ex.Message,
                    Detail = ex.ToString(),
                    Instance = traceId
                };
            else
                return new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "https://httpstatuses.com/500",
                    Title = "Something Went Wrong, Please Try Again After SomeTime.",
                    Detail = $"We apologize for inconvenience. Please let us know about the error at support@orion.com. Include traceId: {traceId} in email",
                    Instance = traceId
                };
        }
        private async Task GetUnAuthorizedProblemDetails(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.Unauthorized;

            ApiResponse apiResponse = new ApiResponse(StatusCodes.Status401Unauthorized);
            await response.WriteAsync(JsonSerializer.Serialize(apiResponse));
        }
    }
}
