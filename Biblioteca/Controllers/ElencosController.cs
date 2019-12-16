using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using Biblioteca.Web.Datos;
using Biblioteca.Web.Models.Elenco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Biblioteca.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElencosController : ControllerBase
    {
        private readonly Core_Elenco _elenco = new Core_Elenco();


        // POST: api/Elencos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(allErrors);
            }

            try
            {
                Elenco elenco = new Elenco
                {
                    idelenco = model.idelenco,
                    idpelicula = model.idelenco,
                    idactor = model.idelenco
                };

                _elenco.CrearElenco(elenco);

            }
            catch (Exception e)
            {
                return BadRequest();

            }

            return Ok();
        }
        // PUT: api/Elencos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idelenco <= 0)
            {
                return BadRequest();
            }

            try
            {
                var elenco = _elenco.CargarElenco(model.idelenco);
                try
                {
                    elenco.idelenco = model.idelenco;
                    elenco.idpelicula = model.idpelicula;

                    _elenco.ActualizarElenco(elenco);

                }
                catch (Exception e)
                {
                    return BadRequest("No se pudo guardar");
                }

            }
            catch (Exception e)
            {
                return NotFound();
            }

            return Ok("Guardado correctamente");
        }

        // DELETE: api/Elencos/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (_elenco.BorrarElenco(id) < 1)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
