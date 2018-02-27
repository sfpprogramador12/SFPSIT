using System;

namespace SFP.SIT.SERVICES.Model.Doc
{
    public class DocumentoMdl
    {
        public Int32 doc_cladoc { get; set; }
        public DateTime doc_fecha { get; set; }
        public String doc_folio { get; set; }
        public String doc_nombre { get; set; }
        public Int64 doc_size { get; set; }
        public String doc_ruta { get; set; }
        public Int32 kte_claext { get; set; }
        public DateTime doc_filesystem { get; set; }
        public String doc_url { get; set; }

        public String doc_MD5 { get; set; }

        public DocumentoMdl() { }
        public DocumentoMdl (   Int32 doc_cladoc,   DateTime doc_fecha, String doc_folio,           String doc_nombre,  
            Int64 doc_size,     String doc_ruta,    Int32 kte_claext,   DateTime doc_filesystem,    String doc_url, String doc_MD5)
        {        
            this.doc_cladoc = doc_cladoc;
            this.doc_fecha = doc_fecha;
            this.doc_folio = doc_folio;
            this.doc_nombre = doc_nombre;
            this.doc_size = doc_size;            
            this.doc_ruta = doc_ruta;
            this.kte_claext = kte_claext;
            this.doc_filesystem = doc_filesystem;
            this.doc_url = doc_url;
            this.doc_MD5 = doc_MD5;
        }
    }
}
