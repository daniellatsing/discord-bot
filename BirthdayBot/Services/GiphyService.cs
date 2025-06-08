using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class GiphyService
{
  private readonly HttpClient _http;
  private readonly string? _apiKey;

  public GiphyService(HttpClient http, IConfiguration config)
  {
    _http = http;
    _apiKey = config["Giphy:ApiKey"];
  }

  public async Task<string?> GetBirthdayGifUrlAsync()
  {
    var res = await _http.GetFromJsonAsync<JsonElement>($"https://api.giphy.com/v1/gifs/search?api_key={_apiKey}&q=birthday&limit=1");
    return res.GetProperty("data")[0].GetProperty("url").GetString();
  }
}