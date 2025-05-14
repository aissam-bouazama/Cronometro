

namespace Cronometro
{
    public enum StatusCronometro
    {
        Started,
        Paused,
    }
    public class Crono
    {
        public string Id { get; set; }
        public DateTime TempsInici { get; set; }
        public double TempsAcumulat { get; set; }

        public StatusCronometro Status { get; set; }

        public Crono()
        {
        }

        public Crono(string id, DateTime tempsInici, StatusCronometro status)
        {
            Id = id;
            TempsInici = tempsInici;
            Status = status;
        }
    }
}
