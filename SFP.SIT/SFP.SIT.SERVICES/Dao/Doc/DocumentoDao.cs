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
    public class DocumentoDao : BaseDao
    {
        public const int OPE_SELECT_DOC_FOLIO = 211;
        public const int OPE_SELECT_DOC_NOMBRE_MD5 = 212;
        public const int OPE_SELECT_DOC_ID = 213;
        public const int OPE_SELECT_DOC_INICIAL = 214;

        public const string COL_US_CLAFOLIO = "US_CLAFOLIO";
        public const string COL_DOC_CLADOC = "DOC_CLADOC";
        public const string COL_DOC_NOMBRE = "DOC_NOMBRE";
        public const string COL_DOC_SIZE = "DOC_SIZE";
        public const string COL_DOC_MD5 = "DOC_MD5";

        public int iSecuencia { get; set; }

        public DocumentoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            dicOperacion[OPE_SELECT_DOC_FOLIO] = new Func<Object, object>(dmlSelectDocFolio);
            dicOperacion[OPE_SELECT_DOC_NOMBRE_MD5] = new Func<Object, object>(dmlSelectDocNombreMD5);
            dicOperacion[OPE_SELECT_DOC_ID] = new Func<Object, object>(dmlSelectDocID);
            dicOperacion[OPE_SELECT_DOC_INICIAL] = new Func<Object, object>(dmlSelectDocInicial);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            DocumentoMdl dtoDatos = (DocumentoMdl)oDatos;
            iSecuencia = SecuenciaDML("SEC_SIT_DOCUMENTO");

            String sqlQuery = ""
                    + " insert into SIT_DOCUMENTO ( DOC_CLADOC, DOC_FECHA, DOC_FOLIO, DOC_NOMBRE,  "
                    + " DOC_SIZE, DOC_RUTA,  KTE_CLAEXT, DOC_FILESYSTEM, DOC_URL, DOC_MD5 )"
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9 ) ";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.doc_fecha, dtoDatos.doc_folio, dtoDatos.doc_nombre, 
                dtoDatos.doc_size, dtoDatos.doc_ruta, dtoDatos.kte_claext, dtoDatos.doc_filesystem, dtoDatos.doc_url,
                dtoDatos.doc_MD5);
        }

        private Object dmlUpdate(Object oDatos)
        {
            DocumentoMdl dtoDatos = (DocumentoMdl)oDatos;
            String sqlQuery = " update SIT_DOCUMENTO "
                    + " set DOC_FECHA= :P0, DOC_FOLIO= :P1, DOC_NOMBRE= :P2, DOC_SIZE= :P3, DOC_RUTA= :P4,  "
                    + " KTE_CLAEXT= :P5, DOC_FILESYSTEM = :P6 , DOC_URL = :P7, DOC_MD5 = :P8 "
                    + " where DOC_CLADOC = :P9 ";

            return EjecutaDML(sqlQuery, dtoDatos.doc_fecha, dtoDatos.doc_folio, dtoDatos.doc_nombre, dtoDatos.doc_size,
                 dtoDatos.doc_ruta, dtoDatos.kte_claext, dtoDatos.doc_filesystem, dtoDatos.doc_url,dtoDatos.doc_MD5, 
                 dtoDatos.doc_cladoc);
        }
        private Object dmlDelete(Object oDatos)
        {
            DocumentoMdl dtoDatos = (DocumentoMdl)oDatos;
            String sqlQuery = " delete from SIT_DOCUMENTO where DOC_CLADOC = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.doc_cladoc);
        }
        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<DocumentoMdl> lstDatos = (List<DocumentoMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_DOCUMENTO ( DOC_CLADOC, DOC_FECHA, DOC_FOLIO, DOC_NOMBRE, DOC_SIZE, DOC_RUTA,  "
                    + " KTE_CLAEXT,  DOC_FILESYSTEM, DOC_URL, DOC_MD5)"
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9) ";

            foreach (DocumentoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.doc_cladoc, dtoDatos.doc_fecha, dtoDatos.doc_folio, dtoDatos.doc_nombre, 
                    dtoDatos.doc_size, dtoDatos.doc_ruta, dtoDatos.kte_claext);
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
                + " SELECT DOC_CLADOC, DOC_FECHA, DOC_FOLIO, DOC_NOMBRE, DOC_SIZE, DOC_RUTA, KTE_CLAEXT,  "
                + " DOC_FILESYSTEM, DOC_URL, DOC_MD5  "
                + " from SIT_DOCUMENTO order by DOC_CLADOC "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select KDC_CLADOC, KDC_DESCRIPCION FROM SIT_DOCUMENTO ORDER BY KDC_CLADOC";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicConsulta = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select KDC_CLADOC, KDC_DESCRIPCION FROM SIT_DOCUMENTO ORDER BY KDC_CLADOC";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicConsulta.Add(Convert.ToInt32(row["KDC_CLADOC"]), row["KDC_DESCRIPCION"].ToString());
            }

            return dicConsulta;
        }

        private DataTable dmlSelectDocFolio(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;

            string sqlQuery = " select doc.DOC_CLADOC as ID , DOC_NOMBRE as text "
                + " from SIT_doc_arista docari, SIT_documento doc "
                + " where us_clafolio = :P0 and docari.doc_cladoc = doc.doc_cladoc";

            return ConsultaDML(sqlQuery, dicParametros[COL_US_CLAFOLIO]);
        }

        private DataTable dmlSelectDocNodo(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;

            string sqlQuery = " select doc.DOC_CLADOC as ID , DOC_NOMBRE as text "
                + " from SIT_doc_arista docari, SIT_documento doc "
                + " where us_clafolio = :P0 and docari.doc_cladoc = doc.doc_cladoc";

            return ConsultaDML(sqlQuery, dicParametros[COL_US_CLAFOLIO]);
        }

        private object dmlSelectDocNombreMD5(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            DataTable dtDatos;
            int iDocumentoID = 0;

            string sqlQuery = " select DOC_CLADOC from SIT_documento doc WHERE doc_nombre like :P0 and doc_MD5 = :P1";
            dtDatos = ConsultaDML(sqlQuery, dicParametros[COL_DOC_NOMBRE], dicParametros[COL_DOC_MD5]);
            foreach (DataRow row in dtDatos.Rows)
            {
                iDocumentoID = Convert.ToInt32(row["DOC_CLADOC"] );
            }

            return iDocumentoID;
        }
        private DataTable dmlSelectDocID(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            string sqlQuery = " SELECT doc_nombre, doc_ruta, kte_mime_type " 
                + " FROM SIT_documento doc, SIT_doc_ktipo_extension ext "
                + " WHERE doc.kte_claext = ext.kte_claext and doc.doc_cladoc = :P0";
            
            return ConsultaDML(sqlQuery, dicParametros[COL_DOC_CLADOC]); ;
        }



        private DataTable dmlSelectDocInicial(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            string sqlQuery = " select doc_nombre from sit_documento where doc_cladoc in ( "
                + " Select doc_cladoc from sit_doc_arista where nre_claarista in ( "
                + " select min(nre_claarista) from sit_red_arista WHERE us_clafolio = :P0 )) ";

            return ConsultaDML(sqlQuery, dicParametros[COL_US_CLAFOLIO]); ;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
