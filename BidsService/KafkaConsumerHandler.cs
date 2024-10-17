namespace BidsService;

using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

public class KafkaConsumerHandler: IKafkaConsumerHandler
{
    public async Task HandleMessageAsync(Bid bid)
    {
        var requestUrl = "https://auctions-runtime-api/addbid";

        var patchData = new
        {
            op = "add",
            path = "/Bids/-",
            value = bid
        };

        var content = new StringContent(JsonConvert.SerializeObject(new[] { patchData }), Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUrl)
        {
            Content = content
        };

        var _httpClient = new HttpClient();

        var response = await _httpClient.SendAsync(request);
    }
}