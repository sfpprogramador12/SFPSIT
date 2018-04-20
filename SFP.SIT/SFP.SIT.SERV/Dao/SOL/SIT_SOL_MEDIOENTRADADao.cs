using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.SOL;
 
namespace SFP.SIT.SERV.Dao.SOL
{
	 public class SIT_SOL_MEDIOENTRADADao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_SOL_MEDIOENTRADADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SOL_MEDIOENTRADA oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_SOL_MEDIOENTRADA"); 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_MEDIOENTRADA( metfecbaja, metdescripcion, metclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.metfecbaja, oDatos.metdescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SOL_MEDIOENTRADA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_MEDIOENTRADA( metfecbaja, metdescripcion, metclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_SOL_MEDIOENTRADA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_SOL_MEDIOENTRADA"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.metfecbaja, oDatos.metdescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SOL_MEDIOENTRADA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SOL_MEDIOENTRADA SET  metfecbaja = :P0, metdescripcion = :P1 WHERE  metclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.metfecbaja, oDatos.metdescripcion, oDatos.metclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SOL_MEDIOENTRADA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SOL_MEDIOENTRADA WHERE  metclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.metclave ); 
	 	 }
 
 
	 	 public List<SIT_SOL_MEDIOENTRADA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_MEDIOENTRADA ";
	 	 	  return CrearListaMDL<SIT_SOL_MEDIOENTRADA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT metclave as ID, METDESCRIPCION as DESCRIP FROM SIT_SOL_MEDIOENTRADA";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT metclave, METDESCRIPCION FROM SIT_SOL_MEDIOENTRADA";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_SOL_MEDIOENTRADA dmlSelectID(SIT_SOL_MEDIOENTRADA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_MEDIOENTRADA WHERE  metclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SOL_MEDIOENTRADA>(ConsultaDML ( sSQL,  oDatos.metclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SOL_MEDIOENTRADA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SOL_MEDIOENTRADA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SOL_MEDIOENTRADA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + "SELECT metclave, METDESCRIPCION, MET_FECBAJA   "
                + " from SIT_SOL_MEDIOENTRADA "
                + " order by metclave "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
