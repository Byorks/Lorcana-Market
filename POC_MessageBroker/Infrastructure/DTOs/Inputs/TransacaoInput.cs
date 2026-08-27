using Domain.Enums;

namespace Infrastructure.DTOs.Inputs;

public class TransacaoInput
{
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
}
