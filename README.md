# fiap-fcg-elastic-func

Esta Azure Function é responsável por consumir eventos publicados no Azure Service Bus relacionados a jogos, promoções/vendas, e indexá-los no Elasticsearch para permitir consultas rápidas e análises futuras.

- A Game API publica eventos como `JOGO_CADASTRADO` e `PROMOCAO_ATUALIZADA`
- O Service Bus retém os eventos de forma assíncrona
- A Azure Function consome os eventos
- Os dados são indexados no Elastic

## Exemplo de evento recebido

### GameEvent
```json
{
  "Tipo": "JOGO_CADASTRADO",
  "JogoId": 3,
  "Nome": "FIFA 2025",
  "Preco": 300,
  "DataEvento": "2025-12-03T15:41:20.158Z"
}
```

### PromocaoEvent
```json
{
  "PromocaoId": 1,
  "Desconto": 25.50,
  "Tipo": "PROMOCAO_CRIADA",
  "Titulo": "Black Friday Games",
  "DataInicio": "2025-11-25T00:00:00Z",
  "DataFim": "2025-11-30T23:59:59Z",
  "Jogos": [
    {
      "JogoId": 3,
      "Nome": "FIFA 2025",
      "Preco": 300
    }
  ],
  "DataEvento": "2025-12-03T15:41:20.158Z"
}
```

## Indexação no Elasticsearch

Cada jogo, promoção ou venda é indexado usando o seu `_id`, garantindo que não haja duplicação.

- **Índice `games`**: Eventos relacionados a jogos
- **Índice `promocoes`**: Eventos relacionados a promoções

## Estrutura do projeto

```
fiap-fcg-elastic-func/
├── Games/
│   ├── GameEvent.cs
│   └── GameEventConsumer.cs
├── Promocoes/
│   ├── PromocaoEvent.cs
│   └── PromocaoEventConsumer.cs
├── _Shared/
│   ├── ElasticConnector.cs
│   └── ApiKeyHttpHandler.cs
├── Program.cs
├── host.json
├── local.settings.json
└── fiap-fcg-elastic-func.csproj
```

## Pré-requisitos

| Requisito | Link para Download |
| --------- | ------------------ |
| `.NET SDK 8.0` | [Baixar aqui](https://dotnet.microsoft.com/en-us/download) |
| `Azure Functions Core Tools` | [Baixar aqui](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local) |

## Tecnologias Utilizadas

| Tecnologia | Versão | Descrição |
| ---------- | ------ | --------- |
| `.NET` | 8.0 | Framework principal |
| `Azure Functions` | v4 | Plataforma serverless |
| `Azure Service Bus` | - | Mensageria |
| `Elasticsearch` | 7.17.5 | Motor de busca |
| `Application Insights` | - | Monitoramento |

## Variáveis de Ambiente

| Variável | Descrição | Obrigatório | Exemplo |
| -------- | --------- | ----------- | ------- |
| `FUNCTIONS_WORKER_RUNTIME` | Runtime das Functions | Sim | `dotnet-isolated` |
| `SERVICEBUS_CONNECTION` | Connection string do Service Bus | Sim | `Endpoint=sb://...` |
| `ELASTIC_URI` | URI do cluster Elasticsearch | Sim | `https://cluster.es.region.azure.elastic.cloud:443` |
| `ELASTIC_API_KEY` | API Key do Elasticsearch | Sim | `encoded-api-key` |

## Execução Local

1. Clone o repositório
2. Configure as variáveis no `local.settings.json`
3. Execute os comandos:

```bash
# Restaurar dependências
dotnet restore

# Executar localmente
func start
```


## Filas do Service Bus

- **`events-games`**: Recebe eventos de jogos
- **`promocao-games`**: Recebe eventos de promoções