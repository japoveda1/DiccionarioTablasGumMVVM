using DiccionarioTablasGUM.Models;
using DiccionarioTablasGUM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DiccionarioTablasGUM.Views
{
    /// <summary>
    /// Lógica de interacción para DiccionarioTablasGUMView.xaml
    /// </summary>
    public partial class DiccionarioTablasGUMView : Window
    {
        ProcesandoView procesando;
        private DataGridRow vRow;
        private int prvIndPermiteDragMove;

        public DiccionarioTablasGUMView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            prvIndPermiteDragMove = 1;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (prvIndPermiteDragMove == 1) {

                this.DragMove();

            }
            prvIndPermiteDragMove = 1;
        }

        private void Run_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            this.Close();
        }

        private void btnTablaCampos_Click(object sender, RoutedEventArgs e)
        {
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            vObjDiccionarioTablasGUM.AbrirVentanaCampos();
        }

        private void DtTablasGUM_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.Equals("Descripción") || e.Column.Header.Equals("Notas") || e.Column.Header.Equals("Activo GUM"))
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                ((clsTablasGUM)dtTablasGUM.Items.GetItemAt(e.Row.GetIndex())).indCambio = 1;

                vRow = e.Row;
                Mouse.OverrideCursor = null;
            }
        }
 
        private async void ButtonAbrir_Click(object sender, RoutedEventArgs e)
        {
           
            procesando = new ProcesandoView();

            procesando.Show();

            await ActualizarTablasGum();

            procesando.Close();

        }

        private  void ButtonCerrar_Click(object sender, RoutedEventArgs e)
        {

            dtTablasGUM.Items.Refresh();

        }

        private async Task ActualizarTablasGum() {
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
             await Task.Run(() => vObjDiccionarioTablasGUM.ObtenerTablasGUM(1));
        }

        private void DtTablasGUM_CurrentCellChanged(object sender, EventArgs e)
        {
           
            if (vRow != null)

            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                dtTablasGUM.CommitEdit();

                dtTablasGUM.Items.Refresh();

                vRow = null;
                Mouse.OverrideCursor = null;
            }
           
        }

        private void minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
               
        private void maximizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult vObjRespuestaUsuario;
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;

            if (vObjDiccionarioTablasGUM.PubListTablasGum.Where(vCampo => vCampo.indCambio == 1).Any())
            {

                vObjRespuestaUsuario = System.Windows.MessageBox.Show("Hay cambios sin salvar.¿Desea salvarlos?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNoCancel);

                if (vObjRespuestaUsuario == MessageBoxResult.None)
                {
                    return;
                }

                if (vObjRespuestaUsuario == MessageBoxResult.Yes)
                {
                    vObjDiccionarioTablasGUM.ConfirmarCambios();
                }
                else
                {
                    if (vObjRespuestaUsuario == MessageBoxResult.Cancel) {
                        return;
                    }

                }
            }

            this.Close();
        }

        private void restaurar_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Normal;

        }


    }
}
