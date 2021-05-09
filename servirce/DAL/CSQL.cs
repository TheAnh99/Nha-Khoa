using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace servirce.DAL
{
    public class CSQL
    {
        private string m_ConnectionString;
        private SqlCommand m_Command = new SqlCommand();
        private SqlConnection m_Connection;
        private SqlDataAdapter m_DataAdapter;
        private SqlDataReader m_DataReader;
        private double m_dblDuration;
        //===============================================================================================================

        public CSQL(string strConnectionString)
        {
            this.m_ConnectionString = strConnectionString;
            this.m_dblDuration = 0;
        }

        public SqlParameterCollection Parameters
        {
            get
            {
                m_Command.Parameters.Clear();
                return m_Command.Parameters;
            }
        }

        public SqlCommand Command
        {
            get { return m_Command; }
            set { m_Command = value; }
        }

        public SqlConnection Connection
        {
            get { return m_Connection; }
            set { m_Connection = value; }
        }

        public SqlDataReader DataReader
        {
            get { return m_DataReader; }
            set { m_DataReader = value; }
        }

        public int ExecDuration
        {
            get { return Convert.ToInt32(this.m_dblDuration); }
        }

        //Ham nay thuc hien viec ket noi voi database
        //COMMAND => The time in seconds to wait for the command to execute. The default is 30 seconds.
        public bool _OpenConnection()
        {
            bool functionReturnValue = false;

            try
            {
                if (m_Connection == null || m_Connection.State == ConnectionState.Closed) // 2015-07-16 09:33:40 ngocta2 => bug: open 2 connection, close 1
                {
                    // CHUA OPEN roi thi tao new connection
                    m_Connection = new SqlConnection(this.m_ConnectionString);
                    m_Connection.Open();
                    functionReturnValue = true;
                }
                else
                {
                    if (m_Connection.State == ConnectionState.Open)
                    {
                        // OPEN roi thi ko tao connection nua
                        functionReturnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                functionReturnValue = false;
                
            }
            return functionReturnValue;
        }

        //Ham nay dung de dong 1 connection den database
        public void _CloseConnection()
        {
            try
            {
                if ((m_Connection.State == ConnectionState.Open))
                {
                    m_Connection.Close();
                    m_Connection.Dispose();
                    m_Command.Dispose();
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
            }
        }

        /// <summary>
        /// log
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        public string GetParamInfo()
        {
            SqlParameterCollection Params = this.m_Command.Parameters;

            int i = 0;
            string s = "\r\n";
            for (i = 0; i <= Params.Count - 1; i++)
                s += "\t" + Params[i].ParameterName + "='" + Params[i].Value + "',\r\n";

            if (_OpenConnection() == false)           // Open Connection
                return null;

            //<add name="CONNECTION_STRING_50" connectionString="server=10.26.248.50;UID=sa;PWD=sa;database=EzTransfer;Connect Timeout=30;Pooling=false;"  providerName="System.Data.SqlClient"/>
            System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
            builder.ConnectionString = this.Connection.ConnectionString;
            string strServer = builder["server"] as string;
            string strDatabase = builder["database"] as string;
            s += "\t" + "--" + strServer + "=>" + strDatabase + "\r\n";

            return s;
        }
        //===============================================================================================================

        /// <summary>
        /// ExecuteScript
        /// </summary>
        /// <param name="SQL">delect * from tbl_test</param>
        /// <returns></returns>
        public bool ExecuteScript(string SQL)
        {
            bool functionReturnValue = false;
            if ((_OpenConnection() == false))           // Open Connection
                return false;

            m_Command.CommandTimeout = 0;
            m_Command.Connection = m_Connection;
            m_Command.CommandText = SQL;
            m_Command.CommandType = CommandType.Text;

            try
            {
                   // log SQL

                DateTime dtBegin = DateTime.Now; // duration                
                m_Command.ExecuteNonQuery();
                this.m_dblDuration = DateTime.Now.Subtract(dtBegin).TotalMilliseconds; // duration
                functionReturnValue = true;
            }
            catch (Exception ex)
            {
                functionReturnValue = false;
                
            }
            finally
            {
                _CloseConnection();     // Close Connection
            }
            return functionReturnValue;
        }

        /// <summary>
        /// ExecuteSP
        /// </summary>
        /// <param name="SPname">sp_DELETE_ALL</param>
        /// <returns></returns>
        public bool ExecuteSP(string SPname)
        {
            bool functionReturnValue = false;
            if ((_OpenConnection() == false))           // Open Connection
                return false;

            m_Command.Connection = m_Connection;
            m_Command.CommandText = SPname;
            m_Command.CommandType = CommandType.StoredProcedure;

            try
            {
                

                DateTime dtBegin = DateTime.Now; // duration                
                m_Command.ExecuteNonQuery();
                this.m_dblDuration = DateTime.Now.Subtract(dtBegin).TotalMilliseconds; // duration
                m_Command.Parameters.Clear();
                functionReturnValue = true;
            }
            catch (Exception ex)
            {
                functionReturnValue = false;
               
            }
            finally
            {
                _CloseConnection();     // Close Connection
            }
            return functionReturnValue;
        }

        /// <summary>
        /// GetDatasetFromScript
        /// </summary>
        /// <param name="SQL">select * from tbl_1 select * from tbl_2</param>
        /// <returns></returns>
        public DataSet GetDatasetFromScript(string SQL)
        {
            DataSet functionReturnValue = null;
            if ((_OpenConnection() == false))           // Open Connection
                return null;

            DataSet ds = new DataSet();
            try
            {
                   // log SQL 

                m_Command.Connection = m_Connection;
                m_Command.CommandText = SQL;
                m_Command.CommandType = CommandType.Text;

                DateTime dtBegin = DateTime.Now; // duration  
                m_DataAdapter = new SqlDataAdapter(m_Command);
                m_DataAdapter.Fill(ds);
                this.m_dblDuration = DateTime.Now.Subtract(dtBegin).TotalMilliseconds; // duration
                functionReturnValue = ds;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                
            }
            finally
            {
                _CloseConnection();     // Close Connection
            }
            return functionReturnValue;
        }

        /// <summary>
        /// GetDatasetFromSP
        /// </summary>
        /// <param name="SPname">sp_selectDataset</param>
        /// <returns></returns>
        public DataSet GetDatasetFromSP(string SPname)
        {
            DataSet functionReturnValue = null;
            if ((_OpenConnection() == false))           // Open Connection
                return null;
            DataSet ds = new DataSet();

            try
            {
             

                m_Command.Connection = m_Connection;
                m_Command.CommandText = SPname;
                m_Command.CommandType = CommandType.StoredProcedure;
                DateTime dtBegin = DateTime.Now; // duration  
                m_DataAdapter = new SqlDataAdapter(m_Command);
                m_DataAdapter.Fill(ds);
                this.m_dblDuration = DateTime.Now.Subtract(dtBegin).TotalMilliseconds; // duration
                m_Command.Parameters.Clear();
                functionReturnValue = ds;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                
            }
            finally
            {
                _CloseConnection();     // Close Connection
            }
            return functionReturnValue;
        }


        /// <summary>
        /// GetDataTableFromScript
        /// </summary>
        /// <param name="SQL">select * from tbl_test</param>
        /// <returns></returns>
        public DataTable GetDataTableFromScript(string SQL)
        {
            DataTable functionReturnValue = null;
            DataTable table = new DataTable();

            if ((_OpenConnection() == false))           // Open Connection
                return null;

            try
            {
                 // log SQL

                m_Command.Connection = m_Connection;
                m_Command.CommandText = SQL;
                m_Command.CommandType = CommandType.Text;

                DateTime dtBegin = DateTime.Now; // duration    
                m_DataAdapter = new SqlDataAdapter(m_Command);
                m_DataAdapter.Fill(table);
                this.m_dblDuration = DateTime.Now.Subtract(dtBegin).TotalMilliseconds; // duration
                functionReturnValue = table;
                //throw new Exception("ngocta2 gay loi");
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                
            }
            finally
            {
                _CloseConnection();     // Close Connection
            }
            return functionReturnValue;

        }

        /// <summary>
        /// GetDataTableFromSP
        /// </summary>
        /// <param name="SPname">sp_select</param>
        /// <returns></returns>
        public DataTable GetDataTableFromSP(string SPname)
        {
            DataTable functionReturnValue = null;
            DataTable table = new DataTable();

            if ((_OpenConnection() == false))           // Open Connection
                return null;

            try
            {
                    // log SQL

                m_Command.Connection = m_Connection;
                m_Command.CommandText = SPname;
                m_Command.CommandType = CommandType.StoredProcedure;

                DateTime dtBegin = DateTime.Now; // duration    
                m_DataAdapter = new SqlDataAdapter(m_Command);
                m_DataAdapter.Fill(table);
                this.m_dblDuration = DateTime.Now.Subtract(dtBegin).TotalMilliseconds; // duration
                m_Command.Parameters.Clear();
                functionReturnValue = table;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                
            }
            finally
            {
                _CloseConnection();     // Close Connection
            }
            return functionReturnValue;

        }

        /// <summary>
        ///     while(myReader.Read())
        ///     Console.WriteLine(myReader["Column1"].ToString());
        /// </summary>
        /// <param name="SQL">select * from tbl_test</param>
        /// <returns></returns>
        public SqlDataReader GetDataReaderFromScript(string SQL)
        {
            if ((_OpenConnection() == false))           // Open Connection
                return null;

            m_Command.Connection = m_Connection;
            m_Command.CommandText = SQL;
            m_Command.CommandType = CommandType.Text;
            try
            {
                  

                DateTime dtBegin = DateTime.Now; // duration  
                m_DataReader = m_Command.ExecuteReader();
                this.m_dblDuration = DateTime.Now.Subtract(dtBegin).TotalMilliseconds; // duration
                return m_DataReader;
            }
            catch (Exception ex)
            {
                
                return null;
            }
            finally
            {
                //_CloseConnection(); // _CloseConnection thi error, phai goi CloseConnection tu ben ngoai (Caller)
            }
        }

        /// <summary>
        ///     while(myReader.Read())
        ///     Console.WriteLine(myReader["Column1"].ToString());
        /// </summary>
        /// <param name="SPname">sp_SELECT</param>
        /// <returns></returns>
        public SqlDataReader GetDataReaderFromSP(string SPname)
        {
            if ((_OpenConnection() == false))           // Open Connection
                return null;

            m_Command.Connection = m_Connection;
            m_Command.CommandText = SPname;
            m_Command.CommandType = CommandType.StoredProcedure;
            try
            {
                  // log SQL

                DateTime dtBegin = DateTime.Now; // duration  
                m_DataReader = m_Command.ExecuteReader();
                this.m_dblDuration = DateTime.Now.Subtract(dtBegin).TotalMilliseconds; // duration
                
                
            }
            catch (Exception ex)
            {
                
                return null;
            }
            finally
            {
              //  _CloseConnection(); // _CloseConnection thi error, phai goi CloseConnection tu ben ngoai (Caller)

            }
            return m_DataReader;
        }
    }
}
