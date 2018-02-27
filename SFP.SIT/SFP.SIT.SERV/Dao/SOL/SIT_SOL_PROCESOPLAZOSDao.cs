using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.SOL;
 
namespace SFP.SIT.SERV.Dao.SOL
{
	 public class SIT_SOL_PROCESOPLAZOSDao : BaseDao
	 {
	 	 public SIT_SOL_PROCESOPLAZOSDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SOL_PROCESOPLAZOS oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_PROCESOPLAZOS( pczclave, sotclave, pczamarillo, pczverde, pczplazo, prcclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.pczclave, oDatos.sotclave, oDatos.pczamarillo, oDatos.pczverde, oDatos.pczplazo, oDatos.prcclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SOL_PROCESOPLAZOS> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_PROCESOPLAZOS( pczclave, sotclave, pczamarillo, pczverde, pczplazo, prcclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5) ";  
	 	 	  foreach (SIT_SOL_PROCESOPLAZOS oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.pczclave, oDatos.sotclave, oDatos.pczamarillo, oDatos.pczverde, oDatos.pczplazo, oDatos.prcclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SOL_PROCESOPLAZOS oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SOL_PROCESOPLAZOS SET  pczamarillo = :P0, pczverde = :P1, pczplazo = :P2 WHERE  pczclave = :P3 AND prcclave = :P4 AND sotclave = :P5 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.pczamarillo, oDatos.pczverde, oDatos.pczplazo, oDatos.pczclave, oDatos.prcclave, oDatos.sotclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SOL_PROCESOPLAZOS oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SOL_PROCESOPLAZOS WHERE  pczclave = :P0 AND prcclave = :P1 AND sotclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.pczclave, oDatos.prcclave, oDatos.sotclave ); 
	 	 }
 
 
	 	 public List<SIT_SOL_PROCESOPLAZOS> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_PROCESOPLAZOS ";
	 	 	  return CrearListaMDL<SIT_SOL_PROCESOPLAZOS>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_SOL_PROCESOPLAZOS dmlSelectID(SIT_SOL_PROCESOPLAZOS oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_PROCESOPLAZOS WHERE  pczclave = :P0 AND prcclave = :P1 AND sotclave = :P2 ";  
	 	 	  return CrearListaMDL<SIT_SOL_PROCESOPLAZOS>(ConsultaDML ( sSQL,  oDatos.pczclave, oDatos.prcclave, oDatos.sotclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SOL_PROCESOPLAZOS );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SOL_PROCESOPLAZOS );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SOL_PROCESOPLAZOS );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public List<SIT_SOL_PROCESOPLAZOS> dmlSelectLista()
        {
            string sqlQuery = "SELECT  prcclave, sotclave, pczclave, pczplazo, pczverde, pczamarillo from SIT_SOL_PROCESOPLAZOS order by prcclave ";

            return CrearListaMDL<SIT_SOL_PROCESOPLAZOS>(ConsultaDML(sqlQuery));
        }


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT PLZ.prcclave,krp_descripcion,  PLZ.sotclave, tso_descripcion, pczclave, pczplazo, pczverde, pczamarillo "
                + " FROM SIT_SOL_PROCESOPLAZOS PLZ, SIT_SOL_PROCESO PRC, SIT_SOL_SOLICITUDTIPO TSOL "
                + " WHERE PRC.prcclave = PLZ.prcclave "
                + " AND TSOL.sotclave = PLZ.sotclave "
                + " ORDER BY prcclave "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup); ;
        }
        /*FIN*/
 
	 }
}
