using Octokit;

namespace Portfolio.Services;

public static class ServiceConfigurationExtensions
{
    public static void RegisterGithubServices(this IServiceCollection services)
    {
        services.AddSingleton<GitHubClient>(_ => new GitHubClient(new ProductHeaderValue("portfolio")));
    }
}