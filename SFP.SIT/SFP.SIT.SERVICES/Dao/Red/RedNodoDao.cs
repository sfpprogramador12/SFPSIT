using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Red;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Red
{
    public class RedNodoDao : BaseDao
    {
        public const int UPDATE_NODO_ATENDIDO = 111;


        public const int OPE_SELECT_NODO_MAX = 211;
        public const int OPE_SELECT_NODO_FOLEDOPRC= 212;
        public const int OPE_SELECT_NODO_ORIGEN = 213;
        public const int OPE_SELECT_NODO_ID = 214;
        public const int OPE_SELECT_NODOS_PENDIENTES = 215;
        public const int OPE_SELECT_NODOS_PENDIENTES_AREA = 216;
        public const int OPE_SELECT_NODOS_FOLIO_ESTADO_NOATENDIDO = 217;
        public const int OPE_SELECT_NODO_EXISTE = 218;
        public const int OPE_SELECT_NODO_ID_UR_SOL = 219;
        public const int OPE_SELECT_NODOS_EN_GRAFO = 220;
        public const int OPE_SELECT_MDL_AMPLIACION_PENDIENTE = 221;

        public const string COL_US_CLAFOLIO = "US_CLAFOLIO";
        public const string COL_KNE_CLANODO_EDO = "KNE_CLANODO_EDO";
        public const string COL_KRP_CLAPROCESO = "KRP_CLAPROCESO";
        public const string COL_NOD_CLANODO = "NOD_CLANODO";
        public const string COL_NOD_DESTINO = "NOD_DESTINO";
        public const string COL_KAR_CLATIPOARI = "KAR_CLATIPOARI";
        public const string COL_KA_CLAAREA = "KA_CLAAREA";
        public const string COL_NOD_CAPA = "NOD_CAPA";
        public const string COL_AREAS_EXCLUIR = "AREA_EXCLUIR";

        public Int64 iSecuencia { get; private set; }

        public RedNodoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            dicOperacion[UPDATE_NODO_ATENDIDO] = new Func<Object, object>(dmlUpdateNodoAtendido);
            

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_NODO_MAX] = new Func<Object, object>(dmlSelectNodoMaximo);
            dicOperacion[OPE_SELECT_NODO_FOLEDOPRC] = new Func<Object, object>(dmlSelectNodoFolioEstadoPrc);
            dicOperacion[OPE_SELECT_NODO_ORIGEN] = new Func<Object, object>(dmlSelectNodoOrigen);
            dicOperacion[OPE_SELECT_NODO_ID] = new Func<Object, object>(dmlSelectNodoID);
            dicOperacion[OPE_SELECT_NODOS_PENDIENTES] = new Func<Object, object>(dmlSelectNodosPendiente);
            dicOperacion[OPE_SELECT_NODOS_PENDIENTES_AREA] = new Func<Object, object>(dmlSelectNodosPendientePorArea);
            dicOperacion[OPE_SELECT_NODOS_FOLIO_ESTADO_NOATENDIDO] = new Func<Object, object>(dmlSelectNodoFolioEstadoAtendido);
            dicOperacion[OPE_SELECT_NODO_EXISTE] = new Func<Object, object>(dmlSelectNodoExiste);
            dicOperacion[OPE_SELECT_NODO_ID_UR_SOL] = new Func<Object, object>(dmlSelectNodoIDurSol);
            dicOperacion[OPE_SELECT_NODOS_EN_GRAFO] = new Func<Object, object>(dmlSelectNodosGrafo);

            dicOperacion[OPE_SELECT_MDL_AMPLIACION_PENDIENTE] = new Func<Object, object>(dmlSelectMdlAmpPendiente);
            
        }   
                 
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            iSecuencia = SecuenciaDML("SEC_SIT_RED_NODO");

            RedNodoMdl dtoDatos = (RedNodoMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_RED_NODO (  US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA,  "
                    + " NOD_FECCREACION, KNE_CLANODO_EDO, "
                    + " nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa )  "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12) ";

            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, iSecuencia, dtoDatos.ka_claarea,
                    dtoDatos.nod_feccreacion, dtoDatos.kne_clanodo_edo,
                    dtoDatos.nod_tip, dtoDatos.nod_til, dtoDatos.nod_ttp, dtoDatos.nod_ttl,
                    dtoDatos.nod_holgura, dtoDatos.krp_claproceso, dtoDatos.nod_atendido, dtoDatos.nod_capa);
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedNodoMdl dtoDatos = (RedNodoMdl) oDatos;
            String sqlQuery = " update  SIT_RED_NODO"
                    + " SET KA_CLAAREA = :P0, NOD_FECCREACION= :P1, KNE_CLANODO_EDO= :P2, nod_tip = :P3, nod_til = :P4,"
                    + " nod_ttp = :P5, nod_ttl = :P6, nod_holgura  = :P7, KRP_CLAPROCESO= :P8, nod_atendido = :P9, nod_capa = :P10 "
                    + " where  US_CLAFOLIO = :P11 AND  NOD_CLANODO = :P12 ";

            return EjecutaDML(sqlQuery, dtoDatos.ka_claarea,
                    dtoDatos.nod_feccreacion, dtoDatos.kne_clanodo_edo, dtoDatos.nod_tip, dtoDatos.nod_til, dtoDatos.nod_ttp,
                    dtoDatos.nod_ttl, dtoDatos.nod_holgura, dtoDatos.krp_claproceso, dtoDatos.nod_atendido, dtoDatos.nod_capa,
                    dtoDatos.us_clafolio, dtoDatos.nod_clanodo);
        }

        private Object dmlUpdateNodoAtendido(Object oDatos)
        {
            RedNodoMdl dtoDatos = (RedNodoMdl)oDatos;
            String sqlQuery = " update  SIT_RED_NODO"
                    + " SET  nod_atendido = :P0 "
                    + " where  US_CLAFOLIO = :P1 AND  NOD_CLANODO = :P2 ";

            return EjecutaDML(sqlQuery, dtoDatos.nod_atendido, dtoDatos.us_clafolio, dtoDatos.nod_clanodo);
        }
        
        private Object dmlDelete(Object oDatos)
        {
            RedNodoMdl dtoDatos = (RedNodoMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_NODO where  US_CLAFOLIO = :P0 AND  NOD_CLANODO = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nod_clanodo);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedNodoMdl> lstDatos = (List<RedNodoMdl>)oDatos;
            iSecuencia = SecuenciaDML("SEC_SIT_RED_NODO");

            String sqlQuery = ""
                    + " insert into SIT_RED_NODO (  US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA,  "
                    + " NOD_FECCREACION,  KNE_CLANODO_EDO, nod_tip, nod_til, nod_ttp, nod_ttl,"
                    + " nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa )  "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12) ";

            foreach (RedNodoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.us_clafolio, iSecuencia, dtoDatos.ka_claarea,
                        dtoDatos.nod_feccreacion, dtoDatos.kne_clanodo_edo,
                        dtoDatos.nod_tip, dtoDatos.nod_til, dtoDatos.nod_ttp, dtoDatos.nod_ttl,
                        dtoDatos.nod_holgura, dtoDatos.krp_claproceso, dtoDatos.nod_capa);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   

        private DataTable dmlSelectGrid(Object  oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT US_CLAFOLIO, NOD.KRP_CLAPROCESO, KRP_DESCRIPCION, NOD.KA_CLAAREA, KA_DESCRIPCION, NOD_CLANODO,  NOD_FECCREACION,  "
                + " NOE.KNE_CLANODO_EDO, KNE_DESCRIPCION, nod_atendido, nod_capa, nod_holgura, nod_tip, nod_til, nod_ttp, nod_ttl "
                + " from SIT_RED_NODO NOD, SIT_ADM_KAREA AREA, SIT_RED_KNODO_ESTADO NOE, SIT_SOL_KPROCESO PRC "
                + " WHERE AREA.KA_CLAAREA = NOD.KA_CLAAREA "
                + " AND NOE.KNE_CLANODO_EDO = NOD.KNE_CLANODO_EDO "
                + " AND PRC.KRP_CLAPROCESO = NOD.KRP_CLAPROCESO "
                + " ORDER BY us_clafolio, nod_clanodo "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        public Object dmlSelectNodoMaximo(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            DataTable dtDatos;
            String sqlQuery = "SELECT MAX(NOD_CLANODO) + 1 from SIT_RED_NODO where US_CLAFOLIO = :P0 ";

            dtDatos = ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO]);

            if (dtDatos.Rows.Count == 0)
                return 1;
            else
                return Convert.ToInt32( dtDatos.Rows[0]);
        }

        private RedNodoMdl dmlSelectNodoFolioEstadoPrc(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = "SELECT US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, "
                    + " nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa "
                    + " from SIT_RED_NODO WHERE us_clafolio = :P0 and kne_clanodo_edo = :P1 "
                    + " and KRP_CLAPROCESO = :P2 ORDER BY NOD_FECCREACION ";

            List<RedNodoMdl> lstDatos = GenerarLista(ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_KNE_CLANODO_EDO], dicParam[COL_KRP_CLAPROCESO]));

            if (lstDatos.Count > 0)
                return lstDatos[0];
            else
                return null;
        }

        public List<RedNodoMdl> dmlSelectNodoOrigen(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " Select nodo.US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, "
                + " nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa "
                + " FROM SIT_red_arista ari, SIT_red_nodo nodo "
                + " where ari.us_clafolio = :P0 "
                + " and ari.us_clafolio = nodo.us_clafolio "
                + " and nod_destino = :P1 and kar_clatipoari = :P2 "
                + " and nod_clanodo = nod_origen ";
            
            return GenerarLista(ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_NOD_DESTINO], dicParam[COL_KAR_CLATIPOARI]));
        }

        public RedNodoMdl dmlSelectNodoID(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " Select nodo.US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, "
                + " nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa "
                + " FROM SIT_red_nodo nodo "
                + " WHERE NOD_CLANODO = :P0 ";

            List<RedNodoMdl> lstDatos = GenerarLista(ConsultaDML(sqlQuery, dicParam[COL_NOD_CLANODO]));

            if (lstDatos.Count > 0 )
                return lstDatos[0];
            else
                return null;
        }

        public List<RedNodoMdl> dmlSelectNodosPendiente(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " Select US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, "
                + "  nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa "
                + " FROM SIT_red_nodo nodo "
                + " where us_clafolio = :P0 "
                + " and  nod_atendido = 0 ";

            return GenerarLista(ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO]));
        }

        public List<RedNodoMdl> dmlSelectNodosPendientePorArea(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " Select US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, "
                + "  nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa "
                + " FROM SIT_red_nodo nodo "
                + " where us_clafolio = :P0 "
                + " and  nod_atendido = 0 "
                + " and  KA_CLAAREA = :P1 ";

            return GenerarLista(ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_KA_CLAAREA] ));
        }

        public RedNodoMdl dmlSelectNodoFolioEstadoAtendido(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " Select nodo.US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, "
                + " nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa "
                + " FROM SIT_red_nodo nodo "
                + " WHERE US_CLAFOLIO = :P0 and KNE_CLANODO_EDO = :P1 and nod_atendido = 0";

            List<RedNodoMdl> lstDatos = GenerarLista(ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_KNE_CLANODO_EDO] ));

            if (lstDatos.Count > 0)
                return lstDatos[0];
            else
                return null;
        }
        
        public RedNodoMdl dmlSelectNodoExiste(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " Select nodo.US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, "
                + " nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa "
                + " FROM SIT_red_nodo nodo "
                + " WHERE US_CLAFOLIO = :P0 and KNE_CLANODO_EDO = :P1 and KA_CLAAREA = :P2 and NOD_CAPA = :P3 ";

            List<RedNodoMdl> lstDatos = GenerarLista(ConsultaDML(sqlQuery, 
                dicParam[COL_US_CLAFOLIO], dicParam[COL_KNE_CLANODO_EDO], dicParam[COL_KA_CLAAREA], dicParam[COL_NOD_CAPA]  ));


            if (lstDatos.Count > 0)
                return lstDatos[0];
            else
                return null;
        }

        public List<RedNodoMdl> dmlSelectNodoIDurSol(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " SELECT nodo.US_CLAFOLIO, NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, nod_tip, nod_til, nod_ttp, nod_ttl, nod_holgura, KRP_CLAPROCESO, nod_atendido, nod_capa"
                + " FROM SIT_red_nodo nodo "
                + " WHERE US_CLAFOLIO = :P0 "
                + " AND krp_claProceso = (select krp_claProceso from sit_solicitud where us_clafolio = :P1 ) "
                + " AND ka_claarea = :P2 "
                + " order by  nod_feccreacion  ";

            List<RedNodoMdl> lstDatos = GenerarLista(ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_US_CLAFOLIO], 
                dicParam[COL_KA_CLAAREA]));

            return lstDatos;
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

            String sqlQuery = " SELECT ka_claarea FROM sit_red_arista arista,  sit_red_nodo nodo "
                + " WHERE arista.us_clafolio = :P0 "
                + " AND ka_claArea not in ( " + sExcluir.Substring(1) + ") "
                + " AND krp_claproceso = :P1 "
                + " AND arista.nod_destino = nodo.nod_clanodo"
                + " GROUP BY  ka_claarea ";
            
            return ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[COL_KRP_CLAPROCESO]);
        }


        public List<RedNodoMdl> dmlSelectMdlAmpPendiente(Object oDatos)
        {
            Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

            String sqlQuery = " SELECT NOD_CLANODO, KA_CLAAREA, NOD_FECCREACION, KNE_CLANODO_EDO, NOD_TIP, NOD_HOLGURA, "
                + " NOD_TTP,NOD_TIL,NOD_TTL,US_CLAFOLIO, NOD_ATENDIDO,KRP_CLAPROCESO, NOD_CAPA"
                + " FROM sit_red_nodo where nod_clanodo = (  "
                + " select nod_destino from sit_red_arista arista  "
                + " WHERE arista.us_clafolio = :P0 and kar_CLATIPOARI = :P1 AND nod_destino <> :P2 ) "
                + " and Nod_atendido = 0  ";

            return GenerarLista( ConsultaDML(sqlQuery, dicParam[COL_US_CLAFOLIO], dicParam[ COL_KAR_CLATIPOARI], dicParam[COL_NOD_DESTINO]));
        }

        private List<RedNodoMdl> GenerarLista(DataTable dtDatos)
        {
            List<RedNodoMdl> lstDatos = new List<RedNodoMdl>();

            foreach (DataRow drDato in dtDatos.Rows)
            {
                RedNodoMdl nodoMdl = new RedNodoMdl( Convert.ToInt64(drDato["US_CLAFOLIO"]),
                    Convert.ToInt32(drDato["NOD_CLANODO"]), Convert.ToDateTime(drDato["NOD_FECCREACION"]), Convert.ToInt32(drDato["KA_CLAAREA"]),
                    Convert.ToInt32(drDato["KNE_CLANODO_EDO"]), Convert.ToInt32(drDato["KRP_CLAPROCESO"]), Convert.ToInt32(drDato["nod_tip"]),
                    Convert.ToInt32(drDato["nod_til"]), Convert.ToInt32(drDato["nod_ttp"]), Convert.ToInt32(drDato["nod_ttl"]),
                    Convert.ToInt32(drDato["nod_holgura"]), Convert.ToInt32(drDato["nod_atendido"]), Convert.ToInt32(drDato["nod_capa"]));

                lstDatos.Add(nodoMdl);
            }
            
            return lstDatos;
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
