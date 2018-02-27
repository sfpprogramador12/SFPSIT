using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Doc
{
    public class DocContenidoMdl : DocumentoMdl
    {
        public Byte[] doc_contenido { get; set;  }
        public String kte_mime_type { get; set;  }

        public DocContenidoMdl(Byte[] doc_contenido, String kte_mime_type, String doc_nombre)
        {
            this.doc_contenido = doc_contenido;
            this.kte_mime_type = kte_mime_type;
            this.doc_nombre = doc_nombre;
        }

        public DocContenidoMdl(Int32 doc_cladoc, DateTime doc_fecha, String doc_folio, String doc_nombre, Int64 doc_size,
            String doc_ruta, Int32 kte_claext, DateTime doc_filesystem, String doc_url, String doc_MD5) : 
            base(doc_cladoc, doc_fecha,  doc_folio,  doc_nombre,doc_size,  doc_ruta,  kte_claext,  doc_filesystem,  doc_url,  doc_MD5)
        {

        }
    }
}
