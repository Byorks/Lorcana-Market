namespace Infrastructure.DTOs.Results;

public class ContaResult
{
    public long Id { get; set; }
    public string Nome { get; set; } = null!;
    public decimal Saldo { get; set; }
}
