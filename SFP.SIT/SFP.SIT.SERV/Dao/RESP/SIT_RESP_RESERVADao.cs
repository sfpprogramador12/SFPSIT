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
	 public class SIT_RESP_RESERVADao : BaseDao
	 {
	 	 public SIT_RESP_RESERVADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RESP_RESERVA oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_RESERVA( sdoclave, rsvdias, rsvmeses, rsvaños, rsvexpediente, repclave, momclave, temclave, araclave, rsvtipoclasif, rsvplazo, rsvfecfin, rsvfecini, rsvtiporeserva) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.sdoclave, oDatos.rsvdias, oDatos.rsvmeses, oDatos.rsvaños, oDatos.rsvexpediente, oDatos.repclave, oDatos.momclave, oDatos.temclave, oDatos.araclave, oDatos.rsvtipoclasif, oDatos.rsvplazo, oDatos.rsvfecfin, oDatos.rsvfecini, oDatos.rsvtiporeserva ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RESP_RESERVA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RESP_RESERVA( sdoclave, rsvdias, rsvmeses, rsvaños, rsvexpediente, repclave, momclave, temclave, araclave, rsvtipoclasif, rsvplazo, rsvfecfin, rsvfecini, rsvtiporeserva) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13) ";  
	 	 	  foreach (SIT_RESP_RESERVA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.sdoclave, oDatos.rsvdias, oDatos.rsvmeses, oDatos.rsvaños, oDatos.rsvexpediente, oDatos.repclave, oDatos.momclave, oDatos.temclave, oDatos.araclave, oDatos.rsvtipoclasif, oDatos.rsvplazo, oDatos.rsvfecfin, oDatos.rsvfecini, oDatos.rsvtiporeserva ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RESP_RESERVA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RESP_RESERVA SET  sdoclave = :P0, rsvdias = :P1, rsvmeses = :P2, rsvaños = :P3, rsvexpediente = :P4, momclave = :P5, temclave = :P6, araclave = :P7, rsvtipoclasif = :P8, rsvplazo = :P9, rsvfecfin = :P10, rsvfecini = :P11, rsvtiporeserva = :P12 WHERE  repclave = :P13 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.sdoclave, oDatos.rsvdias, oDatos.rsvmeses, oDatos.rsvaños, oDatos.rsvexpediente, oDatos.momclave, oDatos.temclave, oDatos.araclave, oDatos.rsvtipoclasif, oDatos.rsvplazo, oDatos.rsvfecfin, oDatos.rsvfecini, oDatos.rsvtiporeserva, oDatos.repclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RESP_RESERVA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RESP_RESERVA WHERE  repclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.repclave ); 
	 	 }
 
 
	 	 public List<SIT_RESP_RESERVA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_RESERVA ";
	 	 	  return CrearListaMDL<SIT_RESP_RESERVA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RESP_RESERVA dmlSelectID(SIT_RESP_RESERVA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RESP_RESERVA WHERE  repclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RESP_RESERVA>(ConsultaDML ( sSQL,  oDatos.repclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RESP_RESERVA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RESP_RESERVA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RESP_RESERVA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public List<ReservaEdoActMdl> dmlSelectExpEdoArea(int iArea)
        {
            String sSQL = " select res.repclave, rsvExpediente, sdoclave "
                + " from sit_resp_reserva rsv, sit_resp_respuesta res "
                + " WHERE rsv.araclave = :P0 "
                + " and rsv.repclave = res.repclave ";

            return CrearListaMDL<ReservaEdoActMdl>(ConsultaDML(sSQL, iArea));
        }

        /*FIN*/
 
	 }
}
