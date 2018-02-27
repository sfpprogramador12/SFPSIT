using System;

namespace SFP.SIT.SERVICES.Model.Doc
{
    public class DocTipoExtensionMdl
    {
        public Int32 kte_claext { get; set; }
        public String kte_extension { get; set; }
        public String kte_mime_type { get; set; }

        public DocTipoExtensionMdl() { }
        public DocTipoExtensionMdl(Int32 kte_claext, String kte_extension, String kte_mime_type)
        {
            this.kte_claext = kte_claext;
            this.kte_extension = kte_extension;
            this.kte_mime_type = kte_mime_type;            
        }
    }
}
