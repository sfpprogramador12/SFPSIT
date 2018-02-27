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
	 public class SIT_ADM_PERFILMODDao : BaseDao
	 {
	 	 public SIT_ADM_PERFILMODDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_PERFILMOD oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_PERFILMOD( perclave, modclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.perclave, oDatos.modclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_PERFILMOD> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_PERFILMOD( perclave, modclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_PERFILMOD oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.perclave, oDatos.modclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_PERFILMOD oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_PERFILMOD oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_PERFILMOD WHERE  modclave = :P0 AND perclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.modclave, oDatos.perclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_PERFILMOD> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_PERFILMOD ";
	 	 	  return CrearListaMDL<SIT_ADM_PERFILMOD>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_PERFILMOD dmlSelectID(SIT_ADM_PERFILMOD oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_PERFILMOD WHERE  modclave = :P0 AND perclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_ADM_PERFILMOD>(ConsultaDML ( sSQL,  oDatos.modclave, oDatos.perclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILMOD );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILMOD );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILMOD );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public List<SIT_ADM_MODULO> dmlSelectModxUsr(int iClaUsu)
        {
            String sSQL = " SELECT * FROM Sit_Adm_Modulo WHERE modClave in ( "
                + " SELECT modClave FROM SIT_Adm_PerfilMod WHERE perClave in "
                + " ( SELECT perClave FROM SIT_Adm_UsrPerfil WHERE usrClave = :P0 )"
                + " GROUP BY modClave )";

            return CrearListaMDL<SIT_ADM_MODULO>(ConsultaDML(sSQL, iClaUsu) as DataTable);
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT * FROM Sit_Adm_Modulo   "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }


        /*FIN*/
 
	 }
}
