using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Repositories;
using System.Net;
using SistemaGestion.Models;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductoRepository repository = new ProductoRepository();

        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto> listaProducto = repository.listarProducto();
                return Ok(listaProducto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<Producto> Get(long Id)
        {
            try
            {
                Producto? producto = repository.obtenerProducto(Id);
                if(producto != null)
                {
                    return Ok(producto);
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

        [HttpGet("GetProducto/{Id}")]
        public ActionResult<Producto> GetProducto(long Id)
        {
            try
            {
                Producto? producto = repository.obtenerProducto(Id);
                if (producto != null)
                {
                    return Ok(producto);
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
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                Producto productoCreado = repository.crearProducto(producto);
                return StatusCode(StatusCodes.Status201Created, productoCreado);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] long Id)
        {
            try
            {
                bool seElimino = repository.eliminarProducto(Id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Producto> Put(long id, [FromBody] Producto productoAActualizar)
        {
            try
            {
                Producto? productoActualizado = repository.actualizarProducto(id, productoAActualizar);
                if (productoActualizado != null)
                {
                    return Ok(productoActualizado);
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
