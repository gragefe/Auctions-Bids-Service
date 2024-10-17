namespace BidsService;

using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class KafkaConsumer : BackgroundService
{
    private IConsumer<string, string> _consumer;
    private ConsumerConfig consumerConfig;
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly IKafkaConsumerHandler _kafkaConsumerHandler;

    public KafkaConsumer(
        IKafkaConsumerHandler kafkaConsumerHandler,
        ILogger<KafkaConsumer> logger,
        IConfiguration configuration)
    {
        _logger = logger;

        _kafkaConsumerHandler = kafkaConsumerHandler;

        consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "auctionkafkagroup-new4",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        _consumer.Subscribe("BidsTopic");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    if (consumeResult != null)
                    {
                        var bid = JsonConvert.DeserializeObject<Bid>(consumeResult.Message.Value);

                        if (bid != null)
                        {
                            await _kafkaConsumerHandler.HandleMessageAsync(bid);
                        }
                    }
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"error to consume event: {e.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Kafka Consumer turning of.");
        }
        finally
        {
            _consumer.Close();
        }
    }
}
