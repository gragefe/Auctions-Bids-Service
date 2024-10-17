namespace Application.Services;

using Application.DTO;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Model.Interfaces;
using Infrastructure.Crosscutting.Enum;
using Infrastructure.Crosscutting.Validations;
using System.ComponentModel.DataAnnotations;
using DTO = Application.DTO;


public class KafkaConsumerHandlerService : IKafkaConsumerHandlerService
{
    private readonly IRuntimeAuctionsGateway _runtimeAuctionsGateway;

    public KafkaConsumerHandlerService(
        IRuntimeAuctionsGateway runtimeAuctionsGateway)
    {
        _runtimeAuctionsGateway = runtimeAuctionsGateway;
    }

    public async Task HandleMessageAsync(Bid bid)
    {
        await _runtimeAuctionsGateway.Patch(bid);
    }
}