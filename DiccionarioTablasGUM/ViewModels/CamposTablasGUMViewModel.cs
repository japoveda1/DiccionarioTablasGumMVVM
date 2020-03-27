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

		private BindableCollection<CamposGUM> _prvListTablasGUMCampos;

        private BindableCollection<clsRelacCamposGUM> _prvListRelacCamposGUM;

        public BindableCollection<clsRelacCamposGUM> PubListRelacCamposGUM
        {
            get { return _prvListRelacCamposGUM; }
            set { _prvListRelacCamposGUM = value; }
        }



        public string PubStrNombreTablaGUM
		{
			get { return _prvStrNombreTablaGUM; }
			set { _prvStrNombreTablaGUM = value; }
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

        /// <summary>
        /// req. 162116 jpa 25032020 
        /// Obitiene las tablas que ya fueron ingresadas en el diccionario GUM  (t735_dd_tablas)
        /// </summary>
        public CamposTablasGUMViewModel(string pvStrNombreTablaGUM)
		{
			PubStrNombreTablaGUM = pvStrNombreTablaGUM;
            ObtenerCamposGUM(pvStrNombreTablaGUM);

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
            
            //creacion de objeto tablasGUM
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

                vListCamposGUM.Add(vCamposGum);
                vCamposGum = null;
            }


            //creacion de objeto tablasGUM
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

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

            // se recorren las tablas editadas y se guardan sus datos
            foreach (CamposGUM vCamposGUM in PubListTablasGUMCampos)
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


    }
}
