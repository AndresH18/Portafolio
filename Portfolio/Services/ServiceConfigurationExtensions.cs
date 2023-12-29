using Octokit;

namespace Portfolio.Services;

public static class ServiceConfigurationExtensions
{
    public static void RegisterGithubServices(this IServiceCollection services)
    {
        services.AddSingleton<IGitHubClient, GitHubClient>(_ => new GitHubClient(new ProductHeaderValue("portfolio")));
        services.AddSingleton<GitHubService>();
    }
}