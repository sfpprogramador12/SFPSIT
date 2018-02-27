using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Red;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Red
{
    public class RedComiteRubroDao : BaseDao
    {
        int iSecuencia { get; set; }

        public RedComiteRubroDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_HABILITAR] = new Func<Object, object>(dmlHabilitar);
            dicOperacion[OPE_DESHABILITAR] = new Func<Object, object>(dmlDeshabilitar);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos )
        {
            RedComiteRubroMdl dtoDatos = (RedComiteRubroMdl)oDatos;
            iSecuencia = SecuenciaDML("SEC_SIT_KCOMITE_RUBRO");

            String sqlQuery = " insert into SIT_RED_KCOMITE_RUBRO ( RBC_CLACOMITERUBRO, RBC_DESCRIPCION, RBC_FECBAJA ) "
                    + " VALUES ( :P0, :P1, :P2 )";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.rbc_descripcion, dtoDatos.rbc_fecbaja);  
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedComiteRubroMdl dtoDatos = (RedComiteRubroMdl)oDatos;
            String sqlQuery = " update SIT_RED_KCOMITE_RUBRO "
                    + " set RBC_DESCRIPCION = :P0, RBC_FECBAJA = :P1 "
                    + " where RBC_CLACOMITERUBRO = :P2 ";

            return EjecutaDML(sqlQuery, dtoDatos.rbc_descripcion, dtoDatos.rbc_fecbaja, dtoDatos.rbc_clacomiterubro);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedComiteRubroMdl dtoDatos = (RedComiteRubroMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_KCOMITE_RUBRO where RBC_CLACOMITERUBRO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.rbc_clacomiterubro);
        }

        public object dmlHabilitar(Object oDatos)
        {
            RedComiteRubroMdl dtoDatos = (RedComiteRubroMdl)oDatos;
            String sqlQuery = " update SIT_RED_KCOMITE_RUBRO set RBC_FECBAJA = null where RBC_CLACOMITERUBRO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.rbc_fecbaja, dtoDatos.rbc_clacomiterubro);
        }

        public object dmlDeshabilitar(Object oDatos)
        {
            RedComiteRubroMdl dtoDatos = (RedComiteRubroMdl)oDatos;
            String sqlQuery = " update SIT_RED_KCOMITE_RUBRO set RBC_FECBAJA = sysdate where RBC_CLACOMITERUBRO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.rbc_fecbaja, dtoDatos.rbc_clacomiterubro);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedComiteRubroMdl> lstDatos = (List<RedComiteRubroMdl>)oDatos;

            String sqlQuery = "insert into SIT_RED_KCOMITE_RUBRO ( RBC_CLACOMITERUBRO, RBC_DESCRIPCION, RBC_FECBAJA ) "
                    + " VALUES ( :P0, :P1, null )";

            foreach (RedComiteRubroMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.rbc_clacomiterubro, dtoDatos.rbc_descripcion);
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
                            + " SELECT RBC_CLACOMITERUBRO, RBC_DESCRIPCION, RBC_FECBAJA "
                + " from SIT_RED_KCOMITE_RUBRO order by RBC_CLACOMITERUBRO "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select RBC_CLACOMITERUBRO AS id , RBC_DESCRIPCION as text FROM SIT_RED_KCOMITE_RUBRO WHERE RBC_CLACOMITERUBRO > 0 ORDER BY RBC_CLACOMITERUBRO";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select RBC_CLACOMITERUBRO, RBC_DESCRIPCION FROM SIT_RED_KCOMITE_RUBRO ORDER BY RBC_CLACOMITERUBRO";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add( Convert.ToInt32(row["RBC_CLACOMITERUBRO"]), row["RBC_DESCRIPCION"].ToString());
            }
            return dicParametros;
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
