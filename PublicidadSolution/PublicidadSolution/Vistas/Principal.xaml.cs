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
using System.Data;
using System.Net;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IO;
using JsonMapping;

namespace PublicidadSolution.Vistas
{
    /// <summary>
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        #region Atributos y Constructor

        /// <summary>
        /// Lista de usuarios 
        /// </summary>
        private List<Usuario> UsuariosJson = new List<Usuario>();

        /// <summary>
        /// Usuario seleccionado
        /// </summary>
        private Usuario user_selected = new Usuario();


        public Principal()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            leerUsuarios();
            userDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            userDataGrid.Columns[2].Visibility = Visibility.Collapsed;
            emptyFields();
            initialStateButtonActions();
            enableReadOnly(true);
            userDataGrid.IsEnabled = true;
        }

        #endregion

        #region UI Validaciones
        private void userDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTextFieldsWithSelectedUser();
        }

        public void emptyFields()
        {
            txtNombres.Text = "";
            txtContrasenia.Password = "";
            txtUsuario.Text = "";
            btnModificar.IsEnabled = false;
            userDataGrid.UnselectAllCells();
            user_selected = null;
        }

        private void initialStateButtonActions()
        {
            btnAgregar.IsEnabled = true;
            btnCancelar.IsEnabled = false;
            btnConfirmar.IsEnabled = false;
            btnModificar.IsEnabled = false;
        }

        private void enableReadOnly(Boolean value)
        {
            txtUsuario.IsReadOnly = value;
            txtNombres.IsReadOnly = value;
            txtContrasenia.IsEnabled = !value;
        }

        private void changeButtonActions(Boolean value)
        {
            btnAgregar.IsEnabled = value;
            btnCancelar.IsEnabled = value;
            btnConfirmar.IsEnabled = value;
            btnModificar.IsEnabled = value;
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
            int indexSelected = 0;
            indexSelected = userDataGrid.SelectedIndex;
            if (indexSelected != -1)
            {
                user_selected = (Usuario)userDataGrid.SelectedItem;
                txtUsuario.Text = user_selected.username;
                txtNombres.Text = user_selected.nombres;
                txtContrasenia.Password = user_selected.password;
                enableActions();
            }
        }

        private void updateUserFromTextFields()
        {
            user_selected.username = txtUsuario.Text;
            user_selected.nombres = txtNombres.Text;
            user_selected.password = txtContrasenia.Password;
        }

        private void enableActions()
        {
            if (this.FindResource("usuario").Equals(user_selected.username))
            {
                btnModificar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
            }
        }

        public Boolean areEmptyFields()
        {
            return (txtNombres.Text == "" || txtUsuario.Text == "" || txtContrasenia.Password == "");
        }

        /// <summary>
        /// Retorna el usuario con los datos del formulario
        /// </summary>
        /// <returns></returns>
        private Usuario userForm()
        {
            Usuario aux = new Usuario();
            aux.nombres = txtNombres.Text;
            aux.password = EncryptUtil.MD5HashMethod(txtContrasenia.Password);
            aux.username = txtUsuario.Text;
            return aux;
        }

        /// <summary>
        /// Verifica si existe el usuario
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Boolean existsUser(String username)
        {
            /*for (int i = 0; i < UsuariosJson.Count; i++)
            {
                if (UsuariosJson[i].username.Equals(username))
                {
                    return true;
                }
            }
            return false;*/
            var findUsuario = UsuariosJson.Find(a => a.username.Equals(username));
            if (findUsuario != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Verifica si existio cambio para actualizar y en caso de ser la password decide si encripta o no
        /// </summary>
        /// <returns></returns>
        public Boolean verifyChanges()
        {
            if (txtNombres.Text != user_selected.nombres || txtUsuario.Text != user_selected.username || txtContrasenia.Password != user_selected.password)
            {
                Boolean aux = txtContrasenia.Password != user_selected.password;
                updateUserFromTextFields();
                if (aux)
                {
                    user_selected.password = EncryptUtil.MD5HashMethod(txtContrasenia.Password);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region CRUD Usuarios evento Button
        /// <summary>
        /// Agrega un nuevo usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            emptyFields();
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            userDataGrid.IsEnabled = false;

        }

        /// <summary>
        /// Modifica el usuario existente solo si es quien ingreso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {            
            enableReadOnly(false);
            enableAproved(true, (Button)sender);
            userDataGrid.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            InitializeData();

        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (btnAgregar.IsEnabled)
            {
                if (!areEmptyFields())
                {
                    user_selected = userForm();
                    if (existsUser(user_selected.username))
                    {
                        MessageBox.Show("El nombre de Usuario ya existe", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        ingresarUsuario(user_selected);
                        MessageBox.Show("Usuario agregado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if (!this.FindResource("usuario").ToString().Equals(userForm().username))
                {
                    if (existsUser(userForm().username))
                    {
                        MessageBox.Show("El nombre de usuario ya existe", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (verifyChanges())
                        {
                            modificarUsuario(user_selected);
                            MessageBox.Show("Usuario actualizado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                            Application.Current.Resources["usuario"] = user_selected.username;
                            InitializeData();
                        }
                    }
                }
                else {
                    if (verifyChanges())
                    {
                        modificarUsuario(user_selected);
                        MessageBox.Show("Usuario actualizado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                        Application.Current.Resources["usuario"] = user_selected.username;
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
        /// Método utilizado para cargar los usuarios de la base de datos
        /// </summary>
        public void leerUsuarios()
        {
            UsuariosJson.Clear();
            var syncClient = new WebClient();
            syncClient.Encoding = ASCIIEncoding.UTF8;
            var content = syncClient.DownloadString(this.FindResource("ws_retrieveUsers").ToString());
            UsuariosJson = JsonConvert.DeserializeObject<List<Usuario>>(content);
            userDataGrid.ItemsSource = UsuariosJson;
        }

        /// <summary>
        /// Método utilizado para el ingreso de Usuarios
        /// </summary>
        /// <param name="user"></param>
        public void ingresarUsuario(Usuario user)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("registrarUsuario").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(Usuario));
                JSrz.WriteObject(MS, user);
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
        /// Método utilizado para la actualización de Usuarios
        /// </summary>
        /// <param name="user"></param>
        public void modificarUsuario(Usuario user)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("editarUsuario").ToString());
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                MemoryStream MS = new MemoryStream();
                DataContractJsonSerializer JSrz = new DataContractJsonSerializer(typeof(Usuario));
                JSrz.WriteObject(MS, user);
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
