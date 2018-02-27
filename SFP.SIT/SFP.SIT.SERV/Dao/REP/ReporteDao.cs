using SFP.Persistencia.Dao;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Dao.REP
{
    public class ReporteDao : BaseDao
    { 
        public const int OPE_SELECT_RECIBIDAS_SFP = 211;

        public const String COL_NUM_REPORTE = "REP";
        public const String COL_STATUS_SOLICITUD = "STATUS";
        public const String COL_AREA = "AREA";
        public const String COL_TIPO_SOLICITUD = "TIPSOL";
        public const String COL_TIPO_RESPUESTA = "TIPRES";
        public const String COL_FOLIO = "FOLIO";
        public const String COL_FECSOL_INI = "FECSOLINI";
        public const String COL_FECSOL_FIN = "FECSOLFIN";
        public const String COL_SEMAFORO = "SEMAFORO";

        public const String PARAM_NO_AREAS = "PARAM_NO_AREAS";


        public static String COL_KA_CLAAREA;

        public ReporteDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
                : base(cn, transaction, sDataAdapter)
            {
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   

        public Object dmlSelectRecibidasSFP(Object oDatos)
        {
            Dictionary<string, Object> pParam = (Dictionary<string, Object>)oDatos;
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append(" WITH NodoAreas AS (  ");
            sbQuery.Append(" select sol.solclave, seg.segfecini, ARHSIGLAS, prcdescripcion, sotdescripcion, rtpdescripcion, ");
            sbQuery.Append(" segdiassemaforo, segsemaforocolor, soldes, soldat");
            sbQuery.Append(" from SIT_SOL_SOLICITUD sol, SIT_red_nodo nodo, SIT_adm_areahist area, SIT_sol_seguimiento seg, SIT_SOL_PROCESO est, ");
            sbQuery.Append(" SIT_RESP_TIPO tipari, SIT_SOL_SOLICITUDTIPO tipsol ");
            sbQuery.Append(" where nodo.solclave  = sol.solclave and seg.solclave = sol.solclave and seg.solclave = sol.solclave  ");
            sbQuery.Append(" and seg.prcclave = sol.prcclave and est.prcclave = seg.prcclave ");
            sbQuery.Append(" and nodo.ARACLAVE = area.ARACLAVE  ");
            sbQuery.Append(" and tipari.rtpclave = seg.repclave ");
            sbQuery.Append(" and tipsol.sotclave = sol.sotclave ");
            //sbQuery.Append(" and nodo.ARACLAVE not in " + pParam[PARAM_NO_AREAS]);

            if (pParam.ContainsKey(COL_FECSOL_INI) == true && pParam.ContainsKey(COL_FECSOL_FIN) == true)
            {
                sbQuery.Append(" and seg.segfecini between to_date('");
                sbQuery.Append(pParam[COL_FECSOL_INI]);
                sbQuery.Append("','dd/mm/yyyy') and to_date('");
                sbQuery.Append(pParam[COL_FECSOL_FIN]);
                sbQuery.Append("','dd/mm/yyyy')");
            }

            if (pParam.ContainsKey(COL_SEMAFORO) == true)
            {
                sbQuery.Append(" and segsemaforocolor = ");
                sbQuery.Append(pParam[COL_SEMAFORO]);
            }

            if (pParam.ContainsKey(COL_AREA) == true)
            {
                if (Convert.ToInt32(pParam[COL_AREA]) > 0)
                {
                    sbQuery.Append(" and nodo.ARACLAVE = ");
                    sbQuery.Append(pParam[COL_AREA]);
                }
            }

            if (pParam.ContainsKey(COL_STATUS_SOLICITUD) == true)
            {
                if (Convert.ToInt32(pParam[COL_STATUS_SOLICITUD]) > 0)
                {
                    sbQuery.Append(" and seg_estado = ");
                    sbQuery.Append(pParam[COL_STATUS_SOLICITUD]);
                }
            }

            if (pParam.ContainsKey(COL_TIPO_RESPUESTA) == true)
            {
                if (Convert.ToInt32(pParam[COL_TIPO_RESPUESTA]) > 0)
                {
                    sbQuery.Append(" and seg.repclave = ");
                    sbQuery.Append(pParam[COL_TIPO_RESPUESTA]);
                }
            }

            if (pParam.ContainsKey(COL_TIPO_SOLICITUD) == true)
            {
                if (Convert.ToInt32(pParam[COL_TIPO_SOLICITUD]) > 0)
                {
                    sbQuery.Append(" and sol.sotclave = ");
                    sbQuery.Append(pParam[COL_TIPO_SOLICITUD]);
                }
            }

            if (pParam.ContainsKey(COL_FOLIO) == true)
            {
                sbQuery.Append(" and sol.solclave = ");
                sbQuery.Append(pParam[COL_FOLIO]);
            }

            sbQuery.Append(" group by sol.solclave, seg.segfecini, ARHSIGLAS, prcdescripcion, sotdescripcion, rtpdescripcion,  ");
            sbQuery.Append("  segdiassemaforo, segsemaforocolor, soldes, soldat )  ");
            sbQuery.Append(" select solclave, CAST(segfecini AS DATE) segfecini,  prcdescripcion, sotdescripcion, rtpdescripcion, segdiassemaforo,  ");
            sbQuery.Append(" segsemaforocolor, LISTAGG(ARHSIGLAS, ' | ') WITHIN GROUP (ORDER BY ARHSIGLAS)  as Areas, soldes, soldat ");
            sbQuery.Append(" from NodoAreas nodo ");
            sbQuery.Append(" GROUP BY solclave, segfecini, prcdescripcion, sotdescripcion, rtpdescripcion, segdiassemaforo, segsemaforocolor, soldes, soldat  ");

            return ConsultaDML(sbQuery.ToString());
        }

    }
}
