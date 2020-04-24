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
using DiccionarioTablasGUM.ViewModels;

namespace DiccionarioTablasGUM.Views
{
    /// <summary>
    /// Interaction logic for TablaVirtualView.xaml
    /// </summary>
    public partial class TablaVirtualView : Window
    {
        public TablaVirtualView()
        {
            InitializeComponent();

     
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {

            TablaVirtualViewModel vObjTablaVirtualViewModel = (TablaVirtualViewModel)this.DataContext;

            MessageBoxResult vObjRespuestaUsuario;

            if (!String.IsNullOrEmpty (vObjTablaVirtualViewModel.PubListTablasGumSeleccionada) && String.IsNullOrEmpty(vObjTablaVirtualViewModel.PubNombreTablaVirtual))
            {
                vObjRespuestaUsuario = System.Windows.MessageBox.Show("Hay cambios sin salvar.¿Desea salvarlos?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNoCancel);

                if (vObjRespuestaUsuario == MessageBoxResult.None)
                {
                    return;
                }

                if (vObjRespuestaUsuario == MessageBoxResult.Yes)
                {
                    vObjTablaVirtualViewModel.ConfirmarCambios();
                }
                else
                {
                    if (vObjRespuestaUsuario == MessageBoxResult.Cancel)
                    {
                        return;
                    }

                }
            }
            this.Close();
        }

        private void BtnConfirmarCambios_Click(object sender, RoutedEventArgs e)
        {
            try {
                TablaVirtualViewModel vObjTablaVirtualViewModel = (TablaVirtualViewModel)this.DataContext;
                vObjTablaVirtualViewModel.ConfirmarCambios();
                this.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }




        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TablaVirtualViewModel vObjTablaVirtualViewModel = (TablaVirtualViewModel)this.DataContext;

            cboTablasGUM.ItemsSource = vObjTablaVirtualViewModel.PubListTablasGum;
            cboTablasGUM.SelectedIndex = 0;
        }
    }
}
