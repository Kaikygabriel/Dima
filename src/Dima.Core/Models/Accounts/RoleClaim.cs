namespace Dima.Core.Models.Accounts;

public class RoleClaim
{
    public RoleClaim()
    {
        
    }
    public RoleClaim(string issuer, string type, string value, string valueType, string originalIssuer)
    {
        Issuer = issuer;
        Type = type;
        Value = value;
        ValueType = valueType;
        OriginalIssuer = originalIssuer;
    }

    public string Issuer { get; set; } = "";
    public string Type { get; set; } = "";
    public string Value { get; set; } = "";
    public string ValueType { get; set; } = "";
    public string OriginalIssuer { get; set; } = "";
}