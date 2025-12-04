using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using fiap_fcg_elastic_func._Shared;

namespace fiap_fcg_elastic_func.Games;

public class GameEventConsumer
{
    private readonly ILogger<GameEventConsumer> _logger;
    private readonly ElasticConnector _elastic;

    public GameEventConsumer(ILogger<GameEventConsumer> logger, IConfiguration config)
    {
        _logger = logger;
        _elastic = new ElasticConnector(config);
    }

    [Function("GameEventConsumer")]
    public async Task Run(
        [ServiceBusTrigger("events-games", Connection = "SERVICEBUS_CONNECTION")]
        string message)
    {
        _logger.LogInformation($"Evento recebido: {message}");

        var evento = JsonSerializer.Deserialize<GameEvent>(message);

        await _elastic.IndexAsync("games", evento, evento.JogoId.ToString());

        _logger.LogInformation("✓ Evento indexado no Elastic!");
    }
}