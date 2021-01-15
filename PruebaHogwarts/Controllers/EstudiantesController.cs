using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using PruebaHogwarts.Entity;
using PruebaHogwarts.model;
using System.Net.Http;

namespace PruebaHogwarts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly ILogger<EstudiantesController> _logger;

        public EstudiantesController(ILogger<EstudiantesController> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        [Route("/estudiantes/add")]
        public IActionResult add([FromBody()] Estudiante estudiante)
        {
            try
            {
                List<Estudiante> estudiantes = Archivo.leer();
                string mensajeValidate = Estudiante.validate(estudiante);
                if(mensajeValidate != "")
                {
                    return StatusCode(500, mensajeValidate);
                }

                if (estudiantes == null)
                {
                    estudiantes = new List<Estudiante>() { estudiante };
                }
                else
                {
                    bool existe = false;
                    Estudiante estudianteEliminado = null;
                    estudiantes.ForEach(delegate (Estudiante est)
                    {
                        if (estudiante.identificacion == est.identificacion)
                        {
                            existe = true;
                            estudianteEliminado = est;
                        }
                    });

                    if (existe)
                        estudiantes.Remove(estudianteEliminado);

                    estudiantes.Add(estudiante);
                }

                Archivo.crear(estudiantes);
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Identificación inválida.");
            }
        }

        [HttpGet]
        [Route("/estudiantes")]
        public IActionResult get()
        {
            try
            {
                IEnumerable<Estudiante> estudiantes = Archivo.leer();
                if (estudiantes == null)
                {
                    return StatusCode(500, "No hay registros todavía.");
                }
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Identificación inválida.");
            }
        }

        [HttpDelete()]
        [Route("/estudiantes/delete")]
        public IActionResult delete([FromBody()] Estudiante estudiante)
        {

            try
            {
                List<Estudiante> estudiantes = Archivo.leer();

                // se valida que haya una identificacion para eliminar
                if (estudiante.identificacion == 0)
                {
                    return StatusCode(500, "Identificación inválida.");
                }

                if (estudiantes != null)
                {
                    Estudiante eliminado = null;
                    //foreach (Estudiante est in estudiantes) { }
                    estudiantes.ForEach((Estudiante est) =>
                    {
                        if (estudiante.identificacion == est.identificacion)
                        {
                            eliminado = est;
                        }
                    });

                    if (eliminado != null)
                    {
                        estudiantes.Remove(eliminado);
                        Archivo.crear(estudiantes);
                        return Ok("Solicitud eliminada");
                    }
                    else
                        return Ok("Identificación no encontrada");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Identificación inválida.");
            }
            return Ok("");
        }
    }
}
