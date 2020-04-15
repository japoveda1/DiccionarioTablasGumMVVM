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
    public class clsConexion
    {
        private SqlConnection prvSqlConnection;

        public class ParametrosSP
        {
            public string nombreParametro { get; set; }
            public object valorParametro { get; set; }
            public SqlDbType? tipoParametro { get; set; }
            public ParameterDirection direccion { get; set; } = ParameterDirection.Input;
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

        public DataSet EjecutarCommand(string pvStrQuery,bool pvIndAbrirTran=true)
        {

            SqlCommand vCommand = new SqlCommand();
            
            SqlDataAdapter vSqlDataAdapter = new SqlDataAdapter();
            DataSet vDataSet = new DataSet();
            SqlTransaction vTransaction;
            vTransaction = null;
            if (pvIndAbrirTran) {

                vTransaction = prvSqlConnection.BeginTransaction();
                vCommand.Transaction = vTransaction;
            }
           

            try
            {

                vCommand.CommandText = pvStrQuery;
                vCommand.CommandTimeout = 600;
                //
                vCommand.Connection = prvSqlConnection;

                vSqlDataAdapter.SelectCommand = vCommand;

                vSqlDataAdapter.Fill(vDataSet);


                if (pvIndAbrirTran)
                {
                    vTransaction.Commit();
                }


            } catch(Exception e) {



                if (pvIndAbrirTran)
                {
                    vTransaction.Rollback();
                }


            }



            vCommand.Dispose();

            return vDataSet;
        }


        public DataSet EjecutarCommand(string stp_name, List<ParametrosSP> paramsStp)
        {
            DataSet vDataSet = new DataSet();
            int vIntContDataReader;
            SqlTransaction vTransaction;

            vIntContDataReader = 0;
            vTransaction = prvSqlConnection.BeginTransaction();

            SqlCommand vCommand = new SqlCommand
            {
                CommandText = stp_name,
                CommandTimeout = 600,
                Connection = prvSqlConnection,
                CommandType = CommandType.StoredProcedure,
                Transaction = vTransaction
            };
            
            try
            {
                foreach (var item in paramsStp)
                {
                  //  vCommand.Parameters.AddWithValue("@" + item.nombreParametro, item.tipoParametro);//.Value = item.valorParametro;
                    vCommand.Parameters.AddWithValue("@" + item.nombreParametro, item.valorParametro).Direction = item.direccion;//.Value = item.valorParametro;
                    

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

                vTransaction.Commit();

            }
            catch(Exception e) {

                vTransaction.Rollback();

            }

           
            vCommand.Dispose();

            return vDataSet;
        }





    }
}
