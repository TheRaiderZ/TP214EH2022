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
        public FAbonnes(DAL dal)
        {
            InitializeComponent();
            abonnes = dal.ReadAbonnes();
            listAbonnes.ItemsSource = abonnes;
        }
    }
}
