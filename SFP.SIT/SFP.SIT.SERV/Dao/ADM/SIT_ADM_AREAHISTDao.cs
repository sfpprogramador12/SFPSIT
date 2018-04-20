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
	 public class SIT_ADM_AREAHISTDao : BaseDao
	 {
	 	 public SIT_ADM_AREAHISTDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_AREAHIST oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREAHIST( atpclave, anlclave, araclave, arhreporta, arhsiglas, arhdescripcion, arhclaveua, arhfecfin, arhfecini) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.atpclave, oDatos.anlclave, oDatos.araclave, oDatos.arhreporta, oDatos.arhsiglas, oDatos.arhdescripcion, oDatos.arhclaveua, oDatos.arhfecfin, oDatos.arhfecini ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_AREAHIST> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREAHIST( atpclave, anlclave, araclave, arhreporta, arhsiglas, arhdescripcion, arhclaveua, arhfecfin, arhfecini) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8) ";  
	 	 	  foreach (SIT_ADM_AREAHIST oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.atpclave, oDatos.anlclave, oDatos.araclave, oDatos.arhreporta, oDatos.arhsiglas, oDatos.arhdescripcion, oDatos.arhclaveua, oDatos.arhfecfin, oDatos.arhfecini ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_AREAHIST oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_AREAHIST SET  atpclave = :P0, anlclave = :P1, arhreporta = :P2, arhsiglas = :P3, arhdescripcion = :P4, arhclaveua = :P5 WHERE  araclave = :P6 AND arhfecfin = :P7 AND arhfecini = :P8 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.atpclave, oDatos.anlclave, oDatos.arhreporta, oDatos.arhsiglas, oDatos.arhdescripcion, oDatos.arhclaveua, oDatos.araclave, oDatos.arhfecfin, oDatos.arhfecini ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_AREAHIST oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_AREAHIST WHERE  araclave = :P0 AND arhfecfin = :P1 AND arhfecini = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.araclave, oDatos.arhfecfin, oDatos.arhfecini ); 
	 	 }
 
 
	 	 public List<SIT_ADM_AREAHIST> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREAHIST ";
	 	 	  return CrearListaMDL<SIT_ADM_AREAHIST>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_AREAHIST dmlSelectID(SIT_ADM_AREAHIST oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREAHIST WHERE  araclave = :P0 AND arhfecfin = :P1 AND arhfecini = :P2 ";  
	 	 	  return CrearListaMDL<SIT_ADM_AREAHIST>(ConsultaDML ( sSQL,  oDatos.araclave, oDatos.arhfecfin, oDatos.arhfecini ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAHIST );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAHIST );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAHIST );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public List<ComboMdl> dmlSelectComboFecActual(DateTime dtFecha)
        {
            String sqlQuery = " Select araClave as ID, arhDESCRIPCION as DESCRIP FROM SIT_adm_areahist where :P0 " +
                " between arhFecIni AND arhFecFin ORDER BY arhDESCRIPCION";
            return CrearListaMDL<ComboMdl>(ConsultaDML(sqlQuery, dtFecha));
        }

        public SIT_ADM_AREAHIST dmlSelectAreaActual(int araClave)
        {            
            String sqlQuery = " Select * FROM SIT_adm_areahist where araclave = :P0  " +
                "and :P1 between arhFecIni AND arhFecFin ORDER BY arhDESCRIPCION";

            return CrearMDL<SIT_ADM_AREAHIST>(ConsultaDML(sqlQuery, araClave, DateTime.Now));
                
        }


        ////////public List<SIT_ADM_AREAHIST> dmlSelectAreasActual(DateTime dtFecha)
        ////////{
        ////////    String sSQL = " SELECT AH.araClave, ArhClaveUA, ArhDescripcion, OrgClaveReporta "
        ////////        + " FROM SIT_ADM_AREAHIST AH, SIT_ADM_AREAORG AO "
        ////////        + " WHERE AH.araClave = AO.araClave "
        ////////        + " AND AO.agnClave IN (SELECT AgnClave FROM SIT_ADM_AreaGestion WHERE: P0 BETWEEN AgnFecIni and AgnFecFin ) ";

        ////////    return CrearListaMDL<SIT_ADM_AREAHIST>(ConsultaDML(sSQL, dtFecha));
        ////////}


        ////////public List<SIT_ADM_AREAHIST> dmlSelectAreasActualReporta(Dictionary<string, object> dicParam)
        ////////{

        ////////String sSQL = " SELECT AH.araClave, ArhClaveUA, ArhDescripcion, OrgClaveReporta "
        ////////        + " FROM SIT_ADM_AREAHIST AH, SIT_ADM_AREAORG AO "
        ////////        + " WHERE AH.araClave = AO.araClave "
        ////////        + " AND AO.agnClave IN (SELECT AgnClave FROM SIT_ADM_AreaGestion WHERE :P0 BETWEEN AgnFecIni and AgnFecFin ) "
        ////////        + " AND OrgClaveReporta = :P1";

        ////////    return CrearListaMDL<SIT_ADM_AREAHIST>(ConsultaDML(sSQL, dicParam[PARAM_FECHA], dicParam[PARAM_AREA_REPORTA] ));
        ////////}


        public List<SIT_ADM_AREAHIST> dmlSelectAreasPeriodo(DateTime ttFecha)
        {
            String sSQL = " SELECT AraClave, arhReporta, arhClaveUA FROM TCP_Adm_AreaHist WHERE Convert(datetime,'" + ttFecha.ToString("yyyyMMdd") + "',112)  between ArhFecIni AND ArhFecFin";
            return CrearListaMDL<SIT_ADM_AREAHIST>(ConsultaDML(sSQL) as DataTable);
        }



        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + "SELECT FROM  "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
