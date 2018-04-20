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
using SFP.SIT.SERV.Model.RED;
 
namespace SFP.SIT.SERV.Dao.RED
{
	 public class SIT_RED_NODORESPDao : BaseDao
	 {
	 	 public SIT_RED_NODORESPDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RED_NODORESP oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RED_NODORESP( sdoclave, repclave, nodclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.sdoclave, oDatos.repclave, oDatos.nodclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RED_NODORESP> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_NODORESP( sdoclave, repclave, nodclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_RED_NODORESP oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.sdoclave, oDatos.repclave, oDatos.nodclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RED_NODORESP oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RED_NODORESP SET  sdoclave = :P0 WHERE  nodclave = :P1 AND repclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.sdoclave, oDatos.nodclave, oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RED_NODORESP oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RED_NODORESP WHERE  nodclave = :P0 AND repclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.nodclave, oDatos.repclave ); 
	 	 }
 
 
	 	 public List<SIT_RED_NODORESP> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_NODORESP ";
	 	 	  return CrearListaMDL<SIT_RED_NODORESP>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RED_NODORESP dmlSelectID(SIT_RED_NODORESP oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_NODORESP WHERE  nodclave = :P0 AND repclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_RED_NODORESP>(ConsultaDML ( sSQL,  oDatos.nodclave, oDatos.repclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RED_NODORESP );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RED_NODORESP );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RED_NODORESP );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public List<SIT_RED_NODORESP> dmlSelectRespNodo(Int64 iNodo)
        {
            String sSQL = " SELECT RNR.NODCLAVE, RNR.REPCLAVE, RNR.sdoclave " 
                + " FROM SIT_RED_NODORESP RNR, SIT_RESP_RESPUESTA RR "
                + " WHERE RNR.nodClave = :P0 "
                + " AND RNR.REPCLAVE = RR.REPCLAVE "
                + " AND RTPCLAVE <> " + Constantes.Respuesta.AMPLIACION_PLAZO;

            return CrearListaMDL<SIT_RED_NODORESP>(ConsultaDML(sSQL, iNodo) as DataTable);
        }

        public List<SIT_RED_NODORESP> dmlSelectRespNodoAmpPlazo(Int64 iNodo)
        {
            String sSQL = " SELECT RNR.NODCLAVE, RNR.REPCLAVE, RNR.sdoclave "
                + " FROM SIT_RED_NODORESP RNR, SIT_RESP_RESPUESTA RR "
                + " WHERE RNR.nodClave = :P0 "
                + " AND RNR.REPCLAVE = RR.REPCLAVE "
                + " AND RTPCLAVE = " + Constantes.Respuesta.AMPLIACION_PLAZO ;

            return CrearListaMDL<SIT_RED_NODORESP>(ConsultaDML(sSQL, iNodo) as DataTable);
        }

        public List<SIT_RED_NODORESP> dmlSelectRespNodoTurnar(Int64 iNodo, int iUsrClave)
        {
            String sSQL = " SELECT RNR.NODCLAVE, RNR.REPCLAVE, RNR.sdoclave "
                + " FROM SIT_RED_NODORESP RNR, SIT_RESP_RESPUESTA RR, SIT_RESP_TURNAR TU"
                + " WHERE RNR.nodClave = :P0 "
                + " AND RNR.REPCLAVE = RR.REPCLAVE "
                + " AND RTPCLAVE = " + Constantes.Respuesta.TURNAR 
                + " AND TU.REPCLAVE = RNR.REPCLAVE"
                + " AND USRclave = " + iUsrClave;

            return CrearListaMDL<SIT_RED_NODORESP>(ConsultaDML(sSQL, iNodo) as DataTable);
        }


        public List<SIT_RED_NODORESP> dmlSelectRespNodoRepClave(Int64 iResp)
        {
            String sSQL = " SELECT * FROM SIT_RED_NODORESP WHERE repClave = :P0";
            return CrearListaMDL<SIT_RED_NODORESP>(ConsultaDML(sSQL, iResp) as DataTable);
        }


        public Boolean dmlSelectRespExiste(SIT_RED_NODORESP  nodoResp)
        {
            String sSQL = " SELECT * FROM SIT_RED_NODORESP WHERE repClave = :P0 AND nodclave = :P1 AND sdoclave = :P2 ";
            List<SIT_RED_NODORESP> lstNodoResp =  CrearListaMDL<SIT_RED_NODORESP>( 
                ConsultaDML(sSQL, nodoResp.repclave, nodoResp.nodclave, nodoResp.sdoclave) as DataTable);

            if (lstNodoResp.Count > 0)
                return true;
            else
                return false;

        }

        /*FIN*/
 
	 }
}
