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
	 public class SIT_RESP_REPRODUCCIONDao : BaseDao
	 {
	 	 public SIT_RESP_REPRODUCCIONDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_REPRODUCCION oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_REPRODUCCION( rccdescripcion, rccclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.rccdescripcion, oDatos.rccclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_REPRODUCCION> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_REPRODUCCION( rccdescripcion, rccclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_RESP_REPRODUCCION oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.rccdescripcion, oDatos.rccclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_REPRODUCCION oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_REPRODUCCION SET  rccdescripcion = :P0 WHERE  rccclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.rccdescripcion, oDatos.rccclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_REPRODUCCION oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_REPRODUCCION WHERE  rccclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.rccclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_REPRODUCCION> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_REPRODUCCION ";
	 	 	  return CrearListaMDL<SIT_RESP_REPRODUCCION>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT rccclave as ID, RCCDESCRIPCION as DESCRIP FROM SIT_RESP_REPRODUCCION";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT rccclave, RCCDESCRIPCION FROM SIT_RESP_REPRODUCCION";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RESP_REPRODUCCION dmlSelectID(SIT_RESP_REPRODUCCION oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_REPRODUCCION WHERE  rccclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_REPRODUCCION>(ConsultaDML ( sSQL,  oDatos.rccclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_REPRODUCCION );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_REPRODUCCION );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_REPRODUCCION );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        /*FIN*/
 
	 }
}
