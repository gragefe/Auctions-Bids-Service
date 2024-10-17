namespace Infrastructure.Gateways.HttpGateways;

using Application.DTO;
using Application.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

public class RuntimeAuctionsGateway : IRuntimeAuctionsGateway
{
    private readonly HttpClient _httpClient;

    public RuntimeAuctionsGateway(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Patch(Bid bid)
    {
        var stopherer = "";

        var requestUrl = "https://api.exemplo.com/your-object-endpoint";

        // Aqui definimos a operação PATCH para adicionar "item3" na lista
        var patchData = new
        {
            op = "add",              // Operação de adição
            path = "/Bids/-",     // Caminho para a lista (usando "-" para adicionar ao final da lista)
            value = bid         // O valor que queremos adicionar (ex: "item3")
        };

        // Serializa o patchData em JSON
        var content = new StringContent(JsonConvert.SerializeObject(new[] { patchData }), Encoding.UTF8, "application/json");

        // Cria a requisição PATCH manualmente
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUrl)
        {
            Content = content
        };

        // Envia a requisição
        var response = await _httpClient.SendAsync(request);

        // Verifica a resposta
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Item adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine($"Falha ao adicionar item: {response.StatusCode}");
        }
    }
}