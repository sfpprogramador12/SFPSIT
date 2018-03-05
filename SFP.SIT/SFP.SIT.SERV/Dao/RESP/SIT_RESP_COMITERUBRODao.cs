using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.RESP;
 
namespace SFP.SIT.SERV.Dao.RESP
{
	 public class SIT_RESP_COMITERUBRODao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_RESP_COMITERUBRODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_COMITERUBRO oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RESP_COMITERUBRO"); 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_COMITERUBRO( corfecbaja, cordescripcion, corclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.corfecbaja, oDatos.cordescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_COMITERUBRO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_COMITERUBRO( corfecbaja, cordescripcion, corclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_RESP_COMITERUBRO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RESP_COMITERUBRO"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.corfecbaja, oDatos.cordescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_COMITERUBRO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_COMITERUBRO SET  corfecbaja = :P0, cordescripcion = :P1 WHERE  corclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.corfecbaja, oDatos.cordescripcion, oDatos.corclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_COMITERUBRO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_COMITERUBRO WHERE  corclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.corclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_COMITERUBRO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_COMITERUBRO ";
	 	 	  return CrearListaMDL<SIT_RESP_COMITERUBRO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT corclave as ID, CORDESCRIPCION as DESCRIP FROM SIT_RESP_COMITERUBRO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT corclave, CORDESCRIPCION FROM SIT_RESP_COMITERUBRO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RESP_COMITERUBRO dmlSelectID(SIT_RESP_COMITERUBRO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_COMITERUBRO WHERE  corclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_COMITERUBRO>(ConsultaDML ( sSQL,  oDatos.corclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_COMITERUBRO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_COMITERUBRO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_COMITERUBRO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }


        /*INICIO*/

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "  WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                "SELECT * from SIT_RESP_COMITERUBRO RUBRO" +
                " ) a ) SELECT* from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }


        /*FIN*/

    }
}
