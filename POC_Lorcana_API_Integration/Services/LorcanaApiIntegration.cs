using POC_Lorcana_API_Integration.DTOs.Results;
using POC_Lorcana_API_Integration.Services.Interfaces;
using System.Text.Json;

namespace POC_Lorcana_API_Integration.Services;

public class LorcanaApiIntegration : ILorcanaApiIntegration
{
    private readonly HttpClient _client;

    public LorcanaApiIntegration()
    {
        _client = new HttpClient();
    }

    public async Task<CardsIntegrationResult> GetAllCardsAsync(CancellationToken cancellationToken)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("https://api.lorcana-api.com/bulk/cards", cancellationToken);

            // Garante que a requisição foi bem sucedida, caso contrário lança uma exceção
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            // Desserializa o JSON para um objeto lista de CardIntegrationResult
            var cardsResult = JsonSerializer.Deserialize<List<CardIntegrationResult>>(responseBody);

            if (cardsResult == null)
                throw new Exception("Falha ao desserializar a resposta da API de cards.");

            return new CardsIntegrationResult(cardsResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao tentar adquirir todos os cards: {ex.Message}");
            return new CardsIntegrationResult();
        }
    }
}
