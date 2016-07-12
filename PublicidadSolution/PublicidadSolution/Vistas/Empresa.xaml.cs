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
    /// Interaction logic for Empresa.xaml
    /// </summary>
    public partial class Empresa : Window
    {

        Modelo.EMPRESA empresa_selected = new EMPRESA();

        public Empresa()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            empresaDataGrid.ItemsSource = EmpresaDao.Instance.Empresas;
            empresaDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            empresaDataGrid.Columns[2].Visibility = Visibility.Collapsed;
            emptyFields();
        }

        public void emptyFields()
        {
            txtEmpresa.Text = "";
            txtRUC.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtRepresentante.Text = "";
            txtTelefono.Text = "";
            btnModificar.IsEnabled = false;
            btnDelete.IsEnabled = false;
            empresaDataGrid.UnselectAllCells();
            empresa_selected = null;
        }

        private void updateTextFieldsWithSelectedUser()
        {
            int indexSelected = empresaDataGrid.SelectedIndex;
            if (indexSelected != -1)
            {
                empresa_selected = (Modelo.EMPRESA)empresaDataGrid.SelectedItem;
                txtEmpresa.Text = empresa_selected.RAZON_SOCIAL;
                txtRUC.Text = empresa_selected.RUC;
                txtDireccion.Text = empresa_selected.DIRECCION;
                txtEmail.Text = empresa_selected.EMAIL;
                txtRepresentante.Text = empresa_selected.REPRESENTANTE;
                txtTelefono.Text = empresa_selected.TELEFONO;
                enableActions(true);
            }
        }

        private void enableActions(Boolean value)
        {
            btnModificar.IsEnabled = value;
            btnDelete.IsEnabled = value;
        }

        public Boolean areEmptyFields()
        {
            return (txtEmpresa.Text == ""
                || txtRUC.Text == ""
                || txtDireccion.Text == ""
                || txtEmail.Text == ""
                || txtRepresentante.Text == ""
                || txtTelefono.Text == "");
        }

        private Modelo.EMPRESA getEmpresaFromForm()
        {
            Modelo.EMPRESA aux = new Modelo.EMPRESA();
            aux.RAZON_SOCIAL = txtEmpresa.Text;
            aux.RUC = txtRUC.Text;
            aux.DIRECCION = txtDireccion.Text;
            aux.EMAIL = txtEmail.Text;
            aux.REPRESENTANTE = txtRepresentante.Text;
            aux.TELEFONO = txtTelefono.Text;
            return aux;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void empresaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTextFieldsWithSelectedUser();
        }

        private void mnuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuHitchUs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuUsuarios_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuEmpresas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuPublicidad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuCampania_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuTarget_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
