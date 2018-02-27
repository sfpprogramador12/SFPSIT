using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Adm;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;
using SFP.SIT.SERVICES.Util;

namespace SFP.SIT.SERVICES.Dao.Adm
{
    public class AdmUsuarioDao : BaseDao
    {      
        public const string COL_KU_CONTRASEÑA = "KU_CONTRASEÑA";
        public const string COL_KU_CORREO = "KU_CORREO";

        public const int OPE_SELECT_VALIDAR_USUARIO = 211;
        public const int OPE_SELECT_VALIDAR_CORREO = 212;
        public const int OPE_SELECT_GRID_PERFIL = 213;
        public const int OPE_UPD_USUARIO = 214;
        public const int OPE_SEL_EXISTE_USUARIO = 215;

        int iSecuencia { get; set; }
        public AdmUsuarioDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_HABILITAR] = new Func<Object, object>(dmlHabilitar);
            dicOperacion[OPE_DESHABILITAR] = new Func<Object, object>(dmlDeshabilitar);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);
            dicOperacion[OPE_UPD_USUARIO] = new Func<Object, object>(dmlUpdContraseña);


            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
            dicOperacion[OPE_SELECT_VALIDAR_USUARIO] = new Func<Object, object>(dmlSelectValidarUsuario);
            dicOperacion[OPE_SELECT_VALIDAR_CORREO] = new Func<Object, object>(dmlSelectValidarCorreo);
            
            dicOperacion[OPE_SELECT_GRID_PERFIL] = new Func<Object, object>(dmlSelectGridPerfil);
            dicOperacion[OPE_SEL_EXISTE_USUARIO] = new Func<Object, object>(dmlSelExisteUsu);
            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            AdmUsuarioMdl dtoDatos = (AdmUsuarioMdl)oDatos;
            iSecuencia = SecuenciaDML("SEC_SIT_USUARIO");

            String sContraseñaMD5 = EncriptarUtil.ObtenerMD5Hash(dtoDatos.ku_contraseña);

            string sqlQuery = " insert into SIT_ADM_USUARIO ( KU_CLAUSU, KU_NOMBRE, KU_PATERNO, KU_MATERNO, KU_PUESTO, KU_FECBAJA, "
                    + " KU_CONTRASEÑA, ka_claarea_origen, KU_CORREO, KU_EXTENSION, KU_INTENTOS, KU_BLOQUEAR_FIN, "
                    + " KU_TITULO, KU_DESIGNACION, KU_FECMOD, KU_AUXCORREO ) VALUES ( "
                    + " :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15 )";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.ku_nombre, dtoDatos.ku_paterno, dtoDatos.ku_materno, dtoDatos.ku_puesto, dtoDatos.ku_fecbaja,
                sContraseñaMD5, dtoDatos.ka_claarea_origen, dtoDatos.ku_correo, dtoDatos.ku_extension, dtoDatos.ku_intentos, dtoDatos.ku_bloquear_fin,
                dtoDatos.ku_titulo, dtoDatos.ku_designacion, dtoDatos.ku_fecmod, dtoDatos.ku_auxcorreo);

        }
        private Object dmlUpdate(Object oDatos)
        {
            AdmUsuarioMdl dtoDatos = (AdmUsuarioMdl)oDatos;
            string sqlQuery = " update SIT_ADM_USUARIO "
                    + " set KU_NOMBRE = :P0, KU_PATERNO = :P1, KU_MATERNO = :P2, KU_PUESTO = :P3, "
                    + " ka_claarea_origen = :P4 ,  KU_CORREO = :P5, KU_FECBAJA = :P6, KU_EXTENSION = :P7, "
                    + " KU_INTENTOS = :P8 , KU_BLOQUEAR_FIN= :P9, KU_TITULO = :P10, KU_DESIGNACION = :P11, " 
                    + " KU_FECMOD = :P12, KU_AUXCORREO = :P13 "
                    + " WHERE KU_CLAUSU = :P14 ";

            return EjecutaDML(sqlQuery,
                    dtoDatos.ku_nombre, dtoDatos.ku_paterno, dtoDatos.ku_materno, dtoDatos.ku_puesto, dtoDatos.ka_claarea_origen, 
                    dtoDatos.ku_correo,  dtoDatos.ku_fecbaja, dtoDatos.ku_extension, dtoDatos.ku_intentos, dtoDatos.ku_bloquear_fin, 
                    dtoDatos.ku_titulo, dtoDatos.ku_designacion, dtoDatos.ku_fecmod, dtoDatos.ku_auxcorreo, dtoDatos.ku_clausu);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmUsuarioMdl dtoDatos = (AdmUsuarioMdl)oDatos;
            string sqlQuery = " delete from SIT_ADM_USUARIO where KU_CLAUSU = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.ku_clausu);
        }

        private Object dmlHabilitar(Object oDatos)
        {
            AdmUsuarioMdl dtoDatos = (AdmUsuarioMdl)oDatos;
            string sqlQuery = " update SIT_ADM_USUARIO set KU_FECBAJA = null where KU_CLAUSU = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.ku_clausu);
        }

        private Object dmlDeshabilitar(Object oDatos)
        {
            AdmUsuarioMdl dtoDatos = (AdmUsuarioMdl)oDatos;
            string sqlQuery = " update SIT_ADM_USUARIO set KU_FECBAJA = sysdate where KU_CLAUSU = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.ku_clausu);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmUsuarioMdl> lstDatos = (List<AdmUsuarioMdl>)oDatos;

            string sqlQuery = " insert into SIT_ADM_USUARIO ( KU_CLAUSU, KU_NOMBRE, KU_PATERNO, KU_MATERNO, KU_PUESTO, KU_FECBAJA, "
                    + " KU_CONTRASEÑA, ka_claarea_origen, KU_CORREO, KU_EXTENSION, KU_INTENTOS, KU_BLOQUEAR_FIN, "
                    + " KU_TITULO, KU_DESIGNACION, KU_FECMOD, KU_AUXCORREO ) VALUES ( "
                    + " :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15 )";

            foreach (AdmUsuarioMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.ku_clausu, dtoDatos.ku_nombre, dtoDatos.ku_paterno, dtoDatos.ku_materno, dtoDatos.ku_puesto, dtoDatos.ku_fecbaja,
                                dtoDatos.ku_contraseña, dtoDatos.ka_claarea_origen, dtoDatos.ku_correo, dtoDatos.ku_extension, dtoDatos.ku_intentos, dtoDatos.ku_bloquear_fin,
                                dtoDatos.ku_titulo, dtoDatos.ku_designacion, dtoDatos.ku_fecmod, dtoDatos.ku_auxcorreo);
                iContador++;
            }
            return iContador;
        }


        private Object dmlUpdContraseña(Object oDatos)
        {
            AdmUsuarioMdl dtoDatos = (AdmUsuarioMdl)oDatos;

            String sContraseñaMD5 = EncriptarUtil.ObtenerMD5Hash(dtoDatos.ku_contraseña);

            string sqlQuery = " UPDATE SIT_ADM_USUARIO SET KU_CONTRASEÑA = :P0 WHERE KU_CLAUSU = :P1 ";
            return EjecutaDML(sqlQuery, sContraseñaMD5, dtoDatos.ku_clausu);
        }
       
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        B U S Q U E D A S
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////       
        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT KU_CLAUSU, KU_NOMBRE, KU_PATERNO, KU_MATERNO, KU_PUESTO, KU_CONTRASEÑA,  "
                + " ka_claarea_origen,  KU_CORREO, KU_EXTENSION, a.KA_CLAAREA,  a.KA_SIGLA, KA_DESCRIPCION, KU_FECBAJA,  "
                + " KU_INTENTOS, KU_BLOQUEAR_FIN, KU_TITULO, KU_DESIGNACION, KU_FECMOD, KU_AUXCORREO "
                + " from SIT_ADM_USUARIO u, SIT_ADM_KAREA a "
                + " where u.KA_CLAAREA_ORIGEN = a.KA_CLAAREA "
                + " order by  KU_PATERNO, KU_MATERNO, KU_NOMBRE "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            string sqlQuery = " Select KU_CLAUSU AS id , KU_CORREO as text FROM SIT_ADM_USUARIO where KU_FECBAJA IS NULL ORDER BY KU_CORREO";

            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select KU_CLAUSU, KU_CORREO FROM SIT_ADM_USUARIO where KU_FECBAJA IS NULL ORDER BY KU_CORREO";
            dtDatos = (DataTable)ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["KU_CLAUSU"]), row["KU_CORREO"].ToString());
            }

            return dicParametros;
        }

        private AdmUsuarioMdl dmlSelectValidarUsuario(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;

            string sqlQuery = " SELECT KU_CLAUSU, KU_NOMBRE, KU_PATERNO, KU_MATERNO, KU_PUESTO, KU_FECBAJA, "
                    + " KU_CONTRASEÑA, ka_claarea_origen, KU_CORREO, KU_EXTENSION, KU_INTENTOS, KU_BLOQUEAR_FIN, "
                    + " KU_TITULO, KU_DESIGNACION, KU_FECMOD, KU_AUXCORREO "
                    + " FROM SIT_ADM_USUARIO WHERE KU_CORREO = :P0 and KU_CONTRASEÑA = :P1 AND KU_FECBAJA is null " 
                    + " ORDER KU_PATERNO, KU_MATERNO, KU_NOMBRE";

            List<AdmUsuarioMdl> lstAdmUsuMdl = (List<AdmUsuarioMdl>)CrearListaMDL(
                    ConsultaDML(sqlQuery, dicParametros[COL_KU_CORREO].ToString(), dicParametros[COL_KU_CONTRASEÑA].ToString()));

            return lstAdmUsuMdl[0];
        }

        private AdmUsuarioMdl dmlSelectValidarCorreo(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;

            //Aqui cambiamos el valor a MDD
            String sContraseñaMD5 = EncriptarUtil.ObtenerMD5Hash(dicParametros[COL_KU_CONTRASEÑA].ToString());

            
            string sqlQuery = "SELECT KU_CLAUSU, KU_NOMBRE, KU_PATERNO, KU_MATERNO, KU_PUESTO, KU_FECBAJA, "
                    + " KU_CONTRASEÑA, ka_claarea_origen, KU_CORREO, KU_EXTENSION, KU_INTENTOS, KU_BLOQUEAR_FIN, "
                    + " KU_TITULO, KU_DESIGNACION, KU_FECMOD, KU_AUXCORREO "
                    + " FROM SIT_ADM_USUARIO WHERE KU_CORREO = :P0 and KU_CONTRASEÑA = :P1 AND KU_FECBAJA is null";

            List<AdmUsuarioMdl> lstAdmUsuMdl = (List<AdmUsuarioMdl>) CrearListaMDL(
                    ConsultaDML(sqlQuery, dicParametros[COL_KU_CORREO].ToString(), sContraseñaMD5));


            if (lstAdmUsuMdl.Count > 0)
                return lstAdmUsuMdl[0];
            else
                return null;

        }

        private DataTable dmlSelectGridPerfil(Object oDatos)
        {
            string sqlQuery = " SELECT KU_CLAUSU, KU_CORREO, KU_NOMBRE, KU_PATERNO, KU_MATERNO, a.KA_SIGLA  "
                + " from SIT_ADM_USUARIO u, SIT_ADM_KAREA a "
                + " where u.KA_CLAAREA_ORIGEN = a.KA_CLAAREA "
                + " order by KU_CORREO ";
            return ConsultaDML(sqlQuery);
        }


        
        private AdmUsuarioMdl dmlSelExisteUsu(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;

            string sqlQuery = " SELECT KU_CLAUSU, KU_NOMBRE, KU_PATERNO, KU_MATERNO, KU_PUESTO, KU_FECBAJA, "
                    + " KU_CONTRASEÑA, ka_claarea_origen, KU_CORREO, KU_EXTENSION, KU_INTENTOS, KU_BLOQUEAR_FIN, "
                    + " KU_TITULO, KU_DESIGNACION, KU_FECMOD, KU_AUXCORREO "
                    + " FROM SIT_ADM_USUARIO WHERE KU_CORREO = :P0 ";

            List<AdmUsuarioMdl> lstAdmUsuMdl = (List<AdmUsuarioMdl>)CrearListaMDL(
                    ConsultaDML(sqlQuery, dicParam[COL_KU_CORREO] ));

            if (lstAdmUsuMdl.Count > 0)
                return lstAdmUsuMdl[0];
            else
                return null;
        }



        protected override object CrearListaMDL(DataTable dtDatos)
        {
            List<AdmUsuarioMdl> lstAdmUsuMdl = new List<AdmUsuarioMdl>();

            foreach (DataRow row in dtDatos.Rows)
            {
                AdmUsuarioMdl usuMdl = new AdmUsuarioMdl();

                usuMdl.ku_clausu = Convert.ToInt32(row["KU_CLAUSU"]);
                usuMdl.ku_nombre = row["KU_NOMBRE"].ToString();
                usuMdl.ku_paterno = row["KU_PATERNO"].ToString();
                usuMdl.ku_materno = row["KU_MATERNO"].ToString();
                usuMdl.ku_puesto = row["KU_PUESTO"].ToString();

                if (row["KU_FECBAJA"].ToString() != "")
                    usuMdl.ku_fecbaja = Convert.ToDateTime(row["KU_FECBAJA"]);
                else
                    usuMdl.ku_fecbaja = new DateTime();

                usuMdl.ku_contraseña = row["KU_CONTRASEÑA"].ToString();
                usuMdl.ku_correo = row["KU_CORREO"].ToString();
                usuMdl.ku_extension = row["KU_EXTENSION"].ToString();
                usuMdl.ka_claarea_origen = Convert.ToInt32(row["ka_claarea_origen"]);
                usuMdl.ku_intentos = Convert.ToInt32(row["KU_INTENTOS"]);

                if (row["KU_BLOQUEAR_FIN"].ToString() != "")
                    usuMdl.ku_bloquear_fin = Convert.ToDateTime(row["KU_BLOQUEAR_FIN"]);
                else
                    usuMdl.ku_bloquear_fin = new DateTime();

                usuMdl.ku_titulo = row["KU_TITULO"].ToString();
                usuMdl.ku_designacion = row["KU_DESIGNACION"].ToString();

                if (row["KU_FECMOD"].ToString() != "")
                    usuMdl.ku_fecmod = Convert.ToDateTime(row["KU_FECMOD"]);
                else
                    usuMdl.ku_fecmod = new DateTime();

                usuMdl.ku_auxcorreo = row["KU_AUXCORREO"].ToString();

                lstAdmUsuMdl.Add(usuMdl);
            }

            return lstAdmUsuMdl;
        }
    }
}