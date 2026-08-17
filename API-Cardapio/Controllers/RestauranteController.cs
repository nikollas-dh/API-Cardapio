using API_Cardapio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Cardapio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly DbCardapioContext ct = new DbCardapioContext();
        public RestauranteController(DbCardapioContext ct) 
        { 
            this.ct = ct;
        }

        [HttpGet]
        public IActionResult ListarRestaurantes() 
        {
            try
            {
                var restaurantes = ct.Restaurantes.ToList();
                //var restaurantes = ct.Restaurantes
                //             .Select(r => new
                //             {
                //                 r.Id,
                //                 r.Nome,
                //                 Foto = r.Foto.ToLowerInvariant(),
                //                 r.Descricao,
                //                 r.Cidade,
                //             })
                //             .ToList();
                return Ok(restaurantes);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Erro interno no servidorr");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult listarRestaurantesProprietario([FromRoute] int id) 
        {
            var proprietario = ct.Usuarios.Where(p=>p.Id== id).FirstOrDefault();
            try
            {
                if (proprietario == null) 
                {
                    return NotFound();
                }
                if (proprietario.PerfilId != 1) return StatusCode(403, "O usuário informado não é um proprietário");
                var restaurantes = ct.Restaurantes.Where(r => r.DonoId == id).ToList();


                return Ok(restaurantes);
            }
            catch (Exception ex) 
            {

                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
