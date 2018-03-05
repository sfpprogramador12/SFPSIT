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
	 public class SIT_ADM_AREATIPODao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_ADM_AREATIPODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_AREATIPO oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_AREATIPO"); 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREATIPO( atpdescripcion, atpclave) VALUES (  :P0, :P1) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.atpdescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_AREATIPO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_AREATIPO( atpdescripcion, atpclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_AREATIPO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_AREATIPO"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.atpdescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_AREATIPO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_AREATIPO SET  atpdescripcion = :P0 WHERE  atpclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.atpdescripcion, oDatos.atpclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_AREATIPO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_AREATIPO WHERE  atpclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.atpclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_AREATIPO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREATIPO ";
	 	 	  return CrearListaMDL<SIT_ADM_AREATIPO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT atpclave as ID, ATPDESCRIPCION as DESCRIP FROM SIT_ADM_AREATIPO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT atpclave, ATPDESCRIPCION FROM SIT_ADM_AREATIPO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_ADM_AREATIPO dmlSelectID(SIT_ADM_AREATIPO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_AREATIPO WHERE  atpclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_AREATIPO>(ConsultaDML ( sSQL,  oDatos.atpclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREATIPO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREATIPO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_AREATIPO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT * FROM SIT_ADM_AREATIPO "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
