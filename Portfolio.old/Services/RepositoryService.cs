using Octokit;
using Portfolio.Data.Models;

namespace Portfolio.Services;

public class RepositoryService(IGitHubClient gitHubClient, ILogger<RepositoryService> logger)
{
    private IEnumerable<RepositoryData> Repositories { get; set; } = Enumerable.Empty<RepositoryData>();

    /// <summary>
    ///     Load repositories from GitHub.
    /// </summary>
    public async Task LoadRepositories()
    {
        try
        {
            var repositories =
                await gitHubClient.Repository.GetAllForUser("AndresH18", new ApiOptions { StartPage = 1 });
            var repoDataTasks = repositories.OrderByDescending(r => r.PushedAt)
                .Select(async repo =>
                {
                    var languages = await gitHubClient.Repository.GetAllLanguages(repo.Id);
                    languages.ToList().ForEach(l =>
                        logger.LogTrace("Repository '{repository}' language: {language}", repo.Name, l.Name));

                    // var orderedLanguagesNames = languages
                    //     .OrderByDescending(l => l.NumberOfBytes)
                    //     .Select(l => l.Name)
                    //     .ToList();

                    var orderedLanguagesNames =
                        from l in languages
                        orderby l.NumberOfBytes descending
                        select l.Name;

                    var repoData =
                        new RepositoryData(repo.Name, repo.Description, repo.HtmlUrl, orderedLanguagesNames.ToList());

                    return repoData;
                });

            Repositories = await Task.WhenAll(repoDataTasks);
            RepositoriesLoaded?.Invoke(this, Repositories);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting github repositories");
            RequestFailed?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? RequestFailed;
    public event EventHandler<IEnumerable<RepositoryData>>? RepositoriesLoaded;
}