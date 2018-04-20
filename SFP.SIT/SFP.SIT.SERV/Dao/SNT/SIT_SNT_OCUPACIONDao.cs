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
	 public class SIT_SNT_OCUPACIONDao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_SNT_OCUPACIONDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SNT_OCUPACION oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_SNT_OCUPACION"); 
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_OCUPACION( ocufecbaja, ocudescripcion, ocuclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.ocufecbaja, oDatos.ocudescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SNT_OCUPACION> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_OCUPACION( ocufecbaja, ocudescripcion, ocuclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_SNT_OCUPACION oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_SNT_OCUPACION"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.ocufecbaja, oDatos.ocudescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SNT_OCUPACION oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SNT_OCUPACION SET  ocufecbaja = :P0, ocudescripcion = :P1 WHERE  ocuclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.ocufecbaja, oDatos.ocudescripcion, oDatos.ocuclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SNT_OCUPACION oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SNT_OCUPACION WHERE  ocuclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.ocuclave ); 
	 	 }
 
 
	 	 public List<SIT_SNT_OCUPACION> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_OCUPACION ";
	 	 	  return CrearListaMDL<SIT_SNT_OCUPACION>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT ocuclave as ID, OCUDESCRIPCION as DESCRIP FROM SIT_SNT_OCUPACION";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT ocuclave, OCUDESCRIPCION FROM SIT_SNT_OCUPACION";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_SNT_OCUPACION dmlSelectID(SIT_SNT_OCUPACION oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_OCUPACION WHERE  ocuclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SNT_OCUPACION>(ConsultaDML ( sSQL,  oDatos.ocuclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SNT_OCUPACION );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SNT_OCUPACION );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SNT_OCUPACION );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/


        public int dmlImportarAux(List<SIT_SNT_OCUPACION> lstDatos)
        {
            int iTotReg = 0;
            String sSQL = " INSERT INTO SIT_SNT_OCUPACION( ocufecbaja, ocudescripcion, ocuclave) VALUES (  :P0, :P1, :P2) ";
            foreach (SIT_SNT_OCUPACION oDatos in lstDatos)
            {
                EjecutaDML(sSQL, oDatos.ocufecbaja, oDatos.ocudescripcion, oDatos.ocuclave);
                iTotReg++;
            }
            return iTotReg;
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + "SELECT OCUCLAVE, OCUDESCRIPCION, OCUFECBAJA from SIT_SNT_OCUPACION order by ocuClave "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        /*FIN*/
 
	 }
}
