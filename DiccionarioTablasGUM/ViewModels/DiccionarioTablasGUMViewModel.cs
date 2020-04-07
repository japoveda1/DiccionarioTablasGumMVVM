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
        private BindableCollection<clsTablasGUM> _prvListTablasGum;
        
        //Objeto de la tabla seleccionada en la grilla principal
        private clsTablasGUM _prvObjTablaGumSeleccionada;
        
        //Objeto campos de todas las tablas en el diccionario gum
        private BindableCollection<CamposGUM> _prvListCamposGum;
        
        //objeto campos de las relaciones segun cada tabla 
        private BindableCollection<clsRelacCamposGUM> _prvRelacCamposGum;

        //Objeto publico lista que contiene las tablas que  ya se encuentran agregadas al GUM
        public BindableCollection<clsTablasGUM> PubListTablasGum
        {
            get { return _prvListTablasGum; }
            set
            {
                _prvListTablasGum = value;
                //Actualiza los datos de la variable publica en el cuando son editados desde la vista
                NotifyOfPropertyChange(() => PubListTablasGum);
            }
        }

        //Objeto de la tabla seleccionada en la grill¡a prinicipal
        public clsTablasGUM PubObjTablaGumSeleccionada
        {
            get
            {
                return _prvObjTablaGumSeleccionada;
            }
            set
            {
                _prvObjTablaGumSeleccionada = value;
                NotifyOfPropertyChange(() => PubObjTablaGumSeleccionada);
            }
        }
        
        //Lista con todos los campos de todas las tablas en el gum 
        public BindableCollection<CamposGUM> PubListCamposGum
        {
            get
            {
                return _prvListCamposGum;
            }
            set
            {
                _prvListCamposGum = value;
                NotifyOfPropertyChange(() => PubListCamposGum);
            }
        }

        //objeto campos de las relaciones segun cada tabla 
        public BindableCollection<clsRelacCamposGUM> PubRelacCamposGum
        {
            get
            {
                return _prvRelacCamposGum;
            }
            set
            {
                _prvRelacCamposGum = value;
                NotifyOfPropertyChange(() => PubRelacCamposGum);
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
            List<clsTablasGUM> vListTablasGUM = new List<clsTablasGUM>();
            clsTablasGUM vTablaGum;
            DataSet vDsTablas;

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

            vDsTablas = vObjConexionDB.EjecutarCommand("sp_gum_dd_leer_todo_tablas");

            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrTablas in vDsTablas.Tables[0].Rows)
            {
                vTablaGum = new clsTablasGUM();


                vTablaGum.nombre = Convert.ToString(vDrTablas["f_nombre"]);
                vTablaGum.descripcion = Convert.ToString(vDrTablas["f_descripcion"]);
                vTablaGum.notas = Convert.ToString(vDrTablas["f_notas"]);
                vTablaGum.indProcesoGum = Convert.ToInt16(vDrTablas["f_ind_proceso_gum"]);
                vTablaGum.indCambio = 0;
                vTablaGum.indCambioEnDB = Convert.ToInt16(vDrTablas["f_ind_cambio_en_db"]);
                vTablaGum.IndEsNuevo = Convert.ToInt16(vDrTablas["f_ind_es_nuevo"]);
                vTablaGum.indCampoAgregado = Convert.ToInt16(vDrTablas["f_ind_campo_agregado"]); 
                vTablaGum.indCampoModificado = Convert.ToInt16(vDrTablas["f_ind_campo_modificado"]);

                vListTablasGUM.Add(vTablaGum);
                vTablaGum = null;
            }

            PubListTablasGum = new BindableCollection<clsTablasGUM>(vListTablasGUM);

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
            List<clsTablasGUM> vListTablasGum;


            vListTablasGum = PubListTablasGum.Where(vTablas => vTablas.indCambio == 1).ToList();
            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

            // se recorren las tablas editadas y se guardan sus datos
            foreach (clsTablasGUM vTablaGUM in vListTablasGum)
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

                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_gum_dd_actualizar_tabla", vListParametrosSP);
                
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

        /// <summary>
        /// req. 162259 jpa 24032020
        /// Abre la ventana que permite vizualizar los campos relaciones y cambios en general relaciados a una tabla 
        /// </summary>
        public void AbrirVentanaCampos()
        {
            List<string> vListNombreTablasGUM;

            vListNombreTablasGUM = PubListTablasGum.Select(vTabla => vTabla.nombre).ToList();

            CamposTablasGUMViewModel vObjCamposTablasGum = new CamposTablasGUMViewModel(PubListTablasGum.ToList(),PubObjTablaGumSeleccionada );

            prvObjManager.ShowDialog(vObjCamposTablasGum, null, null);

        }

        public void exportar() {

            TablasExportarViewModel vObj = new TablasExportarViewModel(PubListTablasGum.Select(x => x.nombre).ToList());

            prvObjManager.ShowDialog(vObj, null, null);

        }


        /// <summary>
        /// req. 162259 jpa 03042020
        /// Marca el indicador de modificado en la tabla selleccionada
        /// </summary>
        public void MarcarCambio()
        {
             //Actualizacion de campo seleccion en publica con tablas del sistema
            (from vTablaGUM in PubListTablasGum
             where vTablaGUM.nombre == PubObjTablaGumSeleccionada.nombre
             select vTablaGUM).ToList().ForEach(vTablaSistema => vTablaSistema.indCambio = 1);

            PubListTablasGum = PubListTablasGum;
        }

        /// <summary>
        ///  req. 162259 jpa 03042020
        ///  Actualiza e insera toda la informacion en las tablas gum
        /// </summary>
        public void ActualizarTablasGum() {

            clsConexion vObjConexionDB = new clsConexion();
            List<clsTablasGUM> vListTablasGUM = new List<clsTablasGUM>();
            clsTablasGUM vTablaGum;

            List<CamposGUM> vListCamposGUM = new List<CamposGUM>();
            CamposGUM vCamposGum;

            List<clsRelacCamposGUM> vListRelacCamposGUM = new List<clsRelacCamposGUM>();
            clsRelacCamposGUM vObjRelacCamposGUM;

            List<clsCambiosCamposGUM> vListCambiosCamposGUM = new List<clsCambiosCamposGUM>();
            clsCambiosCamposGUM vObjCambiosCamposGUM;

            DataSet vDsDiccionarioGUM;

            //Cursor en espera
            //Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

            vDsDiccionarioGUM = vObjConexionDB.EjecutarCommand("sp_gum_dd_actualizar_todo");

            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrTablas in vDsDiccionarioGUM.Tables[0].Rows)
            {
                vTablaGum = new clsTablasGUM();

                vTablaGum.nombre = Convert.ToString(vDrTablas["f1_nombre"]);
                vTablaGum.descripcion = Convert.ToString(vDrTablas["f1_descripcion"]);
                vTablaGum.notas = Convert.ToString(vDrTablas["f1_notas"]);
                vTablaGum.indProcesoGum = Convert.ToInt16(vDrTablas["f1_ind_proceso_gum"]);
                vTablaGum.indCambio = 0;
                vTablaGum.indCambioEnDB = Convert.ToInt16(vDrTablas["f1_ind_cambio_en_db"]);
                vTablaGum.IndEsNuevo = Convert.ToInt16(vDrTablas["f1_ind_es_nuevo"]);
                vTablaGum.indCampoAgregado = Convert.ToInt16(vDrTablas["f1_ind_campo_agregado"]);
                vTablaGum.indCampoModificado = Convert.ToInt16(vDrTablas["f1_ind_campo_modificado"]);

                vListTablasGUM.Add(vTablaGum);
                vTablaGum = null;
            }


            foreach (DataRow vDrCampos in vDsDiccionarioGUM.Tables[1].Rows)
            {
                vCamposGum = new CamposGUM();

                vCamposGum.nombreTabla = Convert.ToString(vDrCampos["f2_nombre_tabla"]);
                vCamposGum.nombre = Convert.ToString(vDrCampos["f2_nombre"]);
                vCamposGum.descripcion = Convert.ToString(vDrCampos["f2_descripcion"]);
                vCamposGum.notas = Convert.ToString(vDrCampos["f2_notas"]);
                vCamposGum.orden = Convert.ToInt16(vDrCampos["f2_orden"]);
                vCamposGum.ordenPk = Convert.ToInt16(vDrCampos["f2_orden_pk"]);
                vCamposGum.indIdentity = Convert.ToInt16(vDrCampos["f2_ind_identity"]);
                vCamposGum.longitud = Convert.ToInt16(vDrCampos["f2_longitud"]);
                vCamposGum.presicion = Convert.ToInt16(vDrCampos["f2_precision"]);
                vCamposGum.ordenIdentificador = Convert.ToInt16(vDrCampos["f2_orden_identificador"]);
                vCamposGum.ordenCampoDesc = Convert.ToInt16(vDrCampos["f2_orden_campo_desc"]);
                vCamposGum.tipoDatoSql = Convert.ToString(vDrCampos["f2_tipo_dato_sql"]);
                vCamposGum.indNulo = Convert.ToInt16(vDrCampos["f2_ind_nulo"]);
                vCamposGum.indGumConfigurable = Convert.ToInt16(vDrCampos["f2_ind_gum_configurable"]);
                vCamposGum.indGumSincronizado = Convert.ToInt16(vDrCampos["f2_ind_gum_sincronizado"]);
                vCamposGum.indGumSugerir = Convert.ToInt16(vDrCampos["f2_ind_gum_sugerir"]);
                vCamposGum.indGumSugerir = Convert.ToInt16(vDrCampos["f2_ind_gum_sugerir"]);
                vCamposGum.indCambioEnDb = Convert.ToInt16(vDrCampos["f2_ind_cambio_en_db"]);

                vListCamposGUM.Add(vCamposGum);
                vCamposGum = null;
            }


            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrRelaciones in vDsDiccionarioGUM.Tables[2].Rows)
            {
                vObjRelacCamposGUM = new clsRelacCamposGUM();
                                
                vObjRelacCamposGUM.nombreRelacion = Convert.ToString(vDrRelaciones["f_nombre_relacion"]);
                vObjRelacCamposGUM.nombreTabla = Convert.ToString(vDrRelaciones["f_nombre_tabla"]);
                vObjRelacCamposGUM.nombreTablaRef = Convert.ToString(vDrRelaciones["f_nombre_tabla_ref"]);
                vObjRelacCamposGUM.nombreCampo = Convert.ToString(vDrRelaciones["f_nombre_campo"]);
                vObjRelacCamposGUM.nombreCampoRef = Convert.ToString(vDrRelaciones["f_nombre_campo_ref"]);
                vObjRelacCamposGUM.indInner = Convert.ToInt16(vDrRelaciones["f_ind_inner"]);
                vObjRelacCamposGUM.indOrden = Convert.ToInt16(vDrRelaciones["f_ind_orden"]);

                vListRelacCamposGUM.Add(vObjRelacCamposGUM);
                vObjRelacCamposGUM = null;
            }

            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrCambios in vDsDiccionarioGUM.Tables[3].Rows)
            {
                vObjCambiosCamposGUM = new clsCambiosCamposGUM();

                vObjCambiosCamposGUM.nombreTabla = Convert.ToString(vDrCambios["f5_nombre_tabla"]);
                vObjCambiosCamposGUM.campoModificados = Convert.ToString(vDrCambios["f5_campo_modificado"]);
                vObjCambiosCamposGUM.propiedad = Convert.ToString(vDrCambios["f5_propiedad"]);
                vObjCambiosCamposGUM.valorAnterior = Convert.ToString(vDrCambios["f5_valor_anterior"]);
                vObjCambiosCamposGUM.valorNuevo = Convert.ToString(vDrCambios["5f_valor_nuevo"]);

                vListCambiosCamposGUM.Add(vObjCambiosCamposGUM);
                vObjCambiosCamposGUM = null;
            }

            PubListTablasGum = new BindableCollection<clsTablasGUM>(vListTablasGUM);
            PubListCamposGum = new BindableCollection<CamposGUM>(vListCamposGUM);
            PubRelacCamposGum = new BindableCollection<clsRelacCamposGUM>(vListRelacCamposGUM);
            
        }

     }
}
