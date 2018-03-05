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
	 public class SIT_RED_AFDFLUJODao : BaseDao
	 {
	 	 public SIT_RED_AFDFLUJODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RED_AFDFLUJO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RED_AFDFLUJO( rtpclave, afforigen, afdclave, affdestino) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.rtpclave, oDatos.afforigen, oDatos.afdclave, oDatos.affdestino ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RED_AFDFLUJO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_AFDFLUJO( rtpclave, afforigen, afdclave, affdestino) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_RED_AFDFLUJO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.rtpclave, oDatos.afforigen, oDatos.afdclave, oDatos.affdestino ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RED_AFDFLUJO oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RED_AFDFLUJO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RED_AFDFLUJO WHERE  afdclave = :P0 AND affdestino = :P1 AND afforigen = :P2 AND rtpclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.afdclave, oDatos.affdestino, oDatos.afforigen, oDatos.rtpclave ); 
	 	 }
 
 
	 	 public List<SIT_RED_AFDFLUJO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_AFDFLUJO ";
	 	 	  return CrearListaMDL<SIT_RED_AFDFLUJO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RED_AFDFLUJO dmlSelectID(SIT_RED_AFDFLUJO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_AFDFLUJO WHERE  afdclave = :P0 AND affdestino = :P1 AND afforigen = :P2 AND rtpclave = :P3 ";  
	 	 	  return CrearListaMDL<SIT_RED_AFDFLUJO>(ConsultaDML ( sSQL,  oDatos.afdclave, oDatos.affdestino, oDatos.afforigen, oDatos.rtpclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RED_AFDFLUJO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RED_AFDFLUJO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RED_AFDFLUJO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectProdNodo(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sqlQuery = " SELECT AFDCLAVE, AFFORIGEN, ARI.rtpclave, ari.rtpdescripcion, affdestino, rtpforma, rtpformato, nedurl, rtpTipo, rtpPlazo " +
            " FROM SIT_RED_AFDFLUJO FLU, SIT_RESP_TIPO ARI, SIT_RED_NODOESTADO NODO " +
            " WHERE FLU.AFDCLAVE = :P0 AND FLU.AFFORIGEN = :P1 AND flu.rtpclave = ARI.rtpclave AND NODO.nedclave = AFFORIGEN ";

            return ConsultaDML(sqlQuery, dicParam[DButil.SIT_RED_AFDFLUJO_COL.AFDCLAVE], dicParam[DButil.SIT_RED_AFDFLUJO_COL.AFFORIGEN]);
        }
    


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + "  SELECT AFDCLAVE, AFFORIGEN, PORI.KP_DESCRIPCION AS ORIGENAREA, ORI.KNE_DESCRIPCION AS ORIGENACCION,  ARI.rtpclave, rtpdescripcion,  "
                + "  affdestino,  PDES.KP_DESCRIPCION as DESTAREA, DES.KNE_DESCRIPCION as DESTACCION, affplazo "
                + "  FROM SIT_RED_AFDFLUJO FLU, SIT_RESP_TIPO ARI, SIT_RED_NODOESTADO ORI, SIT_RED_NODOESTADO DES,  "
                + "  SIT_ADM_KPERFIL PORI, SIT_ADM_KPERFIL PDES "
                + "  WHERE ARI.rtpclave = FLU.rtpclave "
                + "  AND ORI.nedclave = AFFORIGEN "
                + "  AND DES.nedclave = affdestino "
                + "  AND PORI.KP_CLAPERFIL = ORI.KP_CLAPERFIL "
                + "  AND PDES.KP_CLAPERFIL = DES.KP_CLAPERFIL "
                + "  ORDER BY  ORI.KP_CLAPERFIL, ORI.KNE_DESCRIPCION, rtpclave "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.*from( SELECT AFDCLAVE, AFFORIGEN, PORI.PERDESCRIPCION AS ORIGENAREA, ORI.NEDDESCRIPCION AS ORIGENACCION, ARI.rtpclave, rtpdescripcion, affdestino, PDES.PERDESCRIPCION as DESTAREA, DES.NEDDESCRIPCION as DESTACCION, RTPPLAZO FROM SIT_RED_AFDFLUJO FLU, SIT_RESP_TIPO ARI, SIT_RED_NODOESTADO ORI, SIT_RED_NODOESTADO DES, SIT_ADM_PERFIL PORI, SIT_ADM_PERFIL PDES WHERE ARI.rtpclave = FLU.rtpclave   AND ORI.nedclave = AFFORIGEN   AND DES.nedclave = affdestino AND PDES.PERCLAVE = DES.NEDCLAVE ORDER BY   PDES.PERCLAVE, ORI.NEDDESCRIPCION, rtpclave) a ) SELECT* from Resultado WHERE recid between :P0 and :P1";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
