using System;
using System.Collections.Generic;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedGanttMdl
    {
        public int id { get; set; }
        public int nod_origen { get; set; }
        public string origen { get; set; }
        public string name { get; set; }
        public int nod_destino { get; set; }
        public string destino { get; set; }
        public Int64 start { get; set; }
        public Int64 end { get; set; }
        public int duration { get; set; }
        public string nre_observacion { get; set; }
        public string responsable { get; set; }
        public string code { get; set; }
        public int level { get; set; }
        public string status { get; set; }
        public Boolean canWrite { get; set; }
        public Boolean startIsMilestone { get; set; }
        public Boolean endIsMilestone { get; set; }
        public Boolean collapsed { get; set; }
        public List<string> assigs { get; set; }
        public string depends { get; set; }
        public Boolean hasChild { get; set; }

        public RedGanttMdl() { }
    }
}
