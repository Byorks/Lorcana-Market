using Microsoft.AspNetCore.Mvc;
using POC_Lorcana_API_Integration.DTOs.Results;
using POC_Lorcana_API_Integration.Services.Interfaces;

namespace POC_Lorcana_API_Integration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImportsController : ControllerBase
{
    private readonly IImportCardService _importCardService;

    public ImportsController(IImportCardService importCardService)
    {
        _importCardService = importCardService;
    }

    /// <summary>
    /// Importa cards de Lorcana para o banco de dados interno.
    /// </summary>
    /// <returns></returns>
    [HttpPost("Lorcana")]
    public async Task<ImportCardsResult> ImportCards(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _importCardService.ImportCardsAsync(cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            return new ImportCardsResult(0, $"Erro ao importar cards: {ex.Message}");
        }
    }
}
