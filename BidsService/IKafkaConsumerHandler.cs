namespace BidsService;

public interface IKafkaConsumerHandler
{
    Task HandleMessageAsync(Bid bid);
}
