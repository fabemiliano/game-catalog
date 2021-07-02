using ApiCatalogo.Expections;
using ApiCatalogo.InputModel;
using ApiCatalogo.Services;
using ApiCatalogo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploApiCatalogoJogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IGameService _gameService;

        public JogosController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="page">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantity">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Get(page, quantity);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idGame">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>   
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<GameViewModel>> Obter([FromRoute] Guid idGame)
        {
            var game = await _gameService.Get(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="gameInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InserirJogo([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var jogo = await _gameService.Insert(gameInputModel);

                return Ok(jogo);
            }
            catch (NotRegisteredGame ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        /// <summary>
        /// Atualizar um jogo no catálogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser atualizado</param>
        /// <param name="jogoInputModel">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);

                return Ok();
            }
            catch (NotRegisteredGame ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        /// <summary>
        /// Atualizar o preço de um jogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser atualizado</param>
        /// <param name="price">Novo preço do jogo</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);

                return Ok();
            }
            catch (NotRegisteredGame ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        /// <summary>
        /// Excluir um jogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Delete(idGame);

                return Ok();
            }
            catch (NotRegisteredGame ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

    }
}
