using Domain.Enums;

namespace Api.DTOs.Inputs;

public class TransacaoInput
{
    public TipoTransacao TipoTransacao{ get; set; }
    public decimal Valor { get; set; }
}
