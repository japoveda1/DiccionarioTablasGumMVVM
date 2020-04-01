using DiccionarioTablasGUM.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace DiccionarioTablasGUM.Views
{
    /// <summary>
    /// Lógica de interacción para TablasSistemaView.xaml
    /// </summary>
    public partial class TablasSistemaView : Window
    {
        public TablasSistemaView()
        {
            InitializeComponent();
        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Run_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        private void chkSeleccion_Click(object sender, RoutedEventArgs e)
        {

            
            TablasSistemaViewModel vObjTablasSistemaViewModel = (TablasSistemaViewModel)this.DataContext;
            CheckBox vChkSeleccion = (CheckBox)sender;
            Int16 vIntIndSeleccion;

            vIntIndSeleccion = 0;

            if (vChkSeleccion.IsChecked == true)
            {
                vIntIndSeleccion = 1;
            }

            vObjTablasSistemaViewModel.SeleccionarTablasRelacionadas(vIntIndSeleccion);
            dgTablasSistema.Items.Refresh();



        }

    }
}
