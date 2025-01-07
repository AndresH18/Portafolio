namespace Portfolio.Data;

public record RepositoryData(string Name, string? Description, string Url, params IEnumerable<string> Languages);