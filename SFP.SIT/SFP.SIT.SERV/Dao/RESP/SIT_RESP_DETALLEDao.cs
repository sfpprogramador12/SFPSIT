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
	 public class SIT_RESP_DETALLEDao : BaseDao
	 {
	 	 public SIT_RESP_DETALLEDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_DETALLE oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_DETALLE( docclave, repclave, detdescripcion, detclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.docclave, oDatos.repclave, oDatos.detdescripcion, oDatos.detclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_DETALLE> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_DETALLE( docclave, repclave, detdescripcion, detclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_RESP_DETALLE oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.docclave, oDatos.repclave, oDatos.detdescripcion, oDatos.detclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_DETALLE oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_DETALLE SET  docclave = :P0, detdescripcion = :P1 WHERE  detclave = :P2 AND repclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.docclave, oDatos.detdescripcion, oDatos.detclave, oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_DETALLE oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_DETALLE WHERE  detclave = :P0 AND repclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.detclave, oDatos.repclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_DETALLE> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_DETALLE ";
	 	 	  return CrearListaMDL<SIT_RESP_DETALLE>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RESP_DETALLE dmlSelectID(SIT_RESP_DETALLE oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_DETALLE WHERE  detclave = :P0 AND repclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_RESP_DETALLE>(ConsultaDML ( sSQL,  oDatos.detclave, oDatos.repclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_DETALLE );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_DETALLE );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_DETALLE );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public List<SIT_RESP_DETALLE> dmlSelectRespID( long repClave)
        {
            String sSQL = " SELECT * FROM SIT_RESP_DETALLE WHERE repclave = :P0";
            return CrearListaMDL<SIT_RESP_DETALLE>(ConsultaDML(sSQL, repClave) as DataTable);
        }

        public int dmlEditarArchivo(SIT_RESP_DETALLE oDatos)
        {
            String sSQL = " UPDATE SIT_RESP_DETALLE SET  docclave = :P0 WHERE  detclave = :P1 AND repclave = :P1 ";
            return (int)EjecutaDML(sSQL, oDatos.docclave, oDatos.detclave, oDatos.repclave);
        }


        public int dmlBorrarRegistros(SIT_RESP_DETALLE oDatos)
        {
            String sSQL = " DELETE FROM SIT_RESP_DETALLE WHERE  repclave = :P0 ";
            return (int)EjecutaDML(sSQL,  oDatos.repclave);
        }

        /*FIN*/
 
	 }
}
