namespace Portfolio.Data.Cache;

public interface ICachedMessage : ICached
{
    public string Message { get; }
}