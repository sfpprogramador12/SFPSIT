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
	 public class SIT_RESP_TIPODao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_RESP_TIPODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_TIPO oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RESP_TIPO"); 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_TIPO( rtpplazo, rtpreporta, rtptipo, rtpformato, rtpforma, rtpdescripcion, rtpclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.rtpplazo, oDatos.rtpreporta, oDatos.rtptipo, oDatos.rtpformato, oDatos.rtpforma, oDatos.rtpdescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_TIPO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_TIPO( rtpplazo, rtpreporta, rtptipo, rtpformato, rtpforma, rtpdescripcion, rtpclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6) ";  
	 	 	  foreach (SIT_RESP_TIPO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RESP_TIPO"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.rtpplazo, oDatos.rtpreporta, oDatos.rtptipo, oDatos.rtpformato, oDatos.rtpforma, oDatos.rtpdescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_TIPO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_TIPO SET  rtpplazo = :P0, rtpreporta = :P1, rtptipo = :P2, rtpformato = :P3, rtpforma = :P4, rtpdescripcion = :P5 WHERE  rtpclave = :P6 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.rtpplazo, oDatos.rtpreporta, oDatos.rtptipo, oDatos.rtpformato, oDatos.rtpforma, oDatos.rtpdescripcion, oDatos.rtpclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_TIPO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_TIPO WHERE  rtpclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.rtpclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_TIPO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_TIPO ";
	 	 	  return CrearListaMDL<SIT_RESP_TIPO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT rtpclave as ID, RTPDESCRIPCION as DESCRIP FROM SIT_RESP_TIPO";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT rtpclave, RTPDESCRIPCION FROM SIT_RESP_TIPO";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_RESP_TIPO dmlSelectID(SIT_RESP_TIPO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_TIPO WHERE  rtpclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_TIPO>(ConsultaDML ( sSQL,  oDatos.rtpclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_TIPO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_TIPO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_TIPO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public List<SIT_RESP_TIPO> dmlSelectRespTipo(int iTipo)
        {
            String sSQL = " select Rtpplazo, Rtpreporta, Rtptipo, Rtpformato, Rtpforma, Rtpdescripcion, Rtpclave from SIT_RESP_TIPO where RTPTIPO = :P0 ";

            return CrearListaMDL<SIT_RESP_TIPO>(ConsultaDML(sSQL, iTipo));
        }

        /*FIN*/

    }
}
