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
	 public class SIT_ADM_DIANOLABORALDao : BaseDao
	 {
	 	 public SIT_ADM_DIANOLABORALDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_DIANOLABORAL oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_DIANOLABORAL( diatipo, diaclave) VALUES (  :P0, :P1) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.diatipo, oDatos.diaclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_DIANOLABORAL> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_DIANOLABORAL( diatipo, diaclave) VALUES (  :P0, :P1) ";  
	 	 	  foreach (SIT_ADM_DIANOLABORAL oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.diatipo, oDatos.diaclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_DIANOLABORAL oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_DIANOLABORAL SET  diatipo = :P0 WHERE  diaclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.diatipo, oDatos.diaclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_DIANOLABORAL oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_DIANOLABORAL WHERE  diaclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.diaclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_DIANOLABORAL> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_DIANOLABORAL ";
	 	 	  return CrearListaMDL<SIT_ADM_DIANOLABORAL>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_DIANOLABORAL dmlSelectID(SIT_ADM_DIANOLABORAL oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_DIANOLABORAL WHERE  diaclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_DIANOLABORAL>(ConsultaDML ( sSQL,  oDatos.diaclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_DIANOLABORAL );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_DIANOLABORAL );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_DIANOLABORAL );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT diaclave, diatipo from SIT_ADM_DIANOLABORAL "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        public Dictionary<Int64, char> dmlSelectDiccionarioDiaLaboral()
        {
            Dictionary<Int64, char> dicParametros = new Dictionary<Int64, char>();
            DataTable dtDatos;
            DateTime dmDia;

            string sqlQuery = " SELECT diaclave, diatipo from SIT_ADM_DIANOLABORAL ";
            dtDatos = (DataTable)ConsultaDML(sqlQuery);

            foreach (DataRow drDatos in dtDatos.Rows)
            {
                dmDia = (DateTime)drDatos["diaclave"];
                dicParametros.Add(dmDia.Ticks, Convert.ToChar(drDatos["diatipo"]));
            }

            return dicParametros;
        }
        /*FIN*/
 
	 }
}
