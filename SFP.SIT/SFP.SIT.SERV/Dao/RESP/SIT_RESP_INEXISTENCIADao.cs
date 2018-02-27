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
	 public class SIT_RESP_INEXISTENCIADao : BaseDao
	 {
	 	 public SIT_RESP_INEXISTENCIADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_INEXISTENCIA oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_INEXISTENCIA( inxcargo, repclave, inxresponsable) VALUES (  :P0, :P1, :P2) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.inxcargo, oDatos.repclave, oDatos.inxresponsable ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_INEXISTENCIA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_INEXISTENCIA( inxcargo, repclave, inxresponsable) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_RESP_INEXISTENCIA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.inxcargo, oDatos.repclave, oDatos.inxresponsable ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_INEXISTENCIA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_INEXISTENCIA SET  inxcargo = :P0, inxresponsable = :P1 WHERE  repclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.inxcargo, oDatos.inxresponsable, oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_INEXISTENCIA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_INEXISTENCIA WHERE  repclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.repclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_INEXISTENCIA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_INEXISTENCIA ";
	 	 	  return CrearListaMDL<SIT_RESP_INEXISTENCIA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RESP_INEXISTENCIA dmlSelectID(SIT_RESP_INEXISTENCIA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_INEXISTENCIA WHERE  repclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_INEXISTENCIA>(ConsultaDML ( sSQL,  oDatos.repclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_INEXISTENCIA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_INEXISTENCIA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_INEXISTENCIA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public int dmlBorrar(long repClave)
        {
            String sSQL = " DELETE FROM SIT_RESP_INEXISTENCIA WHERE  repclave = :P0 ";
            return (int)EjecutaDML(sSQL, repClave);
        }

        /*FIN*/
 
	 }
}
