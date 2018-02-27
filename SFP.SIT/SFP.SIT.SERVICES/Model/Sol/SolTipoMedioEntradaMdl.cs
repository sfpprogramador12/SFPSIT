using System;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolTipoMedioEntradaMdl
    {
        public Int32 idmedioentrada { get; set; }
        public String met_descripcion { get; set; }
        public DateTime met_fecbaja { get; set; }

        public SolTipoMedioEntradaMdl() { }
    }
}
