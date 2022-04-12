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
    /// Logique d'interaction pour Films.xaml
    /// </summary>
    public partial class FFilms : Page
    {
        List<Film> films = new List<Film>();
        public FFilms(DAL dal)
        {
            InitializeComponent();
            //dal.AddFilms(new Film("Film Test", DateTime.UtcNow));
            films = dal.ReadFilms();
            listeFilms.ItemsSource = films;
        }
    }
}
