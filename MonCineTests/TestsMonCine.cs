using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonCine.Data;
using MonCine.Vues;
using Moq;
using System;
using System.Collections.Generic;

namespace MonCineTests
{
    [TestClass]
    public class RealisateursTests
    {
        Realisateur realisateurTestUnitaire = new Realisateur("Bob", "PierreTest", 43);

        [TestMethod]
        public void AjouterRealisateurTest()
        {
            DAL dal = new DAL();

            dal.AddRealisateur(realisateurTestUnitaire);
            var realisateurs = dal.ReadRealisateurs();
            var result = realisateurs.Find(x => x.Nom == realisateurTestUnitaire.Nom);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReadRealisateursNonNullTest()
        {
            DAL dal = new DAL();
            List<Realisateur> realisateurs = new List<Realisateur>();

            realisateurs = dal.ReadRealisateurs();

            Assert.IsNotNull(realisateurs);

        }
        
        [TestMethod]
        public void ModifyRealisateurTest()
        {
            DAL dal = new DAL();
            //Film film = new Film("Film TestUnitaire", DateTime.UtcNow);
            var foundRealisateur = dal.ReadRealisateurs().Find(x => x.Nom == realisateurTestUnitaire.Nom);
            foundRealisateur.Age = 55;
            dal.UpdateRealisateur(foundRealisateur);

            var foundRealisateur2 = dal.ReadRealisateurs().Find(x => x.Nom == realisateurTestUnitaire.Nom);

            Assert.AreEqual(55, foundRealisateur2.Age);
        }
        
        [TestMethod]
        public void RemoveRealisateurTest()
        {
            DAL dal = new DAL();
            //Film film = new Film("Film TestUnitaire", DateTime.UtcNow);
            Realisateur realisateur = dal.ReadRealisateurs().Find(x=>x.Nom==realisateurTestUnitaire.Nom);
            dal.RemoveRealisateur(realisateur);
            var realisateurs = dal.ReadRealisateurs();
            var result = realisateurs.Find(x => x.Id == realisateur.Id);
            Assert.IsNull(result);
        }

    }

    [TestClass]
    public class ActeursTests
    {
        Acteur acteurTestUnitaire = new Acteur("Bob", "PierreTest", 43);

        [TestMethod]
        public void AjouterActeurTest()
        {
            DAL dal = new DAL();

            dal.AddActeur(acteurTestUnitaire);
            var acteurs = dal.ReadActeurs();
            var result = acteurs.Find(x => x.Nom == acteurTestUnitaire.Nom);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReadActeursNonNullTest()
        {
            DAL dal = new DAL();
            List<Acteur> acteurs = new List<Acteur>();

            acteurs = dal.ReadActeurs();

            Assert.IsNotNull(acteurs);

        }

        [TestMethod]
        public void ModifyActeurTest()
        {
            DAL dal = new DAL();
            //Film film = new Film("Film TestUnitaire", DateTime.UtcNow);
            var foundActeur = dal.ReadActeurs().Find(x => x.Nom == acteurTestUnitaire.Nom);
            foundActeur.Age = 55;
            dal.UpdateActeur(foundActeur);

            var foundActeur2 = dal.ReadActeurs().Find(x => x.Nom == acteurTestUnitaire.Nom);

            Assert.AreEqual(55, foundActeur2.Age);
        }

        [TestMethod]
        public void RemoveActeurTest()
        {
            DAL dal = new DAL();
            //Film film = new Film("Film TestUnitaire", DateTime.UtcNow);
            Acteur acteur = dal.ReadActeurs().Find(x => x.Nom == acteurTestUnitaire.Nom);
            dal.RemoveActeur(acteur);
            var acteurs = dal.ReadActeurs();
            var result = acteurs.Find(x => x.Id == acteur.Id);
            Assert.IsNull(result);
        }

    }

    [TestClass]
    public class FilmsTests
    {
        Film filmTestUnitaire = new Film("Film TestUnitaire", DateTime.UtcNow);

        [TestMethod]
        public void AjouterFilmTest()
        {
            DAL dal = new DAL();

            filmTestUnitaire.SurAffiche = true;
            dal.AddFilm(filmTestUnitaire);
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
        public void ModifyFilmTest()
        {
            DAL dal = new DAL();
            //Film film = new Film("Film TestUnitaire", DateTime.UtcNow);
            var foundfilm = dal.ReadFilms().Find(x => x.Nom == filmTestUnitaire.Nom);
            var real = dal.ReadRealisateurs()[0];
            //foundfilm.Realisateur = new Realisateur("Bob","Bob1", 10);
            foundfilm.Realisateur = real;
            dal.UpdateFilm(foundfilm);
            var foundfilm2 = dal.ReadFilms().Find(x => x.Nom == filmTestUnitaire.Nom);

            Assert.AreEqual("Bob", foundfilm2.Realisateur.Prenom);
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
            dal.RemoveFilm(film);
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