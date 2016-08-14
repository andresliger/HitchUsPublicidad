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
    /// Interaction logic for Target.xaml
    /// </summary>
    public partial class Target : Window
    {

        #region Atributos y Constructor
        /// <summary>
        /// Target seleccionado
        /// </summary>
        TargetJson selected_target = new TargetJson();

        /// <summary>
        /// Lista de targets de la base de datos
        /// </summary>
        List<TargetJson> targetsJson = new List<TargetJson>();

        /// <summary>
        /// Lista de empresas de la Base
        /// </summary>
        List<JsonMapping.Empresa> empresasJson = new List<JsonMapping.Empresa>();

        public Target()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            emptyFields();
            retrieveEmpresas();
            retrieveTargets();
            targetDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            initialStateButtonActions();
            enableReadOnly(true);
            targetDataGrid.IsEnabled = true;
        }

        #endregion

        #region UI Validaciones
        private void targetDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (targetDataGrid.SelectedIndex != -1)
            {
                updateTextFieldsWithSelectedItem();
            }
        }

        private void updateTextFieldsWithSelectedItem()
        {
            int indexSelected = targetDataGrid.SelectedIndex;
            if (indexSelected != -1)
            {
                selected_target = (TargetJson)targetDataGrid.SelectedItem;
                cbEmpresas.Text = selected_target.empresa.razonSocial;
                txtTarget.Text = selected_target.nombre;
                txtDescripcion.Text = selected_target.descripcion;
                checkGender(selected_target.genero);
                slMinimo.Value = selected_target.edadMinima;
                slMaximo.Value = selected_target.edadMaxima;
                enableActions(true);
            }
        }

        private void checkGender(String formed_gender)
        {
            switch (formed_gender)
            {
                case "M__":
                    chkMasculino.IsChecked = true;
                    chkFemenino.IsChecked = false;
                    chkOtro.IsChecked = false;
                    chkTodos.IsChecked = false;
                    break;
                case "_F_":
                    chkMasculino.IsChecked = false;
                    chkFemenino.IsChecked = true;
                    chkOtro.IsChecked = false;
                    chkTodos.IsChecked = false;
                    break;
                case "__O":
                    chkMasculino.IsChecked = false;
                    chkFemenino.IsChecked = false;
                    chkOtro.IsChecked = true;
                    chkTodos.IsChecked = false;
                    break;
                case "MF_":
                    chkMasculino.IsChecked = true;
                    chkFemenino.IsChecked = true;
                    chkOtro.IsChecked = false;
                    chkTodos.IsChecked = false;
                    break;
                case "M_0":
                    chkMasculino.IsChecked = true;
                    chkFemenino.IsChecked = false;
                    chkOtro.IsChecked = true;
                    chkTodos.IsChecked = false;
                    break;
                case "_F0":
                    chkMasculino.IsChecked = false;
                    chkFemenino.IsChecked = true;
                    chkOtro.IsChecked = true;
                    chkTodos.IsChecked = false;
                    break;
                case "MFO":
                    chkMasculino.IsChecked = true;
                    chkFemenino.IsChecked = true;
                    chkOtro.IsChecked = true;
                    chkTodos.IsChecked = true;
                    break;
            }
        }

        private String stringFromChecks()
        {
            String aux = "MFO";
            if (!chkMasculino.IsChecked.Value)
            {
                aux = aux.Replace('M', '_');
            }
            if (!chkFemenino.IsChecked.Value)
            {
                aux = aux.Replace('F', '_');
            }
            if (!chkOtro.IsChecked.Value)
            {
                aux = aux.Replace('O', '_');
            }
            if (chkTodos.IsChecked.Value)
            {
                aux = "MFO";
            }
            return aux;
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
            chkFemenino.IsEnabled = !value;
            chkMasculino.IsEnabled = !value;
            chkOtro.IsEnabled = !value;
            chkTodos.IsEnabled = !value;
            slMinimo.IsEnabled = !value;
            slMaximo.IsEnabled = !value;
            txtTarget.IsReadOnly = value;
            txtDescripcion.IsReadOnly = value;
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
            return (txtTarget.Text == "" || txtDescripcion.Text == "" || cbEmpresas.SelectedIndex == -1 || (!(Boolean)chkMasculino.IsChecked && (Boolean)chkFemenino.IsChecked && (Boolean)chkMasculino.IsChecked));
        }

        public TargetJson getTargetFromForm()
        {
            TargetJson aux = new TargetJson();
            aux.empresa = (JsonMapping.Empresa)cbEmpresas.SelectedItem;
            aux.nombre = txtTarget.Text;
            aux.descripcion = txtDescripcion.Text;
            aux.genero = stringFromChecks();
            aux.edadMinima = Convert.ToInt32(slMinimo.Value);
            aux.edadMaxima = Convert.ToInt32(slMaximo.Value);
            return aux;
        }

        public TargetJson updateTargetFromForm()
        {
            TargetJson aux = selected_target;
            aux.empresa = (JsonMapping.Empresa)cbEmpresas.SelectedItem;
            aux.nombre = txtTarget.Text;
            aux.descripcion = txtDescripcion.Text;
            aux.genero = stringFromChecks();
            aux.edadMinima = Convert.ToInt32(slMinimo.Value);
            aux.edadMaxima = Convert.ToInt32(slMaximo.Value);
            return aux;
        }

        public void emptyFields()
        {
            txtTarget.Text = "";
            txtDescripcion.Text = "";
            chkFemenino.IsChecked = false;
            chkMasculino.IsChecked = false;
            chkOtro.IsChecked = false;
            chkTodos.IsChecked = false;
            slMinimo.Value = 18;
            slMaximo.Value = 36;
                        
        }

        public void setFormFromSelectedData()
        {
            cbEmpresas.SelectedItem = selected_target.empresa;
            txtTarget.Text = selected_target.nombre;
            txtDescripcion.Text = selected_target.descripcion;
            checkGender(selected_target.genero);
            slMinimo.Value = selected_target.edadMinima;
            slMinimo.Value = selected_target.edadMaxima;

        }

        private void chkMasculino_Checked(object sender, RoutedEventArgs e)
        {
            if (chkFemenino.IsChecked.Value && chkOtro.IsChecked.Value)
            {
                if (chkMasculino.IsChecked.Value)
                {
                    chkTodos.IsChecked = true;
                }
                else
                {
                    chkTodos.IsChecked = false;
                }
            }
            else
            {
                chkTodos.IsChecked = false;
            }
        }

        private void chkFemenino_Checked(object sender, RoutedEventArgs e)
        {
            if (chkMasculino.IsChecked.Value && chkOtro.IsChecked.Value)
            {
                if (chkFemenino.IsChecked.Value)
                {
                    chkTodos.IsChecked = true;
                }
                else
                {
                    chkTodos.IsChecked = false;
                }
            }
            else
            {
                chkTodos.IsChecked = false;
            }
        }

        private void chkOtro_Checked(object sender, RoutedEventArgs e)
        {
            if (chkMasculino.IsChecked.Value && chkFemenino.IsChecked.Value)
            {
                if (chkOtro.IsChecked.Value)
                {
                    chkTodos.IsChecked = true;
                }
                else
                {
                    chkTodos.IsChecked = false;
                }
            }
            else {
                chkTodos.IsChecked = false;
            }

        }

        private void chkTodos_Checked(object sender, RoutedEventArgs e)
        {
            if (chkTodos.IsChecked.Value)
            {
                chkFemenino.IsChecked = true;
                chkMasculino.IsChecked = true;
                chkOtro.IsChecked = true;
            }
            else {
                chkFemenino.IsChecked = false;
                chkMasculino.IsChecked = false;
                chkOtro.IsChecked = false;
            }
        }

        #endregion


        #region Button Actions

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            emptyFields();
            enableReadOnly(false);
            retrieveEmpresas();
            enableAproved(true, (Button)sender);
            targetDataGrid.IsEnabled = false;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            targetDataGrid.IsEnabled = false;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selected_target != null)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("Seguro que deseas eliminar el target \"" + selected_target.nombre + "\"?", "Hitch Us - Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    eliminarTarget(updateTargetFromForm());
                    MessageBox.Show("Se ha eliminiado la campaña: " + selected_target.nombre, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);                    
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
                    selected_target = getTargetFromForm();
                    ingresarTarget(selected_target);
                    MessageBox.Show("Target agregado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
                }
                else
                {
                    MessageBox.Show("Uno o más campos vacíos", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (btnModificar.IsEnabled)
            {
                if (selected_target != null)
                {
                    selected_target = updateTargetFromForm();
                    modificarTarget(selected_target);
                    MessageBox.Show("Target modificado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeData();
                }
            }
        }



        #endregion

        #region Servicios REST
        ///<summary>
        /// Traer todas las empresas
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
        /// <summary>
        /// Trae todos los targets
        /// </summary>
        public void retrieveTargets()
        {
            targetsJson.Clear();
            var urlUsers = this.FindResource("retrieveTargetEdad").ToString(); ;
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(urlUsers);
            targetsJson = JsonConvert.DeserializeObject<List<JsonMapping.TargetJson>>(content);
            targetDataGrid.ItemsSource = targetsJson;
        }
        /// <summary>
        /// Registra un nuevo target
        /// </summary>
        /// <param name="target"></param>
        public void ingresarTarget(TargetJson target)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("registrarTargetEdad").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(TargetJson));
                String data2 = JsonConvert.SerializeObject(target, Formatting.None, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ" });
                JSrz.WriteObject(MS, target);
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
        /// <summary>
        /// Modifica un target existente
        /// </summary>
        /// <param name="target"></param>
        public void modificarTarget(TargetJson target)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("editarTargetEdad").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(TargetJson));
                JSrz.WriteObject(MS, target);
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
        /// Elimina un target existente
        /// </summary>
        /// <param name="target"></param>
        public void eliminarTarget(TargetJson target)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("eliminarTargetEdad").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(TargetJson));
                JSrz.WriteObject(MS, target);
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
