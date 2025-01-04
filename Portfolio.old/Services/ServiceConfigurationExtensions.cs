using System.Net.Http.Json;
using Blazored.SessionStorage;
using Blazored.Toast;
using Octokit;
using Portfolio.Data.Models;
using Portfolio.Helpers;

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
    public static void ConfigureAppServices(this IServiceCollection services)
    {
        services.RegisterGithubServices();
        services.RegisterBlazoredServices();
        services.AddScoped<BadgeLanguageHelper>();
    }

    /// <summary>
    ///     Registers GitHub Services
    /// </summary>
    /// <param name="services">the IServiceCollection to add the services to.</param>
    private static void RegisterGithubServices(this IServiceCollection services)
    {
        services.AddSingleton<IGitHubClient, GitHubClient>(_ => new GitHubClient(new ProductHeaderValue("portfolio")));
        services.AddSingleton<RepositoryService>();
    }

    /// <summary>
    ///     Registers the Blazored services used in the app
    /// </summary>
    /// <param name="services"></param>
    /// <see href="https://github.com/Blazored">Blazored</see>
    private static void RegisterBlazoredServices(this IServiceCollection services)
    {
        services.AddBlazoredToast();
        services.AddBlazoredSessionStorage();
    }
}