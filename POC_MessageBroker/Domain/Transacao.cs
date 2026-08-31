using Domain.Enums;

namespace Domain;

public class Transacao
{
    public long Id { get; set; }
    public Guid CodigoId { get; set; } // Campo para armazenar o código único da transação (idempotência)
    public TipoTransacao TipoTransacao { get; set; }
    public StatusTransacao StatusTransacao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataHora { get; set; }

    public string Observacao { get; set; }

    public long ContaId { get; set; }
    public Conta? Conta { get; set; }

    public Transacao(Guid codigoId, TipoTransacao tipoTransacao, StatusTransacao statusTransacao, decimal valor, DateTime dataHora, long contaId, string observacao = "")
    {
        CodigoId = codigoId;
        TipoTransacao = tipoTransacao;
        StatusTransacao = statusTransacao;
        Valor = valor;
        DataHora = dataHora;
        ContaId = contaId;
        Observacao = observacao;
    }
}
