namespace Dima.Core.Abstraction;

public abstract class Request
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