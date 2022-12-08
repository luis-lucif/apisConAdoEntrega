using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository = new UsuarioRepository();

        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            try
            {
                List<Usuario> listaUsuario = repository.listarUsuario();
                return Ok(listaUsuario);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<Usuario> Get(long Id)
        {
            try
            {
                Usuario? usuario = repository.obtenerUsuario(Id);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("Producto no encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                Usuario ususarioCreado = repository.crearUsuario(usuario);
                return StatusCode(StatusCodes.Status201Created, ususarioCreado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] long Id)
        {
            try
            {
                bool seElimino = repository.eliminarUsusario(Id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(long id, [FromBody] Usuario usuarioAActualizar)
        {
            try
            {
                Usuario? usuarioActualizado = repository.actualizarUsuario(id, usuarioAActualizar);
                if (usuarioActualizado != null)
                {
                    return Ok(usuarioActualizado);
                }
                else
                {
                    return NotFound("El producto no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
