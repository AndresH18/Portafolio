namespace Portfolio.Data.Models;

// public record RepositoryData(string Name, string? Description, string Url, bool Archived, List<string> Languages);
public record RepositoryData(string Name, string? Description, string Url, List<string> Languages);