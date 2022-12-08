using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoVendidoController : Controller
    {
        private ProductoVendidoRepository repository = new ProductoVendidoRepository();

        [HttpGet]
        public ActionResult<List<ProductoVendido>> Get()
        {
            try
            {
                List<ProductoVendido> listaProductoVendido = repository.listarProductoVendido();
                return Ok(listaProductoVendido);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<ProductoVendido> Get(long Id)
        {
            try
            {
                ProductoVendido? productoVendido = repository.obtenerProductoVendido(Id);
                if (productoVendido != null)
                {
                    return Ok(productoVendido);
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
        public ActionResult Post([FromBody] ProductoVendido productoVendido)
        {
            try
            {
                ProductoVendido productoiVendidoCreado = repository.crearProductoVendido(productoVendido);
                return StatusCode(StatusCodes.Status201Created, productoiVendidoCreado);
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
                bool seElimino = repository.eliminarProductoVendido(Id);
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
        public ActionResult<ProductoVendido> Put(long id, [FromBody] ProductoVendido productoVendidoAActualizar)
        {
            try
            {
                ProductoVendido? productoVendidoActualizado = repository.actualizarProductoVendido(id, productoVendidoAActualizar);
                if (productoVendidoActualizado != null)
                {
                    return Ok(productoVendidoActualizado);
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
