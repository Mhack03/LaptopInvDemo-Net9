using System.Net;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace API.Utilities
{
    public class ErrorResponseUtility
    {
        public static ActionResult GenerateErrorResponse(Exception ex, ILogger logger)
        {
            logger.LogError(ex, "An error occurred");
            var errorDetails = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "An unexpected error occurred. Please try again later."
            };
            return new ObjectResult(errorDetails)
            {
                StatusCode = errorDetails.StatusCode
            };
        }
    }
}