using System;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolUEnlaceMdl
    {
        public Int32 us_unienl { get; set; }
        public String enl_descripcion { get; set; }
        public DateTime enl_fecbaja { get; set; }


        public SolUEnlaceMdl() { }
        public SolUEnlaceMdl(Int32 us_unienl,  String enl_descripcion)
        {
            this.us_unienl = us_unienl;
            this.enl_descripcion = enl_descripcion;
            this.enl_fecbaja =  DateTime.MinValue;
        }
    }
}
