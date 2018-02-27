using System;
using System.Collections.Generic;

namespace SFP.SIT.SERVICES.Model.Doc
{
    public class FrmImportarMdl
    {
        public string ArchivoZip { get; set; }
        public string RutaCarpeta { get; set; }
        public string RutaServidor { get; set; }
        public int iVersion { get; set; }
        // Si esn ecesario pasarlo a un diccionario si se quiere guardar el archivo..
        public Dictionary<string, DocContenidoMdl> dicArchivoDatos { get; set; }
        public Dictionary<long, Object> dicRegistros { get; set; }
    }

    public class FrmImportarUImdl
    {
        public int iVersion { get; set; }
        public List<string> lstArchivo { get; set; }
        public List<Object> lstRegistros { get; set; }
    }

    public class UploadFilesResult
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
    }
}
