using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.SIT.SERVICES.Model.Sol;
using SFP.Persistencia;
using System.Data;
using SFP.SIT.SERVICES.Model;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Sol
{
    public class SolUEnlaceDao : BaseDao
    {
        public SolUEnlaceDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
        private object dmlInsert(object oDatos)
        {
            SolUEnlaceMdl dtoDatos = (SolUEnlaceMdl)oDatos;

            String sqlQuery = ""
                + " insert into SIT_SOL_KU_ENLACE ( US_UNIENL, ENL_DESCRIPCION, ENL_FECBAJA ) "
                + " VALUES ( :P0, :P1, :P2 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.us_unienl, dtoDatos.enl_descripcion, dtoDatos.enl_fecbaja);
        }

        private object dmlUpdate(object oDatos)
        {
            SolUEnlaceMdl dtoDatos = (SolUEnlaceMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KU_ENLACE set ENL_DESCRIPCION = :P0,  ENL_FECBAJA = :P1 where US_UNIENL = :P2 ";
            return EjecutaDML(sqlQuery, dtoDatos.enl_descripcion,  dtoDatos.enl_fecbaja, dtoDatos.us_unienl);
        }

        private object dmlDelete(object oDatos)
        {
            SolUEnlaceMdl dtoDatos = (SolUEnlaceMdl)oDatos;
            String sqlQuery = " delete from SIT_SOL_KU_ENLACE where US_UNIENL = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_unienl);
        }

        private object dmlHabilitar(object oDatos)
        {
            SolUEnlaceMdl dtoDatos = (SolUEnlaceMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KU_ENLACE set ENL_FECBAJA = null where US_UNIENL = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_unienl);
        }

        private object dmlDeshabilitar(object oDatos)
        {
            SolUEnlaceMdl dtoDatos = (SolUEnlaceMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KU_ENLACE set ENL_FECBAJA = sysdate where US_UNIENL = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_unienl);
        }

        private object dmlImportar(object oDatos)
        {
            Int16 iContador = 0;
            List<SolUEnlaceMdl> lstDatos = (List<SolUEnlaceMdl>)oDatos;

            String sqlQuery = ""
                + " insert into SIT_SOL_KU_ENLACE ( US_UNIENL, ENL_DESCRIPCION, ENL_FECBAJA ) "
                + " VALUES ( :P0, :P1, NULL ) ";

            foreach (SolUEnlaceMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.us_unienl, dtoDatos.enl_descripcion);
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
                + " SELECT US_UNIENL, ENL_DESCRIPCION, ENL_FECBAJA"
                + " from SIT_SOL_KU_ENLACE "
                + " order by US_UNIENL "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(object oDatos)
        {
            String sqlQuery = " Select US_UNIENL as id, ENL_DESCRIPCION as text FROM SIT_SOL_KU_ENLACE where ENL_FECBAJA IS NULL ORDER BY US_UNIENL";
            return ConsultaDML(sqlQuery);
        }

        private Object dmlSelectHashMap(object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select US_UNIENL, ENL_DESCRIPCION FROM SIT_SOL_KU_ENLACE where ENL_FECBAJA IS NULL ORDER BY US_UNIENL";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["US_UNIENL"]), row["ENL_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
