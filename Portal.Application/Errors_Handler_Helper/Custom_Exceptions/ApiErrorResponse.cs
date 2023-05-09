using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Custom_Exceptions
{
    public class ApiErrorResponse : Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }        
        public ApiErrorResponse(int statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode)!;            
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
            => statusCode switch
            {
                400 => "A Bad Request Happen.",
                401 => "Un Authorized.",
                403 => "Confilect Happen.",
                404 => "Resource Was Not Found.",
                500 => "Internal Server Error.",
                _ => null
            };
    }
}
