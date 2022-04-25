using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;


namespace MonCine.Data
{
    public class Recompense
    {
        public TypeRecompense type { get; set; }
        public Film film { get; set; }

        public Recompense(TypeRecompense type, Film film)
        {
            this.type = type;
            this.film = film;
        }

        public override string ToString()
        {
            return type + " : " + film.Nom;
        }
    }
}
