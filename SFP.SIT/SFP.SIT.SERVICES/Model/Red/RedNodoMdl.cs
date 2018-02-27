using System;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedNodoMdl
    {
        public Int64 us_clafolio { get; set; }
        public Int64 nod_clanodo { get; set; }
        public DateTime nod_feccreacion { get; set; }
        public int ka_claarea { get; set; }
        public int kne_clanodo_edo { get; set; }
        public int krp_claproceso  { get; set; }
        public int nod_tip { get; set; }
        public int nod_til { get; set; }
        public int nod_ttp { get; set; }
        public int nod_ttl { get; set; }
        public int nod_holgura { get; set; }
        public int nod_atendido { get; set; }

        public int nod_capa { get; set; }

        public RedNodoMdl() { }
        public RedNodoMdl(
            Int64 us_clafolio,  Int64 nod_clanodo,  DateTime nod_feccreacion,   int ka_claarea,     
            int kne_clanodo_edo,int krp_claproceso,     int nod_tip,                int nod_til,        
            int nod_ttp,        int nod_ttl,        int nod_holgura,        int nod_atendido,   int nod_capa
        ) {

            this.us_clafolio = us_clafolio;
            this.nod_clanodo = nod_clanodo;
            this.nod_feccreacion = nod_feccreacion;
            this.ka_claarea = ka_claarea;
            this.kne_clanodo_edo = kne_clanodo_edo;
            this.krp_claproceso = krp_claproceso;
            this.nod_tip = nod_tip;
            this.nod_til = nod_til;
            this.nod_ttp = nod_ttp;
            this.nod_ttl = nod_ttl;
            this.nod_holgura = nod_holgura;
            this.nod_atendido = nod_atendido;
            this.nod_capa = nod_capa;            
        }

        public RedNodoMdl(Int64 us_clafolio, Int64 nod_clanodo, int nod_atendido)
        {
            this.us_clafolio = us_clafolio;
            this.nod_clanodo = nod_clanodo;
            this.nod_atendido = nod_atendido;
        }
    }
}
