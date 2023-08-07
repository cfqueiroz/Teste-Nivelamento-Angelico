namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public static class ContaCorrente
    {
        public static string GetConta = $"SELECT idcontacorrente, ativo FROM ContaCorrente WHERE idcontacorrente = @Id";
    }
}
