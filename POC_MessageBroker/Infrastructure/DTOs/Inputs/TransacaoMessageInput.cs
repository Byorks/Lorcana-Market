using Domain.Enums;

namespace Infrastructure.DTOs.Inputs;

public class TransacaoMessageInput
{
    public Guid CodigoId { get; set; } // Idempotência
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }

    public TransacaoMessageInput(Guid codigoId, TipoTransacao tipoTransacao, decimal valor)
    {
        CodigoId = codigoId;
        TipoTransacao = tipoTransacao;
        Valor = valor;
    }
}
