using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Dapper;

namespace Questao5.Infrastructure.Services.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private static IDataAccessDapper _dataAccess { get; set; }

        public MovimentoRepository(IDataAccessDapper dataAccessDapper)
        {
            _dataAccess = dataAccessDapper;
        }

        public long InserirMovimento(Movimento Movimento)
        {
            var conta = GetContaAtiva(Movimento.IdContaCorrente);
            if (conta == null)
            {
                throw new BadHttpRequestException("Tipo = \"INVALID_ACCOUNT\", Mensagem = \"Conta inexistente.\"");
            }

            if (Movimento.Valor <= 0)
            {
                throw new BadHttpRequestException("Tipo = \"INVALID_VALUE\", Mensagem = \"O valor da movimentação deve ser positivo.\"");
            }

            if (Movimento.TipoEnum is null)
            {
                throw new BadHttpRequestException("Tipo = \"INVALID_TYPE\", Mensagem = \"O tipo de movimento deve ser 'C' (crédito) ou 'D' (débito).\"");
            }

            var id = _dataAccess.InsertLongReturn(Database.QueryStore.Requests.Movimento.InsertMovimento,
                new { IdContaCorrente = Movimento.IdContaCorrente, TipoMovimento = Movimento.TipoMovimento, Valor = Movimento.Valor, DataMovimento = DateTime.Now });

            return id;
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
