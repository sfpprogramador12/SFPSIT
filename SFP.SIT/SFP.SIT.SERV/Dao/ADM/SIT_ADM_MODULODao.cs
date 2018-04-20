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
	 public class SIT_ADM_MODULODao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_ADM_MODULODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_MODULO oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_MODULO"); 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_MODULO( modmetodo, modpadre, modfecbaja, modconsecutivo, modcontrol, moddescripcion, modclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.modmetodo, oDatos.modpadre, oDatos.modfecbaja, oDatos.modconsecutivo, oDatos.modcontrol, oDatos.moddescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_MODULO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_MODULO( modmetodo, modpadre, modfecbaja, modconsecutivo, modcontrol, moddescripcion, modclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6) ";  
	 	 	  foreach (SIT_ADM_MODULO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_MODULO"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.modmetodo, oDatos.modpadre, oDatos.modfecbaja, oDatos.modconsecutivo, oDatos.modcontrol, oDatos.moddescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_MODULO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_MODULO SET  modmetodo = :P0, modpadre = :P1, modfecbaja = :P2, modconsecutivo = :P3, modcontrol = :P4, moddescripcion = :P5 WHERE  modclave = :P6 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.modmetodo, oDatos.modpadre, oDatos.modfecbaja, oDatos.modconsecutivo, oDatos.modcontrol, oDatos.moddescripcion, oDatos.modclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_MODULO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_MODULO WHERE  modclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.modclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_MODULO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_MODULO ";
	 	 	  return CrearListaMDL<SIT_ADM_MODULO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT modclave as ID, MODDESCRIPCION as DESCRIP FROM SIT_ADM_MODULO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT modclave, MODDESCRIPCION FROM SIT_ADM_MODULO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_ADM_MODULO dmlSelectID(SIT_ADM_MODULO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_MODULO WHERE  modclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_MODULO>(ConsultaDML ( sSQL,  oDatos.modclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_MODULO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_MODULO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_MODULO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT modclave, modpadre, modconsecutivo, modDescripcion, modcontrol, modmetodo, modfecbaja   "
                + " from SIT_ADM_KMODULO "
                + " order by modclave "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }


        /*FIN*/
 
	 }
}
