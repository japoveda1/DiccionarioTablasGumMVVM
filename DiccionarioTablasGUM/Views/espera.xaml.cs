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

namespace DiccionarioTablasGUM.Views
{
    /// <summary>
    /// Interaction logic for espera.xaml
    /// </summary>
    public partial class espera : Window
    {
        int txt;
        public espera()
        {
            InitializeComponent();
            txt = 0;
            while (1 == 2) {


                txt++;
            }
        }
    }
}
