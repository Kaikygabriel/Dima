using Dima.Core.Models;

namespace Dima.Core.Requests.Stripe;

public record  CreateSessionRequest : Request
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductSummary { get; set; }= string.Empty;
    public long Total { get; set; } 
}