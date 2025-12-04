using System.Text.Json;
using fiap_fcg_elastic_func._Shared;
using fiap_fcg_elastic_func.Games;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace fiap_fcg_elastic_func.Promocoes;

public class PromocaoEventConsumer
{
    private readonly ILogger<GameEventConsumer> _logger;
    private readonly ElasticConnector _elastic;

    public PromocaoEventConsumer(ILogger<GameEventConsumer> logger, IConfiguration config)
    {
        _logger = logger;
        _elastic = new ElasticConnector(config);
    }

    [Function("PromocaoEventConsumer")]
    public async Task Run(
        [ServiceBusTrigger("promocao-games", Connection = "SERVICEBUS_CONNECTION")]
        string message)
    {
        _logger.LogInformation($"Evento recebido: {message}");

        var evento = JsonSerializer.Deserialize<PromocaoEvent>(message);

        await _elastic.IndexAsync("promocoes", evento, evento.PromocaoId.ToString());

        _logger.LogInformation("✓ Evento indexado no Elastic!");
    }
}