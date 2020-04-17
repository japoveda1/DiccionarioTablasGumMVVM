using Caliburn.Micro;
using DiccionarioTablasGUM.Conexion;
using DiccionarioTablasGUM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiccionarioTablasGUM.ViewModels
{
	class CamposTablasGUMViewModel : Screen
	{


		private string _prvStrNombreTablaGUM;
        private int? _prvIntTablaConCambios;
        //pubIntindCambioEnTabla

        private int _prvIntindCambioEnTabla;

        public int PubIntindCambioEnTabla
        {
            get { return _prvIntindCambioEnTabla; }
            set { _prvIntindCambioEnTabla = value;
                NotifyOfPropertyChange(() => PubIntindCambioEnTabla);
            }
        }

        //Objeto privado lista que contiene las tablas que  ya se encuentran agregadas al GUM
        private List<clsTablasGUM> PrvListTablasGum;
        private clsTablasGUM PrvObjTablaGumSeleccionada;
        private clsCamposGUM _prvListTablasGUMCamposSeleccionada;

        private List<clsCamposGUM> PrvListCamposGUM;
        private List<clsRelacCamposGUM> PrvListRelacCamposGUM;
        private List<clsCambiosCamposGUM> PrvListCambiosCamposGum;

        private BindableCollection<clsCamposGUM> _prvListCamposGUMActual;
        private BindableCollection<clsRelacCamposGUM> _prvListRelacCamposGUMActual;
        private BindableCollection<clsCambiosCamposGUM> _prvListCambiosCampoGUMActual;

        private List<clsObjetosExportar> PubListExportar = new List<clsObjetosExportar>();

        public clsCamposGUM PubListTablasGUMCamposSeleccionada
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

        public string PubStrNombreTablaGUM
        {
            get { return _prvStrNombreTablaGUM; }
            set
            {
                _prvStrNombreTablaGUM = value;
                NotifyOfPropertyChange(() => PubStrNombreTablaGUM);
            }
        }

        public BindableCollection<clsCamposGUM> PubListCamposGUMActual
        {
            get { return _prvListCamposGUMActual; }
            set
            {
                _prvListCamposGUMActual = value;
                NotifyOfPropertyChange(() => PubListCamposGUMActual);
            }
        }

        public BindableCollection<clsRelacCamposGUM> PubListRelacCamposGUMActual
        {
            get { return _prvListRelacCamposGUMActual; }
            set
            {
                _prvListRelacCamposGUMActual = value;
                NotifyOfPropertyChange(() => PubListRelacCamposGUMActual);
            }
        }

        public BindableCollection<clsCambiosCamposGUM> PubListCambiosCampoGUMAtual
        {
            get {
                    return _prvListCambiosCampoGUMActual;
                }
            set {
                    _prvListCambiosCampoGUMActual = value;
                    NotifyOfPropertyChange(() => PubListCambiosCampoGUMAtual);
            }
        }           
        
        /// <summary>
        /// req. 162116 jpa 25032020 
        /// Obitiene las tablas que ya fueron ingresadas en el diccionario GUM  (t735_dd_tablas)
        ///// </summary>
        public CamposTablasGUMViewModel(List<clsTablasGUM> pvListTablasGUM ,clsTablasGUM pvObjTablaGUMSeleccionada,
                                        List<clsCamposGUM> pvListTablasGUMCampos, List<clsRelacCamposGUM> pvListRelacCamposGUM,
                                        List<clsCambiosCamposGUM> pvListCambiosCampoGUM)
		{

            PubStrNombreTablaGUM = pvObjTablaGUMSeleccionada.nombre;
            PubIntTablaConCambios = pvObjTablaGUMSeleccionada.indCambioEnDB;

            PrvListTablasGum = pvListTablasGUM;
            PrvObjTablaGumSeleccionada = pvObjTablaGUMSeleccionada;
            PrvListCamposGUM = pvListTablasGUMCampos;
            PubListCamposGUMActual = new  BindableCollection<clsCamposGUM>( PrvListCamposGUM.Where(vCampo => vCampo.nombreTabla.Equals(PubStrNombreTablaGUM)).OrderBy(vCampo => vCampo.orden).ToList());
            PrvListRelacCamposGUM = pvListRelacCamposGUM;
            PubListRelacCamposGUMActual = new BindableCollection<clsRelacCamposGUM>(PrvListRelacCamposGUM.Where(vRelac => vRelac.nombreTabla.Equals(PubStrNombreTablaGUM)).OrderBy(vCampo => vCampo.nombreRelacion).ToList());
            PrvListCambiosCamposGum = pvListCambiosCampoGUM;
            PubListCambiosCampoGUMAtual = new BindableCollection<clsCambiosCamposGUM>(PrvListCambiosCamposGum.Where(vCambios => vCambios.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());

            PubIntindCambioEnTabla = 0;
            if (PubListCamposGUMActual.Where(x => x.indCambio == 1).Any()) {

                PubIntindCambioEnTabla = 1;
            }
            else if(PubListCamposGUMActual.Where(x => x.indEsNuevo == 1).Any())
            {
                PubIntindCambioEnTabla = 3;

            }
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
            List<clsCamposGUM> vListCamposGUM;

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            EliminarRelacion();

            try {

              AgregarRelacionManual();

            } catch (Exception e) {
                Mouse.OverrideCursor =null;
                return;
            }         
               
            vObjConexionDB.AbrirConexion();

            vListCamposGUM = PubListCamposGUMActual.Where(vTablaCampos => vTablaCampos.indCambio == 1).ToList();

            // se recorren las tablas editadas y se guardan sus datos
            foreach (clsCamposGUM vCamposGUM in vListCamposGUM)
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

            PubListCamposGUMActual.Where(vTablaCampos => vTablaCampos.indCambio == 1).Apply(x => x.indCambio =0);

            PubListCamposGUMActual = PubListCamposGUMActual;

            
            //Cursor en espera
            Mouse.OverrideCursor = null;
            PubIntindCambioEnTabla = 0;
            System.Windows.MessageBox.Show("Los cambios se guardaron correctamente", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);

        }

        /// <summary>
        /// req. 162116 jpa 08042020 
        /// Permite la navegacion en la ventana de campos 
        /// </summary>
        /// <param name="pvStrAccion">Indica que boton ejecuto la accion </param>
        public void Navegacion(string pvStrAccion) {

            //Cursor en espera
            

            int vIntIndexNuevaTablaSeleccionada;

            if (PubListCamposGUMActual.Where(vCampos => vCampos.indCambio == 1).Any()) {

                if (System.Windows.MessageBox.Show("Para realizar esta operacion es necesario salvar los datos.¿Desea salvarlos?", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ConfirmarCambios();
                }
                else {
                    return;
                }
            }


            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            switch (pvStrAccion) {
                case "primero":
                    PrvObjTablaGumSeleccionada = PrvListTablasGum.First();

                    break;
                case "anterior":
                    //Validacion de primer registro
                    if (PrvListTablasGum.IndexOf(PrvObjTablaGumSeleccionada) == 0) {
                        break;
                    }

                    vIntIndexNuevaTablaSeleccionada=  PrvListTablasGum.IndexOf(PrvObjTablaGumSeleccionada)-1;
                    PrvObjTablaGumSeleccionada = PrvListTablasGum[vIntIndexNuevaTablaSeleccionada];

                    break;
                case "siguiente":

                    //Validacion de ultimo registro
                    if (PrvListTablasGum.IndexOf(PrvObjTablaGumSeleccionada) == PrvListTablasGum.Count()-1)
                    {
                        break;
                    }

                    vIntIndexNuevaTablaSeleccionada = PrvListTablasGum.IndexOf(PrvObjTablaGumSeleccionada) + 1;
                    PrvObjTablaGumSeleccionada = PrvListTablasGum[vIntIndexNuevaTablaSeleccionada];

                    break;
                case "ultimo":
                    PrvObjTablaGumSeleccionada = PrvListTablasGum.Last();

                    break;
            }

            PubIntTablaConCambios = PrvObjTablaGumSeleccionada.indCambioEnDB;
            PubStrNombreTablaGUM = PrvObjTablaGumSeleccionada.nombre;

            PubListCamposGUMActual = new BindableCollection<clsCamposGUM>(PrvListCamposGUM.Where(vCampo => vCampo.nombreTabla.Equals(PubStrNombreTablaGUM)).OrderBy(vCampo => vCampo.orden).ToList());
            PubListRelacCamposGUMActual = new BindableCollection<clsRelacCamposGUM>(PrvListRelacCamposGUM.Where(vRelac => vRelac.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());
            PubListCambiosCampoGUMAtual = new BindableCollection<clsCambiosCamposGUM>(PrvListCambiosCamposGum.Where(vCambios => vCambios.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());

            //Cursor en espera
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// req. 162116 jpa 14042020 
        /// </summary>
        public void AgregarRelacionManual()
        {
            clsConexion vObjConexionDB = new clsConexion();
            //Objeto con parametros lista de parametros
            List<clsConexion.ParametrosSP> vListParametrosSP;
            List<clsRelacCamposGUM> vListRelacionesAgregadas;

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            var a = PubListRelacCamposGUMActual;

            vObjConexionDB.AbrirConexion();

            vListRelacionesAgregadas = PubListRelacCamposGUMActual.Where(vTablaCampos => vTablaCampos.indCreadoEnGrilla == 1).ToList();

            if (vListRelacionesAgregadas.Count() == 0) {
                return;
            }

            // se recorren las tablas editadas y se guardan sus datos
            foreach (clsRelacCamposGUM vRelacion in vListRelacionesAgregadas)
            {
                vListParametrosSP = new List<clsConexion.ParametrosSP>();

                vRelacion.nombreTabla = PubStrNombreTablaGUM;

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_relacion",
                    valorParametro = vRelacion.nombreRelacion,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_tabla",
                    valorParametro = vRelacion.nombreTabla,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_campo",
                    valorParametro = vRelacion.nombreCampo,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_tabla_ref",
                    valorParametro = vRelacion.nombreTablaRef,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_campo_ref",
                    valorParametro = vRelacion.nombreCampoRef,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_ind_orden",
                    valorParametro = vRelacion.indOrden,
                    tipoParametro = System.Data.SqlDbType.SmallInt
                });


                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_gum_dd_insertar_relacion", vListParametrosSP);
               
                ValidarErrorEnSp(vListSIDocument);

                vRelacion.indCreadoEnGrilla = 0;

                PrvListRelacCamposGUM.Add(vRelacion);

            }

            vObjConexionDB.CerrarConexion();

            PubListRelacCamposGUMActual = new BindableCollection<clsRelacCamposGUM>(PrvListRelacCamposGUM.Where(vRelac => vRelac.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());
            //Cursor en espera
            Mouse.OverrideCursor = null;

            System.Windows.MessageBox.Show("Los cambios se guardaron correctamente", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);

        }


        public void ValidarErrorEnSp(DataSet pvDtsresultado)
        {
            string vStrResultado;

            vStrResultado = String.Empty;

            try
            {
                foreach (DataRow vDrCampos in pvDtsresultado.Tables[0].Rows)
                {
                    vStrResultado = Convert.ToString(vDrCampos["f_error"]);
                }


            }
            catch (Exception e) { }

            if (!String.IsNullOrEmpty(vStrResultado))
            {
                System.Windows.MessageBox.Show(vStrResultado, "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);
                throw new System.Exception();
            }

        }

        public void EliminarRelacion() {



            clsConexion vObjConexionDB = new clsConexion();
            //Objeto con parametros lista de parametros
            List<clsConexion.ParametrosSP> vListParametrosSP;
            List<clsRelacCamposGUM> vListRelacionesEliminadas;
            string vStrResultado;
            vStrResultado = "";
            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();


            vListRelacionesEliminadas = PrvListRelacCamposGUM.Where(s => (!PubListRelacCamposGUMActual.Where(es => es.nombreRelacion == s.nombreRelacion && es.nombreTabla == PubStrNombreTablaGUM && es.nombreCampo == s.nombreCampo && es.nombreCampoRef == s.nombreCampoRef && es.nombreTablaRef == s.nombreTablaRef  ).Any()) && s.nombreTabla == PubStrNombreTablaGUM).ToList();


            if (vListRelacionesEliminadas.Count() == 0)
            {
                return;
            }

            // se recorren las tablas editadas y se guardan sus datos
            foreach (clsRelacCamposGUM vCamposGUM in vListRelacionesEliminadas)
            {
                vListParametrosSP = new List<clsConexion.ParametrosSP>();

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_relacion",
                    valorParametro = vCamposGUM.nombreRelacion,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_tabla",
                    valorParametro = PubListCamposGUMActual.First().nombreTabla,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_campo",
                    valorParametro = vCamposGUM.nombreCampo,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_tabla_ref",
                    valorParametro = vCamposGUM.nombreTablaRef,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_campo_ref",
                    valorParametro = vCamposGUM.nombreCampoRef,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                var vListSIDocument = vObjConexionDB.EjecutarCommand("sp_gum_dd_eliminar_relacion", vListParametrosSP);

                PrvListRelacCamposGUM.Remove(vCamposGUM);
              }

            vObjConexionDB.CerrarConexion();

            //Cursor en espera
            Mouse.OverrideCursor = null;

            PubListRelacCamposGUMActual = new BindableCollection<clsRelacCamposGUM>(PrvListRelacCamposGUM.Where(vRelac => vRelac.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());

            System.Windows.MessageBox.Show("Los cambios se guardaron correctamente", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);

        }


        /// <summary>
        /// req. 162259 jpa 17042020
        /// Marca el indicador de modificado en la tabla selleccionada
        /// </summary>
        public void MarcarCambio()
        {
            //Actualizacion de campo seleccion en publica con tablas del sistema
            (from vCamposGUM in PubListCamposGUMActual
             where vCamposGUM.nombre == PubListTablasGUMCamposSeleccionada.nombre
             select vCamposGUM).ToList().ForEach(vTablaSistema => vTablaSistema.indCambio = 1);

            PubIntindCambioEnTabla = 1;
        }

    }
}
