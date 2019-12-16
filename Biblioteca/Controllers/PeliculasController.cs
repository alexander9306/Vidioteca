using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using Biblioteca.Web.Datos;
using Biblioteca.Web.Models.Pelicula;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Biblioteca.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly  Core_Pelicula _pelicula = new Core_Pelicula();
        private readonly Core_Pelicula_Foto _foto = new Core_Pelicula_Foto();
        private readonly Core_Actor_Foto _foto_a = new Core_Actor_Foto();
        private readonly Core_Elenco _elenco = new Core_Elenco();


        // GET: api/Peliculas/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {

            var peliculas = _pelicula.CargarPeliculas();
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
                    model.foto = _foto.CargarFoto(pelicula.idfoto).foto;

                    lstmodel.Add(model);
                }
                return Ok(lstmodel);
            }

            return NoContent();

        }

        // GET: api/Peliculas/Actores/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Actores([FromRoute] int idpelicula)
        {

            var actores = _elenco.CargarActores(idpelicula);
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
                    model.foto = _foto_a.CargarFoto(actor.idfoto).foto;

                    lstmodel.Add(model);
                }
                return Ok(lstmodel);
            }

            return NoContent();

        }

        // GET: api/Peliculas/ListarGenero/Terror
        [HttpGet("[action]/{genero}")]
        public async Task<IActionResult> ListarGenero([FromRoute] string genero)
        {
            var peliculas = _pelicula.CargarPeliculaGenero(genero);

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
                    model.foto = _foto.CargarFoto(pelicula.idfoto).foto;

                    lstmodel.Add(model);
                }
                return Ok(lstmodel);
            }

            return NoContent();

        }

        // GET: api/Peliculas/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            try
            {
                var pelicula = _pelicula.CargarPelicula(id);

                return Ok(new PeliculaViewModel
                {
                    idpelicula = pelicula.idpelicula,
                    titulo = pelicula.titulo,
                    genero = pelicula.genero,
                    fechaestreno = pelicula.fechaestreno,
                    foto = _foto.CargarFoto(pelicula.idfoto).foto
                });
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        // POST: api/Peliculas/Crear
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
                Pelicula pelicula = new Pelicula
                {
                    titulo = model.titulo,
                    genero =model.genero,
                    fechaestreno = DateTime.Parse(model.fechaestreno),
                    idfoto = _foto.CrearFoto(model.foto)
                };

                _pelicula.CrearPelicula(pelicula);

            }
            catch (Exception e)
            {
                return BadRequest("No se pudo guardar la pelicula \n " );

            }

            return Ok("Pelicula guardada correctamente");
        }

        // PUT: api/Peliculas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idpelicula <= 0)
            {
                return BadRequest();
            }

            try
            {
                var pelicula = _pelicula.CargarPelicula(model.idpelicula);
                try
                {
                    pelicula.titulo = model.titulo;
                    pelicula.genero = model.genero;
                    pelicula.fechaestreno = DateTime.Parse(model.fechaestreno);
                    _foto.ActualizarFoto(pelicula.idfoto, model.foto);

                    _pelicula.ActualizarPelicula(pelicula);

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

            return Ok("Pelicula guardada correctamente");
        }

        // DELETE: api/Peliculas/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }


            if (_pelicula.BorrarPelicula(id) < 1)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
