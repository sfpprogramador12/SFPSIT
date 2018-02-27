using System;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedAristaResolucionMdl
    {
        public Int64 us_clafolio { get; set; }
        public Int64 nre_claarista { get; set; }
        public int tpi_clatipo_info { get; set; }
        public string rsl_ubicacion { get; set; }
        public string rsl_tam_cant_dir { get; set; }  
        public string rsl_art7 { get; set; }

        public RedAristaResolucionMdl() { }
        public RedAristaResolucionMdl ( Int64 us_clafolio, Int64 nre_claarista, int tpi_clatipo_info, 
            string rsl_ubicacion, string rsl_tam_cant_dir,  string rsl_art7 )
        {
            this.us_clafolio = us_clafolio;
            this.nre_claarista = nre_claarista;
            this.rsl_art7 = rsl_art7;
            this.tpi_clatipo_info = tpi_clatipo_info;
            this.rsl_ubicacion = rsl_ubicacion;
            this.rsl_tam_cant_dir = rsl_tam_cant_dir;  
        }
    }
}
