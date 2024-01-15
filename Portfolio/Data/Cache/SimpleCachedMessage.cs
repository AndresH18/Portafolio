namespace Portfolio.Data.Cache;

/// <summary>
///     Record used to cache simple messages
/// </summary>
public record SimpleCachedMessage(string Message, DateTime Expire) : ICachedMessage;