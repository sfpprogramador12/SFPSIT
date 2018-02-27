using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model;
 
namespace SFP.SIT.SERV.Dao
{
	 public class SIT_DOCUMENTODao : BaseDao
	 {
	 	 public SIT_DOCUMENTODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_DOCUMENTO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_DOCUMENTO( doc_md5, doc_url, doc_filesystem, doc_cladoc, kte_claext, doc_nombre, doc_folio, doc_ruta, doc_size, doc_fecha) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.doc_md5, oDatos.doc_url, oDatos.doc_filesystem, oDatos.doc_cladoc, oDatos.kte_claext, oDatos.doc_nombre, oDatos.doc_folio, oDatos.doc_ruta, oDatos.doc_size, oDatos.doc_fecha ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_DOCUMENTO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_DOCUMENTO( doc_md5, doc_url, doc_filesystem, doc_cladoc, kte_claext, doc_nombre, doc_folio, doc_ruta, doc_size, doc_fecha) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9) ";  
	 	 	  foreach (SIT_DOCUMENTO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.doc_md5, oDatos.doc_url, oDatos.doc_filesystem, oDatos.doc_cladoc, oDatos.kte_claext, oDatos.doc_nombre, oDatos.doc_folio, oDatos.doc_ruta, oDatos.doc_size, oDatos.doc_fecha ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_DOCUMENTO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_DOCUMENTO SET  doc_md5 = :P0, doc_url = :P1, doc_filesystem = :P2, doc_cladoc = :P3, kte_claext = :P4, doc_nombre = :P5, doc_folio = :P6, doc_ruta = :P7, doc_size = :P8, doc_fecha = :P9 WHERE  doc_cladoc = :P10 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.doc_md5, oDatos.doc_url, oDatos.doc_filesystem, oDatos.doc_cladoc, oDatos.kte_claext, oDatos.doc_nombre, oDatos.doc_folio, oDatos.doc_ruta, oDatos.doc_size, oDatos.doc_fecha, oDatos.doc_cladoc ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_DOCUMENTO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_DOCUMENTO WHERE  doc_cladoc = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.doc_cladoc ); 
	 	 }
 
 
	 	 public List<SIT_DOCUMENTO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_DOCUMENTO ";
	 	 	  return CrearListaMDL<SIT_DOCUMENTO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_DOCUMENTO dmlSelectID(SIT_DOCUMENTO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_DOCUMENTO WHERE  doc_cladoc = :P0 ";  
	 	 	  return CrearListaMDL<SIT_DOCUMENTO>(ConsultaDML ( sSQL,  oDatos.doc_cladoc ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_DOCUMENTO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_DOCUMENTO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_DOCUMENTO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
	 }
}
