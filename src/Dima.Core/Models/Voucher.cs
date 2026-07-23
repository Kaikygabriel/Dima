using Dima.Core.Abstraction;

namespace Dima.Core.Models;

public class Voucher : Model
{
    private Voucher()
    {
     
    }

    public Voucher(string code, decimal amount,string title,string description, DateTime startDate, DateTime endDate)
    {
        Code = code;
        Amount = amount;
        StartDate = startDate;
        EndDate = endDate;
        Title = title;
        Description = description;
    }

    public string Code { get;private set; } = null!;
    public string Title { get;private set; } = string.Empty;
    public string Description { get;private set; } = string.Empty;
    
    public decimal Amount { get;private set; }
    
    public DateTime StartDate { get;private set; }
    public DateTime EndDate { get;private set; }

    public bool IsActive =>
        StartDate >= DateTime.Now && EndDate <= DateTime.Now;
} 