using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Adm;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Adm
{
    public class AdmConfiguracionDao : BaseDao
    {
        public const int OPE_SELECT_CLAVE = 211;

        public const string COL_CON_CLAVE = "CON_CLAVE";


        public AdmConfiguracionDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
            dicOperacion[OPE_SELECT_CLAVE] = new Func<Object, object>(dmlSelectClave);            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                          
        private Object dmlInsert(Object oDatos)
        {
            AdmConfiguracionMdl dtoDatos = (AdmConfiguracionMdl)oDatos;
            String sqlQuery = " insert into SIT_ADM_KCONFIGURACION ( CON_CLACONFIGURACION, CON_CLAVE, CON_VALOR, CON_FECBAJA ) VALUES ( :P0, :P1, :P2, :P3 )";
            return EjecutaDML(sqlQuery, dtoDatos.con_claconfiguracion, dtoDatos.con_clave, dtoDatos.con_valor, dtoDatos.con_fecbaja);
        }

        private Object dmlUpdate(Object oDatos)
        {
            AdmConfiguracionMdl dtoDatos = (AdmConfiguracionMdl) oDatos;
            String sqlQuery = " update SIT_ADM_KCONFIGURACION "
                    + " set CON_CLAVE= :P0, CON_VALOR= :P1, CON_FECBAJA = :P2 "
                    + " where CON_CLACONFIGURACION = :P3 ";

            return EjecutaDML(sqlQuery, dtoDatos.con_clave, dtoDatos.con_valor, dtoDatos.con_fecbaja, dtoDatos.con_claconfiguracion);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmConfiguracionMdl dtoDatos = (AdmConfiguracionMdl) oDatos;
            String sqlQuery = " delete from SIT_ADM_KCONFIGURACION where CON_CLACONFIGURACION = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.con_claconfiguracion);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmConfiguracionMdl> lstDatos = (List<AdmConfiguracionMdl>)oDatos;

            String sqlQuery = " insert into SIT_ADM_KCONFIGURACION ( CON_CLACONFIGURACION, CON_CLAVE, CON_VALOR, CON_FECBAJA ) VALUES ( :P0, :P1, :P2, :P3 )";

            foreach (AdmConfiguracionMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.con_claconfiguracion, dtoDatos.con_clave, dtoDatos.con_valor, dtoDatos.con_fecbaja);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////         
        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT CON_CLACONFIGURACION, CON_CLAVE, CON_VALOR, CON_FECBAJA from SIT_ADM_KCONFIGURACION "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";

            return (DataTable) ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = "SELECT CON_CLAVE, CON_VALOR from SIT_ADM_KCONFIGURACION ";
            dtDatos = (DataTable) ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["CON_CLAVE"]), row["CON_VALOR"].ToString());
            }

            return dicParametros;
        }

        private String dmlSelectClave(Object oDatos)
        {
            Dictionary<String, object > dicParametros = (Dictionary<String, object>) oDatos;
            DataTable dtDatos;
            String sClave = "";

            String sqlQuery = "SELECT CON_VALOR from SIT_ADM_KCONFIGURACION WHERE CON_CLAVE = :P0 ";
            dtDatos = (DataTable)ConsultaDML(sqlQuery, dicParametros[COL_CON_CLAVE]);
            foreach (DataRow row in dtDatos.Rows)
            {
                sClave = row["CON_VALOR"].ToString();
            }

            return sClave;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
