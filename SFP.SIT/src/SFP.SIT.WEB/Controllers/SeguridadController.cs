using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Util;
using SFP.SIT.WEB.Injection;
using SFP.SIT.WEB.Services;
using SFP.SIT.WEB.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace SFP.SIT.WEB.Controllers
{
    public class SeguridadController : SitBaseCtlr
    {
        SolicitudSer _solServ;
        public SeguridadController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<SeguridadController> logger, IHostingEnvironment app) 
            : base(memCache, httpContextAccessor, logger, app)
        {            
            _solServ = new SolicitudSer(_sitDmlDbSer);
            _iUsuario = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
        }

        [HttpGet]
        public string InicializarPerfil()
        {            
            Response.ContentType = "application/json; charset=UTF-8";
            string sJsonUsuarios = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_USUARIODao>(nameof(SIT_ADM_USUARIODao.dmlSelectGridPerfil),  null));
            return "[" + sJsonUsuarios + "]";
        }

        [HttpGet]
        public string UsuarioPerfil(string ClaUsuario)
        {
            Response.ContentType = "application/json; charset=UTF-8";
            string sJsonArea = JsonTransform.convertJson((DataTable) _sitDmlDbSer.operEjecutar<SIT_ADM_USUARIOAREADao>(nameof(SIT_ADM_USUARIOAREADao.dmlUPAarbol), ClaUsuario));
            return "[" + sJsonArea + "]";

        }

        [HttpPost]
        public string UsuarioPerfil(string ClaUsuario, string param)
        {
            String sMensaje; 

            Response.ContentType = "application/json; charset=UTF-8";
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, Convert.ToInt32(ClaUsuario));
            dicParam.Add(SIT_ADM_USUARIOAREADao.PARAM_AREAS, param);

            int iRegistros = (int)_sitDmlDbSer.operEjecutar<SIT_ADM_USUARIOAREADao>(nameof(SIT_ADM_USUARIOAREADao.dmlActualizarPerfil), dicParam);
            if (iRegistros > 0)
                sMensaje = "Datos actualizados";
            else
                sMensaje = "La actualización no fue realizada";

            return sMensaje;
        }

        private object SIT_ADM_USUAREADao()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Perfil()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // B  I  T  A  C  O  R  A
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public ActionResult Bitacora()
        {
            DataTable tlArchivo = new DataTable();
            tlArchivo.Columns.Add("recid", typeof(int));
            tlArchivo.Columns.Add("archivo", typeof(string));

            int iOrden = 1;
            
            string[] fileEntries = Directory.GetFiles(_app.ContentRootPath + "\\Logs");
            foreach (string fileName in fileEntries)
            {
                tlArchivo.Rows.Add(iOrden, Path.GetFileName(fileName));
                iOrden++;
            }
            @ViewBag.lstArchivos = JsonTransform.convertJson(tlArchivo);
            return View();
        }

        [HttpGet]
        public FileResult ArchivoLog(String Archivo)
        {
            FileResult frLog = null;
            
            if (Archivo == "" || Archivo == "undefined")
            {
                ///logger.Error(new WebAppLog(Request.UserHostAddress, UsrMdl.usuNombre, ConstantesWeb.LOG_ARCHIVO, "Get ArchivoLog", 0, "EL ARCHIVO A LEER NO EXISTE", Archivo));
            }                
            else
            {
                DocContenidoMdl frmDocMdl = _solServ.ObtenerDocumentoNombre(_app.ContentRootPath + "\\Logs\\" + Archivo);
                if (frmDocMdl != null)
                {
                    frLog = File(frmDocMdl.doc_contenido, frmDocMdl.extmimetype, frmDocMdl.docnombre);
                    ////if (frLog != null)
                    ////    logger.Info(new WebAppLog(Request.UserHostAddress, UsrMdl.usuNombre, ConstantesWeb.LOG_ARCHIVO, "Get ArchivoLog", 0, "LECTURA DEL ARCHIVO CORRECTA", frmDocMdl.Nombre));
                    ////else
                    ////    logger.Error(new WebAppLog(Request.UserHostAddress, UsrMdl.usuNombre, ConstantesWeb.LOG_ARCHIVO, "Get ArchivoLog", 0, "ERROR EN LA LECTURA DEL ARCHIVO", frmDocMdl.Nombre));
                }                    
                //else
                //    logger.Error(new WebAppLog(Request.UserHostAddress, UsrMdl.usuNombre, ConstantesWeb.LOG_ARCHIVO, "Get ArchivoLog", 0, "EL ARCHIVO DE LECTURA NO EXISTE", frmDocMdl.Nombre));
            }

            //Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            //Response.Cache.SetValidUntilExpires(false);
            //Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();
            Response.ContentType = "application/octet-stream; charset=UTF-8";
            //Response.ContentEncoding = "base64";

            return frLog;

        }
    }
}