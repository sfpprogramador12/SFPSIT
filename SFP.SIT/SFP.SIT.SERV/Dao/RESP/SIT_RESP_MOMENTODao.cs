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
	 public class SIT_RESP_MOMENTODao : BaseDao
	 {
	 	 public SIT_RESP_MOMENTODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_MOMENTO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_MOMENTO( momdescripcion, momclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.momdescripcion, oDatos.momclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_MOMENTO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_MOMENTO( momdescripcion, momclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_RESP_MOMENTO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.momdescripcion, oDatos.momclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_MOMENTO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_MOMENTO SET  momdescripcion = :P0 WHERE  momclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.momdescripcion, oDatos.momclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_MOMENTO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_MOMENTO WHERE  momclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.momclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_MOMENTO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_MOMENTO ";
	 	 	  return CrearListaMDL<SIT_RESP_MOMENTO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT momclave as ID, MOMDESCRIPCION as DESCRIP FROM SIT_RESP_MOMENTO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT momclave, MOMDESCRIPCION FROM SIT_RESP_MOMENTO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RESP_MOMENTO dmlSelectID(SIT_RESP_MOMENTO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_MOMENTO WHERE  momclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_MOMENTO>(ConsultaDML ( sSQL,  oDatos.momclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_MOMENTO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_MOMENTO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_MOMENTO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
/*INICIO*/
 
 
/*FIN*/
 
	 }
}
