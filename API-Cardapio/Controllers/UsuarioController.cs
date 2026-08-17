using API_Cardapio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Cardapio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DbCardapioContext ct = new DbCardapioContext();
        public UsuarioController(DbCardapioContext ct) 
        {
            this.ct = ct;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            try
            {
                var us = ct.Usuarios.FirstOrDefault(u => u.Senha == usuario.Senha && u.Email == usuario.Email);
                if (us == null)
                {
                    return StatusCode(403, "Usuário ou senha incorretos!");
                }

                return Ok(us);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
