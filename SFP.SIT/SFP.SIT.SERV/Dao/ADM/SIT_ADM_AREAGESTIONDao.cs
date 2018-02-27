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
	 public class SIT_ADM_AREAGESTIONDao : BaseDao
	 {
	 	 public SIT_ADM_AREAGESTIONDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_AREAGESTION oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREAGESTION( agndescripcion, agnfecfin, agnfecini, agnclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.agndescripcion, oDatos.agnfecfin, oDatos.agnfecini, oDatos.agnclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_AREAGESTION> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREAGESTION( agndescripcion, agnfecfin, agnfecini, agnclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_ADM_AREAGESTION oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.agndescripcion, oDatos.agnfecfin, oDatos.agnfecini, oDatos.agnclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_AREAGESTION oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_AREAGESTION SET  agndescripcion = :P0, agnfecfin = :P1, agnfecini = :P2 WHERE  agnclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.agndescripcion, oDatos.agnfecfin, oDatos.agnfecini, oDatos.agnclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_AREAGESTION oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_AREAGESTION WHERE  agnclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.agnclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_AREAGESTION> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREAGESTION ";
	 	 	  return CrearListaMDL<SIT_ADM_AREAGESTION>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT agnclave as ID, AGNDESCRIPCION as DESCRIP FROM SIT_ADM_AREAGESTION";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT agnclave, AGNDESCRIPCION FROM SIT_ADM_AREAGESTION";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_ADM_AREAGESTION dmlSelectID(SIT_ADM_AREAGESTION oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREAGESTION WHERE  agnclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_AREAGESTION>(ConsultaDML ( sSQL,  oDatos.agnclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAGESTION );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAGESTION );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREAGESTION );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + "SELECT agndescripcion, agnfecfin, agnfecini, agnclave from  SIT_ADM_AREAGESTION "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }


        /*FIN*/
 
	 }
}
