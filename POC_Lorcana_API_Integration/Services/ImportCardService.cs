using POC_Lorcana_API_Integration.Context;
using POC_Lorcana_API_Integration.Domains;
using POC_Lorcana_API_Integration.DTOs.Results;
using POC_Lorcana_API_Integration.Services.Interfaces;

namespace POC_Lorcana_API_Integration.Services;

public class ImportCardService : IImportCardService
{
    private readonly ILorcanaApiIntegration _lorcanaApi;
    private readonly HttpClient _httpClient;
    private readonly ApiDbContext _db;

    public ImportCardService(ILorcanaApiIntegration lorcanaApi, ApiDbContext db)
    {
        _lorcanaApi = lorcanaApi;
        _httpClient = new HttpClient();
        _db = db;
    }

    public async Task<ImportCardsResult> ImportCardsAsync(CancellationToken cancellationToken)
    {
        var result = await _lorcanaApi.GetAllCardsAsync(cancellationToken);

        // Realiza a aquisição das imagens e a conversão para Base64 de forma assíncrona e paralela
        var tasks = result.Cards.Select(async card =>
        {
            //var imageBase64 = await GetImageBase64Async(card.ImageUrl, cancellationToken);

            return new Card(
                card.Artist,
                card.SetName,
                card.Classifications,
                DateTime.Parse(card.DateAdded),
                card.SetNum,
                card.Color,
                card.Gamemode,
                card.Franchise,
                card.ImageUrl,
                "imageBase64",
                card.Cost,
                card.Inkable,
                card.Name,
                card.Type,
                card.Lore,
                card.Rarity,
                card.FlavorText,
                card.UniqueId,
                card.CardNum,
                card.BodyText,
                card.Willpower,
                DateTime.Parse(card.DateModified),
                card.Strength,
                card.SetId);
        });

        var cardsToImport = (await Task.WhenAll(tasks)).ToList();

        if (cardsToImport.Count > 0)
        {
            await _db.Cards.AddRangeAsync(cardsToImport, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);
        }

        return new ImportCardsResult(cardsToImport.Count);
    }

    public async Task<string> GetImageBase64Async(string imageUrl, CancellationToken cancellationToken)
    {
        var bytes = await _httpClient.GetByteArrayAsync(imageUrl, cancellationToken);
        return Convert.ToBase64String(bytes);
    }
}
