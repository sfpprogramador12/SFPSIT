using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.RED;
 
namespace SFP.SIT.SERV.Dao.RED
{
	 public class SIT_RED_AFDDao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_RED_AFDDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RED_AFD oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RED_AFD"); 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_AFD( afdprefijo, afdfecbaja, afddescripcion, afdclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.afdprefijo, oDatos.afdfecbaja, oDatos.afddescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RED_AFD> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_AFD( afdprefijo, afdfecbaja, afddescripcion, afdclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_RED_AFD oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RED_AFD"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.afdprefijo, oDatos.afdfecbaja, oDatos.afddescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RED_AFD oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RED_AFD SET  afdprefijo = :P0, afdfecbaja = :P1, afddescripcion = :P2 WHERE  afdclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.afdprefijo, oDatos.afdfecbaja, oDatos.afddescripcion, oDatos.afdclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RED_AFD oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RED_AFD WHERE  afdclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.afdclave ); 
	 	 }
 
 
	 	 public List<SIT_RED_AFD> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_AFD ";
	 	 	  return CrearListaMDL<SIT_RED_AFD>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT afdclave as ID, AFDDESCRIPCION as DESCRIP FROM SIT_RED_AFD";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT afdclave, AFDDESCRIPCION FROM SIT_RED_AFD";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RED_AFD dmlSelectID(SIT_RED_AFD oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_AFD WHERE  afdclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RED_AFD>(ConsultaDML ( sSQL,  oDatos.afdclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RED_AFD );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RED_AFD );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RED_AFD );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        private DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT AFDCLAVE, AFD_DESCRIPCION, AFD_FECBAJA, AFD_PREFIJO from SIT_RED_AFD "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        public Dictionary<int, string> dmlSelectDicPrefijo(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select AFDCLAVE, AFD_PREFIJO FROM SIT_RED_AFD";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["AFDCLAVE"]), row["AFD_PREFIJO"].ToString());
            }
            return dicParametros;
        }

        /*FIN*/
 
	 }
}
