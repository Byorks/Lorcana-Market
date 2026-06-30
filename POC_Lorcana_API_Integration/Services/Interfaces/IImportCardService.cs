using POC_Lorcana_API_Integration.DTOs.Results;

namespace POC_Lorcana_API_Integration.Services.Interfaces;

public interface IImportCardService
{
    Task<ImportCardsResult> ImportCardsAsync(CancellationToken cancellationToken);
}
