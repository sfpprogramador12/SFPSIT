using System;

namespace SFP.SIT.SERVICES.Model.Doc
{
    public class DocAristaMdl
    {
        public long us_clafolio { get; set; }
        public Int64 nre_claarista { get; set; }
        public Int32 doc_cladoc { get; set; }

        public DocAristaMdl() { }
        public DocAristaMdl( long us_clafolio, Int64 nre_claarista, Int32 doc_cladoc )
        {
            this.us_clafolio = us_clafolio;
            this.nre_claarista = nre_claarista;
            this.doc_cladoc = doc_cladoc;
        }
    }
}
