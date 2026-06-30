namespace POC_Lorcana_API_Integration.DTOs.Results;

public record ImportCardsResult(
    int TotalCardsImported,
    string Error = ""
);
