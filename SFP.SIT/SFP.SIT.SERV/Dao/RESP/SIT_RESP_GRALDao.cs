using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.RESP;
 
namespace SFP.SIT.SERV.Dao.RESP
{
	 public class SIT_RESP_GRALDao : BaseDao
	 {
	 	 public SIT_RESP_GRALDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_GRAL oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_GRAL( gracontenido, rccclave, repclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.gracontenido, oDatos.rccclave, oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_GRAL> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_GRAL( gracontenido, rccclave, repclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_RESP_GRAL oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.gracontenido, oDatos.rccclave, oDatos.repclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_GRAL oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_GRAL SET  gracontenido = :P0, rccclave = :P1 WHERE  repclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.gracontenido, oDatos.rccclave, oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_GRAL oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_GRAL WHERE  repclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.repclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_GRAL> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_GRAL ";
	 	 	  return CrearListaMDL<SIT_RESP_GRAL>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RESP_GRAL dmlSelectID(SIT_RESP_GRAL oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_GRAL WHERE  repclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_GRAL>(ConsultaDML ( sSQL,  oDatos.repclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_GRAL );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_GRAL );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_GRAL );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public List<SIT_RESP_GRAL> dmlSelectRespID(Int64 repclave)
        {
            String sSQL = " SELECT * FROM SIT_RESP_GRAL WHERE repclave = :P0 ";
            return CrearListaMDL<SIT_RESP_GRAL>(ConsultaDML(sSQL, repclave) as DataTable);
        }


        public int dmlBorrarID(Int64 repclave)
        {
            String sSQL = " DELETE FROM SIT_RESP_GRAL WHERE repclave = :P0 ";
            return (int)EjecutaDML(sSQL, repclave);
        }

        public int dmlActualizarDoc(Dictionary<string, object> dicParam)
        {
            String sSQL = " UPDATE SIT_RESP_GRAL SET  docclave = null WHERE repclave = :P0 ";

            return (int)EjecutaDML(sSQL,  dicParam[DButil.SIT_RESP_GRAL_COL.REPCLAVE]);
        }

        public int dmlEditarDescrp(SIT_RESP_GRAL oDatos)
        {
            String sSQL = " UPDATE SIT_RESP_GRAL SET  gracontenido = :P0 WHERE repclave = :P1 ";
            return (int)EjecutaDML(sSQL, oDatos.gracontenido, oDatos.repclave);
        }

        /*FIN*/
 
	 }
}
