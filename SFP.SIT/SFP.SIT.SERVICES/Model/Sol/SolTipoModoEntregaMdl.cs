using System;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolTipoModoEntregaMdl
    {
        public Int32 us_modent { get; set; }
        public String men_descripcion { get; set; }
        public Int32 men_mostrar { get; set; }

        public SolTipoModoEntregaMdl() { }
        public SolTipoModoEntregaMdl(Int32 us_modent, String men_descripcion, Int32 men_mostrar)
        {
            this.us_modent = us_modent;
            this.men_descripcion = men_descripcion;
            this.men_mostrar = men_mostrar;
        }
    }
}
