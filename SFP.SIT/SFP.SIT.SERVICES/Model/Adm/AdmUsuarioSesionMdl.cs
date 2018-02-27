using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    class AdmUsuarioSesionMdl
    {        
        public Int32 ku_clausu { get; set; }
        public String ku_nombre { get; set; }
        public String ku_paterno { get; set; }
        public String ku_materno { get; set; }
        public String ku_puesto { get; set; }
        public String ku_usuario { get; set; }
        public String ku_correo { get; set; }
        public String ku_extension { get; set; }

        public AdmUsuarioSesionMdl() { }
    }
}
