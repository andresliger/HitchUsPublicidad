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
using JsonMapping;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;

namespace PublicidadSolution.Vistas
{
    /// <summary>
    /// Interaction logic for Empresa.xaml
    /// </summary>
    public partial class Empresa : Window
    {

        #region Atributos y Constructor
        /// <summary>
        /// Empresa seleccionada
        /// </summary>
        JsonMapping.Empresa empresa_selected = new JsonMapping.Empresa();
        /// <summary>
        /// Lista de empresas
        /// </summary>
        List<JsonMapping.Empresa> empresasJson = new List<JsonMapping.Empresa>();

        public Empresa()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            retrieveEmpresas();
            emptyFields();
            initialStateButtonActions();
            enableReadOnly(true);
            empresaDataGrid.IsEnabled = true;
        }
        #endregion

        #region UI Validaciones


        private void empresaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTextFieldsWithSelectedUser();

        }

        public void emptyFields()
        {
            txtEmpresa.Text = "";
            txtRUC.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtRepresentante.Text = "";
            txtTelefono.Text = "";
            empresaDataGrid.UnselectAllCells();
            empresa_selected = null;

        }

        private void initialStateButtonActions()
        {
            btnAgregar.IsEnabled = true;
            btnCancelar.IsEnabled = false;
            btnConfirmar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }


        private void changeButtonActions(Boolean value)
        {
            btnAgregar.IsEnabled = value;
            btnCancelar.IsEnabled = value;
            btnConfirmar.IsEnabled = value;
            btnModificar.IsEnabled = value;
            btnDelete.IsEnabled = value;
        }

        private void enableActions(Boolean value)
        {
            btnModificar.IsEnabled = value;
            btnDelete.IsEnabled = value;
            btnAgregar.IsEnabled = value;
        }

        private void enableReadOnly(Boolean value)
        {
            txtEmpresa.IsReadOnly = value;
            txtRUC.IsReadOnly = value;
            txtDireccion.IsReadOnly = value;
            txtEmail.IsReadOnly = value;
            txtRepresentante.IsReadOnly = value;
            txtTelefono.IsReadOnly = value;
        }


        private void enableAproved(Boolean value, Button btn)
        {
            changeButtonActions(false);
            btn.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            btnConfirmar.IsEnabled = true;
        }

        private void updateTextFieldsWithSelectedUser()
        {
            int indexSelected = empresaDataGrid.SelectedIndex;
            if (indexSelected != -1)
            {
                empresa_selected = (JsonMapping.Empresa)empresaDataGrid.SelectedItem;
                txtEmpresa.Text = empresa_selected.razonSocial;
                txtRUC.Text = empresa_selected.ruc;
                txtDireccion.Text = empresa_selected.direccion;
                txtEmail.Text = empresa_selected.email;
                txtRepresentante.Text = empresa_selected.representante;
                txtTelefono.Text = empresa_selected.telefono;
                enableActions(true);
            }
        }

        private Boolean existeRUC(String ruc)
        {
            var findEmpresa = empresasJson.Find(a => a.ruc.Equals(ruc));
            if (findEmpresa != null)
                return true;
            else
                return false;
        }


        public Boolean hasEmptyFields()
        {
            return (txtRUC.Text == "" || txtTelefono.Text == "" || txtRepresentante.Text == "" || txtEmpresa.Text == "" || txtEmail.Text == "" || txtDireccion.Text == "");
        }

        public JsonMapping.Empresa getEmpresaFromForm()
        {
            JsonMapping.Empresa e = new JsonMapping.Empresa();
            e.ruc = txtRUC.Text;
            e.telefono = txtTelefono.Text;
            e.representante = txtRepresentante.Text;
            e.razonSocial = txtEmpresa.Text;
            e.email = txtEmail.Text;
            e.direccion = txtDireccion.Text;
            return e;
        }

        public JsonMapping.Empresa updateEmpresaFromForm()
        {
            JsonMapping.Empresa e = empresa_selected;
            e.telefono = txtTelefono.Text;
            e.representante = txtRepresentante.Text;
            e.razonSocial = txtEmpresa.Text;
            e.email = txtEmail.Text;
            e.direccion = txtDireccion.Text;
            return e;
        }


        #endregion

        #region CRUD Usuarios evento Button

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            emptyFields();
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            empresaDataGrid.IsEnabled = false;

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            empresaDataGrid.IsEnabled = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (empresa_selected != null)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("Seguro que deseas eliminar la empresa \"" + empresa_selected.razonSocial + "\"?", "Hitch Us - Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    eliminarEmpresa(getEmpresaFromForm());
                    MessageBox.Show("Se ha eliminiado la empresa: " + empresa_selected.razonSocial, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    retrieveEmpresas();
                    InitializeData();
                }
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            InitializeData();

        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (btnAgregar.IsEnabled)
            {
                if (!hasEmptyFields())
                {
                    empresa_selected = getEmpresaFromForm();
                    if (existeRUC(empresa_selected.ruc))
                    {
                        MessageBox.Show("El RUC ya existe", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        ingresarEmpresa(empresa_selected);
                        MessageBox.Show("Empresa agregada exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                        InitializeData();
                    }
                }
                else
                {
                    MessageBox.Show("Uno o más campos vacíos", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (btnModificar.IsEnabled)
            {
                if (empresa_selected != null)
                {
                    String auxEmpresa = "";
                    auxEmpresa = empresa_selected.ruc;
                    empresa_selected = updateEmpresaFromForm();
                    if (auxEmpresa.Equals(empresa_selected.ruc))
                    {
                        modificarEmpresa(empresa_selected);
                        MessageBox.Show("Empresa modificada exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                        InitializeData();
                    }
                    else if (existeRUC(empresa_selected.ruc))
                    {
                        MessageBox.Show("El RUC ya existe", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        modificarEmpresa(empresa_selected);
                        MessageBox.Show("Empresa modificada exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                        InitializeData();
                    }
                }
            }
        }

        #endregion

        #region Opciones del Menu
        /// <summary>
        /// Menu usuarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Principal ventana = new Principal();
            ventana.Show();
            this.Close();
        }
        /// <summary>
        /// Menu Campaña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCampania_Click(object sender, RoutedEventArgs e)
        {
            Campania ventana = new Campania();
            ventana.Show();
            this.Close();
        }
        /// <summary>
        /// Menu Target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTarget_Click(object sender, RoutedEventArgs e)
        {
            Target ventana = new Target();
            ventana.Show();
            this.Close();
        }
        /// <summary>
        /// Menu Empresas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEmpresas_Click(object sender, RoutedEventArgs e)
        {
            Empresa ventanaEmpresa = new Empresa();
            ventanaEmpresa.Show();
            this.Close();
        }
        /// <summary>
        /// Menu Publicidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPublicidad_Click(object sender, RoutedEventArgs e)
        {
            Publicidad ventana = new Publicidad();
            ventana.Show();
            this.Close();
        }
        /// <summary>
        /// Cierra Sesion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Inicio home = new Inicio();
            home.Show();
            this.Close();
        }

        private void mnuAsignacion_Click(object sender, RoutedEventArgs e)
        {
            AsignacionElemento ventana = new AsignacionElemento();
            ventana.Show();
            this.Close();
        }
        #endregion

        #region REST Services
        /// <summary>
        /// Método utilizado para cargar las empresas de la base de datos
        /// </summary>
        public void retrieveEmpresas()
        {
            empresasJson.Clear();
            var urlUsers = this.FindResource("retrieveEmpresas").ToString();
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(urlUsers);
            empresasJson = JsonConvert.DeserializeObject<List<JsonMapping.Empresa>>(content);
            empresaDataGrid.ItemsSource = empresasJson;
        }

        /// <summary>
        ///  Método utilizado para el ingreso de Empresas
        /// </summary>
        /// <param name="empresa"></param>
        public void ingresarEmpresa(JsonMapping.Empresa empresa)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("registrarEmpresa").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(JsonMapping.Empresa));
                JSrz.WriteObject(MS, empresa);
                string data = Encoding.UTF8.GetString(MS.ToArray(), 0, (int)MS.Length);
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }

        /// <summary>
        ///  Método utilizado para la actualización de Empresas
        /// </summary>
        /// <param name="empresa"></param>
        public void modificarEmpresa(JsonMapping.Empresa empresa)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("editarEmpresa").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(JsonMapping.Empresa));
                JSrz.WriteObject(MS, empresa);
                string data = Encoding.UTF8.GetString(MS.ToArray(), 0, (int)MS.Length);
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }

        }

        /// <summary>
        ///  Método utilizado para eliminar una Empresa
        /// </summary>
        /// <param name="empresa"></param>
        public void eliminarEmpresa(JsonMapping.Empresa empresa)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("eliminarEmpresa").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(JsonMapping.Empresa));
                JSrz.WriteObject(MS, empresa);
                string data = Encoding.UTF8.GetString(MS.ToArray(), 0, (int)MS.Length);
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }

        #endregion        
    }
}
