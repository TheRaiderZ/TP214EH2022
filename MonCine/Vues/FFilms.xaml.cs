using MonCine.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
        public List<Realisateur> Realisateurs = new List<Realisateur>();
        public Film SelectedFilm { get; set; }
        private DAL _dal;
        public FFilms(DAL dal)
        {
            InitializeComponent();
            
            _dal = dal;
            Films = _dal.ReadFilms();
            Realisateurs = _dal.ReadRealisateurs();
            listeFilms.ItemsSource = Films;
            listeFilms.DataContext = Films;
            listeRealisateurs.ItemsSource = Realisateurs;
            listeRealisateurs.DataContext = Realisateurs;
            listeCategories.ItemsSource = Enum.GetValues(typeof(Categorie));
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
            listeFilms.SelectedItem = null;
            SelectedFilm = null;
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
            SelectedFilm= (Film)listeFilms.SelectedItem;
            if (SelectedFilm==null)
            {
                return;
            }
            listeRealisateurs.SelectedItem = SelectedFilm.Realisateur;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            EnregistrerChangements();
        }

        public void EnregistrerChangements() {
            foreach (var item in Films)
            {
                _dal.UpdateFilm(item);
            }
        }

        private void listeCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedFilm==null)
            {
                return;
            }
            ((Film)listeFilms.SelectedItem).Categories = listeCategories.SelectedItems.Cast<Categorie>().ToList();
        }

        
    }

    public class CategoriesToStringConverter : IValueConverter
    {

        #region IValueConverter Members

        
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo language)
        {
            List<Categorie> categories = (List<Categorie>)value;

            return String.Join(',', categories);
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo language)
        {
            
            throw new NotImplementedException();
        }

        #endregion
    }

    // Set ListBox SelectedItems based on another collection (https://stackoverflow.com/a/15540770) 
    public static class ListBoxExtensions
    {
        // Using a DependencyProperty as the backing store for SearchValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemListProperty =
            DependencyProperty.RegisterAttached("SelectedItemList", typeof(IList), typeof(ListBoxExtensions),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemListChanged)));

        public static IList GetSelectedItemList(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemListProperty);
        }

        public static void SetSelectedItemList(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemListProperty, value);
        }

        private static void OnSelectedItemListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listbox = d as ListBox;
            if (listbox != null)
            {
                listbox.SelectedItems.Clear();
                var selectedItems = e.NewValue as IList;
                if (selectedItems != null)
                {
                    foreach (var item in selectedItems)
                    {
                        listbox.SelectedItems.Add(item);
                    }
                }
            }
        }
    }
}
