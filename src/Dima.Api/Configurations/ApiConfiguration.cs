using System.ComponentModel.DataAnnotations;

namespace Dima.Api.Configurations;

public class ApiConfiguration
{
    public static string FrontEndUrl { get; } = "http://localhost:5099/";
    [Required] public string StripeKey { get; set; } = null!;

}