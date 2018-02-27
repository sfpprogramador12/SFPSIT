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
	 public class SIT_ADM_AREAORGDao : BaseDao
	 {
	 	 public SIT_ADM_AREAORGDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_AREAORG oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREAORG( agnclave, araclave, orgorden, orgclavereporta) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.agnclave, oDatos.araclave, oDatos.orgorden, oDatos.orgclavereporta ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_AREAORG> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREAORG( agnclave, araclave, orgorden, orgclavereporta) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_ADM_AREAORG oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.agnclave, oDatos.araclave, oDatos.orgorden, oDatos.orgclavereporta ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_AREAORG oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_AREAORG SET  orgorden = :P0, orgclavereporta = :P1 WHERE  agnclave = :P2 AND araclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.orgorden, oDatos.orgclavereporta, oDatos.agnclave, oDatos.araclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_AREAORG oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_AREAORG WHERE  agnclave = :P0 AND araclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.agnclave, oDatos.araclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_AREAORG> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREAORG ";
	 	 	  return CrearListaMDL<SIT_ADM_AREAORG>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_AREAORG dmlSelectID(SIT_ADM_AREAORG oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREAORG WHERE  agnclave = :P0 AND araclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_ADM_AREAORG>(ConsultaDML ( sSQL,  oDatos.agnclave, oDatos.araclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAORG );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAORG );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAORG );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT * FROM SIT_ADM_AREAORG "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
