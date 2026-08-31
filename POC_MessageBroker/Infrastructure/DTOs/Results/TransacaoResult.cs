using Domain.Enums;

namespace Infrastructure.DTOs.Results;

public class TransacaoResult
{
    public long Id { get; set; }
    public long ContaId { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
    public Guid CodigoId { get; set; } // Campo para armazenar o código único da transação (idempotência)
    public StatusTransacao StatusTransacao { get; set; }
    public DateTime DataHora { get; set; }

    public string Observacao { get; set; }
}
