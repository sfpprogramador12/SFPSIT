using System;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedComiteRubroMdl
    {
        public int rbc_clacomiterubro { get; set; }
        public string rbc_descripcion { get; set; }
        public DateTime rbc_fecbaja { get; set; }

        public RedComiteRubroMdl() { }
        public RedComiteRubroMdl(int rbc_clacomiterubro, string rbc_descripcion, DateTime rbc_fecbaja)
        {
            this.rbc_clacomiterubro = rbc_clacomiterubro;
            this.rbc_descripcion = rbc_descripcion;
            this.rbc_fecbaja = rbc_fecbaja;
        }
    }
}
