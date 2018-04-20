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
	 public class SIT_SNT_SOLICITANTETIPODao : BaseDao
	 {
	 	 public SIT_SNT_SOLICITANTETIPODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SNT_SOLICITANTETIPO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_SOLICITANTETIPO( tsldescripcion, tslclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.tsldescripcion, oDatos.tslclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SNT_SOLICITANTETIPO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_SOLICITANTETIPO( tsldescripcion, tslclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_SNT_SOLICITANTETIPO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.tsldescripcion, oDatos.tslclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SNT_SOLICITANTETIPO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SNT_SOLICITANTETIPO SET  tsldescripcion = :P0 WHERE  tslclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.tsldescripcion, oDatos.tslclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SNT_SOLICITANTETIPO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SNT_SOLICITANTETIPO WHERE  tslclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.tslclave ); 
	 	 }
 
 
	 	 public List<SIT_SNT_SOLICITANTETIPO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_SOLICITANTETIPO ";
	 	 	  return CrearListaMDL<SIT_SNT_SOLICITANTETIPO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT tslclave as ID, TSLDESCRIPCION as DESCRIP FROM SIT_SNT_SOLICITANTETIPO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT tslclave, TSLDESCRIPCION FROM SIT_SNT_SOLICITANTETIPO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_SNT_SOLICITANTETIPO dmlSelectID(SIT_SNT_SOLICITANTETIPO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_SOLICITANTETIPO WHERE  tslclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SNT_SOLICITANTETIPO>(ConsultaDML ( sSQL,  oDatos.tslclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SNT_SOLICITANTETIPO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SNT_SOLICITANTETIPO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SNT_SOLICITANTETIPO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "WITH Resultado AS(select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from(" +
                "SELECT * from SIT_SNT_SOLICITANTETIPO order by TSLclave) a) SELECT * from Resultado WHERE recid between :P0 and :P1  ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
