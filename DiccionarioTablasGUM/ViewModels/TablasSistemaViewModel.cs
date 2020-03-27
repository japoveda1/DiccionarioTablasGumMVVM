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
        //Se almacenan las tablas del sistema que no estan en el diccionario GUM    
        private BindableCollection<TablaSistema> _prvListTablasSistema;
        //Se almacena la tabla que esta siendo seleccionada en la grilla de la vista
        private TablaSistema _prvObjTablaSistemaSeleccionada;
        //Se almacena el valor segun el check "con relacion" en la vista
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
            get { 
                    return _prvListTablasSistema; 
                }
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

        /// <summary>
        /// req. 162259 jpa 24032020
        /// constructor
        /// </summary>
        public TablasSistemaViewModel()
        {
            PubIndConRelacion = 1;
            ObtenerTablasDelSistema();
        }

        /// <summary>
        /// req. 162259 jpa 24032020
        /// Se obtienen las tablas de la base de datos que no se encuentren el diccionario de tablas GUM
        /// </summary>
        public void ObtenerTablasDelSistema()
        {
            clsConexion  vObjConexionDB = new clsConexion();
            List<TablaSistema> vListTablasSistema = new List<TablaSistema>();
            TablaSistema vTablaSistema;
            DataSet vDsTablasDB;

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            
            vObjConexionDB.AbrirConexion();            

            vDsTablasDB = vObjConexionDB.EjecutarCommand("sp_gum_dd_leer_todo_tablas_db");

            vObjConexionDB.CerrarConexion();

            //Creacion de objeto tablas del sistema
            foreach (DataRow vDrTablas in vDsTablasDB.Tables[0].Rows)
            {
                vTablaSistema = new TablaSistema();

                vTablaSistema.nombreTabla = Convert.ToString(vDrTablas["f_nombre_tabla"]);
                vTablaSistema.seleccion = Convert.ToInt16(vDrTablas["f_seleccion"]);

                vListTablasSistema.Add(vTablaSistema);
                vTablaSistema = null;
            }

            PubListTablasSistema = new BindableCollection<TablaSistema>(vListTablasSistema);
            //Cursor por defecto
            Mouse.OverrideCursor = null;

        }

        /// <summary>
        /// req. 162259 jpa 24032020
        /// Se seleccionan las tablas que estan relacionadas a la seleccionda
        /// </summary>
        /// <param name="pvIndSeleccion"> nos indica que tipo de marcacion se le va hacera cada tabla , marcar o desmarcar</param>
        public void SeleccionarTablasRelacionadas(Int16 pvIndSeleccion) {
            
            List<string> vListTablasRealacionadas = new List<string>();
            DataSet vDsTablasRelacionadas;

            //creo la conexion a la base de datos
            clsConexion vObjConexionDB = new clsConexion();      

            //se valida que este habilitada la funcion "con relacion" con el checkbox en la vista
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
                
            vDsTablasRelacionadas = vObjConexionDB.EjecutarCommand("sp_gum_dd_leer_relac_x_tabla", vListParametrosSP);

            //Se pasa el dataset a un objeto para usuarlo en inner linq
            vListTablasRealacionadas  = (from vTablaRelacionada in vDsTablasRelacionadas.Tables[0].AsEnumerable() select vTablaRelacionada.Field<string>("f_nombre_tabla_ref") ).ToList();

            //Actualizacion de campo seleccion en publica con tablas del sistema
            (from vTablaSistema in PubListTablasSistema
             join vTablaRelacionada in vListTablasRealacionadas on vTablaSistema.nombreTabla equals vTablaRelacionada
             select vTablaSistema).ToList().ForEach(vTablaSistema => vTablaSistema.seleccion = pvIndSeleccion);


            vObjConexionDB.CerrarConexion();

            //Cursor en espera
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// req. 162259 jpa 24032020
        /// Adicciona las tablas seleccionadas
        /// </summary>
        public void AdicionarTablasGUM()
        {   
            //cursor 
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

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

                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_gum_dd_insetar_tablas_campos", vParamsUserRolls);

            }

            vObjConexionDB.CerrarConexion();

            Mouse.OverrideCursor = null;

            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Las tablas del sistema se agregaron correctamente.¿Desea agregar mas tablas?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNo);

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


        public void AdicionarTablasDiccionarioGUM() 
        {



            //cursor 
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;


            AdicionarTablasYCampos();

            AdicionarRelacionYCampos();

            Mouse.OverrideCursor = null;

            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Las tablas del sistema se agregaron correctamente.¿Desea agregar mas tablas?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNo);

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

        private void AdicionarTablasYCampos() 
        {
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

                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_gum_dd_insetar_tablas_campos", vParamsUserRolls);

            }

            vObjConexionDB.CerrarConexion();

        }

        private void AdicionarRelacionYCampos() 
        {

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

                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_gum_dd_insertar_relac_campos", vParamsUserRolls);

            }

            vObjConexionDB.CerrarConexion();

        }

    }
}
