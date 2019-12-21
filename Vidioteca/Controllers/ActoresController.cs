using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using Vidioteca.Models.Actor;

namespace Vidioteca.Controllers
{
    public class ActoresController : Controller
    {

        // GET: Actores
        public async Task<IActionResult> Index()
        {
            List<Actor> lstactor = new List<Actor>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:14574/api/actores/listar"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lstactor = JsonConvert.DeserializeObject<List<Actor>>(apiResponse);
                }
            }
            return View(lstactor);
        }
      
        // GET: Actores/Sexo/m
        public async Task<IActionResult> Sexo(string sexo)
        {
            List<Actor> lstactor = new List<Actor>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:14574/api/actores/ListarSexo/" + sexo))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lstactor = JsonConvert.DeserializeObject<List<Actor>>(apiResponse);
                }
            }
            return View("Index", lstactor);
        }


        // POST: Actores/CrearActor
        public ViewResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Actor actor = new Actor
            {
                nombre = model.nombre,
                fechanac = model.fechanac,
                sexo = model.sexo
            };

            if (model.foto != null)
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    Path.GetExtension(model.foto.FileName);
                    model.foto.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
                actor.foto = fileBytes;
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(actor), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:14574/api/actores/crear", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }


        // GET: Actores/Actualizar/5
        public async Task<IActionResult> Actualizar(int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Index");
            }
            Actor actor = new Actor();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:14574/api/actores/mostrar/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    actor = JsonConvert.DeserializeObject<Actor>(apiResponse);
                }
            }

            if (actor == null)
            {
                return RedirectToAction("Index");
            }

            ActualizarViewModel model = new ActualizarViewModel
            {
                idactor = actor.idactor,
                nombre = actor.nombre,
                fechanac = actor.fechanac,
                sexo = actor.sexo
            };
            return View(model);
        }

        // PUT: Actores/Actualizar/
        [HttpPost]
        public async Task<IActionResult> Actualizar(ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Actor actor = new Actor
            {
                idactor = model.idactor,
                nombre = model.nombre,
                fechanac = model.fechanac,
                sexo = model.sexo
            };

            if (model.foto != null)
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    model.foto.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
                actor.foto = fileBytes;
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(actor), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:14574/api/actores/actualizar", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Actores/EliminarVista/5
        public async Task<IActionResult> EliminarVista(int id)
        {
            Actor actor = new Actor();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:14574/api/actores/mostrar/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    actor = JsonConvert.DeserializeObject<Actor>(apiResponse);
                }
            }
            return View(actor);
        }
        [HttpPost] 
        public async Task<IActionResult> Eliminar(int idactor)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:14574/api/actores/eliminar/" + idactor))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

    }
}