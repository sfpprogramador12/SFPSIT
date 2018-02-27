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
	 public class SIT_ADM_CONFIGURACIONDao : BaseDao
	 {
	 	 public SIT_ADM_CONFIGURACIONDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_CONFIGURACION oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_CONFIGURACION( cfgfecbaja, cfgvalor, cfgsubclave, cfgclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.cfgfecbaja, oDatos.cfgvalor, oDatos.cfgsubclave, oDatos.cfgclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_CONFIGURACION> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_CONFIGURACION( cfgfecbaja, cfgvalor, cfgsubclave, cfgclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_ADM_CONFIGURACION oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.cfgfecbaja, oDatos.cfgvalor, oDatos.cfgsubclave, oDatos.cfgclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_CONFIGURACION oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_CONFIGURACION SET  cfgfecbaja = :P0, cfgvalor = :P1, cfgsubclave = :P2 WHERE  cfgclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.cfgfecbaja, oDatos.cfgvalor, oDatos.cfgsubclave, oDatos.cfgclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_CONFIGURACION oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_CONFIGURACION WHERE  cfgclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.cfgclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_CONFIGURACION> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_CONFIGURACION ";
	 	 	  return CrearListaMDL<SIT_ADM_CONFIGURACION>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_CONFIGURACION dmlSelectID(SIT_ADM_CONFIGURACION oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_CONFIGURACION WHERE  cfgclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_CONFIGURACION>(ConsultaDML ( sSQL,  oDatos.cfgclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_CONFIGURACION );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_CONFIGURACION );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_CONFIGURACION );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public String dmlSelectClave(string sConClave)
        {
            DataTable dtDatos;
            String sClave = "";

            String sqlQuery = "SELECT cfgvalor from SIT_ADM_CONFIGURACION WHERE cfgsubclave = :P0 ";
            dtDatos = (DataTable)ConsultaDML(sqlQuery, sConClave);
            foreach (DataRow row in dtDatos.Rows)
            {
                sClave = row["cfgvalor"].ToString();
            }

            return sClave;
        }


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT cfgclave, cfgclave, cfgvalor, cfgfecbaja from SIT_ADM_CONFIGURACION "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";

            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        /*FIN*/
 
	 }
}
