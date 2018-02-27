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
    public class RedAristaResolucionDao : BaseDao
    {
        public RedAristaResolucionDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);
            

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            RedAristaResolucionMdl dtoDatos = (RedAristaResolucionMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_RED_ARISTA_RESOLUCION (US_CLAFOLIO, NRE_CLAARISTA,  TPI_CLATIPO_INFO,"
                    + " RSL_UBICACION, RSL_TAM_CANT_DIR, RSL_ART7 ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5) ";

            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nre_claarista, dtoDatos.tpi_clatipo_info, 
                dtoDatos.rsl_ubicacion, dtoDatos.rsl_tam_cant_dir,  dtoDatos.rsl_art7 );
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedAristaResolucionMdl dtoDatos = (RedAristaResolucionMdl)oDatos;
            String sqlQuery = " update  SIT_RED_ARISTA_RESOLUCION"
                    + " SET tpi_clatipo_info = :P0,  rsl_ubicacion = :P1,  rsl_tam_cant_dir = :P2, "
                    + " rsl_art7  = :P3 "
                    + " where us_clafolio= :P4 AND  nre_claarista = :P5 ";

            return EjecutaDML(sqlQuery,
                    dtoDatos.tpi_clatipo_info, dtoDatos.rsl_ubicacion, dtoDatos.rsl_tam_cant_dir,
                    dtoDatos.rsl_art7, dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedAristaResolucionMdl dtoDatos = (RedAristaResolucionMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_ARISTA_RESOLUCION where us_clafolio= :P0 AND  nre_claarista = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedAristaResolucionMdl> lstDatos = (List<RedAristaResolucionMdl>)oDatos;

            String sqlQuery = " update  SIT_RED_ARISTA_RESOLUCION"
                    + " SET tpi_clatipo_info = :P0, rsl_ubicacion = :P1,"
                    + " rsl_tam_cant_dir = :P2, rsl_art7  = :P3"
                    + " where us_clafolio= :P4 AND  nre_claarista = :P5 ";

            foreach (RedAristaResolucionMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery,
                    dtoDatos.tpi_clatipo_info, dtoDatos.rsl_ubicacion, dtoDatos.rsl_tam_cant_dir,   
                    dtoDatos.rsl_art7, dtoDatos.us_clafolio, dtoDatos.nre_claarista);
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
                            + "SELECT US_CLAFOLIO, NRE_CLAARISTA, TPI_CLATIPO_INFO, RSL_UBICACION, RSL_TAM_CANT_DIR, RSL_ART7 "
                    + " from SIT_RED_ARISTA_RESOLUCION ORDER BY us_clafolio, nre_claarista"
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
