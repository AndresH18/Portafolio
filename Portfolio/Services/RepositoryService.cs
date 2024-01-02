using Octokit;
using Portfolio.Data.Models;

namespace Portfolio.Services;

public class RepositoryService(IGitHubClient gitHubClient, ILogger<RepositoryService> logger)
{
    /// <summary>
    ///     Retrieves repositories for a given page from GitHub.
    /// </summary>
    /// <returns>A list of RepositoryData objects.</returns>
    public async Task<IReadOnlyList<RepositoryData>> GetRepositories(int page = 1)
    {
        var repositories =
            await gitHubClient.Repository.GetAllForUser("AndresH18", new ApiOptions { StartPage = page });
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

        var repositoriesData = await Task.WhenAll(repoDataTasks);

       

        return repositoriesData;
    }
}