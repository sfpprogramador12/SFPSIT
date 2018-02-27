using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Sol;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Sol
{
    public class SolProcesoDao : BaseDao
    {
        public SolProcesoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private object dmlInsert(object oDatos)
        {
            SolProcesoMdl dtoDatos = (SolProcesoMdl)oDatos;
            string sqlQuery = " insert into SIT_SOL_KPROCESO ( krp_claproceso, KRP_DESCRIPCION ) "
                    + " VALUES ( :P0, :P1 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.krp_claproceso, dtoDatos.krp_descripcion);
        }

        private object dmlUpdate(object oDatos)
        {
            SolProcesoMdl dtoDatos = (SolProcesoMdl)oDatos;
            string sqlQuery = " update SIT_SOL_KPROCESO "
                    + " set KRP_DESCRIPCION = :P0 "
                    + " where krp_claproceso = :P1 ";

            return EjecutaDML(sqlQuery, dtoDatos.krp_descripcion, dtoDatos.krp_claproceso);
        }

        private object dmlDelete(object oDatos)
        {
            SolProcesoMdl dtoDatos = (SolProcesoMdl)oDatos;
            string sqlQuery = " delete from SIT_SOL_KPROCESO where krp_claproceso = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.krp_claproceso);
        }
        private Object dmlImportar(object oDatos)
        {
            Int16 iContador = 0;
            List<SolProcesoMdl> lstDatos = (List<SolProcesoMdl>)oDatos;

            string sqlQuery = " insert into SIT_SOL_KPROCESO ( krp_claproceso, KRP_DESCRIPCION) VALUES ( :P0, :P1 ) ";

            foreach (SolProcesoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.krp_claproceso, dtoDatos.krp_descripcion);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
        private DataTable dmlSelectGrid(object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + "SELECT krp_claproceso, KRP_DESCRIPCION  from SIT_SOL_KPROCESO order by krp_claproceso "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(object oDatos)
        {
            string sqlQuery = " Select krp_claproceso as id, KRP_DESCRIPCION as text FROM SIT_SOL_KPROCESO ORDER BY krp_claproceso";

            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select krp_claproceso, krp_claproceso FROM SIT_SOL_KPROCESO ORDER BY krp_claproceso";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["krp_claproceso"]), row["krp_claproceso"].ToString());
            }
            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
