using System;
using System.Collections.Generic;
using System.Text;

namespace MonCine.Data
{
    public class Projection
    {
        public Salle salle { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public Film film { get; set; }

        public Projection(Salle salle, DateTime dateDebut, DateTime dateFin, Film film)
        {
            this.salle = salle;
            this.dateDebut = dateDebut;
            this.dateFin = dateFin;
            this.film = film;
        }

        public override string ToString()
        {
            return dateDebut.ToString();
        }
    }
}
