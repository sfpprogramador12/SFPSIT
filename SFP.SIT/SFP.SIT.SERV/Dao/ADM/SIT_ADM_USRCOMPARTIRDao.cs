using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.ADM;
 
namespace SFP.SIT.SERV.Dao.ADM
{
	 public class SIT_ADM_USRCOMPARTIRDao : BaseDao
	 {
	 	 public SIT_ADM_USRCOMPARTIRDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_USRCOMPARTIR oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_USRCOMPARTIR( comusr, usrclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.comusr, oDatos.usrclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_USRCOMPARTIR> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_USRCOMPARTIR( comusr, usrclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_USRCOMPARTIR oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.comusr, oDatos.usrclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_USRCOMPARTIR oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_USRCOMPARTIR oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_USRCOMPARTIR WHERE  comusr = :P0 AND usrclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.comusr, oDatos.usrclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_USRCOMPARTIR> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_USRCOMPARTIR ";
	 	 	  return CrearListaMDL<SIT_ADM_USRCOMPARTIR>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_USRCOMPARTIR dmlSelectID(SIT_ADM_USRCOMPARTIR oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_USRCOMPARTIR WHERE  comusr = :P0 AND usrclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_ADM_USRCOMPARTIR>(ConsultaDML ( sSQL,  oDatos.comusr, oDatos.usrclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_USRCOMPARTIR );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_USRCOMPARTIR );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_USRCOMPARTIR );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
/*INICIO*/
 
 
/*FIN*/
 
	 }
}
