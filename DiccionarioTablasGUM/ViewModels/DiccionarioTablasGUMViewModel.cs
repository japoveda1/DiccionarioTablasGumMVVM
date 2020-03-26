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
        //Objeto para gestionar las ventanas hijas 
        private IWindowManager prvObjManager = new WindowManager();

        //Objeto privado lista que contiene las tablas que  ya se encuentran agregadas al GUM
        private BindableCollection<TablasGUM> _prvListTablasGum;

        //Objeto publico lista que contiene las tablas que  ya se encuentran agregadas al GUM
        public BindableCollection<TablasGUM> PubListTablasGum
        {
            get { return _prvListTablasGum; }
            set {
                _prvListTablasGum = value;
                //Actualiza los datos de la variable publica en el cuando son editados desde la vista
                NotifyOfPropertyChange(() => PubListTablasGum);                
                }
        }

        private TablasGUM _prvObjTablaGumSeleccionada;

        public TablasGUM PubObjTablaGumSeleccionada
        {
            get { 
                    return _prvObjTablaGumSeleccionada; 
                }
            set { 
                    _prvObjTablaGumSeleccionada = value;
                    NotifyOfPropertyChange(() => PubObjTablaGumSeleccionada);
                }
        }

        /// <summary>
        /// req. 162259 jpa 24032020 
        /// Contructor
        /// </summary>
        public DiccionarioTablasGUMViewModel() {  
            ObtenerTablasGUM();           
        }
        /// <summary>
        /// req. 162259 jpa 24032020 
        /// Obitiene las tablas que ya fueron ingresadas en el diccionario GUM  (t735_dd_tablas)
        /// </summary>
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

            //creacion de objeto tablasGUM
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

            //Cursor default
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        ///  req. 162259 jpa 24032020
        ///  Se guarda la informacion digitada para las tablas del agregadas en el diccionario de tablas GUM  
        /// </summary>
        public void ConfirmarCambios()
        {
            clsConexion vObjConexionDB = new clsConexion();
            //Objeto con parametros lista de parametros
            List<clsConexion.ParametrosSP> vListParametrosSP;

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

            // se recorren las tablas editadas y se guardan sus datos
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

        /// <summary>
        /// req. 162259 jpa 24032020
        /// Abre la ventana donde se adjuntan las tablas de sistema al diccionario GUM
        /// </summary>
        public void AbrirVentanaTablasDelSistema()
        {
            TablasSistemaViewModel vObjTablasSistemaViewModel = new TablasSistemaViewModel();

            prvObjManager.ShowDialog(vObjTablasSistemaViewModel, null, null);

            ObtenerTablasGUM();
        }

        public void AbrirVentanaCampos()
        {
            CamposTablasGUMViewModel vObjCamposTablasGum = new CamposTablasGUMViewModel(PubObjTablaGumSeleccionada.nombre);

            prvObjManager.ShowDialog(vObjCamposTablasGum, null, null);

        }



    }
}
