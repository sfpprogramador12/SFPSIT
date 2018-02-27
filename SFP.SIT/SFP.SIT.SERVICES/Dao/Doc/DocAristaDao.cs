using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Doc;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Doc
{
    public class DocAristaDao : BaseDao
    {
        public DocAristaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
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
            DocAristaMdl dtoDatos = (DocAristaMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_DOC_ARISTA ( DOC_CLADOC, US_CLAFOLIO, NRE_CLAARISTA) "
                    + " VALUES ( :P0, :P1, :P2 ) ";
            return EjecutaDML(sqlQuery, dtoDatos.doc_cladoc, dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }

        private Object dmlDelete(Object oDatos)
        {
            DocAristaMdl dtoDatos = (DocAristaMdl)oDatos;
            String sqlQuery = " delete from SIT_DOC_ARISTA where DOC_CLADOC= :P0 AND US_CLAFOLIO= :P1 AND NRE_CLAARISTA = :P2 ";
            return EjecutaDML(sqlQuery, dtoDatos.doc_cladoc, dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }


        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<DocAristaMdl> lstDatos = (List<DocAristaMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_DOC_ARISTA ( DOC_CLADOC, US_CLAFOLIO, NRE_CLAARISTA) "
                    + " VALUES ( :P0, :P1, :P2 ) ";

            foreach (DocAristaMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.doc_cladoc, dtoDatos.us_clafolio, dtoDatos.nre_claarista);
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
                + "SELECT DA.DOC_CLADOC, US_CLAFOLIO, NRE_CLAARISTA, DOC_NOMBRE  "
                + " from SIT_DOC_ARISTA DA, SIT_DOCUMENTO DOC "
                + " WHERE DOC.DOC_CLADOC = DA.DOC_CLADOC "
                + " order by US_CLAFOLIO, NRE_CLAARISTA "
                +" ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }

}
