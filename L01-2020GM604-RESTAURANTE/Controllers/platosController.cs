using L01_2020GM604_RESTAURANTE.dbContext;
using L01_2020GM604_RESTAURANTE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020GM604_RESTAURANTE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public platosController(restauranteContext context)
        {
            _restauranteContext = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {

            List<platos> listadoPlatos = (from e in _restauranteContext.platos
                                            select e).ToList();

            if (listadoPlatos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatos);

        }

        //filtro precio menor de un valor dado
        [HttpGet]
        [Route("Buscar/{precio}")]

        public IActionResult GetCliente(decimal precio)
        {
            List<platos> listPlatos = (from e in _restauranteContext.platos
                               where e.precio < precio 
                               select e).ToList();

            if (listPlatos == null)
            {
                return NotFound();
            }
            return Ok(listPlatos);
        }

        [HttpPost]
        [Route("AddPlato")]

        public IActionResult GuardarPlato([FromBody] platos plato)
        {
            try
            {
                _restauranteContext.platos.Add(plato);
                _restauranteContext.SaveChanges();
                return Ok(plato);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPlato(int id, [FromBody] platos platoModificar)
        {
            platos? platoActual = (from e in _restauranteContext.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();
            if (platoActual == null)
            {
                return NotFound();
            }


            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;

            _restauranteContext.Entry(platoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();
            return Ok(platoModificar);
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public IActionResult EliminarPlato(int id)
        {
            platos? plato = (from e in _restauranteContext.platos
                               where e.platoId == id
                               select e).FirstOrDefault();
            if (plato == null) { return NotFound(); }
            _restauranteContext.platos.Attach(plato);
            _restauranteContext.platos.Remove(plato);
            _restauranteContext.SaveChanges();
            return Ok(plato);
        }
    }
}
