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
	 public class SIT_ADM_USRPERFILDao : BaseDao
	 {
	 	 public SIT_ADM_USRPERFILDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_USRPERFIL oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_USRPERFIL( usrclave, perclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.usrclave, oDatos.perclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_USRPERFIL> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_USRPERFIL( usrclave, perclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_USRPERFIL oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.usrclave, oDatos.perclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_USRPERFIL oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_USRPERFIL oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_USRPERFIL WHERE  perclave = :P0 AND usrclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.perclave, oDatos.usrclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_USRPERFIL> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_USRPERFIL ";
	 	 	  return CrearListaMDL<SIT_ADM_USRPERFIL>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_USRPERFIL dmlSelectID(SIT_ADM_USRPERFIL oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_USRPERFIL WHERE  perclave = :P0 AND usrclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_ADM_USRPERFIL>(ConsultaDML ( sSQL,  oDatos.perclave, oDatos.usrclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_USRPERFIL );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_USRPERFIL );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_USRPERFIL );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public List<SIT_ADM_PERFIL> dmlSelectPerfilxUsr(int iClaUsu)
        {
            String sSQL = " SELECT * FROM sit_adm_perfil WHERE perclave in ( Select PERCLAVE FROM SIT_ADM_USRPERFIL WHERE USRCLAVE = :P0 GROUP BY PERCLAVE)  ";
            return CrearListaMDL<SIT_ADM_PERFIL>(ConsultaDML(sSQL, iClaUsu));
        }


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                "SELECT u.usrNOMBRE, u.usrPATERNO, u.usrMATERNO, P.PERDESCRIPCION FROM sit_adm_perfil P, SIT_ADM_USUARIO U, SIT_ADM_USRPERFIL UP " +
                "WHERE U.USRCLAVE = UP.USRCLAVE AND P.PERCLAVE = UP.PERCLAVE ) a ) SELECT * from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
