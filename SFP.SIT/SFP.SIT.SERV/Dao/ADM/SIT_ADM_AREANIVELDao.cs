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
	 public class SIT_ADM_AREANIVELDao : BaseDao
	 {
	 	 public SIT_ADM_AREANIVELDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_AREANIVEL oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREANIVEL( anldescripcion, anlclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.anldescripcion, oDatos.anlclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_AREANIVEL> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREANIVEL( anldescripcion, anlclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_AREANIVEL oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.anldescripcion, oDatos.anlclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_AREANIVEL oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_AREANIVEL SET  anldescripcion = :P0 WHERE  anlclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.anldescripcion, oDatos.anlclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_AREANIVEL oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_AREANIVEL WHERE  anlclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.anlclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_AREANIVEL> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREANIVEL ";
	 	 	  return CrearListaMDL<SIT_ADM_AREANIVEL>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT anlclave as ID, ANLDESCRIPCION as DESCRIP FROM SIT_ADM_AREANIVEL";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT anlclave, ANLDESCRIPCION FROM SIT_ADM_AREANIVEL";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_ADM_AREANIVEL dmlSelectID(SIT_ADM_AREANIVEL oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREANIVEL WHERE  anlclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_AREANIVEL>(ConsultaDML ( sSQL,  oDatos.anlclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREANIVEL );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREANIVEL );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREANIVEL );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT * FROM SIT_ADM_AREANIVEL "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        /*FIN*/
 
	 }
}
