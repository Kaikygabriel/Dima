namespace Dima.Core.Requests;

public abstract record Request
{
    public Guid UserId { get; set; }
}