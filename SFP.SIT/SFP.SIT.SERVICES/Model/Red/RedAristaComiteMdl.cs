using System;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedAristaComiteMdl
    {
        public Int64 us_clafolio { get; set; }
        public Int64 nre_claarista { get; set; }
        public string com_motivo { get; set; }
        public int rbc_clacomiterubro { get; set; }

        public RedAristaComiteMdl() { }
        public RedAristaComiteMdl( 
            Int64 us_clafolio,  Int64 nre_claarista,    string com_motivo,  int rbc_clacomiterubro)
        {
            this.us_clafolio = us_clafolio;
            this.nre_claarista = nre_claarista;
            this.com_motivo = com_motivo;
            this.rbc_clacomiterubro = rbc_clacomiterubro;           
        }
    }
}
