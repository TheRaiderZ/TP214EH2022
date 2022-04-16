using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Windows;

namespace MonCine.Data
{
    public class DAL
    {
        private IMongoClient mongoDBClient;
        private IMongoDatabase database;

        public DAL(IMongoClient client = null)
        {
            mongoDBClient = client ?? OuvrirConnexion();
            database = ConnectDatabase();
        }
        private IMongoClient OuvrirConnexion()
        {
            MongoClient dbClient = null;
            try
            {
                dbClient = new MongoClient("mongodb://localhost:27017/");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de se connecter à la base de données " + ex.Message, "Erreur");
            }
            return dbClient;
        }

        private IMongoDatabase ConnectDatabase()
        {
            IMongoDatabase db = null;
            try
            {
                db = mongoDBClient.GetDatabase("TP2DB");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de se connecter à la base de données " + ex.Message, "Erreur");
            }
            return db;
        }
        public List<Abonne> ReadAbonnes()
        {
            var abonnes = new List<Abonne>();

            try
            {
                var collection = database.GetCollection<Abonne>("Abonnes");
                abonnes = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'obtenir la collection " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return abonnes;
        }

        // CRUD Films
        public List<Film> ReadFilms()
        {
            var films = new List<Film>();

            try
            {
                var collection = database.GetCollection<Film>("Films");
                films = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'obtenir la collection " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return films;
        }
        public Film FindFilmByName(string nom)
        {
            

            try
            {
                var collection = database.GetCollection<Film>("Films");
                var film = collection.Find(x=>x.Nom==nom).First();
                return film;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'obtenir la collection " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return null;
        }

        public void AddFilms(Film film)
        {
            

            try
            {
                var collection = database.GetCollection<Film>("Films");
                collection.InsertOne(film);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'ajouter un film " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void UpdateFilms(Film film)
        {


            try
            {
                var collection = database.GetCollection<Film>("Films");
                collection.ReplaceOne((x=> x.Id==film.Id), film);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de modifier un film " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void RemoveFilms(Film film)
        {

            try
            {
                var collection = database.GetCollection<Film>("Films");
                collection.DeleteOne((x => x.Id == film.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de supprimer un film " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public List<Film> ReadFilmsAffiche()
        {
            var films = new List<Film>();

            try
            {
                var collection = database.GetCollection<Film>("Films");
                films = collection.Find(x=>x.SurAffiche==true).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'obtenir la collection " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return films;
        }
    }
}
