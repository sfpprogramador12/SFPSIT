using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.SIT.SERVICES.Model.Snt;
using SFP.Persistencia;
using System.Data;
using SFP.SIT.SERVICES.Model;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Snt
{
    public class SntEstadoDao : BaseDao
    {
        public const int OPE_IMPORTAR_COMPLETAR = 211;
        public const String COL_CLAPAI = "KPA_CLAPAI";

        int iSecuencia { get; set; }

        public SntEstadoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_HABILITAR] = new Func<Object, object>(dmlHabilitar);
            dicOperacion[OPE_DESHABILITAR] = new Func<Object, object>(dmlDeshabilitar);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);
            dicOperacion[OPE_IMPORTAR_COMPLETAR] = new Func<Object, object>(dmlImportarCompletar);


            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_TABLE] = new Func<Object, object>(dmlSelectTable);
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);           
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos) 
        {
            SntEstadoMdl dtoDatos = (SntEstadoMdl)oDatos;
            iSecuencia = SecuenciaDML("SEC_SIT_KESTADO");

            String sqlQuery = ""
                + " insert into SIT_SNT_KESTADO ( KE_CLAEST, KPA_CLAPAI, KE_DESCRIPCION, KE_FECBAJA ) "
                + " VALUES ( :P0, :P1, :P2, :P3 ) ";

            return EjecutaDML(sqlQuery,  iSecuencia, dtoDatos.kpa_clapai, dtoDatos.ke_descripcion, dtoDatos.ke_fecbaja);
        }

        private Object dmlUpdate(Object oDatos) 
        {
            SntEstadoMdl dtoDatos = (SntEstadoMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KESTADO "
                + " set  KE_DESCRIPCION = :P0, KE_FECBAJA = :P1, KPA_CLAPAI = :P2 "
                + " where  KE_CLAEST = :P3 ";
            return EjecutaDML(sqlQuery, dtoDatos.ke_descripcion, dtoDatos.ke_fecbaja, dtoDatos.kpa_clapai, dtoDatos.ke_claest);
        }

        private Object dmlDelete(Object oDatos) 
        {
            SntEstadoMdl dtoDatos = (SntEstadoMdl)oDatos;
            String sqlQuery = " delete from SIT_SNT_KESTADO where KPA_CLAPAI = :P0 AND KE_CLAEST = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.kpa_clapai, dtoDatos.ke_claest);
        }

        private Object dmlHabilitar(Object oDatos) 
        {
            SntEstadoMdl dtoDatos = (SntEstadoMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KESTADO set KPA_FECBAJA = null where KPA_CLAPAI = :P0 AND KE_CLAEST = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.kpa_clapai, dtoDatos.ke_claest);
        }

        private Object dmlDeshabilitar(Object oDatos) 
        {
            SntEstadoMdl dtoDatos = (SntEstadoMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KESTADO set KPA_FECBAJA = sysdate where KPA_CLAPAI = :P0 AND KE_CLAEST = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.kpa_clapai, dtoDatos.ke_claest);
        }

        private Object dmlImportar(Object oDatos) 
        {            
            Int16 iContador = 0;
            List<SntEstadoMdl> lstDatos = (List<SntEstadoMdl>)oDatos;

            String sqlQuery = ""
                + " insert into SIT_SNT_KESTADO ( KE_CLAEST, KPA_CLAPAI, KE_DESCRIPCION, KE_FECBAJA ) "
                + " VALUES ( :P0, :P1, :P2, NULL ) ";

            foreach (SntEstadoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.ke_claest, dtoDatos.kpa_clapai, dtoDatos.ke_descripcion);
                iContador++;
            }
            return iContador;
        }
        private Object dmlImportarCompletar(Object oDatos) 
        {
            SntEstadoMdl dtoDatos = (SntEstadoMdl)oDatos;
            String sqlQuery = ""
                + " Insert into SIT_SNT_kestado (KE_CLAEST, ke_descripcion, KPA_CLAPAI)  "
                + " select 33, 'Extranjero', kpa_clapai from SIT_snt_kpais "
                + " where not kpa_clapai  in ( SELECT kpa_clapai FROM  SIT_SNT_KESTADO)";         

            return EjecutaDML(sqlQuery);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   

        private DataTable dmlSelectTable(Object oDatos) 
        {
            String sqlQuery = "SELECT KE_CLAEST, P.KPA_CLAPAI, P.KPA_DESCRIPCION, KE_DESCRIPCION, KE_FECBAJA   "
            + " from SIT_SNT_KESTADO E, SIT_SNT_KPAIS P WHERE  P.KPA_CLAPAI = E.KPA_CLAPAI "
            + " order by KPA_CLAPAI, KE_CLAEST ";
            return ConsultaDML(sqlQuery);
        }
    
        private DataTable dmlSelectGrid(Object oDatos) 
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT KE_CLAEST, EDO.KPA_CLAPAI, KPA_DESCRIPCION, KE_DESCRIPCION,  KE_FECBAJA  "
            + " FROM SIT_SNT_KESTADO EDO, SIT_SNT_KPAIS PAIS "
            + " WHERE EDO.KPA_CLAPAI = PAIS.KPA_CLAPAI "
            + " ORDER BY KPA_CLAPAI, KE_CLAEST "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);

        }

        private DataTable dmlSelectCombo(Object oDatos) 
        {        
            String sqlQuery = " Select KE_CLAEST as id, KE_DESCRIPCION as text FROM SIT_SNT_KESTADO where KE_FECBAJA IS NULL ORDER BY KPA_CLAPAI";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select KE_CLAEST, KE_DESCRIPCION FROM SIT_SNT_KESTADO where KE_FECBAJA IS NULL ORDER BY KPA_CLAPAI";
            dtDatos = (DataTable)ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["KE_CLAEST"]), row["KE_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}