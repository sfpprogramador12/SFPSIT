using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmModuloMdl
    {
        public Int32    km_clamodulo { get; set; }
        public Int32    km_clamodulo_padre { get; set; }
        public Int32    km_consecutivo { get; set; }
        public String   km_descripcion { get; set; }
        public String   km_control { get; set; }
        public String   km_metodo { get; set; }
        public DateTime km_fecbaja { get; set; }

        public AdmModuloMdl() { }
        public AdmModuloMdl( Int32 km_clamodulo, Int32 km_clamodulo_padre, Int32 km_consecutivo, String km_descripcion, String km_control, String km_metodo, DateTime km_fecbaja )
        {
            this.km_clamodulo = km_clamodulo;
            this.km_clamodulo_padre = km_clamodulo_padre;
            this.km_consecutivo = km_consecutivo;
            this.km_descripcion = km_descripcion;
            this.km_control = km_control;
            this.km_metodo = km_metodo;
            this.km_fecbaja = km_fecbaja;
        }
    }
}
