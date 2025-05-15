using Emovere.SharedKernel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Emovere.WebApi.Middlewares
{
    public sealed class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var problemDetails = new
                {
                    Message = "Internal Server Error.",
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                };

                var json = JsonSerializer.Serialize(problemDetails);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCode.INTERNAL_SERVER_ERROR_STATUS_CODE;
                await context.Response.WriteAsync(json);
            }
        }
    }
}