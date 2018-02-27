using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmUsuAreaMdl
    {
        public Int32 ku_clausu { get; set; }
        public Int32 ka_claarea { get; set; }

        public AdmUsuAreaMdl() { }
        public AdmUsuAreaMdl(Int32 ku_clausu, Int32 ka_claarea)
        {
            this.ku_clausu = ku_clausu;            
            this.ka_claarea = ka_claarea;
        }
    }
}
