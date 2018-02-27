using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.SIT.SERVICES.Model.Red;
using SFP.Persistencia;
using System.Data;
using SFP.SIT.SERVICES.Model;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Red
{
    public class RedAfdFlujoDao : BaseDao
    {
        public const string PARAM_COL_AFD_CLAAFD = "AFD_CLAAFD";
        public const string PARAM_COL_KNE_ORIGEN = "KNE_ORIGEN";

        public const short OPE_SELECT_PROD_NODO = 211;
        public RedAfdFlujoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S             
            dicOperacion[OPE_SELECT_TABLE] = new Func<Object, object>(dmlSelectTable);
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_PROD_NODO] = new Func<Object, object>(dmlSelectProdNodo);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                          

        private Object dmlInsert(Object oDatos)
        {
            RedAfdFlujoMdl dtoDatos = (RedAfdFlujoMdl)oDatos;

            String sqlQuery = " insert into SIT_RED_AFD_FLUJO ( AFD_CLAAFD, KNE_ORIGEN, KAR_CLATIPOARI, KNE_DESTINO, AFF_PLAZO ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.afd_claAfd , dtoDatos.kne_origen, dtoDatos.kar_clatipoari, dtoDatos.kne_destino, dtoDatos.aff_plazo);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedAfdFlujoMdl dtoDatos = (RedAfdFlujoMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_AFD_FLUJO where AFD_CLAAFD = :P0 AND KNE_ORIGEN = :P1 "
                + " AND KAR_CLATIPOARI = :P2 AND KNE_DESTINO = :P3 ";
            return EjecutaDML(sqlQuery, dtoDatos.afd_claAfd, dtoDatos.kne_origen, dtoDatos.kar_clatipoari, dtoDatos.kne_destino);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedAfdFlujoMdl> lstDatos = (List<RedAfdFlujoMdl>)oDatos;

            String sqlQuery = " insert into SIT_RED_AFD_FLUJO ( AFD_CLAAFD, KNE_ORIGEN, KAR_CLATIPOARI, KNE_DESTINO, AFF_PLAZO ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4 ) ";

            foreach (RedAfdFlujoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.afd_claAfd, dtoDatos.kne_origen, dtoDatos.kar_clatipoari, dtoDatos.kne_destino, dtoDatos.aff_plazo);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     
        private DataTable dmlSelectTable(Object oDatos)
        {
            String sqlQuery = " SELECT AFD_CLAAFD, KNE_ORIGEN, KAR_CLATIPOARI, KNE_DESTINO, AFF_PLAZO  from SIT_RED_AFD_FLUJO ";
            return (DataTable)ConsultaDML(sqlQuery);
        }

        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + "  SELECT AFD_CLAAFD, KNE_ORIGEN, PORI.KP_DESCRIPCION AS ORIGENAREA, ORI.KNE_DESCRIPCION AS ORIGENACCION,  ARI.KAR_CLATIPOARI, KAR_DESCRIPCION,  "
                + "  KNE_DESTINO,  PDES.KP_DESCRIPCION as DESTAREA, DES.KNE_DESCRIPCION as DESTACCION, AFF_PLAZO "
                + "  FROM SIT_RED_AFD_FLUJO FLU, SIT_RED_KTIPO_ARISTA ARI, SIT_RED_KNODO_ESTADO ORI, SIT_RED_KNODO_ESTADO DES,  "
                + "  SIT_ADM_KPERFIL PORI, SIT_ADM_KPERFIL PDES "
                + "  WHERE ARI.KAR_CLATIPOARI = FLU.KAR_CLATIPOARI "
                + "  AND ORI.KNE_CLANODO_EDO = KNE_ORIGEN "
                + "  AND DES.KNE_CLANODO_EDO = KNE_DESTINO "
                + "  AND PORI.KP_CLAPERFIL = ORI.KP_CLAPERFIL "
                + "  AND PDES.KP_CLAPERFIL = DES.KP_CLAPERFIL "
                + "  ORDER BY  ORI.KP_CLAPERFIL, ORI.KNE_DESCRIPCION, KAR_CLATIPOARI "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectProdNodo(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>) oDatos;

            String sqlQuery = " SELECT AFD_CLAAFD, KNE_ORIGEN, ARI.KAR_CLATIPOARI, ari.kar_descripcion, KNE_DESTINO, AFF_PLAZO, KAR_FORMA, KAR_NIVEL, KAR_FORMATO, KNE_URL " +
            " FROM SIT_RED_AFD_FLUJO FLU, SIT_RED_KTIPO_ARISTA ARI, SIT_RED_KNODO_ESTADO NODO " +
            " WHERE FLU.AFD_CLAAFD = :P0 AND FLU.KNE_ORIGEN = :P1 AND flu.kar_clatipoari = ARI.KAR_CLATIPOARI AND NODO.KNE_CLANODO_EDO = KNE_ORIGEN ";

            return ConsultaDML(sqlQuery, dicParam[PARAM_COL_AFD_CLAAFD], dicParam[PARAM_COL_KNE_ORIGEN]);
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}