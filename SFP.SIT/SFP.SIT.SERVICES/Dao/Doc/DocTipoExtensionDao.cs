using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.SIT.SERVICES.Model.Doc;
using SFP.Persistencia;
using System.Data;
using SFP.SIT.SERVICES.Model;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Doc
{
    public class DocTipoExtensionDao : BaseDao
    {
        int iSecuencia { get; set; }

        public const Int32 OPE_SELECT_DIC_TIPOEXTENSION = 211;

        public DocTipoExtensionDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            dicOperacion[OPE_SELECT_DIC_TIPOEXTENSION] = new Func<Object, object>(dmlSelectDicTipoExtension);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            iSecuencia = SecuenciaDML("SEC_SIT_KTIPO_EXTENSION");
            DocTipoExtensionMdl dtoDatos = (DocTipoExtensionMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_DOC_KTIPO_EXTENSION ( KTE_CLAEXT, KTE_EXTENSION, KTE_MIME_TYPE) "
                    + " VALUES ( :P0, :P1, :P2  ) ";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.kte_extension, dtoDatos.kte_mime_type);
        }

        private Object dmlUpdate(Object oDatos)
        {
            DocTipoExtensionMdl dtoDatos = (DocTipoExtensionMdl)oDatos;
            String sqlQuery = " update SIT_DOC_KTIPO_EXTENSION "
                    + " set KTE_EXTENSION = :P0, KTE_MIME_TYPE = :P1 "
                    + " where KTE_CLAEXT = :P2 ";

            return EjecutaDML(sqlQuery, dtoDatos.kte_extension, dtoDatos.kte_mime_type, dtoDatos.kte_claext);
        }

        private Object dmlDelete(Object oDatos)
        {
            DocTipoExtensionMdl dtoDatos = (DocTipoExtensionMdl)oDatos;
            String sqlQuery = " delete from SIT_DOC_KTIPO_EXTENSION where KTE_CLAEXT = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kte_claext);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<DocTipoExtensionMdl> lstDatos = (List<DocTipoExtensionMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_DOC_KTIPO_EXTENSION ( KTE_CLAEXT, KTE_EXTENSION, KTE_MIME_TYPE ) "
                    + " VALUES ( :P0, :P1, :P2) ";

            foreach (DocTipoExtensionMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.kte_claext, dtoDatos.kte_extension, dtoDatos.kte_mime_type);
                iContador++;
            }
            return iContador;

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
        public DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT KTE_CLAEXT, KTE_EXTENSION, KTE_MIME_TYPE  "
                + " from SIT_DOC_KTIPO_EXTENSION "
                + " order by KTE_CLAEXT "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select KTE_CLAEXT as id, KTE_EXTENSION as text FROM SIT_DOC_KTIPO_EXTENSION ORDER BY KTE_CLAEXT";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<string, int> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<string, int> dicParametros = new Dictionary<string, int>();
            DataTable dtDatos;

            string sqlQuery = " Select KTE_CLAEXT, KTE_EXTENSION FROM SIT_DOC_KTIPO_EXTENSION ORDER BY KTE_CLAEXT";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
                dicParametros.Add(row["KTE_EXTENSION"].ToString(), Convert.ToInt32(row["KTE_CLAEXT"]));

            return dicParametros;
        }


        private Dictionary<string, DocTipoExtensionMdl> dmlSelectDicTipoExtension(Object oDatos)
        {
            Dictionary<string, DocTipoExtensionMdl> dicDatos = new Dictionary<string, DocTipoExtensionMdl>();
            DataTable dtDatos;

            string sqlQuery = " select KTE_CLAEXT, KTE_MIME_TYPE, KTE_EXTENSION FROM SIT_DOC_KTIPO_EXTENSION ";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
                dicDatos.Add(row["KTE_EXTENSION"].ToString(), new DocTipoExtensionMdl(Convert.ToInt32(row["KTE_CLAEXT"]), row["KTE_EXTENSION"].ToString(), row["KTE_MIME_TYPE"].ToString() )  );
            
            return  dicDatos;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
