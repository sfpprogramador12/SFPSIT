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
	 public class SIT_ADM_PERFILNODODao : BaseDao
	 {
	 	 public SIT_ADM_PERFILNODODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_PERFILNODO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_PERFILNODO( perclave, nedclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.perclave, oDatos.nedclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_PERFILNODO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_PERFILNODO( perclave, nedclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_PERFILNODO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.perclave, oDatos.nedclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_PERFILNODO oDatos)
	 	 {
	 	 	 // NO SE IMPLEMENTA PORQUE TODO ES LLAVE 
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_PERFILNODO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_PERFILNODO WHERE  nedclave = :P0 AND perclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.nedclave, oDatos.perclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_PERFILNODO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_PERFILNODO ";
	 	 	  return CrearListaMDL<SIT_ADM_PERFILNODO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_PERFILNODO dmlSelectID(SIT_ADM_PERFILNODO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_PERFILNODO WHERE  nedclave = :P0 AND perclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_ADM_PERFILNODO>(ConsultaDML ( sSQL,  oDatos.nedclave, oDatos.perclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILNODO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILNODO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_PERFILNODO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public Dictionary<int, List<int>> dmlSelectDicPerfilNodo()
        {
            Dictionary<int, List<int>> dicDatos = new Dictionary<int, List<int>>();

            String sqlQuery = " Select perclave, nedClave FROM SIT_ADM_PERFILNODO";
            DataTable dtDatos = ConsultaDML(sqlQuery);
            int iPerClave = 0;
            int iClaNodoEdo = 0;

            if (dtDatos != null)
                foreach (DataRow drDato in dtDatos.Rows)
                {
                    iPerClave = Convert.ToInt32(drDato["perclave"]);
                    iClaNodoEdo = Convert.ToInt32(drDato["nedClave"]);

                    if (dicDatos.ContainsKey(iPerClave))
                    {
                        dicDatos[iPerClave].Add(iClaNodoEdo);
                    }
                    else
                    {
                        List<int> lstNodoEstado = new List<int>();
                        lstNodoEstado.Add(iClaNodoEdo);
                        dicDatos.Add(iPerClave, lstNodoEstado);
                    }
                }

            return dicDatos;
        }


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT * FROM SIT_ADM_PERFILNODO "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
