using System.Data.Common;
using SFP.SIT.SERVICES.Model.Rep;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Rep
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
            // A D M I N I S T R A C I O N

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_RECIBIDAS_SFP] = new Func<Object, object>(dmlSelectRecibidasSFP);

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   

        public Object dmlSelectRecibidasSFP(Object oDatos)
        {
            Dictionary<string, Object> pParam = (Dictionary<string, Object>)oDatos;
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append(" WITH NodoAreas AS (  ");
            sbQuery.Append(" select sol.us_clafolio, seg.seg_fecini, ka_sigla, krp_descripcion, tso_descripcion, kar_descripcion, ");
            sbQuery.Append(" seg_diassemaforo, seg_colorsemaforo, us_des, us_dat");
            sbQuery.Append(" from SIT_solicitud sol, SIT_red_nodo nodo, SIT_adm_karea area, SIT_sol_seguimiento seg, SIT_sol_kproceso est, ");
            sbQuery.Append(" SIT_red_ktipo_arista tipari, SIT_sol_ktipo_solicitud tipsol ");
            sbQuery.Append(" where nodo.us_clafolio  = sol.us_clafolio and seg.us_clafolio = sol.us_clafolio and seg.us_clafolio = sol.us_clafolio  ");
            sbQuery.Append(" and seg.krp_claproceso = sol.krp_claproceso and est.krp_claproceso = seg.krp_claproceso ");
            sbQuery.Append(" and nodo.ka_claarea = area.ka_claarea  ");            
            sbQuery.Append(" and tipari.kar_clatipoari = seg.kar_clatipoari ");
            sbQuery.Append(" and tipsol.tso_clatiposol = sol.tso_clatiposol ");
            sbQuery.Append(" and nodo.KA_CLAAREA not in " + pParam[PARAM_NO_AREAS]);
            
            if (pParam.ContainsKey(COL_FECSOL_INI) == true  && pParam.ContainsKey(COL_FECSOL_FIN) == true)
            {
                sbQuery.Append(" and seg.seg_fecini between to_date('");
                sbQuery.Append(pParam[COL_FECSOL_INI]);
                sbQuery.Append("','dd/mm/yyyy') and to_date('");
                sbQuery.Append(pParam[COL_FECSOL_FIN]);
                sbQuery.Append("','dd/mm/yyyy')");
            }

            if (pParam.ContainsKey(COL_SEMAFORO) == true)
            {
                sbQuery.Append(" and seg_colorsemaforo = ");
                sbQuery.Append(pParam[COL_SEMAFORO]);
            }

            if (pParam.ContainsKey(COL_AREA) == true )
            {
                if (Convert.ToInt32(pParam[COL_AREA]) > 0)
                {
                    sbQuery.Append(" and nodo.ka_claarea = ");
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
                    sbQuery.Append(" and seg.kar_clatipoari = ");
                    sbQuery.Append(pParam[COL_TIPO_RESPUESTA]);
                }
            }

            if (pParam.ContainsKey(COL_TIPO_SOLICITUD) == true)
            {
                if (Convert.ToInt32(pParam[COL_TIPO_SOLICITUD]) > 0)
                {
                    sbQuery.Append(" and sol.tso_clatiposol = ");
                    sbQuery.Append(pParam[COL_TIPO_SOLICITUD]);
                }
            }

            if (pParam.ContainsKey(COL_FOLIO) == true)
            {
                sbQuery.Append(" and sol.us_clafolio = ");
                sbQuery.Append(pParam[COL_FOLIO]);
            }

            sbQuery.Append(" group by sol.us_clafolio, seg.seg_fecini, ka_sigla, krp_descripcion, tso_descripcion, kar_descripcion,  ");
            sbQuery.Append("  seg_diassemaforo, seg_colorsemaforo, us_des, us_dat )  ");
            sbQuery.Append(" select us_clafolio, CAST(seg_fecini AS DATE) seg_fecini,  krp_descripcion, tso_descripcion, kar_descripcion, seg_diassemaforo,  ");
            sbQuery.Append(" seg_colorsemaforo, LISTAGG(ka_sigla, ' | ') WITHIN GROUP (ORDER BY ka_sigla)  as Areas, us_des, us_dat ");
            sbQuery.Append(" from NodoAreas nodo ");
            sbQuery.Append(" GROUP BY us_clafolio, seg_fecini, krp_descripcion, tso_descripcion, kar_descripcion, seg_diassemaforo, seg_colorsemaforo, us_des, us_dat  ");

            return ConsultaDML(sbQuery.ToString());
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
