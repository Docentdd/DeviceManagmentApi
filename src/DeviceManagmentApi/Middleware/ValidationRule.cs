namespace DeviceManagmentApi.Middleware;

public class ValidationRule
{
    public string ParamName { get; set; } = string.Empty;
    public string Regex { get; set; } = string.Empty;
}