using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{ 
    public class RespViewModel 
    {
        public Int64 folio { set; get; }
        public Int64 nodo  { set; get; }
        public int tipoArista { set; get; }
        public string lstArt7 { set; get; }
        public int tipoInfo { set; get; }
        public string respuesta { set; get; }
        public string FechaArchivo { set; get; }
        public string Oficio { set; get; }
        public IFormFile ArchivoResolucion { set; get; }
    }
}
