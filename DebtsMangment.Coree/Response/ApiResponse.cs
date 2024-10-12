using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EC.Core.Response
{
    public class ApiResponse
    {
        public int ? StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string ? Message { get; set; }
        public object  ? Result { get; set; }


        public ApiResponse(int ? statusCode = null, string? message =null, object ?result = null) 
        
        { 
            StatusCode = statusCode;
            Message = message ?? getMessageForStatusCode(statusCode);
            Result = result;
            IsSuccess = statusCode >= 200 && statusCode <= 300;
        
        }

        private string ? getMessageForStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                200 => "successfuly",
                201 => "User created",
                400 => "Bad Request",
                401 => "Unothrized",
                404 => "Not Found",
                500 => "Internal Srrver Error",
                _ => null
            };

        }
    }
}
