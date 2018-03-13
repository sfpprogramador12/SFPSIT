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
	 public class SIT_RESP_RREVISIONDao : BaseDao
	 {
	 	 public SIT_RESP_RREVISIONDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_RREVISION oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_RREVISION( repclave) VALUES (  :P0) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_RREVISION> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_RREVISION( repclave) VALUES (  :P0) ";  
	 	 	  foreach (SIT_RESP_RREVISION oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.repclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_RREVISION oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_RREVISION oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_RREVISION WHERE  repclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.repclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_RREVISION> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_RREVISION ";
	 	 	  return CrearListaMDL<SIT_RESP_RREVISION>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RESP_RREVISION dmlSelectID(SIT_RESP_RREVISION oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_RREVISION WHERE  repclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_RREVISION>(ConsultaDML ( sSQL,  oDatos.repclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_RREVISION );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_RREVISION );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_RREVISION );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public object dmlSelectTranspuesta() {
            return null;
        }




        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "  WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                "SELECT * from SIT_RESP_rrevision RUBRO" +
                " ) a ) SELECT* from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }


        /*FIN*/

    }
}
