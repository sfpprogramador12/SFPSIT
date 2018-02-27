using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.RESP;
 
namespace SFP.SIT.SERV.Dao.RESP
{
	 public class SIT_RESP_TEMADao : BaseDao
	 {
	 	 public SIT_RESP_TEMADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_TEMA oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_TEMA( araclave, temdescripcion, temclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.araclave, oDatos.temdescripcion, oDatos.temclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_TEMA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_TEMA( araclave, temdescripcion, temclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_RESP_TEMA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.araclave, oDatos.temdescripcion, oDatos.temclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_TEMA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_TEMA SET  araclave = :P0, temdescripcion = :P1 WHERE  temclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.araclave, oDatos.temdescripcion, oDatos.temclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_TEMA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_TEMA WHERE  temclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.temclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_TEMA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_TEMA ";
	 	 	  return CrearListaMDL<SIT_RESP_TEMA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT temclave as ID, TEMDESCRIPCION as DESCRIP FROM SIT_RESP_TEMA";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT temclave, TEMDESCRIPCION FROM SIT_RESP_TEMA";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RESP_TEMA dmlSelectID(SIT_RESP_TEMA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_TEMA WHERE  temclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_TEMA>(ConsultaDML ( sSQL,  oDatos.temclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_TEMA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_TEMA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_TEMA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public Dictionary<int, string> dmlSelectDicUnidad(int iAraClave)
        {
            String sSQL = " SELECT temclave, TEMDESCRIPCION FROM SIT_RESP_TEMA WHERE araclave = :P0";
            DataTable dtDatos = (DataTable)ConsultaDML(sSQL, iAraClave);
            Dictionary<int, string> dicCatClases = new Dictionary<int, string>();

            foreach (DataRow drDatos in dtDatos.Rows)
                dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString());
            return dicCatClases;
        }

        /*FIN*/
 
	 }
}
