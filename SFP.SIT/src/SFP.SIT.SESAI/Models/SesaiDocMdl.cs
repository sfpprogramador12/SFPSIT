using System;

namespace SFP.SIT.SESAI.Models
{
    public class SesaiDocMdl
    {
        public String name { get; set; }
        public String mime_type { get; set; }
        public Int32 doctamaño { get; set; }
        public String doc_charset { get; set; }
        public DateTime last_update { get; set; }
        public String content_type { get; set; }
        public String content { get; set; }
        public byte[] blob_content { get; set; }
        public String no_folio { get; set; }
        public Int64 id_turnada { get; set; }
        public String tipo { get; set; }
        public String extencion { get; set; }
    }
}
