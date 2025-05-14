using System.Collections.ObjectModel;
using Cronometro;
using Microsoft.AspNetCore.Mvc;


namespace Cronomitro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CronometroController : Controller
    {
        private Dictionary<string, Crono> _cronometros;

        [HttpGet("LlistaCrono")]
        public ActionResult List()
        {
            if (_cronometros == null)
            {
                return NotFound("no hi ha Cap Llista");
            }
            else
            {
                return Ok(_cronometros.Keys.ToArray());
            }

        }

        [HttpPost("AfegirCrono")]
        public ActionResult Start()
        {
            if (_cronometros == null)
            {
                _cronometros = new Dictionary<string, Crono>();
            }
                string id = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser null o buit");
            }
            else
            {
                if (_cronometros.ContainsKey(id))
                {
                    return BadRequest("El id ja existeix");
                }
                else
                {
                    Crono crono = new Crono(id, DateTime.Now, StatusCronometro.Started);
                    _cronometros.Add(id, crono);
                    return Ok(_cronometros);

                }
            }
        }
        [HttpDelete("AturaCrono")]
        public ActionResult Stop(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser null o buit");
            }
            else
            {
                if (_cronometros.ContainsKey(id))
                {
                    var crono = _cronometros.FirstOrDefault(x => x.Key == id).Value;
                    _cronometros.Remove(id);
                    return Ok(crono.TempsAcumulat);
                }
                else
                {
                    return NotFound("El id no existeix");
                }
            }
        }
        [HttpPost("PausaCrono")]
        public ActionResult Pause(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser null o buit");
            }
            else
            {
                if (_cronometros.ContainsKey(id))
                {
                    var crono = _cronometros.FirstOrDefault(x => x.Key == id).Value;
                    crono.TempsAcumulat = (DateTime.Now - crono.TempsInici).TotalHours;
                    crono.Status = StatusCronometro.Paused;

                    return Ok(crono);
                }
                else
                {
                    return NotFound("El id no existeix");
                }
            }


        }

        [HttpPost("ReanudaCrono")]
        public ActionResult PauseStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("El id no pot ser null o buit");
            }
            else
            {
                if (_cronometros.ContainsKey(id))
                {
                    var crono = _cronometros.FirstOrDefault(x => x.Key == id).Value;
                    crono.TempsInici = DateTime.Now;
                    crono.Status = StatusCronometro.Started;
                    return Ok(crono);
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
                if (_cronometros.ContainsKey(id))
                {
                    var crono = _cronometros.FirstOrDefault(x => x.Key == id).Value;

                    return Ok(crono.Status.ToString());
                }
                else
                {
                    return NotFound("El id no existeix");
                }
            }
        }


    }
}
