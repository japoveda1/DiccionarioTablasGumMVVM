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
        public DiccionarioTablasGUMView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
            //DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            ////dtTablasGUM.BeginEdit();
            //vObjDiccionarioTablasGUM.MarcarCambio();
            //dtTablasGUM.CommitEdit(DataGridEditingUnit.Cell, true);
            //dtTablasGUM.Items.Refresh();


            //var a = 5;
            //var vObj = (System.Windows.Controls.DataGrid)sender;

            //if (vObj.CurrentColumn.Header.Equals("Descripción"))
            //{

            //    var c = (clsTablasGUM)vObj.CurrentItem;


            //    c.indCambio = 1;

            //    dtTablasGUM.CommitEdit(DataGridEditingUnit.Cell, true);
            //    dtTablasGUM.Items.Refresh();

            //}


            //var vObj = (System.Windows.Controls.DataGrid)sender;
        }


        private void DtTablasGUM_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var a = 5;
            var vObj = (System.Windows.Controls.DataGrid)sender;


            var vE =e.Row;

            var f = (clsTablasGUM)vE.Item;

            f.indCambio = 1;

            dtTablasGUM.CommitEdit(DataGridEditingUnit.Row, true);

            //if (vObj.CurrentColumn.Header.Equals("Descripción"))
            //{

            //    var c = (clsTablasGUM)vObj.CurrentItem;

            //    //vObjDiccionarioTablasGUM.MarcarCambio();

            //    c.indCambio = 1;

            //    dtTablasGUM.CommitEdit(DataGridEditingUnit.Cell, true);
            //    //dtTablasGUM.Items.Refresh();

            //}
            //            DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            //dtTablasGUM.BeginEdit();

            //vObjDiccionarioTablasGUM.MarcarCambio();

            //dtTablasGUM.Items.Refresh();


            //var vItemCurrent = (clsTablasGUM)dtTablasGUM.CurrentItem;
            ////vItemCurrent.indCambio = 1;
            ////vItemCurrent.indCampoAgregado = 1;
            ////vItemCurrent.indCampoModificado = -1;
            ////vItemCurrent.IndEsNuevo = 1;
            ////vItemCurrent.indProcesoGum = 1;
            ////dtTablasGUM.CommitEdit();
            //var vObj = (System.Windows.Controls.DataGrid)sender;
            //var c = (clsTablasGUM)vObj.CurrentItem;

            //c.indCambio = 1;
            //c.indCampoAgregado = 1;
            //c.indCampoModificado = -1;
            //c.IndEsNuevo = 1;
            //c.indProcesoGum = 1;

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

            await Task.Run(()=> vObjDiccionarioTablasGUM.ActualizarTablasGum());

        }

        private void DtTablasGUM_CurrentCellChanged(object sender, EventArgs e)
        {


            //DiccionarioTablasGUMViewModel vObjDiccionarioTablasGUM = (DiccionarioTablasGUMViewModel)DataContext;
            ////dtTablasGUM.BeginEdit();
            //var a = 5;
            //var vObj = (System.Windows.Controls.DataGrid)sender;

            //if (vObj.CurrentColumn.Header.Equals("Descripción"))
            //{

            //    var c = (clsTablasGUM)vObj.CurrentItem;

            //    //vObjDiccionarioTablasGUM.MarcarCambio();

            //    c.indCambio = 1;

            //    dtTablasGUM.CommitEdit(DataGridEditingUnit.Cell, true);
                //dtTablasGUM.Items.Refresh();

            //}




        }



    }
}
