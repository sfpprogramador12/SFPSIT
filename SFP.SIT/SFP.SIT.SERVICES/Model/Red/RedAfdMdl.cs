using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedAfdMdl
    {
        public int afd_claAfd { get; set; }
        public string afd_descripcion { get; set; }
        public DateTime afd_fecbaja { get; set; }
        public string afd_prefijo { get; set; }

        public RedAfdMdl() { }
        public RedAfdMdl( int claAfd,  string descripcion, DateTime fecbaja, string afd_prefijo)
        {
            this.afd_claAfd = claAfd;
            this.afd_descripcion = descripcion;
            this.afd_fecbaja = fecbaja;
            this.afd_prefijo = afd_prefijo;
        }   
    }
}
