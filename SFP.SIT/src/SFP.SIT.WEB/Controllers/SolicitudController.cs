using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SFP.SIT.WEB.Injection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using SFP.SIT.WEB.Models;
using SFP.SIT.WEB.Services;
using SFP.SIT.WEB.Util;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Newtonsoft.Json;
using SFP.SIT.AFD.Core;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Dao.SOL;
using SFP.Persistencia.Model;
using System.Linq;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Dao.SNT;
using SFP.SIT.SERV.Dao.DOC;
using SFP.Persistencia.Util;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.AFD.Model;
using SFP.SIT.FLUJO;
using Newtonsoft.Json.Linq;
//using System.Web.Script.Serialization;
using SFP.SIT.SERV.Model.ADM;

namespace SFP.SIT.WEB.Controllers
{
    /*PASAR A OTROS ARCHIVOS*/
    public class NodeDataArray
    {
        public int id { get; set; }
        public string loc { get; set; }
        public string text { get; set; }
        public string fecha { get; set; }
        public string Perfil { get; set; }
    }

    public class LinkDataArray
    {
        public int from { get; set; }
        public int to { get; set; }
        public List<double> points { get; set; }
        public string text { get; set; }
    }

    public class RootObject
    {
        public string @class { get; set; }
        public string nodeKeyProperty { get; set; }
        public List<NodeDataArray> nodeDataArray { get; set; }
        public List<LinkDataArray> linkDataArray { get; set; }
    }

    public class SolicitudController : SitBaseCtlr
    {
        SolicitudSer _solServ;
        public SolicitudController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<SolicitudController> logger, IHostingEnvironment app)
            : base(memCache, httpContextAccessor, logger, app)
        {
            _iUsuario = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
            _solServ = new SolicitudSer(_sitDmlDbSer);
        }

        /////* ********************************************
        ////       BANDEJA DE ENTRADA
        ////******************************************** */
        [HttpGet]
        public IActionResult BandejaEntrada()
        {
            ViewBag.Usuario = @User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value;

            return View();
            //return View("BandejaEntrada", "_WebmastLayout");

        }

        [HttpPost]
        public IActionResult BandejaEntrada([FromForm] FrmAfdDatosMdl datosSolVM)
        {
            datosSolVM.fecini = FechaUtil.AsignarFecha(datosSolVM.sfecini);
            if (datosSolVM.mvc == ConstantesWeb.FLUJO.AMPLIACION)
            {
                // ASGINAMOS EL ESTADO INICIAL 
                datosSolVM.estado = Constantes.NodoEstado.UT_RECIBIR_SOLICITUD;

                // BUSCAR EL NODO DE INICIAL DE LA UNIDAD DE TRANSPARENCIA PARA GENERAR UNA AMPLIACION
                SIT_RED_NODO redNodo = _solServ.ObtenerNodoFolioEdoPrc(datosSolVM.folio, Constantes.NodoEstado.UT_RECIBIR_SOLICITUD, datosSolVM.prcID);
                datosSolVM.clanodo = redNodo.nodclave;
            }

            if (datosSolVM.area == 0)
                datosSolVM.area = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);

            string json = JsonConvert.SerializeObject(datosSolVM);

            if (datosSolVM.mvc == ConstantesWeb.FLUJO.RESPONDER || datosSolVM.mvc == ConstantesWeb.FLUJO.AMPLIACION)
            {
                Dictionary<Int32, AfdNodoFlujo> dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                string sForma = dicAfdFlujo[datosSolVM.estado].nedurl;

                return RedirectToAction(sForma, "Estado", new
                {
                    iPrc = datosSolVM.prcID,
                    lNodo = datosSolVM.clanodo,
                    iArea = datosSolVM.area,
                    iPerfil = datosSolVM.perfil
                });

                //new { folio = datosSolVM.folio, tipoPrcActual = datosSolVM.prcID,  nodo = datosSolVM.clanodo, nodoEdo = datosSolVM.estado });
                ///return RedirectToAction("NodoBase", "Flujo" + _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.FLUJO), new { DatosMdl = json });
            }
            else
            {
                return View("BandejaRuta");
            }
        }

        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Produces("application/json")]
        public String BandejaFlujo(String txtFolio)
        {
            Int64 lFolio = Convert.ToInt64(txtFolio);
            object[] aoDatos = _solServ.DatosSeguimiento(lFolio) as object[];

            Dictionary<Int64, RedAristaSegMdl> dicRedArista = (Dictionary<Int64, RedAristaSegMdl>)aoDatos[0];
            Dictionary<Int64, FrmSegNodoMdl> dicNodo = (Dictionary<Int64, FrmSegNodoMdl>)aoDatos[1];
            Int32[] aiDatos = (Int32[])aoDatos[2];

            string sDiagrama = JsonTransform.convertJsonAristaSeg(dicRedArista, dicNodo, aiDatos);
            return sDiagrama;
        }

        [HttpGet]
        public IActionResult Documento(Int32 id)
        {
            DocContenidoMdl docContMdl = _solServ.ObtenerDocumentoID(id, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);
            if (docContMdl != null)
                return File(docContMdl.doc_contenido, docContMdl.extmimetype, docContMdl.docnombre);
            else
            {
                return Content("<html><head></head><body></br><b>El archivo a desplegar no fue localizado, favor de comunicarlo al área de Tecnologías de la Información</b></body></html>");
            }
        }

        /* VALIDAR SI LA SESION SE ENCUENTRA INVALIDA */
        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Produces("application/json")]
        public String ConsultaActual(SolBuscarMdl frmBuscarMdl)
        {
            frmBuscarMdl.usrclave = Convert.ToInt32(User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);

            if (frmBuscarMdl.araClave > 0 && frmBuscarMdl.perClave == 0)
                frmBuscarMdl.perClave = Constantes.Perfil.UA;

            DataTable dtResultado = _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectSolicitudPendiente), frmBuscarMdl) as DataTable;
            String sDatos = JsonTransform.convertJson(dtResultado);
            return sDatos;
        }

        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Produces("application/json")]
        public String ConsultaSeguimiento(SolBuscarMdl frmBuscarMdl)
        {
            frmBuscarMdl.usrclave = Convert.ToInt32(User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);

            if (frmBuscarMdl.araClave > 0 && frmBuscarMdl.perClave == 0)
                frmBuscarMdl.perClave = Constantes.Perfil.UA;

            // Validar los valores NULLS
            if (frmBuscarMdl.FecIngresoIni == null)
                frmBuscarMdl.FecIngresoIni = DateTime.MinValue;

            if (frmBuscarMdl.FecIngresoFin == null)
                frmBuscarMdl.FecIngresoFin = DateTime.MinValue;

            if (frmBuscarMdl.FecConcIni == null)
                frmBuscarMdl.FecConcIni = DateTime.MinValue;

            if (frmBuscarMdl.FecConcFin == null)
                frmBuscarMdl.FecConcFin = DateTime.MinValue;

            if (frmBuscarMdl.FecRespIni == null)
                frmBuscarMdl.FecRespIni = DateTime.MinValue;

            if (frmBuscarMdl.FecRespFin == null)
                frmBuscarMdl.FecRespFin = DateTime.MinValue;

            return JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectSolicitudSeguimiento), frmBuscarMdl));
        }

        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Produces("application/json")]
        public String Solicitante(FrmAfdDatosMdl frmSol)
        {
            Int64 lFolio = Convert.ToInt64(frmSol.folio);
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE, lFolio);
            dicParam.Add(SIT_SOL_SOLICITUDDao.PARAM_FECHA, DateTime.Now);

            string sJsonSolicitante = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_SOLICITANTEDao>(nameof(SIT_SNT_SOLICITANTEDao.dmlSelectSolicitanteTranspuestoPorID), lFolio));
            string sJsonAreaFaltante = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectAreasFaltantes), dicParam));
            string sJsonDocumento = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectDocInicial), lFolio));

            // Actualizamos el NODO.
            _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlUpdateNodoLectura), lFolio);


            return "[" + sJsonSolicitante + "," + sJsonAreaFaltante + "," + sJsonDocumento + "]";
        }

        [HttpGet]
        public IActionResult SolicitudEditar()
        {
            return View();
        }

        [HttpPost]
        public string SolicitudEditar([FromBody] SIT_SOL_SOLICITUD SolMdl)
        {
            return _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlUpdateID), SolMdl).ToString();
        }

        [HttpGet]
        public IActionResult SolicitudBuscar()
        {
            SolBuscarMdl solBuscar = new SolBuscarMdl();

            List<ComboMdl> lstTemp = _memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_PROCESO) as List<ComboMdl>;
            var rtn = lstTemp.Select(x => new { Id = x.ID, Value = x.DESCRIP });
            ViewBag.lstPrcTipo = new SelectList(rtn, "Id", "Value");

            lstTemp = _memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_TIPO) as List<ComboMdl>;
            rtn = lstTemp.Select(x => new { Id = x.ID, Value = x.DESCRIP });
            ViewBag.lstSolTipo = new SelectList(rtn, "Id", "Value");

            lstTemp = _sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectCombo), null) as List<ComboMdl>;
            lstTemp.Insert(0, new ComboMdl("-1", ""));
            rtn = lstTemp.Select(x => new { Id = x.ID, Value = x.DESCRIP });
            ViewBag.lstAccion = new SelectList(rtn, "Id", "Value");

            lstTemp = _sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectComboFecActual), DateTime.Now) as List<ComboMdl>;
            lstTemp.Insert(0, new ComboMdl("-1", ""));
            rtn = lstTemp.Select(x => new { Id = x.ID, Value = x.DESCRIP });
            ViewBag.lstArea = new SelectList(rtn, "Id", "Value");

            return View(solBuscar);
        }




        /////* ********************************************
        ////       EDITOR DEFLUJO
        ////******************************************** */
        [HttpGet]
        public IActionResult FlujoEditor()
        {
            ViewBag.Usuario = @User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value;


            return View();
        }

        [HttpPost]
        public IActionResult FlujoEditor(string JSONFlujo)
        {
            FlujoEditor fe = new FLUJO.FlujoEditor();

            JObject json = JObject.Parse(JSONFlujo);

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //RootObject rt = jss.Deserialize<RootObject>(JSONFlujo);
            /*
            for (int i = 0; i < rt.nodeDataArray.Capacity; i++)
            {
                fe.createViews(rt.nodeDataArray[i].text);
            }
            */
            //fe.createClasses("Edonew.cshtml"); 

            return View();
        }


        /////* ********************************************
        ////       EDITOR DEFLUJO de HISTORIAL DE AREAS
        ////******************************************** */
        [HttpGet]
        public IActionResult AreasHistorial()
        {
            ViewBag.Usuario = @User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value;
            AreaHistorialViewModel _catViewMdl = new AreaHistorialViewModel();
            DateTime date = Convert.ToDateTime("01/01/2018");
            date = date.Date;
            _catViewMdl.lstAreaHist = JsonTransform.convertJson((List<SIT_ADM_AREAHIST>)_sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectComboFecActual), date));
            string sRes = _catViewMdl.lstAreaHist.ToString();
            ViewBag.Areas = sRes;
            return View();
        }


        [HttpPost]
        public string FlujoAreas(string fechaF)
        {
            string sRes = "";
            fechaF = fechaF.Replace('-', '/');
            AreaHistorialViewModel _catViewMdl = new AreaHistorialViewModel();
            DateTime date = Convert.ToDateTime(fechaF);
            date = date.Date;

            _catViewMdl.lstAreaHist = JsonTransform.convertJson((List<SIT_ADM_AREAHIST>)_sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectComboFecActual), date));
            sRes = _catViewMdl.lstAreaHist.ToString();
            return sRes;

        }


        [HttpPost]
        public string FlujoAreasTraerHijos(string fechaF, string id)
        {
            string sRes = "";
            fechaF = fechaF.Replace('-', '/');
            AreaHistorialViewModel _catViewMdl = new AreaHistorialViewModel();
            DateTime date = Convert.ToDateTime(fechaF);
            date = date.Date;
            string dataP = date.ToString("d") + "|" + id;
            _catViewMdl.lstAreaHist = JsonTransform.convertJson((List<SIT_ADM_AREAHIST>)_sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectAreaHijos), dataP));
            sRes = _catViewMdl.lstAreaHist.ToString();
            return sRes;

        }

        [HttpGet]
        public string ARHGetParent(string id)
        {
            return "[{'text' : 'Root', 'id' : '1', 'children' : true}]";
        }

    }
}