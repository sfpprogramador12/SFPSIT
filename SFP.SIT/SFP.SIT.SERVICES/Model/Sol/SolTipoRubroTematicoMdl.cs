using System;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolTipoRubroTematicoMdl
    {
        public int krt_clatema { get; set; }
        public String krt_rubro { get; set; }
        public String krt_plazo_reserva { get; set; }
        public String krt_fundamento_legal { get; set; }
        public DateTime krt_fecbaja { get; set; }

        public SolTipoRubroTematicoMdl() { }
        public SolTipoRubroTematicoMdl(int krt_clatema, String krt_rubro, String krt_plazo_reserva, String krt_fundamento_legal, DateTime krt_fecbaja)
        {
            this.krt_clatema = krt_clatema;
            this.krt_rubro = krt_rubro;
            this.krt_plazo_reserva = krt_plazo_reserva;
            this.krt_fundamento_legal = krt_fundamento_legal;
            this.krt_fecbaja = krt_fecbaja;
        }
    }
}
