using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SESAI.Models
{
    public class RedAristaRevFormaMdl
    {
        public Int64 solclave { get; set; }
        public Int64 ariclave { get; set; }
        public string csc_resuelto { get; set; }
        public DateTime csc_fecha { get; set; }
        public string csc_conf_rev { get; set; }
        public string csc_observacion { get; set; }

        public RedAristaRevFormaMdl(Int64 solclave, Int64 ariclave, string csc_resuelto,
            DateTime csc_fecha, string csc_conf_rev, string csc_observacion)
        {
            this.solclave = solclave;
            this.ariclave = ariclave;
            this.csc_resuelto = csc_resuelto;
            this.csc_fecha = csc_fecha;
            this.csc_conf_rev = csc_conf_rev;
            this.csc_observacion = csc_observacion;
        }
    }
}
