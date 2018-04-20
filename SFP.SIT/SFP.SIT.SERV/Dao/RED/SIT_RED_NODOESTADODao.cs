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
	 public class SIT_RED_NODOESTADODao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_RED_NODOESTADODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RED_NODOESTADO oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RED_NODOESTADO"); 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_NODOESTADO( nedtipo, nedurl, neddescripcion, nedclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.nedtipo, oDatos.nedurl, oDatos.neddescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RED_NODOESTADO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_NODOESTADO( nedtipo, nedurl, neddescripcion, nedclave) VALUES (  :P0, :P1, :P2, :P3) ";  
	 	 	  foreach (SIT_RED_NODOESTADO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RED_NODOESTADO"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.nedtipo, oDatos.nedurl, oDatos.neddescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RED_NODOESTADO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RED_NODOESTADO SET  nedtipo = :P0, nedurl = :P1, neddescripcion = :P2 WHERE  nedclave = :P3 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.nedtipo, oDatos.nedurl, oDatos.neddescripcion, oDatos.nedclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RED_NODOESTADO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RED_NODOESTADO WHERE  nedclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.nedclave ); 
	 	 }
 
 
	 	 public List<SIT_RED_NODOESTADO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_NODOESTADO ";
	 	 	  return CrearListaMDL<SIT_RED_NODOESTADO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT nedclave as ID, NEDDESCRIPCION as DESCRIP FROM SIT_RED_NODOESTADO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT nedclave, NEDDESCRIPCION FROM SIT_RED_NODOESTADO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RED_NODOESTADO dmlSelectID(SIT_RED_NODOESTADO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_NODOESTADO WHERE  nedclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RED_NODOESTADO>(ConsultaDML ( sSQL,  oDatos.nedclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RED_NODOESTADO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RED_NODOESTADO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RED_NODOESTADO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public Dictionary<int, string> dmlSelectNodoEstado(int iPerfil)
        {
            //REVISAR
            //REVISAR
            //REVISAR
            //REVISAR
            //REVISAR
            //REVISAR
            String sSQL = " SELECT nedclave FROM SIT_RED_NODOESTADO WHERE nedclave in (SELECT nedclave FROM SIT_ADM_PERFILNODO WHERE perClave = :P0) ";
            DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
            Dictionary<int, string> dicCatClases = new Dictionary<int, string>();

            foreach (DataRow drDatos in dtDatos.Rows)
                dicCatClases.Add(iPerfil, drDatos["nedclave"].ToString());

            return dicCatClases;
        }

        public Dictionary<int, string> dmlSelectNodoEstadoUrl()
        {
            Dictionary<int, string> dicDatos = new Dictionary<int, string>();

            String sqlQuery = " Select nedclave, nedurl FROM SIT_RED_NODOESTADO";
            DataTable dtDatos = ConsultaDML(sqlQuery);

            if (dtDatos != null)
                foreach (DataRow drDato in dtDatos.Rows)
                {
                    dicDatos.Add(Convert.ToInt32(drDato["nedclave"]), drDato["nedurl"].ToString());
                }

            return dicDatos;
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( SELECT * " +
                "from SIT_RED_NODOESTADO NOE ORDER BY  nedclave ) a ) SELECT * from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/

    }
}
