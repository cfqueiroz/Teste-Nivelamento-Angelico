using System.Globalization;

namespace Questao5.Application.Queries.Responses
{
    public class ContaCorrente
    {
        public string Saldo { get; set; } = "R$ 0,00";

        public long Numero { get; set; }

        public string Nome { get; set; }

        public string DataHoraConsulta { get; set; }

        public ContaCorrente()
        {
            DataHoraConsulta = DateTime.Now.ToString();
        }
        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {Nome}, Saldo: $ {Saldo}";
        }
    }
}
