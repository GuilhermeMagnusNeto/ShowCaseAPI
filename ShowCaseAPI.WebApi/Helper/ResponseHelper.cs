using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.WebApi.Model.ResponseHelper;

namespace ShowCaseAPI.WebApi.Helper
{
    public class ResponseHelper
    {
        public static IActionResult CustomResponse(int statusCode,object data = null, string message = null)
        {
            var response = new ApiResponseViewModel
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };

            return new JsonResult(response);
        }

        public static IActionResult Success(object data = null, string message = "Success")
        {
            var response = new ApiResponseViewModel
            {
                StatusCode = 200,
                Message = message,
                Data = data
            };

            return new JsonResult(response);
        }

        public static IActionResult BadRequest(object data = null, string message = "Bad Request")
        {
            var response = new ApiResponseViewModel
            {
                StatusCode = 400,
                Message = message,
                Data = data
            };

            return new JsonResult(response);
        }

        public static IActionResult NotFound(string message = "Not Found")
        {
            var response = new ApiResponseViewModel
            {
                StatusCode = 404,
                Message = message
            };

            return new JsonResult(response);
        }

        public static IActionResult InternalServerError(object data = null, string message = "Internal Server Error")
        {
            var response = new ApiResponseViewModel
            {
                StatusCode = 500,
                Message = message,
                Data = data
            };

            return new JsonResult(response);
        }
    }      
}
