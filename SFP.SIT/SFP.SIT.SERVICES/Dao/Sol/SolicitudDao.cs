using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Sol;

using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Sol
{
    public class SolicitudDao : BaseDao
    {
        public const int OPE_UPDATE_SOLICITUD_TIPO_MEDIO_ID = 111;        
        public const int OPE_SELECT_SOLICITUD_POR_ID = 211;
        public const int OPE_SELECT_SOLICITUD_PENDIENTE = 212;
        public const int OPE_SELECT_SOLICITUD_SEGUMIENTO = 213;
        public const int OPE_SELECT_SOLICITUD_TRANSPUESTA_POR_ID = 214;

        public const string COL_KU_CLAUSU = "KU_CLAUSU";
        public const string COL_KP_CLAPERFIL = "KP_CLAPERFIL";


        public const string COL_US_CLAFOLIO = "US_CLAFOLIO";        
        ////public const string COL_KA_CLAAREA = "KA_CLAAREA";
        ////public const string COL_NRE_ARISTAESTADO = "NRE_ARISTAESTADO";        
        public const string COL_KRP_CLAPROCESO = "KRP_CLAPROCESO";

        public const string COL_DIC_NODO_ESTADO = "COL_DIC_NODO_ESTADO";
        public const string COL_TABLA_USUARIO_PERFIL = "COL_TABLA_USUARIO_PERFIL";
        public const string COL_AREAS = "COL_TABLA_USUARIO_AREA";        

        public const string TABLERO_RENGLON = "RENGLON";
        public const string TABLERO_COLUMNA = "COLUMNA";


        Int64 lSecuencia { get; set; }

        public SolicitudDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_HABILITAR] = new Func<Object, object>(dmlHabilitar);
            dicOperacion[OPE_DESHABILITAR] = new Func<Object, object>(dmlDeshabilitar);            
            dicOperacion[OPE_UPDATE_SOLICITUD_TIPO_MEDIO_ID] = new Func<Object, object>(dmlUpdateID);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_SOLICITUD_POR_ID] = new Func<Object, object>(dmlSelectExisteSolicitud);
            dicOperacion[OPE_SELECT_SOLICITUD_PENDIENTE] = new Func<Object, object>(dmlSelectSolicitudPendiente);
            dicOperacion[OPE_SELECT_SOLICITUD_SEGUMIENTO] = new Func<Object, object>(dmlSelectSolicitudSeguimiento);
            dicOperacion[OPE_SELECT_SOLICITUD_TRANSPUESTA_POR_ID] = new Func<Object, object>(dmlSelectSolicitudTranspuestaID);
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              

        private object dmlInsert(object oDatos)
        {
            /// NO SE REQUEIRE UNA SECUENCIA ES UNICO
            ////iSecuencia = SecuenciaDML("SEC_SIT_SOLICITUD");
            SolicitudMdl dtoDatos = (SolicitudMdl)oDatos;

            String sqlQuery = " insert into SIT_SOLICITUD ( "
                    + " US_FOLIO,       US_CLAFOLIO,            US_FECREC,          US_OTROMOD,     US_ARCDES, "
                    + " US_DES,         US_DAT,                 US_FECHASOL,        "
                    + " US_FECENT,      US_FECENV, "
                    + " US_FECHARESP,   US_OTRODERECHOACCESO,   IDMEDIOENTRADA,  "
                    + " IDRESPUESTA,    "
                    + " MOTIVODESECHA,  NOTIFICADO, "
                    + " tso_clatiposol, US_CLAUSU,              US_UNIENL,          IDFORMAENTREGA, US_MODENT, "
                    + " KRT_CLATEMA,            US_FECACLARACION,   krp_claproceso, us_fecrecursorevision "
                    + " ) VALUES (  "
                    + " :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9,"
                    + " :P10, :P11, :P12, :P13, :P14, :P15, :P16, :P17, :P18, :P19,"
                    + " :P20, :P21, :P22, :P23, :P24)";

            return EjecutaDML(sqlQuery,
                dtoDatos.us_folio,      dtoDatos.us_clafolio, dtoDatos.us_fecrec, dtoDatos.us_otromod, dtoDatos.us_arcdes,
                dtoDatos.us_des,        dtoDatos.us_dat, dtoDatos.us_fechasol, 
                dtoDatos.us_fecent,     dtoDatos.us_fecenv,
                dtoDatos.us_fecharesp, dtoDatos.us_otroderechoacceso, dtoDatos.idmedioentrada, 
                dtoDatos.idrespuesta, 
                dtoDatos.motivodesecha, dtoDatos.notificado, 
                dtoDatos.tso_clatiposol, dtoDatos.us_clausu, dtoDatos.us_unienl, dtoDatos.idformaentrega, dtoDatos.us_modent,
                dtoDatos.krt_clatema, dtoDatos.us_fecaclaracion, dtoDatos.krp_claproceso, dtoDatos.us_fecrecursorevision);
        }

        private object dmlUpdate(object oDatos)
        {
            SolicitudMdl dtoDatos = (SolicitudMdl)oDatos;
            String sqlQuery = " update SIT_SOLICITUD SET "
                + " US_FOLIO=:P0,       US_FECREC=:P1,              US_OTROMOD=:P2,           US_ARCDES=:P3, "
                + " US_DES=:P4,         US_DAT=:P5,                 US_FECHASOL=:P6,          "
                + " US_FECENT=:P7,      US_FECENV=:P8, "
                + " US_FECHARESP=:P9,  US_OTRODERECHOACCESO=:P10,  IDMEDIOENTRADA=:P11, "
                + " IDRESPUESTA=:P12, "
                + " MOTIVODESECHA=:P13, NOTIFICADO=:P14,  "
                + " tso_clatiposol=:P15, US_CLAUSU=:P16, US_UNIENL=:P17, IDFORMAENTREGA=:P18, US_MODENT=:P19, "
                + " KRT_CLATEMA=:P20, US_FECACLARACION=:P21, krp_claproceso = :P22, us_fecrecursorevision = :P23 "
                + " where US_CLAFOLIO = :P24 ";

            return EjecutaDML(sqlQuery,

            dtoDatos.us_folio, dtoDatos.us_fecrec, dtoDatos.us_otromod, dtoDatos.us_arcdes,
            dtoDatos.us_des, dtoDatos.us_dat, dtoDatos.us_fechasol, 
            dtoDatos.us_fecent, dtoDatos.us_fecenv,
            dtoDatos.us_fecharesp, dtoDatos.us_otroderechoacceso, dtoDatos.idmedioentrada, 
            dtoDatos.idrespuesta, 
            dtoDatos.motivodesecha, dtoDatos.notificado, 
            dtoDatos.tso_clatiposol, dtoDatos.us_clausu, dtoDatos.us_unienl, dtoDatos.idformaentrega, dtoDatos.us_modent,
            dtoDatos.krt_clatema, dtoDatos.us_fecaclaracion, dtoDatos.krp_claproceso, dtoDatos.us_fecrecursorevision,
            dtoDatos.us_clafolio);
        }

        private object dmlDelete(object oDatos)
        {
            SolicitudMdl dtoDatos = (SolicitudMdl)oDatos;
            String sqlQuery = " delete from SIT_SOLICITUD where US_CLAFOLIO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio);
        }

        private object dmlHabilitar(object oDatos)
        {
            SolicitudMdl dtoDatos = (SolicitudMdl)oDatos;
            String sqlQuery = " update SIT_SOLICITUD set DTFECHABAJA = null where US_CLAFOLIO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio);
        }

        private object dmlDeshabilitar(object oDatos)
        {
            SolicitudMdl dtoDatos = (SolicitudMdl)oDatos;
            String sqlQuery = " update SIT_SOLICITUD set DTFECHABAJA = sysdate where US_CLAFOLIO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio);
        }
        
        private object dmlUpdateID(object oDatos)
        {
            SolicitudMdl dtoDatos = (SolicitudMdl)oDatos;
            String sqlQuery = " update SIT_SOLICITUD SET "
                + " IDMEDIOENTRADA=:P0, tso_clatiposol=:P1 "
                + " where US_CLAFOLIO = :P2";

            return EjecutaDML(sqlQuery, dtoDatos.idmedioentrada, dtoDatos.tso_clatiposol, dtoDatos.us_clafolio);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////      
        private DataTable dmlSelectGrid(object oDatos)
        {

            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + "SELECT   US_FOLIO,  US_CLAFOLIO, US_FECREC,         US_OTROMOD,     US_ARCDES, "
                    + " US_DES,         US_DAT,                 US_FECHASOL,    US_FECENT,      US_FECENV, "
                    + " US_FECHARESP,   US_OTRODERECHOACCESO,   IDMEDIOENTRADA, IDRESPUESTA,    MOTIVODESECHA, "
                    + " NOTIFICADO,     tso_clatiposol,         US_CLAUSU,      US_UNIENL,      IDFORMAENTREGA, US_MODENT, "
                    + " KRT_CLATEMA,            US_FECACLARACION,   krp_claproceso, us_fecrecursorevision "
                    + " from SIT_SOLICITUD ORDER BY krp_claproceso "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private SolicitudMdl dmlSelectExisteSolicitud(object oDatos)
        {
            Dictionary<string, Object> dicParametros = (Dictionary<string, Object>)oDatos;
            String sQuery = " SELECT US_FOLIO, US_CLAFOLIO, US_FECREC, US_OTROMOD, US_ARCDES,  US_DES, US_DAT, US_FECHASOL,  " +
                " US_FECENT, US_FECENV, US_FECHARESP, US_OTRODERECHOACCESO, IDMEDIOENTRADA, " +
                " IDRESPUESTA, MOTIVODESECHA, NOTIFICADO, " +
                " tso_clatiposol, US_CLAUSU, US_UNIENL, IDFORMAENTREGA, US_MODENT, KRT_CLATEMA, US_FECACLARACION,   krp_claproceso, us_fecrecursorevision " +
                " FROM SIT_SOLICITUD WHERE US_CLAFOLIO = :P0 ";                        

            DataTable dtDatos = ConsultaDML(sQuery, Convert.ToInt64(dicParametros[COL_US_CLAFOLIO]));
            if (dtDatos.Rows.Count == 0)
                return null;
            else
            {
                List<SolicitudMdl> lstSolicitud = CrearLista(dtDatos);
                return lstSolicitud[0];
            }            
        }

        private DataTable dmlSelectSolicitudPendiente(object oDatos)
        {
            SolBuscarMdl solBuscar = (SolBuscarMdl)oDatos;
            StringBuilder sbQuery = new StringBuilder();

            ////sbQuery.Append(" WITH Resultado AS( ");
            ////sbQuery.Append(" select COUNT(*) OVER() RESULT_COUNT, rownum renID, a.* from ( ");
            sbQuery.Append(" SELECT sol.US_CLAFOLIO,  us_folio, seg_fecini, nod_feccreacion, SEG_FECESTIMADA, ");
            sbQuery.Append(" sol.tso_clatiposol AS  carac1, NVL2(us_fecaclaracion, 1,0)  AS  carac2, seg_multiple AS  carac3,  ");
            sbQuery.Append(" seg_edoproceso AS  carac4,  NVL2(seg_fecampliacion, 1,0) AS  carac5,  NVL2(us_fecrecursorevision, 1, 0) AS  carac6, ");
            sbQuery.Append(" seg_diassemaforo, seg_colorsemaforo, us_des, MEN_DESCRIPCION, us_dat, ");
            sbQuery.Append(" TSO_DESCRIPCION, KRT_RUBRO || ' ' || KRT_PLAZO_RESERVA || ' ' || KRT_FUNDAMENTO_LEGAL as KRT_RUBRO,  ");
            sbQuery.Append(" KNE_CLANODO_EDO, seg.krp_claproceso, nod.nod_clanodo, seg.afd_claafd, idmedioentrada, NRE_CLAARISTA as CLAVERESP ");
            sbQuery.Append(" FROM  SIT_SOLICITUD sol,  SIT_SOL_SEGUIMIENTO seg, SIT_red_nodo nod,  ");
            sbQuery.Append(" SIT_sol_ktipo_solicitud ts, SIT_sol_ktipo_modo_entrega te, SIT_sol_ktipo_rubro_tematico tt ");
            sbQuery.Append(" WHERE  seg.us_clafolio = sol.us_clafolio  ");
            sbQuery.Append(" AND seg.krp_claproceso = sol.krp_claproceso ");
            sbQuery.Append(" AND ts.tso_clatiposol = sol.tso_clatiposol ");
            sbQuery.Append(" AND sol.US_MODENT = te.us_modent ");
            sbQuery.Append(" AND tt.krt_clatema = sol.krt_clatema ");
            sbQuery.Append(" AND nod.us_clafolio = sol.us_clafolio ");
            sbQuery.Append(" AND nod.nod_atendido = 0 ");
            ////////sbQuery.Append(" AND nod.KNE_CLANODO_EDO <> " + AfdSFP.AfdConstantes.ESTADO.INAI_INCOMPLETA  );
            sbQuery.Append(" AND nod.ka_claarea = :P0 ");
            sbQuery.Append(" ORDER BY seg_fecini,  sol.US_CLAFOLIO ");
            ////sbQuery.Append(" ) a )  SELECT * from Resultado ");
            ////sbQuery.Append(" WHERE  renID  between :P1 and :P2 ");

            ////solBuscar.CalcularLimites();
            ////return ConsultaDML(sbQuery.ToString(), solBuscar.TipoPerfil, solBuscar.LimInf, solBuscar.LimSup);
            return ConsultaDML(sbQuery.ToString(), solBuscar.TipoPerfil);
        }

        private DataTable dmlSelectSolicitudSeguimiento(object oDatos)
        {
            SolBuscarMdl solBuscar = (SolBuscarMdl) oDatos;
            StringBuilder sbQuery = new StringBuilder();

            ////sbQuery.Append(" WITH Resultado AS( ");
            ////sbQuery.Append(" select COUNT(*) OVER() RESULT_COUNT, rownum renID, a.* from ( ");
            sbQuery.Append(" SELECT sol.US_CLAFOLIO,  us_folio, seg_fecini, us_fechasol as nod_feccreacion, SEG_FECESTIMADA,  ");
            sbQuery.Append(" sol.tso_clatiposol AS  carac1, NVL2(us_fecaclaracion, 1, 0)  AS carac2, seg_multiple AS carac3, ");
            sbQuery.Append(" seg_edoproceso AS carac4, NVL2(seg_fecampliacion, 1, 0) AS carac5, NVL2(us_fecrecursorevision, 1, 0) AS  carac6, ");
            sbQuery.Append(" seg_diassemaforo, seg_colorsemaforo, us_des, MEN_DESCRIPCION, us_dat,  TSO_DESCRIPCION,    ");
            sbQuery.Append(" KRT_RUBRO || ' ' || KRT_PLAZO_RESERVA || ' ' || KRT_FUNDAMENTO_LEGAL as KRT_RUBRO,");
            sbQuery.Append(" 0 as KNE_CLANODO_EDO, seg.krp_claproceso, 0 as nod_clanodo, seg.afd_claafd, idmedioentrada, NRE_CLAARISTA as CLAVERESP ");
            sbQuery.Append(" FROM SIT_SOLICITUD sol,  SIT_SOL_SEGUIMIENTO seg, SIT_sol_ktipo_solicitud ts, SIT_sol_ktipo_modo_entrega te, ");
            sbQuery.Append("  SIT_sol_ktipo_rubro_tematico tt ");
            sbQuery.Append(" WHERE seg.us_clafolio = sol.us_clafolio ");
            sbQuery.Append(" and seg.krp_claproceso = sol.krp_claproceso ");
            sbQuery.Append(" AND ts.tso_clatiposol = sol.tso_clatiposol ");
            sbQuery.Append(" and sol.US_MODENT = te.us_modent ");
            sbQuery.Append(" AND tt.krt_clatema = sol.krt_clatema ");
            sbQuery.Append(" AND (Select count(*) FROM SIT_red_nodo nodo where nodo.ka_claarea = :P0 ");
            sbQuery.Append(" and us_clafolio = sol.us_clafolio) > 0 ");
            //////sbQuery.Append(" and  nodo.KNE_CLANODO_EDO <> " + AfdSFP.AfdConstantes.ESTADO.INAI_INCOMPLETA + ") > 0 ");

            if (solBuscar.FolioIni > 0  && solBuscar.FolioFin == 0)
            {
                sbQuery.Append(" AND seg.US_CLAFOLIO = ");
                sbQuery.Append(solBuscar.FolioIni);
            }
            else if (solBuscar.FolioIni > 0 && solBuscar.FolioFin > 0)
            {
                sbQuery.Append(" AND seg.US_CLAFOLIO  between ");
                sbQuery.Append(solBuscar.FolioIni);
                sbQuery.Append(" and ");
                sbQuery.Append(solBuscar.FolioFin);
            }

            if (solBuscar.FechaIni  != DateTime.MinValue && solBuscar.FechaFin != DateTime.MinValue)
            {
                sbQuery.Append(" and seg.seg_fecini between to_date('");
                sbQuery.Append(solBuscar.FechaIni.ToString("yyyy/MM/dd") );
                sbQuery.Append("','yyyy/mm/dd') and to_date('");
                sbQuery.Append(solBuscar.FechaFin.ToString("yyyy/MM/dd"));
                sbQuery.Append("','yyyy/mm/dd')");

            }
            else if (solBuscar.FechaIni != DateTime.MinValue)
            {
                sbQuery.Append(" and seg.seg_fecini >= to_date('");
                sbQuery.Append(solBuscar.FechaIni.ToString("yyyy/MM/dd"));
                sbQuery.Append("','yyyy/mm/dd') ");
            }

            if (solBuscar.FechaConclusion != DateTime.MinValue)
            {
                sbQuery.Append(" and TRUNC(seg.seg_fecfin) = to_date('");
                sbQuery.Append(solBuscar.FechaConclusion.ToString("yyyy/MM/dd"));
                sbQuery.Append("','yyyy/mm/dd') ");
            }

             if (solBuscar.Periodo > 0)
            {
                sbQuery.Append(" and EXTRACT(YEAR FROM US_FECHASOL) = ");
                sbQuery.Append(solBuscar.Periodo);
            }

            if (solBuscar.SolicitudEstado > 0)
                    sbQuery.Append(" and seg.seg_fecfin is not null ");                    
            else
                sbQuery.Append(" and seg.seg_fecfin is  null ");

            if (solBuscar.TipoSolicitud > 0)
            {
                sbQuery.Append(" and sol.tso_clatiposol = ");
                sbQuery.Append(solBuscar.TipoSolicitud );
            }

            if (solBuscar.RubroTematico > 0)
            {
                sbQuery.Append(" and tt.krt_clatema = ");
                sbQuery.Append(solBuscar.RubroTematico);
            }

            if (solBuscar.TipoProceso > 0)
            {
                sbQuery.Append(" and seg.krp_claproceso = ");
                sbQuery.Append(solBuscar.TipoProceso);
            }

            if (solBuscar.Datos != null)
            {
                sbQuery.Append(" and sol.us_des like  '%");
                sbQuery.Append(solBuscar.Datos);
                sbQuery.Append("%'");
            }
            sbQuery.Append(" ORDER BY sol.US_CLAFOLIO ");
            ////sbQuery.Append(" ) a ) SELECT * from Resultado ");
            ////sbQuery.Append(" WHERE  renID  between :P1 and :P2 ");


            ////solBuscar.CalcularLimites();
            ////return ConsultaDML(sbQuery.ToString(), solBuscar.TipoPerfil, solBuscar.LimInf, solBuscar.LimSup);
            return ConsultaDML(sbQuery.ToString(), solBuscar.TipoPerfil);
        }

        private DataTable dmlSelectSolicitudTranspuestaID(Object oDatos)
        {
            Dictionary<string, Object> dicParametros = (Dictionary<string, Object>)oDatos;

            DataTable catalogoRet = new DataTable();
            catalogoRet.Columns.Add("titulo", typeof(string));
            catalogoRet.Columns.Add("valor", typeof(string));

            String sqlQuery = " SELECT us_fechasol, met_descripcion, men_descripcion, us_arcdes, us_otromod, seg_diassemaforo, "
               + " seg_colorsemaforo, US_DES, us_dat, seg_fecAmpliacion, plaz.KPZ_PLAZO, us_fecaclaracion, us_fecrecursorevision, seg_fecestimada "
               + " FROM SIT_SOL_KPROCESO_PLAZOS plaz, SIT_sol_seguimiento seg, SIT_solicitud sol  "
               + " LEFT JOIN SIT_SOL_KTIPO_MEDIO_ENTRADA me ON sol.idmedioentrada = me.idmedioentrada "
               + " LEFT JOIN SIT_SOL_KTIPO_MODO_ENTREGA mt ON sol.us_modent = mt.us_modent "
               + " LEFT JOIN SIT_SOL_KTIPO_SOLICITUD ts ON sol.tso_clatiposol = ts.tso_clatiposol "
               + " WHERE sol.us_clafolio = seg.us_clafolio  and seg.krp_claproceso = sol.krp_claproceso  and sol.us_clafolio = :P0 "
               + " and plaz.TSO_CLATIPOSOL = sol.TSO_CLATIPOSOL and plaz.KRP_CLAPROCESO = sol.KRP_CLAPROCESO and "
               + "  plaz.KPZ_TIPOPLAZO = nvl2(SEG_FECAMPLIACION, 2, 1) ";

            /* SOLO DEBE EXISTIR UN REGISTRO */

            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametros[COL_US_CLAFOLIO]);
            foreach (DataRow row in dtDatos.Rows)
            {
                catalogoRet.Rows.Add("Fecha solicitud", ((DateTime)row["us_fechasol"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA

                if (row["seg_fecestimada"].ToString() == "")
                    catalogoRet.Rows.Add("Fecha limite", "");
                else
                    catalogoRet.Rows.Add("Fecha limite", ((DateTime)row["seg_fecestimada"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA

                if (row["seg_fecAmpliacion"].ToString() == "")
                {
                    catalogoRet.Rows.Add("Fecha prorroga", "");
                    catalogoRet.Rows.Add("Plazo", row["KPZ_PLAZO"].ToString());
                }
                else
                {
                    catalogoRet.Rows.Add("Fecha prorroga", ((DateTime)row["seg_fecAmpliacion"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA    
                    catalogoRet.Rows.Add("Plazo", row["KPZ_PLAZO"].ToString());
                }

                if (row["us_fecaclaracion"].ToString() != "")
                {
                    catalogoRet.Rows.Add("Fecha aclaración", ((DateTime)row["us_fecaclaracion"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA    
                }

                if (row["us_fecrecursorevision"].ToString() != "")
                {
                    catalogoRet.Rows.Add("Fecha recurso", ((DateTime)row["us_fecrecursorevision"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA    
                }

                catalogoRet.Rows.Add("Días transcurridos", row["seg_diassemaforo"].ToString());
                catalogoRet.Rows.Add("Semáforo", row["seg_colorsemaforo"].ToString());
                catalogoRet.Rows.Add("Medio de recepción", row["met_descripcion"].ToString());
                catalogoRet.Rows.Add("Modo de entrega", row["men_descripcion"].ToString());
                catalogoRet.Rows.Add("Otra modalidad", row["us_otromod"].ToString());
                catalogoRet.Rows.Add("Descripción", row["US_DES"].ToString());
                catalogoRet.Rows.Add("Otros datos", row["us_dat"].ToString());
            }
            return catalogoRet;
        }



        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }

        private List<SolicitudMdl> CrearLista(DataTable dtDatos)
        {
            DateTime dtFechaRec;
            DateTime dtFechaSol;
            DateTime dtFechaEnt;
            DateTime dtFechaEnv;
            DateTime dtFechaResp;
            DateTime dtFechaAcla;
            DateTime dtFechaRecRev;

            List<SolicitudMdl> lstDatos = new List<SolicitudMdl>();

            foreach (DataRow drDato in dtDatos.Rows)
            {
                // DUDA ACERCA DEL DATE TIME SI ES NULO LOCONTERNIMOS EN NADA.....
                if (drDato["US_FECREC"].ToString() == "")
                    dtFechaRec = new DateTime();
                else
                    dtFechaRec = (DateTime)drDato["US_FECREC"];

                if (drDato["US_FECHASOL"].ToString() == "")
                    dtFechaSol = new DateTime();
                else
                    dtFechaSol = (DateTime)drDato["US_FECHASOL"];

                if (drDato["US_FECENT"].ToString() == "")
                    dtFechaEnt = new DateTime();
                else
                    dtFechaEnt = (DateTime)drDato["US_FECENT"];

                if (drDato["US_FECENV"].ToString() == "")
                    dtFechaEnv = new DateTime();
                else
                    dtFechaEnv = (DateTime)drDato["US_FECENV"];

                if (drDato["US_FECHARESP"].ToString() == "")
                    dtFechaResp = new DateTime();
                else
                    dtFechaResp = (DateTime)drDato["US_FECHARESP"];

                if (drDato["US_FECACLARACION"].ToString() == "")
                    dtFechaAcla = new DateTime();
                else
                    dtFechaAcla = (DateTime)drDato["US_FECACLARACION"];

                if (drDato["us_fecrecursorevision"].ToString() == "")
                    dtFechaRecRev = new DateTime();
                else
                    dtFechaRecRev = (DateTime)drDato["us_fecrecursorevision"];
                
                try
                {
                    SolicitudMdl solMdl = new SolicitudMdl();
                    solMdl.us_folio = drDato["US_FOLIO"].ToString();
                    solMdl.us_clafolio = Convert.ToInt64(drDato["US_CLAFOLIO"]);
                    solMdl.us_fecrec = dtFechaRec;
                    solMdl.us_otromod = drDato["US_OTROMOD"].ToString();
                    solMdl.us_arcdes = drDato["US_ARCDES"].ToString();
                    solMdl.us_des = drDato["US_DES"].ToString();
                    solMdl.us_dat = drDato["US_DAT"].ToString();
                    solMdl.us_fechasol = dtFechaSol;
                    solMdl.us_fecent = dtFechaEnt;
                    solMdl.us_fecenv = dtFechaEnv;
                    solMdl.us_fecharesp = dtFechaResp;
                    solMdl.us_otroderechoacceso = drDato["US_OTRODERECHOACCESO"].ToString();
                    solMdl.idmedioentrada = Convert.ToInt32(drDato["IDMEDIOENTRADA"]);
                    solMdl.idrespuesta = Convert.ToInt32(drDato["IDRESPUESTA"]);
                    solMdl.motivodesecha = drDato["MOTIVODESECHA"].ToString();
                    solMdl.notificado = Convert.ToInt32(drDato["NOTIFICADO"]);
                    solMdl.tso_clatiposol = Convert.ToInt32(drDato["tso_clatiposol"]);
                    solMdl.us_clausu = Convert.ToInt64(drDato["US_CLAUSU"]);
                    solMdl.us_unienl = Convert.ToInt32(drDato["US_UNIENL"]);
                    solMdl.idformaentrega = Convert.ToInt32(drDato["IDFORMAENTREGA"]);
                    solMdl.us_modent = Convert.ToInt32(drDato["US_MODENT"]);
                    solMdl.us_fecaclaracion = dtFechaAcla;
                    solMdl.krt_clatema = Convert.ToInt32(drDato["KRT_CLATEMA"]);
                    solMdl.krp_claproceso = Convert.ToInt32(drDato["krp_claproceso"]);
                    solMdl.us_fecrecursorevision = dtFechaRecRev;

                    lstDatos.Add(solMdl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (lstDatos.Count == 0)
                return null;
            else
                return lstDatos;
        }
    }
}
