using Blazored.LocalStorage;
using Octokit;
using Portfolio.Data;

namespace Portfolio;

public class GithubService(
    IGitHubClient client,
    ILocalStorageService storage,
    ILogger<GithubService> logger)
{
    private const string User = "AndresH18";
    private const string CacheKey = "AndresH18/repos";

    public async Task<RepositoryData[]> GetRepositories()
    {
        try
        {
            var repositories = await GetRepos();
            if (repositories != null)
            {
                logger.LogInformation("Retrieving repositories from cache");
                return repositories;
            }

            var githubRepos = await client.Repository.GetAllForUser(User);
            var repoDataTasks = githubRepos.OrderByDescending(x => x.PushedAt)
                .Select(async repo =>
                {
                    var languages = await client.Repository.GetAllLanguages(repo.Id);
                    logger.LogTrace("Repository {name} languages: {languages}", repo.Name,
                        string.Join(", ", languages.Select(l => l.Name)));

                    var orderedLanguages =
                        from language in languages
                        orderby language.NumberOfBytes descending
                        select language.Name;

                    return new RepositoryData(repo.Name, repo.Description, repo.HtmlUrl, orderedLanguages);
                });

            repositories = await Task.WhenAll(repoDataTasks);
            return repositories;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting repositories");
            Error?.Invoke(this, "Error getting repositories");
            return [];
        }
    }

    public event EventHandler<string>? Error;

    private async Task SetRepos(RepositoryData[] repositories)
    {
        var repoStorage = new RepositoryStorage
        {
            Repos = repositories,
        };

        await storage.SetItemAsync(CacheKey, repoStorage);
    }

    private async Task<RepositoryData[]?> GetRepos()
    {
        var repoStorage = await storage.GetItemAsync<RepositoryStorage>(CacheKey);
        if (repoStorage is null)
            return null;

        if (repoStorage.Expiration >= DateTime.Now)
            return repoStorage.Repos;

        await storage.RemoveItemAsync(CacheKey);
        return null;
    }
}

file class RepositoryStorage
{
    public RepositoryData[] Repos { get; set; } = [];
    public DateTime Expiration { get; set; } = DateTime.Now.AddMinutes(5);
}