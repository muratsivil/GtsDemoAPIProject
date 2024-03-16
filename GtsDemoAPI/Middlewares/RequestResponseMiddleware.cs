using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtsDemoAPI.Middlewares;

public class RequestResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseMiddleware> _logger;

    public RequestResponseMiddleware(RequestDelegate next, ILogger<RequestResponseMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Capture request body
        var originalBodyStream = context.Response.Body;
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            // Continue down the Middleware pipeline
            await _next(context);

            // Log user information from Basic Authorization header
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();
                if (authHeader.StartsWith("Basic"))
                {
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    _logger.LogInformation($"User making the API call: {username}");
                }else{
                    _logger.LogInformation("Invalid Authorization Header: header is not Basic");
                }
            }

            // Log entry point
            _logger.LogInformation($"API Request: {context.Request.Path} - Method: {context.Request.Method}");

            // Log response body
            responseBody.Seek(0, SeekOrigin.Begin);
            string responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();
            _logger.LogInformation($"API Response Body: {responseBodyContent}");
            
            // Log exit point
            _logger.LogInformation($"API Response: {context.Request.Path} - StatusCode: {context.Response.StatusCode} - ElapsedTime: {stopwatch.ElapsedMilliseconds}ms");

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
