using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Imp
{
    public class ImpInfomexArchivoAclMdl
    {
        public Int64 us_folio { get; set; }
        public int clarespuesta { get; set; }
        public DateTime fecha_respuesta { get; set; }
        public int clatipo_info { get; set; }
        public String descripcion { get; set; }
        public String archivo_respuesta { get; set; }
        public Int64 archivo_tamaño { get; set; }

        public string res_descripcion { get; set; }
        public string tpi_informacion { get; set; }

        public string Mensaje { get; set; }
        public int Error { get; set; }

        public ImpInfomexArchivoAclMdl() { }
    }
}
