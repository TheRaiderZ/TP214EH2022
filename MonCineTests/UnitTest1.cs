using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonCine.Data;
using MonCine.Vues;
using Moq;
using System;
using System.Collections.Generic;

namespace MonCineTests
{
    [TestClass]
    public class FilmsTests
    {
        Film filmTestUnitaire = new Film("Film TestUnitaire", DateTime.UtcNow);

        [TestMethod]
        public void AjouterFilmTest()
        {
            DAL dal = new DAL();

            filmTestUnitaire.SurAffiche = true;
            dal.AddFilms(filmTestUnitaire);
            var films = dal.ReadFilms();
            var result = films.Find(x=>x.Nom==filmTestUnitaire.Nom);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReadFilmsNonNullTest()
        {
            DAL dal = new DAL();
            List<Film> films = new List<Film>();

            films = dal.ReadFilms();

            Assert.IsNotNull(films);

        }
        [TestMethod]
        public void FindFilmParNomTest()
        {
            DAL dal = new DAL();

            Film result = dal.FindFilmByName("Film TestUnitaire");

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void UpdateFilmTest()
        {
            DAL dal = new DAL();
            //Film film = new Film("Film TestUnitaire", DateTime.UtcNow);
            filmTestUnitaire.Realisateur = "Bob";
            dal.UpdateFilms(filmTestUnitaire);

            Assert.AreEqual("Bob", filmTestUnitaire.Realisateur);
        }
        [TestMethod]
        public void ReadFilmAfficheTest()
        {
            DAL dal = new DAL();
            //Film film = new Film("Film TestUnitaire", DateTime.UtcNow);
            var films = dal.ReadFilmsAffiche();
            var result = films.Find(x => x.Nom == filmTestUnitaire.Nom);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void RemoveFilmTest()
        {
            DAL dal = new DAL();
            //Film film = new Film("Film TestUnitaire", DateTime.UtcNow);
            Film film = dal.FindFilmByName("Film TestUnitaire");
            dal.RemoveFilms(film);
            var films = dal.ReadFilms();
            var result = films.Find(x => x.Id == film.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RechercherFilmsParNom()
        {
            DateTime sameDate = System.DateTime.UtcNow;
            Film item1 = new Film("Un Bon Film", sameDate);
            Film item2 = new Film("Le Film 3", sameDate);
            List<Film> mockListefilms = new List<Film>()
            {
                new Film("BlaBla", sameDate),
                new Film("BlaBla 2", sameDate),
                new Film("Truc", sameDate),
                new Film("Chose 4", sameDate),
                item1,
                item2,
            };
            List<Film> resultatAttendu = new List<Film>()
            {
                item1,
                item2,
            };


            List<Film> resultat = FFilms.FiltrerFilmsParNom(mockListefilms, "film");


            CollectionAssert.AreEquivalent(resultatAttendu, resultat);
            

        }

    }
}