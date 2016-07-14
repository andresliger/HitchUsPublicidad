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
    /// Interaction logic for Publicidad.xaml
    /// </summary>
    public partial class Publicidad : Window
    {
        public Publicidad()
        {
            InitializeComponent();
        }

        private void mnuHitchUs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Principal home = new Principal();
            home.Show();
            this.Close();
        }

        private void mnuEmpresas_Click(object sender, RoutedEventArgs e)
        {
            Empresa home = new Empresa();
            home.Show();
            this.Close();
        }

        private void mnuCampania_Click(object sender, RoutedEventArgs e)
        {
            Campania home = new Campania();
            home.Show();
            this.Close();
        }

        private void mnuTarget_Click(object sender, RoutedEventArgs e)
        {
            Target home = new Target();
            home.Show();
            this.Close();
        }

        private void mnuPublicidad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenFileWeb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenFileMovil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
