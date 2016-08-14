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
    /// Interaction logic for Campania.xaml
    /// </summary>
    public partial class Campania : Window
    {

        #region Atributos y Constructor
        /// <summary>
        /// Lista de campañas de la base de datos
        /// </summary>
        List<CampaniaJson> campaniasJson = new List<CampaniaJson>();
        /// <summary>
        /// Lista de empresas en la base de datos
        /// </summary>
        List<JsonMapping.Empresa> empresasJson = new List<JsonMapping.Empresa>();
        /// <summary>
        /// Campaña seleccionada de la tabla
        /// </summary>
        CampaniaJson selected_campania = new CampaniaJson();

        public Campania()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            emptyFields();
            retrieveEmpresas();
            retrieveCampanias();
            campaniaDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            initialStateButtonActions();
            enableReadOnly(true);
            campaniaDataGrid.IsEnabled = true;
        }

        #endregion

        #region UI Validaciones

        private void campaniaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (campaniaDataGrid.SelectedIndex != -1)
            {
                updateTextFieldsWithSelectedItem();
            }
        }

        private void updateTextFieldsWithSelectedItem()
        {
            int indexSelected = campaniaDataGrid.SelectedIndex;
            if (indexSelected != -1)
            {
                selected_campania = (CampaniaJson)campaniaDataGrid.SelectedItem;
                cbEmpresas.Text = selected_campania.empresa.razonSocial;
                txtCampania.Text = selected_campania.nombre;
                txtDescripcion.Text = selected_campania.descripcion;
                calendarInicio.SelectedDate = selected_campania.fechaInicio;
                calendarFin.SelectedDate = selected_campania.fechaFin;
                chkEstado.IsChecked = selected_campania.estado == "A" ? true : false;
                enableActions(true);
            }
        }

        private void enableActions(Boolean value)
        {
            btnModificar.IsEnabled = value;
            btnDelete.IsEnabled = value;
            btnAgregar.IsEnabled = value;
        }

        private void initialStateButtonActions()
        {
            btnAgregar.IsEnabled = true;
            btnCancelar.IsEnabled = false;
            btnConfirmar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void enableReadOnly(Boolean value)
        {
            cbEmpresas.IsReadOnly = value;
            txtCampania.IsReadOnly = value;
            txtDescripcion.IsReadOnly = value;
            calendarInicio.IsEnabled = !value;
            calendarFin.IsEnabled = !value;
            chkEstado.IsEnabled = !value;
        }

        private void enableAproved(Boolean value, Button btn)
        {
            changeButtonActions(false);
            btn.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            btnConfirmar.IsEnabled = true;
        }

        private void changeButtonActions(Boolean value)
        {
            btnAgregar.IsEnabled = value;
            btnCancelar.IsEnabled = value;
            btnConfirmar.IsEnabled = value;
            btnModificar.IsEnabled = value;
            btnDelete.IsEnabled = value;
        }

        public Boolean hasEmptyFields()
        {
            return (txtCampania.Text == "" || txtDescripcion.Text == "" || cbEmpresas.SelectedIndex == -1 || !calendarInicio.SelectedDate.HasValue || !calendarFin.SelectedDate.HasValue);
        }

        public CampaniaJson getCampaniaFromForm()
        {
            CampaniaJson aux = new CampaniaJson();
            aux.empresa = (JsonMapping.Empresa)cbEmpresas.SelectedItem;
            aux.nombre = txtCampania.Text;
            aux.descripcion = txtDescripcion.Text;
            aux.fechaCreacion = DateTime.UtcNow.Date;
            aux.fechaInicio = calendarInicio.SelectedDate.Value;
            aux.fechaFin = (DateTime)calendarFin.SelectedDate.Value;
            aux.estado = (bool)chkEstado.IsChecked ? "A" : "I";
            return aux;
        }

        public CampaniaJson updateCampaniaFromForm()
        {
            CampaniaJson aux = selected_campania;
            aux.nombre = txtCampania.Text;
            aux.descripcion = txtDescripcion.Text;
            aux.fechaCreacion = DateTime.UtcNow.Date;
            aux.fechaInicio = (DateTime)calendarInicio.SelectedDate.Value;
            aux.fechaFin = (DateTime)calendarFin.SelectedDate.Value;
            aux.estado = (bool)chkEstado.IsChecked ? "A" : "I";
            return aux;
        }

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
            cbEmpresas.SelectedItem = selected_campania.empresa;
            txtCampania.Text = selected_campania.nombre;
            txtDescripcion.Text = selected_campania.descripcion;
            calendarInicio.DisplayDate = selected_campania.fechaInicio;
            calendarFin.DisplayDate = selected_campania.fechaFin;
            chkEstado.IsChecked = selected_campania.estado == "A";

        }

        #endregion

        #region Button Actions


        /// <summary>
        /// Agrega una Campaña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            emptyFields();
            enableReadOnly(false);
            retrieveEmpresas();
            enableAproved(true, (Button)sender);
            campaniaDataGrid.IsEnabled = false;

        }

        /// <summary>
        /// Modifica una campaña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            campaniaDataGrid.IsEnabled = false;

        }

        /// <summary>
        /// Elimina una campaña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selected_campania != null)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("Seguro que deseas eliminar la campaña \"" + selected_campania.nombre + "\"?", "Hitch Us - Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    eliminarCampania(updateCampaniaFromForm());
                    MessageBox.Show("Se ha eliminiado la campaña: " + selected_campania.nombre, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    selected_campania = getCampaniaFromForm();
                    ingresarCampania(selected_campania);
                    MessageBox.Show("Campaña agregada exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
                }
                else
                {
                    MessageBox.Show("Uno o más campos vacíos", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (btnModificar.IsEnabled)
            {
                if (selected_campania != null)
                {
                    selected_campania = updateCampaniaFromForm();
                    modificarCampania(selected_campania);
                    MessageBox.Show("Campaña modificada exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
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

        #region Servicios REST
        ///<summary>
        /// Serivicos de Campaña
        ///</summary>

        public void retrieveEmpresas()
        {
            empresasJson.Clear();
            var urlUsers = this.FindResource("retrieveEmpresas").ToString();
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(urlUsers);
            empresasJson = JsonConvert.DeserializeObject<List<JsonMapping.Empresa>>(content);
            cbEmpresas.ItemsSource = empresasJson;
        }

        public void retrieveCampanias()
        {
            campaniasJson.Clear();
            var urlUsers = this.FindResource("retrieveCampanias").ToString();
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(urlUsers);
            Console.WriteLine(content);
            campaniasJson = JsonConvert.DeserializeObject<List<CampaniaJson>>(content);
            campaniaDataGrid.ItemsSource = campaniasJson;
        }


        public void ingresarCampania(CampaniaJson campania)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("registrarCampania").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(CampaniaJson));
                String data2 = JsonConvert.SerializeObject(campania, Formatting.None, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ" });
                JSrz.WriteObject(MS, campania);
                //string data = Encoding.UTF8.GetString(MS.ToArray(), 0, (int)MS.Length);
                streamWriter.Write(data2);
                Console.WriteLine(data2);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }

        }

        public void modificarCampania(CampaniaJson campania)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("editarCampania").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(CampaniaJson));
                String data2 = JsonConvert.SerializeObject(campania, Formatting.None, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ" });
                JSrz.WriteObject(MS, campania);
                //string data = Encoding.UTF8.GetString(MS.ToArray(), 0, (int)MS.Length);
                streamWriter.Write(data2);
                Console.WriteLine(data2);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }

        }

        public void eliminarCampania(CampaniaJson campania)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("eliminarCampania").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(CampaniaJson));
                String data2 = JsonConvert.SerializeObject(campania, Formatting.None, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ" });
                JSrz.WriteObject(MS, campania);
                //string data = Encoding.UTF8.GetString(MS.ToArray(), 0, (int)MS.Length);
                streamWriter.Write(data2);
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
