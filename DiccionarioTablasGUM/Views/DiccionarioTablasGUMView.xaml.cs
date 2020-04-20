using Caliburn.Micro;
using DiccionarioTablasGUM.Models;
using DiccionarioTablasGUM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
        //Ventana procesando
        //ProcesandoView ProcesandoView = new ProcesandoView();

        private bool prvboolPermiteRefrescar;

        /// <summary>
        /// req.162116 jpa 14042020
        /// Contructor
        /// </summary>
        public DiccionarioTablasGUMView()
        {
            InitializeComponent();
            //configuracion del tamaño de l a ventana segun el pantalla 
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            btnRestaurar.Visibility = Visibility.Visible;
            btnMaximizar.Visibility = Visibility.Collapsed;

        }

        /// <summary>
        ///  req.162116 jpa 14042020
        ///  Permite desplazar la ventana principal haciendo clic sostenido desde la barra superiroi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

        ///// <summary>
        ///// req.162116 jpa 14042020
        ///// Se ejecuta cada vez que se termina de editar una celda de la grilla principal
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void DtTablasGUM_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            vObjDiccionarioTablasGUM.MarcarCambio();
 
        }

        /// <summary>
        /// req.162116 jpa 14042020
        /// evento del boton actualizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
       

            ProcesandoView vProcesandoView = new ProcesandoView();
          
            vProcesandoView.pubDiccionarioTablasGUMViewModel = (DiccionarioTablasGUMViewModel)DataContext;

            vProcesandoView.pubIndAccion = 1;

            vProcesandoView.ShowDialog();

        }

        public void HabilitarControl(bool pvindHabilitar)
        {

            PubListTablasGum.IsEnabled = pvindHabilitar;
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
        private async Task ActualizarTablasGum()
        {
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            await Task.Run(() => vObjDiccionarioTablasGUM.ObtenerTablasGUM(1));
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
                    vObjDiccionarioTablasGUM.ConfirmarCambios(false);
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

        /// <summary>
        /// req.162116 jpa 14042020
        /// Restaura el tamaño de la ventana principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRestaurar_Click(object sender, RoutedEventArgs e)
        {
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
        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {

            ProcesandoView vProcesandoView = new ProcesandoView();

            vProcesandoView.pubDiccionarioTablasGUMViewModel = (DiccionarioTablasGUMViewModel)DataContext;

            vProcesandoView.pubIndAccion = 2;

            vProcesandoView.ShowDialog();


        }

        private void PubListTablasGum_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            vObjDiccionarioTablasGUM.MarcarCambio();
        }
    }

  


}
