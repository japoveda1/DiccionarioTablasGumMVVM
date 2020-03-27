using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionarioTablasGUM.Conexion
{
    public  class clsConexion
    {
        private SqlConnection prvSqlConnection;

        public class ParametrosSP
        {
            public string nombreParametro { get; set; }
            public object valorParametro { get; set; }
            public SqlDbType? tipoParametro { get; set; }
        }



        private void Inicializar()
        {
            prvSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UNOEE_CadeneConexion"].ConnectionString);
        }

        public bool AbrirConexion()
        {
            try
            {
                Inicializar();
                prvSqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool CerrarConexion()
        {
            try
            {
                prvSqlConnection.Close();
                prvSqlConnection.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet EjecutarCommand(string pvStrQuery)
        {

            SqlCommand vCommand = new SqlCommand();
            SqlDataAdapter vSqlDataAdapter = new SqlDataAdapter();
            DataSet vDataSet = new DataSet();

            vCommand.CommandText = pvStrQuery;
            vCommand.CommandTimeout = 600;
            vCommand.Connection = prvSqlConnection;

            vSqlDataAdapter.SelectCommand = vCommand;

            vSqlDataAdapter.Fill(vDataSet);

            return vDataSet;
        }


        public DataSet EjecutarCommand(string stp_name, List<ParametrosSP> paramsStp)
        {
            DataSet vDataSet = new DataSet();
            int vIntContDataReader;

            vIntContDataReader = 0;

            SqlCommand vCommand = new SqlCommand
            {
                CommandText = stp_name,
                CommandTimeout = 600,
                Connection = prvSqlConnection,
                CommandType = CommandType.StoredProcedure
            };

            foreach (var item in paramsStp) 
            {
                vCommand.Parameters.Add("@" + item.nombreParametro, item.tipoParametro).Value = item.valorParametro;
            }


            using (SqlDataReader dataReader = vCommand.ExecuteReader())
            {

                    while (dataReader.IsClosed == false)
                    {
                        //Create a new DataSet.
                        vDataSet.Tables.Add("Table" + vIntContDataReader.ToString());

                        //Load DataReader into the DataTable.
                        vDataSet.Tables[vIntContDataReader].Load(dataReader);

                        vIntContDataReader++;
                    }




            }


            vCommand.Dispose();

            return vDataSet;
        }





    }
}
