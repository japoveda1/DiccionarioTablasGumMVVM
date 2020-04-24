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
        public event PropertyChangedEventHandler PropertyChanged;

        private string _notas;
        private Int16 _indCambio;
        private string _descripcion;

        public string nombre { get; set; }
        public Int16 indProcesoGum { get; set; }
        public Int16 indCambioEnDB { get; set; }
        public Int16 indEsNuevo { get; set; }
        public Int16 indCampoAgregado { get; set; }
        public Int16 indCampoModificado { get; set; }
        public Int16 indTablaVirtual { get; set; }

        public string notas
        {
            get { return _notas; }
            set {
                    _notas = value;
                    OnPropertyChanged("notas");
            }
        }
        
        public Int16 indCambio 
        {
            get { return _indCambio; }
            set {
                    _indCambio = value;
                    OnPropertyChanged("indCambio");
            }
        }

        public string descripcion
        {
            get { return _descripcion; }
            set
            {
                _descripcion = value;
                OnPropertyChanged("descripcion");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string pvNombreVariable = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pvNombreVariable));
        }
    }
}
