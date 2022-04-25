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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MonCine.Data;


namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour FCreerProjection.xaml
    /// </summary>
    public partial class FCreerProjection : Page
    {
        public List<Film> Films = new List<Film>();

        private DAL _dal;

        public FCreerProjection(DAL dal)
        {
            InitializeComponent();
            _dal = dal;
            InitializeLists();
        }

        private void InitializeLists()
        {
            Films = _dal.ReadFilms();
            listeFilms.ItemsSource = Films;
            listeSalle.ItemsSource = Enum.GetValues(typeof(Salle));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            EnregistrerProjection();
        }

        private void EnregistrerProjection()
        {
            Film film = (Film)listeFilms.SelectedItem;
            DateTime debut = (DateTime)dtpDateDebut.SelectedDate;
            DateTime fin = (DateTime)dtpDateFin.SelectedDate;
            Projection projection = new Projection((Salle)listeSalle.SelectedItem,debut,fin,film);
            _dal.AddProjection(projection);
            this.NavigationService.GoBack();
        }
    }
}
