using System;

namespace SFP.SIT.SERVICES.Model.Snt
{
    public class SntSolicitanteMdl
    {
        public Int64 US_CLAUSU { get; set; }
        public String US_RFC { get; set; }
        public String US_APEPAT { get; set; }
        public String US_APEMAT { get; set; }
        public String US_NOMBRE { get; set; }
        public String US_CURP { get; set; }
        public String US_CALLE { get; set; }
        public String US_NUMEXT { get; set; }
        public String US_NUMINT { get; set; }
        public String US_COL { get; set; }
        public String US_CODPOS { get; set; }
        public String US_TEL { get; set; }
        public String US_CORELE { get; set; }
        public String US_EDOEXT { get; set; }
        public String US_CIUDADEXT { get; set; }
        public String US_SEXO { get; set; }
        public DateTime US_FECNAC { get; set; }
        public String US_USUARIO { get; set; }
        public int KPA_CLAPAI { get; set; }
        public int KE_CLAEST { get; set; }
        public int KMU_CLAMUN { get; set; }
        public String US_REPLEG { get; set; }
        public String NIVEDU_CLAVE { get; set; }
        public String US_OTRAOCUPACION { get; set; }
        public String US_OTRONIVELEDUC { get; set; }
        public int TSL_CLATIPOSOLTE{ get; set; }
        public int US_OCUPACION { get; set; }

        public SntSolicitanteMdl() { }
        public SntSolicitanteMdl(
            Int64 US_CLAUSU, string US_RFC, string US_APEPAT, string US_APEMAT,
            string US_NOMBRE, string US_CURP, string US_CALLE, string US_NUMEXT,
            string US_NUMINT, string US_COL, string US_CODPOS, string US_TEL,
            string US_CORELE, string US_EDOEXT, string US_CIUDADEXT, string US_SEXO,
            DateTime US_FECNAC, string US_USUARIO, int KPA_CLAPAI, int KE_CLAEST,
            int KMU_CLAMUN, string US_REPLEG, string NIVEDU_CLAVE, string US_OTRAOCUPACION,
            string US_OTRONIVELEDUC, int TSL_CLATIPOSOLTE, int US_OCUPACION)                                         
        {
            this.US_CLAUSU = US_CLAUSU;
            this.US_RFC = US_RFC;
            this.US_APEPAT = US_APEPAT;
            this.US_APEMAT = US_APEMAT;
            this.US_NOMBRE = US_NOMBRE;
            this.US_CURP = US_CURP;
            this.US_CALLE = US_CALLE;
            this.US_NUMEXT = US_NUMEXT;
            this.US_NUMINT = US_NUMINT;
            this.US_COL = US_COL;
            this.US_CODPOS = US_CODPOS;
            this.US_TEL = US_TEL;
            this.US_CORELE = US_CORELE;
            this.US_EDOEXT = US_EDOEXT;
            this.US_CIUDADEXT = US_CIUDADEXT;
            this.US_SEXO = US_SEXO;
            this.US_FECNAC = US_FECNAC;
            this.US_USUARIO = US_USUARIO;
            this.KPA_CLAPAI = KPA_CLAPAI;
            this.KE_CLAEST = KE_CLAEST;
            this.KMU_CLAMUN = KMU_CLAMUN;
            this.US_REPLEG = US_REPLEG;
            this.NIVEDU_CLAVE = NIVEDU_CLAVE;
            this.US_OTRAOCUPACION = US_OTRAOCUPACION;
            this.US_OTRONIVELEDUC = US_OTRONIVELEDUC;
            this.TSL_CLATIPOSOLTE = TSL_CLATIPOSOLTE;
            this.US_OCUPACION = US_OCUPACION;
        }
    }
}
