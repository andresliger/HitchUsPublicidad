using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using Newtonsoft.Json;

namespace PublicidadSolution
{
    /// <summary>
    /// Interaction logic for Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        public Inicio()
        {
            InitializeComponent();
        }

        #region Interaccion UI
        /// <summary>
        /// Evento que permite el ingreso a la aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            Usuario loggedUser = new Usuario();
            if (txtUsuario.Text != "")
            {
                loggedUser = retrieveUserLogged(txtUsuario.Text.Trim(), JsonMapping.EncryptUtil.MD5HashMethod(txtPassword.Password.Trim()));
                if (loggedUser != null)
                {
                    String display = "Bienvenido ";
                    Application.Current.Resources["usuario"] = loggedUser.username;
                    display += this.FindResource("usuario");
                    MessageBox.Show(display, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    Vistas.Principal principal = new Vistas.Principal();
                    principal.Show();
                    this.Close();
                }
                else
                {
                    String display = "No se ha encontrado el usuario.\nIntente De nuevo";
                    MessageBox.Show(display, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                clearFields();
            }
            else
            {
                String display = "Ingrese todos los campos para continuar";
                MessageBox.Show(display, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// Método que permite restablecer campos del formulario
        /// </summary>
        private void clearFields()
        {
            txtUsuario.Text = "";
            txtPassword.Password = "";
        }
        #endregion

        #region Links Social Networks
        /// <summary>
        /// Evento que permite acceder al sitio web de HitchUs!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWebPanel_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://ec2-54-67-56-108.us-west-1.compute.amazonaws.com/HitchUs-web/login.xhtml");
        }
        /// <summary>
        /// Evento que permite acceder a Google Play y descargar la AppMovil HitchUs!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMovilPanel_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://play.google.com/store/apps?hl=es");
        }
        /// <summary>
        /// Evento que permite acceder al sitio oficial de Facebook de HitchUs!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFacebook_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://play.google.com/store/apps?hl=es");
        }
        /// <summary>
        /// Evento que permite acceder al sitio oficial de Twitter HitchUs!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTwitter_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://play.google.com/store/apps?hl=es");
        }
        /// <summary>
        /// Evento que permite acceder al sitio web de administración y reportes de HitchUs!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://play.google.com/store/apps?hl=es");
        }
        #endregion

        #region REST Services
        /// <summary>
        /// Busca el usuario y si existe lo devuelve
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns>Usuario</returns>
        public Usuario retrieveUserLogged(String user, String password)
        {
            Usuario auxUser = new Usuario();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.FindResource("ws_retrieveUserLogged").ToString());
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string postString = string.Format("usuario={0}&password={1}", user, password);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postString);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                if (result != null)
                    auxUser = JsonConvert.DeserializeObject<Usuario>(result);
                else
                    return null;
            }
            return auxUser;
        }

        #endregion
    }
}
