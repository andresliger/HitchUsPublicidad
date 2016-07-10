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
        private LoginDao objLoginDao = new LoginDao();

        public Principal()
        {
            InitializeComponent();
        }        

        private void mnuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            PublicidadSolution.PublicidadDataSet publicidadDataSet = ((PublicidadSolution.PublicidadDataSet)(this.FindResource("publicidadDataSet")));
            // Load data into the table USUARIO. You can modify this code as needed.
            PublicidadSolution.PublicidadDataSetTableAdapters.USUARIOTableAdapter publicidadDataSetUSUARIOTableAdapter = new PublicidadSolution.PublicidadDataSetTableAdapters.USUARIOTableAdapter();
            publicidadDataSetUSUARIOTableAdapter.Fill(publicidadDataSet.USUARIO);
            System.Windows.Data.CollectionViewSource uSUARIOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("uSUARIOViewSource")));
            uSUARIOViewSource.View.MoveCurrentToFirst();
        }

        private void userDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {    
                //updateSelectedUser();
                //objLoginDao.modificarUsuario(user_selected);
                MessageBox.Show("Finalizar de Editar");
            }
        }

        private void userDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectedUser();
        }

        private void updateSelectedUser()
        {
            indexSelected = userDataGrid.SelectedIndex;
            foreach (DataRowView drv in userDataGrid.SelectedItems)
            {
                MessageBox.Show("Actualizar Campos");
                //DataRow row = drv.Row;
                //user_selected.ID_USUARIO = Convert.ToInt32(row[0]);
                //user_selected.USERNAME = row[1].ToString();
                //user_selected.PASSWORD = row[2].ToString();
                //user_selected.NOMBRES = row[3].ToString();
            }
        }
    }
}
