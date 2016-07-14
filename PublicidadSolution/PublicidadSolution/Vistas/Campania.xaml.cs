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
using Controller;
using Modelo;

namespace PublicidadSolution.Vistas
{
    /// <summary>
    /// Interaction logic for Campania.xaml
    /// </summary>
    public partial class Campania : Window
    {
        CAMPANIA selected_campania = new CAMPANIA();

        public Campania()
        {
            InitializeComponent();
            cbEmpresas.ItemsSource = EmpresaDao.Instance.Empresas;
        }

        private void chkEstado_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void mnuHitchUs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void mnuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Principal ventana = new Principal();
            ventana.Show();
            this.Close();
        }

        private void mnuEmpresas_Click(object sender, RoutedEventArgs e)
        {
            Empresa ventana = new Empresa();
            ventana.Show();
            this.Close();
        }

        private void mnuCampania_Click(object sender, RoutedEventArgs e)
        {
            Campania ventana = new Campania();
            ventana.Show();
            this.Close();
        }

        private void mnuTarget_Click(object sender, RoutedEventArgs e)
        {
            Target ventana = new Target();
            ventana.Show();
            this.Close();
        }

        private void mnuPublicidad_Click(object sender, RoutedEventArgs e)
        {
            Publicidad ventana = new Publicidad();
            ventana.Show();
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

            CAMPANIA aux = new CAMPANIA();
            aux.EMPRESA = (EMPRESA)cbEmpresas.SelectedItem;
            aux.NOMBRE = txtCampania.Text;
            aux.DESCRIPCION = txtDescripcion.Text;
            aux.FECHA_INICIO = (DateTime)calendarInicio.SelectedDate;
            aux.FECHA_FIN = (DateTime)calendarFin.SelectedDate;
            aux.ESTADO = (bool)chkEstado.IsChecked ? "S" : "N";

        }

        private void campaniaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (campaniaDataGrid.SelectedIndex != -1)
            {
                selected_campania = (CAMPANIA)campaniaDataGrid.SelectedItem;
                setFormFromSelectedData();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Load data into the table EMPRESA. You can modify this code as needed.
        }

        private void cbEmpresas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEmpresas.SelectedIndex == -1)
            {
                emptyFields();
            }
        }

        #region Controles en UI
        public void emptyFields()
        {
            txtCampania.Text = "";
            txtDescripcion.Text = "";
            calendarInicio.SelectedDate = null;
            calendarInicio.DisplayDate = DateTime.Today;
            calendarFin.SelectedDate = null;
            chkEstado.IsChecked = false;
        }

        public void setFormFromSelectedData()
        {
            cbEmpresas.SelectedItem = selected_campania.EMPRESA;
            txtCampania.Text = selected_campania.NOMBRE;
            txtDescripcion.Text = selected_campania.DESCRIPCION;
            calendarInicio.DisplayDate = selected_campania.FECHA_INICIO;
            calendarFin.DisplayDate = selected_campania.FECHA_FIN;
            chkEstado.IsChecked = selected_campania.ESTADO == "A";

        }

        #endregion


    }
}
