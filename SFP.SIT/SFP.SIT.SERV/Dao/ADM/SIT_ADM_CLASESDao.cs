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
	 public class SIT_ADM_CLASESDao : BaseDao
	 {
	 	 public SIT_ADM_CLASESDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_CLASES oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_CLASES( clanombre, cladescripcion, claclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.clanombre, oDatos.cladescripcion, oDatos.claclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_CLASES> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_CLASES( clanombre, cladescripcion, claclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_ADM_CLASES oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.clanombre, oDatos.cladescripcion, oDatos.claclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_CLASES oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_CLASES SET  clanombre = :P0, cladescripcion = :P1 WHERE  claclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.clanombre, oDatos.cladescripcion, oDatos.claclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_CLASES oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_CLASES WHERE  claclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.claclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_CLASES> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_CLASES ";
	 	 	  return CrearListaMDL<SIT_ADM_CLASES>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT claclave as ID, CLADESCRIPCION as DESCRIP FROM SIT_ADM_CLASES";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT claclave, CLADESCRIPCION FROM SIT_ADM_CLASES";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_ADM_CLASES dmlSelectID(SIT_ADM_CLASES oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_CLASES WHERE  claclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_CLASES>(ConsultaDML ( sSQL,  oDatos.claclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_CLASES );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_CLASES );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_CLASES );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public Dictionary<int, string> dmlSelectDiccionarioNombre()
        {
            String sSQL = " SELECT CLACLAVE, CLANOMBRE FROM SIT_ADM_CLASES";
            DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
            Dictionary<int, string> dicCatClases = new Dictionary<int, string>();

            foreach (DataRow drDatos in dtDatos.Rows)
                dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString());
            return dicCatClases;
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT * FROM SIT_ADM_CLASES "
                + " ) a ) SELECT * from SIT_ADM_AREATIPO  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
