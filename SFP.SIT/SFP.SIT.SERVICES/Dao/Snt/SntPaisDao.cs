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
    public class SntPaisDao : BaseDao
    {
        int iSecuencia { get; set; }

        public SntPaisDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
        private Object dmlInsert(Object odatos)
        {
            SntPaisMdl datosMdl = (SntPaisMdl)odatos;
            iSecuencia = SecuenciaDML("SEC_SIT_KPAIS");

            String sqlQuery = ""
                + " insert into SIT_SNT_KPAIS ( KPA_CLAPAI, KPA_DESCRIPCION, KPA_FECBAJA ) "
                + " VALUES ( :P0, :P1, :P2 ) ";

            return EjecutaDML(sqlQuery, iSecuencia, datosMdl.kpa_descripcion, datosMdl.kpa_fecbaja);
        }

        private Object dmlUpdate(Object odatos)
        {
            SntPaisMdl datosMdl = (SntPaisMdl)odatos;
            String sqlQuery = " update SIT_SNT_KPAIS "
                + " set KPA_DESCRIPCION = :P0, KPA_FECBAJA = :P1 "
                + " where KPA_CLAPAI = :P2 ";

            return EjecutaDML(sqlQuery, datosMdl.kpa_descripcion, datosMdl.kpa_fecbaja, datosMdl.kpa_clapai);
        }

        private Object dmlDelete(Object odatos)
        {
            SntPaisMdl datosMdl = (SntPaisMdl)odatos;
            String sqlQuery = " delete from SIT_SNT_KPAIS where KPA_CLAPAI = :P0 ";
            return EjecutaDML(sqlQuery, datosMdl.kpa_clapai);
        }

        private Object dmlHabilitar(Object odatos)
        {
            SntPaisMdl datosMdl = (SntPaisMdl)odatos;
            String sqlQuery = " update SIT_SNT_KPAIS set KPA_FECBAJA = null where KPA_CLAPAI = :P0 ";
            return EjecutaDML(sqlQuery, datosMdl.kpa_clapai);
        }

        private Object dmlDeshabilitar(Object odatos)
        {
            SntPaisMdl datosMdl = (SntPaisMdl)odatos;
            String sqlQuery = " update SIT_SNT_KPAIS set KPA_FECBAJA = sysdate where KPA_CLAPAI = :P0 ";
            return EjecutaDML(sqlQuery, datosMdl.kpa_clapai);
        }

        private Object dmlImportar(Object odatos)
        {
            Int16 iContador = 0;
            List<SntPaisMdl> lstDatos = (List<SntPaisMdl>)odatos;

            String sqlQuery = ""
                + " insert into SIT_SNT_KPAIS ( KPA_CLAPAI, KPA_DESCRIPCION, KPA_FECBAJA ) "
                + " VALUES ( :P0, :P1, NULL ) ";

            foreach (SntPaisMdl dtoDatos in lstDatos)
            {
                 EjecutaDML(sqlQuery, dtoDatos.kpa_clapai, dtoDatos.kpa_descripcion);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl) oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT KPA_CLAPAI, KPA_DESCRIPCION, KPA_FECBAJA   "
                + " from SIT_SNT_KPAIS "
                + " order by KPA_DESCRIPCION "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object odatos)
        {
            String sqlQuery = " Select KPA_CLAPAI as id, KPA_DESCRIPCION as text FROM SIT_SNT_KPAIS where KPA_FECBAJA IS NULL ORDER BY KPA_DESCRIPCION";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object odatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select KPA_CLAPAI, KPA_DESCRIPCION FROM SIT_SNT_KPAIS where KPA_FECBAJA IS NULL ORDER BY KPA_DESCRIPCION";
            dtDatos = (DataTable)ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["KPA_CLAPAI"]), row["KPA_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}