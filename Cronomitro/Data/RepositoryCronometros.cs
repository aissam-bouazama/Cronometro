
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Cronometro.Data
{
    public class RepositoryCronometros 
    {
        static Dictionary<string, Crono> _cronometros { get; set; }

       
        public double Get(string id)
        {
            var crono = _cronometros[id];
            double temps;
            if (crono.Status == StatusCronometro.Started)
            {
                 temps = crono.TempsAcumulat + (DateTime.Now - crono.TempsInici).TotalSeconds;
            }
            else
            {
                temps = crono.TempsAcumulat;
            }
           
            return temps;
           
        }

        public string[] Getall()
        {
            if(_cronometros == null)
            {
                _cronometros = new Dictionary<string, Crono>();
            }
         return _cronometros.Keys.ToArray();
        }

        public double Pause(string id)
        {
            if (_cronometros.ContainsKey(id))
            {
                var crono = _cronometros[id];
                crono.TempsAcumulat = (DateTime.Now - crono.TempsInici).TotalSeconds;
                crono.Status = StatusCronometro.Paused;

                return crono.TempsAcumulat;
            }
            else
            {
                return 0;
            }
            

        }

        public double Resume(string id)
        {
            if (_cronometros.ContainsKey(id))
            {
                var crono = _cronometros[id];
                crono.TempsInici = DateTime.Now;
                crono.Status = StatusCronometro.Started;
                return crono.TempsAcumulat;
            }
            else
            {
                return -1;
            }
        }

        public string Start()
        {
            string id = Guid.NewGuid().ToString();
            if (_cronometros.ContainsKey(id))
            {
                return string.Empty;
            }
            else
            {
                Crono crono = new Crono(id, DateTime.Now, StatusCronometro.Started);
                _cronometros.Add(id, crono);
                return id;

            }
        }

        public string Status(string id)
        {
           return _cronometros[id].Status.ToString();
        }

        public double Stop(string id)
        {
            if (_cronometros.ContainsKey(id))
            {
                var crono = _cronometros[id];
                _cronometros.Remove(id);
                return crono.TempsAcumulat;
            }
            else
            {
                return -1;
            }
        }
    }
}
