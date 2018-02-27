using Microsoft.AspNetCore.Http;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.WEB.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace SFP.SIT.WEB.Services
{
    public class DocumentoSer
    {
        public enum EnumDocPersistencia
        {
            FileSystem,
            SharePoint,
        };

        private EnumDocPersistencia _iTipoPersistencia;
        private CfgSharePointMdl _CfgSharePointMdl;
        public Dictionary<string, SIT_DOC_EXTENSION> dicExtension { get; set; }


        public DocumentoSer(CfgSharePointMdl sSharePoint)
        {
            if (sSharePoint != null)
            {
                _CfgSharePointMdl = (CfgSharePointMdl)sSharePoint;
                _iTipoPersistencia = EnumDocPersistencia.SharePoint;
            }
            else
            {
                _CfgSharePointMdl = null;
                _iTipoPersistencia = EnumDocPersistencia.FileSystem;
            }
        }


        public void guardar(SIT_DOC_DOCUMENTO docMdl, byte[] docBytes)
        {

            if (_iTipoPersistencia == EnumDocPersistencia.FileSystem)
            {
                GuardarSistArchivos(docMdl, docBytes);
            }
            else
            {
                GuardarSharePoint(docMdl, docBytes);
            }

        }
        private void GuardarSistArchivos(SIT_DOC_DOCUMENTO docMdl, byte[] docBytes)
        {

        }


        private void GuardarSharePoint(SIT_DOC_DOCUMENTO docMdl, byte[] docBytes)
        {

        }


        //////////public void GestionArchivo(ref AfdEdoDataMdl afdDataMdl, String sRutaArchivo, String sDocOficio, String sNombreArchivo, int iArchivoTamaño, )
        //////////{
        //////////    if (afdDataMdl.AFDlstdocs == null)
        //////////        afdDataMdl.AFDlstdocs = new List<SIT_DOC_DOCUMENTO>();

        //////////    SIT_DOC_DOCUMENTO docMdl = null;
        //////////    DateTime dtOFicioFecha;

        //////////    String sExtension;
        //////////    int iTipoExtension = 0;
        //////////    int iIdxExtension = sNombreArchivo.LastIndexOf('.');

        //////////    if (iIdxExtension > 0)
        //////////    {
        //////////        sExtension = sNombreArchivo.Substring(iIdxExtension + 1).ToUpper();
        //////////        if (dicExtension.ContainsKey(sExtension))
        //////////            iTipoExtension = (dicExtension[sExtension]).extclave;
        //////////    }
        //////////    else
        //////////        iTipoExtension = 0;

        //////////    dtOFicioFecha = DateTime.Now;
        //////////    docMdl = new SIT_DOC_DOCUMENTO(Constantes.General.ID_PENDIENTE, dtOFicioFecha, sDocOficio, sNombreArchivo, iArchivoTamaño, sRutaArchivo, iTipoExtension, DateTime.MinValue, null);
        //////////    afdDataMdl.AFDlstdocs.Add(docMdl);
        //////////    afdDataMdl.AFDdocArista = new DocAristaMdl(afdDataMdl.ID_folio, Constantes.General.ID_PENDIENTE, Constantes.General.ID_PENDIENTE);

        //////////    afdDataMdl.RutaFolio = CrearRuta(afdDataMdl.solicitud.solfecsol.Year, afdDataMdl.solicitud.solfecsol.Month,
        //////////        afdDataMdl.solicitud.solfecsol.Day, afdDataMdl.solicitud.solclave, afdDataMdl.RutaServidor);

        //////////}

        public string CrearRuta(Int32 iAño, Int32 iMes, Int32 iDia, Int64 iFolio, String sSistemaRuta)
        {
            string sCarpeta = "";
            try
            {
                if (!Directory.Exists(sSistemaRuta + "\\" + iAño))
                    Directory.CreateDirectory(sSistemaRuta + "\\" + iAño);

                if (!Directory.Exists(sSistemaRuta + "\\" + iAño + "\\" + iMes))
                    Directory.CreateDirectory(sSistemaRuta + "\\" + iAño + "\\" + iMes);

                if (!Directory.Exists(sSistemaRuta + "\\" + iAño + "\\" + iMes + "\\" + iDia))
                    Directory.CreateDirectory(sSistemaRuta + "\\" + iAño + "\\" + iMes + "\\" + iDia);

                sCarpeta = sSistemaRuta + "\\" + iAño + "\\" + iMes + "\\" + iDia + "\\" + iFolio;
                if (!Directory.Exists(sCarpeta))
                    Directory.CreateDirectory(sCarpeta);
            }
            catch (Exception excep)
            {
                Console.Write("Error en crear la subcarpetas de la solicitud" + excep.Message);
            }
            return sCarpeta;
        }

        public Boolean GrabarArchivoWeb(Int32 iAño, Int32 iMes, Int32 iDia, Int64 iFolio, String sSistemaRuta, MemoryStream msArchivo)
        {

            /// agegar la ruta que se envia...........
            ///String sRuta = sSistemaRuta + Constantes.WebCarpetas.ARCHIVO;

            Boolean bExiste = true;
            try
            {
                if (CrearRuta(iAño, iMes, iDia, iFolio, sSistemaRuta) != "")
                {
                    // mover el archivo....
                    // grabar arhicvo..
                }

            }
            catch
            {
                bExiste = false;

            }

            return bExiste;
        }

        public DocContenidoMdl AnexarArchivo(IFormFile oArchivo, String sRootPath)
        {
            if (oArchivo == null)
                return null;

            string sExtension = oArchivo.FileName.Substring(oArchivo.FileName.LastIndexOf(".") + 1).ToUpper();
            SIT_DOC_EXTENSION docTipoMdl;
            int iFileExt = 0;

            if (dicExtension.ContainsKey(sExtension) == true)
            {
                docTipoMdl = dicExtension[sExtension];
                iFileExt = docTipoMdl.extclave;
            }

            DocContenidoMdl docContenidoMdl = new DocContenidoMdl(0, DateTime.Now, "", oArchivo.FileName, oArchivo.Length,
                        sRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO, iFileExt, DateTime.Now, null, null);


            if (oArchivo.Length > 0)
            {
                using (var fileStream = oArchivo.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    docContenidoMdl.doc_contenido = ms.ToArray();
                }
            }
            return docContenidoMdl;
        }



        public void BorrarArchivo(String sRutaTemp)
        {
            if (sRutaTemp != null)
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(sRutaTemp);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                di.Delete();
            }
        }

        public void BorrarCarpetaContenido(String sRutaTemp)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(sRutaTemp);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                di.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
