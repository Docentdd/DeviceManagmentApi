using System.Text.Json;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    
    namespace DeviceManagmentApi.Middleware;
    
    public class AdditionalPropertiesValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AdditionalPropertiesValidationMiddleware> _logger;
    
        public AdditionalPropertiesValidationMiddleware(RequestDelegate next, ILogger<AdditionalPropertiesValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
    
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Starting AdditionalPropertiesValidationMiddleware");
    
            if (context.Request.ContentType == "application/json")
            {
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;
    
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(body);
                if (jsonData == null || !jsonData.ContainsKey("type"))
                {
                    await _next(context);
                    return;
                }
    
                var type = jsonData["type"]?.ToString();
                var isEnabled = jsonData.ContainsKey("isEnabled") && jsonData["isEnabled"]?.ToString() == "true";
    
                if (!isEnabled)
                {
                    await _next(context);
                    return;
                }
    
                var rulesJson = await File.ReadAllTextAsync("src/Resource/validation_rules.json");
                var validationRules = JsonSerializer.Deserialize<ValidationRules>(rulesJson);
    
                var typeRules = validationRules?.Validations.FirstOrDefault(v => v.Type == type && v.PreRequestName == "isEnabled" && v.PreRequestValue == "true");
                if (typeRules != null)
                {
                    foreach (var rule in typeRules.Rules)
                    {
                        if (jsonData.ContainsKey(rule.ParamName))
                        {
                            var value = jsonData[rule.ParamName]?.ToString();
                            if (!Regex.IsMatch(value ?? string.Empty, rule.Regex))
                            {
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                await context.Response.WriteAsync($"Validation failed for property: {rule.ParamName}");
                                _logger.LogInformation("Validation failed for property: {Property}", rule.ParamName);
                                return;
                            }
                        }
                    }
                }
            }
    
            await _next(context);
            _logger.LogInformation("Finished AdditionalPropertiesValidationMiddleware");
        }
    }