using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Datos;
using Biblioteca.Entidades;
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
            
            var actores =_actor.CargarActores();
            if (actores.Count() >= 1)
            {
                var lstmodel = new List<ActorViewModel>();

                foreach (var actor in actores)
                {
                    var model = new ActorViewModel();
                    model.idactor = actor.idactor;
                    model.nombre = actor.nombre;
                    model.fechanac = actor.fechanac;
                    model.sexo = actor.sexo;
                    model.foto = _foto.CargarFoto(actor.idfoto).foto;

                    lstmodel.Add(model);
                }
                return Ok(lstmodel);
            }

            return NoContent();

        }

        // GET: api/Actores/Peliculas/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Peliculas([FromRoute] int idactor)
        {

            var peliculas = _elenco.CargarPeliculas(idactor);
            if (peliculas.Count() >= 1)
            {
                var lstmodel = new List<PeliculaViewModel>();

                foreach (var pelicula in peliculas)
                {
                    var model = new PeliculaViewModel();

                    model.idpelicula = pelicula.idpelicula;
                    model.titulo = pelicula.titulo;
                    model.genero = pelicula.genero;
                    model.fechaestreno = pelicula.fechaestreno;
                    model.foto = _foto_p.CargarFoto(pelicula.idfoto).foto;

                    lstmodel.Add(model);
                }
                return Ok(lstmodel);
            }

            return NoContent();

        }

        // GET: api/Actores/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
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
                        foto = _foto.CargarFoto(actor.idfoto).foto
                    });
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        // GET: api/Actores/MostrarSexo/M
        [HttpGet("[action]/{sexo}")]
        public async Task<IActionResult> ListarSexo([FromRoute] string sexo)
        {
            var actores = _actor.CargarActorSexo(sexo);
            
            if (actores.Count() >= 1)
            {
                var lstmodel = new List<ActorViewModel>();

                foreach (var actor in actores)
                {
                    var model = new ActorViewModel();
                    model.idactor = actor.idactor;
                    model.nombre = actor.nombre;
                    model.fechanac = actor.fechanac;
                    model.sexo = actor.sexo;
                    model.foto = _foto.CargarFoto(actor.idfoto).foto;

                    lstmodel.Add(model);
                }
                return Ok(lstmodel);
            }

            return NoContent();

        }

        // POST: api/Actores/Crear
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
                Actor actor = new Actor
                {
                    nombre = model.nombre,
                    fechanac = DateTime.Parse(model.fechanac),
                    sexo = Char.Parse(model.sexo.ToUpper()),
                    idfoto =  _foto.CrearFoto(model.foto)
                };

                _actor.CrearActor(actor);

            }
            catch (Exception e)
            {
                return BadRequest("No se pudo guardar el actor \n ");

            }

            return Ok("Actor guardado correctamente");
        }

        // PUT: api/Actores/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
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
                    actor.sexo = Char.Parse(model.sexo.ToUpper());
                    _foto.ActualizarFoto(actor.idfoto, model.foto);

                    _actor.ActualizarActor(actor);

                }
                catch (Exception e)
                {
                    return BadRequest("No se pudo guardar los cambios");
                }

            }
            catch (Exception e)
            {
                return NotFound();
            }

            return Ok("Actor guardado correctamente");
        }

        // DELETE: api/Actores/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
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
