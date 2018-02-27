using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Adm;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Adm
{
    public class AdmPerfilDao : BaseDao
    {
        public const Int16 OPE_SELECT_COMBO_ADMINISTRADOR = 211;
        public const Int16 OPE_SELECT_DICCIONARIO_PERFIL = 212;
        public const Int16 OPE_SELECT_COMBO_SISTEMAS = 213;

        int iSecuencia { get; set; }

        public AdmPerfilDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_HABILITAR] = new Func<Object, object>(dmlHabilitar);
            dicOperacion[OPE_DESHABILITAR] = new Func<Object, object>(dmlDeshabilitar);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            dicOperacion[OPE_SELECT_DICCIONARIO_PERFIL] = new Func<Object, object>(dmlSelectHashMapPerfil);
            dicOperacion[OPE_SELECT_COMBO_ADMINISTRADOR] = new Func<Object, object>(dmlSelectComboAdministrador);
            dicOperacion[OPE_SELECT_COMBO_SISTEMAS] = new Func<Object, object>(dmlSelectComboSistemas);

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                          
        private Object dmlInsert(Object oDatos)
        {
            iSecuencia = SecuenciaDML("SEC_SIT_KPERFIL");
            AdmPerfilMdl dtoDatos = (AdmPerfilMdl)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_ADM_KPERFIL ( KP_CLAPERFIL, KP_DESCRIPCION, KP_SIGLA, KP_MULTIPLE, KP_FECBAJA ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, NULL ) ";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.kp_descripcion, dtoDatos.kp_sigla, dtoDatos.kp_multiple);
        }

        private Object dmlUpdate(Object oDatos)
        {
            AdmPerfilMdl dtoDatos = (AdmPerfilMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KPERFIL "
                    + " set KP_DESCRIPCION = :P0, KP_SIGLA = :P1, KP_MULTIPLE = :P2,  KP_FECBAJA = :P3"
                    + " where KP_CLAPERFIL = :P4 ";

            return EjecutaDML(sqlQuery, dtoDatos.kp_descripcion, dtoDatos.kp_sigla, dtoDatos.kp_multiple, dtoDatos.kp_fecbaja, dtoDatos.kp_claperfil);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmPerfilMdl dtoDatos = (AdmPerfilMdl)oDatos;
            String sqlQuery = " delete from SIT_ADM_KPERFIL where KP_CLAPERFIL = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kp_claperfil);
        }

        public Object dmlHabilitar(Object oDatos)
        {
            AdmPerfilMdl dtoDatos = (AdmPerfilMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KPERFIL set KP_FECBAJA = null where KP_CLAPERFIL = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kp_claperfil);
        }

        public Object dmlDeshabilitar(Object oDatos)
        {
            AdmPerfilMdl dtoDatos = (AdmPerfilMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KPERFIL set KP_FECBAJA = sysdate where KP_CLAPERFIL = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kp_claperfil);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmPerfilMdl> lstDatos = (List<AdmPerfilMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_ADM_KPERFIL ( KP_CLAPERFIL, KP_DESCRIPCION, KP_SIGLA, KP_MULTIPLE, KP_FECBAJA ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, NULL) ";

            foreach (AdmPerfilMdl dtoDatos in lstDatos)
            { 
                 EjecutaDML(sqlQuery, dtoDatos.kp_claperfil, dtoDatos.kp_descripcion, dtoDatos.kp_sigla, dtoDatos.kp_multiple);
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
                + "SELECT KP_CLAPERFIL, KP_DESCRIPCION, KP_SIGLA, KP_MULTIPLE, KP_FECBAJA  "
                + " from SIT_ADM_KPERFIL "
                + " order by KP_CLAPERFIL "
                +" ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select KP_CLAPERFIL as id, KP_DESCRIPCION as text FROM SIT_ADM_KPERFIL where KP_FECBAJA IS NULL ORDER BY KP_CLAPERFIL";
            return (DataTable) ConsultaDML(sqlQuery);
        }

        private DataTable dmlSelectComboAdministrador(Object oDatos)
        {
            String sqlQuery = " Select KP_CLAPERFIL, KP_DESCRIPCION, KP_FECBAJA, KP_MULTIPLE FROM SIT_ADM_KPERFIL where KP_FECBAJA IS NULL AND KP_CLAPERFIL > 1 ORDER BY KP_CLAPERFIL";
            return (DataTable) ConsultaDML(sqlQuery);
        }

        private DataTable dmlSelectComboSistemas(Object oDatos)
        {
            String sqlQuery = " Select KP_CLAPERFIL, KP_DESCRIPCION, KP_FECBAJA, KP_MULTIPLE, KP_AREA FROM SIT_ADM_KPERFIL where KP_FECBAJA IS NULL  ORDER BY KP_CLAPERFIL";
            return (DataTable)ConsultaDML(sqlQuery);
        }

        private Dictionary<int, AdmPerfilMdl> dmlSelectHashMapPerfil(Object oDatos)
        {
            Dictionary<int, AdmPerfilMdl> dicPerfil = new Dictionary<int, AdmPerfilMdl>();
            DataTable dtDatos;
            int iPerfil; 

            string sqlQuery = " Select  KP_CLAPERFIL, KP_DESCRIPCION, KP_FECBAJA, KP_SIGLA, KP_MULTIPLE FROM SIT_ADM_KPERFIL where KP_FECBAJA IS NULL ORDER BY KP_CLAPERFIL";
            dtDatos = (DataTable) ConsultaDML(sqlQuery);
            foreach (DataRow row in dtDatos.Rows)
            {
                iPerfil = Convert.ToInt32(row["KP_CLAPERFIL"]);
                dicPerfil.Add(iPerfil, new AdmPerfilMdl(
                    iPerfil, row["KP_DESCRIPCION"].ToString(), new DateTime(), row["KP_SIGLA"].ToString(), Convert.ToInt32(row["KP_MULTIPLE"] )));

            }
            return dicPerfil;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
