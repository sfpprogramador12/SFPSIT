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
	 public class SIT_ADM_PERFILDao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_ADM_PERFILDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_PERFIL oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_PERFIL"); 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_PERFIL( permultiple, persigla, perdescripcion, perfecbaja, perclave) VALUES (  :P0, :P1, :P2, :P3, :P4) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.permultiple, oDatos.persigla, oDatos.perdescripcion, oDatos.perfecbaja, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_PERFIL> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_PERFIL( permultiple, persigla, perdescripcion, perfecbaja, perclave) VALUES (  :P0, :P1, :P2, :P3, :P4) ";  
	 	 	  foreach (SIT_ADM_PERFIL oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_PERFIL"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.permultiple, oDatos.persigla, oDatos.perdescripcion, oDatos.perfecbaja, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_PERFIL oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_PERFIL SET  permultiple = :P0, persigla = :P1, perdescripcion = :P2, perfecbaja = :P3 WHERE  perclave = :P4 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.permultiple, oDatos.persigla, oDatos.perdescripcion, oDatos.perfecbaja, oDatos.perclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_PERFIL oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_PERFIL WHERE  perclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.perclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_PERFIL> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_PERFIL ";
	 	 	  return CrearListaMDL<SIT_ADM_PERFIL>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT perclave as ID, PERDESCRIPCION as DESCRIP FROM SIT_ADM_PERFIL";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT perclave, PERDESCRIPCION FROM SIT_ADM_PERFIL";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_ADM_PERFIL dmlSelectID(SIT_ADM_PERFIL oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_PERFIL WHERE  perclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_PERFIL>(ConsultaDML ( sSQL,  oDatos.perclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFIL );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFIL );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFIL );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public Dictionary<int, SIT_ADM_PERFIL> dmlSelectDicEntidad(Object oDatos)
        {
            Dictionary<int, SIT_ADM_PERFIL> dicPerfil = new Dictionary<int, SIT_ADM_PERFIL>();
            DataTable dtDatos;
            int iPerfil;

            string sqlQuery = " Select  * FROM SIT_ADM_KPERFIL where perFecBaja IS NULL ORDER BY perClave";
            dtDatos = (DataTable)ConsultaDML(sqlQuery);
            foreach (DataRow row in dtDatos.Rows)
            {
                iPerfil = Convert.ToInt32(row["perClave"]);
                dicPerfil.Add(iPerfil, new SIT_ADM_PERFIL(Convert.ToInt32(row["perMultiple"]), row["perSigla"].ToString(),
                    row["perDescripcion"].ToString(), new DateTime(), iPerfil));

            }
            return dicPerfil;
        }


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + "SELECT *  "
                + " from SIT_ADM_PERFIL "
                + " order by PERCLAVE "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
