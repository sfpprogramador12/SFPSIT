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
        //public const short OPE_ACTUALIZAR = 6;
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
        public BaseDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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

                        if (propActual.PropertyType == typeof(string))
                        {
                            propActual.SetValue(objMdl, dtDatos.Rows[iIdx][nombreCampos[iIdxCol]].ToString());
                        }
                        else if (propActual.PropertyType == typeof(int) || propActual.GetType() == typeof(Int32) )
                        {
                            if ( dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull )
                                propActual.SetValue(objMdl, 0);
                            else
                                propActual.SetValue(objMdl, Convert.ToInt32(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(int?))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, null);
                            else
                                propActual.SetValue(objMdl, Convert.ToInt32(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(long))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, 0);
                            else
                                propActual.SetValue(objMdl, Convert.ToInt64(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(long?))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, null);
                            else
                                propActual.SetValue(objMdl, Convert.ToInt64(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(short))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, (short)0);
                            else
                                propActual.SetValue(objMdl, Convert.ToInt16(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(short?))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, null);
                            else
                                propActual.SetValue(objMdl, Convert.ToInt16(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(Single))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, 0);
                            else
                                propActual.SetValue(objMdl, Convert.ToSingle(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(Single?))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, null);
                            else
                                propActual.SetValue(objMdl, Convert.ToSingle(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }

                        else if (propActual.PropertyType == typeof(Double))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, 0);
                            else
                                propActual.SetValue(objMdl, Convert.ToDouble(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(Double?))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, null);
                            else
                                propActual.SetValue(objMdl, Convert.ToDouble(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(DateTime))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]].ToString() == "")
                                propActual.SetValue(objMdl, DateTime.MinValue);
                            else
                                propActual.SetValue(objMdl, Convert.ToDateTime(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else if (propActual.PropertyType == typeof(Byte))
                        {
                            if (dtDatos.Rows[iIdx][nombreCampos[iIdxCol]] is DBNull)
                                propActual.SetValue(objMdl, DBNull.Value);
                            else
                                propActual.SetValue(objMdl, Convert.ToByte(dtDatos.Rows[iIdx][nombreCampos[iIdxCol]]));
                        }
                        else
                        {
                            propActual.SetValue(objMdl, dtDatos.Rows[iIdx][nombreCampos[iIdxCol]].ToString());
                        }
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
            int iIdxRow = 0;
            int iIdxCol = 0;

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

                for (iIdxRow = 0; iIdxRow < dtDatos.Rows.Count; iIdxRow++)
                {                    
                    sbJson.Append("{ \"recid\":" + iRegTotal.ToString());
                    
                    for (iIdxCol = 0; iIdxCol < nombreCampos.Length; iIdxCol++)
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
            String sInsCampos = "";
            String sInsValores = "";
            string sTipoLLave = "";
            int iLLave = 0;
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
                        sInsCampos = sInsCampos + "," + info.Name;
                        sInsValores = sInsValores + "," + _prefijoParam + "P" + iIdx;
                    }
                }
                else
                {
                    sInsCampos = sInsCampos + "," + info.Name;
                    sInsValores = sInsValores + "," + _prefijoParam + "P" + iIdx;
                    lstParam.Add(info.GetValue(oDatosMdl, null));
                    iIdx++;
                }
            }

            String sQuery = sInsert + " (" + sInsCampos.Substring(1) + " ) VALUES ( " + sInsValores.Substring(1) + ")";


            return (int)EjecutaDML(sQuery, lstParam.ToArray());
        }

    }
}
