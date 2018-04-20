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
	 public class SIT_RESP_RESPUESTADao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_RESP_RESPUESTADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_RESPUESTA oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RESP_RESPUESTA"); 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_RESPUESTA( repcantidad, megclave, docclave, repoficio, repedofec, rtpclave, repclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.repcantidad, oDatos.megclave, oDatos.docclave, oDatos.repoficio, oDatos.repedofec, oDatos.rtpclave, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_RESPUESTA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_RESPUESTA( repcantidad, megclave, docclave, repoficio, repedofec, rtpclave, repclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6) ";  
	 	 	  foreach (SIT_RESP_RESPUESTA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RESP_RESPUESTA"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.repcantidad, oDatos.megclave, oDatos.docclave, oDatos.repoficio, oDatos.repedofec, oDatos.rtpclave, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_RESPUESTA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_RESPUESTA SET  repcantidad = :P0, megclave = :P1, docclave = :P2, repoficio = :P3, repedofec = :P4, rtpclave = :P5 WHERE  repclave = :P6 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.repcantidad, oDatos.megclave, oDatos.docclave, oDatos.repoficio, oDatos.repedofec, oDatos.rtpclave, oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_RESPUESTA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_RESPUESTA WHERE  repclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.repclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_RESPUESTA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_RESPUESTA ";
	 	 	  return CrearListaMDL<SIT_RESP_RESPUESTA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RESP_RESPUESTA dmlSelectID(SIT_RESP_RESPUESTA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_RESPUESTA WHERE  repclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_RESPUESTA>(ConsultaDML ( sSQL,  oDatos.repclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_RESPUESTA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_RESPUESTA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_RESPUESTA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public int dmlEditarArchivo(SIT_RESP_RESPUESTA oDatos)
        {
            String sSQL = " UPDATE SIT_RESP_RESPUESTA SET docclave = :P0 WHERE  repclave = :P1 ";
            return (int)EjecutaDML(sSQL, oDatos.docclave, oDatos.repclave);
        }


        public int dmlEditarSinDoc(SIT_RESP_RESPUESTA oDatos)
        {
            String sSQL = " UPDATE SIT_RESP_RESPUESTA SET  repcantidad = :P0, megclave = :P1, repoficio = :P2, repedofec = :P3, rtpclave = :P4 WHERE  repclave = :P5 ";
            return (int)EjecutaDML(sSQL, oDatos.repcantidad, oDatos.megclave,  oDatos.repoficio, oDatos.repedofec, oDatos.rtpclave, oDatos.repclave);
        }


        public List<SIT_RESP_RESPUESTA> dmlSelectRespNodoRtpDif(Int64 nodClave, int rtpClave, int sdoClave)
        {
            String sSQL = " SELECT  RR.rtpclave, RR.repclave "
                + " FROM SIT_RED_NODORESP RNR, SIT_RESP_RESPUESTA RR "
                + " WHERE RNR.nodClave = :P0 "
                + " AND RNR.REPCLAVE = RR.REPCLAVE "
                + " AND RTPCLAVE <> :P1 AND SDOCLAVE = :P2 ";

            return CrearListaMDL<SIT_RESP_RESPUESTA>(ConsultaDML(sSQL, nodClave, rtpClave, sdoClave) as DataTable);
        }


        public List<SIT_RESP_RESPUESTA> dmlSelectRespNodoRtpIgual(Int64 nodClave, int rtpClave, int sdoClave)
        {
            String sSQL = " SELECT  RR.rtpclave, RR.repclave "
                + " FROM SIT_RED_NODORESP RNR, SIT_RESP_RESPUESTA RR "
                + " WHERE RNR.nodClave = :P0 "
                + " AND RNR.REPCLAVE = RR.REPCLAVE "
                + " AND RTPCLAVE = :P1 AND SDOCLAVE = :P2 ";

            return CrearListaMDL<SIT_RESP_RESPUESTA>(ConsultaDML(sSQL, nodClave, rtpClave, sdoClave) as DataTable);
        }


        /*FIN*/

    }
}
