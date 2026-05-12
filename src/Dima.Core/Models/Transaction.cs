using Dima.Core.Abstraction;
using Dima.Core.Enum;

namespace Dima.Core.Models;

public class Transaction : Model
{
    public string Title { get; set; } = string.Empty;

    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? PaidOrReceivedAt { get; set; }

    public ETypeTransaction EType { get; set; } = ETypeTransaction.Out;

    public Decimal Amount { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public Guid UserId { get; set; }
}