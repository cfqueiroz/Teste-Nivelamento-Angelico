using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Services.Repository;
using System.Net;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/contacorrente")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ContaCorrenteController(IMovimentoRepository repository, IContaCorrenteRepository contaCorrenteRepository)
        {
            _movimentoRepository = repository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        /// <summary>
        /// Movimentar:
        ///     Movimentar a conta com créditos ou débitos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna o item solicitado</response>
        /// <response code="400">Regras de negócios inválidas ou solicitação mal formatada</response>   
        /// <response code="500">Erro Interno do Servidor</response>        ///  
        /// <response code="401">Não autorizado</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> MovimentarConta([FromBody] Movimento movimentarConta)
        {
            return Ok(_movimentoRepository.InserirMovimento(movimentarConta));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna o item solicitado</response>
        /// <response code="400">Regras de negócios inválidas ou solicitação mal formatada</response>   
        /// <response code="500">Erro Interno do Servidor</response> 
        /// <response code="401">Não autorizado</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaldoConta(int id)
        {
            return Ok(_contaCorrenteRepository.GetSaldo(id).ToString());
        }
    }
}
