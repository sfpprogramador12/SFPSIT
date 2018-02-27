using System;

namespace SFP.SIT.SERVICES.Model.Doc
{
    public class DocImportarSolicitudMdl
    {
        public DocImportarSolicitudMdl() { }
        public Int32 nod_clanodo { get; set; }
        public long us_clafolio { get; set; }
        public Int32 imp_status { get; set; }
        public Int32 zip_claarchivo { get; set; }
    }
}
