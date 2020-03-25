using Caliburn.Micro;
using DiccionarioTablasGUM.Conexion;
using DiccionarioTablasGUM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiccionarioTablasGUM.ViewModels
{
    public class TablasSistemaViewModel:Screen
    {
        private TablaSistema _prvObjTablaSistemaSeleccionada;
        //public BindableCollection<TablaSistema> PubListTablasSistema { get; set; }

        private BindableCollection<TablaSistema> _prvListTablasSistema;

        private int _prvIndConRelacion;

        public int PubIndConRelacion
        {
            get { 
                    return _prvIndConRelacion; 
                }
            set {
                    _prvIndConRelacion = value;
                    NotifyOfPropertyChange(() => PubIndConRelacion);
                }
        }


        public BindableCollection<TablaSistema> PubListTablasSistema
        {
            get { return _prvListTablasSistema; }
            set {
                _prvListTablasSistema = value;
                NotifyOfPropertyChange(() => PubListTablasSistema);
            }
        }


        public TablaSistema PubObjTablaSistemaSeleccionada
        {
            get { 
                return _prvObjTablaSistemaSeleccionada; 
                }
            set {
                _prvObjTablaSistemaSeleccionada = value;
                NotifyOfPropertyChange(()=>PubObjTablaSistemaSeleccionada);                
                }
        }
        
        public TablasSistemaViewModel()
        {
            PubIndConRelacion = 1;
            ObtenerTablasDelSistema();
        }

        public void ObtenerTablasDelSistema()
        {
            clsConexion  vObjConexionDB = new clsConexion();
            List<TablaSistema> vListTablasSistema = new List<TablaSistema>();
            TablaSistema vTablaSistema;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            vObjConexionDB.AbrirConexion();
            DataSet vDsTablasDB;

            vDsTablasDB = vObjConexionDB.EjecutarCommand("sp_dd_tablas_db");

            vObjConexionDB.CerrarConexion();


            foreach (DataRow vDrTablas in vDsTablasDB.Tables[0].Rows)
            {
                vTablaSistema = new TablaSistema();


                vTablaSistema.nombreTabla = Convert.ToString(vDrTablas["f_nombre_tabla"]);
                vTablaSistema.nombreRelacion = Convert.ToString(vDrTablas["f_nombre_relacion"]);
                vTablaSistema.seleccion = Convert.ToInt16(vDrTablas["f_seleccion"]);

                vListTablasSistema.Add(vTablaSistema);
                vTablaSistema = null;
            }

            PubListTablasSistema = new BindableCollection<TablaSistema>(vListTablasSistema);
            Mouse.OverrideCursor = null;

        }

        public void SeleccionarTablasRelacionadas(Int16 pvIndSeleccion) {

            //creo la conexion a la base de datos
            clsConexion vObjConexionDB = new clsConexion();
            DataSet vDsRelacionTablas;

            if (PubIndConRelacion == 0) {
                return;
            }


            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            vObjConexionDB.AbrirConexion();

            List<clsConexion.ParametrosSP> vListParametrosSP;

            vListParametrosSP = new List<clsConexion.ParametrosSP>();

            vListParametrosSP.Add(new clsConexion.ParametrosSP
            {
                nombreParametro = "p_nombre_tabla",
                valorParametro = PubObjTablaSistemaSeleccionada.nombreTabla,
                tipoParametro = System.Data.SqlDbType.VarChar
            });

            List<string> vListTablasRealacionadas = new List<string>();
                
            vDsRelacionTablas = vObjConexionDB.EjecutarCommand("sp_dd_rel_tabla_gum", vListParametrosSP);

            foreach (DataRow dtRowTabla in vDsRelacionTablas.Tables[0].Rows)
            {
                vListTablasRealacionadas.Add(dtRowTabla["f_nombre_tabla_ref"].ToString());
            }

             (from p in PubListTablasSistema
             join f in vListTablasRealacionadas on p.nombreTabla equals f
             select p).ToList().ForEach(x => x.seleccion = pvIndSeleccion);


            //foreach (DataRow dtRowTabla in vDsRelacionTablas.Tables[0].Rows)
            //{
            //    (from p in PubListTablasSistema
            //     where p.nombreTabla == dtRowTabla["f_nombre_tabla_ref"].ToString()
            //     select p).ToList().ForEach(x => x.seleccion = pvIndSeleccion);
            //}

            vObjConexionDB.CerrarConexion();

            //Cursor en espera
            Mouse.OverrideCursor = null;
        }

        public void AdicionarTablasGUM()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            //creo la conexion a la base de datos
            clsConexion vObjConexionDB = new clsConexion();

            //Objeto donde se van a almacenar los parametros para el sp
            List<clsConexion.ParametrosSP> vParamsUserRolls;

            List<TablaSistema> vListTablaSistemas = new List<TablaSistema>();

            vListTablaSistemas = PubListTablasSistema.Where(x => x.seleccion == 1).ToList(); 

            //abro conexxion
            vObjConexionDB.AbrirConexion();            

            foreach (TablaSistema vObjTabla in vListTablaSistemas)
            {
                vParamsUserRolls = new List<clsConexion.ParametrosSP>();

                vParamsUserRolls.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_tabla",
                    valorParametro = vObjTabla.nombreTabla,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_dd_insetar_tablas_gum", vParamsUserRolls);

            }

            vObjConexionDB.CerrarConexion();

            Mouse.OverrideCursor = null;

            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Las tablas del sistema se agregaron correctamente , ¿ Desea agregar mas tablas ?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)  //
            {
                ObtenerTablasDelSistema();
            }
            else
            {
                foreach (Window item in System.Windows.Application.Current.Windows)
                {
                    if (item.DataContext == this) item.Close();
                }
            }

            
        }


    }
}
