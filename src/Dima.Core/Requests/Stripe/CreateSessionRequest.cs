using Dima.Core.Models;

namespace Dima.Core.Requests.Stripe;

public record  CreateSessionRequest : Request
{
    public Guid Id { get; set; }
}