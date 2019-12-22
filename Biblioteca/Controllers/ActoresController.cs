using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using Biblioteca.Web.Datos;
using Biblioteca.Web.Models;
using Biblioteca.Web.Models.Actor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Biblioteca.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
       private readonly Core_Actor _actor = new Core_Actor();
       private readonly Core_Actor_Foto _foto = new Core_Actor_Foto();
       private readonly Core_Pelicula_Foto _foto_p = new Core_Pelicula_Foto();
       private readonly Core_Elenco _elenco = new Core_Elenco();

       // GET: api/Actores/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var actores = _actor.CargarActores();
                if (actores.Any())
                {
                    var lstmodel = actores.Select(actor => new ActorViewModel
                        {
                            idactor = actor.idactor,
                            nombre = actor.nombre,
                            fechanac = actor.fechanac,
                            sexo = actor.sexo,
                            foto = _foto.CargarFoto(actor.idactor).foto
                        }).ToList();

                    return Ok(lstmodel);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
           

        }

        // GET: api/Actores/Peliculas/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Peliculas([FromRoute] int idactor)
        {

            var peliculas = _elenco.CargarPeliculas(idactor);
            if (peliculas.Any())
            {
                var lstmodel = peliculas.Select(pelicula => new PeliculaViewModel
                    {
                        idpelicula = pelicula.idpelicula,
                        titulo = pelicula.titulo,
                        genero = pelicula.genero,
                        fechaestreno = pelicula.fechaestreno,
                        foto = _foto_p.CargarFoto(pelicula.idpelicula).foto
                    }).ToList();
                return Ok(lstmodel);
            }
            return NoContent();
        }

        // GET: api/Actores/Mostrar/5
        [HttpGet("[action]/{id}")]
        public IActionResult Mostrar([FromRoute] int id)
        {
            try
            {
                var actor = _actor.CargarActor(id);

                return Ok(new ActorViewModel
                    {
                        idactor = actor.idactor,
                        nombre = actor.nombre,
                        fechanac = actor.fechanac,
                        sexo = actor.sexo,
                        foto = _foto.CargarFoto(actor.idactor).foto
                    });
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        // GET: api/Actores/MostrarSexo/M
        [HttpGet("[action]/{sexo}")]
        public async Task<IActionResult> ListarSexo([FromRoute] string sexo)
        {
            var actores = _actor.CargarActorSexo(sexo);
            
            if (actores.Any())
            {
                var lstmodel = actores.Select(actor => new ActorViewModel
                    {
                        idactor = actor.idactor,
                        nombre = actor.nombre,
                        fechanac = actor.fechanac,
                        sexo = actor.sexo,
                        foto = _foto.CargarFoto(actor.idactor).foto
                    })
                    .ToList();
                return Ok(lstmodel);
            }

            return NoContent();

        }

        // POST: api/Actores/Crear
        [HttpPost("[action]")]
        public IActionResult Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(allErrors);
            }

            try
            {
                var actor = new Actor
                {
                    nombre = model.nombre,
                    fechanac = DateTime.Parse(model.fechanac),
                    sexo = char.Parse(model.sexo.ToUpper())
                };

                var foto = new Actor_Foto
                {
                    idfoto = _actor.CrearActor(actor),
                    foto = model.foto
                };

                _foto.CrearFoto(foto);
            }
            catch (Exception)
            {
                return BadRequest("No se pudo guardar el actor \n ");

            }

            return Ok("Actor guardado correctamente");
        }

        // PUT: api/Actores/Actualizar
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idactor <= 0)
            {
                return BadRequest();
            }

            try
            {
                var actor = _actor.CargarActor(model.idactor);
                try
                {
                    actor.nombre = model.nombre;
                    actor.fechanac = DateTime.Parse(model.fechanac);
                    actor.sexo = char.Parse(model.sexo.ToUpper());

                    if (model.foto != null)
                    {
                        var foto = new Actor_Foto
                        {
                            idfoto = actor.idactor,
                            foto = model.foto
                        };
                        _foto.ActualizarFoto(foto);
                    }

                    _actor.ActualizarActor(actor);

                }
                catch (Exception)
                {
                    return BadRequest("No se pudo guardar los cambios");
                }

            }
            catch (Exception)
            {
                return NotFound();
            }

            return Ok("Actor guardado correctamente");
        }

        // DELETE: api/Actores/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (_actor.BorrarActor(id) < 1)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}
