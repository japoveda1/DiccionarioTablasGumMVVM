using Caliburn.Micro;
using DiccionarioTablasGUM.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiccionarioTablasGUM.ViewModels
{
    public class TablaVirtualViewModel:Screen
    {
        private BindableCollection<string> _prvListTablasGum;

        private string _prvListTablasGumSeleccionada;
        private string _prvNombreTablaVirtual;

        public string PubListTablasGumSeleccionada
        {
            get { return _prvListTablasGumSeleccionada; }
            set { _prvListTablasGumSeleccionada = value;
                NotifyOfPropertyChange(() => PubListTablasGumSeleccionada);
            }
        }

        public BindableCollection<string> PubListTablasGum
        {
            get { return _prvListTablasGum; }
            set
            {
                _prvListTablasGum = value;
                NotifyOfPropertyChange(() => PubListTablasGum);
            }
        }

        public string PubNombreTablaVirtual
        {
            get { return _prvNombreTablaVirtual; }
            set
            {
                _prvNombreTablaVirtual = value;
                NotifyOfPropertyChange(() => PubNombreTablaVirtual);
            }
        }

        public TablaVirtualViewModel(List<string> pvListNombreTablas)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            PubListTablasGum = new BindableCollection<string>(pvListNombreTablas);
            Mouse.OverrideCursor = null;
        }

        public void ConfirmarCambios() {

                clsConexion vObjConexionDB = new clsConexion();
                //Objeto con parametros lista de parametros
                List<clsConexion.ParametrosSP> vListParametrosSP;


                if ( String.IsNullOrEmpty( PubListTablasGumSeleccionada.Trim()) || String.IsNullOrEmpty(PubNombreTablaVirtual.Trim())|| (PubListTablasGumSeleccionada.Trim().Equals(PubNombreTablaVirtual.Trim())))
                {
                    System.Windows.MessageBox.Show("Por favor valide los datos ingresados.Para agregar una tabla virtual debe seleciccionar una tabla de origen y asignar un nuevo nombre.", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);

                    return;
                }

                vObjConexionDB.AbrirConexion();

 
                    vListParametrosSP = new List<clsConexion.ParametrosSP>();

                    vListParametrosSP.Add(new clsConexion.ParametrosSP
                    {
                        nombreParametro = "p_nombre_tabla_origen",
                        valorParametro = PubListTablasGumSeleccionada,
                        tipoParametro = System.Data.SqlDbType.VarChar
                    });

                    vListParametrosSP.Add(new clsConexion.ParametrosSP
                    {
                        nombreParametro = "p_nombre_tabla_virtual",
                        valorParametro = PubNombreTablaVirtual,
                        tipoParametro = System.Data.SqlDbType.VarChar
                    });

                    var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_gum_dd_crear_tabla_virtual", vListParametrosSP);


                    System.Windows.MessageBox.Show("Creacion de tabla virtual exitosa.", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);



        }
    }
}
