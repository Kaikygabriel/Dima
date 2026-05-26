namespace Dima.Core.Requests;

public abstract record Request
{
    public Request()
    {
        
    }

    public Request(Guid userId)
    {
        UserId = userId;
    }
    public Guid UserId { get; set; }
}