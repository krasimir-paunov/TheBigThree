using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;
using static TheBigThree.Web.ViewModels.RawgSearchResultViewModel;

namespace TheBigThree.Services
{
    public class RawgService : IRawgService
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;
        private const string BaseUrl = "https://api.rawg.io/api";

        public RawgService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.apiKey = configuration["RawgApi:ApiKey"] ?? string.Empty;
        }

        public bool IsConfigured() => !string.IsNullOrWhiteSpace(apiKey);

        public async Task<IEnumerable<RawgSearchResultViewModel>> SearchGamesAsync(string query)
        {
            if (!IsConfigured() || string.IsNullOrWhiteSpace(query))
            {
                return Enumerable.Empty<RawgSearchResultViewModel>();
            }


            try
            {
                string url = $"{BaseUrl}/games?key={apiKey}&search={Uri.EscapeDataString(query)}&page_size=5";

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<RawgSearchResultViewModel>();
                }

                string json = await response.Content.ReadAsStringAsync();

                JsonDocument doc = JsonDocument.Parse(json);

                JsonElement results = doc.RootElement.GetProperty("results");

                List<RawgSearchResultViewModel> games = new List<RawgSearchResultViewModel>();

                foreach (JsonElement game in results.EnumerateArray())
                {
                    games.Add(new RawgSearchResultViewModel
                    {
                        Id = game.GetProperty("id").GetInt32(),
                        Name = game.TryGetProperty("name", out JsonElement name) ? name.GetString() ?? "" : "",
                        BackgroundImage = game.TryGetProperty("background_image", out JsonElement img) && img.ValueKind != JsonValueKind.Null ? img.GetString() ?? "" : "",
                        Released = game.TryGetProperty("released", out JsonElement released) && released.ValueKind != JsonValueKind.Null ? released.GetString() ?? "" : "",
                        Rating = game.TryGetProperty("rating", out JsonElement rating) ? rating.GetDouble() : 0,
                        Genres = game.TryGetProperty("genres", out JsonElement genresEl)
                        ? genresEl.EnumerateArray()
                            .Select(g => new RawgGenreItem { Name = g.GetProperty("name").GetString() ?? "" })
                            .ToList()
                        : new List<RawgGenreItem>()
                                        });
                }

                return games;
            }
            catch
            {
                return Enumerable.Empty<RawgSearchResultViewModel>();
            }
        }

        public async Task<RawgGameViewModel?> GetGameDetailsAsync(string gameName)
        {
            if (!IsConfigured() || string.IsNullOrWhiteSpace(gameName))
                return null;

            try
            {
                string searchUrl = $"{BaseUrl}/games?key={apiKey}&search={Uri.EscapeDataString(gameName)}&page_size=1";

                HttpResponseMessage searchResponse = await httpClient.GetAsync(searchUrl);

                if (!searchResponse.IsSuccessStatusCode) return null;

                string searchJson = await searchResponse.Content.ReadAsStringAsync();

                JsonDocument searchDoc = JsonDocument.Parse(searchJson);

                JsonElement results = searchDoc.RootElement.GetProperty("results");

                if (!results.EnumerateArray().Any()) return null;

                JsonElement firstResult = results.EnumerateArray().First();

                int gameId = firstResult.GetProperty("id").GetInt32();

                string detailUrl = $"{BaseUrl}/games/{gameId}?key={apiKey}";

                HttpResponseMessage detailResponse = await httpClient.GetAsync(detailUrl);

                if (!detailResponse.IsSuccessStatusCode) return null;

                string detailJson = await detailResponse.Content.ReadAsStringAsync();

                JsonDocument detailDoc = JsonDocument.Parse(detailJson);

                JsonElement g = detailDoc.RootElement;

                string screenshotUrl = $"{BaseUrl}/games/{gameId}/screenshots?key={apiKey}";

                HttpResponseMessage screenshotResponse = await httpClient.GetAsync(screenshotUrl);

                List<string> screenshots = new List<string>();

                if (screenshotResponse.IsSuccessStatusCode)
                {
                    string screenshotJson = await screenshotResponse.Content.ReadAsStringAsync();

                    JsonDocument screenshotDoc = JsonDocument.Parse(screenshotJson);

                    foreach (JsonElement shot in screenshotDoc.RootElement.GetProperty("results").EnumerateArray().Take(4))
                    {
                        if (shot.TryGetProperty("image", out JsonElement imgEl))
                            screenshots.Add(imgEl.GetString() ?? "");
                    }
                }

                string developers = "";
                if (g.TryGetProperty("developers", out JsonElement devs))
                {
                    developers = string.Join(", ", devs.EnumerateArray().Select(d => d.GetProperty("name").GetString() ?? ""));
                }

                string platforms = "";

                if (g.TryGetProperty("platforms", out JsonElement plats))
                {
                    platforms = string.Join(", ", plats.EnumerateArray().Select(p => p.GetProperty("platform").GetProperty("name").GetString() ?? "").Take(4));
                }

                string genres = "";

                if (g.TryGetProperty("genres", out JsonElement genreEl))
                {
                    genres = string.Join(", ", genreEl.EnumerateArray().Select(ge => ge.GetProperty("name").GetString() ?? ""));
                }

                int? metacritic = null;

                if (g.TryGetProperty("metacritic", out JsonElement mc) && mc.ValueKind != JsonValueKind.Null)
                    metacritic = mc.GetInt32();

                return new RawgGameViewModel
                {
                    Name = g.TryGetProperty("name", out JsonElement n) ? n.GetString() ?? "" : "",
                    BackgroundImage = g.TryGetProperty("background_image", out JsonElement bg) && bg.ValueKind != JsonValueKind.Null ? bg.GetString() ?? "" : "",
                    Released = g.TryGetProperty("released", out JsonElement rel) && rel.ValueKind != JsonValueKind.Null ? rel.GetString() ?? "" : "",
                    Rating = g.TryGetProperty("rating", out JsonElement rat) ? rat.GetDouble() : 0,
                    MetacriticScore = metacritic,
                    Developers = developers,
                    Platforms = platforms,
                    Genres = genres,
                    Screenshots = screenshots
                };
            }
            catch
            {
                return null;
            }
        }
    }
}