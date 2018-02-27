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
	 public class SIT_RESP_ESTADODao : BaseDao
	 {
	 	 public SIT_RESP_ESTADODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_ESTADO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_ESTADO( sdodescripcion, sdoclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.sdodescripcion, oDatos.sdoclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_ESTADO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_ESTADO( sdodescripcion, sdoclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_RESP_ESTADO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.sdodescripcion, oDatos.sdoclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_ESTADO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_ESTADO SET  sdodescripcion = :P0 WHERE  sdoclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.sdodescripcion, oDatos.sdoclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_ESTADO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_ESTADO WHERE  sdoclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.sdoclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_ESTADO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_ESTADO ";
	 	 	  return CrearListaMDL<SIT_RESP_ESTADO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT sdoclave as ID, SDODESCRIPCION as DESCRIP FROM SIT_RESP_ESTADO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT sdoclave, SDODESCRIPCION FROM SIT_RESP_ESTADO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RESP_ESTADO dmlSelectID(SIT_RESP_ESTADO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_ESTADO WHERE  sdoclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_ESTADO>(ConsultaDML ( sSQL,  oDatos.sdoclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_ESTADO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_ESTADO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_ESTADO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
/*INICIO*/
 
 
/*FIN*/
 
	 }
}
