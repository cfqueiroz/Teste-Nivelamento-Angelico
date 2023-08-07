using Response = Questao5.Application.Queries.Responses;
namespace Questao5.Infrastructure.Services.Repository
{
    public interface IContaCorrenteRepository
    {
        Task<Response.ContaCorrente> GetSaldo(long id);
    }
}
