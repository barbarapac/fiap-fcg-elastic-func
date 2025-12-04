using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace fiap_fcg_elastic_func._Shared;

public class ElasticConnector
{
    private readonly string _uri;
    private readonly string _apiKey;
    private readonly HttpClient _http;

    public ElasticConnector(IConfiguration config)
    {
        _uri = Environment.GetEnvironmentVariable("ELASTIC_URI") ?? config["ELASTIC_URI"];
        _apiKey = Environment.GetEnvironmentVariable("ELASTIC_API_KEY") ?? config["ELASTIC_API_KEY"];

        _http = new HttpClient();
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", _apiKey);
    }

    public async Task IndexAsync(string index, object data, string id)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PutAsync($"{_uri}/{index}/_doc/{id}", content);
        var body = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[ELASTIC STATUS] {response.StatusCode}");
        Console.WriteLine($"[ELASTIC BODY] {body}");
    }

}