using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SFP.Persistencia.Dao
{
    public abstract class BaseDao : BaseDatos
    {
        public const short OPE_INSERTAR = 1;
        public const short OPE_EDITAR = 2;
        public const short OPE_BORRAR = 3;
        public const short OPE_HABILITAR = 4;
        public const short OPE_DESHABILITAR = 5;

        public const short OPE_IMPORTAR = 7;
        public const short OPE_BULKCOPY_ALL = 8;

        public const string CFG_TABLA_IDENTIDAD = "identidad";
        public const string CFG_TABLA_SECUENCIA = "secuencia";

        public const string OPE_INSERTAR_ETIQUETA = "Insertar";
        public const string OPE_EDITAR_ETIQUETA = "Editar";
        public const string OPE_BORRAR_ETIQUETA = "Borrar";

        public const string CMD_ENTIDAD = "ENTIDAD";
        public const string CMD_OPERACION = "OPERACION";

        public const string CMD_GRID = "dmlSelectGrid";

        public const short PAGINA_NUMERO = 0;
        public const short PAGINA_REGISTROS = 0;

        public const string NO_RECORDS = "{ \"records\":[]}";


        public Dictionary<string, string> _dicTabla;

        public int _iLimInf;
        public int _iLimSup;
        protected BaseDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
        }


        protected T CrearMDL<T>(DataTable dtDatos) where T : new()
        {
            List<T> lstDatos = CrearListaMDL<T>(dtDatos);

            if (lstDatos.Count > 0)
                return lstDatos[0];
            else
                return default(T);
        }


        private Int16  DatoInt16(  ref object oDato  )
        {
            if (oDato is DBNull)
                return 0;
            else
                return Convert.ToInt16(oDato);
        }

        private Int16? DatoInt16Null(  ref object oDato  )
        {
            if (oDato is DBNull)
                return null;
            else
                return Convert.ToInt16(oDato);
        }

        private Int32 DatoInt32(  ref object oDato  )
        {
            if (oDato is DBNull)
                return 0;
            else
                return Convert.ToInt32(oDato);
        }

        private Int32? DatoInt32Null(  ref object oDato )
        {
            if (oDato is DBNull)
                return null;
            else
                return  Convert.ToInt32(oDato);
        }

        private Int64 DatoInt64(  ref object oDato  )
        {
            if (oDato is DBNull)
                return 0;
            else
                return Convert.ToInt64(oDato);
        }

        private Int64? DatoInt64Null(  ref object oDato  )
        {
            if (oDato is DBNull)
                return null;
            else
                return Convert.ToInt64(oDato);
        }

        private Single DatoSingle(  ref object oDato  )
        {
            if (oDato is DBNull)
                return 0;
            else
                return Convert.ToSingle(oDato);
        }

        private Single? DatoSingleNull(  ref object oDato  )
        {
            if (oDato is DBNull)
                return null;
            else
                return Convert.ToSingle(oDato);
        }

        private Double DatoDouble(  ref object oDato  )
        {
            if (oDato is DBNull)
                return 0;
            else
                return Convert.ToDouble(oDato);
        }

        private Double? DatoDoubleNull( ref object oDato)
        {
            if (oDato is DBNull)
                return null;
            else
                return Convert.ToDouble(oDato);
        }

        private DateTime DatoDateTime(ref object oDato)
        {
            if (oDato is DBNull)
                return DateTime.MinValue;
            else
                return  Convert.ToDateTime(oDato);
        }

        private byte DatoByte(ref object oDato)
        {
            if (oDato is DBNull)
                return 0;
            else
                return Convert.ToByte(oDato);
        }


        private object  ValidarTipoDato(ref PropertyInfo propActual, object oDato)
        {

            if (propActual.PropertyType == typeof(string))
            {
                return oDato.ToString();
            }
            else if (propActual.PropertyType == typeof(int) || propActual.PropertyType == typeof(Int32))
            {
                return DatoInt32( ref oDato);
            }
            else if (propActual.PropertyType == typeof(int?))
            {
                return DatoInt32Null( ref oDato);
            }
            else if (propActual.PropertyType == typeof(long))
            {
                return DatoInt64( ref oDato );
            }
            else if (propActual.PropertyType == typeof(long?))
            {
                return DatoInt64Null(ref oDato);
            }
            else if (propActual.PropertyType == typeof(short))
            {
                return DatoInt16( ref oDato);
            }
            else if (propActual.PropertyType == typeof(short?))
            {
                return DatoInt16Null( ref oDato);
            }
            else if (propActual.PropertyType == typeof(Single))
            {
                return DatoSingle( ref oDato);
            }
            else if (propActual.PropertyType == typeof(Single?))
            {
                return DatoSingleNull(ref oDato);
            }
            else if (propActual.PropertyType == typeof(Double))
            {
                return DatoDouble( ref oDato);
            }
            else if (propActual.PropertyType == typeof(Double?))
            {
                return  DatoDoubleNull( ref oDato);
            }
            else if (propActual.PropertyType == typeof(DateTime))
            {                
                return  DatoDateTime(ref oDato);
            }
            else if (propActual.PropertyType == typeof(Byte))
            {
                return DatoByte(ref oDato);
            }
            else
            {
                throw new System.ArgumentException("ERROR EN ELTIPO DE DATO", "original");
            }

        }

        protected List<T> CrearListaMDL<T>(DataTable dtDatos) where T : new()
        {
            
            if (dtDatos == null)
            {
                return new List<T>();
            }

            string[] nombreCampos = dtDatos.Columns.Cast<DataColumn>()
                                             .Select(x => x.ColumnName)
                                             .ToArray();

            List<T> lstObj = new List<T>();
            Type type = typeof(T);

            try
            {
                for (int iIdx = 0; iIdx < dtDatos.Rows.Count; iIdx++)
                {
                    T objMdl = new T();
                    for (int iIdxCol = 0; iIdxCol < dtDatos.Columns.Count; iIdxCol++)
                    {
                        PropertyInfo propActual = type.GetProperty(nombreCampos[iIdxCol], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        propActual.SetValue(objMdl, ValidarTipoDato(ref propActual, dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]) );

                    }
                    lstObj.Add(objMdl);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(" Error en el dato :  " + ex.Message);
                throw new System.ArgumentException("ERROR EN ELTIPO DE DATO", "original");
            }

            return lstObj;
        }

        public String JsonW2uiRecords(DataTable dtDatos)
        {
            StringBuilder sbJson = new StringBuilder();
            int iRegTotal = 0;

            sbJson.AppendLine("{  \"status\":\"success\",");
            
            if (dtDatos.Rows.Count == 0)
            {
                sbJson.AppendLine(NO_RECORDS);
                
            }
            else
            {
                sbJson.AppendLine(" \"records\":[");
                string[] nombreCampos = dtDatos.Columns.Cast<DataColumn>()
                                                 .Select(x => x.ColumnName)
                                                 .ToArray();

                for (int iIdxRow = 0; iIdxRow < dtDatos.Rows.Count; iIdxRow++)
                {                    
                    sbJson.Append("{ \"recid\":" + iRegTotal.ToString());
                    
                    for (int iIdxCol = 0; iIdxCol < nombreCampos.Length; iIdxCol++)
                    {
                        sbJson.Append(", \"" + nombreCampos[iIdxCol] + "\":\"" + dtDatos.Rows[iIdxRow][iIdxCol].ToString().Replace("\r\n", "") + "\"");
                    }

                    if ( iIdxRow +1 == dtDatos.Rows.Count)
                        sbJson.AppendLine("}");
                    else
                        sbJson.AppendLine("},");

                    iRegTotal++;

                }
                sbJson.AppendLine("] }");
            }


            return sbJson.ToString();
        }
        public int Agregar(object oDatosMdl)
        {

            Type typeObj = oDatosMdl.GetType();

            int iIdx = 0;
            String sInsert = "INSERT INTO " + typeObj.Name;
            StringBuilder sbInsCampos = new StringBuilder();
            StringBuilder sbInsValores = new StringBuilder();
            String sTipoLLave;
            int iLLave;
            List<object> lstParam = new List<object>();

            foreach (PropertyInfo info in typeObj.GetProperties())
            {
                if (_dicTabla.ContainsKey(info.Name))
                {
                    // SI ES SECUNECIA LLAMAR A LASECUENCIA Y OBTENER EL DATO
                    sTipoLLave = _dicTabla[info.Name];
                    if (sTipoLLave != CFG_TABLA_IDENTIDAD)
                    {
                        iLLave = SecuenciaDML(sTipoLLave);
                        lstParam.Add(iLLave);
                        sbInsCampos.Append("," + info.Name);
                        sbInsValores.Append("," + _prefijoParam + "P" + iIdx);
                    }
                }
                else
                {
                    sbInsCampos.Append(", " + info.Name);
                    sbInsValores.Append("," + _prefijoParam + "P" + iIdx);
                    lstParam.Add(info.GetValue(oDatosMdl, null));
                    iIdx++;
                }
            }

            String sQuery = sInsert + " (" + sbInsCampos.ToString().Substring(1) + " ) VALUES ( " + sbInsValores.ToString().Substring(1) + ")";

            return (int)EjecutaDML(sQuery, lstParam.ToArray());
        }
    }
}
