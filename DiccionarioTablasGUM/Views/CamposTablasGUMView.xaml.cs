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
        private DataGridRow vRow;


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
            this.Close();
        }

        private void BtnPrimero_Click(object sender, RoutedEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.navegacion("primero");
        }

        private void BtnAnterior_Click(object sender, RoutedEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.navegacion("anterior");

        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.navegacion("siguiente");

        }

        private void BtnUltimo_Click(object sender, RoutedEventArgs e)
        {
            CamposTablasGUMViewModel vObjCamposTablasGUMViewModel = (CamposTablasGUMViewModel)this.DataContext;
            vObjCamposTablasGUMViewModel.navegacion("ultimo");
        }

        private void DtCamposGUM_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.Equals("Descripción") || e.Column.Header.Equals("Notas") || e.Column.Header.Equals("Activo GUM"))
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                ((clsCamposGUM)dtCamposGUM.Items.GetItemAt(e.Row.GetIndex())).indCambio = 1;

                vRow = e.Row;
                Mouse.OverrideCursor = null;
            }
        }

        private void DtCamposGUM_CurrentCellChanged(object sender, EventArgs e)
        {

            if (vRow != null)

            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                dtCamposGUM.CommitEdit();

                dtCamposGUM.Items.Refresh();

                vRow = null;
                Mouse.OverrideCursor = null;
            }

        }
    }
}
