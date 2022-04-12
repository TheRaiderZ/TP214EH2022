using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace MonCine.Data
{
    public class Film
    {
        public ObjectId Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateSortie { get; set; }
        public DateTime DateProjection { get; set; }
        public bool SurAffiche { get; set; }
        public List<object> Acteurs { get; set; }
        public object Categories { get; set; }
        private List<int> Notes { get; set; }
        public int NbProjections { get; set; }


        public override string ToString()
        {
            return Nom;
        }

        public Film(string nom, DateTime dateSortie)
        {
            Nom = nom;
            DateSortie = dateSortie;
        }


        // A compléter

    }
}
