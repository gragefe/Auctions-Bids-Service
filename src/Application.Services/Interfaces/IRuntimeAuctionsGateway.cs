namespace Application.Services.Interfaces;

using Application.DTO;
using Infrastructure.Crosscutting.GatewayEntites;

public interface IRuntimeAuctionsGateway
{
    Task Patch(Bid bid);
}