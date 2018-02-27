using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.ADM;
 
namespace SFP.SIT.SERV.Dao.ADM
{
	 public class SIT_ADM_USUARIOAREADao : BaseDao
	 {
	 	 public SIT_ADM_USUARIOAREADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_USUARIOAREA oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_USUARIOAREA( usrclave, uarorigen, araclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.usrclave, oDatos.uarorigen, oDatos.araclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_USUARIOAREA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_USUARIOAREA( usrclave, uarorigen, araclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_ADM_USUARIOAREA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.usrclave, oDatos.uarorigen, oDatos.araclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_USUARIOAREA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_USUARIOAREA SET  uarorigen = :P0 WHERE  araclave = :P1 AND usrclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.uarorigen, oDatos.araclave, oDatos.usrclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_USUARIOAREA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_USUARIOAREA WHERE  araclave = :P0 AND usrclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.araclave, oDatos.usrclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_USUARIOAREA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_USUARIOAREA ";
	 	 	  return CrearListaMDL<SIT_ADM_USUARIOAREA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_USUARIOAREA dmlSelectID(SIT_ADM_USUARIOAREA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_USUARIOAREA WHERE  araclave = :P0 AND usrclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_ADM_USUARIOAREA>(ConsultaDML ( sSQL,  oDatos.araclave, oDatos.usrclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_USUARIOAREA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_USUARIOAREA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_USUARIOAREA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public const string PARAM_AREAS = "PARAM_AREAS";
        public const string PARAM_FECHA = "PARAM_FECHA";


        public Object dmlActualizarPerfil(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            // PRIMERO BORRAMOS LOS DATOS...

            EjecutaDML("DELETE FROM SIT_ADM_USUARIOAREA WHERE USRCLAVE = :P0", dicParam[DButil.SIT_ADM_USUARIO_COL.USRCLAVE]);


            String sqlQuery = " INSERT INTO SIT_ADM_USUARIOAREA ( USRCLAVE, araClave )"
                + " SELECT " + dicParam[DButil.SIT_ADM_USUARIO_COL.USRCLAVE] + ", araClave FROM SIT_adm_areahist   "
                + " WHERE araClave in ( " + dicParam[PARAM_AREAS] + " )";

            return EjecutaDML(sqlQuery);
        }


        public List<SIT_ADM_AREAHIST> dmlUsuarioArea(Dictionary<string, object> oDatos)
        {
            string sqlQuery = " select * from SIT_ADM_AREAHIST WHERE araclave in (SELECT araclave FROM SIT_ADM_USUARIOAREA WHERE USRCLAVE = :P0 ) "
                + " AND :P1 BETWEEN arhfecini and arhfecfin ";

            return CrearListaMDL<SIT_ADM_AREAHIST>(ConsultaDML(sqlQuery,
                oDatos[DButil.SIT_ADM_USUARIO_COL.USRCLAVE], oDatos[PARAM_FECHA]));
        }


        //////public List<Tuple<int, string>> dmlUsuAreaDesc(int iClaUsu)
        //////{
        //////    List<Tuple<int, string>> lstUsuPerArea = new List<Tuple<int, string>>();

        //////    string sqlQuery = " SELECT perClave as id, arhSiglas as text FROM SIT_ADM_USUARIOAREA up, SIT_adm_areahist a "
        //////        + " WHERE up.ARACLAVE = a.ARACLAVE and USRCLAVE = :P0 GROUP BY perClave, arhSiglas  ";

        //////    DataTable dtDatos = (DataTable)ConsultaDML(sqlQuery, iClaUsu);
        //////    for (int iIdx = 0; iIdx < dtDatos.Rows.Count; iIdx++)
        //////    {
        //////        lstUsuPerArea.Add(new Tuple<int, string>(Convert.ToInt32(dtDatos.Rows[iIdx][0]), dtDatos.Rows[iIdx][0].ToString()));
        //////    }

        //////    return lstUsuPerArea;
        //////}

        public DataTable dmlUPAarbol(int iClaUsu)
        {
            String sqlQuery = " SELECT area.araClave, araDescripcion, orgClaveReporta, area.perClave, upa.araClave as activo ";
            ////    + " FROM SIT_adm_areahist area "
            ////    + " LEFT JOIN SIT_ADM_Kperfil perfil ON area.perClave = perfil.perClave "
            ////    + " LEFT JOIN SIT_ADM_USUARIOAREA upa ON USRCLAVE = :P0  "
            ////    + " AND upa.perClave = perfil.perClave AND area.araClave = upa.araClave  "
            ////    + " WHERE "
            ////    + " ka_fecbaja is null "
            ////    + " AND perfil.perClave > 1 "
            ////    + " order by  kp_multiple, area.perClave ";        
            return (DataTable)ConsultaDML(sqlQuery, iClaUsu);
        }
        public DataTable dmlTurnarArbol(int iClaUsu)
        {
            String sqlQuery = " SELECT area.araClave, araDescripcion, orgClaveReporta, area.perClave, upa.araClave as activo ";
            ////+ " FROM SIT_adm_areahist area "
            ////+ " LEFT JOIN SIT_ADM_Kperfil perfil ON area.perClave = perfil.perClave "
            ////+ " LEFT JOIN SIT_ADM_USUARIOAREA upa ON USRCLAVE = :P0  "
            ////+ " AND upa.perClave = perfil.perClave AND area.araClave = upa.araClave "
            ////+ " WHERE ka_fecbaja is null "
            ////+ " AND perfil.perClave = " + Util.Constantes.Perfil.UA
            ////+ " order by   area.kta_clatipo_area, area.araDescripcion ";

            return (DataTable)ConsultaDML(sqlQuery, iClaUsu);
        }
        public DataTable dmlUsuarioAreas(int iClaUsu)
        {
            List<SIT_ADM_AREA> lstDatos = new List<SIT_ADM_AREA>();
            string sqlQuery = "select area.araClave as id,  area.arhSiglas as text "
            + " from  SIT_ADM_USUARIOAREA pua, SIT_adm_areahist area "
            + " WHERE pua.araClave = area.araClave and USRCLAVE = :P0 and area.araClave > 0"
            + " GROUP BY area.araClave, area.arhSiglas ORDER BY area.araClave ";

            return ConsultaDML(sqlQuery, iClaUsu);
        }

        public DataTable dmlOUsuarioAreas(int iClaArea)
        {
            List<SIT_ADM_AREA> lstDatos = new List<SIT_ADM_AREA>();

            string sqlQuery = " Select usrNombre || ' ' || usrPaterno || ' ' || usrmaterno as nombre, usrCorreo  "
                + " from SIT_ADM_USUARIO usu, SIT_ADM_USUARIOAREA ua "
                + " where ua.araClave = :P0 "
                + " and usu.USRCLAVE = ua.USRCLAVE ";

            return ConsultaDML(sqlQuery, iClaArea);
        }


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                      " SELECT UPA.USRCLAVE, UPA.araClave, usrCorreo,  araDescripcion " +
                      " from SIT_ADM_USER_AREA UPA, SIT_ADM_USUARIO US, SIT_ADM_KAREA AR " +
                      " WHERE US.USRCLAVE = UPA.USRCLAVE " +
                      "  AND AR.araClave = UPA.araClave " +
                      " order by usrCorreo,  UPA.araClave "
                      + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
