using MonCine.Data;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Projections.xaml
    /// </summary>
    public partial class FProjections : Page
    {
        public List<Film> Films = new List<Film>();
        public List<Projection> Projections = new List<Projection>();
        public List<Projection> projectionsFilm = new List<Projection>();
        public Film SelectedFilm { get; set; }
        public Projection SelectedProjection { get; set; }
        private DAL _dal;

        public FProjections(DAL dal)
        {
            InitializeComponent();
            _dal = dal;
            readListe();
            fillLists();
        }

        private void readListe()
        {
            Films = _dal.ReadFilms();
            Projections = _dal.ReadProjections();
        }

        private void fillLists()
        {
            listeFilms.ItemsSource = Films;
            listeSalle.ItemsSource = Enum.GetValues(typeof(Salle));
        }

        private void listeProjections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProjection = (Projection)listeProjections.SelectedItem;
            if (SelectedProjection == null)
            {
                return;
            }
            dtpDebut.SelectedDate = SelectedProjection.dateDebut;
            dtpFin.SelectedDate = SelectedProjection.dateFin;
            listeSalle.SelectedItem = SelectedProjection.salle;
        }

        private void listeFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFilm = (Film)listeFilms.SelectedItem;
            if (SelectedFilm == null)
            {
                return;
            }
            fillProjectionsList();
        }

        private void fillProjectionsList()
        {
            projectionsFilm.Clear();
            foreach (var projection in Projections)
            {
                if (projection.film.Nom == SelectedFilm.Nom)
                {
                    projectionsFilm.Add(projection);
                }
            }
            listeProjections.ItemsSource = projectionsFilm;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Enregistrer();
        }

        private void Enregistrer()
        {
            if (SelectedProjection.dateDebut > SelectedProjection.dateFin)
            {
                MessageBox.Show("Vous ne pouvez pas mettre la date de fin de projection avant le début de la projection.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _dal.UpdateProjection(SelectedProjection);
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            FCreerProjection fCreerProjection = new FCreerProjection(_dal);

            this.NavigationService.Navigate(fCreerProjection);
        }

        private void listeSalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedProjection == null)
            {
                return;
            }
            ((Projection)listeProjections.SelectedItem).salle = (Salle)listeSalle.SelectedItem;
        }

        private void dtpDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedProjection == null)
            {
                return;
            }
            ((Projection)listeProjections.SelectedItem).dateDebut = (DateTime)dtpDebut.SelectedDate;
        }

        private void dtpFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedProjection == null)
            {
                return;
            }
            ((Projection)listeProjections.SelectedItem).dateFin = (DateTime)dtpFin.SelectedDate;

        }
    }
}
