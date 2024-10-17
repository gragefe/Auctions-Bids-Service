namespace BidsService;

public class Bid
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public Guid AuctionId { get; set; }

    public double Value { get; set; }
}