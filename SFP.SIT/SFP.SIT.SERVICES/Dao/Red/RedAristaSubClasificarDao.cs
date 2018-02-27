using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Red;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;


namespace SFP.SIT.SERVICES.Dao.Red
{
    public class RedAristaSubClasificarDao : BaseDao
    {

        public RedAristaSubClasificarDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            RedAristaSubClasificarMdl dtoDatos = (RedAristaSubClasificarMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_RED_ARISTA_SUBCLASIFICAR (us_clafolio, NRE_CLAARISTA,  KAR_CLATIPOARI )"
                    + " VALUES ( :P0, :P1, :P2) ";
        
            return EjecutaDML(sqlQuery, dtoDatos.us_claFolio, dtoDatos.nre_claarista, dtoDatos.kar_clatipoari);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedAristaSubClasificarMdl dtoDatos = (RedAristaSubClasificarMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_ARISTA_SUBCLASIFICAR where us_clafolio= :P0 AND  NRE_CLAARISTA = :P1 AND KAR_CLATIPOARI = :P2 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_claFolio, dtoDatos.nre_claarista, dtoDatos.kar_clatipoari);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + "SELECT us_clafolio, NRE_CLAARISTA,  KAR_CLATIPOARI from SIT_RED_ARISTA_SUBCLASIFICAR order by us_clafolio, NRE_CLAARISTA,  KAR_CLATIPOARI"
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
