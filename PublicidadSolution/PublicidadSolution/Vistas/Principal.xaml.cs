using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PublicidadSolution.Vistas
{
    /// <summary>
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
        }        

        private void mnuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            PublicidadSolution.PublicidadDataSet publicidadDataSet = ((PublicidadSolution.PublicidadDataSet)(this.FindResource("publicidadDataSet")));
            // Load data into the table USUARIO. You can modify this code as needed.
            PublicidadSolution.PublicidadDataSetTableAdapters.USUARIOTableAdapter publicidadDataSetUSUARIOTableAdapter = new PublicidadSolution.PublicidadDataSetTableAdapters.USUARIOTableAdapter();
            publicidadDataSetUSUARIOTableAdapter.Fill(publicidadDataSet.USUARIO);
            System.Windows.Data.CollectionViewSource uSUARIOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("uSUARIOViewSource")));
            uSUARIOViewSource.View.MoveCurrentToFirst();
        }
    }
}
