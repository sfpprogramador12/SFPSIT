using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmAreaMdl 
    {
        public Int32 ka_claarea { get; set; }
        public String ka_descripcion { get; set; }
        public String ka_clave { get; set; }
        public String ka_sigla { get; set; }
        public Int32 ka_reporta { get; set; }
        public Int32 ka_orden { get; set; }
        public Int32 ka_nivel { get; set; }
        public Int32 kta_clatipo_area { get; set; }              
        public DateTime ka_fecini { get; set; }
        public DateTime ka_fecfin { get; set; }
        public String ka_ubicacion { get; set; }
        public Int32 kp_claperfil { get; set; }

        public AdmAreaMdl() { }

        public AdmAreaMdl(
            Int32 ka_claarea,   String ka_descripcion,  String ka_clave,        String  ka_sigla,
            Int32 ka_reporta,   Int32 ka_orden,         Int32 ka_nivel,         Int32   kta_clatipo_area,
            DateTime ka_fecini, DateTime ka_fecfin,     String ka_ubicacion,    Int32   kp_claperfil)
        {
            this.ka_claarea = ka_claarea;
            this.ka_descripcion = ka_descripcion;
            this.ka_clave = ka_clave;
            this.ka_sigla = ka_sigla;
            this.ka_reporta = ka_reporta;
            this.ka_orden = ka_orden;
            this.ka_nivel = ka_nivel;
            this.kta_clatipo_area = kta_clatipo_area;

            this.ka_fecini = ka_fecini;
            this.ka_fecfin = ka_fecfin;
            this.ka_ubicacion = ka_ubicacion;
            this.kp_claperfil = kp_claperfil;
        }
    }
}
