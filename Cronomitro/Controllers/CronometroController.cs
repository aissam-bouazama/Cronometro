using System.Collections.ObjectModel;
using Cronometro;
using Cronometro.Data;
using Microsoft.AspNetCore.Mvc;


namespace Cronomitro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CronometroController : Controller
    {
        private readonly  RepositoryCronometros _repo = new RepositoryCronometros();


        

        [HttpGet("LlistaCrono")]
        public ActionResult List()
        {
            var Cronos = _repo.Getall();
            if ( Cronos== null)
            {
                return NotFound("no hi ha Cap Llista");
            }
            else
            {
                return Ok(Cronos);
            }


        }
        

        [HttpGet("Crono")]
        public ActionResult Get(string id) {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser buit");
            }
            else
            {
                double temps = _repo.Get(id);
                if (temps != -1)
                {
                    return Ok(temps);
                }
                else
                {
                    return NotFound("El id no existeix");
                }
            }
        }

        [HttpPost("AfegirCrono")]
        public ActionResult Start()
        {

            string id = _repo.Start();

             if (id != null)
            {
                return Ok(id);
            }
            else
            {
                return BadRequest("No s'ha pogut afegir el crono");
            }
        }
        [HttpDelete("AturaCrono")]
        public ActionResult Stop(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser buit");
            }
            else
            {
                 double temps = _repo.Stop(id);
                if (temps != -1)
                {
                    return Ok(temps);
                }
                else
                {
                    return NotFound("El id no existeix");
                }
               
            }
        }
        [HttpPut("PausaCrono")]
        public ActionResult Pause(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser null o buit");
            }
            else
            {
               double temps = _repo.Pause(id);
                if (temps != -1)
                {
                    return Ok(temps);
                }
                else
                {
                    return NotFound("El id no existeix");
                }
            }


        }

        [HttpPut("ReanudaCrono")]
        public ActionResult PauseStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser null o buit");
            }
            else
            {
               double temps= _repo.Resume(id);
                if(temps != -1)
                {
                    return Ok(temps);
                }
                else
                {
                    return NotFound("El id no existeix");
                }


            }
        }

        [HttpPost("Estat")]
        public ActionResult Status(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser null o buit");
            }
            else
            {
             string status = _repo.Status(id);
                if (status != null)
                {
                    return Ok(status);
                }
                else
                {
                    return NotFound("El id no existeix");
                }
            }
        }


    }
}
