using System;


namespace SFP.SIT.WEB.Models
{
    public class FrmAfdDatosMdl
    {
        public Int64 folio { get; set; }
        public Int32 estado { get; set; }
        public Int32 prcID { get; set; }
        public string sfecini { get; set; }
        public DateTime fecini { get; set; }
        public Int32 solTipo { get; set; }
        public Int64 clanodo { get; set; }
        public int mvc { get; set; }
        public int flujo { get; set; }
        public string segFlujo { get; set; }
        public int perfil { get; set; }
        public int area { get; set; }   
        public int usuario { get; set; }

        public FrmAfdDatosMdl() { }
        public FrmAfdDatosMdl( Int64 Folio,    Int32 Estado,  Int32 prcID,
            DateTime Fecini,  Int32 SolTipo,  Int64 Nodo,
            int MVC,        int Flujo , int perfil, int area, int usuario)
        {
            this.folio = Folio;
            this.estado = Estado;
            this.prcID = prcID;
            this.fecini = Fecini;
            this.solTipo = SolTipo;
            this.clanodo = Nodo;
            this.mvc = MVC;
            this.flujo = Flujo;
            this.perfil = perfil;
            this.area = area;
            this.usuario = usuario;
        }
    }
}
