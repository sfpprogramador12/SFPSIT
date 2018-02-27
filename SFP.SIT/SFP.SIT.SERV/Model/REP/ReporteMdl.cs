using System;

namespace SFP.SIT.SERV.Model.REP
{
    public class ReporteMdl
    {
        public Int64 solclave { get; set; }
        public DateTime segfecini { get; set; }
        public String eto_descripcion { get; set; }
        public String sotdescripcion { get; set; }
        public String rtpdescripcion { get; set; }
        public Int32 segdiassemaforo { get; set; }
        public Int32 segsemaforocolor { get; set; }
        public String areas { get; set; }
        public String soldes { get; set; }
        public String soldat { get; set; }

        public ReporteMdl() { }
    }
}
