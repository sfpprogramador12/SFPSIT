using System;

namespace SFP.SIT.SERV.Model.DOC
{
    public class DocImportarSolicitudMdl
    {
        public DocImportarSolicitudMdl() { }
        public Int32 nodclave { get; set; }
        public long solclave { get; set; }
        public Int32 imp_status { get; set; }
        public Int32 zip_claarchivo { get; set; }
    }
}
