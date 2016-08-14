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
using System.Drawing.Imaging;
using System.Drawing;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PublicidadSolution.Vistas
{
    /// <summary>
    /// Interaction logic for Publicidad.xaml
    /// </summary>
    public partial class Publicidad : Window
    {
        #region Atributos y Constructor

        /// <summary>
        /// Lista de elementos de la base de datos
        /// </summary>
        List<ElementoJson> elementosJson = new List<ElementoJson>();
        /// <summary>
        /// Elemento seleccionado de la tabla
        /// </summary>
        ElementoJson selected_elemento = new ElementoJson();
        /// <summary>
        /// Data to database
        /// </summary>
        byte[] data = null;
        String txtBannerWeb = "";

        public MyCustomCollection<string> listaWeb { get; set; }

        public MyCustomCollection<string> listaMovil { get; set; }
        public MyCustomCollection<string> listaVacia { get; set; }

        public Publicidad()
        {
            InitializeComponent();
            InitializeData();
            cbPosicion.Items.Clear();
            listaWeb = new MyCustomCollection<string>(new[] { "Cabecera", "Pie de Pagina", "Izquierda", "Derecha" });
            listaMovil = new MyCustomCollection<string>(new[] { "Cabecera", "Pie de Pagina", "Completa" });
            listaVacia = new MyCustomCollection<string>(new[] { "Seleccione" });
        }

        public void InitializeData()
        {
            emptyFields();
            retrieveElementos();
            elementoDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            initialStateButtonActions();
            enableReadOnly(true);
            elementoDataGrid.IsEnabled = true;
            cbPosicion.DataContext = listaVacia;

        }

        #endregion

        #region UI Validaciones



        private void updateTextFieldsWithSelectedItem()
        {
            int indexSelected = elementoDataGrid.SelectedIndex;
            if (indexSelected != -1)
            {
                selected_elemento = (ElementoJson)elementoDataGrid.SelectedItem;
                txtNombre.Text = selected_elemento.nombre;
                txtURL.Text = selected_elemento.url;
                imageFromBase64(selected_elemento.path);
                setPosicion(selected_elemento.posicion);
                enableActions(true);
            }
        }

        private void enableActions(Boolean value)
        {
            btnModificar.IsEnabled = value;
            btnDelete.IsEnabled = value;
            btnAgregar.IsEnabled = value;
        }

        private void elementoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTextFieldsWithSelectedItem();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenFileWeb_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image File (*.jpg;*.png)|*.jpg;*.png";
            dlg.ShowDialog();
            if (dlg.FileName.Length != 0)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                txtBannerWeb = filePathToBase64(fs.Name);
                imageFromBase64(filePathToBase64(fs.Name));
            }

        }

        private String filePathToBase64(String path)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        private void imageFromBase64(String base64_string)
        {
            byte[] binaryData = Convert.FromBase64String(base64_string);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.EndInit();
            img_banner.Source = bi;
            txtBannerWeb = base64_string;

        }

        private void btnOpenFileMovil_Click(object sender, RoutedEventArgs e)
        {

        }

        public void emptyFields()
        {
            txtNombre.Text = "";
            txtURL.Text = "";
            img_banner.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/add_movil.png", UriKind.RelativeOrAbsolute));
            cbPosicion.DataContext = listaVacia;


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
            txtURL.IsReadOnly = value;
            txtNombre.IsReadOnly = value;
            //txtBannerWeb.IsReadOnly = value;
            btnOpenFileWeb.IsEnabled = !value;

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
            return (txtNombre.Text == "" || txtURL.Text == "" || cbType.SelectedIndex == -1);
        }

        public ElementoJson getTargetFromForm()
        {
            ElementoJson aux = new ElementoJson();
            aux.nombre = txtNombre.Text;
            aux.path = txtBannerWeb;
            aux.url = txtURL.Text;
            //aux.posicion = cbType.Text;
            aux.posicion = getPosicion();

            return aux;
        }

        public ElementoJson updateTargetFromForm()
        {
            ElementoJson aux = selected_elemento;
            aux.nombre = txtNombre.Text;
            aux.path = txtBannerWeb;
            aux.url = txtURL.Text;
            //aux.posicion = cbType.Text;
            aux.posicion = getPosicion();
            return aux;
        }

        public String getPosicion()
        {
            String aux = "";
            if (cbType.SelectedIndex == 0)
            {
                aux = "W";
            }
            else if (cbType.SelectedIndex == 1)
            {
                aux = "M";
            }

            switch (cbPosicion.SelectedItem.ToString())
            {
                case "Cabecera":
                    aux += "T";
                    break;
                case "Pie de Pagina":
                    aux += "D";
                    break;
                case "Izquierda":
                    aux += "L";
                    break;
                case "Derecha":
                    aux += "R";
                    break;
                case "Completa":
                    aux += "C";
                    break;
            }

            return aux;

        }

        public void setPosicion(String aux)
        {
            if (aux[0] == 'W')
            {
                cbPosicion.DataContext = listaWeb;
            }
            else {
                cbPosicion.DataContext = listaMovil;
            }
                

            switch (aux)
            {
                case "WT":
                    cbType.SelectedIndex = 0;
                    cbPosicion.SelectedIndex = 0;
                    break;
                case "WD":
                    cbType.SelectedIndex = 0;
                    cbPosicion.SelectedIndex = 1;
                    break;
                case "WL":
                    cbType.SelectedIndex = 0;
                    cbPosicion.SelectedIndex = 2;
                    break;
                case "WR":
                    cbType.SelectedIndex = 0;
                    cbPosicion.SelectedIndex = 3;
                    break;
                case "MT":
                    cbType.SelectedIndex = 1;
                    cbPosicion.SelectedIndex = 0;
                    break;
                case "MD":
                    cbType.SelectedIndex = 1;
                    cbPosicion.SelectedIndex = 1;
                    break;
                case "MC":
                    cbType.SelectedIndex = 1;
                    cbPosicion.SelectedIndex = 2;
                    break;
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


        #region Button Actions

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selected_elemento != null)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("Seguro que deseas eliminar el elemento \"" + selected_elemento.nombre + "\"?", "Hitch Us - Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    eliminarCampania(updateTargetFromForm());
                    MessageBox.Show("Se ha eliminiado el elemento: " + selected_elemento.nombre, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            elementoDataGrid.IsEnabled = false;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            emptyFields();
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            elementoDataGrid.IsEnabled = false;
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

                    ingresarElemento(selected_elemento);
                    MessageBox.Show("Elemento agregado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    selected_elemento = updateTargetFromForm();
                    modificarElemento(selected_elemento);
                    MessageBox.Show("Target modificado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
                }
            }

        }

        #endregion

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
            elementoDataGrid.ItemsSource = elementosJson;
        }

        public void ingresarElemento(ElementoJson elemento)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("registrarElemento").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(ElementoJson));
                String data2 = JsonConvert.SerializeObject(elemento, Formatting.None, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ" });
                JSrz.WriteObject(MS, elemento);
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

        public void modificarElemento(ElementoJson elemento)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("editarElemento").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(ElementoJson));
                String data2 = JsonConvert.SerializeObject(elemento, Formatting.None, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ" });
                JSrz.WriteObject(MS, elemento);
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

        public void eliminarCampania(ElementoJson elemento)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("eliminarElemento").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(ElementoJson));
                String data2 = JsonConvert.SerializeObject(elemento, Formatting.None, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ" });
                JSrz.WriteObject(MS, elemento);
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

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbType.SelectedIndex == 0)
            {
                cbPosicion.DataContext = listaWeb;
                /*img_banner.Height = 124;
                img_banner.Width = 447;*/

                //img_banner.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/banner_hitch_us.png", UriKind.RelativeOrAbsolute));
            }
            else if (cbType.SelectedIndex == 1)
            {

                cbPosicion.DataContext = listaMovil;
                //img_banner.Source = new BitmapImagListe(new Uri("pack://application:,,,/Resources/add_movil.png", UriKind.RelativeOrAbsolute));
            }
        }
    }

    public class MyCustomCollection<T> : ObservableCollection<T>
    {
        private T _mySelectedItem;

        public MyCustomCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        public T MySelectedItem
        {
            get { return _mySelectedItem; }
            set
            {
                if (Equals(value, _mySelectedItem)) return;
                _mySelectedItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MySelectedItem"));
            }
        }
    }
}
