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
 
namespace SFP.SIT.SERV.Dao.ARISTA
{
	 public class SIT_RESP_TIPOINFODao : BaseDao
	 {
	 	 public SIT_RESP_TIPOINFODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_TIPOINFO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_TIPOINFO( nfoclave, rtpclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.nfoclave, oDatos.rtpclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_TIPOINFO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_TIPOINFO( nfoclave, rtpclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_RESP_TIPOINFO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.nfoclave, oDatos.rtpclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_TIPOINFO oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_TIPOINFO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_TIPOINFO WHERE  nfoclave = :P0 AND rtpclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.nfoclave, oDatos.rtpclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_TIPOINFO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_TIPOINFO ";
	 	 	  return CrearListaMDL<SIT_RESP_TIPOINFO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RESP_TIPOINFO dmlSelectID(SIT_RESP_TIPOINFO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_TIPOINFO WHERE  nfoclave = :P0 AND rtpclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_RESP_TIPOINFO>(ConsultaDML ( sSQL,  oDatos.nfoclave, oDatos.rtpclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_TIPOINFO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_TIPOINFO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_TIPOINFO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }


        /*INICIO*/
        public object dmlSelectTranspuesta()
        {
            return null;
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "  WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                "SELECT RT.RTPCLAVE, CI.NFODESCRIPCION  from SIT_RESP_TIPOINFO RT, SIT_RESP_CLASINFO CI WHERE CI.NFOCLAVE = RT.NFOCLAVE ORDER BY RT.RTPCLAVE" +
                " ) a ) SELECT* from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/

    }
}
