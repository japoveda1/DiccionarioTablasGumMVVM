using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ProcesandoView.xaml
    /// </summary>
    public partial class ProcesandoView : Window
    {

        public DiccionarioTablasGUMViewModel pubDiccionarioTablasGUMViewModel;
        public  int pubIndAccion; 

        public ProcesandoView()
        {
            InitializeComponent();

        }

        private async Task Exportar()
        {
            await Task.Run(() => pubDiccionarioTablasGUMViewModel.Exportar());
        }

        private async Task ActualizarTablasGum()
        {
            await Task.Run(() => pubDiccionarioTablasGUMViewModel.ObtenerTablasGUM(1));
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
        
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (pubIndAccion)
            {
                case 1:
                    await ActualizarTablasGum();
                    break;
                case 2:
                    await Exportar();
                    break;
                default:

                    break;
            }

            this.DialogResult = false;
        }
    }
}
