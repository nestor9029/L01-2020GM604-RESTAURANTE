using L01_2020GM604_RESTAURANTE.dbContext;
using L01_2020GM604_RESTAURANTE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020GM604_RESTAURANTE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public pedidosController(restauranteContext context)
        {
            _restauranteContext = context;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {

            List<pedidos> listadoPedidos = (from e in _restauranteContext.pedidos
                                            select e).ToList();

            if (listadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPedidos);

        }

        [HttpGet]
        [Route("GetByIdCliente/{id}")]

        public IActionResult GetCliente(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                               where e.clienteId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
        [HttpGet]
        [Route("GetByIdMotorista/{id}")]

        public IActionResult GetMotorista(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }


        [HttpPost]
        [Route("AddPedido")]

        public IActionResult GuardarPedido([FromBody] pedidos pedido)
        {
            try
            {
                _restauranteContext.pedidos.Add(pedido);
                _restauranteContext.SaveChanges();
                return Ok(pedido);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoActual = (from e in _restauranteContext.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();
            if (pedidoActual == null)
            {
                return NotFound();
            }

           
            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContext.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();
            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public IActionResult EliminarPedido(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                               where e.pedidoId == id
                               select e).FirstOrDefault();
            if (pedido == null) { return NotFound(); }
            _restauranteContext.pedidos.Attach(pedido);
            _restauranteContext.pedidos.Remove(pedido);
            _restauranteContext.SaveChanges();
            return Ok(pedido);
        }

    }
}
