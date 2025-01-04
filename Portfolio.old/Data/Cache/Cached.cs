namespace Portfolio.Data.Cache;

/// <summary>
///     Represents cached 'flags'. If it is present then it's true, kinda. <br/>
/// </summary>
/// <param name="Expire">When the cached item expires</param>
/// <seealso cref="ICached"/>
public record Cached(DateTime Expire) : ICached;