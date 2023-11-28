using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Contact.Domain.Enums;
using Contact.Domain.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Contact.Application.CQRS.Core
{
    /// <summary>
    /// This is the base Response class which all services must be response with
    /// this patter
    /// </summary>
    /// <typeparam name="TOutput">TOutput is generic response type which can be anything</typeparam>
    public class ApiResult<TOutput>
    {
        public TOutput Response { get; set; }
        public int StatusCode { get; set; }
        public int ErrorCode { get; set; }
        public string Description { get; set; }



        /// <summary>
        /// OK method will be used for Success responses
        /// </summary>
        /// <param name="response">Service response</param>
        /// <returns>It returns our static ApiResult pattern and generic response
        ///also errorcode and statuscode info
        /// </returns>
        public static ApiResult<TOutput> OK(TOutput response)
        {
            return new ApiResult<TOutput>()
            {
                Response = response,
                StatusCode = (int)HttpStatusCode.OK,
                Description = "The operation has finished successfully.",
            };
        }



        /// <summary>
        /// Error method will be used for Error responses
        /// </summary>
        /// <param name="errorCode">Errocode is enum which gives us info about error description</param>
        /// <param name="statusCode">StatusCode is HTTPStatusCode , if you dont mention it then it will be BadRequest (you can switch it internal error or smth)</param>
        /// <returns>This also return our statis ApiResult pattern</returns>
        public static ApiResult<TOutput> Error(ErrorCodes errorCode, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new ApiResult<TOutput>()
            {
                Response = default,
                StatusCode = statusCode,
                ErrorCode = (int)errorCode,
                Description = errorCode.GetEnumDescription()
            };
        }
    }
}
