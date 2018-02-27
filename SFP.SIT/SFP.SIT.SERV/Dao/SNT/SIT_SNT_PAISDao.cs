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
	 public class SIT_SNT_PAISDao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_SNT_PAISDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SNT_PAIS oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_SNT_PAIS"); 
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_PAIS( paifecbaja, paidescripcion, paiclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.paifecbaja, oDatos.paidescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SNT_PAIS> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_PAIS( paifecbaja, paidescripcion, paiclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_SNT_PAIS oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_SNT_PAIS"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.paifecbaja, oDatos.paidescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SNT_PAIS oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SNT_PAIS SET  paifecbaja = :P0, paidescripcion = :P1 WHERE  paiclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.paifecbaja, oDatos.paidescripcion, oDatos.paiclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SNT_PAIS oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SNT_PAIS WHERE  paiclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.paiclave ); 
	 	 }
 
 
	 	 public List<SIT_SNT_PAIS> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_PAIS ";
	 	 	  return CrearListaMDL<SIT_SNT_PAIS>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT paiclave as ID, PAIDESCRIPCION as DESCRIP FROM SIT_SNT_PAIS";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT paiclave, PAIDESCRIPCION FROM SIT_SNT_PAIS";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_SNT_PAIS dmlSelectID(SIT_SNT_PAIS oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_PAIS WHERE  paiclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SNT_PAIS>(ConsultaDML ( sSQL,  oDatos.paiclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SNT_PAIS );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SNT_PAIS );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SNT_PAIS );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public int dmlImportarAux(List<SIT_SNT_PAIS> lstDatos)
        {
            int iTotReg = 0;
            String sSQL = " INSERT INTO SIT_SNT_PAIS( paifecbaja, paidescripcion, paiclave) VALUES (  :P0, :P1, :P2) ";
            foreach (SIT_SNT_PAIS oDatos in lstDatos)
            {
                EjecutaDML(sSQL, oDatos.paifecbaja, oDatos.paidescripcion, oDatos.paiclave);
                iTotReg++;
            }
            return iTotReg;

        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT KPA_CLAPAI, KPA_DESCRIPCION, paifecbaja   "
                + " from SIT_SNT_PAIS "
                + " order by KPA_DESCRIPCION "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        /*FIN*/
 
	 }
}
