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
using SFP.SIT.WEB.Models;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Dao.RESP;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFP.SIT.SERV.Dao;
using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.DOC;
using SFP.SIT.SERV.Model.SOL;
using System.IO;
using Newtonsoft.Json;
using SFP.Persistencia.Model;
using System.Data;
using SFP.Persistencia.Util;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SFP.SIT.WEB.Controllers
{ 

    public class RespuestaController : SitBaseCtlr
    {
        SolicitudSer _solServ;

        public RespuestaController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<RespuestaController> logger, IHostingEnvironment app)
            : base(memCache, httpContextAccessor, logger, app)
        {
            _iUsuario = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
            _solServ = new SolicitudSer(_sitDmlDbSer);
        }

        /*********************************************
               FUNCIONES GENERALES PARA LOS NODOS
        ******************************************** */
        public AfdEdoDataMdl NodoActualIni(Int64 folio, Int32 tipoArista, Int64 nodo)
        {
            AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();

            afdDataMdl.solClave = folio;
            afdDataMdl.ID_AreaInai = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI);
            afdDataMdl.ID_AreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);

            afdDataMdl.usrClaveOrigen = _iUsuario;
            afdDataMdl.FechaRecepcion = DateTime.Now;
            afdDataMdl.rtpclave = tipoArista;
            afdDataMdl.dicDiaNoLaboral = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DIA_NO_LABORAL) as Dictionary<Int64, char>;
            afdDataMdl.lstProcesoPlazos = _memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_PROCESOPLAZOS) as List<SIT_SOL_PROCESOPLAZOS>;
            afdDataMdl.SharePointCxn = _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl;

            // BUSCAR DATOS PARA PROCESAR LA ACCION
            afdDataMdl.AFDnodoActMdl = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoID), nodo) as SIT_RED_NODO;

            afdDataMdl.ID_EstadoActual = (int)afdDataMdl.AFDnodoActMdl.nedclave;
            afdDataMdl.AFDseguimientoMdl = _solServ.ObtenerSeguimiento(afdDataMdl.solClave, (int)afdDataMdl.AFDnodoActMdl.prcclave);
            afdDataMdl.solicitud = _solServ.ObtenerSolicitudID(afdDataMdl.solClave);
            afdDataMdl.ID_ClaAfd = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.FLUJO);
            afdDataMdl.ID_Capa = afdDataMdl.AFDnodoActMdl.nodcapa;
            return afdDataMdl;
        }

        [HttpPost]
        public string PVborrar(Int64 repclave, int rtpclave, int nodclave)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_RESP_RESPUESTA_COL.REPCLAVE, repclave);
            dicParam.Add(DButil.SIT_RESP_RESPUESTA_COL.RTPCLAVE, rtpclave);
            dicParam.Add(DButil.SIT_RED_NODORESP_COL.NODCLAVE, nodclave);
            
            return _sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.BorrarRespuesta), dicParam).ToString();
        }

        /*********************************************
                ASIGNAR
        ******************************************** */
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult PVasignar()
        {
            ViewBag.controlName = "Respuesta";

            _appLog.opdesc = "PVasignar ";
            _appLog.data = "";
            return View();
        }

        [HttpPost]
        public IActionResult PVasignar(FrmRespAsignarVM respAsignar)
        {
            respAsignar.resRespuesta.rtpclave = Constantes.Respuesta.ASIGNAR;
            respAsignar.resRespuesta.repedofec = DateTime.Now;
            respAsignar.Oper = 1; // INSERTAR
            respAsignar.resAsignar.araclave = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = respAsignar.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            // DATOS DE LA RESPUESTA..
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, respAsignar.resRespuesta);           
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_TURNAR, respAsignar.resAsignar);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);        
           
            //// DATOS GENERALES A AGRUPAR....
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, respAsignar.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(respAsignar.solfecsolticks));
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, respAsignar.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_ASIGNAR);

            AfdEdoDataMdl afdDataMdl = NodoActualIni(respAsignar.solclave, Constantes.Respuesta.ASIGNAR, respAsignar.nodclave);
            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            afdDataMdl.dicAuxRespuesta = dicDatos;            
            afdDataMdl.usrClaveDestino = respAsignar.resAsignar.usrclave;
            afdDataMdl.usrClaveOrigen = _iUsuario;
            afdDataMdl.usrClaveAusencia = _iUsuario;  // Aqui cambiar de acuerdo a la página WEB
            afdDataMdl.ID_PerfilDestino = Constantes.Perfil.PRUT;

            afdDataMdl.ID_AreaUT = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;            
            string oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl).ToString();

            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

        /*********************************************
               GENERAL TURNAR
        ******************************************** */
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult PVturnar()
        {
            ViewBag.controlName = "Respuesta";

            _appLog.opdesc = "PVturnar ";
            _appLog.data = "";
            return View();
        }

        [HttpPost]
        //////[ValidateInput(false)]
        public IActionResult PVturnar(Int64 turnarFolio, Int64 turnarClaNodo, string turnarAreas, int turnarTipoArista, IFormFile ArchivoTurnar)
        {
            AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
            List<Tuple<int, string, int>> lstTurnar = JsonTransform.ValidarAreas(turnarAreas);

            if (lstTurnar != null)
            {
                ///VERIFICAR::: qeu tanto se necestia...
                afdDataMdl = NodoActualIni(turnarFolio, turnarTipoArista, turnarClaNodo);


                SIT_RESP_RESPUESTA resRespuesta = new SIT_RESP_RESPUESTA();
                resRespuesta.repedofec = DateTime.Now;
                resRespuesta.rtpclave = Constantes.Respuesta.TURNAR;
                resRespuesta.repclave = 0;


                SIT_RED_NODORESP nodoResp = new SIT_RED_NODORESP();
                nodoResp.nodclave = afdDataMdl.AFDnodoActMdl.nodclave;
                nodoResp.sdoclave =  Constantes.RespuestaEstado.PROPUESTA;


                Dictionary<string, object> dicDatos = new Dictionary<string, object>();
                // DATOS DE LA RESPUESTA..
                dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, resRespuesta);
                dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, nodoResp);

                DocContenidoMdl DocResp = null;
                if (ArchivoTurnar != null)
                {
                    DocResp = DocumentoConvertir(ArchivoTurnar, "S/N");
                }
                dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, DocResp);

                //// DATOS GENERALES A AGRUPAR....
                dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, afdDataMdl.AFDnodoActMdl.nodclave);
                dicDatos.Add(ProcesoGralDao.PARAM_FECHA, afdDataMdl.solicitud.solfecsol);
                dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
                dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, 1);
                dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
                dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_TURNAR);
                dicDatos.Add(ProcesoGralDao.PARAM_LISTA_TURNAR, lstTurnar);


                ////afdDataMdl.lstPersonasTurnar = lstTurnar;
                ////if (afdDataMdl.lstPersonasTurnar.Count > 1)


                if (lstTurnar.Count > 1)
                    afdDataMdl.AFDseguimientoMdl.segmultiple = 1;


                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.dicAuxRespuesta = dicDatos;
                ///  afdDataMdl.usrClaveDestino = respAsignar.resAsignar.usrclave; cambio por cada turno
                afdDataMdl.usrClaveOrigen = _iUsuario;
                afdDataMdl.usrClaveAusencia = _iUsuario;  // Aqui cambiar de acuerdo a la página WEB
                afdDataMdl.ID_PerfilDestino = Constantes.Perfil.UA;
                
                afdDataMdl.ID_AreaUT = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
                object oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl).ToString();

            }

            _appLog.opdesc = "PVturnar - AfdServicio.OPE_TURNAR ";
            _appLog.data = afdDataMdl.Datos();

            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

        /*********************************************
               RESPUESTA PUBLICA
        ******************************************** */
        [HttpGet]
        // aqui mne falta el nodo....
        public IActionResult PVpublica(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            // SERA NECESARIO BUSCAR
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA

            FrmRespPublicaVM frmResponder = new FrmRespPublicaVM();
            frmResponder.solclave = solclave;
            frmResponder.proclave = proclave;
            frmResponder.nodclave = nodclave;
            frmResponder.araclave = araclave;
            frmResponder.Oper = Oper;

            frmResponder.solfecsol = solFecTics;
            frmResponder.respRespuesta = new SIT_RESP_RESPUESTA();

            frmResponder.respRespuesta = new SIT_RESP_RESPUESTA();
            frmResponder.respGeneral = new SIT_RESP_GRAL();            
            

            Dictionary<int, String> dicModoEntrega = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SOL_MODOENTREGA_VISIBLE) as Dictionary<int, String>;
            var selectList = new SelectList(dicModoEntrega, "Key", "Value");
            ViewBag.tipoModoEntrega = selectList;

            frmResponder.respRespuesta.repclave = repclave;

            if (repclave > 0)
            {                
                frmResponder.respRespuesta = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), frmResponder.respRespuesta) as SIT_RESP_RESPUESTA;
                frmResponder.respGeneral.repclave = repclave;
                frmResponder.respGeneral = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_GRALDao>(nameof(SIT_RESP_GRALDao.dmlSelectID), frmResponder.respGeneral) as SIT_RESP_GRAL;

                if (frmResponder.respRespuesta.docclave != null)
                {
                    frmResponder.respDoc = new DocContenidoMdl();
                    frmResponder.respDoc.docclave = (long)frmResponder.respRespuesta.docclave;

                    SIT_DOC_DOCUMENTO docAux = _sitDmlDbSer.operEjecutarTransaccion<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), frmResponder.respDoc) as SIT_DOC_DOCUMENTO;
                    frmResponder.respDoc.docclave = docAux.docclave;
                    frmResponder.respDoc.docnombre = docAux.docnombre;
                }
            }

            _appLog.opdesc = "PVpublica ";
            ViewBag.controlName = "Respuesta";
            return View(frmResponder);
        }

        [HttpPost]
        public IActionResult PVpublica(FrmRespPublicaVM publicaVM, IFormFile docResolucion, int Oper)
        {
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            if (docResolucion != null)
            {
                publicaVM.respDoc = DocumentoConvertir(docResolucion, publicaVM.respRespuesta.repoficio);
            }

            publicaVM.respRespuesta.rtpclave = Constantes.Respuesta.PUBLICA;
            publicaVM.respRespuesta.repedofec = DateTime.Now;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = publicaVM.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, publicaVM.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(publicaVM.solfecsol));

            // DATOS DE LA RESPUESTA..
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, publicaVM.respRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_GENERAL, publicaVM.respGeneral);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, publicaVM.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, publicaVM.respDoc);
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_GENERAL);

            long oResultado = 0;

            if (publicaVM.avanzar == 0)
            {
                // SOLO GUARDAMOS EL ITEM
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmRegistro), dicDatos);
            }
            else
            {
                // AQUI OBTENEMOS LOS DATOS DEL NODO
                AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
                afdDataMdl = NodoActualIni(publicaVM.solclave, (int)publicaVM.respRespuesta.rtpclave, publicaVM.nodclave);
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.oRespRespuesta = publicaVM.respRespuesta;
                afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
                afdDataMdl.usrClaveDestino = (int)afdDataMdl.AFDseguimientoMdl.usrclave;
                dicDatos.Add(RespuestaSer.PARAM_AFDEDODATADML, afdDataMdl);

                //GUARDADMOS LA RESPUESTA Y AVANZAMOS
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<RespuestaSer>(nameof(RespuestaSer.GrabarRespAvanzar), dicDatos);
            }
            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

        /*********************************************
               RESPUESTA RESERVADA
        ******************************************** */
        private FrmRespReservadaVM ObtenerDatosReservada(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            FrmRespReservadaVM frmReservada = new FrmRespReservadaVM();

            frmReservada.recid = 0;
            frmReservada.Oper = 0;
            frmReservada.solclave = solclave;
            frmReservada.proclave = proclave;
            frmReservada.nodclave = nodclave;
            frmReservada.araclave = araclave;
            frmReservada.solfecsol = solFecTics;

            frmReservada.resRespuesta = new SIT_RESP_RESPUESTA();
            frmReservada.resRespuesta.repclave = repclave;

            frmReservada.resReserva = new SIT_RESP_RESERVA();
            frmReservada.dicDocumento = new Dictionary<string, DocContenidoMdl>();

            //AREA                        
            frmReservada.areaNombre = (_sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectAreaActual), araclave) as SIT_ADM_AREAHIST).arhdescripcion;

            // BUSCAR LOS EXPEDIENTES Y SU ESTATUS..
            List<ReservaEdoActMdl> lstResEdoMdl = _sitDmlDbSer.operEjecutar<SIT_RESP_RESERVADao>(nameof(SIT_RESP_RESERVADao.dmlSelectExpEdoArea), araclave) as List<ReservaEdoActMdl>;
            frmReservada.jsonReservaEdo = JsonTransform.convertJson(lstResEdoMdl);

            ViewBag.controlName = "Respuesta";

            Dictionary<int, String> dicModoEntrega = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SOL_MODOENTREGA_VISIBLE) as Dictionary<int, String>;
            var selectList = new SelectList(dicModoEntrega, "Key", "Value");
            ViewBag.tipoModoEntrega = selectList;

            Dictionary<int, String> dicMomento = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_RESP_MOMENTO) as Dictionary<int, String>;            
            var lstMomento = new SelectList(dicMomento, "Key", "Value");
            ViewBag.tipoMomento = lstMomento;

            Dictionary<int, String> dicTema = _sitDmlDbSer.operEjecutar<SIT_RESP_TEMADao>(nameof(SIT_RESP_TEMADao.dmlSelectDicUnidad), araclave) as Dictionary<int, String>;
            if (dicTema != null)
            {
                var lstTema = new SelectList(dicTema, "Key", "Value");
                ViewBag.tipoTema = lstTema;
            }

            if (repclave > 0)
            {
                List<SIT_RESP_DETALLE> lstRespDatos = _sitDmlDbSer.operEjecutar<SIT_RESP_DETALLEDao>(nameof(SIT_RESP_DETALLEDao.dmlSelectRespID), repclave) as List<SIT_RESP_DETALLE>;
                frmReservada.dirRespDetalle = new Dictionary<string, SIT_RESP_DETALLE>();

                foreach (SIT_RESP_DETALLE redpDatos in lstRespDatos)
                {
                    frmReservada.dirRespDetalle.Add(redpDatos.detclave, redpDatos);

                    if (redpDatos.docclave != null)
                    {
                        SIT_DOC_DOCUMENTO docDocumento = new SIT_DOC_DOCUMENTO { docclave = (long)redpDatos.docclave };
                        docDocumento = _sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), docDocumento) as SIT_DOC_DOCUMENTO;
                        frmReservada.dicDocumento.Add(redpDatos.detclave, copiarDocDatos(docDocumento));                        
                    }
                }
            

                ////SIT_RED_NODORESP nodoResp = null;
                SIT_RESP_RESPUESTA resRespuesta = new SIT_RESP_RESPUESTA();
                resRespuesta.repclave = repclave;
                frmReservada.resRespuesta = _sitDmlDbSer.operEjecutar<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), resRespuesta) as SIT_RESP_RESPUESTA;

                SIT_RESP_RESERVA resReserva = new SIT_RESP_RESERVA();
                resReserva.repclave = repclave;
                frmReservada.resReserva = _sitDmlDbSer.operEjecutar<SIT_RESP_RESERVADao>(nameof(SIT_RESP_RESERVADao.dmlSelectID), resReserva) as SIT_RESP_RESERVA;

                if (frmReservada.resRespuesta.docclave != null)
                {
                    frmReservada.respDoc = new DocContenidoMdl();
                    frmReservada.respDoc.docclave = (long)frmReservada.resRespuesta.docclave;

                    SIT_DOC_DOCUMENTO docAux = _sitDmlDbSer.operEjecutarTransaccion<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), frmReservada.respDoc) as SIT_DOC_DOCUMENTO;
                    frmReservada.respDoc.docclave = docAux.docclave;
                    frmReservada.respDoc.docnombre = docAux.docnombre;
                }

            }
            frmReservada.Oper = Oper;

            return frmReservada;
        }

        [HttpGet]
        public IActionResult PVreservada(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            FrmRespReservadaVM frmReservada = ObtenerDatosReservada(solclave, proclave, nodclave, araclave, repclave, solFecTics, Oper);

            _appLog.opdesc = "PVreservada";
            _appLog.data = "";
            return View(frmReservada);
        }

        [HttpPost]
        public IActionResult PVreservada(FrmRespReservadaVM frmReservada, IFormFile docResolucion, IFormFile docFundamento,
            IFormFile docJustificacion, IFormFile docMotivos, IFormFile docPartes, int Oper)
        {
            frmReservada.resRespuesta.repedofec = DateTime.Now;
            frmReservada.resRespuesta.repoficio = frmReservada.resReserva.rsvexpediente;


            frmReservada.dicDocumento = new Dictionary<string, DocContenidoMdl>();

            if (docResolucion != null)
            {
                frmReservada.dicDocumento.Add(Constantes.RespuestaTipoContenido.CONTENIDO,
                    DocumentoConvertir(docResolucion, frmReservada.resRespuesta.repoficio));
            }

            if (docFundamento != null)
            {
                frmReservada.dicDocumento.Add(Constantes.RespuestaTipoContenido.FUNDAMENTO_LEGAL,
                    DocumentoConvertir(docFundamento, docFundamento.FileName));
            }

            if (docJustificacion != null)
            {
                frmReservada.dicDocumento.Add(Constantes.RespuestaTipoContenido.JUSTIFICACION,
                    DocumentoConvertir(docResolucion, docResolucion.FileName));
            }

            if (docMotivos != null)
            {
                frmReservada.dicDocumento.Add(Constantes.RespuestaTipoContenido.MOTIVOS,
                    DocumentoConvertir(docMotivos, docMotivos.FileName));
            }

            if (docPartes != null)
            {
                frmReservada.dicDocumento.Add(Constantes.RespuestaTipoContenido.RESERVA_PARTES,
                    DocumentoConvertir(docPartes, docPartes.FileName));
            }

            frmReservada.dirRespDetalle[Constantes.RespuestaTipoContenido.FUNDAMENTO_LEGAL].detclave = Constantes.RespuestaTipoContenido.FUNDAMENTO_LEGAL;
            frmReservada.dirRespDetalle[Constantes.RespuestaTipoContenido.JUSTIFICACION].detclave = Constantes.RespuestaTipoContenido.JUSTIFICACION;
            frmReservada.dirRespDetalle[Constantes.RespuestaTipoContenido.MOTIVOS].detclave = Constantes.RespuestaTipoContenido.MOTIVOS;
            if (frmReservada.dirRespDetalle.ContainsKey(Constantes.RespuestaTipoContenido.RESERVA_PARTES))
            {
                frmReservada.dirRespDetalle[Constantes.RespuestaTipoContenido.RESERVA_PARTES].detclave = Constantes.RespuestaTipoContenido.RESERVA_PARTES;
            }
            

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = frmReservada.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, frmReservada.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(frmReservada.solfecsol));
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, frmReservada.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);

            //RESPUESTA
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_RESERVA);

            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESERVA, frmReservada.resReserva);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, frmReservada.resRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            dicDatos.Add(ProcesoGralDao.PARAM_DIC_RESP_DETALLE, frmReservada.dirRespDetalle);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, frmReservada.dicDocumento[Constantes.RespuestaTipoContenido.CONTENIDO]);
            dicDatos.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            long oResultado = 0;
            if (frmReservada.avanzar == 0)
            {
                // SOLO GUARDAMOS EL ITEM
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmRegistro), dicDatos);
            }
            else
            {
                // AQUI OBTENEMOS LOS DATOS DEL NODO
                AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
                afdDataMdl = NodoActualIni(frmReservada.solclave, (int)frmReservada.resRespuesta.rtpclave, frmReservada.nodclave);
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.oRespRespuesta = frmReservada.resRespuesta;
                afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
                afdDataMdl.usrClaveDestino = (int)afdDataMdl.AFDseguimientoMdl.usrclave;

                dicDatos.Add(RespuestaSer.PARAM_AFDEDODATADML, afdDataMdl);

                //GUARDADMOS LA RESPUESTA Y AVANZAMOS
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<RespuestaSer>(nameof(RespuestaSer.GrabarRespAvanzar), dicDatos);
            }

            return RedirectToAction("BandejaEntrada", "Solicitud");
            //return Content(" <div id='Resultado'>" + oResultado + "</div>");

        }


        /*********************************************
               RESPUESTA RESERVADA PARCIAL
        ******************************************** */
        [HttpGet]
        public IActionResult PVreservadaParcial(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            FrmRespReservadaVM frmReservada = ObtenerDatosReservada(solclave, proclave, nodclave, araclave, repclave, solFecTics, Oper);

            _appLog.opdesc = "PVreservada";
            _appLog.data = "";
            return View(frmReservada);
        }

        /*********************************************
                RESPONDER AL INAI
        ******************************************** */
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult PVresponder(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            FrmRespFinal frmResponder = new FrmRespFinal();
            frmResponder.solclave = solclave;
            frmResponder.proclave = proclave;
            frmResponder.nodclave = nodclave;
            frmResponder.araclave = araclave;

            frmResponder.solfecsol = solFecTics;
            frmResponder.resRespuesta = new SIT_RESP_RESPUESTA();
            frmResponder.resRespuesta.repclave = repclave;
            frmResponder.resRespuesta.repcantidad = 0;

            ///PARAM_RESP_GENERAL
            frmResponder.resRespGral = new SIT_RESP_GRAL();
            frmResponder.resRespGral.gracontenido = "";
            
            frmResponder.resRespuesta.repoficio = "";

            ViewBag.controlName = "Respuesta";

            Dictionary<int, String> dicModoEntrega = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SOL_MODOENTREGA_VISIBLE) as Dictionary<int, String>;
            var selectList = new SelectList(dicModoEntrega, "Key", "Value");
            ViewBag.tipoModoEntrega = selectList;

            Dictionary<int, String> dicReproduccion = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_RESP_REPRODUCCION) as Dictionary<int, String>;
            selectList = new SelectList(dicReproduccion, "Key", "Value");
            ViewBag.tipoReproduccion = selectList;

            Dictionary<int, String> dicRespINAI = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_RESP_INAI) as Dictionary<int, String>;
            selectList = new SelectList(dicRespINAI, "Key", "Value");
            ViewBag.respINAI = selectList;

            frmResponder.Oper = ProcesoGralDao.OP_INSERTAR;

            _appLog.opdesc = "PVresponder ";
            _appLog.data = "";
            return View(frmResponder);
        }
        
        [HttpPost]
        public IActionResult PVresponder(FrmRespFinal frmRespINAI, IFormFile docResolucion)
        {
            if (frmRespINAI.resRespuesta.repoficio.Trim() == "")
                frmRespINAI.resRespuesta.repoficio = "S/N";

            frmRespINAI.docDocumento = DocumentoConvertir(docResolucion, frmRespINAI.resRespuesta.repoficio);
            frmRespINAI.resRespuesta.rtpclave = frmRespINAI.rtpclaveAux;
            frmRespINAI.resRespuesta.repedofec = DateTime.Now;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = frmRespINAI.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;
            
            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            // DATOS DE LA RESPUESTA..
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_GENERAL);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, frmRespINAI.resRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_GENERAL, frmRespINAI.resRespGral);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, frmRespINAI.docDocumento);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);

            // DATOS GENERALES A AGRUPAR....
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, frmRespINAI.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(frmRespINAI.solfecsolticks));
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, frmRespINAI.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);

            AfdEdoDataMdl afdDataMdl = NodoActualIni(frmRespINAI.solclave, Constantes.Respuesta.RESPONDER, frmRespINAI.nodclave);
            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            afdDataMdl.dicAuxRespuesta = dicDatos;
            afdDataMdl.usrClaveDestino = _iUsuario;
            afdDataMdl.usrClaveOrigen = _iUsuario;
            afdDataMdl.usrClaveAusencia = _iUsuario;  // Aqui cambiar de acuerdo a la página WEB
            afdDataMdl.ID_PerfilDestino = Constantes.Perfil.INAI;
            string oResultado =  _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl).ToString();
            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

        /*********************************************
               RESPUESTA CONFIDENCIAL
        ******************************************** */
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult PVconfidencial(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            ViewBag.controlName = "Respuesta";

            // SERA NECESARIO BUSCAR
            FrmRespConfidencialVM frmConfidencial = new FrmRespConfidencialVM();

            frmConfidencial.Oper = 0;
            frmConfidencial.solclave = solclave;
            frmConfidencial.proclave = proclave;
            frmConfidencial.nodclave = nodclave;
            frmConfidencial.araclave = araclave;
            frmConfidencial.solfecsol = solFecTics;

            frmConfidencial.resRespuesta = new SIT_RESP_RESPUESTA();
            frmConfidencial.resRespuesta.repclave = repclave;
            frmConfidencial.docResolucionMdl = new DocContenidoMdl();

            Dictionary<int, String> dicModoEntrega = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SOL_MODOENTREGA_VISIBLE) as Dictionary<int, String>;
            var selectList = new SelectList(dicModoEntrega, "Key", "Value");
            ViewBag.tipoModoEntrega = selectList;

            frmConfidencial.DatosJson = JsonTransform.NO_RECORDS;

            if (repclave > 0)
            {
                frmConfidencial.DatosJson = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectRespuestaTipoDatos), repclave) as DataTable);
                frmConfidencial.resRespuesta = new SIT_RESP_RESPUESTA();
                frmConfidencial.resRespuesta.repclave = repclave;
                frmConfidencial.resRespuesta = _sitDmlDbSer.operEjecutar<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), frmConfidencial.resRespuesta) as SIT_RESP_RESPUESTA;
                frmConfidencial.Oper = ProcesoGralDao.OP_EDITAR;

                if (frmConfidencial.resRespuesta.docclave != null)
                {
                    frmConfidencial.docResolucionMdl = copiarDocDatos(_sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID),
                        new SIT_DOC_DOCUMENTO { docclave = (long)frmConfidencial.resRespuesta.docclave }) as SIT_DOC_DOCUMENTO);
                }
            }
            else
            {
                frmConfidencial.Oper = ProcesoGralDao.OP_INSERTAR;
            }

                _appLog.opdesc = "PVconfidencial";
            _appLog.data = "";
            return View(frmConfidencial);
        }

        [HttpPost]
        public IActionResult PVconfidencial(FrmRespConfidencialVM frmConfidencial, IFormFile docResolucion, IList<IFormFile> files)
        {
            
            frmConfidencial.resRespuesta.repedofec = DateTime.Now;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = frmConfidencial.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            if (docResolucion != null)
            {
                frmConfidencial.docResolucionMdl = DocumentoConvertir(docResolucion, frmConfidencial.resRespuesta.repoficio);
            }

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, frmConfidencial.nodclave);

            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(frmConfidencial.solfecsol));
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, frmConfidencial.resRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, frmConfidencial.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            // AQUI TRTMAOS TANTO LA CONFIDENICAL ASI COM LA PARCIAL,,,
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_CONFIDENCIAL);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, frmConfidencial.docResolucionMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            long oResultado = 0;

            if (frmConfidencial.avanzar == 0)
            {
                // SOLO GUARDAMOS EL ITEM
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmRegistro), dicDatos);
            }
            else
            {
                // AQUI OBTENEMOS LOS DATOS DEL NODO
                AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
                afdDataMdl = NodoActualIni(frmConfidencial.solclave, (int)frmConfidencial.resRespuesta.rtpclave, frmConfidencial.nodclave);
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.oRespRespuesta = frmConfidencial.resRespuesta;
                afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
                afdDataMdl.usrClaveDestino = (int)afdDataMdl.AFDseguimientoMdl.usrclave;

                dicDatos.Add(RespuestaSer.PARAM_AFDEDODATADML, afdDataMdl);

                //GUARDADMOS LA RESPUESTA Y AVANZAMOS
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<RespuestaSer>(nameof(RespuestaSer.GrabarRespAvanzar), dicDatos);
            }

            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

        [HttpPost]
        public string PVconfidencialItem(int ctpClave, long repClave, long nodclave, long solfecsolticks, int OperSubTipo, 
           string repoficio, int megclave, int repcantidad, int rtpclave, 
           string txtClave, string txtDescripcion, IFormFile docTipoDato)
        {
            // OCULTAR LOS BOTONES EN LA FORMA PARA EVITAR EL AVANCE...
            // OCULTAR LOS BOTONES EN LA FORMA PARA EVITAR EL AVANCE...
            // OCULTAR LOS BOTONES EN LA FORMA PARA EVITAR EL AVANCE...
            // OCULTAR LOS BOTONES EN LA FORMA PARA EVITAR EL AVANCE...
            // OCULTAR LOS BOTONES EN LA FORMA PARA EVITAR EL AVANCE...
            // OCULTAR LOS BOTONES EN LA FORMA PARA EVITAR EL AVANCE...
            // OCULTAR LOS BOTONES EN LA FORMA PARA EVITAR EL AVANCE...
            // OCULTAR LOS BOTONES EN LA FORMA PARA EVITAR EL AVANCE...

            /* RESPUESTA POR SI NO EXISTE */
            SIT_RESP_RESPUESTA respRespuesta = new SIT_RESP_RESPUESTA();
            respRespuesta.repclave = repClave;
            respRespuesta.repoficio = repoficio;
            respRespuesta.repedofec = DateTime.Now;
            respRespuesta.rtpclave = Constantes.Respuesta.INFO_CONFIDENCIAL;
            respRespuesta.megclave = megclave;
            respRespuesta.repcantidad = repcantidad;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            DocContenidoMdl docItemMdl = null;
            if (docTipoDato != null)
            {
                docItemMdl = DocumentoConvertir(docTipoDato, docTipoDato.Name);
            }

            SIT_RESP_DETALLE respDetalle = new SIT_RESP_DETALLE();
            respDetalle.detclave = txtClave;
            respDetalle.detdescripcion = txtDescripcion;
            respDetalle.repclave = repClave;

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(solfecsolticks));
            
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, OperSubTipo);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
           
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_CONFIDENCIAL_ITEM);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, respRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_DETALLE, respDetalle);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            dicDatos.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            if (docItemMdl != null)
            {
                dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO_ITEM, docItemMdl);
            }

            // REQUIERO AGREGAR EL ITEM...
            long oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmConfItemRegistro), dicDatos);

            if (repClave == 0)
                repClave = oResultado;

            DataTable dtDatos = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectRespuestaTipoDatos), repClave) as DataTable;
            String sResultado = JsonTransform.convertJson(dtDatos);

            ///return "{ \"recid\":\"John\", \"repclave\":31, \"repclave\":\"New York\", \"ctpclave\": ,  \"ctpdescripcion\": ,\"docclave\": ,  \"datdescripcion\": } ";
            // objecto a regresar,,,, 
            /// recid: obj.recid, repclave: obj.repclave, ctpclave: obj.ctpclave, ctpdescripcion: obj.ctpdescripcion, docclave: obj.docclave,  datdescripcion: obj.datdescripcion
            return sResultado;
        }


        /*********************************************
               RESPUESTA CONFIDENCIAL PARCIAL
        ******************************************** */

        // NO EXISTE DIFENRENICA EN CONFIDENCIAL
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult PVconfidencialParcial(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {

            return PVconfidencial(solclave, proclave, nodclave, araclave, repclave, solFecTics, Oper);

        }

        /*********************************************
               RESPUESTA INEXISTENCIA
        ******************************************** */
        private DocContenidoMdl copiarDocDatos( SIT_DOC_DOCUMENTO oOrigen)
        {
            DocContenidoMdl oDestino = new DocContenidoMdl();

            if (oOrigen != null)
            {                
                oDestino.docclave = oOrigen.docclave;
                oDestino.docnombre = oOrigen.docnombre;
                oDestino.docfolio = oOrigen.docfolio;
            }
            return oDestino;
        }

        [HttpGet]
        public IActionResult PVinexistencia(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            ViewBag.controlName = "Respuesta";

            Dictionary<int, String> dicModoEntrega = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SOL_MODOENTREGA_VISIBLE) as Dictionary<int, String>;
            var selectList = new SelectList(dicModoEntrega, "Key", "Value");
            ViewBag.tipoModoEntrega = selectList;

            FrmRespInexistenciaVM frmInexistencia = new FrmRespInexistenciaVM();

            frmInexistencia.Oper = Oper;
            frmInexistencia.solclave = solclave;
            frmInexistencia.proclave = proclave;
            frmInexistencia.nodclave = nodclave;
            frmInexistencia.araclave = araclave;
            frmInexistencia.solfecsol = solFecTics;

            frmInexistencia.resRespuesta = new SIT_RESP_RESPUESTA();
            frmInexistencia.resRespuesta.repclave = repclave;
            frmInexistencia.resInexistencia = new SIT_RESP_INEXISTENCIA();

            frmInexistencia.respLugar = new SIT_RESP_DETALLE();
            frmInexistencia.respModo = new SIT_RESP_DETALLE();
            frmInexistencia.respTiempo = new SIT_RESP_DETALLE();


            if (repclave > 0) {                
                SIT_RESP_RESPUESTA respDatos = new SIT_RESP_RESPUESTA();
                respDatos.repclave = repclave;
                frmInexistencia.resRespuesta = _sitDmlDbSer.operEjecutar<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), respDatos) as SIT_RESP_RESPUESTA;

                if (frmInexistencia.resRespuesta.docclave != null)
                {                    
                    frmInexistencia.docResolucionMdl = copiarDocDatos(_sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), 
                        new SIT_DOC_DOCUMENTO { docclave = (long)frmInexistencia.resRespuesta.docclave } ) as SIT_DOC_DOCUMENTO);
                }


                SIT_RESP_INEXISTENCIA respInex = new SIT_RESP_INEXISTENCIA();
                respInex.repclave = repclave;
                frmInexistencia.resInexistencia = _sitDmlDbSer.operEjecutar<SIT_RESP_INEXISTENCIADao>(nameof(SIT_RESP_INEXISTENCIADao.dmlSelectID), respInex) as SIT_RESP_INEXISTENCIA;

                List<SIT_RESP_DETALLE> lstRespDatos = _sitDmlDbSer.operEjecutar<SIT_RESP_DETALLEDao>(nameof(SIT_RESP_DETALLEDao.dmlSelectRespID), repclave) as List<SIT_RESP_DETALLE>;
                SIT_DOC_DOCUMENTO docDocumento = null;

                foreach (SIT_RESP_DETALLE redpDatos in lstRespDatos)
                {
                    if (redpDatos.docclave != null)
                    {
                        docDocumento = new SIT_DOC_DOCUMENTO { docclave = (long)redpDatos.docclave };
                        docDocumento = _sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), docDocumento) as SIT_DOC_DOCUMENTO;
                    }

                    if (redpDatos.detclave == Constantes.RespuestaTipoContenido.LUGAR)
                    {
                        frmInexistencia.respLugar = redpDatos;
                        frmInexistencia.docLugarMdl = copiarDocDatos(docDocumento);
                    }
                    else if (redpDatos.detclave == Constantes.RespuestaTipoContenido.MODO)
                    {
                        frmInexistencia.respModo = redpDatos;
                        frmInexistencia.docModoMdl = copiarDocDatos(docDocumento);
                    }
                    else if (redpDatos.detclave == Constantes.RespuestaTipoContenido.TIEMPO)
                    {
                        frmInexistencia.respTiempo = redpDatos;
                        frmInexistencia.docTiempoMdl = copiarDocDatos(docDocumento);
                    }
                    docDocumento = null;

                }
            }
            else
            {
                frmInexistencia.respLugar.detclave = Constantes.RespuestaTipoContenido.LUGAR;
                frmInexistencia.respModo.detclave = Constantes.RespuestaTipoContenido.MODO;
                frmInexistencia.respTiempo.detclave = Constantes.RespuestaTipoContenido.TIEMPO;

                frmInexistencia.docResolucionMdl = new DocContenidoMdl();
                frmInexistencia.docLugarMdl = new DocContenidoMdl();
                frmInexistencia.docModoMdl = new DocContenidoMdl();
                frmInexistencia.docTiempoMdl = new DocContenidoMdl();
            }

            _appLog.opdesc = "PVinexistencia";
            _appLog.data = "";

            return View(frmInexistencia);
        }

        [HttpPost]
        public IActionResult PVinexistencia(FrmRespInexistenciaVM oInexistencia, IFormFile docResolucion,  IFormFile docLugar, IFormFile docModo, IFormFile docTiempo)
        {
            
            if (docResolucion != null)
            {
                oInexistencia.docResolucionMdl = DocumentoConvertir(docResolucion, "Resolución " + oInexistencia.resRespuesta.repoficio );
            }

            if (docLugar != null)
            {
                oInexistencia.docLugarMdl = DocumentoConvertir(docLugar, "Resp Lugar " + oInexistencia.solclave);
            }

            if (docModo != null)
            {
                oInexistencia.docModoMdl = DocumentoConvertir(docModo, "Resp modo " + oInexistencia.solclave);
            }

            if (docTiempo != null)
            {
                oInexistencia.docTiempoMdl = DocumentoConvertir(docTiempo, "Resp Lugar " + oInexistencia.solclave);
            }
            oInexistencia.resRespuesta.rtpclave = Constantes.Respuesta.INEXISTENCIA_EN_AREA;
            oInexistencia.resRespuesta.repedofec = DateTime.Now;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = oInexistencia.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;
            
            Dictionary <String, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>> dicRespDatos = new Dictionary<String, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>>();
            dicRespDatos.Add(Constantes.RespuestaTipoContenido.LUGAR, new Tuple<SIT_RESP_DETALLE, DocContenidoMdl>(oInexistencia.respLugar, oInexistencia.docLugarMdl));
            dicRespDatos.Add(Constantes.RespuestaTipoContenido.MODO, new Tuple<SIT_RESP_DETALLE, DocContenidoMdl>(oInexistencia.respModo, oInexistencia.docModoMdl));
            dicRespDatos.Add(Constantes.RespuestaTipoContenido.TIEMPO, new Tuple<SIT_RESP_DETALLE, DocContenidoMdl>(oInexistencia.respTiempo, oInexistencia.docTiempoMdl));

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, oInexistencia.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_INEXISTENCIA, oInexistencia.resInexistencia);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_LSTDETALLE, dicRespDatos);
            

            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(oInexistencia.solfecsol));
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, oInexistencia.resRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, oInexistencia.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_INEXISTENCIA);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, oInexistencia.docResolucionMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            long oResultado = 0;

            if (oInexistencia.avanzar == 0)
            {
                // SOLO GUARDAMOS EL ITEM
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmRegistro), dicDatos);
            }
            else
            {
                // AQUI OBTENEMOS LOS DATOS DEL NODO
                AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
                afdDataMdl = NodoActualIni(oInexistencia.solclave, (int)oInexistencia.resRespuesta.rtpclave, oInexistencia.nodclave);
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.oRespRespuesta = oInexistencia.resRespuesta;
                afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
                afdDataMdl.usrClaveDestino = (int)afdDataMdl.AFDseguimientoMdl.usrclave;

                dicDatos.Add(RespuestaSer.PARAM_AFDEDODATADML, afdDataMdl);

                //GUARDADMOS LA RESPUESTA Y AVANZAMOS
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<RespuestaSer>(nameof(RespuestaSer.GrabarRespAvanzar), dicDatos);
            }

            return RedirectToAction("BandejaEntrada", "Solicitud");

            // SEIR ANECESARIO SOLO COLOCAR UNA RESPUESTA EN TEXTO.....
        }

        /*********************************************
               RESPUESTA INCOMPETENCIA
        ******************************************** */
        [HttpGet]
        public IActionResult PVincompetencia(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            FrmRespIncompVM incompVM = new FrmRespIncompVM();

            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            incompVM.solclave = solclave;
            incompVM.proclave = proclave;
            incompVM.nodclave = nodclave;
            incompVM.araclave = araclave;
            incompVM.Oper = Oper;

            incompVM.respRespuesta = new SIT_RESP_RESPUESTA();
            incompVM.respGeneral = new SIT_RESP_GRAL();
            if (repclave > 0)
            {
                incompVM.respRespuesta.repclave = repclave;
                incompVM.respRespuesta = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), incompVM.respRespuesta) as SIT_RESP_RESPUESTA;

                incompVM.respGeneral.repclave = repclave;
                incompVM.respGeneral = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_GRALDao>(nameof(SIT_RESP_GRALDao.dmlSelectID), incompVM.respGeneral) as SIT_RESP_GRAL;

                if (incompVM.respRespuesta.docclave != null)
                {
                    incompVM.respDoc = new DocContenidoMdl();
                    incompVM.respDoc.docclave = (long)incompVM.respRespuesta.docclave;

                    SIT_DOC_DOCUMENTO docAux = _sitDmlDbSer.operEjecutarTransaccion<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), incompVM.respDoc) as SIT_DOC_DOCUMENTO;
                    incompVM.respDoc.docclave = docAux.docclave;
                    incompVM.respDoc.docnombre = docAux.docnombre;
                }
            }
            else
            {
                incompVM.respRespuesta.repclave = repclave;
            }

            ViewBag.controlName = "Respuesta";
            return View(incompVM);
        }

        [HttpPost]
        public IActionResult PVincompetencia(FrmRespIncompVM incompVM, IFormFile docResolucion)
        {
            if (docResolucion != null)
            {
                incompVM.respDoc = DocumentoConvertir(docResolucion, incompVM.respRespuesta.repoficio);
            }

            incompVM.respRespuesta.rtpclave = Constantes.Respuesta.INCOMPETENCIA_TOTAL;
            incompVM.respRespuesta.repedofec = DateTime.Now;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = incompVM.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, incompVM.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(incompVM.solfecsol));

            // DATOS DE LA RESPUESTA..
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, incompVM.respRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_GENERAL, incompVM.respGeneral);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, incompVM.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, incompVM.respDoc);
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_GENERAL);
            dicDatos.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            long oResultado = 0;

            if (incompVM.avanzar == 0)
            {
                // SOLO GUARDAMOS EL ITEM
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmRegistro), dicDatos);
            }
            else
            {
                // AQUI OBTENEMOS LOS DATOS DEL NODO
                AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
                afdDataMdl = NodoActualIni(incompVM.solclave, (int)incompVM.respRespuesta.rtpclave, incompVM.nodclave);
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.oRespRespuesta = incompVM.respRespuesta;
                afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
                afdDataMdl.usrClaveDestino = (int)afdDataMdl.AFDseguimientoMdl.usrclave;

                dicDatos.Add(RespuestaSer.PARAM_AFDEDODATADML, afdDataMdl);

                //GUARDADMOS LA RESPUESTA Y AVANZAMOS
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<RespuestaSer>(nameof(RespuestaSer.GrabarRespAvanzar), dicDatos);
            }
            return RedirectToAction("BandejaEntrada", "Solicitud");            
        }


        /*********************************************
               RESPUESTA INCOMPETENCIA AREA 
        ******************************************** */        
        [HttpGet]
        public IActionResult PVincompAreaParcial(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            FrmRespIncompVM incompVM = new FrmRespIncompVM();

            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            incompVM.solclave = solclave;
            incompVM.proclave = proclave;
            incompVM.nodclave = nodclave;
            incompVM.araclave = araclave;
            incompVM.Oper = Oper;

            incompVM.respRespuesta = new SIT_RESP_RESPUESTA();
            incompVM.respGeneral = new SIT_RESP_GRAL();
            if (repclave > 0)
            {
                incompVM.respRespuesta.repclave = repclave;
                incompVM.respRespuesta = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), incompVM.respRespuesta) as SIT_RESP_RESPUESTA;

                incompVM.respGeneral.repclave = repclave;
                incompVM.respGeneral = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_GRALDao>(nameof(SIT_RESP_GRALDao.dmlSelectID), incompVM.respGeneral) as SIT_RESP_GRAL;

                if (incompVM.respRespuesta.docclave != null)
                {
                    incompVM.respDoc = new DocContenidoMdl();
                    incompVM.respDoc.docclave = (long)incompVM.respRespuesta.docclave;

                    SIT_DOC_DOCUMENTO docAux = _sitDmlDbSer.operEjecutarTransaccion<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), incompVM.respDoc) as SIT_DOC_DOCUMENTO;
                    incompVM.respDoc.docclave = docAux.docclave;
                    incompVM.respDoc.docnombre = docAux.docnombre;
                }
            }
            else
            {
                incompVM.respRespuesta.repclave = repclave;
            }

            ViewBag.controlName = "Respuesta";
            return View(incompVM);
        }


        [HttpGet]
        public IActionResult PVincompArea(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            FrmRespIncompVM incompVM = new FrmRespIncompVM();

            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            incompVM.solclave = solclave;
            incompVM.proclave = proclave;
            incompVM.nodclave = nodclave;
            incompVM.araclave = araclave;
            incompVM.Oper = Oper;

            incompVM.respRespuesta = new SIT_RESP_RESPUESTA();
            incompVM.respGeneral = new SIT_RESP_GRAL();
            if (repclave > 0)
            {
                incompVM.respRespuesta.repclave = repclave;
                incompVM.respRespuesta = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), incompVM.respRespuesta) as SIT_RESP_RESPUESTA;

                incompVM.respGeneral.repclave = repclave;
                incompVM.respGeneral = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_GRALDao>(nameof(SIT_RESP_GRALDao.dmlSelectID), incompVM.respGeneral) as SIT_RESP_GRAL;

                if (incompVM.respRespuesta.docclave != null)
                {
                    incompVM.respDoc = new DocContenidoMdl();
                    incompVM.respDoc.docclave = (long)incompVM.respRespuesta.docclave;

                    SIT_DOC_DOCUMENTO docAux = _sitDmlDbSer.operEjecutarTransaccion<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), incompVM.respDoc) as SIT_DOC_DOCUMENTO;
                    incompVM.respDoc.docclave = docAux.docclave;
                    incompVM.respDoc.docnombre = docAux.docnombre;
                }
            }
            else
            {
                incompVM.respRespuesta.repclave = repclave;
            }

            ViewBag.controlName = "Respuesta";
            return View(incompVM);
        }

        [HttpPost]        
        public IActionResult PVincompArea(FrmRespIncompVM incompVM, IFormFile docResolucion)
        {
            if (docResolucion != null)
            {
                incompVM.respDoc = DocumentoConvertir(docResolucion, incompVM.respRespuesta.repoficio);
            }

            // OBTENER ESTE DATO DE LA FORMA DE CAPTURA.....
            // OBTENER ESTE DATO DE LA FORMA DE CAPTURA.....
            // OBTENER ESTE DATO DE LA FORMA DE CAPTURA.....
            // OBTENER ESTE DATO DE LA FORMA DE CAPTURA.....
            // OBTENER ESTE DATO DE LA FORMA DE CAPTURA.....
            ////incompVM.respRespuesta.rtpclave = Constantes.Respuesta.INCOMPETENCIA_TOTAL;
            incompVM.respRespuesta.repedofec = DateTime.Now;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = incompVM.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, incompVM.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(incompVM.solfecsol));

            // DATOS DE LA RESPUESTA..
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, incompVM.respRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_GENERAL, incompVM.respGeneral);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, incompVM.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, incompVM.respDoc);
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_GENERAL);
            dicDatos.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            long oResultado = 0;

            if (incompVM.avanzar == 0)
            {
                // SOLO GUARDAMOS EL ITEM
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmRegistro), dicDatos);
            }
            else
            {
                // AQUI OBTENEMOS LOS DATOS DEL NODO
                AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
                afdDataMdl = NodoActualIni(incompVM.solclave, (int)incompVM.respRespuesta.rtpclave, incompVM.nodclave);
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.oRespRespuesta = incompVM.respRespuesta;
                afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
                afdDataMdl.usrClaveDestino = (int)afdDataMdl.AFDseguimientoMdl.usrclave;

                dicDatos.Add(RespuestaSer.PARAM_AFDEDODATADML, afdDataMdl);

                //GUARDADMOS LA RESPUESTA Y AVANZAMOS
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<RespuestaSer>(nameof(RespuestaSer.GrabarRespAvanzar), dicDatos);
            }
            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

        /*********************************************
               RESPUESTA INCOMPETENCIA PARCIAL
        ******************************************** */
        [HttpGet]
        public IActionResult PVincompParcial(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            ViewBag.controlName = "Respuesta";
            FrmRespIncompVM incompVM = new FrmRespIncompVM();
            SIT_RESP_RESPUESTA respDatos = new SIT_RESP_RESPUESTA();

            incompVM.nodclave = nodclave;
            incompVM.solfecsolticks = solFecTics;


            ////if (repclave > 0)
            ////{
            ////    respDatos.repclave = repclave;
            ////    incompVM.respRespuesta = _sitDmlDbSer.operEjecutar<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), respDatos) as SIT_RESP_RESPUESTA;

            ////    List<SIT_RESP_DATOS> lstRespDatos = _sitDmlDbSer.operEjecutar<SIT_RESP_DATOSDao>(nameof(SIT_RESP_DATOSDao.dmlSelectRespID), repclave) as List<SIT_RESP_DATOS>;
            ////    if (lstRespDatos.Count > 0)
            ////    {
            ////        incompVM.respDatos = lstRespDatos[0];
            ////        incompVM.docDocumento = new SIT_DOC_DOCUMENTO { docclave = (long)incompVM.respDatos.docclave };
            ////        incompVM.docDocumento = _sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), incompVM.docDocumento) as SIT_DOC_DOCUMENTO;
            ////    }
            ////}
            ////else
            ////{
            ////    incompVM.respDatos = new SIT_RESP_DATOS();
            ////}

            return View(incompVM);
        }

        [HttpPost]        
        public IActionResult PVincompParcial(FrmRespIncompVM incompVM, IFormFile docResolucion)
        {
            DocContenidoMdl docContenidoMdl = null;

            //////if (docResolucion != null)
            //////{
            //////    docContenidoMdl = DocumentoConvertir(docResolucion, incompVM.respDatos.dattitulo);
            //////}

            //////if (incompVM.respRespuesta == null)
            //////{
            //////    incompVM.respRespuesta = new SIT_RESP_RESPUESTA();
            //////}

            //////incompVM.respRespuesta.rtpclave = Constantes.Respuesta.INCOMPETENCIA_PARCIAL;
            //////incompVM.respRespuesta.repedofec = DateTime.Now;
            //////incompVM.respDatos.ctpclave = Constantes.RespuestaTipoContenido.CONTENIDO;

            //////Dictionary<int, Tuple<SIT_RESP_DATOS, DocContenidoMdl>> dicRespDatos = new Dictionary<int, Tuple<SIT_RESP_DATOS, DocContenidoMdl>>();
            //////dicRespDatos.Add(Constantes.RespuestaTipoContenido.CONTENIDO, new Tuple<SIT_RESP_DATOS, DocContenidoMdl>(incompVM.respDatos, docContenidoMdl));

            //////Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            //////dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, incompVM.nodclave);
            //////dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(incompVM.solfecsolticks));
            //////dicDatos.Add(ProcesoGralDao.PARAM_DICRESPDOC, dicRespDatos);
            //////dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, incompVM.respRespuesta);
            //////dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            //////dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, incompVM.oper);
            //////dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);

            //////long oResultado = 0;
            //////oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.GrabarRespuestaDic), dicDatos);

            //return Content( " <div id='Resultado'>" + oResultado + "</div>" );
            return null;
        }
        
        /* ********************************************
                RESPUESTA AMPLIACION DE PLAZO
        ******************************************** */
        [HttpGet]
        public IActionResult PVampliacionPlazo(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)
        {
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            ViewBag.controlName = "Respuesta";
            FrmRespAmpPlazoVM ampPlazoVM = new FrmRespAmpPlazoVM();


            ampPlazoVM.solclave = solclave;
            ampPlazoVM.proclave = proclave;
            ampPlazoVM.nodclave = nodclave;
            ampPlazoVM.araclave = araclave;
            ampPlazoVM.Oper = Oper;

            ampPlazoVM.respRespuesta = new SIT_RESP_RESPUESTA();
            ampPlazoVM.respGeneral = new SIT_RESP_GRAL();

            if (repclave > 0)
            {
                ampPlazoVM.respRespuesta.repclave = repclave;
                ampPlazoVM.respRespuesta = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), ampPlazoVM.respRespuesta) as SIT_RESP_RESPUESTA;

                ampPlazoVM.respGeneral.repclave = repclave;
                ampPlazoVM.respGeneral = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_GRALDao>(nameof(SIT_RESP_GRALDao.dmlSelectID), ampPlazoVM.respGeneral) as SIT_RESP_GRAL;

                if (ampPlazoVM.respRespuesta.docclave != null)
                {
                    ampPlazoVM.respDoc = new DocContenidoMdl();
                    ampPlazoVM.respDoc.docclave = (long)ampPlazoVM.respRespuesta.docclave;

                    SIT_DOC_DOCUMENTO docAux = _sitDmlDbSer.operEjecutarTransaccion<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), ampPlazoVM.respDoc) as SIT_DOC_DOCUMENTO;
                    ampPlazoVM.respDoc.docclave = docAux.docclave;
                    ampPlazoVM.respDoc.docnombre = docAux.docnombre;
                }
            }
            else
            {
                ampPlazoVM.respRespuesta.repclave = repclave;
            }

            return View(ampPlazoVM);
        }

        [HttpPost]
        public IActionResult PVampliacionPlazo(FrmRespAmpPlazoVM AmpPlazoVM, IFormFile docResolucion, int Oper, string descripcion)
        {
            if (docResolucion != null)
            {
                AmpPlazoVM.respDoc = DocumentoConvertir(docResolucion, AmpPlazoVM.respRespuesta.repoficio);
            }

            AmpPlazoVM.respRespuesta.rtpclave = Constantes.Respuesta.AMPLIACION_PLAZO;
            AmpPlazoVM.respRespuesta.repedofec = DateTime.Now;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = AmpPlazoVM.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, AmpPlazoVM.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(AmpPlazoVM.solfecsol));

            // DATOS DE LA RESPUESTA..
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, AmpPlazoVM.respRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_GENERAL, AmpPlazoVM.respGeneral);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, AmpPlazoVM.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, AmpPlazoVM.respDoc);
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_GENERAL);
            dicDatos.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            long oResultado = 0;
            if (AmpPlazoVM.avanzar == 0)
            {
                // SOLO GUARDAMOS EL ITEM
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmRegistro), dicDatos);
            }
            else
            {
                // AQUI OBTENEMOS LOS DATOS DEL NODO
                AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
                afdDataMdl = NodoActualIni(AmpPlazoVM.solclave, (int)AmpPlazoVM.respRespuesta.rtpclave, AmpPlazoVM.nodclave);
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.oRespRespuesta = AmpPlazoVM.respRespuesta;
                afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
                afdDataMdl.usrClaveDestino = (int)afdDataMdl.AFDseguimientoMdl.usrclave;

                dicDatos.Add(RespuestaSer.PARAM_AFDEDODATADML, afdDataMdl);

                //GUARDADMOS LA RESPUESTA Y AVANZAMOS
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<RespuestaSer>(nameof(RespuestaSer.GrabarRespAvanzar), dicDatos);
            }
            
            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

        /*********************************************
               RESPUESTA ACLARACION
        ******************************************** */
        [HttpGet]
        public IActionResult PVria(Int64 solclave, int proclave, Int64 nodclave, int araclave, Int64 repclave, Int64 solFecTics, int Oper)            
        {
            FrmRespAclaracionVM aclaracionVM = new FrmRespAclaracionVM();

            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            //FALKT ACOLOCAR EN TODS QUIN ES EL USUARIO ACTUAL........ DE LA RESPUESTA
            aclaracionVM.solclave = solclave;
            aclaracionVM.proclave = proclave;
            aclaracionVM.nodclave = nodclave;
            aclaracionVM.araclave = araclave;
            aclaracionVM.Oper = Oper;

            aclaracionVM.respRespuesta = new SIT_RESP_RESPUESTA();
            aclaracionVM.respGeneral = new SIT_RESP_GRAL();
            if (repclave > 0)
            {
                aclaracionVM.respRespuesta.repclave = repclave;
                aclaracionVM.respRespuesta = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_RESPUESTADao>(nameof(SIT_RESP_RESPUESTADao.dmlSelectID), aclaracionVM.respRespuesta) as SIT_RESP_RESPUESTA;

                aclaracionVM.respGeneral.repclave = repclave;
                aclaracionVM.respGeneral = _sitDmlDbSer.operEjecutarTransaccion<SIT_RESP_GRALDao>(nameof(SIT_RESP_GRALDao.dmlSelectID), aclaracionVM.respGeneral) as SIT_RESP_GRAL;

                if (aclaracionVM.respRespuesta.docclave != null)
                {
                    aclaracionVM.respDoc = new DocContenidoMdl();
                    aclaracionVM.respDoc.docclave = (long)aclaracionVM.respRespuesta.docclave;

                    SIT_DOC_DOCUMENTO docAux = _sitDmlDbSer.operEjecutarTransaccion<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectID), aclaracionVM.respDoc) as SIT_DOC_DOCUMENTO;
                    aclaracionVM.respDoc.docclave = docAux.docclave;
                    aclaracionVM.respDoc.docnombre = docAux.docnombre;
                }
            }            
            else
            {                
                aclaracionVM.respRespuesta.repclave = repclave;
            }
            
            ViewBag.controlName = "Respuesta";
            return View(aclaracionVM);
        }
        
        [HttpPost]
        public IActionResult PVria(FrmRespAclaracionVM aclaracionVM, IFormFile docResolucion)
        {
            if (docResolucion != null)
            {
                aclaracionVM.respDoc = DocumentoConvertir(docResolucion, aclaracionVM.respRespuesta.repoficio);
            }
            
            aclaracionVM.respRespuesta.rtpclave = Constantes.Respuesta.ACLARACION;
            aclaracionVM.respRespuesta.repedofec = DateTime.Now;
            aclaracionVM.respRespuesta.megclave = 0;
            aclaracionVM.respGeneral.repclave = aclaracionVM.respRespuesta.repclave;

            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.nodclave = aclaracionVM.nodclave;
            oNodoResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, aclaracionVM.nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_FECHA, new DateTime(aclaracionVM.solfecsol));

            // DATOS DE LA RESPUESTA..
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_RESPUESTA, aclaracionVM.respRespuesta);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_GENERAL, aclaracionVM.respGeneral);
            dicDatos.Add(ProcesoGralDao.PARAM_RED_NODORESP, oNodoResp);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            dicDatos.Add(ProcesoGralDao.PARAM_RESP_ESTADO, Constantes.RespuestaEstado.PROPUESTA);
            dicDatos.Add(ProcesoGralDao.PARAM_OPERACION, aclaracionVM.Oper);
            dicDatos.Add(ProcesoGralDao.PARAM_DOC_CONTENIDO, aclaracionVM.respDoc);
            dicDatos.Add(ProcesoGralDao.PARAM_ENTIDAD, ProcesoGralDao.PARAM_RESP_GENERAL);
            dicDatos.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            long oResultado = 0;

            if (aclaracionVM.avanzar == 0)
            {
                // SOLO GUARDAMOS EL ITEM
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<ProcesoGralDao>(nameof(ProcesoGralDao.AdmRegistro), dicDatos);
            }
            else
            {
                // AQUI OBTENEMOS LOS DATOS DEL NODO
                AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();
                afdDataMdl = NodoActualIni(aclaracionVM.solclave, (int)aclaracionVM.respRespuesta.rtpclave, aclaracionVM.nodclave);
                afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                afdDataMdl.oRespRespuesta = aclaracionVM.respRespuesta;
                afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
                afdDataMdl.usrClaveDestino = (int)afdDataMdl.AFDseguimientoMdl.usrclave;

                dicDatos.Add(RespuestaSer.PARAM_AFDEDODATADML, afdDataMdl);

                //GUARDADMOS LA RESPUESTA Y AVANZAMOS
                oResultado = (long)_sitDmlDbSer.operEjecutarTransaccion<RespuestaSer>(nameof(RespuestaSer.GrabarRespAvanzar), dicDatos);
            }
            return RedirectToAction("BandejaEntrada", "Solicitud");
        }

       
        /* ********************************************
                    D O C U M E N T O  S       
        ******************************************** */
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

        public List<DocContenidoMdl> DocumentoGestionar(String Oficio, ref ICollection<IFormFile> ArchivosResolucion)
        {
            List<DocContenidoMdl> lstDoc = new List<DocContenidoMdl>();
            SIT_DOC_EXTENSION docTipoMdl = new SIT_DOC_EXTENSION();
            DocContenidoMdl docContenidoMdl = null;
            int iFileExt = 0;

            foreach (var archivoRes in ArchivosResolucion)
            {
                if (archivoRes.Length > 0)
                {
                    Dictionary<string, SIT_DOC_EXTENSION> _dicTipoExt = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DOC_EXTENSION) as Dictionary<string, SIT_DOC_EXTENSION>;
                    string sExtension = archivoRes.FileName.Substring(archivoRes.FileName.LastIndexOf(".") + 1).ToUpper();

                    if (_dicTipoExt.ContainsKey(sExtension) == true)
                    {
                        docTipoMdl = _dicTipoExt[sExtension];
                        iFileExt = docTipoMdl.extclave;
                    }

                    docContenidoMdl = new DocContenidoMdl(0, DateTime.Now, Oficio, archivoRes.FileName, archivoRes.Length,
                             _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO, iFileExt, DateTime.Now, null, null);

                    using (var fileStream = archivoRes.OpenReadStream())
                    using (var ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);
                        docContenidoMdl.doc_contenido = ms.ToArray();
                    }

                }
                else if (Oficio != "")
                {
                    docContenidoMdl = new DocContenidoMdl(0, DateTime.Now, Oficio, " ", 0, " ", iFileExt, DateTime.Now, null, null);
                }

                if (docContenidoMdl != null)
                    lstDoc.Add(docContenidoMdl);
            }
            return lstDoc;
        }
        public DocContenidoMdl DocumentoConvertir(IFormFile ArchivoTurnar, string sOficio)
        {
            if (ArchivoTurnar != null )
            {
                SIT_DOC_EXTENSION docTipoMdl = new SIT_DOC_EXTENSION();
                DocContenidoMdl docContenidoMdl = null;

                Dictionary<string, SIT_DOC_EXTENSION> _dicTipoExt = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DOC_EXTENSION) as Dictionary<string, SIT_DOC_EXTENSION>;
                string sExtension = ArchivoTurnar.FileName.Substring(ArchivoTurnar.FileName.LastIndexOf(".") + 1).ToUpper();
                int iFileExt = 0;

                if (_dicTipoExt.ContainsKey(sExtension) == true)
                {
                    docTipoMdl = _dicTipoExt[sExtension];
                    iFileExt = docTipoMdl.extclave;
                }

                docContenidoMdl = new DocContenidoMdl(0, DateTime.Now, sOficio, ArchivoTurnar.FileName, ArchivoTurnar.Length,
                         _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO, iFileExt, DateTime.Now, null, null);

                using (var fileStream = ArchivoTurnar.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    docContenidoMdl.doc_contenido = ms.ToArray();
                }

                return docContenidoMdl;
            }
            return null;
        }

        [HttpPost]
        public int DocRemoverRespuesta(long repclave, string detclave,  long docclave)
        {
            if (detclave == null)
                detclave = "";

            Dictionary<string, Object> dicParam = new Dictionary<string, Object>();
            dicParam.Add(DButil.SIT_RESP_RESPUESTA_COL.REPCLAVE, repclave);
            dicParam.Add(DButil.SIT_RESP_RESPUESTA_COL.DOCCLAVE, docclave);
            dicParam.Add(DButil.SIT_RESP_DETALLE_COL.DETCLAVE, detclave);
            dicParam.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG));
            dicParam.Add(ProcesoGralDao.PARAM_RUTA_ARCHIVOS, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);

            return (int)_sitDmlDbSer.operEjecutar<ProcesoGralDao>(nameof(ProcesoGralDao.BorrarArchivo), dicParam);
        }
    }
}
