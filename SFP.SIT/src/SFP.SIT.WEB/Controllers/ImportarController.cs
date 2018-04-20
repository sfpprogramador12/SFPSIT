using System;
using System.Collections.Generic;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using SFP.SIT.WEB.Util;
using System.Linq;
using SFP.SIT.WEB.Services;
using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.WEB.Injection;
using System.Threading.Tasks;
using System.Threading;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.IMP;
using SFP.SIT.SERV.Model.SNT;
using SFP.SIT.SERV.Negocio;
using SFP.Persistencia.Model;

namespace SFP.SIT.WEB.Controllers
{
    public class ImportarController : SitBaseCtlr
    {
        public ImportarController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<CatalogoController> logger, 
            IHostingEnvironment app, ICacheWebSIT cacheSIT) 
            : base(memCache, httpContextAccessor, logger, app)
        {
            CfgSharePointMdl spModel = _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl;
        }

        [HttpGet]
        public IActionResult FormatoInfoMex()
        {
            return View();
        }

        // SI LO COLOCAMOS EXISTE UN MENSAJE DE ERRO DEBIDO QUE CONTIENE ASI MISMO ARCHIVOS ZIP..
        // SI LO COLOCAMOS EXISTE UN MENSAJE DE ERRO DEBIDO QUE CONTIENE ASI MISMO ARCHIVOS ZIP..
        // SI LO COLOCAMOS EXISTE UN MENSAJE DE ERRO DEBIDO QUE CONTIENE ASI MISMO ARCHIVOS ZIP..
        // ValidateAntiForgeryToken] 
        [HttpPost]        
        public String VistaPrevia(IFormFile ArchivoInfoMex)
        {
            return ImportarArchivos(ref ArchivoInfoMex, true);
        }
            
        [HttpPost]           
        public async Task<string> Importar(IFormFile ArchivoInfoMex)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(300000);    // Se cancela en 5 minutos.
            return  await Task.Run(() => ImportarArchivos(ref ArchivoInfoMex, false), cts.Token);            
        }

        private string ImportarArchivos(ref IFormFile ArchivoInfoMex, Boolean BVistaPrevia)
        {
            Int32 iUsuarioID = 0;

            FrmImportarUImdl frmImpDatosMdl = new FrmImportarUImdl();
            if (ArchivoInfoMex != null && ArchivoInfoMex.Length > 0)
            {
                iUsuarioID = Convert.ToInt32(User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
                ZipArchive zipArchivo = new ZipArchive(ArchivoInfoMex.OpenReadStream());
                ImportarSer solImpSer = new ImportarSer(
                    iUsuarioID,
                    (Int32) _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.FLUJO),
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DOC_EXTENSION) as Dictionary<string, SIT_DOC_EXTENSION>,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SNT_ESTADO) as Dictionary<int, String>,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SOL_MODOENTREGA) as Dictionary<int, String>,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SNT_MUNICIPIO) as Dictionary<int, String>,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SNT_OCUPACION) as Dictionary<int, String>,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SNT_PAIS) as Dictionary<int, String>,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_SNT_TIPO) as Dictionary<int, String>,
                    _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.CLAVE_INST) as String,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DIA_NO_LABORAL) as Dictionary<Int64, char>,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_RESP_TIPO) as Dictionary<int, String>,
                    _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_RESP_CLASINFO) as Dictionary<int, String>
                );

                FrmImportarMdl impMdl = solImpSer.Importar(zipArchivo, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO, ArchivoInfoMex.FileName, BVistaPrevia);

                if (BVistaPrevia == false)
                {
                    if (impMdl.iVersion == Constantes.Infomex.ARCHIVO_ACLARACION)
                        impMdl = ImportarAclaracion(impMdl, iUsuarioID);
                    else if (impMdl.iVersion == Constantes.Infomex.ARCHIVO_SOLICITUD_VER2)
                        impMdl = ImportarSolicitud(impMdl, iUsuarioID);
                }

                frmImpDatosMdl.iVersion = impMdl.iVersion;
                if (impMdl.dicRegistros != null)
                    frmImpDatosMdl.lstRegistros = impMdl.dicRegistros.Select(kvp => kvp.Value).ToList();

                if (impMdl.dicArchivoDatos != null)
                    frmImpDatosMdl.lstArchivo = impMdl.dicArchivoDatos.Select(kvp => kvp.Key).ToList();

                return JsonTransform.convertJsonNoRecords(frmImpDatosMdl);
            }
            else
                return "";
        }

        private FrmImportarMdl ImportarAclaracion(FrmImportarMdl impMdl, Int32 iUsuarioID)
        {
            Dictionary<string, object> dicParamSol = new Dictionary<string, object>();
            Dictionary<String, Object> dicParam = new Dictionary<String, Object>();
            Dictionary<Int32, AfdNodoFlujo> dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;

            AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();

            afdDataMdl.ID_ClaAfd = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.FLUJO);
            afdDataMdl.ID_AreaInai = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI);
            afdDataMdl.ID_AreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
            afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
            afdDataMdl.usrClaveOrigen = iUsuarioID;
            afdDataMdl.usrClaveDestino = iUsuarioID;
            afdDataMdl.dicDiaNoLaboral = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DIA_NO_LABORAL) as Dictionary<Int64, char>;
            afdDataMdl.lstProcesoPlazos = _memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_PROCESOPLAZOS) as List<SIT_SOL_PROCESOPLAZOS>;
            afdDataMdl.SharePointCxn = _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl;
            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;

            dicParam.Add(AfdServicio.PARAM_FRM_IMPORTAR_MDL, impMdl);  // <-- Aqui vienen los archivos

            dicParamSol.Add(DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE, 0);
            dicParamSol.Add(DButil.SIT_SOL_SOLICITUD_COL.PRCCLAVE, Constantes.ProcesoTipo.SOLICITUD);

            foreach (KeyValuePair<long, object> entry in impMdl.dicRegistros)
            {
                try
                {
                    ImpInfomexArchivoAclMdl regAclaracion = (ImpInfomexArchivoAclMdl)entry.Value;

                    dicParamSol[DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE] = regAclaracion.solclave;
                    dicParamSol[DButil.SIT_SOL_SOLICITUD_COL.PRCCLAVE] = Constantes.ProcesoTipo.SOLICITUD;

                    SIT_SOL_SEGUIMIENTO segMdlSol = _sitDmlDbSer.operEjecutar<SIT_SOL_SEGUIMIENTODao>(nameof(SIT_SOL_SEGUIMIENTODao.dmlSelectSeguimientoPorID), dicParamSol) as SIT_SOL_SEGUIMIENTO;

                    if (segMdlSol == null)
                    {
                        ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Error = 1;
                        ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Mensaje = "La solicitud no existe en el sistema";
                        continue;
                    }

                    if (segMdlSol.segfecfin == null || segMdlSol.segultimonodo == 0)
                    {
                        ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Error = 1;
                        ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Mensaje = "La solicitud sigue en proceso de ejecución";
                        continue;
                    }

                    dicParamSol[DButil.SIT_SOL_SOLICITUD_COL.PRCCLAVE] = Constantes.ProcesoTipo.ACLARACION;
                    SIT_SOL_SEGUIMIENTO segMdlAcl = _sitDmlDbSer.operEjecutar<SIT_SOL_SEGUIMIENTODao>(nameof(SIT_SOL_SEGUIMIENTODao.dmlSelectSeguimientoPorID) , dicParamSol) as SIT_SOL_SEGUIMIENTO;

                    if (segMdlAcl != null)
                    {
                        ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Error = 1;
                        ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Mensaje = "Esta aclaración ya se registró en el sistema";
                        continue;
                    }

                    afdDataMdl.solicitud = _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectID),  dicParamSol) as SIT_SOL_SOLICITUD;
                    // VALORES FIJOS PARA TODOS LOS VALORES DE LA IMPORTACIÓN
                    afdDataMdl.rtpclave = Constantes.Respuesta.RECEPCION_INFO_ADICIONAL;
                    afdDataMdl.ID_EstadoSiguiente = dicAfdFlujo[Constantes.NodoEstado.INAI_RESPUESTA_FINAL].dicAccionEstado[Constantes.Respuesta.TURNAR];
                    afdDataMdl.ID_EstadoActual = Constantes.NodoEstado.INAI_RESPUESTA_FINAL;
                    afdDataMdl.ID_Capa = Constantes.Capa.NIVEL_CERO;
                    afdDataMdl.solClave = regAclaracion.solclave;
                    afdDataMdl.Observacion = regAclaracion.descripcion;
                    afdDataMdl.FechaRecepcion = regAclaracion.fecha_respuesta;

                    afdDataMdl.lstDocContenidoMdl = buscarDoc(impMdl.dicArchivoDatos, afdDataMdl.solicitud.solfolio);

                    ////_sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.OPE_IMPORTAR_ACLARACION),  dicParam);
                    _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);


                    if (_sitDmlDbSer.MsjError != null)
                    {
                        ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Error = 1;
                        ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Mensaje = _sitDmlDbSer.MsjError;
                    }
                }
                catch (Exception ex)
                {
                    ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Error = 1;
                    ((ImpInfomexArchivoAclMdl)impMdl.dicRegistros[entry.Key]).Mensaje = _sitDmlDbSer.MsjError + " " + ex.Message ;
                }

            }
            return impMdl;
        }

        private FrmImportarMdl ImportarSolicitud(FrmImportarMdl impMdl, Int32 iUsuarioID)
        {
            Dictionary<String, Object> dicParam = new Dictionary<String, Object>();            
            Dictionary<Int32, AfdNodoFlujo> dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();

            afdDataMdl.ID_ClaAfd = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.FLUJO);
            afdDataMdl.ID_AreaInai = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI);
            afdDataMdl.ID_AreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
            afdDataMdl.ID_AreaDestino = afdDataMdl.ID_AreaUT;
            afdDataMdl.usrClaveOrigen = iUsuarioID;
            afdDataMdl.usrClaveDestino = iUsuarioID;
            afdDataMdl.dicDiaNoLaboral = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DIA_NO_LABORAL) as Dictionary<Int64, char>;
            afdDataMdl.lstProcesoPlazos = _memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_PROCESOPLAZOS) as List<SIT_SOL_PROCESOPLAZOS>;
            afdDataMdl.SharePointCxn = _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl;
            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
                       
            foreach (KeyValuePair<long, object> entry in impMdl.dicRegistros)
            {
                // Verificar si existe omitir...                
                ImpInfomexArchivoSolMdl regArchivoSol = (ImpInfomexArchivoSolMdl)entry.Value;
                SIT_SOL_SOLICITUD solMdl = _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectIDxFolio), regArchivoSol.solclave) as SIT_SOL_SOLICITUD;

                if (solMdl == null)
                {
                    // VALORES FIJOS PARA TODOS LOS VALORES DE LA IMPORTACIÓN
                    afdDataMdl.FechaRecepcion = DateTime.Now;
                    afdDataMdl.rtpclave = Constantes.Respuesta.ENVIAR;
                    afdDataMdl.ID_PerfilDestino = Constantes.Perfil.UT; 
                    afdDataMdl.ID_EstadoActual = Constantes.NodoEstado.INAI_CREAR_SOLICITUD;
                    afdDataMdl.ID_Capa = Constantes.Capa.NIVEL_CERO;
                    afdDataMdl.AFDnodoActMdl = null;
                    if (regArchivoSol.Error == 0)
                    {
                        /* CREAMOS AL SOLICITANTE */
                        // Debemos de recordar que la clave del usuario es la misma que la clave de la SOLICITUD hasta nuevo aviso.
                        afdDataMdl.solicitante = new SIT_SNT_SOLICITANTE();
                        afdDataMdl.solicitante.paiclave = regArchivoSol.paiclave;
                        afdDataMdl.solicitante.ocuclave = regArchivoSol.ocuClave;
                        afdDataMdl.solicitante.tslclave = regArchivoSol.idtipo;
                        afdDataMdl.solicitante.sntotroniveledu = regArchivoSol.sntotroniveledu;
                        afdDataMdl.solicitante.sntotraocup = regArchivoSol.sntotraocup;
                        afdDataMdl.solicitante.sntniveduc = regArchivoSol.sntniveduc;
                        afdDataMdl.solicitante.sntrepleg = regArchivoSol.sntrepleg;
                        afdDataMdl.solicitante.munclave = regArchivoSol.munclave;
                        afdDataMdl.solicitante.edoclave = regArchivoSol.edoclave;
                        afdDataMdl.solicitante.sntusuario = regArchivoSol.sntusuario;
                        afdDataMdl.solicitante.sntfecnac = regArchivoSol.sntfecnac;
                        afdDataMdl.solicitante.sntsexo = regArchivoSol.sntsexo;
                        afdDataMdl.solicitante.sntciudadext = regArchivoSol.sntciudadext;
                        afdDataMdl.solicitante.sntedoext = regArchivoSol.sntedoext;
                        afdDataMdl.solicitante.sntcorele = regArchivoSol.sntcorele;
                        afdDataMdl.solicitante.snttel = regArchivoSol.snttel;
                        afdDataMdl.solicitante.sntcodpos = regArchivoSol.sntcodpos;
                        afdDataMdl.solicitante.sntcol = regArchivoSol.sntcol;
                        afdDataMdl.solicitante.sntnumint = regArchivoSol.sntnumint;
                        afdDataMdl.solicitante.sntnumext = regArchivoSol.sntnumext;
                        afdDataMdl.solicitante.sntcalle = regArchivoSol.sntcalle;
                        afdDataMdl.solicitante.sntcurp = regArchivoSol.sntcurp;
                        afdDataMdl.solicitante.sntnombre = regArchivoSol.sntnombre;
                        afdDataMdl.solicitante.sntapemat = regArchivoSol.sntapemat;
                        afdDataMdl.solicitante.sntapepat = regArchivoSol.sntapepat;
                        afdDataMdl.solicitante.sntrfc = regArchivoSol.sntrfc;
                        afdDataMdl.solicitante.sntclave = regArchivoSol.solclave;

                        /* CREAMOS AL SOLICITUD */
                        afdDataMdl.solicitud = new SIT_SOL_SOLICITUD();
                        afdDataMdl.solicitud.prcclave = Constantes.ProcesoTipo.SOLICITUD;
                        afdDataMdl.solicitud.solclave = regArchivoSol.solclave;
                        afdDataMdl.solicitud.solfolio = regArchivoSol.solfolio;
                        afdDataMdl.solicitud.solfecrec = regArchivoSol.solfecrec;
                        afdDataMdl.solicitud.solfecrecrev = new DateTime();
                        afdDataMdl.solicitud.solfecacl = new DateTime();
                        afdDataMdl.solicitud.solfecresp = regArchivoSol.solfecresp;
                        afdDataMdl.solicitud.solfecenv = regArchivoSol.solfecenv;
                        afdDataMdl.solicitud.solfecent = regArchivoSol.solfecent;
                        afdDataMdl.solicitud.solfecsol = regArchivoSol.solfecsol;
                        afdDataMdl.solicitud.metclave = regArchivoSol.metclave;
                        afdDataMdl.solicitud.megclave = regArchivoSol.idformaentrega;
                        afdDataMdl.solicitud.sntclave = regArchivoSol.solclave;
                        afdDataMdl.solicitud.sotclave = regArchivoSol.idtiposol;
                        afdDataMdl.solicitud.solnotificado = regArchivoSol.solnotificado;
                        afdDataMdl.solicitud.solmotdesecha = regArchivoSol.solmotdesecha;
                        afdDataMdl.solicitud.solrespclave = regArchivoSol.solrespclave;
                        afdDataMdl.solicitud.solotroderacc = regArchivoSol.us_otroderechoacceso;
                        afdDataMdl.solicitud.soldat = regArchivoSol.soldat;
                        afdDataMdl.solicitud.soldes = regArchivoSol.soldes;
                        afdDataMdl.solicitud.solarcdes = regArchivoSol.solarcdes;
                        afdDataMdl.solicitud.solotromod = regArchivoSol.solotromod;


                        afdDataMdl.lstDocContenidoMdl = buscarDoc(impMdl.dicArchivoDatos, afdDataMdl.solicitud.solfolio);

                        _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);

                        if (_sitDmlDbSer.MsjError != null)
                        {
                            ((ImpInfomexArchivoSolMdl)impMdl.dicRegistros[entry.Key]).Error = 1;
                            ((ImpInfomexArchivoSolMdl)impMdl.dicRegistros[entry.Key]).Mensaje = _sitDmlDbSer.MsjError;
                        }
                    }
                }
                else
                {
                    //Aqui debo de insertar los documentos...
                    AdmDocContNeg docContNeg = new AdmDocContNeg(null, regArchivoSol.solfecsol);
                    int iTotAtchivos = docContNeg.ActualizarDoc(impMdl.dicArchivoDatos, regArchivoSol.solclave.ToString());

                    ((ImpInfomexArchivoSolMdl)impMdl.dicRegistros[entry.Key]).Error = 1;
                    if (iTotAtchivos > 0)
                        ((ImpInfomexArchivoSolMdl)impMdl.dicRegistros[entry.Key]).Mensaje = "La solicitud ya se encuentra en el sistema, sin embargo se actualizó el archivo de solicitud";
                    else
                        ((ImpInfomexArchivoSolMdl)impMdl.dicRegistros[entry.Key]).Mensaje = "La solicitud ya se encuentra en el sistema";
                }
                

            }
            return impMdl;
        }


        private List<DocContenidoMdl> buscarDoc(Dictionary<string, DocContenidoMdl> dicDocContenido, String sFolio)
        {
            List<DocContenidoMdl> lstDocCont = new List<DocContenidoMdl>();

            foreach (KeyValuePair<string, DocContenidoMdl> entry in dicDocContenido)
            {
                if (entry.Key.IndexOf(sFolio) != -1)
                    lstDocCont.Add(entry.Value);
            }

            if (lstDocCont.Count == 0)
                return null;
            else
                return lstDocCont;
        }

    }
}


