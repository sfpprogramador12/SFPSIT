using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFP.SIT.WEB.Services;
using SFP.SIT.WEB.Injection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using SFP.SIT.WEB.Util;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Core;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Dao.SOL;
using System.Data;
using SFP.Persistencia.Util;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Model.RESP;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SFP.SIT.WEB.Controllers
{
    public class RecursoController : SitBaseCtlr
    {
        SolicitudSer _solServ;
        public RecursoController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<SolicitudController> logger, IHostingEnvironment app) 
            : base(memCache, httpContextAccessor, logger, app)
        {
            _iUsuario = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
            _solServ = new SolicitudSer(_sitDmlDbSer);
        }
        [HttpGet]
        public IActionResult Revision()
        {
            int iAreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
            @ViewBag.turnarArea = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectAreasActual), DateTime.Now)); 
           
            return View();
        }
       
        [HttpGet]
        public IActionResult PVrevision(long lFolio)
        {
            Int32 iTipoProceso = 0;
            SIT_SOL_SEGUIMIENTO segMdlAcl;

            DateTime dtFechaSol; 

            @ViewBag.controlName = "Recurso";            
            @ViewBag.solicitante = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectSolicitudTranspuestaID), lFolio) as DataTable);
            @ViewBag.solicitud = _solServ.ObtenerSolicitudTranspuesta(lFolio, out dtFechaSol);
            SIT_SOL_SOLICITUD solMdl = _solServ.ObtenerSolicitudID(lFolio);

            if (solMdl == null)
            {
                @ViewBag.fecAclaracion = 0;
                @ViewBag.aclaracion = JsonTransform.NO_RECORDS;
                @ViewBag.fecfin = 0;
                @ViewBag.fecRecRev = 0;
                @ViewBag.respAreas = JsonTransform.NO_RECORDS;
                @ViewBag.tipoPrcActual = 0;
                @ViewBag.folio = 0;
                @ViewBag.ultimoNodo = 0;
            }
            else
            {
                if (solMdl.solfecacl == DateTime.MinValue)
                {
                    @ViewBag.fecAclaracion = 0;
                    @ViewBag.aclaracion = JsonTransform.NO_RECORDS;
                    iTipoProceso = Constantes.ProcesoTipo.SOLICITUD;
                    segMdlAcl = _solServ.ObtenerSeguimiento(lFolio, Constantes.ProcesoTipo.SOLICITUD);
                }
                else
                {
                    @ViewBag.fecAclaracion = 1;
                    iTipoProceso = Constantes.ProcesoTipo.ACLARACION;
                    segMdlAcl = _solServ.ObtenerSeguimiento(lFolio, iTipoProceso);
                    @ViewBag.aclaracion = _solServ.ObtenerAclaracion(lFolio, Constantes.Respuesta.REQ_INFO_ADICIONAL,
                        Constantes.Respuesta.RECEPCION_INFO_ADICIONAL);
                }

                if (segMdlAcl.segfecfin == DateTime.MinValue)
                    @ViewBag.fecfin = 0;
                else
                    @ViewBag.fecfin = 1;


                if (solMdl.solfecrecrev == DateTime.MinValue)
                    @ViewBag.fecRecRev = 0;
                else
                    @ViewBag.fecRecRev = 1;

                @ViewBag.respAreas = _solServ.ObtenerRespParaRecurso(lFolio, segMdlAcl.segultimonodo, iTipoProceso);
                @ViewBag.tipoPrcActual = iTipoProceso;
                @ViewBag.folio = lFolio;

                // BSUCAR EL SEUGIENTO 
                SIT_SOL_SEGUIMIENTO segMdl = _solServ.ObtenerSeguimiento(lFolio, iTipoProceso);
                if (segMdl == null)
                    @ViewBag.ultimoNodo = 0;
                else
                    @ViewBag.ultimoNodo = segMdl.segultimonodo;
            }
            return View();
        }

        [HttpPost]
        public IActionResult PVrevision(Int64 turnarFolio, string turnarAreas, 
            string expediente, string fecRecepcion, string responsable, string correo, string fecRespuesta,
            IFormFile docNotificar, IFormFile docRespuesta, IFormFile docSolicitud, IFormFile docRecurso, IFormFile docAcuerdo,
            string actos, string resumen)
        {
            AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
            List<Tuple<int, string, int>> lstPersonasTurnar = JsonTransform.ValidarAreas(turnarAreas);

            //if (afdDataMdl.lstPersonasTurnar.Count > 0)
            if (lstPersonasTurnar.Count > 0)
            {
                afdDataMdl.solClave = turnarFolio;
                afdDataMdl.usrClaveOrigen = _iUsuario;
                afdDataMdl.dicDiaNoLaboral = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DIA_NO_LABORAL) as Dictionary<Int64, char>;
                afdDataMdl.lstProcesoPlazos = _memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_PROCESOPLAZOS) as List<SIT_SOL_PROCESOPLAZOS>;
                afdDataMdl.FechaRecepcion = FechaUtil.AsignarFecha(fecRecepcion);
                afdDataMdl.ID_AreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
                afdDataMdl.ID_EstadoActual = Constantes.NodoEstado.UT_RECIBIR_SOLICITUD;
                afdDataMdl.ID_ClaAfd = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.FLUJO);
                afdDataMdl.Observacion = actos;

                // OBTENER LA SOLICITUD
                afdDataMdl.solicitud = (SIT_SOL_SOLICITUD)_solServ.ObtenerSolicitudID(turnarFolio);

                /* DATOS DEL RECURSO DE REVISION*/
                ////////////SIT_ARISTA_RREVISION  recRevisionMdl = new SIT_ARISTA_RREVISION(rrvcorreo: correo);
                SIT_RESP_RREVISION recRevisionMdl = new SIT_RESP_RREVISION();

                afdDataMdl.recRevisionMdl = recRevisionMdl;
                afdDataMdl.ID_FecEstimada = FechaUtil.AsignarFecha(fecRespuesta);
                
                DocumentoSer DocAdmSer = new DocumentoSer(null);
                DocAdmSer.dicExtension = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DOC_EXTENSION) as Dictionary<string, SIT_DOC_EXTENSION>;

                afdDataMdl.lstDocContenidoMdl = new List<DocContenidoMdl>();
                if (docNotificar != null)
                    afdDataMdl.lstDocContenidoMdl.Add( DocAdmSer.AnexarArchivo(docNotificar, _app.ContentRootPath));

                if (docRespuesta != null)
                    afdDataMdl.lstDocContenidoMdl.Add( DocAdmSer.AnexarArchivo(docRespuesta, _app.ContentRootPath));

                if (docSolicitud != null)
                    afdDataMdl.lstDocContenidoMdl.Add( DocAdmSer.AnexarArchivo(docSolicitud, _app.ContentRootPath));

                if (docRecurso != null)
                    afdDataMdl.lstDocContenidoMdl.Add( DocAdmSer.AnexarArchivo(docRecurso, _app.ContentRootPath));

                if (docAcuerdo != null)
                    afdDataMdl.lstDocContenidoMdl.Add( DocAdmSer.AnexarArchivo(docAcuerdo, _app.ContentRootPath));

                /* EJECUTAR LA TRANSACCION */
                Dictionary<String, Object> dicParam = new Dictionary<String, Object>();                 
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);              
            }
            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

        [HttpGet]
        public IActionResult Documento(Int32 id)
        {
            _appLog.opdesc = "Documento";
            _appLog.data = " { Documento : { \"id\":" + id + "}} ";

            DocContenidoMdl frmDocMdl = _solServ.ObtenerDocumentoID(id, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);
            if (frmDocMdl != null)
                return File(frmDocMdl.doc_contenido, frmDocMdl.extmimetype, frmDocMdl.docnombre);
            else
                return Content("<b>El archivo a desplegar no fue ser localizado, favor de comunicarlo al área de Tecnologías de la Información</b>");
        }
    }
}