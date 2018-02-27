using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.DOC;
 
namespace SFP.SIT.SERV.Dao.DOC
{
	 public class SIT_DOC_DOCUMENTODao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_DOC_DOCUMENTODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_DOC_DOCUMENTO oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_DOC_DOCUMENTO"); 
	 	 	  String  sSQL = " INSERT INTO SIT_DOC_DOCUMENTO( docmd5, docurl, docfilesystem, docclave, extclave, docnombre, docfolio, docruta, doctamaño, docfecha) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.docmd5, oDatos.docurl, oDatos.docfilesystem, iSecuencia , oDatos.extclave, oDatos.docnombre, oDatos.docfolio, oDatos.docruta, oDatos.doctamaño, oDatos.docfecha ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_DOC_DOCUMENTO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_DOC_DOCUMENTO( docmd5, docurl, docfilesystem, docclave, extclave, docnombre, docfolio, docruta, doctamaño, docfecha) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9) ";  
	 	 	  foreach (SIT_DOC_DOCUMENTO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_DOC_DOCUMENTO"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.docmd5, oDatos.docurl, oDatos.docfilesystem, iSecuencia , oDatos.extclave, oDatos.docnombre, oDatos.docfolio, oDatos.docruta, oDatos.doctamaño, oDatos.docfecha ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_DOC_DOCUMENTO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_DOC_DOCUMENTO SET  docmd5 = :P0, docurl = :P1, docfilesystem = :P2, extclave = :P3, docnombre = :P4, docfolio = :P5, docruta = :P6, doctamaño = :P7, docfecha = :P8 WHERE  docclave = :P9 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.docmd5, oDatos.docurl, oDatos.docfilesystem, oDatos.extclave, oDatos.docnombre, oDatos.docfolio, oDatos.docruta, oDatos.doctamaño, oDatos.docfecha, oDatos.docclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_DOC_DOCUMENTO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_DOC_DOCUMENTO WHERE  docclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.docclave ); 
	 	 }
 
 
	 	 public List<SIT_DOC_DOCUMENTO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_DOC_DOCUMENTO ";
	 	 	  return CrearListaMDL<SIT_DOC_DOCUMENTO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_DOC_DOCUMENTO dmlSelectID(SIT_DOC_DOCUMENTO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_DOC_DOCUMENTO WHERE  docclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_DOC_DOCUMENTO>(ConsultaDML ( sSQL,  oDatos.docclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_DOC_DOCUMENTO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_DOC_DOCUMENTO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_DOC_DOCUMENTO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public int dmlUpdateParcial(SIT_DOC_DOCUMENTO oDatos)
        {
            String sSQL = " UPDATE SIT_DOC_DOCUMENTO SET  docfolio = :P0  WHERE  docclave = :P1 ";
            return (int)EjecutaDML(sSQL, oDatos.docfolio, oDatos.docclave);
        }


        public DataTable dmlSelectDocFolio(Int64 iClaFolio)
        {
            string sqlQuery = " select doc.docClave as ID , docNombre as text "
                + " from SIT_doc_arista docari, SIT_DOC_DOCUMENTO doc "
                + " where solclave = :P0 and docari.docClave = doc.docClave";

            return ConsultaDML(sqlQuery, iClaFolio);
        }

        public DataTable dmlSelectDocNodo(Int64 iClaFolio)
        {
            string sqlQuery = " select doc.docClave as ID , docNombre as text "
                + " from SIT_doc_arista docari, SIT_DOC_DOCUMENTO doc "
                + " where solclave = :P0 and docari.docClave = doc.docClave";

            return ConsultaDML(sqlQuery, iClaFolio);
        }

        public object dmlSelectDocNombreMD5(string sClaveMD5)
        {
            DataTable dtDatos;
            int iDocumentoID = 0;

            string sqlQuery = " select docClave from SIT_DOC_DOCUMENTO doc WHERE docMD5 = :P0 ";
            dtDatos = ConsultaDML(sqlQuery, sClaveMD5 );
            foreach (DataRow row in dtDatos.Rows)
            {
                iDocumentoID = Convert.ToInt32(row["docClave"]);
            }

            return iDocumentoID;
        }


        public DataTable dmlSelectDocID(Int64 iClaDoc)
        {
            string sqlQuery = " SELECT docNombre, docruta, extmimetype, docfolio "
                + " FROM SIT_DOC_DOCUMENTO doc, SIT_DOC_EXTENSION ext "
                + " WHERE doc.extClave = ext.extClave and doc.docClave = :P0";

            return ConsultaDML(sqlQuery, iClaDoc);
        }


 
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT docClave, docFecha, docFolio, docNombre, doctamaño, docruta, extClave,  "
                + " docfilesystem, docurl, docMD5  "
                + " from SIT_DOC_DOCUMENTO order by docClave "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }


        public List<SIT_DOC_DOCUMENTO> dmlSelectDocInicial(Int64 lFolio)
        {
            String sSQL = " SELECT * FROM SIT_DOC_DOCUMENTO WHERE  DOCCLAVE IN  ( select DOCCLAVE  FROM SIT_SOL_DOC WHERE SOLCLAVE = :P0 )   ";
            return CrearListaMDL<SIT_DOC_DOCUMENTO>(ConsultaDML(sSQL, lFolio) as DataTable);
        }


        public int dmlBorrarDocTodos(Int64 ldocClave)
        {
            int iTotal = 0;
            String sSQL = " DELETE FROM SIT_DOC_DOCUMENTO WHERE  docclave = :P0 ";

            try
            {
                iTotal = (int)EjecutaDML(sSQL, ldocClave);
            }
            catch {
                // Existe un registro en otra tabl ay no se borro
            }
            return iTotal;
        }


        /*FIN*/
 
	 }
}
