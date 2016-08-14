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
    /// Interaction logic for AsignacionElemento.xaml
    /// </summary>
    public partial class AsignacionElemento : Window
    {
        #region Atributos y Constructor

        /// <summary>
        /// Lista de elementos de la base de datos
        /// </summary>
        List<ElementoJson> elementosJson = new List<ElementoJson>();
        /// <summary>
        /// Lista de empresas en la base de datos
        /// </summary>
        List<JsonMapping.Empresa> empresasJson = new List<JsonMapping.Empresa>();
        /// <summary>
        /// Lista de campanias de la base de datos
        /// </summary>
        List<CampaniaJson> campaniasJson = new List<CampaniaJson>();
        /// <summary>
        /// Lista de targets de la base de datos
        /// </summary>
        List<SegmentoDetalleCampaniaJson> segmentoDetalleCamp = new List<SegmentoDetalleCampaniaJson>();
        /// <summary>
        /// Lista de targets de la base de datos
        /// </summary>
        List<TargetJson> targetsJson = new List<TargetJson>();
        /// <summary>
        /// Elemento seleccionado de la tabla
        /// </summary>
        SegmentoDetalleCampaniaJson selected_elemento = new SegmentoDetalleCampaniaJson();


        public AsignacionElemento()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            emptyFields();
            retrieveDetails();
            retrieveTargets();
            retrieveCampanias();
            retrieveElementos();
            initialStateButtonActions();
            enableReadOnly(true);
            dgSegmentoDetalleCamp.IsEnabled = true; 
        }

        #endregion

        #region UI Validaciones

        private void cbEmpresas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (elementoDataGrid.SelectedIndex != -1)
            {
                updateTextFieldsWithSelectedItem();
            }*/
        }

        private void updateTextFieldsWithSelectedItem()
        {

            int indexSelected = dgSegmentoDetalleCamp.SelectedIndex;
            if (indexSelected != -1)
            {
                selected_elemento = (SegmentoDetalleCampaniaJson)dgSegmentoDetalleCamp.SelectedItem;
                cbElementos.Text = selected_elemento.elemento.nombre;
                cbCampania.Text = selected_elemento.campania.nombre;
                cbTarget.Text = selected_elemento.targetEdad.nombre;
                tpInicio.Text = selected_elemento.horaInicio;
                tpFin.Text = selected_elemento.horaFin;
                enableActions(true);
            }
        }

        private void enableActions(Boolean value)
        {
            btnModificar.IsEnabled = value;
            btnDelete.IsEnabled = value;
            btnAgregar.IsEnabled = value;
        }

        public void emptyFields()
        {
            slDespliegues.Value = 0;
            slClics.Value = 0;
            img_banner.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/add_movil.png", UriKind.RelativeOrAbsolute));
            cbPago.SelectedIndex = 0;

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
            slClics.IsEnabled = !value;
            slDespliegues.IsEnabled = !value;
            //txtBannerWeb.IsReadOnly = value;
            cbCampania.IsEnabled = !value;
            cbElementos.IsEnabled = !value;
            cbPago.IsEnabled = !value;
            cbTarget.IsEnabled = !value;

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
            return (cbElementos.SelectedIndex == -1 || cbCampania.SelectedIndex == -1 || cbTarget.SelectedIndex == -1);
        }

        public SegmentoDetalleCampaniaJson getTargetFromForm()
        {
            SegmentoDetalleCampaniaJson aux = new SegmentoDetalleCampaniaJson();
            aux.campania = (CampaniaJson)cbCampania.SelectedItem;
            aux.elemento = (ElementoJson)cbElementos.SelectedItem;
            aux.targetEdad = (TargetJson)cbTarget.SelectedItem;
            aux.horaInicio = tpInicio.Text;
            aux.horaFin = tpFin.Text;

            return aux;
        }

        public DetalleCampaniaJson getDetalleCampaniaFromForm()
        {
            DetalleCampaniaJson aux = new DetalleCampaniaJson();
            aux.campania = (CampaniaJson)cbCampania.SelectedItem;
            aux.elemento = (ElementoJson)cbElementos.SelectedItem;
            aux.despliegues = (Int32)slDespliegues.Value;
            aux.clics = (Int32)slClics.Value;
            aux.modoFacturacion = cbPago.Text.Substring(0, 3);
            return aux;
        }


        public SegmentoDetalleCampaniaJson updateSegmentoFromForm()
        {
            SegmentoDetalleCampaniaJson aux = selected_elemento;
            aux.campania = (CampaniaJson)cbCampania.SelectedItem;
            aux.elemento = (ElementoJson)cbElementos.SelectedItem;
            aux.targetEdad = (TargetJson)cbTarget.SelectedItem;
            aux.horaInicio = tpInicio.Text;
            aux.horaFin = tpFin.Text;
            return aux;
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

        #region Button Actions

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            /*if (selected_elemento != null)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("Seguro que deseas eliminar el elemento \"" + selected_elemento.nombre + "\"?", "Hitch Us - Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    eliminarCampania(updateTargetFromForm());
                    MessageBox.Show("Se ha eliminiado el elemento: " + selected_elemento.nombre, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
                }
            }*/
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            dgSegmentoDetalleCamp.IsEnabled = false;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            emptyFields();
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            dgSegmentoDetalleCamp.IsEnabled = false;
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
                    selected_elemento = getTargetFromForm();
                    ingresarDetalleCampania(getDetalleCampaniaFromForm());
                    ingresarSegmentoDetalleCampania(selected_elemento);
                    MessageBox.Show("Asignación agregada exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
                }
                else
                {
                    MessageBox.Show("Uno o más campos vacíos", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (btnModificar.IsEnabled)
            {
                if (selected_elemento != null)
                {
                    selected_elemento = updateSegmentoFromForm();
                    //modificarS(selected_elemento);
                    MessageBox.Show("Target modificado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
                }
            }

        }


        #endregion

        private void cbElementos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbElementos.SelectedIndex != -1)
                imageFromBase64(cbElementos.SelectedValue.ToString());
        }

        private void imageFromBase64(String base64_string)
        {
            byte[] binaryData = Convert.FromBase64String(base64_string);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.EndInit();
            img_banner.Source = bi;

        }

        #region Servicios REST
        ///<summary>
        /// Serivicos de Campaña
        ///</summary>
        public void retrieveElementos()
        {
            elementosJson.Clear();
            var urlUsers = this.FindResource("retrieveElementos").ToString();
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(urlUsers);
            elementosJson = JsonConvert.DeserializeObject<List<ElementoJson>>(content);
            cbElementos.ItemsSource = elementosJson;
        }

        /// <summary>
        /// Trae todos los targets
        /// </summary>
        public void retrieveTargets()
        {
            targetsJson.Clear();
            var urlUsers = this.FindResource("retrieveTargetEdad").ToString();
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(urlUsers);
            targetsJson = JsonConvert.DeserializeObject<List<JsonMapping.TargetJson>>(content);
            cbTarget.ItemsSource = targetsJson;
        }

        /// <summary>
        /// Trae todos las Campañas
        /// </summary>
        public void retrieveCampanias()
        {
            campaniasJson.Clear();
            var urlUsers = this.FindResource("retrieveCampanias").ToString();
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(urlUsers);
            Console.WriteLine(content);
            campaniasJson = JsonConvert.DeserializeObject<List<CampaniaJson>>(content);
            cbCampania.ItemsSource = campaniasJson;
        }

        /// <summary>
        /// Trae todos los targets
        /// </summary>
        public void retrieveDetails()
        {
            segmentoDetalleCamp.Clear();
            var urlUsers = this.FindResource("retrieveSegmentoDetalleCamp").ToString(); 
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(urlUsers);
            segmentoDetalleCamp = JsonConvert.DeserializeObject<List<SegmentoDetalleCampaniaJson>>(content);
            dgSegmentoDetalleCamp.ItemsSource = segmentoDetalleCamp;
        }

        /// <summary>
        ///  Método utilizado para el ingreso de Empresas
        /// </summary>
        /// <param name="empresa"></param>
        public void ingresarDetalleCampania(DetalleCampaniaJson elemento)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("registrarDetalleCampania").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(DetalleCampaniaJson));
                JSrz.WriteObject(MS, elemento);
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

        public void ingresarSegmentoDetalleCampania(SegmentoDetalleCampaniaJson elemento)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("registrarSegmentoDetalleCamp").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(SegmentoDetalleCampaniaJson));
                JSrz.WriteObject(MS, elemento);
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load data by setting the CollectionViewSource.Source property:
            // segmentoDetalleCampaniaJsonViewSource.Source = [generic data source]
        }

        private void dgSegmentoDetalleCamp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTextFieldsWithSelectedItem();
        }
    }
}
