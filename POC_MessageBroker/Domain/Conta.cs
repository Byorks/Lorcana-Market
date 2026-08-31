using Domain.Enums;

namespace Domain;

public class Conta
{
    public long Id { get; set; }
    public string Nome { get; set; } = null!; // Esse campo não pode ser nulo. O null! significa que iremos garantir que nome não seja nulo e fala pro compilador que não precisa se preocupar com isso. (remove o warning)
    public decimal Saldo => Transacoes.Count > 0 ? Transacoes
            .Where(x => x.StatusTransacao == StatusTransacao.Aprovada 
                        && x.TipoTransacao == TipoTransacao.Deposito)
            .Sum(x => x.Valor) - Transacoes
            .Where(x => x.StatusTransacao == StatusTransacao.Aprovada && x.TipoTransacao == TipoTransacao.Saque)
            .Sum(x => x.Valor) : 0; // Isto seria uma computed property, que não é armazenada no banco de dados, mas é calculada a partir de outras propriedades.
                                    // O Saldo é calculado a partir das transações aprovadas de depósito e saque.

    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}
