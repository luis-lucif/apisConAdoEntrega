using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {
        private VentaRepository repository = new VentaRepository();

        [HttpGet]
        public ActionResult<List<Venta>> Get()
        {
            try
            {
                List<Venta> listaVenta = repository.listarVenta();
                return Ok(listaVenta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<Venta> Get(long Id)
        {
            try
            {
                Venta? venta = repository.obtenerVenta(Id);
                if (venta != null)
                {
                    return Ok(venta);
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
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                Venta ventaCreada = repository.crearVenta(venta);
                return StatusCode(StatusCodes.Status201Created, ventaCreada);
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
                bool seElimino = repository.eliminarVenta(Id);
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
        public ActionResult<Venta> Put(long id, [FromBody] Venta ventaAActualizar)
        {
            try
            {
                Venta? ventaActualizado = repository.actualizarVenta(id, ventaAActualizar);
                if (ventaActualizado != null)
                {
                    return Ok(ventaActualizado);
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
