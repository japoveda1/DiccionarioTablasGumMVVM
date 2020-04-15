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

        /// <summary>
        /// req.162116 jpa 14042020
        /// Contructor
        /// </summary>
        public DiccionarioTablasGUMView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            prvIndPermiteDragMove = 1;
            btnRestaurar.Visibility = Visibility.Visible;
            btnMaximizar.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        ///  req.162116 jpa 14042020
        ///  permite desplazar la ventana principal haciendo clic sostenido desde la barra superiroi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (prvIndPermiteDragMove == 1) {

                this.DragMove();

            }
            prvIndPermiteDragMove = 1;
        }

        /// <summary>
        /// req.162116 jpa 14042020
        /// Abre la ventana de campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTablaCampos_Click(object sender, RoutedEventArgs e)
        {
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            vObjDiccionarioTablasGUM.AbrirVentanaCampos();
        }

        /// <summary>
        /// req.162116 jpa 14042020
        /// Se ejecuta cada vez que se termina de editar una celda de la grilla principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtTablasGUM_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.Equals("Descripción") || e.Column.Header.Equals("Notas") || e.Column.Header.Equals("Permite GUM"))
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                ((clsTablasGUM)dtTablasGUM.Items.GetItemAt(e.Row.GetIndex())).indCambio = 1;

                vRow = e.Row;
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// req.162116 jpa 14042020
        /// evento del boton actualizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
           
            procesando = new ProcesandoView();

  
            procesando.Show();

            HabilitarControl(false);

            await ActualizarTablasGum();

            procesando.Close();

            HabilitarControl(true);


        }


        public void HabilitarControl(bool pvindHabilitar) {

            dtTablasGUM.IsEnabled = pvindHabilitar;
            btnActualizar.IsEnabled = pvindHabilitar;
            btnExportar.IsEnabled = pvindHabilitar;
            ConfirmarCambios.IsEnabled = pvindHabilitar;
            btnMinimizar.IsEnabled = pvindHabilitar;
            btnMaximizar.IsEnabled = pvindHabilitar;
            btnRestaurar.IsEnabled = pvindHabilitar;
            btnCerrar.IsEnabled = pvindHabilitar;
        }
        /// <summary>
        ///  req.162116 jpa 14042020
        ///  Evento asincronico que ejecuta la actualizacion
        /// </summary>
        /// <returns></returns>
        private async Task ActualizarTablasGum() {
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
             await Task.Run(() => vObjDiccionarioTablasGUM.ObtenerTablasGUM(1));
        }

        /// <summary>
        ///  req.162116 jpa 14042020
        ///  Evento que se ejecuta cuando los datos de la celda actual cambia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// req.162116 jpa 14042020
        /// Minimiza la ventana principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// req.162116 jpa 14042020
        /// Maximiza la ventana principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void BtnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            btnRestaurar.Visibility = Visibility.Visible;
            btnMaximizar.Visibility = Visibility.Collapsed;
       
        }

        /// <summary>
        /// req.162116 jpa 14042020
        /// Cierra la ventana principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// req.162116 jpa 14042020
        /// Restaura el tamaño de la ventana principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRestaurar_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Normal;
            btnRestaurar.Visibility = Visibility.Collapsed;
            btnMaximizar.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// req.162116 jpa 14042020
        /// Ejectua la exportacion  a oracle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            procesando = new ProcesandoView();

            procesando.Show();

            HabilitarControl(false);

            await Exportar();

            procesando.Close();

            HabilitarControl(true);

        }

        private async Task Exportar()
        {
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            await Task.Run(() => vObjDiccionarioTablasGUM.Exportar());
        }
    }
}
