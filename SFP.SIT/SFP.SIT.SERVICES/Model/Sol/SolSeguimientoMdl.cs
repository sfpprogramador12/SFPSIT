using System;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolSeguimientoMdl
    {
        public Int64 us_clafolio { get; set; }
        public Int32 krp_claproceso { get; set; }
        public Int32 seg_multiple { get; set; }
        public DateTime seg_fecini { get; set; }
        public DateTime seg_fecfin { get; set; }
        public DateTime seg_fecampliacion { get; set; }
        public Int32 seg_diassemaforo { get; set; }        
        public Int32 seg_diasnolaborales { get; set; }
        public Int32 seg_colorsemaforo { get; set; }                
        public DateTime seg_feccalculo { get; set; }
        public Int32 kar_clatipoari { get; set; }    // esto tiene su equivalente ocm id_respuesta
        public Int32 seg_edoproceso { get; set; }
        public Int32 afd_claafd { get; set; }
        public Int32 seg_resp_exterior { get; set; }
        public Int64 nre_claarista { get; set; }
        public Int64 seg_ultimonodo { get; set; }
        public DateTime seg_fecestimada { get; set; }

        public SolSeguimientoMdl() { }
        public SolSeguimientoMdl(
            Int64 us_clafolio,          Int32 krp_claproceso,       DateTime seg_fecini,            DateTime seg_fecfin,        
            DateTime seg_fecampliacion, Int32 seg_diassemaforo,     Int32 seg_colorsemaforo,        Int32 seg_multiple,         
            Int32 seg_diasnolaborales,  DateTime seg_feccalculo,    Int32 kar_clatipoari ,          Int32 seg_edoproceso,
            Int32 afd_claafd,           Int32 seg_resp_exterior,    Int64 nre_claarista,            Int64 seg_ultimonodo,
            DateTime seg_fecestimada ) {
            this.us_clafolio = us_clafolio;
            this.krp_claproceso = krp_claproceso;
            this.seg_fecini = seg_fecini;
            this.seg_fecfin = seg_fecfin;
            this.seg_fecampliacion = seg_fecampliacion;
            this.seg_diassemaforo = seg_diassemaforo;
            this.seg_colorsemaforo = seg_colorsemaforo;
            this.seg_multiple = seg_multiple;
            this.seg_diasnolaborales = seg_diasnolaborales;
            this.seg_feccalculo = seg_feccalculo;
            this.kar_clatipoari = kar_clatipoari;
            this.seg_edoproceso = seg_edoproceso;
            this.afd_claafd = afd_claafd;
            this.seg_resp_exterior = seg_resp_exterior;
            this.nre_claarista = nre_claarista;
            this.seg_ultimonodo = seg_ultimonodo;
            this.seg_fecestimada = seg_fecestimada;
        }
    }
}
