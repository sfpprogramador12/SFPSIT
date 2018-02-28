using SFP.SIT.SERV.Model.ADM;
using System.Collections.Generic;

namespace SFP.SIT.WEB.Models
{
    public class UsuarioViewModel
    {
        public SIT_ADM_USUARIO AdmUsuMdl { get; set; }
        public string UsuarioNombre {
            get {
                if (AdmUsuMdl == null)
                    return "";
                else
                    return AdmUsuMdl.usrnombre + " " + AdmUsuMdl.usrpaterno + " " + AdmUsuMdl.usrmaterno;
            }
        }

        public List<SIT_ADM_AREAHIST> lstAreas { get; set; }
        public List<SIT_ADM_PERFIL> lstPerfil { get; set; }
        public List<SIT_ADM_MODULO> lstModulo { get; set; }
        public string sCboPerfilArea { get; set; }
        public Dictionary<int, string> usuarioBase { get; set; }
        public Dictionary<int, string> usuariosCompartidos{ get; set; }
    }
}