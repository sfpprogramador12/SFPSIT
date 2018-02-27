using System.Data.Common;
using SFP.SIT.SERVICES.Model.Tab;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Tab
{
    public class TabConsultaDao : BaseDao
    {
        public const int OPE_SELECT_TABLERO_SOLICITUD_AREA = 211;
        public const int OPE_SELECT_TABLERO_SOLICITUD_USUARIO = 212;
        public const int OPE_SELECT_TABLERO_SOLICITUD_RESPUESTA = 213;
        public const int OPE_SELECT_TABLERO_SOLICITUD_ESTADO = 214;

        public const int OPE_SELECT_TABLERO_AREA_USUARIO = 215;
        public const int OPE_SELECT_TABLERO_AREA_RESPUESTA = 216;
        public const int OPE_SELECT_TABLERO_AREA_ESTADO = 217;

        public const int OPE_SELECT_TABLERO_USUARIO_RESPUESTA = 218;
        public const int OPE_SELECT_TABLERO_USUARIO_ESTADO = 219;

        public const int OPE_SELECT_TABLERO_ESTADO_RESPUESTA = 220;

        public const int DIMENSION_SOLICITUD = 0;
        public const int DIMENSION_AREA = 1;
        public const int DIMENSION_USUARIO = 2;
        public const int DIMENSION_RESPUESTA = 3;
        public const int DIMENSION_ESTADO = 4;

        public const String COL_US_FECHASOL_FECINI = "us_fechasol_fecini";
        public const String COL_US_FECHASOL_FECFIN = "us_fechasol_fecfin";
        public const String PARAM_ORDEN = "orden";
        public const String PARAM_RENGLON = "renglon";
        public const String PARAM_COLUMNA = "columna";
        public const String PARAM_NO_AREAS = "no_areas";



        public TabConsultaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_TABLERO_SOLICITUD_AREA] = new Func<Object, object>(dmlSelectTableroSolicitudArea);
            dicOperacion[OPE_SELECT_TABLERO_SOLICITUD_USUARIO] = new Func<Object, object>(dmlSelectTableroSolicitudUsuario);
            dicOperacion[OPE_SELECT_TABLERO_SOLICITUD_RESPUESTA] = new Func<Object, object>(dmlSelectTableroSolicitudRespuesta);
            dicOperacion[OPE_SELECT_TABLERO_SOLICITUD_ESTADO] = new Func<Object, object>(dmlSelectTableroSolicitudEstado);
            dicOperacion[OPE_SELECT_TABLERO_AREA_USUARIO] = new Func<Object, object>(dmlSelectTableroAreaUsuario);
            dicOperacion[OPE_SELECT_TABLERO_AREA_RESPUESTA] = new Func<Object, object>(dmlSelectTableroAreaRespuesta);
            dicOperacion[OPE_SELECT_TABLERO_AREA_ESTADO] = new Func<Object, object>(dmlSelectTableroAreaEstado);
            dicOperacion[OPE_SELECT_TABLERO_USUARIO_RESPUESTA] = new Func<Object, object>(dmlSelectTableroUsuarioRespuesta);
            dicOperacion[OPE_SELECT_TABLERO_USUARIO_ESTADO] = new Func<Object, object>(dmlSelectTableroUsuarioEstado);
            dicOperacion[OPE_SELECT_TABLERO_ESTADO_RESPUESTA] = new Func<Object, object>(dmlSelectTableroEstadoRespuesta);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              

        private Object dmlSelectTableroSolicitudArea(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery  = " WITH datos_sol AS" +
                "( SELECT sol.us_clafolio,  KA_SIGLA, tsol.tso_descripcion " +
                " FROM SIT_solicitud sol, SIT_red_nodo nodo, SIT_adm_karea area, SIT_sol_ktipo_solicitud tsol " +
                " WHERE sol.us_clafolio = nodo.us_clafolio " + 
                " and nodo.ka_claarea not in  " + dicParam[PARAM_NO_AREAS] +
                " AND nodo.ka_claarea = area.ka_claarea and tsol.tso_clatiposol = sol.tso_clatiposol " +
                " AND sol.us_fechasol between to_date(:P0, 'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy') " +
                " GROUP BY sol.us_clafolio, ka_descripcion, KA_SIGLA, tso_descripcion " +
                " ORDER BY tsol.tso_descripcion, sol.us_clafolio ) " +
                " SELECT  KA_SIGLA, tso_descripcion, count(*) from datos_sol  " +
                " GROUP BY KA_SIGLA,tso_descripcion" +
                " ORDER BY 2,3 DESC ";
            
            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), 
                Convert.ToInt32( dicParam[PARAM_ORDEN]));
        }

        private Object dmlSelectTableroSolicitudUsuario(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery  = " WITH datos_sol AS " +
                " ( SELECT sol.us_clafolio, ku_nombre, tsol.tso_descripcion " +
                " FROM SIT_solicitud sol, SIT_red_nodo nodo, SIT_red_arista arista, SIT_adm_usuario usu, SIT_sol_ktipo_solicitud tsol  " +
                " WHERE nodo.us_clafolio = sol.us_clafolio AND arista.us_clafolio = sol.us_clafolio AND arista.nod_origen = nodo.nod_clanodo  " +
                " AND arista.ku_clausu = usu.ku_clausu and tsol.tso_clatiposol = sol.tso_clatiposol  " +
                " AND nodo.ka_claarea not in  " + dicParam[PARAM_NO_AREAS] +
                " AND sol.us_fechasol between to_date(:P0, 'dd/mm/yyyy') and to_date(:P1, 'dd/mm/yyyy')  " +
                " AND arista.nre_hito = 1 " +
                " GROUP BY sol.us_clafolio, ku_nombre, tsol.tso_descripcion " +
                " ORDER BY sol.us_clafolio)  " +
                " SELECT ku_nombre, tso_descripcion, count(*) from datos_sol   " +
                " GROUP BY ku_nombre, tso_descripcion " +
                " ORDER BY 2,3 DESC ";

            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        private Object dmlSelectTableroSolicitudRespuesta(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            //////////// ARREGLAR
            ////////////String sQuery  = " SELECT kar_descripcion, tsol.tso_descripcion, count(*)  " +
            ////////////        " FROM SIT_solicitud sol, SIT_sol_seguimiento seg, SIT_sol_ktipo_solicitud tsol, SIT_red_ktipo_arista tiparis " +
            ////////////        " WHERE seg.us_clafolio = sol.us_clafolio AND seg.krp_claproceso = sol.krp_claproceso " +
            ////////////        " AND tsol.tso_clatiposol = sol.tso_clatiposol  AND seg.kar_clatipoari = tiparis.kar_clatipoari " +
            ////////////        " AND sol.us_fechasol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')  " +
            ////////////        " AND seg.kar_clatipoari <> " + AfdConstantes.ARISTA.ERROR +
            ////////////        " GROUP BY kar_descripcion, tsol.tso_descripcion  " +
            ////////////        " ORDER BY  2,3 DESC ";

            ////////////return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));

            return null;
        }

        private Object dmlSelectTableroSolicitudEstado(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery  = " SELECT krp_descripcion, tsol.tso_descripcion, count(*) " +
                " FROM SIT_solicitud sol, SIT_sol_seguimiento seg, SIT_sol_ktipo_solicitud tsol,  SIT_sol_kproceso edo" +
                " WHERE seg.us_clafolio = sol.us_clafolio AND seg.krp_claproceso = sol.krp_claproceso AND edo.krp_claproceso = sol.krp_claproceso " +
                " AND tsol.tso_clatiposol = sol.tso_clatiposol  " +
                " AND sol.us_fechasol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy') " +
                " GROUP BY krp_descripcion, tsol.tso_descripcion  " +
                " ORDER BY  2,3 DESC ";


            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        private Object dmlSelectTableroAreaUsuario(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery  = " SELECT area.ka_descripcion, usu.ku_nombre, count(*)  "
                + "  FROM SIT_solicitud sol, SIT_red_arista arista, SIT_adm_usuario usu, SIT_red_nodo nodo, SIT_adm_karea area "
                + " where nodo.us_clafolio = sol.us_clafolio"
                + " and usu.ku_clausu = arista.ku_clausu AND nodo.ka_claarea = area.ka_claarea"
                + " and nodo.nod_clanodo = arista.nod_origen and nodo.us_clafolio = arista.us_clafolio"
                + " AND nodo.ka_claarea not in  " + dicParam[PARAM_NO_AREAS] 
                + " AND sol.us_fechasol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy') "
                + " AND arista.nre_hito = 1 "
                + " GROUP BY  area.ka_descripcion, usu.ku_nombre"
                + " ORDER BY 2,3 DESC ";

            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        private Object dmlSelectTableroAreaRespuesta(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery  = " SELECT  area.ka_descripcion, kar_descripcion, count(*) "
                + " FROM SIT_solicitud sol, SIT_red_nodo nodo, SIT_adm_karea area, SIT_red_arista arista,  SIT_RED_KTIPO_ARISTA tipoAri "
                + " where nodo.us_clafolio = sol.us_clafolio "
                + " and arista.us_clafolio = nodo.us_clafolio and arista.nod_origen= nodo.nod_clanodo "
                + " AND nodo.ka_claarea = area.ka_claarea  "
                + " AND tipoAri.kar_clatipoari =  arista.kar_clatipoari "
                + " AND nodo.ka_claarea not in  " + dicParam[PARAM_NO_AREAS]
                + " AND sol.us_fechasol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')  "
                + " AND arista.nre_hito = 1 "
                + " GROUP BY  area.ka_descripcion, kar_descripcion "
                + " ORDER BY 1,2 DESC ";

            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        private Object dmlSelectTableroAreaEstado(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            //////////// ARREGLAR
            ////////String sQuery  = " SELECT area.KA_DESCRIPCION, krp_descripcion, count(*)  "
            ////////    + " FROM SIT_solicitud sol, SIT_sol_seguimiento seg, SIT_sol_kproceso edo, SIT_red_nodo nodo, SIT_adm_karea area "
            ////////    + " WHERE sol.us_clafolio = seg.us_clafolio and edo.krp_claProceso = seg.krp_claProceso "
            ////////    + " and sol.us_clafolio = nodo.us_clafolio and area.KA_CLAAREA = nodo.KA_CLAAREA "
            ////////    + " AND sol.us_fechasol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy') "
            ////////    + " and nodo.KNE_CLANODO_EDO in ( " + AfdConstantes.ESTADO.INAI_RESPUESTA_SOLICITUD + "," + AfdConstantes.ESTADO.INAI_RESPUESTA_ACLARACION + ")"
            ////////    + " GROUP BY area.KA_DESCRIPCION, krp_descripcion"
            ////////    + " ORDER BY 2,3 DESC ";
            ////////return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));

            return null;
        }

        private Object dmlSelectTableroUsuarioRespuesta(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery  = " SELECT usu.ku_nombre, kar_descripcion, count(*) "
                + " FROM SIT_solicitud sol, SIT_red_arista arista, SIT_red_nodo nodo, "
                + " SIT_red_ktipo_arista tipoAri, SIT_adm_usuario usu "
                + " WHERE nodo.us_clafolio = sol.us_clafolio     "
                + " and arista.us_clafolio = sol.us_clafolio     "
                + " and arista.nod_origen = nodo.nod_clanodo "
                + " and tipoAri.kar_clatipoari = arista.kar_clatipoari "                
                + " and usu.ku_clausu = arista.ku_clausu "
                + " AND nodo.ka_claarea > 0 and nodo.ka_claarea <> 7 and arista.kar_clatipoari <> 30 "
                + " AND sol.us_fechasol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')   "
                + " AND arista.nre_hito = 1 "
                + " group by usu.ku_nombre, kar_descripcion "
                + " order by 2,3 desc";

            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        private Object dmlSelectTableroUsuarioEstado(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            ////////String sQuery  = " SELECT usu.ku_nombre, krp_descripcion, count(*)  "
            ////////    + " FROM SIT_solicitud sol, SIT_sol_seguimiento seg, SIT_sol_kproceso edo, SIT_red_nodo nodo, SIT_red_arista arista, SIT_adm_usuario usu "
            ////////    + " WHERE sol.us_clafolio = seg.us_clafolio and edo.krp_claProceso = seg.krp_claProceso "
            ////////    + " and sol.us_clafolio = nodo.us_clafolio  "
            ////////    + " and arista.us_clafolio = nodo.us_clafolio and arista.nod_origen = nodo.nod_clanodo and arista.KU_CLAUSU = usu.KU_CLAUSU"
            ////////    + " AND sol.us_fechasol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')   "
            ////////    + " and nodo.KNE_CLANODO_EDO in ( " + AfdConstantes.ESTADO.INAI_RESPUESTA_SOLICITUD + "," + AfdConstantes.ESTADO.INAI_RESPUESTA_ACLARACION  + ")"
            ////////    + " AND arista.nre_hito = 1 "
            ////////    + " GROUP BY usu.ku_nombre, krp_descripcion"
            ////////    + " order by 2,3 desc";
            ////////return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));


            //////////// ARREGLAR
            return null;
        }

        private Object dmlSelectTableroEstadoRespuesta(object oDatos)
        {
            //////Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;
            //////////// ARREGLAR
            //////String sQuery  = " SELECT edo.krp_descripcion, kar_descripcion, count(*)  "
            //////    + " FROM SIT_solicitud sol, SIT_sol_seguimiento seg, SIT_red_ktipo_arista tiparis, SIT_sol_kproceso edo "
            //////    + " WHERE  sol.us_clafolio = seg.us_clafolio and sol.krp_claproceso = seg.krp_claproceso "
            //////    + " and seg.KAR_CLATIPOARI = tiparis.KAR_CLATIPOARI and seg.krp_claProceso = edo.krp_claProceso"
            //////    + " AND sol.us_fechasol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')   "
            //////    + " AND seg.kar_clatipoari <> " + AfdConstantes.ARISTA.ERROR 
            //////    + " GROUP BY  krp_descripcion, kar_descripcion  "
            //////    + " order by 2,3 desc ";
            //////return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_US_FECHASOL_FECINI], dicParam[COL_US_FECHASOL_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));


            return null;
        }


        private TabConsultaMdl convertirHashMap(DataTable crsDatos, int iOrden)
        {
            Dictionary<String, Dictionary<String, int>> hmMatriz = new Dictionary<String, Dictionary<String, int>>();
            Dictionary<String, int> hmMatrizAux = null;

            Dictionary<String, int> hmColumnas = new Dictionary<String, int>();
            int iCantidad;
            int iIdxGralColumna = 0;

            List<string> asTituloX= new List<string>();
            List<string> asTituloY = new List<string>();
            int iEjeX, iEjeY;

            int idxCampo1;
            int idxCampo2;

            if (iOrden == 1)
            {
                idxCampo1 = 0;
                idxCampo2 = 1;
            }
            else {
                idxCampo1 = 1;
                idxCampo2 = 0;
            }
            // soolo son 3 columans y dependne del orden sobre cual comenzar        

            foreach (DataRow drDato in crsDatos.Rows)
            {

                if (hmColumnas.ContainsKey(drDato[idxCampo2].ToString()) == false )
                {
                    hmColumnas.Add(drDato[idxCampo2].ToString(), iIdxGralColumna);
                    asTituloY.Add(drDato[idxCampo2].ToString());
                    iIdxGralColumna++;
                }

                if (hmMatriz.ContainsKey(drDato[idxCampo1].ToString()) == false )
                {
                    hmMatrizAux = new Dictionary<string, int>();
                    hmMatrizAux.Add(drDato[idxCampo2].ToString(), Convert.ToInt32(drDato[2])); // indice en 2 o 3'
                    hmMatriz.Add(drDato[idxCampo1].ToString(), hmMatrizAux);
                }
                else
                {
                    if (hmMatrizAux.ContainsKey(drDato[idxCampo2].ToString()) == false)
                        hmMatrizAux.Add(drDato[idxCampo2].ToString(), Convert.ToInt32(drDato[2]));
                    else
                        System.Console.WriteLine("Existe un error");
                }
            }

            TabConsultaMdl TabConsultaMdl = new TabConsultaMdl();

            //CREAMOS UNA MATRIZhmColumnas.Count()           
            int [,] iMatriz = new int[hmMatriz.Count(), hmColumnas.Count()];
            iEjeX = 0;

            foreach (KeyValuePair<string, Dictionary<string, int>> sTituloX in hmMatriz)
            {
                //kvp.Key, kvp.Value                
                asTituloX.Add( sTituloX.Key );

                foreach (KeyValuePair<string, int> sTituloY in sTituloX.Value)
                {
                    iEjeY = hmColumnas[sTituloY.Key];

                    hmMatrizAux = hmMatriz[sTituloX.Key];
                    iCantidad = hmMatrizAux[sTituloY.Key];

                    if (iCantidad > 0)
                        iMatriz[iEjeX,iEjeY] = iCantidad;
                    else
                        iMatriz[iEjeX,iEjeY] = 0;
                }
                iEjeX++;
            }

            TabConsultaMdl.asTituloX = asTituloX;
            TabConsultaMdl.asTituloY = asTituloY;
            TabConsultaMdl.Grafica = iMatriz;

            return TabConsultaMdl;
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }

    }
}
