using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.SOL;
 
namespace SFP.SIT.SERV.Dao.SOL
{
	 public class SIT_SOL_DOCDao : BaseDao
	 {
	 	 public SIT_SOL_DOCDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SOL_DOC oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_DOC( docclave, solclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.docclave, oDatos.solclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SOL_DOC> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_DOC( docclave, solclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_SOL_DOC oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.docclave, oDatos.solclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SOL_DOC oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SOL_DOC oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SOL_DOC WHERE  docclave = :P0 AND solclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.docclave, oDatos.solclave ); 
	 	 }
 
 
	 	 public List<SIT_SOL_DOC> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_DOC ";
	 	 	  return CrearListaMDL<SIT_SOL_DOC>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_SOL_DOC dmlSelectID(SIT_SOL_DOC oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_DOC WHERE  docclave = :P0 AND solclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_SOL_DOC>(ConsultaDML ( sSQL,  oDatos.docclave, oDatos.solclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SOL_DOC );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SOL_DOC );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SOL_DOC );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/


        /*FIN*/
 
	 }
}
