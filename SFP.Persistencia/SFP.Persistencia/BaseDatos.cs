using Oracle.ManagedDataAccess.Client;
using SFP.Persistencia.Model;
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
        public string _sQuery { set; get; }
        private TipoBaseDatos _TipoBd { set; get; }

        protected string _prefijoParam;
        private enum TipoBaseDatos { ORACLE, MSSQL };
        public BaseDatos(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            if ( cn is OracleConnection )
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
            if (_cn is OracleConnection)
            {
                DataSet ds = new DataSet();
                try
                {
                    using (DbDataAdapter oDataAdapter = ObtenerObjeto<DbDataAdapter>(_sDataAdapter))
                    {
                        oDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        // Create the commands.
                        oDataAdapter.SelectCommand = new OracleCommand("SELECT " + sSecuencia + ".nextval as NEXTVAL FROM dual", (OracleConnection)_cn);
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
                    MsjError = "Error al ejecutar la instrucción de SQL : " + _sQuery;
                    throw new PesistenciaException("Error al ejecutar la instrucción de SQL : " + ex.ToString());
                }
            }
            else
                throw new PesistenciaException("Solo habilitado para ORACLE");

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
                    oDbCmd.Transaction = _transaction;

                iRegistros = oDbCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MsjError = "Error al ejecutar la instrucción de SQL : " + _sQuery;
                throw new PesistenciaException("Error al ejecutar la instrucción de SQL : " + ex.ToString());
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
            if (_cn is SqlConnection)
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)_cn, SqlBulkCopyOptions.KeepIdentity, (SqlTransaction)_transaction))
                {
                    bulkCopy.DestinationTableName = sTabla;
                    bulkCopy.WriteToServer(dtDatos);
                }
            }
            else
                throw new PesistenciaException("Solo habilitado para MSSQL server ");

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
                MsjError = "Error al ejecutar la instrucción de SQL : " + _sQuery;
                throw new PesistenciaException("Error al ejecutar la instrucción de SQL : " + ex.ToString());
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
                MsjError = "Error al ejecutar la instrucción de SQL : " + _sQuery;
                throw new PesistenciaException("Error al ejecutar la instrucción de SQL : " + ex.ToString());
            }
            return ds.Tables[0];
        }
        private void ReemplazarPrimero(ref StringBuilder sbOrigen, string sBuscar, string sReemplazar)
        {
            String sOriBuscar = sbOrigen.ToString();

            int iPosicion = sOriBuscar.IndexOf(sBuscar, StringComparison.InvariantCulture); 

            if (iPosicion > -1)
                sbOrigen.Replace(sBuscar, sReemplazar, iPosicion, sBuscar.Length);
        }
        private DbCommand obtenerCommand(string sQuery, params Object[] aoParametros)
        {
            DbCommand oDbCmd; 

            if (_TipoBd == TipoBaseDatos.ORACLE)
                oDbCmd = AgregarParametroOracle(sQuery, aoParametros);
            else
            {
                oDbCmd = AgregarParametroMsSql(sQuery, aoParametros);
            }

            return oDbCmd;
        }

       
        private void OracleTipoDato(object oDato, ref OracleCommand oracleCmd, ref StringBuilder sbQuery, int iElem)
        {

            if (oDato is int)
            {
                oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Int32).Value = oDato;
                ReemplazarPrimero(ref sbQuery, ":P" + iElem, oDato.ToString());
            }

            else if ((oDato is UInt32) ||
                (oDato is long))
            {
                oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Int64).Value = oDato;
                ReemplazarPrimero(ref sbQuery, ":P" + iElem, oDato.ToString());
            }
            else if (oDato is DateTime)
            {
                DateTime dtFechaParam = (DateTime)oDato;

                if (dtFechaParam.Equals(DateTime.MinValue))
                {
                    oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Date).Value = DBNull.Value;
                    ReemplazarPrimero(ref sbQuery, ":P" + iElem, " null ");
                }
                else if (dtFechaParam.TimeOfDay.TotalSeconds < 1)
                {
                    oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Date).Value = oDato;
                    ReemplazarPrimero(ref sbQuery, ":P" + iElem, " TO_DATE('" + dtFechaParam.ToString("yyyy/MM/dd") + "', 'yyyy/mm/dd') ");
                }
                else
                {
                    oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.TimeStamp).Value = oDato;
                    ReemplazarPrimero(ref sbQuery, ":P" + iElem, " TO_TIMESTAMP('" + dtFechaParam.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture) + "', 'MM/DD/YYYY HH24:MI:SS') ");
                }
            }
            else if (oDato is Byte)
            {
                oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Blob).Value = oDato;
                ReemplazarPrimero(ref sbQuery, ":P" + iElem, "byte[]");

            }
            else if (oDato is short)
            {
                oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Int16).Value = oDato;
                ReemplazarPrimero(ref sbQuery, ":P" + iElem, oDato.ToString());

            }
            else if (oDato is Single)
            {
                oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Single).Value = oDato;
                Single sDato = (Single)oDato;
                ReemplazarPrimero(ref sbQuery, ":P" + iElem, sDato.ToString("N4"));
            }
            else if (oDato is Double)
            {
                oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Double).Value = oDato;
                Double dDato = (Double)oDato;
                ReemplazarPrimero(ref sbQuery, ":P" + iElem, dDato.ToString("N4"));
            }
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
                    else if (aoParametros[iElem] is string)
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Varchar2).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, "'" + aoParametros[iElem] + "'");

                    }
                    else if (aoParametros[iElem] is Char)
                    {
                        oracleCmd.Parameters.Add(":P" + iElem, OracleDbType.Char).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, ":P" + iElem, aoParametros[iElem].ToString());
                    }
                    else
                    {
                        OracleTipoDato(aoParametros[iElem], ref oracleCmd, ref sbQuery, iElem);
                    }                    
                }
                _sQuery = sbQuery.ToString();
            }
            return oracleCmd;
        }

        //FALTA IMPLEMENTAR un tipo de tipo boolen  ( aoParametros[iElem] IS Boolean)
        //FALTA IMPLEMENTAR un tipo de tipo boolen  ( aoParametros[iElem] IS Byte)
        /*  TAMBIEN REIVSAR EL TIPO DATE
                        ElseIf TypeOf aoParametros(iElem) Is Date Then
                            sqlCmd.Parameters.AddWithValue("@P" & iElem, SqlDbType.Date).Value = aoParametros(iElem)
                            sbQuery.Replace("@P" & iElem, " Convert(datetime,'" & CDate(aoParametros(iElem)).ToString("yyyyMMdd") & "',112)")
        */

        private void MSsqlTipoDato(object oDato, ref SqlCommand sqlCmd, ref StringBuilder sbQuery, int iElem)
        {
            if (oDato is int)
            {
                sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Int).Value = oDato;
                ReemplazarPrimero(ref sbQuery, "@P" + iElem, oDato.ToString());

            }
            else if (oDato is long)
            {
                sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Float).Value = oDato;
                ReemplazarPrimero(ref sbQuery, "@P" + iElem, oDato.ToString());
            }
            else if (oDato is DateTime)
            {
                // if si el valor es el minimo signific que lleva NULL
                if (((DateTime)oDato).Equals((DateTime.MinValue)))
                {
                    sqlCmd.Parameters.AddWithValue("@P" + iElem, DBNull.Value).Value = DBNull.Value;
                    ReemplazarPrimero(ref sbQuery, "@P" + iElem, "null");
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.DateTime).Value = oDato;
                    ReemplazarPrimero(ref sbQuery, "@P" + iElem, " Convert(datetime,'" + ((DateTime)oDato).Date.ToString("yyyyMMdd") + "',112)");
                }
            }
            else if (oDato is Char)
            {
                sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Char).Value = oDato;
                ReemplazarPrimero(ref sbQuery, "@P" + iElem, oDato.ToString());

            }

            else if (oDato is short)
            {
                sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.SmallInt).Value = oDato;
                ReemplazarPrimero(ref sbQuery, "@P" + iElem, oDato.ToString());

            }
            else if (oDato is Single)
            {
                sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Float).Value = oDato;
                ReemplazarPrimero(ref sbQuery, "@P" + iElem, string.Format(oDato.ToString()));

            }
            else if (oDato is Double)
            {
                sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Real).Value = oDato;
                ReemplazarPrimero(ref sbQuery, "@P" + iElem, string.Format(oDato.ToString()));
            }
        }


        private DbCommand AgregarParametroMsSql(string sQuery, params Object[] aoParametros)
        {
            /*  Acutalziar esta rutina
             *  http://stackoverflow.com/questions/4233536/c-sharp-store-functions-in-a-dictionary   */
            SqlCommand sqlCmd;

            if ( _transaction != null)
                sqlCmd = new SqlCommand(sQuery, (SqlConnection)_cn, (SqlTransaction) _transaction);
            else
                sqlCmd = new SqlCommand(sQuery, (SqlConnection) _cn);

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
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, DBNull.Value).Value = DBNull.Value;
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, "null");
                    }

                    else if (aoParametros[iElem] is string)
                    {
                        sqlCmd.Parameters.AddWithValue("@P" + iElem, SqlDbType.Text).Value = aoParametros[iElem];
                        ReemplazarPrimero(ref sbQuery, "@P" + iElem, "'" + aoParametros[iElem] + "'");
                    }
                    else
                    {
                        MSsqlTipoDato(aoParametros[iElem], ref sqlCmd, ref sbQuery, iElem);
                    }

                    _sQuery = sbQuery.ToString();
                }
            }
            return sqlCmd;
        }

    }
}
