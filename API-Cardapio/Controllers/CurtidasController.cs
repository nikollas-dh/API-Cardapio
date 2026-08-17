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
        public IActionResult GetCurtidas() 
        {
            try
            {
                var pratosMaisCurtidos = ct.Pratos
                    .Select(p => new
                    {
                        p.Id,
                        p.Nome,
                        p.Foto,
                        Curtidas = p.ClienteCurtida.Count()
                    })
                    .OrderByDescending(p => p.Curtidas)
                    .Take(3)
                    .ToList();

                return Ok(pratosMaisCurtidos);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
