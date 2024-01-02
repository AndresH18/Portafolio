using Octokit;

namespace Portfolio.Services;

public static class ServiceConfigurationExtensions
{
    /// <summary>
    ///     Registers the Application's services
    /// </summary>
    /// <param name="services">the IServiceCollection to add the services to.</param>
    /// <seealso
    ///     href="https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-8.0#service-lifetime">
    ///     Blazor Service Lifetime
    /// </seealso>
    public static void RegisterGithubServices(this IServiceCollection services)
    {
        services.AddSingleton<IGitHubClient, GitHubClient>(_ => new GitHubClient(new ProductHeaderValue("portfolio")));
        services.AddSingleton<RepositoryService>();
    }
}