using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                success = true,
                message = message,
                data = data
            };
        }

        /// <summary>
        /// Creates a failed API response
        /// </summary>
        public static ApiResponse<T> ErrorResponse( T? data = default, string message ="")
        {
            return new ApiResponse<T>
            {
                success = false,
                message = message,
                data = data
            };
        }
    }
    
}
