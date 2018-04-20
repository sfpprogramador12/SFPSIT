using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.SNT;
 
namespace SFP.SIT.SERV.Dao.SNT
{
	 public class SIT_SNT_ESTADODao : BaseDao
	 {
	 	 public SIT_SNT_ESTADODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SNT_ESTADO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_ESTADO( paiclave, edofecbaja, edodescripcion, edoclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.paiclave, oDatos.edofecbaja, oDatos.edodescripcion, oDatos.edoclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SNT_ESTADO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_ESTADO( paiclave, edofecbaja, edodescripcion, edoclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_SNT_ESTADO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.paiclave, oDatos.edofecbaja, oDatos.edodescripcion, oDatos.edoclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SNT_ESTADO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SNT_ESTADO SET  paiclave = :P0, edofecbaja = :P1, edodescripcion = :P2 WHERE  edoclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.paiclave, oDatos.edofecbaja, oDatos.edodescripcion, oDatos.edoclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SNT_ESTADO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SNT_ESTADO WHERE  edoclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.edoclave ); 
	 	 }
 
 
	 	 public List<SIT_SNT_ESTADO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_ESTADO ";
	 	 	  return CrearListaMDL<SIT_SNT_ESTADO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT edoclave as ID, EDODESCRIPCION as DESCRIP FROM SIT_SNT_ESTADO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT edoclave, EDODESCRIPCION FROM SIT_SNT_ESTADO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_SNT_ESTADO dmlSelectID(SIT_SNT_ESTADO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_ESTADO WHERE  edoclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SNT_ESTADO>(ConsultaDML ( sSQL,  oDatos.edoclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SNT_ESTADO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SNT_ESTADO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SNT_ESTADO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "   WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                "SELECT EDO.edoclave, PAIS.paiclave, EDO.edodescripcion, PAIS.paidescripcion,  EDO.edofecbaja" +
                " FROM SIT_SNT_ESTADO EDO, SIT_SNT_PAIS PAIS" +
                " WHERE EDO.PAICLAVE = PAIS.paiclave ORDER BY PAIS.paiclave, EDO.edoclave ) a ) SELECT * from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        /*FIN*/
 
	 }
}
