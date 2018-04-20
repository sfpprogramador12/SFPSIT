using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.RESP;
 
namespace SFP.SIT.SERV.Dao.RESP
{
	 public class SIT_RESP_TURNARDao : BaseDao
	 {
	 	 public SIT_RESP_TURNARDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_TURNAR oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_TURNAR( araclave, usrclave, turinstruccion, repclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.araclave, oDatos.usrclave, oDatos.turinstruccion, oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_TURNAR> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_TURNAR( araclave, usrclave, turinstruccion, repclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_RESP_TURNAR oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.araclave, oDatos.usrclave, oDatos.turinstruccion, oDatos.repclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_TURNAR oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_TURNAR SET  turinstruccion = :P0 WHERE  araclave = :P1 AND repclave = :P2 AND usrclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.turinstruccion, oDatos.araclave, oDatos.repclave, oDatos.usrclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_TURNAR oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_TURNAR WHERE  araclave = :P0 AND repclave = :P1 AND usrclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.araclave, oDatos.repclave, oDatos.usrclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_TURNAR> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_TURNAR ";
	 	 	  return CrearListaMDL<SIT_RESP_TURNAR>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RESP_TURNAR dmlSelectID(SIT_RESP_TURNAR oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_TURNAR WHERE  araclave = :P0 AND repclave = :P1 AND usrclave = :P2 ";  
	 	 	  return CrearListaMDL<SIT_RESP_TURNAR>(ConsultaDML ( sSQL,  oDatos.araclave, oDatos.repclave, oDatos.usrclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_TURNAR );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_TURNAR );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_TURNAR );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
/*INICIO*/
 
 
/*FIN*/
 
	 }
}
