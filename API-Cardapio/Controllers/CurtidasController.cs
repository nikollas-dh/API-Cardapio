using API_Cardapio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Cardapio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurtidasController : ControllerBase
    {
        private readonly DbCardapioContext ct = new DbCardapioContext();
        public CurtidasController(DbCardapioContext ct) 
        { 
            this.ct = ct;
        }

        [HttpGet]
        public IActionResult Ranking() 
        {
            try
            {
                var pratosMaisCurtidos = ct.Pratos
                    .Select(p => new
                    {
                       Id = p.Id,
                       Nome = p.Nome,
                       Foto =  p.Foto,
                       Curtidas = p.ClienteCurtida.Count()
                    })
                    .OrderByDescending(p => p.Curtidas)
                    .Take(3)
                    .ToList();
                if (!pratosMaisCurtidos.Any()) return NotFound();
                return Ok(pratosMaisCurtidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Erro interno no servidor");
            }
        }

        [HttpPost]
        public IActionResult CurtirDescurtir([FromBody] ClienteCurtida curtida)
        {
            try
            {
                var curtidaBanco = ct.ClienteCurtidas
                    .FirstOrDefault(c => c.IdCliente == curtida.IdCliente && c.IdPrato == curtida.IdPrato);
                
                if (curtidaBanco != null)
                {
                    ct.ClienteCurtidas.Remove(curtidaBanco);
                    ct.SaveChanges();
                    return StatusCode(204, "Curtida removida");
                }
                ct.ClienteCurtidas.Add(curtida);
                ct.SaveChanges();
               return Ok(curtida);
            }
            catch (Exception)
            {

                return StatusCode(500,"Erro interno no servidor");
            }
        }

    }
}
