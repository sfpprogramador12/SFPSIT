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
    public class SntTipoSolicitanteDao : BaseDao
    {
        int iSecuencia { get; set; }

        public SntTipoSolicitanteDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
        private Object dmlInsert(Object oDatos)
        {
            SntTipoSolicitanteMdl dtoDatos = (SntTipoSolicitanteMdl)oDatos;
            String sqlQuery = ""
                + " insert into SIT_SNT_KTIPO_SOLICITANTE ( TSL_CLATIPOSOLTE, TSL_DESCRIPCION ) "
                + " VALUES ( :P0 , :P1 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.tsl_clatiposolte, dtoDatos.tsl_descripcion);
        }

        private Object dmlUpdate(Object oDatos)
        {
            SntTipoSolicitanteMdl dtoDatos = (SntTipoSolicitanteMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KTIPO_SOLICITANTE "
                + " set TSL_DESCRIPCION = :P0 "
                + " where TSL_CLATIPOSOLTE = :P1 ";

            return EjecutaDML(sqlQuery, dtoDatos.tsl_descripcion, dtoDatos.tsl_clatiposolte);
        }

        private Object dmlDelete(Object oDatos)
        {
            SntTipoSolicitanteMdl dtoDatos = (SntTipoSolicitanteMdl)oDatos;
            String sqlQuery = " delete from SIT_SNT_KTIPO_SOLICITANTE where TSL_CLATIPOSOLTE = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.tsl_clatiposolte);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<SntTipoSolicitanteMdl> lstDatos = (List<SntTipoSolicitanteMdl>)oDatos;

            String sqlQuery = ""
                + " insert into SIT_SNT_KTIPO_SOLICITANTE ( TSL_CLATIPOSOLTE, TSL_DESCRIPCION ) "
                + " VALUES ( :P0 , :P1 ) ";

            foreach (SntTipoSolicitanteMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.tsl_clatiposolte, dtoDatos.tsl_descripcion);
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
                            + " SELECT TSL_CLATIPOSOLTE, TSL_DESCRIPCION  "
               + " from SIT_SNT_KTIPO_SOLICITANTE "
                + " order by TSL_CLATIPOSOLTE "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select TSL_CLATIPOSOLTE as id, TSL_DESCRIPCION as text FROM SIT_SNT_KTIPO_SOLICITANTE ORDER BY TSL_CLATIPOSOLTE";
            return ConsultaDML(sqlQuery);
        }

        private Object dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select TSL_CLATIPOSOLTE, TSL_DESCRIPCION FROM SIT_SNT_KTIPO_SOLICITANTE ORDER BY TSL_CLATIPOSOLTE";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add( Convert.ToInt32(row["TSL_CLATIPOSOLTE"]), row["TSL_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}