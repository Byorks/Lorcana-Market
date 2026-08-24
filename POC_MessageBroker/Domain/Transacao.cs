using Domain.Enums;

namespace Domain;

public class Transacao
{
    public long Id { get; set; }
    public Guid CodigoId { get; set; } // Campo para armazenar o código único da transação (idempotência)
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataHora { get; set; }
}
