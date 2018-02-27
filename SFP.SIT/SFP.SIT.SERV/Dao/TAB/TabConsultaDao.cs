using SFP.Persistencia.Dao;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.TAB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Dao.TAB
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

        public const String COL_solfecsol_FECINI = "solfecsol_fecini";
        public const String COL_solfecsol_FECFIN = "solfecsol_fecfin";
        public const String PARAM_ORDEN = "orden";
        public const String PARAM_RENGLON = "renglon";
        public const String PARAM_COLUMNA = "columna";
        public const String PARAM_NO_AREAS = "no_areas";
        public const String PARAM_OPERACION = "Operacion";

        public Dictionary<int, Delegate> dicOperacion = new Dictionary<int, Delegate>();
        public TabConsultaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
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

        public object EjecutarOperacion(Object oDatos)
        {
            Dictionary<String, Object> dicParam = (Dictionary<String, Object>)oDatos;

            return dicOperacion[(int)dicParam[PARAM_OPERACION]].DynamicInvoke(oDatos);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              

        public Object dmlSelectTableroSolicitudArea(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery = " WITH datos_sol AS" +
                "( SELECT sol.solclave,  KA_SIGLA, tsol.sotdescripcion " +
                " FROM SIT_SOL_SOLICITUD sol, SIT_red_nodo nodo, SIT_adm_areahist area, SIT_SOL_SOLICITUDTIPO tsol " +
                " WHERE sol.solclave = nodo.solclave " +
                " and nodo.ka_claarea not in  " + dicParam[PARAM_NO_AREAS] +
                " AND nodo.ka_claarea = area.ka_claarea and tsol.sotclave = sol.sotclave " +
                " AND sol.solfecsol between to_date(:P0, 'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy') " +
                " GROUP BY sol.solclave, ka_descripcion, KA_SIGLA, sotdescripcion " +
                " ORDER BY tsol.sotdescripcion, sol.solclave ) " +
                " SELECT  KA_SIGLA, sotdescripcion, count(*) from datos_sol  " +
                " GROUP BY KA_SIGLA,sotdescripcion" +
                " ORDER BY 2,3 DESC ";

            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]),
                Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        public Object dmlSelectTableroSolicitudUsuario(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery = " WITH datos_sol AS " +
                " ( SELECT sol.solclave, usrnombre, tsol.sotdescripcion " +
                " FROM SIT_SOL_SOLICITUD sol, SIT_red_nodo nodo, SIT_red_arista arista, SIT_adm_usuario usu, SIT_SOL_SOLICITUDTIPO tsol  " +
                " WHERE nodo.solclave = sol.solclave AND arista.solclave = sol.solclave AND arista.nodorigen = nodo.nodclave  " +
                " AND arista.usrclave = usu.usrclave and tsol.sotclave = sol.sotclave  " +
                " AND nodo.ka_claarea not in  " + dicParam[PARAM_NO_AREAS] +
                " AND sol.solfecsol between to_date(:P0, 'dd/mm/yyyy') and to_date(:P1, 'dd/mm/yyyy')  " +
                " AND arista.arihito = 1 " +
                " GROUP BY sol.solclave, usrnombre, tsol.sotdescripcion " +
                " ORDER BY sol.solclave)  " +
                " SELECT usrnombre, sotdescripcion, count(*) from datos_sol   " +
                " GROUP BY usrnombre, sotdescripcion " +
                " ORDER BY 2,3 DESC ";

            return convertirHashMap(ConsultaDML(sQuery, 
                dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        public Object dmlSelectTableroSolicitudRespuesta(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            //////////// ARREGLAR
            ////////////String sQuery  = " SELECT rtpdescripcion, tsol.sotdescripcion, count(*)  " +
            ////////////        " FROM SIT_SOL_SOLICITUD sol, SIT_sol_seguimiento seg, SIT_SOL_SOLICITUDTIPO tsol, SIT_RESP_TIPO tiparis " +
            ////////////        " WHERE seg.solclave = sol.solclave AND seg.prcclave = sol.prcclave " +
            ////////////        " AND tsol.sotclave = sol.sotclave  AND seg.rtpclave = tiparis.rtpclave " +
            ////////////        " AND sol.solfecsol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')  " +
            ////////////        " AND seg.rtpclave <> " + AfdConstantes.RESPUESTA.ERROR +
            ////////////        " GROUP BY rtpdescripcion, tsol.sotdescripcion  " +
            ////////////        " ORDER BY  2,3 DESC ";

            ////////////return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));

            return null;
        }

        public Object dmlSelectTableroSolicitudEstado(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery = " SELECT prcdescripcion, tsol.sotdescripcion, count(*) " +
                " FROM SIT_SOL_SOLICITUD sol, SIT_sol_seguimiento seg, SIT_SOL_SOLICITUDTIPO tsol,  SIT_SOL_PROCESO edo" +
                " WHERE seg.solclave = sol.solclave AND seg.prcclave = sol.prcclave AND edo.prcclave = sol.prcclave " +
                " AND tsol.sotclave = sol.sotclave  " +
                " AND sol.solfecsol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy') " +
                " GROUP BY prcdescripcion, tsol.sotdescripcion  " +
                " ORDER BY  2,3 DESC ";


            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        public Object dmlSelectTableroAreaUsuario(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery = " SELECT area.ka_descripcion, usu.usrnombre, count(*)  "
                + "  FROM SIT_SOL_SOLICITUD sol, SIT_red_arista arista, SIT_adm_usuario usu, SIT_red_nodo nodo, SIT_adm_areahist area "
                + " where nodo.solclave = sol.solclave"
                + " and usu.usrclave = arista.usrclave AND nodo.ka_claarea = area.ka_claarea"
                + " and nodo.nodclave = arista.nodorigen and nodo.solclave = arista.solclave"
                + " AND nodo.ka_claarea not in  " + dicParam[PARAM_NO_AREAS]
                + " AND sol.solfecsol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy') "
                + " AND arista.arihito = 1 "
                + " GROUP BY  area.ka_descripcion, usu.usrnombre"
                + " ORDER BY 2,3 DESC ";

            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        public Object dmlSelectTableroAreaRespuesta(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery = " SELECT  area.ka_descripcion, rtpdescripcion, count(*) "
                + " FROM SIT_SOL_SOLICITUD sol, SIT_red_nodo nodo, SIT_adm_areahist area, SIT_red_arista arista,  SIT_RESP_TIPO tipoAri "
                + " where nodo.solclave = sol.solclave "
                + " and arista.solclave = nodo.solclave and arista.nodorigen= nodo.nodclave "
                + " AND nodo.ka_claarea = area.ka_claarea  "
                + " AND tipoAri.rtpclave =  arista.rtpclave "
                + " AND nodo.ka_claarea not in  " + dicParam[PARAM_NO_AREAS]
                + " AND sol.solfecsol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')  "
                + " AND arista.arihito = 1 "
                + " GROUP BY  area.ka_descripcion, rtpdescripcion "
                + " ORDER BY 1,2 DESC ";

            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        public Object dmlSelectTableroAreaEstado(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            //////////// ARREGLAR
            ////////String sQuery  = " SELECT area.KA_DESCRIPCION, prcdescripcion, count(*)  "
            ////////    + " FROM SIT_SOL_SOLICITUD sol, SIT_sol_seguimiento seg, SIT_SOL_PROCESO edo, SIT_red_nodo nodo, SIT_adm_areahist area "
            ////////    + " WHERE sol.solclave = seg.solclave and edo.prcclave = seg.prcclave "
            ////////    + " and sol.solclave = nodo.solclave and area.KA_CLAAREA = nodo.KA_CLAAREA "
            ////////    + " AND sol.solfecsol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy') "
            ////////    + " and nodo.nodclave in ( " + AfdConstantes.ESTADO.INAI_RESPUESTA_SOLICITUD + "," + AfdConstantes.ESTADO.INAI_RESPUESTA_ACLARACION + ")"
            ////////    + " GROUP BY area.KA_DESCRIPCION, prcdescripcion"
            ////////    + " ORDER BY 2,3 DESC ";
            ////////return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));

            return null;
        }

        public Object dmlSelectTableroUsuarioRespuesta(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            String sQuery = " SELECT usu.usrnombre, rtpdescripcion, count(*) "
                + " FROM SIT_SOL_SOLICITUD sol, SIT_red_arista arista, SIT_red_nodo nodo, "
                + " SIT_RESP_TIPO tipoAri, SIT_adm_usuario usu "
                + " WHERE nodo.solclave = sol.solclave     "
                + " and arista.solclave = sol.solclave     "
                + " and arista.nodorigen = nodo.nodclave "
                + " and tipoAri.rtpclave = arista.rtpclave "
                + " and usu.usrclave = arista.usrclave "
                + " AND nodo.ka_claarea > 0 and nodo.ka_claarea <> 7 and arista.rtpclave <> 30 "
                + " AND sol.solfecsol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')   "
                + " AND arista.arihito = 1 "
                + " group by usu.usrnombre, rtpdescripcion "
                + " order by 2,3 desc";

            return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));
        }

        public Object dmlSelectTableroUsuarioEstado(object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            ////////String sQuery  = " SELECT usu.usrnombre, prcdescripcion, count(*)  "
            ////////    + " FROM SIT_SOL_SOLICITUD sol, SIT_sol_seguimiento seg, SIT_SOL_PROCESO edo, SIT_red_nodo nodo, SIT_red_arista arista, SIT_adm_usuario usu "
            ////////    + " WHERE sol.solclave = seg.solclave and edo.prcclave = seg.prcclave "
            ////////    + " and sol.solclave = nodo.solclave  "
            ////////    + " and arista.solclave = nodo.solclave and arista.nodorigen = nodo.nodclave and arista.usrclave = usu.usrclave"
            ////////    + " AND sol.solfecsol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')   "
            ////////    + " and nodo.nodclave in ( " + AfdConstantes.ESTADO.INAI_RESPUESTA_SOLICITUD + "," + AfdConstantes.ESTADO.INAI_RESPUESTA_ACLARACION  + ")"
            ////////    + " AND arista.arihito = 1 "
            ////////    + " GROUP BY usu.usrnombre, prcdescripcion"
            ////////    + " order by 2,3 desc";
            ////////return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));


            //////////// ARREGLAR
            return null;
        }

        public Object dmlSelectTableroEstadoRespuesta(object oDatos)
        {
            //////Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;
            //////////// ARREGLAR
            //////String sQuery  = " SELECT edo.prcdescripcion, rtpdescripcion, count(*)  "
            //////    + " FROM SIT_SOL_SOLICITUD sol, SIT_sol_seguimiento seg, SIT_RESP_TIPO tiparis, SIT_SOL_PROCESO edo "
            //////    + " WHERE  sol.solclave = seg.solclave and sol.prcclave = seg.prcclave "
            //////    + " and seg.rtpclave = tiparis.rtpclave and seg.prcclave = edo.prcclave"
            //////    + " AND sol.solfecsol between to_date(:P0,'dd/mm/yyyy') and to_date(:P1,'dd/mm/yyyy')   "
            //////    + " AND seg.rtpclave <> " + AfdConstantes.RESPUESTA.ERROR 
            //////    + " GROUP BY  prcdescripcion, rtpdescripcion  "
            //////    + " order by 2,3 desc ";
            //////return convertirHashMap(ConsultaDML(sQuery, dicParam[COL_solfecsol_FECINI], dicParam[COL_solfecsol_FECFIN]), Convert.ToInt32(dicParam[PARAM_ORDEN]));


            return null;
        }


        public TabConsultaMdl convertirHashMap(DataTable crsDatos, int iOrden)
        {
            Dictionary<String, Dictionary<String, int>> hmMatriz = new Dictionary<String, Dictionary<String, int>>();
            Dictionary<String, int> hmMatrizAux = null;

            Dictionary<String, int> hmColumnas = new Dictionary<String, int>();
            int iCantidad;
            int iIdxGralColumna = 0;

            List<string> asTituloX = new List<string>();
            List<string> asTituloY = new List<string>();
            int iEjeX, iEjeY;

            int idxCampo1;
            int idxCampo2;

            if (iOrden == 1)
            {
                idxCampo1 = 0;
                idxCampo2 = 1;
            }
            else
            {
                idxCampo1 = 1;
                idxCampo2 = 0;
            }
            // soolo son 3 columans y dependne del orden sobre cual comenzar        

            foreach (DataRow drDato in crsDatos.Rows)
            {

                if (hmColumnas.ContainsKey(drDato[idxCampo2].ToString()) == false)
                {
                    hmColumnas.Add(drDato[idxCampo2].ToString(), iIdxGralColumna);
                    asTituloY.Add(drDato[idxCampo2].ToString());
                    iIdxGralColumna++;
                }

                if (hmMatriz.ContainsKey(drDato[idxCampo1].ToString()) == false)
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
            int[,] iMatriz = new int[hmMatriz.Count(), hmColumnas.Count()];
            iEjeX = 0;

            foreach (KeyValuePair<string, Dictionary<string, int>> sTituloX in hmMatriz)
            {
                //kvp.Key, kvp.Value                
                asTituloX.Add(sTituloX.Key);

                foreach (KeyValuePair<string, int> sTituloY in sTituloX.Value)
                {
                    iEjeY = hmColumnas[sTituloY.Key];

                    hmMatrizAux = hmMatriz[sTituloX.Key];
                    iCantidad = hmMatrizAux[sTituloY.Key];

                    if (iCantidad > 0)
                        iMatriz[iEjeX, iEjeY] = iCantidad;
                    else
                        iMatriz[iEjeX, iEjeY] = 0;
                }
                iEjeX++;
            }

            TabConsultaMdl.asTituloX = asTituloX;
            TabConsultaMdl.asTituloY = asTituloY;
            TabConsultaMdl.Grafica = iMatriz;

            return TabConsultaMdl;
        }


    }
}
