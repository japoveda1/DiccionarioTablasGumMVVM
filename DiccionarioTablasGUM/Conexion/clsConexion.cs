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
                    vCommand.Dispose();
                    vTransaction.Rollback();
                    throw new System.Exception(e.Message.ToString());
                }

            }

            vCommand.Dispose();

            return vDataSet;
        }


        public DataSet EjecutarCommand(string stp_name, List<ParametrosSP> paramsStp, bool pvIndAbrirTran = true)
        {
            DataSet vDataSet = new DataSet();
            SqlCommand vCommand = new SqlCommand();
            int vIntContDataReader;
            SqlTransaction vTransaction;
            vTransaction = null;
            if (pvIndAbrirTran)
            {

                vTransaction = prvSqlConnection.BeginTransaction();
                vCommand.Transaction = vTransaction;
            }

            vIntContDataReader = 0;

            vCommand.CommandText = stp_name;
            vCommand.CommandTimeout = 600;
            vCommand.Connection = prvSqlConnection;
            vCommand.CommandType = CommandType.StoredProcedure;
            
            try
            {
                foreach (var item in paramsStp)
                {
                    vCommand.Parameters.AddWithValue("@" + item.nombreParametro, item.valorParametro).Direction = item.direccion;
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

                if (pvIndAbrirTran)
                {
                    vTransaction.Commit();
                }
            }
            catch(Exception e) {

                if (pvIndAbrirTran)
                {
                    vCommand.Dispose();
                    vTransaction.Rollback();
                    throw new System.Exception(e.Message.ToString());
                }

            }
           
            vCommand.Dispose();

            return vDataSet;
        }

    }
}
