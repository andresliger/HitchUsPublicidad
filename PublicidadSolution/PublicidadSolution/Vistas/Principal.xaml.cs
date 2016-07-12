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
using Controller;

namespace PublicidadSolution.Vistas
{
    /// <summary>
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        private Modelo.USUARIO user_selected = new Modelo.USUARIO();
        private Int32 indexSelected = 0;

        #region
        /// <summary>
        /// Constructor and UI Control Validations
        /// </summary>  

        public Principal()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            userDataGrid.ItemsSource = LoginDao.Instance.retrieveAllUsers();
            userDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            userDataGrid.Columns[2].Visibility = Visibility.Collapsed;
            emptyFields();
        }

        public void emptyFields()
        {
            txtNombres.Text = "";
            txtContrasenia.Password = "";
            txtUsuario.Text = "";
            btnModificar.IsEnabled = false;
            btnDelete.IsEnabled = false;
            userDataGrid.UnselectAllCells();
            user_selected = null;
        }

        private void mnuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void userDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTextFieldsWithSelectedUser();
        }

        private void updateTextFieldsWithSelectedUser()
        {
            indexSelected = userDataGrid.SelectedIndex;
            if (indexSelected != -1)
            {
                user_selected = (Modelo.USUARIO)userDataGrid.SelectedItem;
                txtUsuario.Text = user_selected.USERNAME;
                txtNombres.Text = user_selected.NOMBRES;
                txtContrasenia.Password = user_selected.PASSWORD;
                enableActions();
            }
        }

        private void updateUserFromTextFields()
        {
            user_selected.USERNAME = txtUsuario.Text;
            user_selected.NOMBRES = txtNombres.Text;
            user_selected.PASSWORD = txtContrasenia.Password;
        }

        private void enableActions()
        {
            if (this.FindResource("usuario").Equals(user_selected.USERNAME))
            {
                btnModificar.IsEnabled = true;
                btnDelete.IsEnabled = false;
            }
            else
            {
                btnModificar.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }

        public Boolean areEmptyFields()
        {
            return (txtNombres.Text == "" || txtUsuario.Text == "" || txtContrasenia.Password == "");
        }

        private Modelo.USUARIO userForm()
        {
            Modelo.USUARIO aux = new Modelo.USUARIO();
            aux.NOMBRES = txtNombres.Text;
            aux.PASSWORD = Controller.Utils.Encrypt.MD5HashMethod(txtContrasenia.Password);
            aux.USERNAME = txtUsuario.Text;
            return aux;
        }

        public Boolean verifyChanges()
        {
            if (txtNombres.Text != user_selected.NOMBRES || txtUsuario.Text != user_selected.USERNAME || txtContrasenia.Password != user_selected.PASSWORD)
            {
                Boolean aux = txtContrasenia.Password != user_selected.PASSWORD;
                updateUserFromTextFields();
                if (aux)
                {
                    user_selected.PASSWORD = Controller.Utils.Encrypt.MD5HashMethod(txtContrasenia.Password);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void mnuEmpresas_Click(object sender, RoutedEventArgs e)
        {
            Empresa ventanaEmpresa = new Empresa();
            ventanaEmpresa.Show();
            this.Close();
        }

        #endregion

        #region
        /// <summary>
        /// Button actions for CRUD 
        /// </summary>

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

            if (LoginDao.Instance.existsUser(userForm().USERNAME))
            {
                MessageBox.Show("El nombre de usuario ya existe", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                emptyFields();
            }
            else
            {
                if (verifyChanges())
                {
                    LoginDao.Instance.modificarUsuario(user_selected);
                    MessageBox.Show("Usuario actualizado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Resources["usuario"] = user_selected.USERNAME;
                    emptyFields();
                    userDataGrid.ItemsSource = LoginDao.Instance.retrieveAllUsers();
                }
            }

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (!areEmptyFields())
            {
                if (LoginDao.Instance.existsUser(userForm().USERNAME))
                {
                    MessageBox.Show("El nombre de usuario ya existe", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    LoginDao.Instance.agregarUsuario(userForm());
                    MessageBox.Show("Usuario agregado exitosamente", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    emptyFields();
                    userDataGrid.ItemsSource = LoginDao.Instance.retrieveAllUsers();

                }
            }
            else
            {
                MessageBox.Show("Uno o más campos vacíos", "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            btnDelete.IsEnabled = false;
            btnModificar.IsEnabled = false;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!verifyChanges())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you really want to delete the product \"" + user_selected.USERNAME + "\"?", "Confirm product deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    LoginDao.Instance.eliminarUsuario(user_selected);
                    MessageBox.Show("Se ha eliminiado el usuario: " + user_selected.USERNAME, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    userDataGrid.ItemsSource = LoginDao.Instance.retrieveAllUsers();
                    emptyFields();
                }

            }
        }

        #endregion

        
    }
}
