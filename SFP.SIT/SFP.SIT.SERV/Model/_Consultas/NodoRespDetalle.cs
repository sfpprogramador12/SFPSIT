using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Model._Consultas
{
    public class NodoRespDetalle
    {

        public int UsrClave { set; get; }
        public string UsrNombre { set; get; }
        public string UsrPaterno { set; get; }
        public string UsrMaterno { set; get; }
        public string ArhDescripcion { set; get; }
        public Int64 NodOrigen { set; get; }
        public int RtpClave { get; set; }
        public DateTime RepEdoFec { get; set; }
        public string RepOficio { get; set; }
        public Int64 DocClave { get; set; }
        public int RepCantidad { get; set; }
        public string RtpDescripcion { get; set; }
        public string RtpForma { get; set; }
        public Int64 Repclave { set; get; }
        public int SdoClave { get; set; }
        public Int64 Nodclave { get; set; }

        public NodoRespDetalle() { }

        public NodoRespDetalle(int UsrClave, string UsrNombre, string UsrPaterno , string UsrMaterno, string ArhDescripcion, Int64 NodOrigen, int RtpClave, 
            DateTime RepEdoFec, string RepOficio, Int64 DocClave, int RepCantidad, string RtpDescripcion, string RtpForma, Int64 Repclave, int SdoClave, Int64 nodclave)
        {
            this.UsrClave = UsrClave;
            this.UsrNombre = UsrNombre;
            this.UsrPaterno = UsrPaterno;
            this.UsrMaterno = UsrMaterno;
            this.ArhDescripcion = ArhDescripcion;
            this.NodOrigen = NodOrigen;

            this.RtpClave = RtpClave;
            this.RepEdoFec = RepEdoFec;
            this.RepOficio = RepOficio;
            this.DocClave = DocClave;
            this.RepCantidad = RepCantidad;
            this.RtpDescripcion = RtpDescripcion;
            this.RtpForma = RtpForma;
            this.Repclave = Repclave;

            this.SdoClave = SdoClave;
            this.Nodclave = nodclave;
        }

    }
}
