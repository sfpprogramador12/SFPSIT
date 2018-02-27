using Microsoft.SharePoint.Client;
using SFP.SIT.SERVICES.Model.App;
using SFP.SIT.SERVICES.Model.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SFP.SIT.SERVICES.Negocio
{
    public class AdmDocContNeg
    {
        const int ERROR_GRABAR_SHAREPOINT = 1;
        const int ERROR_GRABAR_FILESYSTEM = 2;

        private DateTime _dtFolioCarpeta;
        private const int SPServerFileNotFoundExceptionErrorCode = -2147024894;

        SharePointMdl _spCxn;
        private DocContenidoMdl _docContMdl;

        public AdmDocContNeg(SharePointMdl shaPointMdl, DateTime dtFolioCarpeta)
        {
            _spCxn = shaPointMdl;
            _dtFolioCarpeta = dtFolioCarpeta;
        }

        public String Grabar(DocContenidoMdl docContMdl)
        {
            _docContMdl = docContMdl;
            String sRuta;
            ///// INVESTIGAR PARA MEJORAR 
            ///// String sRuta = SitSharePoint();
            // Primero intentamos grabar en SHAREPOINT
            //////if (sRuta == null)
            //////{
                sRuta = SitFileSystem();
                if (sRuta == null)
                    throw new System.ArgumentException("No es posible almacenar el archivo en el sistema ");
            //////}
            return sRuta;
        }


        public int  ActualizarDoc(Dictionary<string, DocContenidoMdl> dicDocContenido, string sFolio)
        {
            int iTotArchivos = 0;

            foreach (KeyValuePair<string, DocContenidoMdl> oDocContMdl in dicDocContenido)
            {                
                if (oDocContMdl.Key.IndexOf(sFolio) > 0 && oDocContMdl.Key.ToUpper().IndexOf(".TXT") == -1)
                {
                    iTotArchivos++;
                    Grabar(oDocContMdl.Value);
                }                
            }

            return iTotArchivos;
        }


        private String SitSharePoint()
        {
            //String sfolderPath = SharePointCrearRuta( _dtFolioCarpeta.Year, _dtFolioCarpeta.Month, _dtFolioCarpeta.Day);

            String sFolder = _spCxn.folder + "/" + _dtFolioCarpeta.Year.ToString();
            try
            {
                using (ClientContext context = new ClientContext(_spCxn.servidor))
                {
                    context.Credentials = new NetworkCredential(_spCxn.usuario, _spCxn.contraseña, _spCxn.dominio);


                    Folder barFolder = context.Web.GetFolderByServerRelativeUrl(_spCxn.folder);

                    string newFolderName = "mak" + DateTime.Now.ToString("yyyyMMddHHmm");
                    barFolder.Folders.Add(newFolderName);
                    barFolder.Update();

                    //////String fileToUpload = @"C:\temp\Examen 1.docx";
                    //////if (!System.IO.File.Exists(fileToUpload))
                    //////    throw new FileNotFoundException("File not found.", fileToUpload);

                    //////String fileName = System.IO.Path.GetFileName(fileToUpload);
                    //////FileStream fileStream = System.IO.File.OpenRead(fileToUpload);
                    //////FileCreationInformation fciDoc = new FileCreationInformation();


                    //////var folderPath = context.Web.GetFolderByServerRelativeUrl(sFolder);
                    //////////context.Load(folderPath);
                    //////context.Web.Context.ExecuteQuery();



                    //////var fciDocCont = new FileCreationInformation();
                    //////fciDocCont.Content = _docContMdl.doc_contenido;
                    //////fciDocCont.Overwrite = true;
                    //////fciDocCont.Url = folderPath.Name;

                    ////////folderPath.ParentFolder
                    //////folderPath.Files.Add(fciDocCont);
                    //////context.ExecuteQuery();
                    return newFolderName;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error :" + ex.Message);

            }
                return null;

        }
         
        private String SharePointCrearRuta(Int32 iAño, Int32 mes, Int32 dia)
        {
            try
            {
                String sFolder = _spCxn.folder + "/" + iAño.ToString();

                if (BuscarFolder(sFolder) == false)
                {
                    CrearFolder(_spCxn.folder, iAño.ToString());
                }

                if (BuscarFolder( sFolder + "/" + mes.ToString()) == false)
                {
                    CrearFolder(_spCxn.folder, mes.ToString());
                    sFolder += "/" + mes.ToString();
                }
                
                if (BuscarFolder( sFolder + "/" + dia.ToString()) == false)
                {
                    CrearFolder(_spCxn.folder, dia.ToString());
                }

                sFolder += "/" + dia.ToString();

                return sFolder;           
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                return null;
            }            
        }

        private Boolean CrearFolder(String sFolderPath, String sFolderNvo)
        {
            using (ClientContext context = new ClientContext(_spCxn.servidor))
            {
                context.Credentials = new NetworkCredential(_spCxn.usuario, _spCxn.contraseña, _spCxn.dominio);
                Folder folder = context.Web.GetFolderByServerRelativeUrl(sFolderPath);
                folder.Folders.Add(sFolderNvo);
                folder.Update();
            }
            return true;
        }

        private Boolean BuscarFolder( string folderRelativePath) 
        {            
            ClientContext context = new ClientContext(_spCxn.servidor);
            context.Credentials = new NetworkCredential(_spCxn.usuario, _spCxn.contraseña, _spCxn.dominio);

            Folder folder = context.Web.GetFolderByServerRelativeUrl(folderRelativePath);
            context.Web.Context.Load(folder);
            context.Web.Context.ExecuteQuery();

            return true;
        }

        private String SitFileSystem()
        {
            string sRutaNvo = FileSystemCrearRuta(_dtFolioCarpeta.Year, _dtFolioCarpeta.Month, _dtFolioCarpeta.Day);
            if (sRutaNvo != null)
            {
                System.IO.File.WriteAllBytes(_docContMdl.doc_ruta  +"\\" + sRutaNvo + "\\" + _docContMdl.doc_nombre, _docContMdl.doc_contenido);
            }
            return sRutaNvo;                
        }

        private String FileSystemCrearRuta(Int32 iAño, Int32 iMes, Int32 iDia)
        {
            
            String sDirNvo = _docContMdl.doc_ruta + "\\" + iAño;

            if (Directory.Exists(sDirNvo) == false)
                Directory.CreateDirectory(sDirNvo);

            sDirNvo += "\\" + iMes;
            if (Directory.Exists(sDirNvo) == false)
                Directory.CreateDirectory(sDirNvo);

            sDirNvo += "\\" + iDia;
            if (Directory.Exists(sDirNvo) == false)
                Directory.CreateDirectory(sDirNvo);

            String sDirRuta = "\\" + iAño + "\\" + iMes + "\\" + iDia;

            return sDirRuta;
        }        
    }
}
