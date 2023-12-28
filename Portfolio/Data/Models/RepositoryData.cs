namespace Portfolio.Data.Models;

// public record RepositoryData(string Name, string? Description, string Url, string Language);
public record RepositoryData(string Name, string? Description, string Url, List<string> Languages);