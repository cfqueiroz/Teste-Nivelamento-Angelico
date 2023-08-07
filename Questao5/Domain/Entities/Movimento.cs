namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public Guid IdMovimento { get; set; }
        public virtual long IdContaCorrente { get; set; }
        public virtual ContaCorrente ContaCorrente { get; set; } = new ContaCorrente();
        public string DataMovimento { get; set; } = DateTime.Now.ToShortDateString();
        public string TipoMovimento { get; set; } = "C";
        public decimal Valor { get; set; }

        public Enumerators.Movimento? TipoEnum
        {
            get
            {
                if (TipoMovimento.Trim() == "C")
                    return Enumerators.Movimento.Credito;
                if(TipoMovimento.Trim() == "D")
                    return Enumerators.Movimento.Debito;
                return null;
            }
        }

        public Movimento()
        {
            IdMovimento = Guid.NewGuid();
        }
    }
}
