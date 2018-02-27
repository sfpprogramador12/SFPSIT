using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.SIT.SERVICES.Model.Adm;
using SFP.Persistencia;
using System.Data;
using SFP.SIT.SERVICES.Model;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Adm
{
    public class AdmUsuAreaDao : BaseDao
    {
        public const int OPE_ACTUALIZAR_PERFIL = 101;

        public const int OPE_SELECT_VALIDAR_USUARIO = 211;
        public const int OPE_SELECT_USUARIO_AREA_DESC = 212;
        public const int OPE_SELECT_USUARIO_AREA = 213;
        public const int OPE_SELECT_UPA_ARBOL = 214;
        
        public const int OPE_SELECT_USUARIO_AREAS = 215;
        public const int OPE_SELECT_TURNAR_ARBOL = 216;
        public const int OPE_SELECT_OUSU_USUARIOS_AREA = 217;

        public const string COL_KU_CLAUSU = "KU_CLAUSU";
        public const string PARAM_AREAS = "PARAM_AREAS";
        public const string PARAM_COL_KA_CLAAREA = "KA_CLAAREA";
        

        int iSecuencia { get; set; }

        public AdmUsuAreaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);
            dicOperacion[OPE_ACTUALIZAR_PERFIL] = new Func<Object, object>(dmlActualizarPerfil);
            

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_USUARIO_AREA_DESC] = new Func<Object, object>(dmlUsuAreaDesc);
            dicOperacion[OPE_SELECT_USUARIO_AREA] = new Func<Object, object>(dmlUsuarioArea);
            dicOperacion[OPE_SELECT_UPA_ARBOL] = new Func<Object, object>(dmlUPAarbol);
            dicOperacion[OPE_SELECT_TURNAR_ARBOL] = new Func<Object, object>(dmlTurnarArbol);

           
            dicOperacion[OPE_SELECT_USUARIO_AREAS] = new Func<Object, object>(dmlUsuarioAreas);
            dicOperacion[OPE_SELECT_OUSU_USUARIOS_AREA] = new Func<Object, object>(dmlOUsuarioAreas);
            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            AdmUsuAreaMdl dtoDatos = (AdmUsuAreaMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_ADM_USER_AREA ( KU_CLAUSU, KA_CLAAREA ) "
                    + " VALUES ( :P0, :P1 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.ku_clausu, dtoDatos.ka_claarea);
        }
        private Object dmlDelete(Object oDatos)
        {
            AdmUsuAreaMdl dtoDatos = (AdmUsuAreaMdl)oDatos;
            String sqlQuery = " delete from SIT_ADM_USER_AREA where KU_CLAUSU = :P0 AND KA_CLAAREA = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.ku_clausu, dtoDatos.ka_claarea);
        }
        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmUsuAreaMdl> lstDatos = (List<AdmUsuAreaMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_ADM_USER_AREA ( KU_CLAUSU, KA_CLAAREA) "
                    + " VALUES ( :P0, :P1 ) ";

            foreach (AdmUsuAreaMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.ku_clausu, dtoDatos.ka_claarea);
                iContador++;
            }
            return iContador;

        }
        private Object dmlActualizarPerfil(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            // PRIMERO BORRAMOS LOS DATOS...

            EjecutaDML("DELETE FROM SIT_ADM_USER_AREA WHERE KU_CLAUSU = :P0", dicParam[COL_KU_CLAUSU]);


            String sqlQuery = " INSERT INTO SIT_ADM_USER_AREA ( KU_CLAUSU, KA_CLAAREA )"
                + " SELECT " + dicParam[COL_KU_CLAUSU] + ", KA_CLAAREA FROM SIT_ADM_KAREA   "
                + " WHERE KA_CLAAREA in ( " + dicParam[PARAM_AREAS] + " )";

            return EjecutaDML(sqlQuery);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                " SELECT UPA.KU_CLAUSU, UPA.KA_CLAAREA, KU_CORREO,  KA_DESCRIPCION " +
                " from SIT_ADM_USER_AREA UPA, SIT_ADM_USUARIO US, SIT_ADM_KAREA AR " +
                " WHERE US.KU_CLAUSU = UPA.KU_CLAUSU " +
                "  AND AR.KA_CLAAREA = UPA.KA_CLAAREA " +
                " order by KU_CORREO,  UPA.KA_CLAAREA "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        public List<Tuple<int, string>> dmlUsuAreaDesc(Object oDatos)
        {
            List<Tuple<int, string>> lstUsuPerArea = new List<Tuple<int, string>>();

            Dictionary<string, object> dicParam = (Dictionary<string, object>) oDatos;
            string sqlQuery = " SELECT KP_CLAPERFIL as id, KA_SIGLA as text FROM SIT_ADM_USER_AREA up, SIT_adm_karea a "
                + " WHERE up.ka_claarea = a.ka_claarea and ku_clausu = :P0 GROUP BY KP_CLAPERFIL, KA_SIGLA  ";

            DataTable dtDatos = (DataTable)ConsultaDML(sqlQuery, dicParam[COL_KU_CLAUSU]);
            for (int iIdx = 0; iIdx < dtDatos.Rows.Count; iIdx++)
            {
                lstUsuPerArea.Add(new Tuple<int, string>(Convert.ToInt32(dtDatos.Rows[iIdx][0]), dtDatos.Rows[iIdx][0].ToString()));                
            }
                               
            return lstUsuPerArea;
        }
        public List<AdmUsuAreaMdl>  dmlUsuarioArea(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            string sqlQuery = " select KU_CLAUSU, KA_CLAAREA from SIT_ADM_USER_AREA WHERE ku_clausu = :P0 and  KA_CLAAREA > 0 ";

            return (List<AdmUsuAreaMdl>) CrearListaMDL(ConsultaDML(sqlQuery, dicParam[COL_KU_CLAUSU]) );
        }
        
        private DataTable dmlUPAarbol(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;
            String sqlQuery = " SELECT area.KA_CLAAREA, KA_DESCRIPCION, KA_REPORTA, area.KP_CLAPERFIL, upa.KA_CLAAREA as activo ";
            ////    + " FROM SIT_adm_karea area "
            ////    + " LEFT JOIN SIT_ADM_Kperfil perfil ON area.kp_claperfil = perfil.kp_claperfil "
            ////    + " LEFT JOIN SIT_ADM_USER_AREA upa ON KU_CLAUSU = :P0  "
            ////    + " AND upa.KP_CLAPERFIL = perfil.KP_CLAPERFIL AND area.KA_CLAAREA = upa.KA_CLAAREA  "
            ////    + " WHERE "
            ////    + " ka_fecbaja is null "
            ////    + " AND perfil.kp_claperfil > 1 "
            ////    + " order by  kp_multiple, area.kp_claperfil ";        
            return (DataTable)ConsultaDML(sqlQuery, dicParam[COL_KU_CLAUSU]  );
        }
        private DataTable dmlTurnarArbol(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;
            String sqlQuery = " SELECT area.KA_CLAAREA, KA_DESCRIPCION, KA_REPORTA, area.KP_CLAPERFIL, upa.KA_CLAAREA as activo ";
                ////+ " FROM SIT_adm_karea area "
                ////+ " LEFT JOIN SIT_ADM_Kperfil perfil ON area.kp_claperfil = perfil.kp_claperfil "
                ////+ " LEFT JOIN SIT_ADM_USER_AREA upa ON KU_CLAUSU = :P0  "
                ////+ " AND upa.KP_CLAPERFIL = perfil.KP_CLAPERFIL AND area.KA_CLAAREA = upa.KA_CLAAREA "
                ////+ " WHERE ka_fecbaja is null "
                ////+ " AND perfil.kp_claperfil = " + Util.Constantes.Perfil.UA
                ////+ " order by   area.kta_clatipo_area, area.ka_descripcion ";

            return (DataTable)ConsultaDML(sqlQuery, dicParam[COL_KU_CLAUSU]);
        }               
        public DataTable dmlUsuarioAreas(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;
            List<AdmAreaMdl> lstDatos = new List<AdmAreaMdl>();

            string sqlQuery = "select area.KA_CLAAREA as id,  area.KA_SIGLA as text "
            + " from  SIT_ADM_USER_AREA pua, SIT_ADM_KAREA area "
            + " WHERE pua.KA_CLAAREA = area.KA_CLAAREA and ku_clausu = :P0 and area.KA_CLAAREA > 0"
            + " GROUP BY area.KA_CLAAREA, area.KA_SIGLA ORDER BY area.KA_CLAAREA ";

            return ConsultaDML(sqlQuery, dicParam[COL_KU_CLAUSU]);
        }

        public DataTable dmlOUsuarioAreas(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;
            List<AdmAreaMdl> lstDatos = new List<AdmAreaMdl>();

            string sqlQuery = " Select KU_nombre || ' ' || ku_paterno || ' ' || ku_materno as nombre, ku_correo  "
                + " from SIT_ADM_USUARIO usu, SIT_ADM_USER_AREA ua "
                + " where ua.ka_claarea = :P0 "
                + " and usu.ku_clausu = ua.ku_clausu ";

            return  ConsultaDML(sqlQuery, dicParam[PARAM_COL_KA_CLAAREA]);
        }
        
        protected override object CrearListaMDL(DataTable dtDatos)
        {
            List<AdmUsuAreaMdl> lstDatos = new List<AdmUsuAreaMdl>();
            if (dtDatos.Rows.Count > 0)
                foreach (DataRow drDatos in dtDatos.Rows)
                {
                    AdmUsuAreaMdl usuPerAreaMdl = new AdmUsuAreaMdl(
                        Convert.ToInt32(drDatos[0]), Convert.ToInt32(drDatos[1]));
                    lstDatos.Add(usuPerAreaMdl);
                }
            return lstDatos;
        }
    }
}
