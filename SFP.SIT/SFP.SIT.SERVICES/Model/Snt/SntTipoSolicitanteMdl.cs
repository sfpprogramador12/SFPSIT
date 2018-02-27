using System;

namespace SFP.SIT.SERVICES.Model.Snt
{
    public class SntTipoSolicitanteMdl
    {
        public Int32 tsl_clatiposolte { get; set; }
        public String tsl_descripcion { get; set; }

        public SntTipoSolicitanteMdl() { }
        public SntTipoSolicitanteMdl(Int32 tsl_clatiposolte, String tsl_descripcion)
        {
            this.tsl_clatiposolte = tsl_clatiposolte;
            this.tsl_descripcion = tsl_descripcion;
        }
    }
}
