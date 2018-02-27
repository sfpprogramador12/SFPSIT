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
    public class SolTipoSolicitudDao : BaseDao
    {
        public SolTipoSolicitudDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            SolTipoSolicitudMdl dtoDatos = (SolTipoSolicitudMdl)oDatos;
            string sqlQuery = " insert into SIT_SOL_KTIPO_SOLICITUD ( TSO_CLATIPOSOL, TSO_DESCRIPCION ) "
                    + " VALUES ( :P0, :P1 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.tso_clatiposol, dtoDatos.tso_descripcion );
        }

        private object dmlUpdate(object oDatos)
        {
            SolTipoSolicitudMdl dtoDatos = (SolTipoSolicitudMdl)oDatos;
            string sqlQuery = " update SIT_SOL_KTIPO_SOLICITUD "
                    + " set TSO_DESCRIPCION = :P0 "
                    + " where TSO_CLATIPOSOL = :P1 ";

            return EjecutaDML(sqlQuery, dtoDatos.tso_descripcion, dtoDatos.tso_clatiposol);
        }

        private object dmlDelete(object oDatos)
        {
            SolTipoSolicitudMdl dtoDatos = (SolTipoSolicitudMdl)oDatos;
            string sqlQuery = " delete from SIT_SOL_KTIPO_SOLICITUD where TSO_CLATIPOSOL = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.tso_clatiposol);
        }

        private Object dmlImportar(object oDatos)
        {
            Int16 iContador = 0;
            List<SolTipoSolicitudMdl> lstDatos = (List<SolTipoSolicitudMdl>)oDatos;

            string sqlQuery = " insert into SIT_SOL_KTIPO_SOLICITUD ( TSO_CLATIPOSOL, TSO_DESCRIPCION) VALUES ( :P0, :P1 ) ";

            foreach (SolTipoSolicitudMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.tso_clatiposol, dtoDatos.tso_descripcion);
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
            + " select TSO_CLATIPOSOL, TSO_DESCRIPCION from SIT_SOL_KTIPO_SOLICITUD order by TSO_CLATIPOSOL "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(object oDatos)
        {
            string sqlQuery = " Select TSO_CLATIPOSOL as id, TSO_DESCRIPCION as text FROM SIT_SOL_KTIPO_SOLICITUD ORDER BY TSO_CLATIPOSOL";

            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select TSO_CLATIPOSOL, TSO_CLATIPOSOL from SIT_SOL_KTIPO_SOLICITUD order by TSO_CLATIPOSOL";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["TSO_CLATIPOSOL"]), row["TSO_CLATIPOSOL"].ToString());
            }
            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}






