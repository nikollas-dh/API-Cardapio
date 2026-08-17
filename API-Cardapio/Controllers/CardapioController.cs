using API_Cardapio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Cardapio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioController : ControllerBase
    {
        private readonly DbCardapioContext ct = new DbCardapioContext();

        public CardapioController(DbCardapioContext ct) 
        { 
            this.ct = ct;
        }

        [HttpGet("{id:int}")]
        public IActionResult CardapioRestauranteId([FromRoute]int id)
        {
            try 
            { 
               var cardapioRestaurante = ct.Cardapios.Where(r => r.RestauranteId == id).Select(c => new
               {
                   c.Id,
                   Restaurante = c.Restaurante.Nome,
                   c.Prato.Nome,
                   c.Prato.Descricao,
                   c.Valor,
                   c.Prato.Foto,
                   //Curtidas = ct.ClienteCurtidas.Where(like=>like.IdPrato==c.PratoId && like.).Count(),
               }).ToList();
                if (cardapioRestaurante.Count ==0) 
                { 
                    return NotFound("Restaurante não possue pratos cadastrados no sistema.");
                }
               return Ok (cardapioRestaurante);
            }
            catch (Exception)
            {
                return BadRequest("Erro interno no servidor!");
            }
        }
    }
}
