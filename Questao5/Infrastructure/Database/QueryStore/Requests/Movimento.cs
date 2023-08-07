namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public static class Movimento
    {
        public static string InsertMovimento = "INSERT INTO Movimento (idcontacorrente, tipomovimento, valor, datamovimento) VALUES (@IdContaCorrente, @TipoMovimento, @Valor, @DataMovimento);";
        public static string GetSomaCredito = "SELECT coalesce(SUM(VALOR),0) FROM contacorrente CC JOIN movimento MV ON MV.idcontacorrente = CC.idcontacorrente WHERE tipomovimento = 'C' AND CC.numero = @ContaId";
        public static string GetSomaDebito = "SELECT coalesce(SUM(VALOR),0) FROM contacorrente CC JOIN movimento MV ON MV.idcontacorrente = CC.idcontacorrente WHERE tipomovimento = 'D' AND CC.numero = @ContaId;";
    }
}
