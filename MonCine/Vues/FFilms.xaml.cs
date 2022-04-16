using MonCine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Films.xaml
    /// </summary>
    public partial class FFilms : Page
    {
        public List<Film> Films = new List<Film>();
        public Film SelectedFilm { get; set; }
        private DAL _dal;
        public FFilms(DAL dal)
        {
            InitializeComponent();
            //DateTime sameDate = System.DateTime.UtcNow;
            //Film item1 = new Film("Un Bon Film", sameDate);
            //Film item2 = new Film("Le Film 3", sameDate);
            //List<Film> mockListefilms = new List<Film>()
            //{
            //    new Film("BlaBla", sameDate),
            //    new Film("BlaBla 2", sameDate),
            //    new Film("Truc", sameDate),
            //    new Film("Chose 4", sameDate),
            //    item1,
            //    item2,
            //};
            //dal.AddFilms(new Film("Film Test", DateTime.UtcNow));
            _dal = dal;
            //Films = mockListefilms;
            Films = _dal.ReadFilms();
            //listeFilms.ItemsSource = Films;

            listeFilms.ItemsSource = Films;
            listeFilms.DataContext = Films;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        public static List<Film> FiltrerFilmsParNom(List<Film> films, string filtre)
        {
            List<Film> resultat = new List<Film>();

            resultat = films.Where(x => x.Nom.ToLower().Contains(filtre.ToLower())).ToList();
            return resultat;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtRecherche.Text))
            {
                listeFilms.ItemsSource = Films;
                return;
            }
            List<Film> resultat=new List<Film>();
            resultat = FiltrerFilmsParNom(Films, txtRecherche.Text);
            listeFilms.ItemsSource = resultat;

        }

        

        private void listeFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedFilm==null)
            {
                return;
            }

            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            EnregistrerChangements();
        }

        public void EnregistrerChangements() {
            foreach (var item in Films)
            {
                _dal.UpdateFilms(item);
            }
        }
    }
}
