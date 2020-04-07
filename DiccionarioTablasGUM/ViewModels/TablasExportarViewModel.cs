using Caliburn.Micro;
using DiccionarioTablasGUM.Conexion;
using DiccionarioTablasGUM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiccionarioTablasGUM.ViewModels
{
    public class TablasExportarViewModel:Screen
    {

        private BindableCollection<TablaSistema> _prvListaTablasExportar;

        public BindableCollection<TablaSistema> pubListaTablasExportar
        {
            get
            {
                return _prvListaTablasExportar;
            }
            set
            {
                _prvListaTablasExportar = value;
                NotifyOfPropertyChange(() => pubListaTablasExportar);
            }
        }

        public TablasExportarViewModel(List<string> pvTablas) {

            TablaSistema vObj;
            List<TablaSistema> vListObj = new List<TablaSistema>();

            foreach (string v in pvTablas ) {

                vObj = new TablaSistema()
                {

                    nombreTabla = v,
                    seleccion = 1
                };

                vListObj.Add(vObj);

                vObj = null;
            }

            pubListaTablasExportar = new BindableCollection<TablaSistema>(vListObj);



        }

        public void export() {


            List<clsConexion.ParametrosSP> vListParametrosSP;
            List<string> vListTablasseleccionadas = new List<string>();
            DataSet vDsTablasRelacionadas;

            //creo la conexion a la base de datos
            clsConexion vObjConexionDB = new clsConexion();

            //Cursor en espera
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            vObjConexionDB.AbrirConexion();

           
           vListParametrosSP = new List<clsConexion.ParametrosSP>();

            vListTablasseleccionadas = pubListaTablasExportar.Where(x => x.seleccion == 1).Select(x =>x.nombreTabla).ToList();

            //vListTablasseleccionadas = pubListaTablasExportar.Where(x => x.nombreTabla == "t010_mm_companias").Select(x =>x.nombreTabla).ToList();


            foreach (string vtablanombre in vListTablasseleccionadas) {

                vListParametrosSP.Add(new clsConexion.ParametrosSP
                {
                    nombreParametro = "p_nombre_tabla",
                    valorParametro = vtablanombre,
                    tipoParametro = System.Data.SqlDbType.VarChar
                });

                vDsTablasRelacionadas = vObjConexionDB.EjecutarCommand("sp_gum_dd_exportar", vListParametrosSP);

                string query;
                string rutaCompleta = @" C:\Users\johan.poveda\Desktop\mi archivo.txt";
                string texto;
                //creacion de objeto clsTablasGUM

                for (int i = 0; i <= 7;i++) {


                    foreach (DataRow vDR in vDsTablasRelacionadas.Tables[i].Rows)
                    {


                        texto = Convert.ToString(vDR["f_query"]);

                        using (StreamWriter mylogs = File.AppendText(rutaCompleta))         //se crea el archivo
                        {

                            //se adiciona alguna información y la fecha


                            //DateTime dateTime = new DateTime();
                            //dateTime = DateTime.Now;
                            //string strDate = Convert.ToDateTime(dateTime).ToString("yyMMdd");

                            mylogs.WriteLine(texto);

                            mylogs.Close();


                        }



                    }

                }

                
                vListParametrosSP.Clear();

            }


            Mouse.OverrideCursor = null;


        }

    }
}
