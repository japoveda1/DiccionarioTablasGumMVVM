using Caliburn.Micro;
using DiccionarioTablasGUM.Conexion;
using DiccionarioTablasGUM.Models;
using DiccionarioTablasGUM.Views;
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
    public class DiccionarioTablasGUMViewModel:Screen
    {
        private IWindowManager prvObjManager = new WindowManager();
        private BindableCollection<TablasGUM> _pubListTablasGum;
               
        public BindableCollection<TablasGUM> PubListTablasGum
        {
            get { return _pubListTablasGum; }
            set { _pubListTablasGum = value;
                NotifyOfPropertyChange(() => PubListTablasGum);                
            }
        }

        public DiccionarioTablasGUMViewModel() {
  
            ObtenerTablasGUM();
           
        }

        private void ObtenerTablasGUM()
        {
            clsConexion vObjConexionDB = new clsConexion();
            List<TablasGUM> vListTablasGUM = new List<TablasGUM>();
            TablasGUM vTablaGum;
            DataSet vDsTablas;
            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            vObjConexionDB.AbrirConexion();

            vDsTablas = vObjConexionDB.EjecutarCommand("sp_tablas_gum");

            foreach (DataRow vDrTablas in vDsTablas.Tables[0].Rows)
            {
                vTablaGum = new TablasGUM();


                vTablaGum.nombre = Convert.ToString(vDrTablas["f_nombre"]);
                vTablaGum.descripcion = Convert.ToString(vDrTablas["f_descripcion"]);
                vTablaGum.notas = Convert.ToString(vDrTablas["f_notas"]);
                vTablaGum.indProcesoGum = Convert.ToInt16(vDrTablas["f_ind_proceso_gum"]);
                vTablaGum.indCambio = Convert.ToInt16(vDrTablas["f_ind_cambio"]);
                vTablaGum.indCambioEnDB = Convert.ToInt16(vDrTablas["f_ind_cambio_en_db"]);
                vTablaGum.IndEsNuevo = Convert.ToInt16(vDrTablas["f_ind_es_nuevo"]);

                vListTablasGUM.Add(vTablaGum);
                vTablaGum = null;
            }

            PubListTablasGum = new BindableCollection<TablasGUM>(vListTablasGUM);

            //Cursor en espera
            Mouse.OverrideCursor = null;
        }

        public void ConfirmarCambios()
        {
            clsConexion vObjConexionDB = new clsConexion();

            //Objeto con parametros lista de parametros
            List<clsConexion.ParametrosSP> vListParametrosSP;
            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            vObjConexionDB.AbrirConexion();

            foreach (TablasGUM vTablaGUM in PubListTablasGum)
            {
                vListParametrosSP = new List<clsConexion.ParametrosSP>();

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_tabla",
                    valorParametro = vTablaGUM.nombre,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_descripcion_tabla",
                    valorParametro = vTablaGUM.descripcion,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_notas_tabla",
                    valorParametro = vTablaGUM.notas,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_ind_proceso_gum",
                    valorParametro = vTablaGUM.indProcesoGum,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_editar_stablas_gum", vListParametrosSP);
                
            }
            vObjConexionDB.CerrarConexion();

            //Cursor en espera
            Mouse.OverrideCursor = null;

             System.Windows.MessageBox.Show("Los cambios se guardaron correctamente", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);            

        }

        public void abrirventana()
        {
            TablasSistemaViewModel vObjTablasSistemaViewModel = new TablasSistemaViewModel();

            prvObjManager.ShowDialog(vObjTablasSistemaViewModel, null, null);

            ObtenerTablasGUM();
        }



    }
}
