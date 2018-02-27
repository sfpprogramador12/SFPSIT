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
    public class RedTipoInfoDao : BaseDao
    {
        int iSecuencia { get; set; }

        public RedTipoInfoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            RedTipoInfoMdl dtoDatos = (RedTipoInfoMdl)oDatos;
            String sqlQuery = " insert into SIT_RED_KTIPO_INFO ( TPI_CLATIPO_INFO, TPI_DESCRIPCION ) "
                    + " VALUES ( :P0 , :P1 ) ";
            return EjecutaDML(sqlQuery, dtoDatos.tpi_clatipo_info, dtoDatos.tpi_descripcion);
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedTipoInfoMdl dtoDatos = (RedTipoInfoMdl)oDatos;
            String sqlQuery = " update SIT_RED_KTIPO_INFO "
                    + " set TPI_DESCRIPCION = :P0 "
                    + " where TPI_CLATIPO_INFO = :P1 ";

            return EjecutaDML(sqlQuery, dtoDatos.tpi_descripcion, dtoDatos.tpi_clatipo_info);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedTipoInfoMdl dtoDatos = (RedTipoInfoMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_KTIPO_INFO where TPI_CLATIPO_INFO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.tpi_clatipo_info);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedTipoInfoMdl> lstDatos = (List<RedTipoInfoMdl>)oDatos;

            String sqlQuery = " insert into SIT_RED_KTIPO_INFO ( TPI_CLATIPO_INFO, TPI_DESCRIPCION ) "
                    + " VALUES ( :P0 , :P1 ) ";

            foreach (RedTipoInfoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.tpi_clatipo_info, dtoDatos.tpi_descripcion);
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
                            + " SELECT TPI_CLATIPO_INFO, TPI_DESCRIPCION   from SIT_RED_KTIPO_INFO  order by TPI_CLATIPO_INFO  "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select TPI_CLATIPO_INFO as id, TPI_DESCRIPCION as text FROM SIT_RED_KTIPO_INFO ORDER BY TPI_CLATIPO_INFO";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select TPI_CLATIPO_INFO, TPI_DESCRIPCION FROM SIT_RED_KTIPO_INFO ORDER BY TPI_CLATIPO_INFO";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["TPI_CLATIPO_INFO"]), row["TPI_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
