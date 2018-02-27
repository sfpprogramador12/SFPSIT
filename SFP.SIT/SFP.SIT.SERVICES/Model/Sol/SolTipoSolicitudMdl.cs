using System;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolTipoSolicitudMdl
    {
        public int tso_clatiposol { get; set; }
        public String tso_descripcion { get; set; }

        public SolTipoSolicitudMdl() { }
        public SolTipoSolicitudMdl(int tso_clatiposol, String tso_descripcion)
        {
            this.tso_clatiposol = tso_clatiposol;
            this.tso_descripcion = tso_descripcion;
        }

    }
}
