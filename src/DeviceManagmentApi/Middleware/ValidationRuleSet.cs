namespace DeviceManagmentApi.Middleware;

public class ValidationRuleSet
{
    public string Type { get; set; } = string.Empty;
    public string PreRequestName { get; set; } = string.Empty;
    public string PreRequestValue { get; set; } = string.Empty;
    public List<ValidationRule> Rules { get; set; } = new();
}