namespace Application.Services.Interfaces;

using Application.DTO;

public interface IKafkaConsumerHandlerService
{
    Task HandleMessageAsync(Bid bid);
}