using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedAristaRrevisionMdl
    {
        public Int64 nre_claarista { get; set; }
        public Int64 us_clafolio { get; set; }
        public string rev_expediente { get; set; }
        public string rev_responsable { get; set; }
        public string rev_correo { get; set; }

        public RedAristaRrevisionMdl() { }
        public RedAristaRrevisionMdl(
            Int64 nre_claarista,    Int64 us_clafolio, 
            string rev_expediente, string rev_responsable, string rev_correo)
        {
            this.nre_claarista = nre_claarista;
            this.us_clafolio = us_clafolio;
            this.rev_expediente = rev_expediente;
            this.rev_responsable = rev_responsable;
            this.rev_correo = rev_correo;
        }
    }
}
