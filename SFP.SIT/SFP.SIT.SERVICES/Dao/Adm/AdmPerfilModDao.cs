using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Adm;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Adm
{
    public class AdmPerfilModDao : BaseDao
    {
        public const Int16 OPE_SELECT_USUARIO_PERFILMOD = 211;

        public const String COL_KU_CLAUSU = "KU_USUARIO";
        public const String COL_PERFILES = "KP_CLAPERFIL";

        public AdmPerfilModDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);
            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_USUARIO_PERFILMOD] = new Func<Object, object>(dmlSelectUsuarioPerfilMod);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            AdmPerfilModMdl dtoDatos = (AdmPerfilModMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_ADM_PERFIL_MOD ( KM_CLAMODULO, KP_CLAPERFIL ) "
                    + " VALUES ( :P0, :P1 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.km_clamodulo, dtoDatos.kp_claperfil);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmPerfilModMdl dtoDatos = (AdmPerfilModMdl)oDatos;
            String sqlQuery = " delete from SIT_ADM_PERFIL_MOD where KM_CLAMODULO = :P0 AND KP_CLAPERFIL = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.km_clamodulo, dtoDatos.kp_claperfil);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmPerfilModMdl> lstDatos = (List<AdmPerfilModMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_ADM_PERFIL_MOD ( KM_CLAMODULO, KP_CLAPERFIL ) "
                    + " VALUES ( :P0, :P1 ) ";

            foreach (AdmPerfilModMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.km_clamodulo, dtoDatos.kp_claperfil);
                iContador++;
            }
            return iContador;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT PM.KM_CLAMODULO, KM_DESCRIPCION, PM.KP_CLAPERFIL, KP_DESCRIPCION "
                + " from SIT_ADM_PERFIL_MOD PM, SIT_ADM_KMODULO MODU, SIT_ADM_KPERFIL PER "
                + " WHERE MODU.KM_CLAMODULO = PM.KM_CLAMODULO "
                + " AND PER.KP_CLAPERFIL = PM.KP_CLAPERFIL ORDER BY KP_CLAPERFIL, KM_CLAMODULO "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";

            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        public List<AdmModuloMdl> dmlSelectUsuarioPerfilMod(Object oDatos)
        {
            Dictionary<string, Object> dicParametros = (Dictionary<string, Object>)oDatos;
            List<Tuple<int, string>> lstPerArea = (List<Tuple<int, string>>) dicParametros[COL_PERFILES];

            StringBuilder sbParam = new StringBuilder();
            
            foreach (Tuple<int, string> tuUsuPerAreaDesc in lstPerArea)
            {
                sbParam.Append(",");
                sbParam.Append(tuUsuPerAreaDesc.Item1);
            }

            String sqlQuery = " SELECT modu.km_clamodulo, km_descripcion, km_control, km_metodo FROM SIT_adm_KModulo modu, SIT_ADM_PERFIL_MOD pmod "
            + " where modu.km_clamodulo = pmod.km_clamodulo and KP_CLAPERFIL in (" + sbParam.ToString().Substring(1) +  " ) "
            + " GROUP BY modu.km_clamodulo, km_descripcion, km_control, km_metodo ORDER BY  modu.km_clamodulo ";


            List<AdmModuloMdl> lstAdmModuloMdl = new List<AdmModuloMdl>();
            DataTable dtDatos = (DataTable)ConsultaDML(sqlQuery);

            for (int iIdx = 0; iIdx < dtDatos.Rows.Count; iIdx++)
            {
                lstAdmModuloMdl.Add(new AdmModuloMdl(Convert.ToInt32(dtDatos.Rows[iIdx][0]), 0, 0, dtDatos.Rows[iIdx][1].ToString(),
                    dtDatos.Rows[iIdx][2].ToString(), dtDatos.Rows[iIdx][3].ToString(), new DateTime()));
            }


            return lstAdmModuloMdl;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
