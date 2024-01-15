namespace Portfolio.Data.Cache;

using static DateTime;

public interface ICached
{
    public DateTime Expire { get; }
    
    /// <summary>
    ///     Returns true if the current datetime is after <see cref="Expire"/>, false otherwise
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=net-8.0">DateTime.Compare </seealso>
    public bool IsExpired() => Compare(Now, Expire) >= 1;
}