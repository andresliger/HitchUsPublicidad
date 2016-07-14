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
    /// Interaction logic for Target.xaml
    /// </summary>
    public partial class Target : Window
    {
        public Target()
        {
            InitializeComponent();
        }

        private void mnuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
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
            Publicidad home = new Publicidad();
            home.Show();
            this.Close();
        }

        private void chkMasculino_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkFemenino_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkOtro_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkTodos_Checked(object sender, RoutedEventArgs e)
        {

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

        private void targetDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void mnuHitchUs_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
