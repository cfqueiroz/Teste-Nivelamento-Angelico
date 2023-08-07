using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Repository
{
    public interface IMovimentoRepository
    {
        long InserirMovimento(Movimento Movimento);
    }
}
