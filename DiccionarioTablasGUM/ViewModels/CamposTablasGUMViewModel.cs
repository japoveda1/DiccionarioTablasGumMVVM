using Caliburn.Micro;
using DiccionarioTablasGUM.Conexion;
using DiccionarioTablasGUM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiccionarioTablasGUM.ViewModels
{
	class CamposTablasGUMViewModel : Screen
	{


		private string _prvStrNombreTablaGUM;
        private int? _prvIntTablaConCambios;
        private clsTablasGUM PrvObjTablaGumSeleccionada;

        //Objeto privado lista que contiene las tablas que  ya se encuentran agregadas al GUM
        private List<clsTablasGUM> PrvListTablasGum;

        private BindableCollection<CamposGUM> _prvListTablasGUMCampos;

        private BindableCollection<clsRelacCamposGUM> _prvListRelacCamposGUM;

        private BindableCollection<clsCambiosCamposGUM> _prvListCambiosCampoGUM;

        public CamposGUM _prvListTablasGUMCamposSeleccionada;

        public CamposGUM PubListTablasGUMCamposSeleccionada
        {
            get
            {
                return _prvListTablasGUMCamposSeleccionada;
            }
            set
            {
                _prvListTablasGUMCamposSeleccionada = value;
                NotifyOfPropertyChange(() => PubListTablasGUMCamposSeleccionada);
            }
        }

        public int? PubIntTablaConCambios
        {
            get { return _prvIntTablaConCambios; }
            set { _prvIntTablaConCambios = value;
                NotifyOfPropertyChange(() => PubIntTablaConCambios);
            }
        }        

        public BindableCollection<clsCambiosCamposGUM> PubListCambiosCampoGUM
        {
            get {
                    return _prvListCambiosCampoGUM;
                }
            set {
                    _prvListCambiosCampoGUM = value;
                    NotifyOfPropertyChange(() => PubListCambiosCampoGUM);
            }
        }

        public string PubStrNombreTablaGUM
		{
			get { return _prvStrNombreTablaGUM; }
			set { _prvStrNombreTablaGUM = value;
                NotifyOfPropertyChange(() => PubStrNombreTablaGUM);
            }
		}

        public BindableCollection<CamposGUM> PubListTablasGUMCampos
		{
			get { 
					return _prvListTablasGUMCampos; 
				}
			set { 
					_prvListTablasGUMCampos = value;
					NotifyOfPropertyChange(() => PubListTablasGUMCampos);
				}
		}

        public BindableCollection<clsRelacCamposGUM> PubListRelacCamposGUM
        {
            get { return _prvListRelacCamposGUM; }
            set { _prvListRelacCamposGUM = value; }
        }

        /// <summary>
        /// req. 162116 jpa 25032020 
        /// Obitiene las tablas que ya fueron ingresadas en el diccionario GUM  (t735_dd_tablas)
        ///// </summary>
        public CamposTablasGUMViewModel(List<clsTablasGUM> pvListTablasGUM ,clsTablasGUM pvObjTablaGUMSeleccionada)
		{
			PubStrNombreTablaGUM = pvObjTablaGUMSeleccionada.nombre;
            ObtenerCamposGUM(PubStrNombreTablaGUM);
            //PubListTablasGUMCampos = new BindableCollection<CamposGUM>(pvListTablasGUMCampos);
            //PubListRelacCamposGUM = new BindableCollection<clsRelacCamposGUM>(pvListRelacCamposGUM);

            //,
            //                            List<CamposGUM> pvListTablasGUMCampos, List< clsRelacCamposGUM > pvListRelacCamposGUM,
            //                            List<clsCambiosCamposGUM> pvListCambiosCampoGUM
            PubIntTablaConCambios = pvObjTablaGUMSeleccionada.indCambioEnDB;

            PrvListTablasGum = pvListTablasGUM;
            PrvObjTablaGumSeleccionada = pvObjTablaGUMSeleccionada;
            //_prvListNombreTablasGUM = pvListNombreTablasGUM;
           
        }


        /// <summary>
        /// req. 162259 jpa 25032020 
        /// Obitiene los campos que ya fueron ingresadas en el diccionario GUM  (t7351_dd_campos)
        /// </summary>
        private void ObtenerCamposGUM(string pvStrNombreTablasGUM)
        {
            clsConexion vObjConexionDB = new clsConexion();
            List<clsConexion.ParametrosSP> vListParametrosSP;
            List<CamposGUM> vListCamposGUM = new List<CamposGUM>();
            CamposGUM vCamposGum;
            List<clsRelacCamposGUM> vListRelacCamposGUM = new List<clsRelacCamposGUM>();
            clsRelacCamposGUM vObjRelacCamposGUM;
            DataSet vDsCampos;

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

            vListParametrosSP = new List<clsConexion.ParametrosSP>();

            vListParametrosSP.Add(new clsConexion.ParametrosSP
            {
                nombreParametro = "p_nombre_tabla",
                valorParametro = pvStrNombreTablasGUM,
                tipoParametro = System.Data.SqlDbType.VarChar
            });

            vDsCampos = vObjConexionDB.EjecutarCommand("sp_gum_dd_leer_campos_relac_x_tabla", vListParametrosSP);
            
            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrCampos in vDsCampos.Tables[0].Rows)
            {
                vCamposGum = new CamposGUM();

                vCamposGum.nombreTabla = Convert.ToString(vDrCampos["f_nombre_tabla"]); 
                vCamposGum.nombre = Convert.ToString(vDrCampos["f_nombre"]);
                vCamposGum.descripcion = Convert.ToString(vDrCampos["f_descripcion"]);
                vCamposGum.notas = Convert.ToString(vDrCampos["f_notas"]);
                vCamposGum.orden = Convert.ToInt16(vDrCampos["f_orden"]);
                vCamposGum.ordenPk = Convert.ToInt16(vDrCampos["f_orden_pk"]);
                vCamposGum.indIdentity = Convert.ToInt16(vDrCampos["f_ind_identity"]);
                vCamposGum.longitud = Convert.ToInt16(vDrCampos["f_longitud"]);
                vCamposGum.presicion = Convert.ToInt16(vDrCampos["f_presicion"]);
                vCamposGum.ordenIdentificador = Convert.ToInt16(vDrCampos["f_orden_identificador"]);
                vCamposGum.ordenCampoDesc = Convert.ToInt16(vDrCampos["f_orden_campo_desc"]);
                vCamposGum.tipoDatoSql = Convert.ToString(vDrCampos["f_tipo_dato_sql"]);
                vCamposGum.indNulo = Convert.ToInt16(vDrCampos["f_ind_nulo"]);
                vCamposGum.indGumConfigurable = Convert.ToInt16(vDrCampos["f_ind_gum_configurable"]);
                vCamposGum.indGumSincronizado = Convert.ToInt16(vDrCampos["f_ind_gum_sincronizado"]);
                vCamposGum.indGumSugerir = Convert.ToInt16(vDrCampos["f_ind_gum_sugerir"]);
                vCamposGum.indGumSugerir = Convert.ToInt16(vDrCampos["f_ind_gum_sugerir"]);
                vCamposGum.indCambioEnDb = Convert.ToInt16(vDrCampos["f_ind_cambio_en_db"]);

                vListCamposGUM.Add(vCamposGum);
                vCamposGum = null;
            }


            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrCampos in vDsCampos.Tables[1].Rows)
            {
                vObjRelacCamposGUM = new clsRelacCamposGUM();

                vObjRelacCamposGUM.nombreTablaRef = Convert.ToString(vDrCampos["f_nombre_tabla_ref"]);
                vObjRelacCamposGUM.nombreCampo = Convert.ToString(vDrCampos["f_nombre_campo"]);
                vObjRelacCamposGUM.nombreCampoRef = Convert.ToString(vDrCampos["f_nombre_campo_ref"]);
                vObjRelacCamposGUM.indInner = Convert.ToInt16(vDrCampos["f_ind_inner"]);
                vObjRelacCamposGUM.nombreRelacion = Convert.ToString(vDrCampos["f_nombre_relacion"]);
                vObjRelacCamposGUM.indOrden = Convert.ToInt16(vDrCampos["f_ind_orden"]);


                vListRelacCamposGUM.Add(vObjRelacCamposGUM);
                vObjRelacCamposGUM = null;
            }


            PubListTablasGUMCampos = new BindableCollection<CamposGUM>(vListCamposGUM);
            PubListRelacCamposGUM = new BindableCollection<clsRelacCamposGUM>(vListRelacCamposGUM);
           

            //Cursor default
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        ///  req. 162259 jpa 25032020 
        ///  Se guarda la informacion digitada para las campos agregadas en el diccionario de tablas GUM  
        /// </summary>
        public void ConfirmarCambios()
        {
            clsConexion vObjConexionDB = new clsConexion();
            //Objeto con parametros lista de parametros
            List<clsConexion.ParametrosSP> vListParametrosSP;
            List<CamposGUM> vListCamposGUM;

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

            vListCamposGUM = PubListTablasGUMCampos.Where(vTablaCampos => vTablaCampos.indCambio == 1).ToList();


            // se recorren las tablas editadas y se guardan sus datos
            foreach (CamposGUM vCamposGUM in vListCamposGUM)
            {
                vListParametrosSP = new List<clsConexion.ParametrosSP>();

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_campo",
                    valorParametro = vCamposGUM.nombre,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_descripcion_campo",
                    valorParametro = vCamposGUM.descripcion,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_notas_campo",
                    valorParametro = vCamposGUM.notas,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_orden_pk",
                    valorParametro = vCamposGUM.ordenPk,
                    tipoParametro = System.Data.SqlDbType.SmallInt
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_orden_identificador",
                    valorParametro = vCamposGUM.ordenIdentificador,
                    tipoParametro = System.Data.SqlDbType.SmallInt
                });
                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_orden_campo_desc",
                    valorParametro = vCamposGUM.ordenCampoDesc,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_ind_gum_configurable",
                    valorParametro = vCamposGUM.indGumConfigurable,
                    tipoParametro = System.Data.SqlDbType.SmallInt
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_ind_gum_sincronizado",
                    valorParametro = vCamposGUM.indGumSincronizado,
                    tipoParametro = System.Data.SqlDbType.SmallInt
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_ind_gum_sugerir",
                    valorParametro = vCamposGUM.indGumSugerir,
                    tipoParametro = System.Data.SqlDbType.SmallInt
                });


                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_gum_dd_actualizar_campo", vListParametrosSP);

            }

            vObjConexionDB.CerrarConexion();

            //Cursor en espera
            Mouse.OverrideCursor = null;

            System.Windows.MessageBox.Show("Los cambios se guardaron correctamente", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);

        }

        public void ObtenerCambiosEnDB() {

            clsConexion vObjConexionDB = new clsConexion();
            //Objeto con parametros lista de parametros
            List<clsConexion.ParametrosSP> vListParametrosSP;
            List<clsCambiosCamposGUM> vListCambiosCamposGUM = new List<clsCambiosCamposGUM>();
            clsCambiosCamposGUM vObjCambiosCamposGUM;
            DataSet vDsCampos;

            if (PubIntTablaConCambios == 0) {
                return;
            }
            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();


            vListParametrosSP = new List<clsConexion.ParametrosSP>();

            vListParametrosSP.Add(new clsConexion.ParametrosSP
            {
                nombreParametro = "p_nombre_tabla",
                valorParametro = PubStrNombreTablaGUM,
                tipoParametro = System.Data.SqlDbType.VarChar
            });

            vDsCampos = vObjConexionDB.EjecutarCommand("sp_gum_dd_leer_cambios_campos", vListParametrosSP);



            //creacion de objeto clsTablasGUM
            foreach (DataRow vDrCampos in vDsCampos.Tables[0].Rows)
            {
                vObjCambiosCamposGUM = new clsCambiosCamposGUM();

                vObjCambiosCamposGUM.campoModificados = Convert.ToString(vDrCampos["f_campo_modificado"]);
                vObjCambiosCamposGUM.valorAnterior = Convert.ToString(vDrCampos["f_valor_anterior"]);
                vObjCambiosCamposGUM.valorNuevo = Convert.ToString(vDrCampos["f_valor_nuevo"]);
                vObjCambiosCamposGUM.propiedad = Convert.ToString(vDrCampos["f_propiedad"]);


                vListCambiosCamposGUM.Add(vObjCambiosCamposGUM);
                vObjCambiosCamposGUM = null;
            }

            PubListCambiosCampoGUM = new BindableCollection<clsCambiosCamposGUM>(vListCambiosCamposGUM);

            //Cursor default
            Mouse.OverrideCursor = null;
        }

        //1:ultimo 2:anterior 3:siguiente 4:final
        public void navegacion(string pvStrAccion) {

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            int vIntIndexNuevaTablaSeleccionada;

            switch (pvStrAccion) {


                case "primero":

                    PrvObjTablaGumSeleccionada = PrvListTablasGum.First();


                    break;

                case "anterior":


                    vIntIndexNuevaTablaSeleccionada=  PrvListTablasGum.IndexOf(PrvObjTablaGumSeleccionada)-1;

                    PrvObjTablaGumSeleccionada = PrvListTablasGum[vIntIndexNuevaTablaSeleccionada];

                    break;

                case "siguiente":


                    vIntIndexNuevaTablaSeleccionada = PrvListTablasGum.IndexOf(PrvObjTablaGumSeleccionada) + 1;

                    PrvObjTablaGumSeleccionada = PrvListTablasGum[vIntIndexNuevaTablaSeleccionada];
                    break;

                case "ultimo":

                    PrvObjTablaGumSeleccionada = PrvListTablasGum.Last();
                    break;





            }

            PubIntTablaConCambios = PrvObjTablaGumSeleccionada.indCambioEnDB;
            PubStrNombreTablaGUM = PrvObjTablaGumSeleccionada.nombre;
            ObtenerCamposGUM(PubStrNombreTablaGUM);

            //Cursor en espera
            Mouse.OverrideCursor = null;
        }


    }
}
