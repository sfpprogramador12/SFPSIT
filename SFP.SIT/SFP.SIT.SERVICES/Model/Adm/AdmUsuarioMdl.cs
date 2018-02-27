using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmUsuarioMdl
    {
        public int ku_clausu { get; set; }
        public string ku_nombre { get; set; }
        public string ku_paterno { get; set; }
        public string ku_materno { get; set; }
        public string ku_puesto { get; set; }
        public DateTime ku_fecbaja { get; set; }
        public string ku_contraseña { get; set; }
        public string ku_correo { get; set; }
        public string ku_extension { get; set; }
        public int ka_claarea_origen { get; set; }
        public int ku_intentos { get; set; }
        public DateTime ku_bloquear_fin { get; set; }
        public string ku_titulo { get; set; }
        public string ku_designacion { get; set; }
        public DateTime ku_fecmod { get; set; }
        public string ku_auxcorreo { get; set; }

       
        public AdmUsuarioMdl() { }
        public AdmUsuarioMdl(
            int ku_clausu, string ku_nombre, string ku_paterno, string ku_materno, string ku_puesto,
            string ku_contraseña, int ka_claarea_origen, string ku_extension, string ku_correo, DateTime ku_fecbaja, 
            int ku_intentos, DateTime ku_bloquear_fin, 
            string ku_titulo, string ku_designacion, DateTime ku_fecmod, string ku_auxcorreo )
        {
            this.ku_clausu = ku_clausu;
            this.ku_nombre = ku_nombre;
            this.ku_paterno = ku_paterno;
            this.ku_materno = ku_materno;
            this.ku_puesto = ku_puesto;
            this.ku_contraseña = ku_contraseña;
            this.ka_claarea_origen = ka_claarea_origen;
            this.ku_extension = ku_extension;
            this.ku_correo = ku_correo;
            this.ku_fecbaja = ku_fecbaja;
            this.ku_intentos = ku_intentos;
            this.ku_bloquear_fin = ku_bloquear_fin;

            this.ku_titulo = ku_titulo;
            this.ku_designacion = ku_designacion;
            this.ku_fecmod = ku_fecmod;
            this.ku_auxcorreo = ku_auxcorreo;
        }
    }
}
