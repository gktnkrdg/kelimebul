using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelimeBul.API.Models
{

    public class ApiResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ApiResponse(bool isSuccess = false, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
    public class ApiResponse<TData> : ApiResponse where TData : class
    {
        public TData Data { get; set; }
        public ApiResponse(TData data, bool isSuccess = false, string message = "")
           : base(isSuccess, message)
        {
            Data = data;
        }
    }
}
