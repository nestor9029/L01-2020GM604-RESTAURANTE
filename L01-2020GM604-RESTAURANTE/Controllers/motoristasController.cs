using L01_2020GM604_RESTAURANTE.dbContext;
using L01_2020GM604_RESTAURANTE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020GM604_RESTAURANTE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristasController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;
        
        public motoristasController (restauranteContext context)
        {
            _restauranteContext = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {

            List<motoristas> listadoMotorista = (from e in _restauranteContext.motoristas
                                          select e).ToList();

            if (listadoMotorista.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoMotorista);

        }

        //Filtro de motoristas por nombres
        [HttpGet]
        [Route("Buscar/{nombre}")]

        public IActionResult GetCliente(string nombre)
        {
            motoristas? motorista = (from e in _restauranteContext.motoristas
                                       where e.nombreMotorista.Contains(nombre)
                                       select e).FirstOrDefault();

            if (motorista == null)
            {
                return NotFound();
            }
            return Ok(motorista);
        }
        [HttpPost]
        [Route("AddMotorista")]

        public IActionResult GuardarPlato([FromBody] motoristas motorista)
        {
            try
            {
                _restauranteContext.motoristas.Add(motorista);
                _restauranteContext.SaveChanges();
                return Ok(motorista);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarMotoristas(int id, [FromBody] motoristas motoristaModificar)
        {
            motoristas? motoristaActual = (from e in _restauranteContext.motoristas
                                   where e.motoristaId == id
                                   select e).FirstOrDefault();
            if (motoristaActual == null)
            {
                return NotFound();
            }


            motoristaActual.nombreMotorista = motoristaModificar.nombreMotorista;
            

            _restauranteContext.Entry(motoristaActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();
            return Ok(motoristaModificar);
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public IActionResult EliminarMotorista(int id)
        {
            motoristas? motorista = (from e in _restauranteContext.motoristas
                             where e.motoristaId== id
                             select e).FirstOrDefault();
            if (motorista == null) { return NotFound(); }
            _restauranteContext.motoristas.Attach(motorista);
            _restauranteContext.motoristas.Remove(motorista);
            _restauranteContext.SaveChanges();
            return Ok(motorista);
        }
    }
}
