namespace Dima.Core.Abstraction;

public abstract class Model
{
    public Guid Id { get; init; } = Guid.NewGuid();
}