using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Custom_Exceptions
{
    public class InvalidRequestException : Exception
    {        
        public List<string> Errors { get; }
        public InvalidRequestException(List<string> errors)
        {            
            Errors = errors;            
        }
    }
}
