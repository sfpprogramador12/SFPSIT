using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Sol;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Sol
{
    public class SolSeguimientoDao : BaseDao
    {
        public const int OPE_SELECT_SOLICITUDES_PENDIENTES = 211;
        public const int OPE_SELECT_SOLICITUD_SEGUIMIENTO_POR_ID = 212;
        public const int OPE_SELECT_SOLICITUD_MULTIPLE = 213;

        public const int OPE_SELECT_LI_SOLICITUDES_PENDIENTES_AREAS = 214;
        public const int OPE_SELECT_OSEG_SOLICITUDES_PENDIENTES_AREAS = 215;


        public const string COL_US_CLAFOLIO = "US_CLAFOLIO";
        public const string COL_KRP_CLAPROCESO = "KRP_CLAPROCESO";

        public const string PARAM_COL_KA_CLAAREA = "KA_CLAAREA";
        public const string PARAM_OMITIR_AREA = "OMITIR_AREA";
        
        public SolSeguimientoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {                    
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_SOLICITUDES_PENDIENTES] = new Func<Object, object>(dmlSelectSolPendientes);
            dicOperacion[OPE_SELECT_SOLICITUD_SEGUIMIENTO_POR_ID] = new Func<Object, object>(dmlSelectSeguimientoPorID);
            dicOperacion[OPE_SELECT_SOLICITUD_MULTIPLE] = new Func<Object, object>(dmlSelectSeguimientoMultiple);

            dicOperacion[OPE_SELECT_LI_SOLICITUDES_PENDIENTES_AREAS] = new Func<Object, object>(dmlSelectLiSolPendientesAreas);
            dicOperacion[OPE_SELECT_OSEG_SOLICITUDES_PENDIENTES_AREAS] = new Func<Object, object>(dmlSelectOSolPendientesAreas);
            
            

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private object dmlInsert(object oDatos)
        {
            SolSeguimientoMdl dtoDatos = (SolSeguimientoMdl)oDatos;
            String sqlQuery = "insert into SIT_SOL_SEGUIMIENTO ( "
                    + " US_CLAFOLIO, KRP_CLAPROCESO, SEG_FECINI, SEG_FECFIN, SEG_FECAMPLIACION,"
                    + " SEG_DIASSEMAFORO, SEG_DIASNOLABORALES, SEG_COLORSEMAFORO, SEG_MULTIPLE, SEG_FECCALCULO, KAR_CLATIPOARI,"
                    + " SEG_EDOPROCESO, AFD_CLAAFD, SEG_RESP_EXTERIOR, NRE_CLAARISTA, SEG_ULTIMONODO, SEG_FECESTIMADA ) VALUES ("
                    + " :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15, :P16) ";

            return EjecutaDML(sqlQuery,
                dtoDatos.us_clafolio, dtoDatos.krp_claproceso, dtoDatos.seg_fecini, dtoDatos.seg_fecfin, dtoDatos.seg_fecampliacion,
                dtoDatos.seg_diassemaforo, dtoDatos.seg_diasnolaborales, dtoDatos.seg_colorsemaforo, dtoDatos.seg_multiple, dtoDatos.seg_feccalculo,
                dtoDatos.kar_clatipoari, dtoDatos.seg_edoproceso, dtoDatos.afd_claafd, dtoDatos.seg_resp_exterior, dtoDatos.nre_claarista,
                dtoDatos.seg_ultimonodo, dtoDatos.seg_fecestimada);

        }

        private object dmlUpdate(object oDatos)
        {
            SolSeguimientoMdl dtoDatos = (SolSeguimientoMdl)oDatos;
            String sqlQuery = " update SIT_SOL_SEGUIMIENTO set SEG_FECINI = :P0, SEG_FECFIN = :P1, SEG_FECAMPLIACION = :P2,"
                    + " SEG_DIASSEMAFORO = :P3,  SEG_DIASNOLABORALES = :P4, SEG_COLORSEMAFORO = :P5, SEG_MULTIPLE = :P6,"
                    + " SEG_FECCALCULO = :P7, KAR_CLATIPOARI  = :P8 , SEG_EDOPROCESO = :P9, AFD_CLAAFD = :P10, SEG_RESP_EXTERIOR = :P11, "
                    + " NRE_CLAARISTA = :P12, SEG_ULTIMONODO = :P13, SEG_FECESTIMADA = :P14 "                    
                    + " where US_CLAFOLIO = :P15 AND krp_claproceso = :P16 ";
            return EjecutaDML(sqlQuery, dtoDatos.seg_fecini, dtoDatos.seg_fecfin, dtoDatos.seg_fecampliacion, dtoDatos.seg_diassemaforo,
                dtoDatos.seg_diasnolaborales, dtoDatos.seg_colorsemaforo, dtoDatos.seg_multiple, dtoDatos.seg_feccalculo,
                dtoDatos.kar_clatipoari, dtoDatos.seg_edoproceso, dtoDatos.afd_claafd, dtoDatos.seg_resp_exterior, dtoDatos.nre_claarista, 
                dtoDatos.seg_ultimonodo, dtoDatos.seg_fecestimada,                
                 dtoDatos.us_clafolio, dtoDatos.krp_claproceso);
        }

        private object dmlDelete(object oDatos)
        {
            SolSeguimientoMdl dtoDatos = (SolSeguimientoMdl)oDatos;
            String sqlQuery = " delete from SIT_SOL_SEGUIMIENTO where US_CLAFOLIO = :P0 AND krp_claproceso = :P1";
            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.krp_claproceso);
        }

        private object dmlImportar(object oDatos)
        {
            Int16 iContador = 0;
            List<SolSeguimientoMdl> lstDatos = (List<SolSeguimientoMdl>)oDatos;
            String sqlQuery = "insert into SIT_SOL_SEGUIMIENTO ( "
                    + " US_CLAFOLIO, KRP_CLAPROCESO, SEG_FECINI, SEG_FECFIN, SEG_FECAMPLIACION,"
                    + " SEG_DIASSEMAFORO, SEG_DIASNOLABORALES, SEG_COLORSEMAFORO, SEG_MULTIPLE, SEG_FECCALCULO, KAR_CLATIPOARI,"
                    + " SEG_EDOPROCESO, AFD_CLAAFD, SEG_RESP_EXTERIOR, NRE_CLAARISTA, SEG_ULTIMONODO, SEG_FECESTIMADA ) VALUES ("
                    + " :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15, :P16) ";

            foreach (SolSeguimientoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, 
                    dtoDatos.us_clafolio, dtoDatos.krp_claproceso, dtoDatos.seg_fecini, dtoDatos.seg_fecfin, dtoDatos.seg_fecampliacion,
                    dtoDatos.seg_diassemaforo, dtoDatos.seg_diasnolaborales, dtoDatos.seg_colorsemaforo, dtoDatos.seg_multiple, dtoDatos.seg_feccalculo,
                    dtoDatos.kar_clatipoari, dtoDatos.seg_edoproceso, dtoDatos.afd_claafd, dtoDatos.seg_resp_exterior, dtoDatos.nre_claarista,
                    dtoDatos.seg_ultimonodo, dtoDatos.seg_fecestimada);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
        private DataTable dmlSelectGrid(object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT US_CLAFOLIO, SEG_FECINI, SEG_DIASSEMAFORO, SEG_COLORSEMAFORO, SEG_FECAMPLIACION, SEG_FECFIN, SEG_MULTIPLE, SEG_DIASNOLABORALES,  "
                + " SEG_FECCALCULO, KAR_CLATIPOARI, SEG_RESP_EXTERIOR, KRP_CLAPROCESO, SEG_EDOPROCESO, AFD_CLAAFD, NRE_CLAARISTA, SEG_ULTIMONODO, SEG_FECESTIMADA "
                + " from SIT_SOL_SEGUIMIENTO order by US_CLAFOLIO, SEG_EDOPROCESO "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private SolSeguimientoMdl dmlSelectSeguimientoPorID(object oDatos)
        {
            Dictionary<string, object> oParam = (Dictionary<string, object>) oDatos;

              String sqlQuery = "select US_CLAFOLIO, krp_claproceso, SEG_FECINI, SEG_FECFIN, SEG_FECAMPLIACION, SEG_DIASSEMAFORO,"
                + " SEG_DIASNOLABORALES, SEG_COLORSEMAFORO, SEG_MULTIPLE, SEG_FECCALCULO,"
                + " KAR_CLATIPOARI, SEG_EDOPROCESO, AFD_CLAAFD, SEG_RESP_EXTERIOR ,NRE_CLAARISTA, SEG_ULTIMONODO, SEG_FECESTIMADA  "
                + " from SIT_SOL_SEGUIMIENTO where US_CLAFOLIO = :P0 and KRP_CLAPROCESO = :P1";

            List<SolSeguimientoMdl> lstSeguimiento = CrearLista(ConsultaDML(sqlQuery, oParam[COL_US_CLAFOLIO], oParam[COL_KRP_CLAPROCESO]));

            if (lstSeguimiento.Count == 0)
                return null;
            else
                return lstSeguimiento[0]; 
        }


        private object dmlSelectSeguimientoMultiple(object oDatos)
        {
            Dictionary<string, object> oParam = (Dictionary<string, object>)oDatos;

            String sqlQuery = " WITH multiple AS "
              + " ( select nodo.us_clafolio, nod_origen, count(*) as total  "
              + " from SIT_red_arista arista, SIT_red_nodo nodo  "
              + " where nodo.us_clafolio = :P0  "
              + " and nodo.nod_clanodo = arista.nod_origen "
              + " and KRP_CLAPROCESO = :P1 "
              + " group by nodo.us_clafolio, nod_origen )  "
              + " SELECT max(total) FROM multiple ";


            DataTable dtResultado = ConsultaDML(sqlQuery, oParam[COL_US_CLAFOLIO], oParam[COL_KRP_CLAPROCESO]);

            if (dtResultado.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtResultado.Rows[0][0]) > 1)
                {
                    sqlQuery = " UPDATE SIT_SOL_SEGUIMIENTO SET SEG_MULTIPLE = 1 WHERE us_clafolio = :P0 and KRP_CLAPROCESO = :P1 ";
                    return EjecutaDML(sqlQuery, oParam[COL_US_CLAFOLIO], oParam[COL_KRP_CLAPROCESO]);
                }
                else
                    return 0;
            }
            return 0;
        }

        private List<int> dmlSelectLiSolPendientesAreas(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            List <int> lstArea = new List<int>();

            String sqlQuery = " Select ka_claarea "
              + " FROM sit_sol_seguimiento seg,  SIT_red_nodo nodo "
              + " where seg_fecfin is null  "
              + " and seg.us_clafolio = nodo.us_clafolio "
              + " and nodo.nod_atendido = 0 "
              + " and nodo.ka_claarea not in (" + dicParam[PARAM_OMITIR_AREA] + ") "
              + " GROUP BY ka_claarea ";


            DataTable dtResultado = ConsultaDML(sqlQuery);
            for (int iIdx = 0; iIdx < dtResultado.Rows.Count; iIdx++)
            {
                lstArea.Add(Convert.ToInt32(dtResultado.Rows[iIdx][0]));
            }

            return lstArea;
        }

        private List<SolSeguimientoMdl> dmlSelectOSolPendientesAreas(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            List<SolSeguimientoMdl> lstSeg = new List<SolSeguimientoMdl>();

            String sqlQuery = " Select seg.us_clafolio, seg.seg_diassemaforo, seg.seg_colorsemaforo, seg.seg_fecestimada "
              + " FROM sit_sol_seguimiento seg, SIT_red_nodo nodo "
              + " where seg_fecfin is null  "
              + " and seg.us_clafolio = nodo.us_clafolio "
              + " and nodo.nod_atendido = 0 "
              + " and nodo.ka_claarea = :P0 "
              + " GROUP BY seg.us_clafolio, seg.seg_diassemaforo, seg.seg_colorsemaforo, seg.seg_fecestimada ";


            DataTable dtResultado = ConsultaDML(sqlQuery, dicParam[PARAM_COL_KA_CLAAREA]);
            for (int iIdx = 0; iIdx < dtResultado.Rows.Count; iIdx++)
            {
                SolSeguimientoMdl solSegMdl = new SolSeguimientoMdl();
                solSegMdl.us_clafolio = Convert.ToInt64(dtResultado.Rows[iIdx][0]);
                solSegMdl.seg_diassemaforo = Convert.ToInt32(dtResultado.Rows[iIdx][1]);
                solSegMdl.seg_colorsemaforo = Convert.ToInt32(dtResultado.Rows[iIdx][2]);
                solSegMdl.seg_fecestimada = Convert.ToDateTime(dtResultado.Rows[iIdx][3]);
                lstSeg.Add(solSegMdl);
            }

            return lstSeg;
        }
        private List<SolicitudSegMdl> dmlSelectSolPendientes(object oDatos)
        {

            DateTime dtFechaIni;
            DateTime dtFechaFin;
            DateTime dtFechaAmpl;
            DateTime dtFechaCal;
            DateTime dtFecEstimada;

            List<SolicitudSegMdl> lstDatos = new List<SolicitudSegMdl>();

            String sqlQuery = " select TSO_CLATIPOSOL, "
              + " SEG.KRP_CLAPROCESO, SOL.US_CLAFOLIO, SEG_FECINI, SEG_DIASSEMAFORO, SEG_COLORSEMAFORO, SEG_FECAMPLIACION, SEG_FECFIN, "
              + " SEG_MULTIPLE, SEG_DIASNOLABORALES, SEG_FECCALCULO, KAR_CLATIPOARI, SEG_RESP_EXTERIOR,  "
              + " SEG_EDOPROCESO, AFD_CLAAFD, NRE_CLAARISTA, SEG_ULTIMONODO, SEG_FECESTIMADA "
              + " from SIT_SOL_SEGUIMIENTO SEG, SIT_SOLICITUD SOL "
              + " where "
              + " SEG.US_CLAFOLIO = SOL.US_CLAFOLIO "
              + " AND SEG_FECFIN IS null ";

            DataTable dtResultado = ConsultaDML(sqlQuery);

            if (dtResultado != null)
            {
                foreach (DataRow drDato in dtResultado.Rows)
                {
                    // DUDA ACERCA DEL DATE TIME SI ES NULO LOCONTERNIMOS EN NADA.....
                    if (drDato["seg_fecini"].ToString() == "")
                        dtFechaIni = new DateTime();
                    else
                        dtFechaIni = (DateTime)drDato["seg_fecini"];

                    if (drDato["seg_fecfin"].ToString() == "")
                        dtFechaFin = new DateTime();
                    else
                        dtFechaFin = (DateTime)drDato["seg_fecfin"];

                    if (drDato["seg_fecampliacion"].ToString() == "")
                        dtFechaAmpl = new DateTime();
                    else
                        dtFechaAmpl = (DateTime)drDato["seg_fecampliacion"];

                    if (drDato["seg_feccalculo"].ToString() == "")
                        dtFechaCal = new DateTime();
                    else
                        dtFechaCal = (DateTime)drDato["seg_feccalculo"];

                    if (drDato["SEG_FECESTIMADA"].ToString() == "")
                        dtFecEstimada = new DateTime();
                    else
                        dtFecEstimada = (DateTime)drDato["SEG_FECESTIMADA"];

                    try
                    {
                        SolicitudSegMdl solSegMdl = new SolicitudSegMdl(
                            Convert.ToInt64(drDato["us_clafolio"]), Convert.ToInt32(drDato["krp_claproceso"]),
                            dtFechaIni, dtFechaFin, dtFechaAmpl, Convert.ToInt32(drDato["seg_diassemaforo"]),
                            Convert.ToInt32(drDato["seg_colorsemaforo"]), Convert.ToInt32(drDato["seg_multiple"]),
                            Convert.ToInt32(drDato["seg_diasnolaborales"]), dtFechaCal, Convert.ToInt32(drDato["kar_clatipoari"]),
                            Convert.ToInt32(drDato["seg_edoproceso"]), Convert.ToInt32(drDato["afd_claafd"]),
                            Convert.ToInt32(drDato["seg_resp_exterior"]), Convert.ToInt32(drDato["NRE_CLAARISTA"]),
                            Convert.ToInt32(drDato["SEG_ULTIMONODO"]), dtFecEstimada,
                            Convert.ToInt32(drDato["TSO_CLATIPOSOL"]));

                        lstDatos.Add(solSegMdl);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return lstDatos;
        }
        
        private List<SolSeguimientoMdl> CrearLista(DataTable dtDatos)
        {
            DateTime dtFechaIni;
            DateTime dtFechaFin;
            DateTime dtFechaAmpl;
            DateTime dtFechaCal;
            DateTime dtFecEstimada;

            List<SolSeguimientoMdl> lstDatos = new List<SolSeguimientoMdl>();

            if (dtDatos != null)
            {
                foreach (DataRow drDato in dtDatos.Rows)
                {
                    // DUDA ACERCA DEL DATE TIME SI ES NULO LOCONTERNIMOS EN NADA.....
                    if (drDato["seg_fecini"].ToString() == "")
                        dtFechaIni = new DateTime();
                    else
                        dtFechaIni = (DateTime)drDato["seg_fecini"];

                    if (drDato["seg_fecfin"].ToString() == "")
                        dtFechaFin = new DateTime();
                    else
                        dtFechaFin = (DateTime)drDato["seg_fecfin"];

                    if (drDato["seg_fecampliacion"].ToString() == "")
                        dtFechaAmpl = new DateTime();
                    else
                        dtFechaAmpl = (DateTime)drDato["seg_fecampliacion"];

                    if (drDato["seg_feccalculo"].ToString() == "")
                        dtFechaCal = new DateTime();
                    else
                        dtFechaCal = (DateTime)drDato["seg_feccalculo"];

                    if (drDato["SEG_FECESTIMADA"].ToString() == "")
                        dtFecEstimada = new DateTime();
                    else
                        dtFecEstimada = (DateTime)drDato["SEG_FECESTIMADA"];
                    
                    try
                    {
                        SolSeguimientoMdl solSegMdl = new SolSeguimientoMdl(
                            Convert.ToInt64(drDato["us_clafolio"]), Convert.ToInt32(drDato["krp_claproceso"]),
                            dtFechaIni, dtFechaFin, dtFechaAmpl, Convert.ToInt32(drDato["seg_diassemaforo"]),
                            Convert.ToInt32(drDato["seg_colorsemaforo"]), Convert.ToInt32(drDato["seg_multiple"]),
                            Convert.ToInt32(drDato["seg_diasnolaborales"]), dtFechaCal, Convert.ToInt32(drDato["kar_clatipoari"]),
                            Convert.ToInt32(drDato["seg_edoproceso"]), Convert.ToInt32(drDato["afd_claafd"]),
                            Convert.ToInt32(drDato["seg_resp_exterior"]), Convert.ToInt32(drDato["NRE_CLAARISTA"]),
                            Convert.ToInt32(drDato["SEG_ULTIMONODO"]), dtFecEstimada );
                        
                        lstDatos.Add(solSegMdl);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return lstDatos;
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
