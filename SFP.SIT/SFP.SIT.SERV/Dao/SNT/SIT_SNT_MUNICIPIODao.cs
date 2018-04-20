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
	 public class SIT_SNT_MUNICIPIODao : BaseDao
	 {
	 	 public SIT_SNT_MUNICIPIODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SNT_MUNICIPIO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_MUNICIPIO( edoclave, paiclave, munfecbaja, mundescripcion, munclave) VALUES (  :P0, :P1, :P2, :P3, :P4) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.edoclave, oDatos.paiclave, oDatos.munfecbaja, oDatos.mundescripcion, oDatos.munclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SNT_MUNICIPIO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_MUNICIPIO( edoclave, paiclave, munfecbaja, mundescripcion, munclave) VALUES (  :P0, :P1, :P2, :P3, :P4) ";  
	 	 	  foreach (SIT_SNT_MUNICIPIO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.edoclave, oDatos.paiclave, oDatos.munfecbaja, oDatos.mundescripcion, oDatos.munclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SNT_MUNICIPIO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SNT_MUNICIPIO SET  edoclave = :P0, paiclave = :P1, munfecbaja = :P2, mundescripcion = :P3 WHERE  munclave = :P4 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.edoclave, oDatos.paiclave, oDatos.munfecbaja, oDatos.mundescripcion, oDatos.munclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SNT_MUNICIPIO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SNT_MUNICIPIO WHERE  munclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.munclave ); 
	 	 }
 
 
	 	 public List<SIT_SNT_MUNICIPIO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_MUNICIPIO ";
	 	 	  return CrearListaMDL<SIT_SNT_MUNICIPIO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT munclave as ID, MUNDESCRIPCION as DESCRIP FROM SIT_SNT_MUNICIPIO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT munclave, MUNDESCRIPCION FROM SIT_SNT_MUNICIPIO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_SNT_MUNICIPIO dmlSelectID(SIT_SNT_MUNICIPIO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_MUNICIPIO WHERE  munclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SNT_MUNICIPIO>(ConsultaDML ( sSQL,  oDatos.munclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SNT_MUNICIPIO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SNT_MUNICIPIO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SNT_MUNICIPIO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT MUN.KPA_CLAPAI, KPA_DESCRIPCION, EDO.KE_CLAEST, KE_DESCRIPCION, KMU_CLAMUN, KMU_DESCRIPCION, munfecbaja "
                + " FROM SIT_SNT_MUNICIPIO MUN, SIT_SNT_PAIS PAIS, SIT_SNT_ESTADO EDO "
                + " WHERE PAIS.KPA_CLAPAI = MUN.KPA_CLAPAI "
                + " AND EDO.KE_CLAEST = MUN.KE_CLAEST "
                + " ORDER BY  KE_CLAEST, KMU_CLAMUN "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
