using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Dapper;
using Response = Questao5.Application.Queries.Responses;
namespace Questao5.Infrastructure.Services.Repository
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private static IDataAccessDapper _dataAccess { get; set; }
        public ContaCorrenteRepository(IDataAccessDapper dataAccessDapper)
        {
            _dataAccess = dataAccessDapper;
        }

        public async Task<Response.ContaCorrente> GetSaldo(long id)
        {
            var response = new Response.ContaCorrente();
            var conta = GetContaAtiva(id);
            

            var valorCredito = _dataAccess.IReturn<decimal>(Database.QueryStore.Requests.Movimento.GetSomaCredito, new { ContaId = id }).FirstOrDefault();
            var valorDebito = _dataAccess.IReturn<decimal>(Database.QueryStore.Requests.Movimento.GetSomaDebito, new { ContaId = id }).FirstOrDefault();

            response.Numero = conta.Numero;
            response.Nome = conta.Nome;
            response.Saldo = String.Format("{0:C}", (valorCredito + (valorDebito * -1)));
            return response;
        }

        private ContaCorrente GetContaAtiva(long id)
        {
           var conta = _dataAccess.IReturn<ContaCorrente>(Database.QueryStore.Requests.ContaCorrente.GetConta, new { ContaId = id }).FirstOrDefault();
            if (conta is null)
                throw new BadHttpRequestException("Tipo = \"INVALID_ACCOUNT\", Mensagem = \"Conta inexistente.\"");
            if (!conta.Ativo)
                throw new BadHttpRequestException("Tipo = \"INACTIVE_ACCOUNT\", Mensagem = \"Conta inativa.\"");
            return conta;
        }
    }
}
