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
	 public class SIT_ADM_PERFILCLASESDao : BaseDao
	 {
	 	 public SIT_ADM_PERFILCLASESDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_PERFILCLASES oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_PERFILCLASES( perclave, claclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.perclave, oDatos.claclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_PERFILCLASES> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_PERFILCLASES( perclave, claclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_PERFILCLASES oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.perclave, oDatos.claclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_PERFILCLASES oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_PERFILCLASES oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_PERFILCLASES WHERE  claclave = :P0 AND perclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.claclave, oDatos.perclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_PERFILCLASES> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_PERFILCLASES ";
	 	 	  return CrearListaMDL<SIT_ADM_PERFILCLASES>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_PERFILCLASES dmlSelectID(SIT_ADM_PERFILCLASES oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_PERFILCLASES WHERE  claclave = :P0 AND perclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_ADM_PERFILCLASES>(ConsultaDML ( sSQL,  oDatos.claclave, oDatos.perclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILCLASES );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILCLASES );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILCLASES );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                "SELECT C.CLADESCRIPCION, P.PERDESCRIPCION FROM SIT_ADM_PERFILCLASES PC, SIT_ADM_CLASES C, SIT_ADM_PERFIL P " +
                "WHERE P.PERCLAVE = PC.PERCLAVE AND C.CLACLAVE = PC.CLACLAVE ) a ) SELECT* from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        /*FIN*/
 
	 }
}
