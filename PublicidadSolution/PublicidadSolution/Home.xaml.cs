﻿using System;
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

namespace PublicidadSolution
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();            
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            Boolean success = false;
            if (txtUsuario.Text != "" && txtUsuario.Text != "")
            {
                success = LoginDao.Instance.validateLogin(txtUsuario.Text.Trim(), Controller.Utils.Encrypt.MD5HashMethod(txtPassword.Password.Trim()));
                if (success)
                {
                    String display = "Bienvenido ";
                    Application.Current.Resources["usuario"] = LoginDao.Instance.retrieveUserNameLogged(txtUsuario.Text.Trim(), txtPassword.Password.Trim());
                    display += this.FindResource("usuario");
                    MessageBox.Show(display, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Information);
                    Vistas.Principal principal = new Vistas.Principal();
                    principal.Show();
                    this.Close();
                }
                else
                {
                    String display = "Los datos de ingreso no son válidos.\nIntente De nuevo";
                    MessageBox.Show(display, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                clearFields();
            }
            else {
                String display = "Ingrese todos los campos para continuar";
                MessageBox.Show(display, "Hitch Us - Publicidad", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void clearFields()
        {
            txtUsuario.Text = "";
            txtPassword.Password = "";
        }
    }
}
