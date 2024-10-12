using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EC.Core.Response
{
    public class ApiValidationResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; } 
        public ApiValidationResponse(int? statusCode = 400, IEnumerable<string> errors = null) : base(statusCode)
        {
            Errors = errors ?? new List<string>();
        }


      

    }
}
