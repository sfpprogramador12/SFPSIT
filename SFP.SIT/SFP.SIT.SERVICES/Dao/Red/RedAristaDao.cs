using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Red;
using SFP.SIT.SERVICES.Util;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Red
{
    public class RedAristaDao : BaseDao
    {
        public const int OPE_UPDATE_FECLECTURA = 111;
        public const int OPE_UPDATE_ARISTAESTADO = 112;
        public const int OPE_UPDATE_ARISTA_HITO = 113;
        public const int OPE_UPDATE_ARISTA_MENSAJE = 114;

        public const int OPE_SELECT_GANTT = 211;
        public const int OPE_SELECT_AREA_FALTANTE = 212;
        public const int OPE_SELECT_ARISTA = 213;
        public const int OPE_SELECT_ARISTA_ID = 214;
        public const int OPE_SELECT_ARISTA_SIN_RESPUESTA = 215;
        public const int OPE_SELECT_ARISTA_EN_PRORROGA = 216;
        //////public const int OPE_SELECT_ARISTA_CI_RESP_AREAS = 217;
        public const int OPE_SELECT_ARISTA_POR_NODO_DESTNO = 218;
        public const int OPE_SELECT_ARISTA_CI_PENDIENTES_NO_PRORROGA = 219;
        public const int OPE_SELECT_ARISTA_PENDIENTES = 220;
        public const int OPE_SELECT_ARISTA_POR_ARISTA_ORIGEN = 221;
        public const int OPE_SELECT_AREAS_SIN_RESPUESTA = 223;
        public const int OPE_SELECT_ARISTA_FOLIO_TIPOARISTA_TRANSPUESTA = 224;
        public const int OPE_SELECT_SEGUIMIENTO = 225;
        public const int OPE_SELECT_ARISTA_FOLIO_NODO_DESTNO = 226;
        public const int OPE_SELECT_ARISTA_RESP_DOCUMENTO = 227;
        public const int OPE_SELECT_ARISTA_POR_NODO_ORIGEN = 228;

        public const int OPE_SELECT_ARISTA_RESP_PREV_REC_REVISION = 229;
        public const int OPE_SELECT_ARISTA_ACCION_AREA_PREV = 230;
        public const int OPE_SELECT_ARISTA_RESP_DOCUMENTO_ANT = 231;
       
        public const int OPE_SELECT_DIC_ARISTA_OBSERVACION_INFOADICIONAL_RESPUESTA = 232;

        public const int OPE_SELECT_AREAS_SIN_RESPUESTA_CORREOS = 233;


        public const string COL_ARJ_ORIGEN = "ARJ_ORIGEN";
        public const string COL_KA_CLAAREA = "KA_CLAAREA";
        public const string COL_KAR_CLATIPOARI = "KAR_CLATIPOARI";
        public const string COL_KAR_CLATIPOARI_PRORROGA = "KAR_CLATIPOARI_PRORROGA";
        public const string COL_KAR_CLATIPOARI_SIN_RESPUESTA = "KAR_CLATIPOARI_SIN_RESPUESTA";
        public const string COL_KNE_CLANODO_EDO = "KNE_CLANODO_EDO";
        public const string COL_KP_CLAPERFIL = "KP_CLAPERFIL";
        public const string COL_KRP_CLAPROCESO = "KRP_CLAPROCESO";        
        public const string COL_NOD_CAPA = "NOD_CAPA";
        public const string COL_NOD_DESTINO = "NOD_DESTINO";
        public const string COL_NOD_ORIGEN = "NOD_ORIGEN";
        public const string COL_NOD_CLANODO = "NOD_CLANODO";


        public const string COL_NRE_CLAARISTA = "NRE_CLAARISTA";
        public const string COL_US_CLAFOLIO = "US_CLAFOLIO";
        public const string PARAM_NO_ESTADOS = "NO_ESTADOS";

        public const string PARAM_ARITIPO_INFOADI = "INFOADI";
        public const string PARAM_ARITIPO_INFOADI_RESP = "INFOADI_RESP";


        public const string COL_RESTRICCION_TIPOARISTA = "RESTRICCION_TIPO_ARISTA";
        public Int64 iSecuencia { get; private set; }

        public RedAristaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            dicOperacion[OPE_UPDATE_FECLECTURA] = new Func<Object, object>(dmlUpdateFecLectura);
            dicOperacion[OPE_UPDATE_ARISTA_HITO] = new Func<Object, object>(dmlUpdateAristaHito);
            dicOperacion[OPE_UPDATE_ARISTA_MENSAJE] = new Func<Object, object>(dmlUpdateAristaMsj);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);            
            dicOperacion[OPE_SELECT_GANTT] = new Func<Object, object>(dmlSelectGantt);
            dicOperacion[OPE_SELECT_ARISTA] = new Func<Object, object>(dmlSelectAristaAclaracion);
            dicOperacion[OPE_SELECT_ARISTA_ID] = new Func<Object, object>(dmlSelectAristaID);
            dicOperacion[OPE_SELECT_ARISTA_SIN_RESPUESTA] = new Func<Object, object>(dmlSelectAristaSinRespuesta);
            dicOperacion[OPE_SELECT_ARISTA_EN_PRORROGA] = new Func<Object, object>(dmlSelectAristasEnProrroga);
            //////dicOperacion[OPE_SELECT_ARISTA_CI_RESP_AREAS] = new Func<Object, object>(dmlSelectCIrespAreas);
            dicOperacion[OPE_SELECT_ARISTA_POR_NODO_DESTNO] = new Func<Object, object>(dmlSelectAristaNodoDestino);
            dicOperacion[OPE_SELECT_ARISTA_POR_NODO_ORIGEN] = new Func<Object, object>(dmlSelectAristaNodoOrigen);
            

            dicOperacion[OPE_SELECT_ARISTA_CI_PENDIENTES_NO_PRORROGA] = new Func<Object, object>(dmlSelectAristaCIpendientesNOprorroga);
            dicOperacion[OPE_SELECT_ARISTA_PENDIENTES] = new Func<Object, object>(dmlSelectAristaPendientes);

            dicOperacion[OPE_SELECT_ARISTA_FOLIO_NODO_DESTNO] = new Func<Object, object>(dmlSelectAristaFolioDestino);
            dicOperacion[OPE_SELECT_ARISTA_POR_ARISTA_ORIGEN] = new Func<Object, object>(dmlSelectAristaPorAristaOrigen);
            dicOperacion[OPE_SELECT_AREAS_SIN_RESPUESTA] = new Func<Object, object>(dmlSelectAreasFaltantes);
            dicOperacion[OPE_SELECT_ARISTA_FOLIO_TIPOARISTA_TRANSPUESTA] = new Func<Object, object>(dmlSelectAristaFolioTipoAristaTrans);
            dicOperacion[OPE_SELECT_SEGUIMIENTO] = new Func<Object, object>(dmlSelectSeguimiento);
            dicOperacion[OPE_SELECT_ARISTA_RESP_DOCUMENTO] = new Func<Object, object>(dmlSelectRespDoc);
            dicOperacion[OPE_SELECT_ARISTA_RESP_PREV_REC_REVISION] = new Func<Object, object>(dmlSelectRespPrevRecRevision);

            dicOperacion[OPE_SELECT_ARISTA_ACCION_AREA_PREV] = new Func<Object, object>(dmlSelectAristaAccionPreviaArea);
            dicOperacion[OPE_SELECT_ARISTA_RESP_DOCUMENTO_ANT] = new Func<Object, object>(dmlSelectRespDocAristaAnt);
            dicOperacion[OPE_SELECT_DIC_ARISTA_OBSERVACION_INFOADICIONAL_RESPUESTA] = new Func<Object, object>(dmlSelectDicInfoAdicResp);


            dicOperacion[OPE_SELECT_AREAS_SIN_RESPUESTA_CORREOS] = new Func<Object, object>(dmlSelectAreasFaltantesCorreos);
            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            iSecuencia = SecuenciaDML("SEC_SIT_RED_ARISTA");

            RedAristaMdl dtoDatos = (RedAristaMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_RED_ARISTA ( US_CLAFOLIO, NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA,  "
                    + " NRE_FECINI, NRE_FECFIN, NRE_OBSERVACION, NRE_DIAS_LABORALES, NRE_DIAS_NATURALES,"
                    + " kar_clatipoari, nre_feclectura, NRE_HITO, US_MODENT, NRE_FECLECUSU ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14) ";

            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nod_origen, dtoDatos.nod_destino, dtoDatos.ku_clausu, iSecuencia,
                    dtoDatos.nre_fecini, dtoDatos.nre_fecfin, dtoDatos.nre_observacion, dtoDatos.nre_dias_laborales, dtoDatos.nre_dias_naturales,
                    dtoDatos.kar_clatipoari,  dtoDatos.nre_feclectura, dtoDatos.nre_hito, dtoDatos.us_modent, dtoDatos.nre_feclecusu);
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedAristaMdl dtoDatos = (RedAristaMdl)oDatos;
            String sqlQuery = " update  SIT_RED_ARISTA"
                    + " SET NRE_FECINI = :P0, NRE_FECFIN = :P1, NRE_OBSERVACION = :P2, NRE_DIAS_LABORALES = :P3, NRE_DIAS_NATURALES = :P4, kar_clatipoari= :P5,"
                    + " KU_CLAUSU= :P6, NOD_ORIGEN = :P7, NOD_DESTINO = :P8, NRE_HITO = :P9, US_MODENT = :P10, NRE_FECLECUSU = :P11, nre_feclectura = :P12  "
                    + " where  US_CLAFOLIO = :P13  AND NRE_CLAARISTA = :P14 ";

            return EjecutaDML(sqlQuery, dtoDatos.nre_fecini, dtoDatos.nre_fecfin, dtoDatos.nre_observacion,
                dtoDatos.nre_dias_laborales, dtoDatos.nre_dias_naturales, dtoDatos.kar_clatipoari, dtoDatos.ku_clausu,
                dtoDatos.nod_origen, dtoDatos.nod_destino, dtoDatos.nre_hito, dtoDatos.us_modent, dtoDatos.nre_feclecusu, dtoDatos.nre_feclectura,
                dtoDatos.us_clafolio,  dtoDatos.nre_claarista);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedAristaMdl dtoDatos = (RedAristaMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_ARISTA where  US_CLAFOLIO = :P0 AND  NOD_ORIGEN = :P1 AND NOD_DESTINO = :P2 AND NRE_CLAARISTA = :P3 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nod_origen, dtoDatos.nod_destino, dtoDatos.nre_claarista);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedAristaMdl> lstDatos = (List<RedAristaMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_RED_ARISTA ( US_CLAFOLIO, NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA,  "
                    + " NRE_FECINI, NRE_FECFIN, NRE_OBSERVACION, NRE_DIAS_LABORALES, NRE_DIAS_NATURALES, "
                    + " kar_clatipoari, nre_feclectura, NRE_HITO, US_MODENT, NRE_FECLECUSU ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, null, :P11, :P12, :P13) ";

            foreach (RedAristaMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nod_origen, dtoDatos.nod_destino, dtoDatos.ku_clausu, dtoDatos.nre_claarista,
                        dtoDatos.nre_fecini, dtoDatos.nre_fecfin, dtoDatos.nre_observacion, dtoDatos.nre_dias_laborales,
                        dtoDatos.nre_dias_naturales, dtoDatos.kar_clatipoari, dtoDatos.nre_hito,
                         dtoDatos.us_modent, dtoDatos.nre_feclecusu);
                iContador++;
            }
            return iContador;
        }

        private Object dmlUpdateFecLectura(Object oDatos)
        {
            RedAristaMdl dtoDatos = (RedAristaMdl) oDatos;
            String sqlQuery = " update  SIT_RED_ARISTA SET nre_feclectura = :P0 "
                    + " where  US_CLAFOLIO = :P1 AND NRE_CLAARISTA = :P2 ";

            return EjecutaDML(sqlQuery, dtoDatos.nre_feclectura,
                    dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }

        private Object dmlUpdateAristaHito(Object oDatos)
        {
            RedAristaMdl dtoDatos = (RedAristaMdl)oDatos;
            String sqlQuery = " update  SIT_RED_ARISTA SET nre_hito = :P0 "
                    + " where  US_CLAFOLIO = :P1 AND NRE_CLAARISTA = :P2 ";

            return EjecutaDML(sqlQuery, dtoDatos.nre_hito,
                    dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }

        private Object dmlUpdateAristaMsj(Object oDatos)
        {
            RedAristaMdl dtoDatos = (RedAristaMdl)oDatos;
            String sqlQuery = " update  SIT_RED_ARISTA SET nre_feclectura = :P0, nre_feclecusu = :P1  "
                    + " where  US_CLAFOLIO = :P2 AND NRE_CLAARISTA = :P3 ";
           
            return EjecutaDML(sqlQuery, dtoDatos.nre_feclectura, dtoDatos.nre_feclecusu, dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }

        

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////       
        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " select ARI.US_CLAFOLIO, NRE_CLAARISTA, USU.KU_CLAUSU, KU_NOMBRE || ' ' || ku_paterno || ' ' || ku_materno as KU_USUARIO, NRE_FECLECUSU,   "
                    + " NOD_ORIGEN, AORI.KA_DESCRIPCION as ORIGEN, ARI.KAR_CLATIPOARI, KAR_DESCRIPCION, NOD_DESTINO, ADES.KA_DESCRIPCION as DESTINO,   "
                    + " NRE_FECINI, NRE_FECFIN, NRE_DIAS_LABORALES,   "
                    + " NRE_FECLECTURA, NRE_DIAS_NATURALES, NRE_OBSERVACION, NRE_HITO,  US_MODENT  "
                    + " from SIT_RED_ARISTA ARI, SIT_RED_KTIPO_ARISTA TARI, SIT_RED_NODO ORI, SIT_RED_NODO DES, SIT_ADM_KAREA AORI, SIT_ADM_KAREA ADES, SIT_ADM_USUARIO USU  "
                    + " WHERE TARI.KAR_CLATIPOARI = ARI.KAR_CLATIPOARI  "
                    + " AND ORI.NOD_CLANODO = NOD_ORIGEN  "
                    + " AND DES.NOD_CLANODO = NOD_DESTINO  "
                    + " AND AORI.KA_CLAAREA = ORI.KA_CLAAREA  "
                    + " AND ADES.KA_CLAAREA = Des.KA_CLAAREA  "
                    + " AND USU.KU_CLAUSU = ARI.KU_CLAUSU  "
                    + " ORDER BY US_CLAFOLIO, NRE_CLAARISTA "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        public DataTable dmlSelectGantt(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            /*
                            + " TO_CHAR (NRE_FECINI, 'YYYY-MM-DD HH24:MI:SS') as NRE_FECINI,"
                            + " TO_CHAR (NRE_FECFIN, 'YYYY-MM-DD HH24:MI:SS') as NRE_FECFIN,"
           */
            String sqlQuery = "SELECT NRE_CLAARISTA, NOD_ORIGEN, AREA.KA_SIGLA as ORIGEN, KAR_DESCRIPCION,   NOD_DESTINO, AREA2.KA_SIGLA as DESTINO, "
                    + " TO_CHAR (NRE_FECINI, 'YYYY-MM-DD') as NRE_FECINI,"
                    + " TO_CHAR (NRE_FECFIN, 'YYYY-MM-DD') as NRE_FECFIN,"
                    + " NRE_DIAS_LABORALES, NRE_OBSERVACION,   KU_NOMBRE || ' ' || KU_PATERNO || ' ' || KU_MATERNO as Responsable, NRE_FECLECUSU "
                    + " FROM SIT_RED_ARISTA R, SIT_ADM_USUARIO U, SIT_RED_KTIPO_ARISTA AEDO,SIT_RED_NODO N,  SIT_ADM_KAREA AREA, "
                    + " SIT_RED_NODO N2, SIT_ADM_KAREA AREA2 "
                    + " WHERE R.US_CLAFOLIO = :P0 " 
                    + " AND N.NOD_CLANODO = R.NOD_ORIGEN AND "
                    + " R.US_CLAFOLIO = N.US_CLAFOLIO AND AREA.KA_CLAAREA = N.KA_CLAAREA AND "
                    + " N2.NOD_CLANODO = R.NOD_DESTINO AND "
                    + " R.US_CLAFOLIO = N2.US_CLAFOLIO AND AREA2.KA_CLAAREA = N2.KA_CLAAREA AND "
                    + " U.KU_CLAUSU = R.KU_CLAUSU AND AEDO.kar_clatipoari = R.kar_clatipoari  "
                    + " ORDER BY NRE_CLAARISTA";

            return ConsultaDML(sqlQuery, Convert.ToInt64(dicParametro[COL_US_CLAFOLIO]));
        }

        public Dictionary<Int64, RedAristaSegMdl> dmlSelectSeguimiento(Object oDatos)
        {
            Int64 iArista = 0;
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

            Dictionary<Int64, RedAristaSegMdl> dicDatos = new Dictionary<Int64, RedAristaSegMdl>();

            String sqlQuery = " WITH ADOC AS( SELECT nre_claarista, LISTAGG(DOC_CLADOC, ',') WITHIN GROUP(ORDER BY DOC_CLADOC) as DOC_CLADOC "
                + " FROM  SIT_DOC_ARISTA "
                + " WHERE us_clafolio =  :P0 "
                + " GROUP BY nre_claarista ) "
                + "SELECT R.NRE_CLAARISTA, NOD_ORIGEN, AREA.KA_SIGLA as ORIGEN, KAR_DESCRIPCION,   NOD_DESTINO, AREA2.KA_SIGLA as DESTINO, "
                + " TO_CHAR (NRE_FECINI, 'YYYY-MM-DD HH24:MI') as NRE_FECINI,"
                + " TO_CHAR (NRE_FECFIN, 'YYYY-MM-DD HH24:MI') as NRE_FECFIN,"
                + " NRE_DIAS_LABORALES, NRE_OBSERVACION,   KU_NOMBRE || ' ' || KU_PATERNO || ' ' || KU_MATERNO as Responsable, DOC_CLADOC, N2.NOD_ATENDIDO, NRE_FECLECUSU "
                + " FROM SIT_ADM_USUARIO U, SIT_RED_KTIPO_ARISTA AEDO,SIT_RED_NODO N,  SIT_ADM_KAREA AREA, "
                + " SIT_RED_NODO N2, SIT_ADM_KAREA AREA2, "
                + " SIT_RED_ARISTA R LEFT JOIN ADOC ON ADOC.NRE_CLAARISTA = R.NRE_CLAARISTA "
                + " WHERE R.US_CLAFOLIO = :P1 "
                + " AND N.NOD_CLANODO = R.NOD_ORIGEN AND "
                + " R.US_CLAFOLIO = N.US_CLAFOLIO AND AREA.KA_CLAAREA = N.KA_CLAAREA AND "
                + " N2.NOD_CLANODO = R.NOD_DESTINO AND "
                + " R.US_CLAFOLIO = N2.US_CLAFOLIO AND AREA2.KA_CLAAREA = N2.KA_CLAAREA AND "
                + " U.KU_CLAUSU = R.KU_CLAUSU AND AEDO.kar_clatipoari = R.kar_clatipoari  "
                + " ORDER BY R.NRE_CLAARISTA";
          
            DataTable dtoDatos = ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_US_CLAFOLIO]);

            foreach (DataRow dr in dtoDatos.Rows)
            {
                iArista = Convert.ToInt64(dr[0]);
                RedAristaSegMdl frmAristaMdl = new RedAristaSegMdl(iArista, Convert.ToInt64(dr[1]), dr[2].ToString(), dr[3].ToString(),
                    Convert.ToInt64(dr[4]), dr[5].ToString(), Convert.ToDateTime(dr[6]), Convert.ToDateTime(dr[7]), Convert.ToInt32(dr[8]),
                    dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), Convert.ToInt32(dr[12]));
                dicDatos.Add(iArista,  frmAristaMdl);
            }

            return dicDatos;
        }
       
        public DataTable dmlSelectAristaAclaracion(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>) oDatos;
            String sqlQuery = " SELECT NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA, NRE_FECINI, NRE_FECFIN,"
                    + " NRE_OBSERVACION, NRE_DIAS_LABORALES, NRE_DIAS_NATURALES, kar_clatipoari, US_CLAFOLIO, nre_feclectura, NRE_HITO, US_MODENT, NRE_FECLECUSU "
                    + " FROM  SIT_RED_ARISTA WHERE kar_clatipoari = " +  Constantes.General.ID_PENDIENTE + " and us_clafolio = :P0 ";
            return ConsultaDML(sqlQuery, Convert.ToInt64(dicParametro[COL_US_CLAFOLIO]));
        }

        public RedAristaMdl dmlSelectAristaID(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " SELECT NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA, NRE_FECINI, NRE_FECFIN,"
                    + " NRE_OBSERVACION, NRE_DIAS_LABORALES, NRE_DIAS_NATURALES, kar_clatipoari, US_CLAFOLIO, nre_feclectura, NRE_HITO, US_MODENT, NRE_FECLECUSU "
                    + " FROM SIT_RED_ARISTA WHERE us_clafolio = :P0 and NRE_CLAARISTA = :P1";

            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_NRE_CLAARISTA]);
            if (dtDatos.Rows.Count == 0)
                return null;
            else
            {
                List<RedAristaMdl> lstDatos = CrearLista(dtDatos);
                return lstDatos[0];
            }
        }

        public List<RedAristaMdl> dmlSelectAristaFolioDestino(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " SELECT NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA, NRE_FECINI, NRE_FECFIN,"
                    + " NRE_OBSERVACION, NRE_DIAS_LABORALES, NRE_DIAS_NATURALES, kar_clatipoari, US_CLAFOLIO, nre_feclectura, NRE_HITO, US_MODENT, NRE_FECLECUSU "
                    + " FROM SIT_RED_ARISTA WHERE us_clafolio = :P0 and NOD_DESTINO = :P1";

            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_NOD_DESTINO]);
            if (dtDatos.Rows.Count == 0)
                return null;
            else
            {                 
                return CrearLista(dtDatos);
            }
        }


        public List<RedAristaMdl> dmlSelectAristaSinRespuesta(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " SELECT NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA, NRE_FECINI, NRE_FECFIN, "
                    + " NRE_OBSERVACION, NRE_DIAS_LABORALES, NRE_DIAS_NATURALES, kar_clatipoari, US_CLAFOLIO, nre_feclectura, NRE_HITO, US_MODENT, NRE_FECLECUSU "
                    + " FROM  SIT_RED_ARISTA WHERE us_clafolio = :P0 and kar_clatipoari = :P1";

            // SERA SELECCIONAR UN TIPO DE NODO EN ESPECIFICO O CERRAR TODOS....               
                     
            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_KAR_CLATIPOARI]);

            return CrearLista(dtDatos);
        }

        public List<RedAristaMdl> dmlSelectAristasEnProrroga(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " select NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA, NRE_FECINI, NRE_FECFIN, "
                + " NRE_OBSERVACION, NRE_DIAS_LABORALES, NRE_DIAS_NATURALES, kar_clatipoari, US_CLAFOLIO, nre_feclectura, NRE_HITO, US_MODENT, NRE_FECLECUSU "
                + " from SIT_RED_ARISTA arista where arista.nod_origen in  "
                + " (select nodo.nod_clanodo from SIT_RED_ARISTA arista, SIT_RED_NODO nodo "
                + " where arista.us_clafolio = :P0 and nodo.nod_clanodo = arista.nod_destino "
                + " and kar_clatipoAri = :P1 "
                + " group by nodo.nod_clanodo ) and arista.KAR_CLATIPOARI = :P2 ";

            return CrearLista(ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_KAR_CLATIPOARI_PRORROGA], dicParam[COL_KAR_CLATIPOARI_SIN_RESPUESTA]));
        }

        ////public DataTable dmlSelectCIrespAreas(Object oDatos)
        ////{
        ////    Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

        ////    String sqlQuery = "  "
        ////        + " Select 1 as Respuesta, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaOrigen.ka_sigla,    "
        ////        + " aritipo.kar_clatipoari, aritipo.kar_descripcion, tpi_descripcion, "
        ////        + " res.TPI_CLATIPO_INFO, res.IDFORMAENTREGA, res.RSL_FECHA, res.RSL_UBICACION, res.RSL_TAM_CANT_DIR, res.RSL_RESOLUCION, res.RSL_ART7, "
        ////        + " doc.doc_cladoc, doc.doc_fecha, doc.doc_folio,  doc.doc_nombre, nodoDest.nod_clanodo "
        ////        + " from SIT_RED_KTIPO_ARISTA aritipo, "
        ////        + " SIT_RED_NODO nodo, SIT_ADM_KAREA areaDestino, "
        ////        + " SIT_RED_NODO nodoOrigen, SIT_ADM_KAREA areaOrigen, "
        ////        + " SIT_RED_ARISTA arista "
        ////        + " left join SIT_RED_ARISTA_RESOLUCION res ON res.nre_claarista = arista.nre_claarista and res.us_clafolio = arista.us_clafolio "
        ////        + " left join SIT_DOC_ARISTA docari  ON docari.nre_claarista = arista.nre_claarista and docari.us_clafolio = arista.us_clafolio "
        ////        + " left join SIT_DOCUMENTO doc ON doc.doc_cladoc = docari.doc_cladoc "
        ////        + " left join SIT_RED_KTIPO_INFO InfoTipo on InfoTipo.tpi_clatipo_info = res.tpi_clatipo_info "
        ////        + " where arista.us_clafolio = :P0 "
        ////        + " and arista.nod_destino = nodo.nod_clanodo "
        ////        + " and nodo.us_clafolio = arista.us_clafolio "
        ////        + " and nodo.ka_claarea = areaDestino.ka_claarea "
        ////        + " and areaDestino.kp_claperfil = :P1 "
        ////        + " and arista.kar_clatipoari > :P2 "
        ////        + " and arista.kar_clatipoari <> :P3 "
        ////        + " and aritipo.kar_clatipoari = arista.kar_clatipoari "
        ////        + " and nodoOrigen.nod_clanodo = arista.nod_origen "
        ////        + " and areaOrigen.ka_claarea = nodoOrigen.ka_claarea "
        ////        + " and arista.Nod_destino in (select nod_origen from SIT_RED_ARISTA where us_clafolio = arista.us_clafolio  and kar_clatipoari = 0) "
        ////        + " UNION ALL "
        ////        + " Select 0 as Respuesta, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaOrigen.ka_sigla,    "
        ////        + " 0, null, null, null, null, null, null, null, null, null, null, null, null, null,null "
        ////        + " from SIT_RED_KTIPO_ARISTA aritipo, "
        ////        + " SIT_RED_NODO nodoOrigen, SIT_ADM_KAREA areaOrigen, SIT_RED_ARISTA arista "
        ////        + " where arista.us_clafolio = :P4 "
        ////        + " and areaOrigen.kp_claperfil <> :P5 "
        ////        + " and arista.kar_clatipoari = :P6 "
        ////        + " and nodoOrigen.nod_clanodo = arista.nod_origen "
        ////        + " and areaOrigen.ka_claarea = nodoOrigen.ka_claarea "
        ////        + " and aritipo.kar_clatipoari = arista.kar_clatipoari "                
        ////        + " ORDER BY 1,2 ";

        ////    return ConsultaDML(sqlQuery, 
        ////        dicParam[COL_US_CLAFOLIO], dicParam[COL_KP_CLAPERFIL], dicParam[COL_KAR_CLATIPOARI], dicParam[COL_RESTRICCION_TIPOARISTA],
        ////        dicParam[COL_US_CLAFOLIO], dicParam[COL_KP_CLAPERFIL], dicParam[COL_KAR_CLATIPOARI]);
        ////}

        public DataTable dmlSelectAreasFaltantes(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " SELECT KU_NOMBRE || ' ' || KU_PATERNO || ' ' || KU_MATERNO as NOMBRE, AREA.KA_SIGLA as SIGLA, AREA.KA_DESCRIPCION as DESCRIPCION " +
            " FROM SIT_ADM_KAREA AREA, SIT_RED_NODO NODO, SIT_ADM_USUARIO USU " +
            " WHERE NODO.us_CLAFOLIO = :P0 " +
            " AND nodo.nod_atendido = 0 " +
            " AND AREA.KA_CLAAREA = NODO.KA_CLAAREA " +
            " and USU.KA_CLAAREA_ORIGEN = NODO.KA_CLAAREA order by DESCRIPCION ";

            return ConsultaDML(sqlQuery, Convert.ToInt64(dicParametro[COL_US_CLAFOLIO]));
        }

        public DataTable dmlSelectAreasFaltantesCorreos(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " SELECT KU_NOMBRE || ' ' || KU_PATERNO || ' ' || KU_MATERNO as NOMBRE, KU_CORREO " +
            " FROM SIT_ADM_KAREA AREA, SIT_RED_NODO NODO, SIT_ADM_USUARIO USU " +
            " WHERE NODO.us_CLAFOLIO = :P0 " +
            " AND nodo.nod_atendido = 0 " +
            " AND AREA.KA_CLAAREA = NODO.KA_CLAAREA " +
            " and USU.KA_CLAAREA_ORIGEN = NODO.KA_CLAAREA order by DESCRIPCION ";

            return ConsultaDML(sqlQuery, Convert.ToInt64(dicParametro[COL_US_CLAFOLIO]));
        }

        
        

        public RedAristaMdl dmlSelectAristaNodoDestino(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = "  "
                + " select * from SIT_RED_ARISTA where us_clafolio = :P0 and   "
                + "  nod_destino = :P1 and kar_clatipoari = :P2 ";


            DataTable dtDatos = ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_NOD_DESTINO], dicParam[COL_KAR_CLATIPOARI]);
            if (dtDatos.Rows.Count == 0)
                return null;
            else
            {
                List<RedAristaMdl> lstDatos = CrearLista(dtDatos);
                return lstDatos[0];
            }
        }

        public RedAristaMdl dmlSelectAristaNodoOrigen(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " select * from SIT_RED_ARISTA where us_clafolio = :P0 AND nod_origen = :P1 ";


            DataTable dtDatos = ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_NOD_ORIGEN]);
            if (dtDatos.Rows.Count == 0)
                return null;
            else
            {
                List<RedAristaMdl> lstDatos = CrearLista(dtDatos);
                return lstDatos[0];
            }
        }


        ////////public DataTable dmlSelectAristaUTpendientes(Object oDatos)
        ////////{
        ////////    Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

        ////////    String sqlQuery = " SELECT 1 as Respuesta, nodoO.nod_clanodo, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla,  tipari.kar_clatipoari, "
        ////////    + " tipari.kar_descripcion, doc.doc_cladoc, doc.doc_fecha, doc.doc_nombre, ari.nre_claarista, nre_observacion, ' ' as Estado,  ari.nre_claarista "
        ////////    + " FROM SIT_RED_NODO_HITO hito, SIT_RED_KTIPO_ARISTA tipari, SIT_ADM_KAREA areaO, SIT_RED_NODO nodoO, SIT_RED_ARISTA ari "
        ////////    + " LEFT JOIN SIT_DOC_ARISTA docari ON docari.nre_claarista = ari.nre_claarista and docari.us_clafolio = ari.us_clafolio "
        ////////    + " LEFT JOIN SIT_DOCUMENTO doc ON doc.doc_cladoc = docari.doc_cladoc"
        ////////    + " WHERE ari.us_clafolio = :P0 "
        ////////    + " AND ari.kar_clatipoari = tipari.kar_clatipoari "
        ////////    + " AND nodoO.nod_clanodo = ari.nod_origen "
        ////////    + " AND areaO.ka_claarea = nodoO.ka_claarea "
        ////////    + " AND nod_destino = :P1  "
        ////////    + " AND hito.us_clafolio = ari.us_clafolio  "
        ////////    + " AND hito.nod_clanodo =  nod_destino "
        ////////    + " AND hito.nre_claarista =  ari.nre_claarista "
        ////////    + " AND nodoO.krp_claproceso = :P2    "
        ////////    + " UNION ALL "
        ////////    + " SELECT 0 as Respuesta, nodoO.nod_clanodo, TO_CHAR(NOD_FECCREACION, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla, null, null, null,  "
        ////////    + " null, null, null, ' ' as Estado, null, nodoO.nod_capa "
        ////////    + " FROM SIT_RED_NODO nodoO, SIT_ADM_KAREA areaO "
        ////////    + " WHERE nodoO.us_clafolio = :P3 "
        ////////    + " AND nodoO.nod_atendido = 0 "
        ////////    + " AND areaO.ka_claarea = nodoO.ka_claarea "
        ////////    + " AND nodoO.nod_clanodo <> :P3 "
        ////////    + " AND nodoO.krp_claproceso <> :P4 ";

        ////////    ////////        String sqlQuery = " SELECT 1 as Respuesta, nodoO.nod_clanodo, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla,  tipari.kar_clatipoari, " 
        ////////    ////////+ " tipari.kar_descripcion, doc.doc_cladoc, doc.doc_fecha, doc.doc_nombre, ari.nre_claarista, nre_observacion, ' ' as Estado,  ari.nre_claarista "
        ////////    ////////+ " FROM SIT_RED_KTIPO_ARISTA tipari, SIT_ADM_KAREA areaO, SIT_RED_NODO nodoO, SIT_RED_ARISTA ari "
        ////////    ////////+ " LEFT JOIN SIT_DOC_ARISTA docari ON docari.nre_claarista = ari.nre_claarista and docari.us_clafolio = ari.us_clafolio "
        ////////    ////////+ " LEFT JOIN SIT_DOCUMENTO doc ON doc.doc_cladoc = docari.doc_cladoc"
        ////////    ////////+ " WHERE ari.us_clafolio = :P0 "
        ////////    ////////+ " AND ari.kar_clatipoari = tipari.kar_clatipoari "
        ////////    ////////+ " AND kar_respuesta in ( " + Constantes.Accion.RESPUESTA + "," + Constantes.Accion.RESPUESTA_INFOMEX  + ")"
        ////////    ////////+ " AND nodoO.nod_clanodo = ari.nod_origen "
        ////////    ////////+ " AND areaO.ka_claarea = nodoO.ka_claarea "
        ////////    ////////+ " AND nod_destino = :P2  "
        ////////    ////////+ " UNION ALL "
        ////////    ////////+ " SELECT 0 as Respuesta, nodoO.nod_clanodo, TO_CHAR(NRE_FECINI, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla, aritipo.kar_clatipoari,  "
        ////////    ////////+ " null, null, null, null, null, null, ' ' as Estado, arista.nre_claarista"
        ////////    ////////+ " FROM SIT_RED_KTIPO_ARISTA aritipo, SIT_ADM_KAREA areaO, SIT_RED_NODO nodoO,  SIT_RED_ARISTA arista "
        ////////    ////////+ " WHERE "
        ////////    ////////+ " arista.us_clafolio = nodoO.us_clafolio "
        ////////    ////////+ " and arista.us_clafolio = :P3 "
        ////////    ////////+ " and nodoO.nod_atendido = 0 "
        ////////    ////////+ " and arista.nod_destino = nodoO.nod_clanodo "
        ////////    ////////+ " and areaO.ka_claarea = nodoO.ka_claarea "
        ////////    ////////+ " and aritipo.kar_clatipoari = arista.kar_clatipoari "
        ////////    ////////+ " and kar_respuesta = " + Constantes.Accion.TURNAR
        ////////    ////////+ " ORDER BY 3";

        ////////    //// FALTARIA VER LAS RESPUESTA POR CAPAS.....
        ////////    //// 


        ////////    // DATOS EN LA RESOLUCION
        ////////    // DATOS EN LA RESOLUCION
        ////////    // DATOS EN LA RESOLUCION
        ////////    //////String sqlQuery = " SELECT 1 as Respuesta, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaOrigen.ka_sigla, nodoDest.ka_claarea as AreaDestino,"
        ////////    //////    + " areaDestino.ka_sigla as siglasDestino, aritipo.kar_clatipoari, aritipo.kar_descripcion, tpi_descripcion, res.TPI_CLATIPO_INFO, res.IDFORMAENTREGA, "
        ////////    //////    + " res.RSL_FECHA, res.RSL_UBICACION, res.RSL_TAM_CANT_DIR, res.RSL_RESOLUCION, res.RSL_ART7, "
        ////////    //////    + " doc.doc_cladoc, doc.doc_fecha, doc.doc_folio,  doc.doc_nombre, arista.nre_claarista, "
        ////////    //////    + " nodoDest.nod_clanodo, COM_FECHA, COM_MOTIVO, RCO_CLACOMITERESP, COM_TAM_CAT_DIR, NRE_FECFIN  "
        ////////    //////    + " FROM  SIT_RED_KTIPO_ARISTA aritipo, SIT_ADM_KAREA areaOrigen, SIT_ADM_KAREA areaDestino,  SIT_RED_NODO nodoDest, SIT_RED_NODO nodoOrigen, SIT_RED_ARISTA arista "
        ////////    //////    + " left join SIT_RED_ARISTA_RESOLUCION res ON res.nre_claarista = arista.nre_claarista and res.us_clafolio = arista.us_clafolio "
        ////////    //////    + " left join SIT_DOC_ARISTA docari  ON docari.nre_claarista = arista.nre_claarista and docari.us_clafolio = arista.us_clafolio "
        ////////    //////    + " left join SIT_DOCUMENTO doc ON doc.doc_cladoc = docari.doc_cladoc "
        ////////    //////    + " left join SIT_RED_KTIPO_INFO InfoTipo on InfoTipo.tpi_clatipo_info = res.tpi_clatipo_info "
        ////////    //////    + " left join SIT_RED_ARISTA_COMITE resComite on resComite.nre_claarista = arista.nre_claarista and resComite.us_clafolio = arista.us_clafolio "
        ////////    //////    + " WHERE "
        ////////    //////    + " arista.us_clafolio = nodoDest.us_clafolio "
        ////////    //////    + " and arista.us_clafolio = :P0 "
        ////////    //////    //                + " and nodoDest.nod_atendido = 0 "
        ////////    //////    + " and arista.nod_destino = nodoDest.nod_clanodo "
        ////////    //////    + " and nodoOrigen.nod_claNodo = arista.nod_origen "
        ////////    //////    + " and areaOrigen.ka_claarea = nodoOrigen.ka_claarea"
        ////////    //////    + " and aritipo.kar_clatipoari = arista.kar_clatipoari "
        ////////    //////    + " and areaDestino.ka_claarea = nodoDest.ka_claarea "
        ////////    //////    + " and kar_respuesta = 1 "
        ////////    //////    + " and aritipo.kar_clatipoari <> " + AfdConstantes.ARISTA.CIERRE_AUTOMATICO
        ////////    //////    + " UNION ALL "
        ////////    //////    + " SELECT 0 as Respuesta, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaOrigen.ka_sigla, aritipo.kar_clatipoari, null, "
        ////////    //////    + " null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null,NRE_FECFIN "
        ////////    //////    + " FROM SIT_RED_KTIPO_ARISTA aritipo, SIT_ADM_KAREA areaOrigen, SIT_RED_NODO nodoDest,  SIT_RED_ARISTA arista "
        ////////    //////    + " WHERE "
        ////////    //////    + " arista.us_clafolio = nodoDest.us_clafolio "
        ////////    //////    + " and arista.us_clafolio = :P1 "
        ////////    //////    + " and nodoDest.nod_atendido = 0 "
        ////////    //////    + " and arista.nod_destino = nodoDest.nod_clanodo "
        ////////    //////    + " and areaOrigen.ka_claarea = nodoDest.ka_claarea "
        ////////    //////    + " and aritipo.kar_clatipoari = arista.kar_clatipoari "
        ////////    //////    + " and kar_respuesta = 0 "
        ////////    //////    + " ORDER BY NRE_FECFIN";

        ////////    return ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_NOD_DESTINO], dicParam[COL_KRP_CLAPROCESO],
        ////////          dicParam[COL_US_CLAFOLIO], dicParam[COL_NOD_DESTINO], dicParam[COL_KRP_CLAPROCESO]);
        ////////}

        public DataTable dmlSelectAristaPendientes(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " SELECT 1 as Respuesta, nodoO.nod_clanodo, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla,  tipari.kar_clatipoari, "
            + " tipari.kar_descripcion, doc.doc_cladoc, doc.doc_fecha, doc.doc_nombre, ari.nre_claarista, nre_observacion, ' ' as Estado,  ari.nre_claarista "
            + " FROM SIT_RED_KTIPO_ARISTA tipari, SIT_ADM_KAREA areaO, SIT_RED_NODO nodoO, SIT_RED_ARISTA ari "
            + " LEFT JOIN SIT_DOC_ARISTA docari ON docari.nre_claarista = ari.nre_claarista and docari.us_clafolio = ari.us_clafolio "
            + " LEFT JOIN SIT_DOCUMENTO doc ON doc.doc_cladoc = docari.doc_cladoc"
            + " WHERE ari.us_clafolio = :P0 "
            + " AND ari.kar_clatipoari = tipari.kar_clatipoari "
            + " AND nodoO.nod_clanodo = ari.nod_origen "
            + " AND areaO.ka_claarea = nodoO.ka_claarea "
            + " AND ari.nre_hito = " + Constantes.AristaHito.SI
            + " AND nodoO.krp_claproceso = :P1 "
            + " AND areaO.KP_CLAPERFIL = :P2 "
            + " UNION ALL "
            + " SELECT 0 as Respuesta, nodoO.nod_clanodo, TO_CHAR(NOD_FECCREACION, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla, null, null, null,  "
            + " null, null, null, ' ' as Estado, null, nodoO.nod_capa "
            + " FROM SIT_RED_NODO nodoO, SIT_ADM_KAREA areaO "
            + " WHERE nodoO.us_clafolio = :P3 "
            + " AND nodoO.nod_atendido = 0 "
            + " AND areaO.ka_claarea = nodoO.ka_claarea "
            + " AND nodoO.KA_CLAAREA <> :P4 "
            + " AND nodoO.krp_claproceso = :P5 "
            + " AND nodoO.kne_clanodo_edo not in ( " + dicParam[PARAM_NO_ESTADOS] + "  )"
            + " AND areaO.KP_CLAPERFIL = :P6 ";


            return ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_KRP_CLAPROCESO],  dicParam[COL_KP_CLAPERFIL],
                 dicParam[COL_US_CLAFOLIO], dicParam[COL_KA_CLAAREA], dicParam[COL_KRP_CLAPROCESO], dicParam[COL_KP_CLAPERFIL]) ;
        }


        public List<RedAristaMdl> dmlSelectAristaCIpendientesNOprorroga(Object oDatos)
        {
            // el primer tipo de arista es para obtener todos las pendients..SIN_RESPUESTA = 0;
            // el seugndi tipo de arita es para encontrado todo aquello qu eno este en PRORROGA
            // y que el área corresponda al COMITE DE INFOMRACION (4)

            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " select arista.US_CLAFOLIO, NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA, NRE_FECINI, NRE_FECFIN, NRE_OBSERVACION, "
                + " NRE_DIAS_LABORALES, KAR_CLATIPOARI, NRE_FECLECTURA, NRE_DIAS_NATURALES, NRE_HITO,US_MODENT, NRE_FECLECUSU "
                + " from SIT_RED_ARISTA arista, SIT_red_nodo nodo "
                + " where arista.us_clafolio = :P0 and kar_clatipoari = :P1 and nod_origen not "
                + " in (Select nod_destino from SIT_RED_ARISTA where us_clafolio = :P2 and kar_clatipoari = :P3 ) "
                + " and arista.us_clafolio = nodo.us_clafolio "
                + " and nodo.nod_clanodo = arista.nod_origen "
                + " and ka_claarea = :P4 ";

            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_KAR_CLATIPOARI_SIN_RESPUESTA], 
                dicParametro[COL_US_CLAFOLIO], dicParametro[COL_KAR_CLATIPOARI_PRORROGA], dicParametro[COL_KA_CLAAREA]);

            return CrearLista(dtDatos);
        }

        public RedAristaMdl dmlSelectAristaPorAristaOrigen(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " Select US_CLAFOLIO, NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA, NRE_FECINI, NRE_FECFIN, NRE_OBSERVACION, "
                + " NRE_DIAS_LABORALES, KAR_CLATIPOARI, NRE_FECLECTURA, NRE_DIAS_NATURALES, NRE_HITO, US_MODENT, NRE_FECLECUSU FROM SIT_red_arista where nre_claarista =  "
                + " (Select ARJ_DESTINO FROM SIT_red_arista_flujo WHERE arj_origen = :P0 ) ";

            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[COL_ARJ_ORIGEN]);

            return CrearLista(dtDatos)[0];
        }

        public DataTable dmlSelectAristaFolioTipoAristaTrans(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " Select US_CLAFOLIO, NOD_ORIGEN, NOD_DESTINO, KU_CLAUSU, NRE_CLAARISTA, NRE_FECINI, NRE_FECFIN, "
                + " NRE_OBSERVACION, NRE_DIAS_LABORALES, KAR_CLATIPOARI, NRE_FECLECTURA, NRE_DIAS_NATURALES FROM SIT_red_arista  "
                + " where us_clafolio = :P0 and kar_clatipoari = :P1 ";

            DataTable catalogoRet = new DataTable();
            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_KAR_CLATIPOARI]);
            catalogoRet.Columns.Add("titulo", typeof(string));
            catalogoRet.Columns.Add("valor", typeof(string));

            foreach (DataRow row in dtDatos.Rows)
            {
                catalogoRet.Rows.Add("Fecha recepción", ((DateTime)row["NRE_FECINI"]).ToString("dd/MM/yyyy"));
                catalogoRet.Rows.Add("Info. adicional", row["NRE_OBSERVACION"].ToString());
            }
            return catalogoRet;
        }

        public DataTable dmlSelectRespDoc(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " SELECT kar_descripcion, doc.doc_cladoc, nre_observacion, doc_nombre, tipo.kar_clatipoari "
                 + " FROM SIT_SOL_SEGUIMIENTO seg, sit_red_ktipo_arista tipo, sit_red_arista ari "
                 + " LEFT JOIN SIT_DOC_ARISTA aridoc ON aridoc.nre_claarista = ari.nre_claarista "
                 + " LEFT JOIN SIT_DOCUMENTO doc ON doc.doc_cladoc = aridoc.doc_cladoc "
                 + " WHERE ari.us_clafolio = :P0 "
                 + " AND ari.nre_claarista = :P1 AND krp_claProceso = :P2 "
                 + " AND seg.us_clafolio = ari.us_clafolio "
                 + " AND tipo.kar_clatipoari = seg.kar_clatipoari ";

            return ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO],  dicParametro[COL_NRE_CLAARISTA],
                dicParametro[COL_KRP_CLAPROCESO]);
        }

        public DataTable dmlSelectRespDocAristaAnt(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " SELECT kar_descripcion, doc.doc_cladoc, nre_observacion, doc_nombre, tipo.kar_clatipoari "
                 + " FROM SIT_SOL_SEGUIMIENTO seg, sit_red_ktipo_arista tipo, sit_red_arista ari "
                 + " LEFT JOIN SIT_DOC_ARISTA aridoc ON aridoc.nre_claarista = ari.nre_claarista "
                 + " LEFT JOIN SIT_DOCUMENTO doc ON doc.doc_cladoc = aridoc.doc_cladoc "
                 + " WHERE ari.us_clafolio = :P0 AND ari.nre_claarista = :P1 "
                 + " AND seg.us_clafolio = ari.us_clafolio "
                 + " AND krp_claProceso = :P2 "
                 + " AND tipo.kar_clatipoari = ari.kar_clatipoari ";

            return ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_NRE_CLAARISTA],
                dicParametro[COL_KRP_CLAPROCESO]);
        }
        
        public Dictionary<int,string> dmlSelectDicInfoAdicResp(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " select 1, nre_observacion from SIT_RED_ARISTA ari "
                 + "  where ari.US_CLAFOLIO = :P0 "
                 + "  and Kar_Clatipoari = :P1 "
                 + "  UNION "
                 + "  select 2, nre_observacion from SIT_RED_ARISTA ari "
                 + "  where ari.US_CLAFOLIO = :P2 "
                 + "  and Kar_Clatipoari = :P3 ";                               

            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[PARAM_ARITIPO_INFOADI], 
                dicParametro[COL_US_CLAFOLIO], dicParametro[PARAM_ARITIPO_INFOADI_RESP]);

            Dictionary<int, string> dicRes = new Dictionary<int, string>();

            for (int iIdx = 0; iIdx < dtDatos.Rows.Count; iIdx++)
            {
                dicRes.Add(  Convert.ToInt32( dtDatos.Rows[iIdx][0]) , dtDatos.Rows[iIdx][1].ToString() );
            }
            return dicRes;
        }



        public object dmlSelectAristaAccionPreviaArea(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " select count(*) "
                 + " from sit_red_arista arista, sit_red_nodo nodo  "
                 + " where arista.us_clafolio = :P0 "
                 + " AND arista.nod_origen = nodo.nod_clanodo "
                 + " AND nodo.ka_claArea in (select ka_claArea from sit_red_nodo where nod_clanodo = :P1 ) "
                 + " AND kar_CLATIPOARI = :P2 ";

            DataTable dtResultado = ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_NOD_CLANODO], dicParametro[COL_KAR_CLATIPOARI]);

            return Convert.ToInt32(dtResultado.Rows[0].ItemArray[0]);
        }


        public DataTable dmlSelectRespPrevRecRevision(Object oDatos)
        {

            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

            ////String sqlQuery = "  WITH arista_resp AS "
            ////+ " (select * from sit_red_arista where nod_destino in   "
            ////+ " (select nod_origen from sit_red_arista where nre_claarista in  "
            ////+ "     (select  nre_claarista from sit_sol_seguimiento where us_clafolio = :P0 and krp_claProceso = :P1) ) "
            ////+ " UNION "
            ////+ " select* from sit_red_arista where nre_claarista in (select  nre_claarista from sit_sol_seguimiento where us_clafolio = :P2 and krp_claProceso = :P3)  "
            ////+ " ) "
            ////+ " SELECT 1 as Respuesta, nodoO.nod_clanodo, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla,  tipari.kar_clatipoari,  "
            ////+ " tipari.kar_descripcion, doc.doc_cladoc, doc.doc_fecha, doc.doc_nombre, ari.nre_claarista, nre_observacion, ' ' as Estado,  ari.nre_claarista  "
            ////+ " FROM  SIT_RED_KTIPO_ARISTA tipari, SIT_ADM_KAREA areaO, SIT_RED_NODO nodoO, arista_resp ari "
            ////+ " LEFT JOIN SIT_DOC_ARISTA docari ON docari.nre_claarista = ari.nre_claarista and docari.us_clafolio = ari.us_clafolio "
            ////+ " LEFT JOIN SIT_DOCUMENTO doc ON doc.doc_cladoc = docari.doc_cladoc "
            ////+ " WHERE ari.us_clafolio = :P4 "
            ////+ " AND ari.kar_clatipoari = tipari.kar_clatipoari "
            ////+ " AND nodoO.nod_clanodo = ari.nod_origen "
            ////+ " AND areaO.ka_claarea = nodoO.ka_claarea "
            ////+ " ORDER BY ari.NRE_CLAARISTA DESC ";


            String sqlQuery = " SELECT 1 as Respuesta, nodoO.nod_clanodo, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla,  tipari.kar_clatipoari,    "
                + " tipari.kar_descripcion, doc.doc_cladoc, doc.doc_fecha, doc.doc_nombre, ari.nre_claarista, nre_observacion, ' ' as Estado,  ari.nre_claarista "
                + " FROM SIT_RED_KTIPO_ARISTA tipari, SIT_ADM_KAREA areaO, SIT_RED_NODO nodoO, sit_red_arista ari "
                + " LEFT JOIN SIT_DOC_ARISTA docari ON docari.nre_claarista = ari.nre_claarista and docari.us_clafolio = ari.us_clafolio "
                + " LEFT JOIN SIT_DOCUMENTO doc ON doc.doc_cladoc = docari.doc_cladoc "
                + " where nre_hito = " + Constantes.AristaHito.SI  + " and ari.us_clafolio = :P0 "
                + " and nodoO.KRP_CLAPROCESO = :P1 "
                + " AND ari.kar_clatipoari = tipari.kar_clatipoari  AND nodoO.nod_clanodo = ari.nod_origen "
                + " AND areaO.ka_claarea = nodoO.ka_claarea "
                + " ORDER BY ari.NRE_CLAARISTA DESC ";
                  
            return ConsultaDML(sqlQuery, dicParametro[COL_US_CLAFOLIO], dicParametro[COL_KRP_CLAPROCESO] );
        }

        private List<RedAristaMdl> CrearLista(DataTable dtDatos)
        {
            DateTime dtFechaIni;
            DateTime dtFechaFin;
            DateTime dtFechaLectura;
            List<RedAristaMdl> lstDatos = new List<RedAristaMdl>();

            foreach (DataRow drDato in dtDatos.Rows)
            {
                if (drDato["NRE_FECINI"].ToString() == "")
                    dtFechaIni = new DateTime();
                else
                    dtFechaIni = (DateTime)drDato["NRE_FECINI"];

                if (drDato["NRE_FECFIN"].ToString() == "")
                    dtFechaFin = new DateTime();
                else
                    dtFechaFin = (DateTime)drDato["NRE_FECFIN"];

                if (drDato["nre_feclectura"].ToString() == "")
                    dtFechaLectura = new DateTime();
                else
                    dtFechaLectura = (DateTime)dtDatos.Rows[0]["nre_feclectura"];

                RedAristaMdl dtoDatos = new RedAristaMdl(
                        Convert.ToInt32(drDato["NRE_CLAARISTA"]), Convert.ToInt64(drDato["US_CLAFOLIO"]),
                        Convert.ToInt32(drDato["NOD_ORIGEN"]), Convert.ToInt32(drDato["NOD_DESTINO"]),
                        Convert.ToInt32(drDato["KU_CLAUSU"]), dtFechaIni, dtFechaFin, 
                        drDato["NRE_OBSERVACION"].ToString(), Convert.ToInt32(drDato["NRE_DIAS_LABORALES"]),
                        Convert.ToInt32(drDato["NRE_DIAS_NATURALES"]), Convert.ToInt32(drDato["kar_clatipoari"]), 
                        dtFechaLectura, Convert.ToInt32(drDato["NRE_HITO"]), Convert.ToInt32(drDato["US_MODENT"]),
                        Convert.ToInt32(drDato["NRE_FECLECUSU"]) );

                lstDatos.Add(dtoDatos);
            }

            if (lstDatos.Count == 0)
                return null; 
            else
                return lstDatos;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}


