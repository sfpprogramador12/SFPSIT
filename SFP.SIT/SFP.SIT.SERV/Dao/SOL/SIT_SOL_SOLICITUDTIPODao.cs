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
	 public class SIT_SOL_SOLICITUDTIPODao : BaseDao
	 {
	 	 public SIT_SOL_SOLICITUDTIPODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SOL_SOLICITUDTIPO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_SOLICITUDTIPO( sotdescripcion, sotclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.sotdescripcion, oDatos.sotclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SOL_SOLICITUDTIPO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_SOLICITUDTIPO( sotdescripcion, sotclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_SOL_SOLICITUDTIPO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.sotdescripcion, oDatos.sotclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SOL_SOLICITUDTIPO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SOL_SOLICITUDTIPO SET  sotdescripcion = :P0 WHERE  sotclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.sotdescripcion, oDatos.sotclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SOL_SOLICITUDTIPO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SOL_SOLICITUDTIPO WHERE  sotclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.sotclave ); 
	 	 }
 
 
	 	 public List<SIT_SOL_SOLICITUDTIPO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_SOLICITUDTIPO ";
	 	 	  return CrearListaMDL<SIT_SOL_SOLICITUDTIPO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT sotclave as ID, SOTDESCRIPCION as DESCRIP FROM SIT_SOL_SOLICITUDTIPO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT sotclave, SOTDESCRIPCION FROM SIT_SOL_SOLICITUDTIPO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_SOL_SOLICITUDTIPO dmlSelectID(SIT_SOL_SOLICITUDTIPO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_SOLICITUDTIPO WHERE  sotclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SOL_SOLICITUDTIPO>(ConsultaDML ( sSQL,  oDatos.sotclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SOL_SOLICITUDTIPO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SOL_SOLICITUDTIPO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SOL_SOLICITUDTIPO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
            + " select TSO_CLATIPOSOL, TSO_DESCRIPCION from SIT_SOL_SOLICITUDTIPO order by TSO_CLATIPOSOL "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
