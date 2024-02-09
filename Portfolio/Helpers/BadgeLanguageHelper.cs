using System.Net.Http.Json;
using Portfolio.Data.Models;

namespace Portfolio.Helpers;

public class BadgeLanguageHelper
{
    private LanguageData[] _languages = [];
        // http.GetFromJsonAsync<LanguageData[]>("data/language-data.json").Result!;
    // public BadgeLanguageHelper(HttpClient http) { Languages = http.GetFromJsonAsync<LanguageData[]>("data/language-data.json").Result; }

    public BadgeLanguageHelper(HttpClient httpClient)
    {
        GetLanguages(httpClient);
    }

    public LanguageData? this[string language] =>
        _languages.FirstOrDefault(l => l.Name.Equals(language, StringComparison.OrdinalIgnoreCase));

    private async void GetLanguages(HttpClient httpClient)
    {
        _languages = (await httpClient.GetFromJsonAsync<LanguageData[]>("data/language-data.json"))!;
    }
}