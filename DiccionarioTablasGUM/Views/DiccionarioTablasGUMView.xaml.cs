using DiccionarioTablasGUM.Models;
using DiccionarioTablasGUM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        //Ventana procesando
        ProcesandoView ProcesandoView;
        //Guarda la fila que fue editada
        private DataGridRow prvDgFilaEditada;

        //PErmite controlar el loop despues de hacer commitEdit
        private bool prvBoolControlLoop;

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

            prvBoolControlLoop = true;
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

            
            marcarRefrescarGRilla(e.Row.GetIndex(), e.Column.Header.ToString(),e.Row);


            //bool vBoolCommithRow;
            //bool vBoolCommitColumn;
            ////Solo las columnas que son editables
            //if (prvBoolControlLoop && (e.Column.Header.Equals("Descripción") || e.Column.Header.Equals("Notas") || e.Column.Header.Equals("Permite GUM")))
            //{
            //    //Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            //    ((clsTablasGUM)PubListTablasGum.Items.GetItemAt(e.Row.GetIndex())).indCambio = 1;
            //    //((clsTablasGUM)PubListTablasGum.Items.GetItemAt(e.Row.GetIndex())).indProcesoGum = 1;
            //    prvDgFilaEditada = e.Row;

            //    prvBoolControlLoop = false;

            //    vBoolCommithRow = true;
            //    vBoolCommitColumn = true;

            //    vBoolCommithRow = PubListTablasGum.CommitEdit();
            //    vBoolCommitColumn = PubListTablasGum.CommitEdit();

            //    if (vBoolCommithRow && vBoolCommitColumn)
            //    {
            //        PubListTablasGum.Items.Refresh();
            //    }
            //    //prvDgFilaEditada = null;

            //    //Mouse.OverrideCursor = null;
            //}
            //prvBoolControlLoop = true;
        }


        public void marcarRefrescarGRilla(int index, string nombreColumna , DataGridRow row)
        {
            bool vBoolCommithRow;
            bool vBoolCommitColumn;
            //Solo las columnas que son editables
            if (prvBoolControlLoop && (nombreColumna.Equals("Descripción") || nombreColumna.Equals("Notas") || nombreColumna.Equals("Permite GUM")))
            {
                //Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                ((clsTablasGUM)PubListTablasGum.Items.GetItemAt(index)).indCambio = 1;
                //((clsTablasGUM)PubListTablasGum.Items.GetItemAt(e.Row.GetIndex())).indProcesoGum = 1;

                prvDgFilaEditada = row;

                prvBoolControlLoop = false;

                vBoolCommithRow = true;
                vBoolCommitColumn = true;

                vBoolCommithRow = PubListTablasGum.CommitEdit();
                vBoolCommitColumn = PubListTablasGum.CommitEdit();

                if (vBoolCommithRow && vBoolCommitColumn)
                {
                    PubListTablasGum.Items.Refresh();
                }
                //prvDgFilaEditada = null;

                //Mouse.OverrideCursor = null;
            }
            prvBoolControlLoop = true;


        }

        ///// <summary>
        /////  req.162116 jpa 14042020
        /////  Evento que se ejecuta cuando los datos de la celda actual cambia
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void DtTablasGUM_CurrentCellChanged(object sender, EventArgs e)
        {
            System.Windows.Controls.DataGrid vObjCeldaActual = (System.Windows.Controls.DataGrid)sender;

            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            if (prvDgFilaEditada != null)
            {
                //Solo para si sigue en la misma fila
                if (prvDgFilaEditada.Item == vObjCeldaActual.CurrentCell.Item)
                {
                    #region Configuracion de focus

                    PubListTablasGum.UpdateLayout();
                    PubListTablasGum.ScrollIntoView(vObjCeldaActual.CurrentCell.Item, vObjCeldaActual.CurrentCell.Column);
                    PubListTablasGum.Focus();
                    PubListTablasGum.SelectedItem = vObjCeldaActual.CurrentItem;

                    //Se valida que si haya seleccionada una celda
                    if (PubListTablasGum.SelectedItem != null)
                    {
                        //se optiene el contenido de la celda seleccionada
                        var cellcontent = PubListTablasGum.Columns[vObjCeldaActual.CurrentCell.Column.DisplayIndex].GetCellContent(PubListTablasGum.SelectedItem);

                        //Se obtiene la celda
                        var cell = cellcontent?.Parent as System.Windows.Controls.DataGridCell;

                        if (cell != null)
                        {
                            cell.Focus();

                            //Si la celda es solo de lectura no la configura editable.
                            if (!cell.IsReadOnly)
                            {
                                cell.IsEditing = true;
                            }

                            cellcontent.Focus();
                        }
                    }
                    #endregion
                }
            }


            Mouse.OverrideCursor = null;
        }


        /// <summary>
        /// req.162116 jpa 14042020
        /// evento del boton actualizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Actualizar_Click(object sender, RoutedEventArgs e)
        {

            ProcesandoView = new ProcesandoView();


            ProcesandoView.Show();

            HabilitarControl(false);

            await ActualizarTablasGum();

            ProcesandoView.Close();

            HabilitarControl(true);


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
                    vObjDiccionarioTablasGUM.ConfirmarCambios();
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
        private async void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            ProcesandoView = new ProcesandoView();

            ProcesandoView.Show();

            HabilitarControl(false);

            await Exportar();

            ProcesandoView.Close();

            HabilitarControl(true);

        }

        private async Task Exportar()
        {
            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            await Task.Run(() => vObjDiccionarioTablasGUM.Exportar());
        }

        private void ChkIndProcesoGum_Click(object sender, RoutedEventArgs e)
        {
            var a = sender;
            var b = e;
            var c = 1;

        }
    }
}
