using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace SFP.Persistencia
{
    public class BaseDatos : BaseFunc, IBaseDb
    {
        private string _sQuery;
        private TipoBaseDatos _TipoBd;
        protected string _prefijoParam;
        private enum TipoBaseDatos { ORACLE, MSSQL };
        public BaseDatos(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            if (cn.GetType() == typeof(OracleConnection))
            {
                _TipoBd = TipoBaseDatos.ORACLE;
                _prefijoParam = "#";
            }
            else
            {
                _TipoBd = TipoBaseDatos.MSSQL;
                _prefijoParam = "@";
            }                
        }

        protected int SecuenciaDML(String sSecuencia)
        {
            int iID = -1;
            if (_cn.GetType() == typeof(OracleConnection))
            {
                DataSet ds = new DataSet();
                try
                {
                    using (DbDataAdapter oDataAdapter = ObtenerObjeto<DbDataAdapter>(_sDataAdapter))
                    {
                        oDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        // Create the commands.
                        oDataAdapter.SelectCommand = new OracleCommand("SELECT " + sSecuencia + ".nextval as NEXTVAL FROM dual", (OracleConnection)_cn); ;
                        oDataAdapter.Fill(ds);

                        DataTable dtDatos = ds.Tables[0];
                        foreach (DataRow row in dtDatos.Rows)
                        {
                            iID = Convert.ToInt32(dtDatos.Rows[0]["NEXTVAL"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _sMsjError = "Error al ejecutar la instrucción de SQL : " + _sQuery;
                    throw new Exception("Error al ejecutar la instrucción de SQL : " + ex.ToString());
                }
            }
            else
                throw new Exception("Solo habilitado para ORACLE");

            return iID;
        }

        public object EjecutaDML(string psQuery, params object[] aoParametros)
        {
            DbCommand oDbCmd = null;

            int iRegistros;
            try
            {
                oDbCmd = obtenerCommand(psQuery, aoParametros);
                if (_transaction != null)
                    oDbCmd.Transaction = (DbTransaction)_transaction;

                iRegistros = oDbCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _sMsjError = "Error al ejecutar la instrucción de SQL : " + _sQuery;
                throw new Exception("Error al ejecutar la instrucción de SQL : " + ex.ToString());
            }
            finally
            {
                if (!oDbCmd.Equals(null))
                    oDbCmd.Dispose();
            }
            return iRegistros;
        }

        // SOLO FUNCIONA PARA MSQLSERVER 
        public bool EjecutarBulkCopy(DataTable dtDatos, string sTabla)
        {
            if (_cn.GetType() == typeof(SqlConnection))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)_cn, SqlBulkCopyOptions.KeepIdentity, (SqlTransaction)_transaction))
                {
                    bulkCopy.DestinationTableName = sTabla;
                    bulkCopy.WriteToServer(dtDatos);
                }
            }
            else
                throw new Exception("Solo habilitado para MSSQL server ");

            return true;
        }


        public DataTable ConsultaSP(string psStoreProc, Dictionary<string, object> dicParam)
        {
            DataTable dtResultado = new DataTable();
            try
            {
                using (DbDataAdapter oDataAdapter = ObtenerObjeto<DbDataAdapter>(_sDataAdapter))
                {
                    oDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    // Create the commands.                    
                    if (_transaction != null)
                        oDataAdapter.SelectCommand = new SqlCommand(psStoreProc, (SqlConnection)_cn, (SqlTransaction)_transaction);
                    else
                        oDataAdapter.SelectCommand = new SqlCommand(psStoreProc, (SqlConnection)_cn);

                    if (dicParam != null)
                    {
                        foreach (KeyValuePair<string, object> kpvParam in dicParam)
                        {
                            oDataAdapter.SelectCommand.Parameters.Add(new SqlParameter(kpvParam.Key, kpvParam.Value));
                        }
                    }


                    oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    
                    DbDataReader mReader = oDataAdapter.SelectCommand.ExecuteReader();
                    dtResultado.Load(mReader);                    
                }
            }
            catch (Exception ex)
            {
                _sMsjError = "Error al ejecutar la instrucción de SQL : " + _sQuery;
                throw new Exception("Error al ejecutar la instrucción de SQL : " + ex.ToString());
            }
            return dtResultado;
        }



        public DataTable ConsultaDML(string psQuery, params object[] aoParametros)
        {
            DataSet ds = new DataSet();
            try 
            {
                using (DbDataAdapter oDataAdapter = ObtenerObjeto<DbDataAdapter>(_sDataAdapter))
                {
                    oDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    // Create the commands.
                    oDataAdapter.SelectCommand = obtenerCommand(psQuery, aoParametros);
                    oDataAdapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                _sMsjError = "Error al ejecutar la instrucción de SQL : " + _sQuery;
                throw new Exception("Error al ejecutar la instrucción de SQL : " + ex.ToString());
            }
            return ds.Tables[0];
        }
        private void ReemplazarPrimero(ref StringBuilder sbOrigen, string sBuscar, string sReemplazar)
        {
            int iPosicion = sbOrigen.ToString().IndexOf(sBuscar);

            if (iPosicion > -1)
                sbOrigen.Replace(sBuscar, sReemplazar, iPosicion, sBuscar.Length);
        }
        private DbCommand obtenerCommand(string sQuery, params Object[] aoParametros)
        {
            DbCommand oDbCmd = null; 

            if (_TipoBd == TipoBaseDatos.ORACLE)
                oDbCmd = AgregarParametroOracle(sQuery, aoParametros);
            else
            {
                oDbCmd = AgregarParametroMsSql(sQuery, aoParametros);
            }

            return oDbCmd;
        }
        private DbCommand AgregarParametroOracle(string sQuery, params Object[] aoParametros)
        {
            OracleCommand oracleCmd = new OracleCommand(sQuery, (OracleConnection)_cn);
            int iTotElem;
            Int16 iElem;
            StringBuilder sbQuery = new StringBuilder(sQuery);

            iTotElem = aoParametros.Length;

            if (iTotElem == 0)
                _sQuery = sQuery;
            else
            {
                for (iElem = 0; iElem < iTotElem; iElem++)
                {
                    if (aoParametros[iElem] == null)
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, DBNull.Value);
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, "null");
                    }

                    else if (aoParametros[iElem].GetType() == typeof(string))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Varchar2).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, "'" + aoParametros[iElem] + "'");

                    }
                    else if (aoParametros[iElem].GetType() == typeof(int))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Int32).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, aoParametros[iElem].ToString());
                    }

                    else if (aoParametros[iElem].GetType() == typeof(UInt32))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Int64).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, aoParametros[iElem].ToString());
                    }

                    else if (aoParametros[iElem].GetType() == typeof(long))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Int64).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, aoParametros[iElem].ToString());

                    }
                    else if (aoParametros[iElem].GetType() == typeof(DateTime))
                    {
                        if (((DateTime)(aoParametros[iElem])).Equals(DateTime.MinValue))
                        {
                            oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Date).Value = DBNull.Value;
                            ReemplazarPrimero(ref sbQuery, ":P" + iElem, " null ");
                        }
                        else
                        {
                            DateTime FechaActual = (DateTime)aoParametros[iElem];
                            if (FechaActual.TimeOfDay.TotalSeconds == 0)
                            {
                                oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Date).Value = aoParametros[iElem];
                                ReemplazarPrimero(ref sbQuery, ":P" + iElem, " TO_DATE('" + FechaActual.ToString("yyyy/MM/dd") + "', 'yyyy/mm/dd') ");
                            }
                            else
                            {
                                oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.TimeStamp).Value = aoParametros[iElem];
                                ReemplazarPrimero(ref sbQuery, ":P" + iElem, " TO_TIMESTAMP('" + FechaActual.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture) + "', 'MM/DD/YYYY HH24:MI:SS') ");
                            }
                        }
                    }
                    else if (aoParametros[iElem].GetType() == typeof(Char))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Char).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, aoParametros[iElem].ToString());
                    }
                    else if (aoParametros[iElem].GetType() == typeof(Byte))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Blob).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, "byte[]");

                    }
                    else if (aoParametros[iElem].GetType() == typeof(short))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Int16).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, aoParametros[iElem].ToString());

                    }
                    else if (aoParametros[iElem].GetType() == typeof(Single))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Single).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, string.Format("0.00", aoParametros[iElem].ToString()));

                    }
                    else if (aoParametros[iElem].GetType() == typeof(Double))
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Double).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, string.Format("0.00", aoParametros[iElem].ToString()));

                    }
                    _sQuery = sbQuery.ToString();
                }
            }
            return oracleCmd;
        }
        private DbCommand AgregarParametroMsSql(string sQuery, params Object[] aoParametros)
        {

            /*  Acutalziar esta rutina
             *  http://stackoverflow.com/questions/4233536/c-sharp-store-functions-in-a-dictionary   */

            string[] asDatos;

            SqlCommand sqlCmd;

            if ( _transaction != null)
                sqlCmd = new SqlCommand(sQuery, (SqlConnection)_cn, (SqlTransaction) _transaction);
            else
                sqlCmd = new SqlCommand(sQuery, (SqlConnection) _cn);

            int iTotElem;
            Int16 iElem;
            StringBuilder sbQuery = new StringBuilder(sQuery);

            asDatos = sQuery.Split(new string[] { "@P" }, StringSplitOptions.None);

            iTotElem = aoParametros.Length;

            if (iTotElem == 0)
                _sQuery = sQuery;
            else
            {
                for (iElem = 0; iElem < iTotElem; iElem++)
                {
                    if (aoParametros[iElem] == null)
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, DBNull.Value).Value = DBNull.Value;
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, "null");
                    }
                    else if (aoParametros[iElem].GetType() == typeof(string))
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Text).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, "'" + aoParametros[iElem] + "'");

                    }
                    else if (aoParametros[iElem].GetType() == typeof(int))
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Int).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, aoParametros[iElem].ToString());

                    }
                    else if (aoParametros[iElem].GetType() == typeof(long))
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Float).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, aoParametros[iElem].ToString());
                    }
                    else if (aoParametros[iElem].GetType() == typeof(DateTime))
                    {
                        // if si el valor es el minimo signific que lleva NULL

                        if (((DateTime)aoParametros[iElem]).Equals((DateTime.MinValue)))
                        {
                            sqlCmd.Parameters.AddWithValue("@P" + iElem, DBNull.Value).Value = DBNull.Value;
                            ReemplazarPrimero(ref sbQuery, "@P" + iElem, "null");
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.DateTime).Value = aoParametros[iElem];
                            ReemplazarPrimero(ref sbQuery, "@P" + iElem, " Convert(datetime,'" + ((DateTime)aoParametros[iElem]).Date.ToString("yyyyMMdd") + "',112)");
                        }
                    }
                    /*                    
                                    ElseIf TypeOf aoParametros(iElem) Is Date Then
                                        sqlCmd.Parameters.AddWithValue("@P" & iElem, SqlDbType.Date).Value = aoParametros(iElem)
                                        sbQuery.Replace("@P" & iElem, " Convert(datetime,'" & CDate(aoParametros(iElem)).ToString("yyyyMMdd") & "',112)")
                    */
                    else if (aoParametros[iElem].GetType() == typeof(Char))
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Char).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, aoParametros[iElem].ToString());

                    }
                    else if (aoParametros[iElem].GetType() == typeof(Byte))
                    {

                    }
                    else if (aoParametros[iElem].GetType() == typeof(short))
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.SmallInt).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, aoParametros[iElem].ToString());

                    }
                    else if (aoParametros[iElem].GetType() == typeof(Single))
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Float).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, string.Format(aoParametros[iElem].ToString()));

                    }
                    else if (aoParametros[iElem].GetType() == typeof(Double))
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Real).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, string.Format( aoParametros[iElem].ToString()));

                    }
                    //else if (aoParametros[iElem].GetType() == typeof(Boolean))
                    //{

                    //}

                    _sQuery = sbQuery.ToString();
                }
            }
            return sqlCmd;
        }

    }
}
