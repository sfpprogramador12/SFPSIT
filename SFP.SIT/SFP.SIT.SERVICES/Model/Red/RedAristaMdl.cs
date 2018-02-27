using System;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedAristaMdl
    {
        public Int64 nre_claarista { get; set; }
        public Int64 us_clafolio { get; set; }
        public Int64 nod_origen { get; set; }
        public Int64 nod_destino { get; set; }
        public int ku_clausu { get; set; }
        public DateTime nre_fecini { get; set; }
        public DateTime nre_fecfin { get; set; }
        public int nre_dias_laborales { get; set; }
        public int nre_dias_naturales { get; set; }
        public int kar_clatipoari { get; set; }
        public DateTime nre_feclectura { get; set; }
        public string nre_observacion { get; set; }
        public int nre_hito { get; set; }
        public int us_modent { get; set; }

        public int nre_feclecusu { get; set; }

        public RedAristaMdl() { }
        public RedAristaMdl(
            Int64 nre_claarista,    Int64 us_clafolio,      Int64 nod_origen,   Int64 nod_destino,
            int ku_clausu,          DateTime nre_fecini,    DateTime nre_fecfin,string nre_observacion,
            int nre_dias_laborales, int nre_dias_naturales, int kar_clatipoari, DateTime nre_feclectura,
            int nre_hito,           int us_modent,          int nre_feclecusu)
        {            
            this.nre_claarista = nre_claarista;
            this.us_clafolio = us_clafolio;
            this.nod_origen = nod_origen;
            this.nod_destino = nod_destino;
            this.ku_clausu = ku_clausu;
            this.nre_fecini = nre_fecini;
            this.nre_fecfin = nre_fecfin;
            this.nre_dias_laborales = nre_dias_laborales;
            this.nre_dias_naturales = nre_dias_naturales;
            this.kar_clatipoari = kar_clatipoari;
            this.nre_feclectura = nre_feclectura;
            this.nre_observacion = nre_observacion;
            this.nre_hito = nre_hito;
            this.us_modent = us_modent;
            this.nre_feclecusu = nre_feclecusu;
        }

        public RedAristaMdl(
            Int64 nre_claarista, Int64 us_clafolio, int nre_hito)
        {
            this.nre_claarista = nre_claarista;
            this.us_clafolio = us_clafolio;
            this.nre_hito = nre_hito;
        }

        public RedAristaMdl(
            Int64 nre_claarista, Int64 us_clafolio, DateTime nre_feclectura, int nre_feclecusu)
        {
            this.nre_claarista = nre_claarista;
            this.us_clafolio = us_clafolio;
            this.nre_feclectura = nre_feclectura;
            this.nre_feclecusu = nre_feclecusu;
        }
    }
}
