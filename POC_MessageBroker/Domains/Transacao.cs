using POC_MessageBroker.Domains.Enums;

namespace POC_MessageBroker.Domains;

public class Transacao
{
    public long Id { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
}
