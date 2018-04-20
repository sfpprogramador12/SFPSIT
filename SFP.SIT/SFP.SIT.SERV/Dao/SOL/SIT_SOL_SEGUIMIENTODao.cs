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
using SFP.SIT.SERV.Model.SOL;
 
namespace SFP.SIT.SERV.Dao.SOL
{
	 public class SIT_SOL_SEGUIMIENTODao : BaseDao
	 {
	 	 public SIT_SOL_SEGUIMIENTODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SOL_SEGUIMIENTO oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_SEGUIMIENTO( repclave, usrclave, segfecestimada, segultimonodo, afdclave, segedoproceso, prcclave, segfeccalculo, segdiasnolab, segmultiple, segfecfin, segfecamp, segsemaforocolor, segdiassemaforo, segfecini, solclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.repclave, oDatos.usrclave, oDatos.segfecestimada, oDatos.segultimonodo, oDatos.afdclave, oDatos.segedoproceso, oDatos.prcclave, oDatos.segfeccalculo, oDatos.segdiasnolab, oDatos.segmultiple, oDatos.segfecfin, oDatos.segfecamp, oDatos.segsemaforocolor, oDatos.segdiassemaforo, oDatos.segfecini, oDatos.solclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SOL_SEGUIMIENTO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_SEGUIMIENTO( repclave, usrclave, segfecestimada, segultimonodo, afdclave, segedoproceso, prcclave, segfeccalculo, segdiasnolab, segmultiple, segfecfin, segfecamp, segsemaforocolor, segdiassemaforo, segfecini, solclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15) ";  
	 	 	  foreach (SIT_SOL_SEGUIMIENTO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.repclave, oDatos.usrclave, oDatos.segfecestimada, oDatos.segultimonodo, oDatos.afdclave, oDatos.segedoproceso, oDatos.prcclave, oDatos.segfeccalculo, oDatos.segdiasnolab, oDatos.segmultiple, oDatos.segfecfin, oDatos.segfecamp, oDatos.segsemaforocolor, oDatos.segdiassemaforo, oDatos.segfecini, oDatos.solclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SOL_SEGUIMIENTO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SOL_SEGUIMIENTO SET  repclave = :P0, usrclave = :P1, segfecestimada = :P2, segultimonodo = :P3, afdclave = :P4, segedoproceso = :P5, segfeccalculo = :P6, segdiasnolab = :P7, segmultiple = :P8, segfecfin = :P9, segfecamp = :P10, segsemaforocolor = :P11, segdiassemaforo = :P12, segfecini = :P13 WHERE  prcclave = :P14 AND solclave = :P15 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.repclave, oDatos.usrclave, oDatos.segfecestimada, oDatos.segultimonodo, oDatos.afdclave, oDatos.segedoproceso, oDatos.segfeccalculo, oDatos.segdiasnolab, oDatos.segmultiple, oDatos.segfecfin, oDatos.segfecamp, oDatos.segsemaforocolor, oDatos.segdiassemaforo, oDatos.segfecini, oDatos.prcclave, oDatos.solclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SOL_SEGUIMIENTO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SOL_SEGUIMIENTO WHERE  prcclave = :P0 AND solclave = :P1 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.prcclave, oDatos.solclave ); 
	 	 }
 
 
	 	 public List<SIT_SOL_SEGUIMIENTO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_SEGUIMIENTO ";
	 	 	  return CrearListaMDL<SIT_SOL_SEGUIMIENTO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_SOL_SEGUIMIENTO dmlSelectID(SIT_SOL_SEGUIMIENTO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_SEGUIMIENTO WHERE  prcclave = :P0 AND solclave = :P1 ";  
	 	 	  return CrearListaMDL<SIT_SOL_SEGUIMIENTO>(ConsultaDML ( sSQL,  oDatos.prcclave, oDatos.solclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SOL_SEGUIMIENTO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SOL_SEGUIMIENTO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SOL_SEGUIMIENTO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
/*INICIO*/             
        public const string PARAM_OMITIR_AREA = "OMITIR_AREA";

        public object dmlUpdMultiple(SIT_SOL_SEGUIMIENTO oDatos)
        {
            String sqlQuery = " UPDATE SIT_SOL_SEGUIMIENTO SET segmultiple = 1 WHERE  prcclave = :P0 AND solclave = :P1 ";            
            return EjecutaDML(sqlQuery, oDatos.prcclave, oDatos.solclave);
        }



        public DataTable dmlSelectGrid(object oDatos)
        {
            BasePagMdl baseMdl = (BasePagMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT solclave, segfecini, segdiassemaforo, segsemaforocolor, segfecamp, segfecfin, segmultiple, segdiasnolab,  "
                + " segfeccalculo, rtpclave, SEG_RESP_EXTERIOR, prcclave, segedoproceso, AFDCLAVE, ariclave, segultimonodo, segfecestimada "
                + " from SIT_SOL_SEGUIMIENTO order by solclave, segedoproceso "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        public SIT_SOL_SEGUIMIENTO dmlSelectSeguimientoPorID(object oDatos)
        {
            Dictionary<string, object> oParam = (Dictionary<string, object>)oDatos;

            String sqlQuery = "select * from SIT_SOL_SEGUIMIENTO where solclave = :P0 and prcclave = :P1";

            List<SIT_SOL_SEGUIMIENTO> lstSeguimiento = CrearListaMDL<SIT_SOL_SEGUIMIENTO>(ConsultaDML(sqlQuery, 
                oParam[DButil.SIT_SOL_SEGUIMIENTO_COL.SOLCLAVE], oParam[DButil.SIT_SOL_SEGUIMIENTO_COL.PRCCLAVE]));

            if (lstSeguimiento.Count == 0)
                return null;
            else
                return lstSeguimiento[0];
        }


        public object dmlSelectSeguimientoMultiple(object oDatos)
        {
            Dictionary<string, object> oParam = (Dictionary<string, object>)oDatos;

            String sqlQuery = " WITH multiple AS "
              + " ( select nodo.solclave, nodorigen, count(*) as total  "
              + " from SIT_red_arista arista, SIT_red_nodo nodo  "
              + " where nodo.solclave = :P0  "
              + " and nodo.nodclave = arista.nodorigen "
              + " and prcclave = :P1 "
              + " group by nodo.solclave, nodorigen )  "
              + " SELECT max(total) FROM multiple ";

            DataTable dtResultado = ConsultaDML(sqlQuery, oParam[DButil.SIT_SOL_SEGUIMIENTO_COL.SOLCLAVE], oParam[DButil.SIT_SOL_SEGUIMIENTO_COL.PRCCLAVE]);

            if (dtResultado.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtResultado.Rows[0][0]) > 1)
                {
                    sqlQuery = " UPDATE SIT_SOL_SEGUIMIENTO SET segmultiple = 1 WHERE solclave = :P0 and prcclave = :P1 ";
                    return EjecutaDML(sqlQuery, oParam[DButil.SIT_SOL_SEGUIMIENTO_COL.SOLCLAVE], oParam[DButil.SIT_SOL_SEGUIMIENTO_COL.PRCCLAVE]);
                }
                else
                    return 0;
            }
            return 0;
        }

        public List<int> dmlSelectLiSolPendientesAreas(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            List<int> lstArea = new List<int>();

            String sqlQuery = " Select ARACLAVE "
              + " FROM sit_sol_seguimiento seg,  SIT_red_nodo nodo "
              + " where segfecfin is null  "
              + " and seg.solclave = nodo.solclave "
              + " and nodo.nodatendido = 0 "
              + " and nodo.ARACLAVE not in (" + dicParam[PARAM_OMITIR_AREA] + ") "
              + " GROUP BY ARACLAVE ";


            DataTable dtResultado = ConsultaDML(sqlQuery);
            for (int iIdx = 0; iIdx < dtResultado.Rows.Count; iIdx++)
            {
                lstArea.Add(Convert.ToInt32(dtResultado.Rows[iIdx][0]));
            }

            return lstArea;
        }

        public List<SolSegEnProcesoMdl> dmlSelectOSolPendientesAreas(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            List<SolSegEnProcesoMdl> lstSeg = new List<SolSegEnProcesoMdl>();

            String sqlQuery = " Select seg.solclave, seg.segdiassemaforo, seg.segsemaforocolor, seg.segfecestimada,  sotclave"
              + " FROM sit_sol_seguimiento seg, SIT_red_nodo nodo, SIT_ADM_SOLICITUD sol "
              + " where segfecfin is null  "
              + " and seg.solclave = nodo.solclave "
              + " and nodo.nodatendido = 0 "
              + " and nodo.ARACLAVE = :P0 "
              + " and sol.solclave = seg.solclave"
              + " GROUP BY seg.solclave, seg.segdiassemaforo, seg.segsemaforocolor, seg.segfecestimada,sotclave ";

            DataTable dtResultado = ConsultaDML(sqlQuery, dicParam[DButil.SIT_ADM_AREA_COL.ARACLAVE]);
            for (int iIdx = 0; iIdx < dtResultado.Rows.Count; iIdx++)
            {
                SolSegEnProcesoMdl solSegMdl = new SolSegEnProcesoMdl();
                solSegMdl.solclave = Convert.ToInt64(dtResultado.Rows[iIdx][0]);
                solSegMdl.segdiassemaforo = Convert.ToInt32(dtResultado.Rows[iIdx][1]);
                solSegMdl.segsemaforocolor = Convert.ToInt32(dtResultado.Rows[iIdx][2]);
                solSegMdl.segfecestimada = Convert.ToDateTime(dtResultado.Rows[iIdx][3]);
                solSegMdl.sotclave = Convert.ToInt32(dtResultado.Rows[iIdx][4]);
            }
            return lstSeg;
        }

        public List<SIT_SOL_SEGUIMIENTO> dmlSelectSolPendientes(object oDatos)
        {

            String sqlQuery = " select sotclave, "
              + " SEG.prcclave, SOL.solclave, segfecini, segdiassemaforo, segsemaforocolor, segfecamp, segfecfin, "
              + " segmultiple, segdiasnolab, segfeccalculo, rtpclave, SEG_RESP_EXTERIOR,  "
              + " segedoproceso, AFDCLAVE, ariclave, segultimonodo, segfecestimada "
              + " from SIT_SOL_SEGUIMIENTO SEG, SIT_SOL_SOLICITUD SOL "
              + " where "
              + " SEG.solclave = SOL.solclave "
              + " AND segfecfin IS null ";

            return CrearListaMDL<SIT_SOL_SEGUIMIENTO>(ConsultaDML(sqlQuery) as DataTable);
        }
/*FIN*/
 
	 }
}
