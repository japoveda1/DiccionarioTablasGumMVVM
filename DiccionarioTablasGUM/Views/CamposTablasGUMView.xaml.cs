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
using DiccionarioTablasGUM.Models;
using DiccionarioTablasGUM.ViewModels; 

namespace DiccionarioTablasGUM.Views
{
    /// <summary>
    /// Lógica de interacción para CamposTablasGUMView.xaml
    /// </summary>
    public partial class CamposTablasGUMView : Window
    {
        //Guarda la fila que fue editada
        private DataGridRow prvDgFilaEditada;
        //PErmite controlar el loop despues de hacer commitEdit
        private bool prvBoolControlLoop;


        public CamposTablasGUMView()
        {
            InitializeComponent();
        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Run_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;

            MessageBoxResult vObjRespuestaUsuario;

           

            if (vObjCamposTablasGUMViewModel.PubListCamposGUMActual.Where(vCampo => vCampo.indCambio == 1).Any())
            {
                vObjRespuestaUsuario = System.Windows.MessageBox.Show("Hay cambios sin salvar.¿Desea salvarlos?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNoCancel);

                if (vObjRespuestaUsuario == MessageBoxResult.None) {
                    return;
                }

                if (vObjRespuestaUsuario == MessageBoxResult.Yes)
                {
                    vObjCamposTablasGUMViewModel.ConfirmarCambios(false);
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

        private void BtnPrimero_Click(object sender, RoutedEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.Navegacion("primero");
        }

        private void BtnAnterior_Click(object sender, RoutedEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.Navegacion("anterior");

        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.Navegacion("siguiente");

        }

        private void BtnUltimo_Click(object sender, RoutedEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.Navegacion("ultimo");
        }

        private void DtCamposGUM_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.MarcarCambio();

           // PubIntindCambioEnTabla.Foreground = Brushes.Yellow;
            ////if (e.Column.Header.Equals("Descripción") || e.Column.Header.Equals("Notas") || e.Column.Header.Equals("Configurable en GUM") || e.Column.Header.Equals("Sincronizable en GUM") || e.Column.Header.Equals("Sugerible en GUM") || e.Column.Header.Equals("Orden de campo descripcion"))
            ////{
            ////    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            ////    ((clsCamposGUM)dtCamposGUM.Items.GetItemAt(e.Row.GetIndex())).indCambio = 1;

            ////    prvDgFilaEditada = e.Row;
            ////    Mouse.OverrideCursor = null;
            ////}

            //bool vBoolCommithRow;
            //bool vBoolCommitColumn;
            ////Solo las columnas que son editables
            //if (prvBoolControlLoop && (e.Column.Header.Equals("Descripción") || e.Column.Header.Equals("Notas") || e.Column.Header.Equals("Orden de campo PK") || e.Column.Header.Equals("Configurable en GUM") || e.Column.Header.Equals("Sincronizable en GUM") || e.Column.Header.Equals("Sugerible en GUM") || e.Column.Header.Equals("Orden de campo descripcion")))
            //{
            //    //Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            //    ((clsCamposGUM)dtCamposGUM.Items.GetItemAt(e.Row.GetIndex())).indCambio = 1;
            //    //((clsTablasGUM)PubListTablasGum.Items.GetItemAt(e.Row.GetIndex())).indProcesoGum = 1;
            //    prvDgFilaEditada = e.Row;

            //    prvBoolControlLoop = false;

            //    vBoolCommithRow = true;
            //    vBoolCommitColumn = true;

            //    vBoolCommithRow = dtCamposGUM.CommitEdit();
            //    vBoolCommitColumn = dtCamposGUM.CommitEdit();

            //    if (vBoolCommithRow && vBoolCommitColumn)
            //    {
            //        dtCamposGUM.Items.Refresh();
            //    }
            //    //prvDgFilaEditada = null;

            //    //Mouse.OverrideCursor = null;
            //}
            //prvBoolControlLoop = true;
        }

        //private void DtCamposGUM_CurrentCellChanged(object sender, EventArgs e)
        //{

        //    System.Windows.Controls.DataGrid vObjCeldaActual = (System.Windows.Controls.DataGrid)sender;
            
        //    if (prvDgFilaEditada != null)
        //    {
        //        //Solo para si sigue en la misma fila
        //        if (prvDgFilaEditada.Item == vObjCeldaActual.CurrentCell.Item)
        //        {
        //            #region Configuracion de focus

        //            dtCamposGUM.UpdateLayout();
        //            dtCamposGUM.ScrollIntoView(vObjCeldaActual.CurrentCell.Item, vObjCeldaActual.CurrentCell.Column);
        //            dtCamposGUM.Focus();
        //            dtCamposGUM.SelectedItem = vObjCeldaActual.CurrentItem;

        //            //Se valida que si haya seleccionada una celda
        //            if (dtCamposGUM.SelectedItem != null)
        //            {
        //                //se optiene el contenido de la celda seleccionada
        //                var cellcontent = dtCamposGUM.Columns[vObjCeldaActual.CurrentCell.Column.DisplayIndex].GetCellContent(dtCamposGUM.SelectedItem);

        //                //Se obtiene la celda
        //                var cell = cellcontent?.Parent as System.Windows.Controls.DataGridCell;

        //                if (cell != null)
        //                {
        //                    cell.Focus();

        //                    //Si la celda es solo de lectura no la configura editable.
        //                    if (!cell.IsReadOnly)
        //                    {
        //                        cell.IsEditing = true;
        //                    }

        //                    cellcontent.Focus();
        //                }
        //            }
        //            #endregion
        //        }
        //    }

        //}

        private void ConfirmarCambios_Click(object sender, RoutedEventArgs e)
        {

            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.ConfirmarCambios();

            dtCamposGUM.Items.Refresh();
            dtRelacGUM.Items.Refresh();


        }

        private void DtCamposGUM_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.MarcarCambio();

        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {

            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;

            MessageBoxResult vObjRespuestaUsuario;

            if (vObjCamposTablasGUMViewModel.PubListCamposGUMActual.Where(vCampo => vCampo.indCambio == 1).Any())
            {
                vObjRespuestaUsuario = System.Windows.MessageBox.Show("Hay cambios sin salvar.¿Desea salvarlos?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNoCancel);

                if (vObjRespuestaUsuario == MessageBoxResult.None)
                {
                    return;
                }

                if (vObjRespuestaUsuario == MessageBoxResult.Yes)
                {
                    vObjCamposTablasGUMViewModel.ConfirmarCambios(false);
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
    }
}
