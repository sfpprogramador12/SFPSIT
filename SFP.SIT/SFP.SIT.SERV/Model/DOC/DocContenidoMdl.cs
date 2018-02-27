using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Model.DOC
{
    public class DocContenidoMdl :SIT_DOC_DOCUMENTO
    {
        public Byte[] doc_contenido { get; set; }
        public String extmimetype { get; set; }

        public DocContenidoMdl()
        {
        }

        public DocContenidoMdl(Byte[] doc_contenido, String extmimetype, String docnombre)
        {
            this.doc_contenido = doc_contenido;
            this.extmimetype = extmimetype;
            this.docnombre = docnombre;
        }

        public DocContenidoMdl(Int32 docclave, DateTime docfecha, String docfolio, String docnombre, Int64 doctamaño,
            String docruta, Int32 extclave, DateTime docfilesystem, String docurl, String docmd5) 
        {
            this.docmd5 = docmd5;
	 	 	 this.docurl = docurl;
	 	 	 this.docfilesystem = docfilesystem;
	 	 	 this.docclave = docclave;
	 	 	 this.extclave = extclave;
	 	 	 this.docnombre = docnombre;
	 	 	 this.docfolio = docfolio;
	 	 	 this.docruta = docruta;
	 	 	 this.doctamaño = doctamaño;
	 	 	 this.docfecha = docfecha;
        }
    }
}