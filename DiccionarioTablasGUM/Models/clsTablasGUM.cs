using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DiccionarioTablasGUM.Models
{
    public class clsTablasGUM : INotifyPropertyChanged
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string notas { get; set; }
        public Int16 indProcesoGum { get; set; }

        private Int16 _indCambio;

        public Int16 indCambio 
        {
            get { return _indCambio; }
            set { _indCambio = value;
                OnPropertyChanged("indCambio");
            }
        }

        public Int16 indCambioEnDB { get; set; }
        public Int16 IndEsNuevo { get; set; }
        public Int16 indCampoAgregado { get; set; }
        public Int16 indCampoModificado { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
