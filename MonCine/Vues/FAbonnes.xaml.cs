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
using MonCine.Data;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour FAbonnes.xaml
    /// </summary>
    public partial class FAbonnes : Page
    {
        private List<Abonne> abonnes;
        private List<Film> films;
        public Abonne selectedAbonne { get; set; }
        private DAL _dal;
        public FAbonnes(DAL dal)
        {
            InitializeComponent();
            _dal = dal;
            readListe();
            fillLists();
        }

        private void readListe()
        {
            abonnes = _dal.ReadAbonnes();
            films = _dal.ReadFilms();
        }

        private void fillLists()
        {
            listAbonnes.ItemsSource = abonnes;
            listeFilms.ItemsSource = films; listeFilms.DataContext = films;
            listeType.ItemsSource = Enum.GetValues(typeof(TypeRecompense));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            enregistrement();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void listAbonnes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAbonne = (Abonne)listAbonnes.SelectedItem;
            if (selectedAbonne == null) { return; }
        }

        private void enregistrement()
        {
            TypeRecompense type = (TypeRecompense)listeType.SelectedItem;
            Film film = (Film)listeFilms.SelectedItem;
            Recompense newRecompense = new Recompense( type, film);
            selectedAbonne.recompenses.Add(newRecompense);
            _dal.AddRecompense(selectedAbonne);

        }

    }
}
