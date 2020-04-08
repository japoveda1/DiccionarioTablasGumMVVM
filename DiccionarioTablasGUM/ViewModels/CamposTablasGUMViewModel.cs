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
                                        List<clsCambiosCamposGUM> pvListCambiosCampoGUM,ref List<clsObjetosExportar> pvListExportar)
		    {

            PubStrNombreTablaGUM = pvObjTablaGUMSeleccionada.nombre;
            PubIntTablaConCambios = pvObjTablaGUMSeleccionada.indCambioEnDB;

            PrvListTablasGum = pvListTablasGUM;
            PrvObjTablaGumSeleccionada = pvObjTablaGUMSeleccionada;
            PrvListCamposGUM = pvListTablasGUMCampos;
            PubListCamposGUMActual = new  BindableCollection<clsCamposGUM>( PrvListCamposGUM.Where(vCampo => vCampo.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());
            PrvListRelacCamposGUM = pvListRelacCamposGUM;
            PubListRelacCamposGUMActual = new BindableCollection<clsRelacCamposGUM>(PrvListRelacCamposGUM.Where(vRelac => vRelac.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());
            PrvListCambiosCamposGum = pvListCambiosCampoGUM;
            PubListCambiosCampoGUMAtual = new BindableCollection<clsCambiosCamposGUM>(PrvListCambiosCamposGum.Where(vCambios => vCambios.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());

            PubListExportar = pvListExportar;
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

            vObjConexionDB.AbrirConexion();

            vListCamposGUM = PubListCamposGUMActual.Where(vTablaCampos => vTablaCampos.indCambio == 1).ToList();
            GuardarExportar();

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

            //Cursor en espera
            Mouse.OverrideCursor = null;

            System.Windows.MessageBox.Show("Los cambios se guardaron correctamente", "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);

        }
        
        //1:ultimo 2:anterior 3:siguiente 4:final
        public void navegacion(string pvStrAccion) {

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            int vIntIndexNuevaTablaSeleccionada;

            if (ConfirmacionCambiosPendientes("Hay cambios sin guardar.¿Desea continuar?", 1)) {
                Mouse.OverrideCursor = null;
                return;
            }

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




            PubListCamposGUMActual = new BindableCollection<clsCamposGUM>(PrvListCamposGUM.Where(vCampo => vCampo.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());
            PubListRelacCamposGUMActual = new BindableCollection<clsRelacCamposGUM>(PrvListRelacCamposGUM.Where(vRelac => vRelac.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());
            PubListCambiosCampoGUMAtual = new BindableCollection<clsCambiosCamposGUM>(PrvListCambiosCamposGum.Where(vCambios => vCambios.nombreTabla.Equals(PubStrNombreTablaGUM)).ToList());



            //Cursor en espera
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Valida si hay tablas sin almacenar y muesta mensaje para que el usuario desida la operacion
        /// </summary>
        /// <param name="pvStrMensaje"></param>
        /// <returns></returns>
        public bool ConfirmacionCambiosPendientes(string pvStrMensaje, int pvIntIndYesNo)
        {

            bool vBoolresultado;

            vBoolresultado = false;

            if (PubListCamposGUMActual.Where(vTablas => vTablas.indCambio == 1).Any())
            {
                if (pvIntIndYesNo == 1)
                {
                    if (System.Windows.MessageBox.Show(pvStrMensaje, "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        vBoolresultado = false;
                    }
                    else
                    {
                        vBoolresultado = true;
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show(pvStrMensaje, "Siesa - Diccionario Tablas GUM", System.Windows.MessageBoxButton.OK);
                    vBoolresultado = true;
                }
            }

            return vBoolresultado;

        }

        //llamar antes de ejecutar procesos
        private void GuardarExportar()
        {

            clsObjetosExportar vObjExportar;




            foreach (clsCamposGUM vTabla in PubListCamposGUMActual.Where(vCampos=> vCampos.indCambio == 1 ))
            {

                vObjExportar = new clsObjetosExportar();
                vObjExportar.nombreTablaGUM = "t7351_dd_campos";
                vObjExportar.nombreObjeto = vTabla.nombre;
                vObjExportar.indUpdate = 1;
                vObjExportar.IndInsert = 0;
  

                PubListExportar.Add(vObjExportar);
                vObjExportar = null;
            }




        }


    }
}
