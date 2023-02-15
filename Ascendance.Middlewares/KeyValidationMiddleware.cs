using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ascendance.Middlewares
{
    public class KeyValidationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IConfiguration _configuration;

        private const string APIKEY = "ApiKey";
        public KeyValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
                _requestDelegate = next;
                _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEY, out var key))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("unable to find Api key.");
                return;
            }

            var _key = _configuration.GetValue<string>(APIKEY);
            if (!_key.Equals(key))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }

            await _requestDelegate(context);
        }
    }
}
