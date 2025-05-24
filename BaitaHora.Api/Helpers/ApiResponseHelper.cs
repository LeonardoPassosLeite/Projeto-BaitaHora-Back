using Microsoft.AspNetCore.Mvc;

namespace BaitaHora.Api.Helpers
{
    public static class ApiResponseHelper
    {
        public static IActionResult CreateSuccess(string message, object? data = null)
        {
            return new OkObjectResult(new
            {
                success = true,
                message,
                data
            });
        }

        public static IActionResult CreateError(string message, string? error = null, int statusCode = 400)
        {
            var response = new
            {
                success = false,
                message,
                error,
                statusCode
            };

            return new ObjectResult(response) { StatusCode = statusCode };
        }
    }
}