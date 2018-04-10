using SFP.Persistencia;
using SFP.Persistencia.Model;
using SFP.Persistencia.Util;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using SFP.SIT.WEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SFP.SIT.WEB.Services
{
    public class SeguridadSer : BaseFunc
    {
        /////// private readonly ILogger _logger;
        public const string PARAM_IP = "IP";
        public const string PARAM_ARCHIVO_MENSAJE = "ARCHIVO_MENSAJE";
        public const string PARAM_CFGCORREO = "CFGCORREO";

        public SeguridadSer(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
        }

        public UsuarioViewModel ValidarUsuario(Dictionary<string, object> dicParam)
        {
            UsuarioViewModel UsrMdl = new UsuarioViewModel();

            /* BUSCAR LOS DATOS DEL USUARIO POR MEDIO DEL CORREO*/
            SIT_ADM_USUARIODao admUsuDao = new SIT_ADM_USUARIODao(_cn, _transaction, _sDataAdapter);
            SIT_ADM_USUARIO admUsrMdl = admUsuDao.dmlSelectValidarCorreo(dicParam) as SIT_ADM_USUARIO;
            if (admUsrMdl != null)
            {                                
                UsrMdl.AdmUsuMdl = admUsrMdl;
                dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, admUsrMdl.usrclave);
                UsrMdl.lstAreas = new SIT_ADM_USUARIOAREADao(_cn, _transaction, _sDataAdapter).dmlUsuarioArea(dicParam) as List<SIT_ADM_AREAHIST>;
                UsrMdl.lstPerfil = new SIT_ADM_USRPERFILDao(_cn, _transaction, _sDataAdapter).dmlSelectPerfilxUsr(admUsrMdl.usrclave) as List<SIT_ADM_PERFIL>;
                UsrMdl.lstModulo = new SIT_ADM_PERFILMODDao(_cn, _transaction, _sDataAdapter).dmlSelectModxUsr(admUsrMdl.usrclave) as List<SIT_ADM_MODULO>;


                string sComboPerfil = "";
                foreach (SIT_ADM_PERFIL admPerfil in UsrMdl.lstPerfil)
                {
                    if (admPerfil.perclave > Constantes.Perfil.SISTEMAS)
                    {
                        if (admPerfil.perclave == Constantes.Perfil.UA)
                        {
                            foreach (SIT_ADM_AREAHIST admAreas in UsrMdl.lstAreas)
                            {
                                sComboPerfil = sComboPerfil + "<option value=\"A" + admAreas.araclave + "\"> UA - " + admAreas.arhsiglas + "<option> \r";
                            }
                        }
                        else
                        {
                            sComboPerfil = sComboPerfil + "<option value=\"P" + admPerfil.perclave + "\"> " + admPerfil.perdescripcion + "<option> \r";
                        }

                    }
                }
                UsrMdl.sCboPerfilArea = sComboPerfil.Trim();


            }
            return UsrMdl;
        }


        public UsuarioViewModel EncontrarUsuario(Dictionary<string, object> dicParam)
        {
            UsuarioViewModel UsrMdl = new UsuarioViewModel();

            /* BUSCAR LOS DATOS DEL USUARIO POR MEDIO DEL CORREO*/
            SIT_ADM_USUARIODao admUsuDao = new SIT_ADM_USUARIODao(_cn, _transaction, _sDataAdapter);
            SIT_ADM_USUARIO admUsrMdl = admUsuDao.dmlSelectEncontrarUsuario(dicParam) as SIT_ADM_USUARIO;
            if (admUsrMdl != null)
            {
                UsrMdl.AdmUsuMdl = admUsrMdl;
                //dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, admUsrMdl.usrclave);
                UsrMdl.lstAreas = new SIT_ADM_USUARIOAREADao(_cn, _transaction, _sDataAdapter).dmlUsuarioArea(dicParam) as List<SIT_ADM_AREAHIST>;
                UsrMdl.lstPerfil = new SIT_ADM_USRPERFILDao(_cn, _transaction, _sDataAdapter).dmlSelectPerfilxUsr(admUsrMdl.usrclave) as List<SIT_ADM_PERFIL>;
                UsrMdl.lstModulo = new SIT_ADM_PERFILMODDao(_cn, _transaction, _sDataAdapter).dmlSelectModxUsr(admUsrMdl.usrclave) as List<SIT_ADM_MODULO>;


                string sComboPerfil = "";
                foreach (SIT_ADM_PERFIL admPerfil in UsrMdl.lstPerfil)
                {
                    if (admPerfil.perclave > Constantes.Perfil.SISTEMAS)
                    {
                        if (admPerfil.perclave == Constantes.Perfil.UA)
                        {
                            foreach (SIT_ADM_AREAHIST admAreas in UsrMdl.lstAreas)
                            {
                                sComboPerfil = sComboPerfil + "<option value=\"A" + admAreas.araclave + "\"> UA - " + admAreas.arhsiglas + "<option> \r";
                            }
                        }
                        else
                        {
                            sComboPerfil = sComboPerfil + "<option value=\"P" + admPerfil.perclave + "\"> " + admPerfil.perdescripcion + "<option> \r";
                        }

                    }
                }
                UsrMdl.sCboPerfilArea = sComboPerfil.Trim();


            }
            return UsrMdl;
        }


        public List<UsuarioViewModel> EncontrarUsuarios(Dictionary<string, object> dicParam)
        {
            UsuarioViewModel UsrMdl = new UsuarioViewModel();
            SIT_ADM_USUARIODao admUsuDao = new SIT_ADM_USUARIODao(_cn, _transaction, _sDataAdapter);
            List<SIT_ADM_USUARIO> admUsrMdl = admUsuDao.dmlSelectEncontrarUsuariosActivos(dicParam) as List<SIT_ADM_USUARIO>;
            List<UsuarioViewModel> respuesta = new List<UsuarioViewModel>();
            if (admUsrMdl != null)
            {
                foreach (SIT_ADM_USUARIO admPerfil in admUsrMdl)
                {
                    respuesta.Add(new UsuarioViewModel() {
                        AdmUsuMdl = admPerfil
                    });   
                }

            }
            return respuesta;
        }

        /*encuentra los mensajes salientes y entrantes de un usuario*/
        public UsuarioViewModel MensajesUsuario(Dictionary<string, object> dicParam)
        {
            UsuarioViewModel UsrMdl = new UsuarioViewModel();
            SIT_ADM_USUARIODao admUsuDao = new SIT_ADM_USUARIODao(_cn, _transaction, _sDataAdapter);
            SIT_ADM_USUARIO admUsrMdl = admUsuDao.dmlSelectUsuarioConversations(dicParam) as SIT_ADM_USUARIO;
            UsuarioViewModel respuesta = new UsuarioViewModel();
            if (admUsrMdl != null)
            {
                    respuesta = (new UsuarioViewModel()
                    {
                        AdmUsuMdl = admUsrMdl
                    });
            }
            return respuesta;
        }

        public Object CambiarContraseña(SIT_ADM_USUARIO admUsr)
        {            
            return  (int)new SIT_ADM_USUARIODao(_cn, _transaction, _sDataAdapter).dmlUpdContraseña(admUsr) ; 
        }

        /*
         *Obtiene el id y el nombre de los usuarios que se pueden personificar      
        */
        public Dictionary<int, string> GetUsuariosCompartidos(string userClave)
        {
            Dictionary<int, string> users = new Dictionary<int, string>();

            foreach (SIT_ADM_USUARIO actual in new SIT_ADM_USUARIODao(_cn, _transaction, _sDataAdapter).dmlGetSharedUsers(userClave)) {
                users.Add(actual.usrclave, actual.usrnombre + " " + actual.usrpaterno + " " + actual.usrmaterno);
            }

            return users;
        }

        

        public Object OlvidarContraseña(Object oDatos)
        {
            //dicParam.Add(SeguridadSer.PARAM_CORREO, oUsuCtrVM.correo);
            //dicParam.Add(SeguridadSer.PARAM_ARCHIVO_MENSAJE, sMensaje);
            //dicParam.Add(SeguridadSer.PARAM_CFGCORREO, _memCache.ObtenerDato(CacheWebSIT.APP_CORREO_CONFIG) as CfgCorreoMdl);

            Dictionary<string, object> dicParamIn = oDatos as Dictionary<string, object>;

            String sMensaje = null;
            AdmCorreoNeg admCorreo = null;
            CfgCorreoMdl oCfgCorreo = null;
           
            SIT_ADM_USUARIODao admUsuDao = new SIT_ADM_USUARIODao(_cn, _transaction, _sDataAdapter);
            SIT_ADM_USUARIO admUsuMdl = admUsuDao.dmlSelectExisteUsr(dicParamIn[DButil.SIT_ADM_USUARIO_COL.USRCORREO].ToString()) as SIT_ADM_USUARIO;

            if (admUsuMdl != null)
            {
                admUsuMdl.usrcontraseña = EncriptarUtil.CrearContraseña(10);
                sMensaje = dicParamIn[SeguridadSer.PARAM_ARCHIVO_MENSAJE].ToString().Replace("|contraseña|", admUsuMdl.usrcontraseña);
                Int32 iRes = (Int32)admUsuDao.dmlUpdContraseña(admUsuMdl);

                oCfgCorreo = dicParamIn[PARAM_CFGCORREO] as CfgCorreoMdl;
                admCorreo = new AdmCorreoNeg(oCfgCorreo);
                return admCorreo.Enviar(admUsuMdl.usrcorreo, "SIT - Olvidó de contraseña", sMensaje);
            }
            return false;
        }
    }
}
