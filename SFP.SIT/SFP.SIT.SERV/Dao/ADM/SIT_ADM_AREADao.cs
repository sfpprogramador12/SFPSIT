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
	 public class SIT_ADM_AREADao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_ADM_AREADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_AREA oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_AREA"); 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREA( arafeccreacion, araclave) VALUES (  :P0, :P1) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.arafeccreacion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_AREA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREA( arafeccreacion, araclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_AREA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_AREA"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.arafeccreacion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_AREA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_AREA SET  arafeccreacion = :P0 WHERE  araclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.arafeccreacion, oDatos.araclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_AREA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_AREA WHERE  araclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.araclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_AREA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREA ";
	 	 	  return CrearListaMDL<SIT_ADM_AREA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_AREA dmlSelectID(SIT_ADM_AREA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREA WHERE  araclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_AREA>(ConsultaDML ( sSQL,  oDatos.araclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public int dmlImportarAux(List<SIT_ADM_AREA> lstDatos)
        {
            int iTotReg = 0;
            String sSQL = " INSERT INTO SIT_ADM_AREA( arafeccreacion, araclave) VALUES (  :P0, :P1) ";
            foreach (SIT_ADM_AREA oDatos in lstDatos)
            {
                EjecutaDML(sSQL, oDatos.arafeccreacion, oDatos.araclave);
                iTotReg++;
            }
            return iTotReg;


        }
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + "SELECT KA_CLAAREA,  KA_DESCRIPCION, KA_CLAVE, KA_SIGLA, KA_REPORTA, KA_ORDEN, KA_NIVEL,  AREA.KTA_CLATIPO_AREA, KTA_DESCRIPCION, KP_CLAPERFIL "
                + " from SIT_ADM_KAREA AREA, SIT_ADM_KTIPO_AREA TAREA "
                + " WHERE AREA.KTA_CLATIPO_AREA = TAREA.KTA_CLATIPO_AREA "
                + " order by KA_CLAAREA"
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        /*FIN*/
 
	 }
}
