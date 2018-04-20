using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.RED;
 
namespace SFP.SIT.SERV.Dao.RED
{
	 public class SIT_RED_NODODao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_RED_NODODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RED_NODO oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RED_NODO"); 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_NODO( perclave, nodfeclectura, nodusrausencia, usrclave, prcclave, solclave, araclave, nodcapa, nodatendido, nedclave, nodfeccreacion, nodclave, nodregresar) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.perclave, oDatos.nodfeclectura, oDatos.nodusrausencia, oDatos.usrclave, oDatos.prcclave, oDatos.solclave, oDatos.araclave, oDatos.nodcapa, oDatos.nodatendido, oDatos.nedclave, oDatos.nodfeccreacion, iSecuencia, oDatos.nodregresar); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RED_NODO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_NODO( perclave, nodfeclectura, nodusrausencia, usrclave, prcclave, solclave, araclave, nodcapa, nodatendido, nedclave, nodfeccreacion, nodclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12) ";
            foreach (SIT_RED_NODO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RED_NODO"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.perclave, oDatos.nodfeclectura, oDatos.nodusrausencia, oDatos.usrclave, oDatos.prcclave, oDatos.solclave, oDatos.araclave, oDatos.nodcapa, oDatos.nodatendido, oDatos.nedclave, oDatos.nodfeccreacion, iSecuencia, oDatos.nodregresar);
                iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RED_NODO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RED_NODO SET  perclave = :P0, nodfeclectura = :P1, nodusrausencia = :P2, usrclave = :P3, prcclave = :P4, solclave = :P5, araclave = :P6, nodcapa = :P7, nodatendido = :P8, nedclave = :P9, nodfeccreacion = :P10, nodregresar = :P11 WHERE  nodclave = :P12 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.perclave, oDatos.nodfeclectura, oDatos.nodusrausencia, oDatos.usrclave, oDatos.prcclave, oDatos.solclave, oDatos.araclave, oDatos.nodcapa, oDatos.nodatendido, oDatos.nedclave, oDatos.nodfeccreacion, oDatos.nodregresar, oDatos.nodclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RED_NODO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RED_NODO WHERE  nodclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.nodclave ); 
	 	 }
 
 
	 	 public List<SIT_RED_NODO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_NODO ";
	 	 	  return CrearListaMDL<SIT_RED_NODO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RED_NODO dmlSelectID(SIT_RED_NODO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_NODO WHERE  nodclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RED_NODO>(ConsultaDML ( sSQL,  oDatos.nodclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RED_NODO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RED_NODO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RED_NODO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/
        public const string COL_AREAS_EXCLUIR = "AREA_EXCLUIR";



        public Object dmlUpdateNodoLectura(Int64 clanodo)
        {
            String sqlQuery = " UPDATE  SIT_RED_NODO"
                    + " SET  nodfeclectura = :P0 WHERE  solclave = :P1 ";

            return EjecutaDML(sqlQuery, DateTime.Now, clanodo);
        }

        public Object dmlUpdateNodoAtendido(SIT_RED_NODO dtoDatos)
        {
            String sqlQuery = " update  SIT_RED_NODO"
                    + " SET  nodatendido = :P0 "
                    + " where  solclave = :P1 AND  nodclave = :P2 ";

            return EjecutaDML(sqlQuery, dtoDatos.nodatendido, dtoDatos.solclave, dtoDatos.nodclave);
        }

        public Object dmlUpdateNodoRegresar(SIT_RED_NODO dtoDatos)
        {
            String sqlQuery = " update  SIT_RED_NODO"
                    + " SET  nodregresar = :P0 "
                    + " where   nodclave = :P1 ";

            return EjecutaDML(sqlQuery, dtoDatos.nodregresar,  dtoDatos.nodclave);
        }


        public SIT_RED_NODO dmlSelectNodoFolioEstadoPrc(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sSQL = "SELECT solclave, nodclave, ARACLAVE, nodfeccreacion, nedclave, "
                    + " nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, prcclave, nodatendido, nodcapa, nodregresar "
                    + " from SIT_RED_NODO WHERE solclave = :P0 and nedclave = :P1 "
                    + " and prcclave = :P2 ORDER BY nodfeccreacion ";

            List<SIT_RED_NODO> lstDatos = CrearListaMDL<SIT_RED_NODO>(ConsultaDML(sSQL,
                dicParam[DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE], dicParam[DButil.SIT_RED_NODO_COL.NEDCLAVE],
                dicParam[DButil.SIT_SOL_SOLICITUD_COL.PRCCLAVE]));

            if (lstDatos.Count > 0)
                return lstDatos[0];
            else
                return null;
        }

        public SIT_RED_NODO dmlSelectNodoID(Int64 iNodo)
        {
            String sSQL = " SELECT * FROM SIT_RED_NODO WHERE  nodclave = :P0 ";
            return CrearListaMDL<SIT_RED_NODO>(ConsultaDML(sSQL, iNodo) as DataTable)[0];
        }


        public SIT_RED_NODO dmlSelectNodoExiste(SIT_RED_NODO oNodo)
        {
            String sqlQuery = " Select nodo.solclave, nodclave, ARACLAVE, nodfeccreacion, nedclave, prcclave, nodatendido, nodcapa "
                + " FROM SIT_red_nodo nodo "
                + " WHERE solclave = :P0 and nedclave = :P1 and usrClave = :P2 and nodcapa = :P3 ";

            List<SIT_RED_NODO> lstDatos = CrearListaMDL<SIT_RED_NODO>(ConsultaDML(sqlQuery, oNodo.solclave, oNodo.nedclave, oNodo.usrclave, oNodo.nodcapa));

            if (lstDatos.Count > 0)
                return lstDatos[0];
            else
                return null;
        }


        public List<SIT_RED_NODO> dmlSelectMdlAmpPendiente(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " SELECT nodclave, ARACLAVE, nodfeccreacion, nedclave, NOD_TIP, NOD_HOLGURA, "
                + " NOD_TTP,NOD_TIL,NOD_TTL,solclave, nodatendido,prcclave, nodcapa"
                + " FROM sit_red_nodo where nodclave = (  "
                + " select noddestino from sit_red_arista arista  "
                + " WHERE arista.solclave = :P0 and rtpclave = :P1 AND noddestino <> :P2 ) "
                + " and nodatendido = 0  ";

            return CrearListaMDL<SIT_RED_NODO>(ConsultaDML(sqlQuery,
                dicParam[DButil.SIT_RED_NODO_COL.SOLCLAVE], dicParam[DButil.SIT_RESP_TIPO_COL.RTPCLAVE], dicParam[DButil.SIT_RED_ARISTA_COL.NODDESTINO]));
        }


        public DataTable dmlSelectNodosGrafo(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;
            String sExcluir = "";

            List<Int32> lstArea = (List<Int32>)dicParam[COL_AREAS_EXCLUIR];
            foreach (Int32 iArea in lstArea)
            {
                sExcluir = sExcluir + "," + iArea.ToString();
            }

            String sqlQuery = " SELECT ARACLAVE FROM sit_red_arista arista,  sit_red_nodo nodo "
                + " WHERE arista.solclave = :P0 "
                + " AND ARACLAVE not in ( " + sExcluir.Substring(1) + ") "
                + " AND prcclave = :P1 "
                + " AND arista.noddestino = nodo.nodclave"
                + " GROUP BY  ARACLAVE ";

            return ConsultaDML(sqlQuery, dicParam[DButil.SIT_RED_NODO_COL.SOLCLAVE], dicParam[DButil.SIT_RED_NODO_COL.PRCCLAVE]);
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT solclave, NOD.prcclave, KRP_DESCRIPCION, NOD.KA_CLAAREA, KA_DESCRIPCION, nodclave,  nodfeccreacion,  "
                + " NOE.nedclave, KNE_DESCRIPCION, nodatendido, nodcapa, nod_holgura, nod_tip, nod_til, nod_ttp, nod_ttl "
                + " from SIT_RED_NODO NOD, SIT_ADM_KAREA AREA, SIT_RED_NODOESTADO NOE, SIT_SOL_PROCESO PRC "
                + " WHERE AREA.KA_CLAAREA = NOD.KA_CLAAREA "
                + " AND NOE.nedclave = NOD.nedclave "
                + " AND PRC.prcclave = NOD.prcclave "
                + " ORDER BY solclave, nodclave "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
