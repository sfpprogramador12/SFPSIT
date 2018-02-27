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
	 public class SIT_RESP_CLASINFODao : BaseDao
	 {
	 	 public SIT_RESP_CLASINFODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_CLASINFO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_CLASINFO( nfodescripcion, nfoclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.nfodescripcion, oDatos.nfoclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_CLASINFO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_CLASINFO( nfodescripcion, nfoclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_RESP_CLASINFO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.nfodescripcion, oDatos.nfoclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_CLASINFO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_CLASINFO SET  nfodescripcion = :P0 WHERE  nfoclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.nfodescripcion, oDatos.nfoclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_CLASINFO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_CLASINFO WHERE  nfoclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.nfoclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_CLASINFO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_CLASINFO ";
	 	 	  return CrearListaMDL<SIT_RESP_CLASINFO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT nfoclave as ID, NFODESCRIPCION as DESCRIP FROM SIT_RESP_CLASINFO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT nfoclave, NFODESCRIPCION FROM SIT_RESP_CLASINFO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RESP_CLASINFO dmlSelectID(SIT_RESP_CLASINFO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_CLASINFO WHERE  nfoclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_CLASINFO>(ConsultaDML ( sSQL,  oDatos.nfoclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_CLASINFO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_CLASINFO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_CLASINFO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
/*INICIO*/
 
 
/*FIN*/
 
	 }
}
