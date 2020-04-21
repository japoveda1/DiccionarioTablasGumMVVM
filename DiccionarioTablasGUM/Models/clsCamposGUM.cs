using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DiccionarioTablasGUM.Models
{
    public class clsCamposGUM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private Int16 _indCambio;
        private string _descripcion;
        private string _notas;
        private Int16 _ordenCampoDesc;

        public string nombreTabla { get; set; }
        public string nombre { get; set; }      
        public Int16 orden { get; set; }
        public Int16 ordenPk { get; set; }
        public Int16 indIdentity { get; set; }
        public Int16 longitud { get; set; }
        public Int16 presicion { get; set; }
        public Int16 ordenIdentificador { get; set; }
        public string tipoDatoSql { get; set; }
        public Int16 indNulo { get; set; }
        public Int16 indGumConfigurable { get; set; }
        public Int16 indGumSincronizado { get; set; }
        public Int16 indGumSugerir { get; set; }
        public Int16 indCambioEnDb { get; set; }
        public Int16 indEsNuevo { get; set; }       

        public Int16 ordenCampoDesc
        {
            get { return _ordenCampoDesc; }
            set {
                    _ordenCampoDesc = value;
                    OnPropertyChanged("ordenCampoDesc");
                }   
        }

        public string descripcion
        {
            get { return _descripcion; }
            set {
                    _descripcion = value;
                    OnPropertyChanged("descripcion");
                }
        }        

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
            set
            {
                _indCambio = value;
                OnPropertyChanged("indCambio");
            }
        }       

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
