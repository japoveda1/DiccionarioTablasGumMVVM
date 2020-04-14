using Caliburn.Micro;
using DiccionarioTablasGUM.Conexion;
using DiccionarioTablasGUM.Models;
using DiccionarioTablasGUM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiccionarioTablasGUM.ViewModels
{
    public class DiccionarioTablasGUMViewModel:Screen
    {

        //VARIABLE PRIVADAS

        //Objeto para gestionar las ventanas hijas 
        private IWindowManager prvObjManager = new WindowManager();
        
        //Objeto privado lista que contiene las tablas que  ya se encuentran agregadas al GUM
        private BindableCollection<clsTablasGUM> _prvListTablasGum; 
        
        //Objeto de la tabla seleccionada en la grilla principal
        private clsTablasGUM _prvObjTablaGumSeleccionada; 
        
        //Objeto campos de todas las tablas en el diccionario gum
        private List<clsCamposGUM> PrvListCamposGum;

        //objeto campos de las relaciones segun cada tabla 
        private List<clsRelacCamposGUM> PrvListRelacCamposGum;

        private List<clsObjetosExportar> PubListExportar = new List<clsObjetosExportar>();

        public List<clsCambiosCamposGUM> PrvListCambiosCamposGum;
        
        //VARIABLE PUBLICAS

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
        public void ObtenerTablasGUM(int pvIndActualizarDiccionario=0)
        {
            clsConexion vObjConexionDB = new clsConexion();
            List<clsTablasGUM> vListTablasGUM = new List<clsTablasGUM>();
            PrvListRelacCamposGum = new List<clsRelacCamposGUM>();
            PrvListCambiosCamposGum = new List<clsCambiosCamposGUM>();
            PrvListCamposGum = new List<clsCamposGUM>();
            clsTablasGUM vTablaGum;
            clsCamposGUM vCamposGum;
            clsRelacCamposGUM vObjRelacCamposGUM;
            clsCambiosCamposGUM vObjCambiosCamposGUM;
            DataSet vDsDiccionarioGUM;

            vObjConexionDB.AbrirConexion();
            
            if (pvIndActualizarDiccionario == 1) {

                if ( PubListTablasGum.Where(vTabla => vTabla.indCambio ==1 ).Any()) {

                    if (System.Windows.MessageBox.Show("Para realizar esta operacion es necesario salvar los datos.¿Desea salvarlos?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        ConfirmarCambios();
                    }
                    else {

                        return;
                    }
                }

                vDsDiccionarioGUM = vObjConexionDB.EjecutarCommand("sp_gum_dd_actualizar_todo");
            }
            else
            {
                vDsDiccionarioGUM = vObjConexionDB.EjecutarCommand("sp_gum_dd_leer_todo_tablas");
            }

            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrTablas in vDsDiccionarioGUM.Tables[0].Rows)
            {
                vTablaGum = new clsTablasGUM();

                vTablaGum.nombre = Convert.ToString(vDrTablas["f1_nombre"]);
                vTablaGum.descripcion = Convert.ToString(vDrTablas["f1_descripcion"]).Trim();
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
                vCamposGum = new clsCamposGUM();

                vCamposGum.nombreTabla = Convert.ToString(vDrCampos["f2_nombre_tabla"]).Trim();
                vCamposGum.nombre = Convert.ToString(vDrCampos["f2_nombre"]).Trim();
                vCamposGum.descripcion = Convert.ToString(vDrCampos["f2_descripcion"]).Trim();
                vCamposGum.notas = Convert.ToString(vDrCampos["f2_notas"]).Trim();
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
                vCamposGum.indEsNuevo = Convert.ToInt16(vDrCampos["f2_ind_es_nuevo"]); 
                vCamposGum.indCambio = 0;

                PrvListCamposGum.Add(vCamposGum);
                vCamposGum = null;
            }

            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrRelaciones in vDsDiccionarioGUM.Tables[2].Rows)
            {
                vObjRelacCamposGUM = new clsRelacCamposGUM();

                vObjRelacCamposGUM.nombreRelacion = Convert.ToString(vDrRelaciones["f_nombre_relacion"]).Trim();
                vObjRelacCamposGUM.nombreTabla = Convert.ToString(vDrRelaciones["f_nombre_tabla"]).Trim();
                vObjRelacCamposGUM.nombreTablaRef = Convert.ToString(vDrRelaciones["f_nombre_tabla_ref"]).Trim();
                vObjRelacCamposGUM.nombreCampo = Convert.ToString(vDrRelaciones["f_nombre_campo"]).Trim();
                vObjRelacCamposGUM.nombreCampoRef = Convert.ToString(vDrRelaciones["f_nombre_campo_ref"]).Trim();
                vObjRelacCamposGUM.indInner = Convert.ToInt16(vDrRelaciones["f_ind_inner"]);
                vObjRelacCamposGUM.indOrden = Convert.ToInt16(vDrRelaciones["f_ind_orden"]);

                PrvListRelacCamposGum.Add(vObjRelacCamposGUM);
                vObjRelacCamposGUM = null;
            }

            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrCambios in vDsDiccionarioGUM.Tables[3].Rows)
            {
                vObjCambiosCamposGUM = new clsCambiosCamposGUM();

                vObjCambiosCamposGUM.nombreTabla = Convert.ToString(vDrCambios["f5_nombre_tabla"]).Trim();
                vObjCambiosCamposGUM.campoModificados = Convert.ToString(vDrCambios["f5_campo_modificado"]).Trim();
                vObjCambiosCamposGUM.propiedad = Convert.ToString(vDrCambios["f5_propiedad"]).Trim();
                vObjCambiosCamposGUM.valorAnterior = Convert.ToString(vDrCambios["f5_valor_anterior"]).Trim();
                vObjCambiosCamposGUM.valorNuevo = Convert.ToString(vDrCambios["f5_valor_nuevo"]).Trim();

                PrvListCambiosCamposGum.Add(vObjCambiosCamposGUM);
                vObjCambiosCamposGUM = null;
            }

            PubListTablasGum = new BindableCollection<clsTablasGUM>(vListTablasGUM);

        }

        /// <summary>
        ///  req. 162259 jpa 24032020
        ///  Se guarda la informacion digitada para las tablas del agregadas en el diccionario de tablas GUM  
        /// </summary>
        public void ConfirmarCambios()
        {
            //Cursor en espera
            //Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            clsConexion vObjConexionDB = new clsConexion();
            //Objeto con parametros lista de parametros
            List<clsConexion.ParametrosSP> vListParametrosSP;
            List<clsTablasGUM> vListTablasGumModificados;

            //Solo los registros editados
            vListTablasGumModificados = PubListTablasGum.Where(vTablas => vTablas.indCambio == 1).ToList();

            vObjConexionDB.AbrirConexion();

            // se recorren las tablas editadas y se guardan sus datos
            foreach (clsTablasGUM vTablaGUM in vListTablasGumModificados)
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


            ObtenerTablasGUM();
            //Cursor en espera
            //Mouse.OverrideCursor = null;

            System.Windows.MessageBox.Show("Los cambios se guardaron correctamente", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);            

        }

        /// <summary>
        /// req. 162259 jpa 24032020
        /// Abre la ventana que permite vizualizar los campos relaciones y cambios en general relaciados a una tabla 
        /// </summary>
        public void AbrirVentanaCampos()
        {
            List<string> vListNombreTablasGUM;
            string vStrNombreTablaSeleccionda;

            if (PubListTablasGum.Where(vTablas => vTablas.indCambio == 1).Any()) {

                if (System.Windows.MessageBox.Show("Para realizar esta operacion es necesario salvar los datos.¿Desea salvarlos?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    vStrNombreTablaSeleccionda = PubObjTablaGumSeleccionada.nombre;

                    ConfirmarCambios();

                    PubObjTablaGumSeleccionada = PubListTablasGum.Where(vTabla => vTabla.nombre == vStrNombreTablaSeleccionda).First();

                }
                else {

                    return;
                }
            }

            vListNombreTablasGUM = PubListTablasGum.Select(vTabla => vTabla.nombre).ToList();

            CamposTablasGUMViewModel vObjCamposTablasGum = new CamposTablasGUMViewModel(PubListTablasGum.ToList(),PubObjTablaGumSeleccionada,   
                                                                                        PrvListCamposGum,PrvListRelacCamposGum,PrvListCambiosCamposGum);

            prvObjManager.ShowDialog(vObjCamposTablasGum, null, null);
        }

        /// <summary>
        /// req. 162259 jpa 03042020
        /// Marca el indicador de modificado en la tabla selleccionada
        /// </summary>
        public void MarcarCambio()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            //Actualizacion de campo seleccion en publica con tablas del sistema
            (from vTablaGUM in PubListTablasGum
             where vTablaGUM.nombre == PubObjTablaGumSeleccionada.nombre
             select vTablaGUM).ToList().ForEach(vTablaSistema => vTablaSistema.indCambio = 1);

            Mouse.OverrideCursor = null;
        }

        /// <summary>
        ///  req. 162259 jpa 03042020
        ///  Actualiza e insera toda la informacion en las tablas gum
        /// </summary>
        public void ActualizarTablasGum() {

            clsConexion vObjConexionDB = new clsConexion();
            List<clsTablasGUM> vListTablasGUM = new List<clsTablasGUM>();
            clsTablasGUM vTablaGum;

            clsCamposGUM vCamposGum;

            //List<clsRelacCamposGUM> vListRelacCamposGUM = new List<clsRelacCamposGUM>();
            clsRelacCamposGUM vObjRelacCamposGUM;

            List<clsCambiosCamposGUM> vListCambiosCamposGUM = new List<clsCambiosCamposGUM>();
            clsCambiosCamposGUM vObjCambiosCamposGUM;

            DataSet vDsDiccionarioGUM;

            vObjConexionDB.AbrirConexion();

            vDsDiccionarioGUM = vObjConexionDB.EjecutarCommand("sp_gum_dd_actualizar_todo");

            vObjConexionDB.CerrarConexion();

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
                vCamposGum = new clsCamposGUM();

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

                PrvListCamposGum.Add(vCamposGum);
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

                PrvListRelacCamposGum.Add(vObjRelacCamposGUM);
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
                vObjCambiosCamposGUM.valorNuevo = Convert.ToString(vDrCambios["f5_valor_nuevo"]);

                PrvListCambiosCamposGum.Add(vObjCambiosCamposGUM);
                vObjCambiosCamposGUM = null;
            }

            PubListTablasGum = new BindableCollection<clsTablasGUM>(vListTablasGUM);
            
        }

        public void Exportar()
        {
            List<clsConexion.ParametrosSP> vListParametrosSP;
            List<string> vListTablasseleccionadas = new List<string>();
            DataSet vDsTablasRelacionadas;
            DateTime vDtmFechaHora = new DateTime();
            string vStrPrefijoFecha;
            string vStrPrefijoHora;
            string vStrRuta;
            string vStrQuery;

            //creo la conexion a la base de datos
            clsConexion vObjConexionDB = new clsConexion();

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

            vListParametrosSP = new List<clsConexion.ParametrosSP>();

            vDsTablasRelacionadas = vObjConexionDB.EjecutarCommand("sp_gum_dd_exportar");

            vObjConexionDB.CerrarConexion();


            vDtmFechaHora = DateTime.Now;
            vStrPrefijoFecha = Convert.ToDateTime(vDtmFechaHora).ToString("yyddMM");
            vStrPrefijoHora = Convert.ToDateTime(vDtmFechaHora).ToString("HHmmss");


            vStrRuta = @" C:\Users\johan.poveda\Desktop\"+ vStrPrefijoFecha + "_"+ vStrPrefijoHora + "_diccionarioGum.pdc";

            for (int i = 0; i <= 11; i++)
            {
                foreach (DataRow vDR in vDsTablasRelacionadas.Tables[i].Rows)
                {
                    vStrQuery = Convert.ToString(vDR["f_query"]);

                    using (StreamWriter ArchivoExportar = File.AppendText(vStrRuta))         //se crea el archivo
                    {
                        ArchivoExportar.WriteLine(vStrQuery);

                        ArchivoExportar.Close();
                    }
                }
            }

            vListParametrosSP.Clear();            

            Mouse.OverrideCursor = null;


        }

    }
}
