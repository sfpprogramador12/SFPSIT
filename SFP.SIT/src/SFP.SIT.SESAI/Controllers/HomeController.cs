using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SFP.SIT.SESAI.Servicio;
using SFP.SIT.SESAI.Models;
using SFP.Persistencia.Servicio;
using SFP.Persistencia.Model;

namespace SFP.SIT.SESAI.Controllers
{
    public class HomeController : Controller
    {
        private DmlDbSer _sitDmlOrigenDbSer;
        private DmlDbSer _sitDmlDestinoDbSer;

        public HomeController(IMemoryCache memCache)
        {
            _sitDmlOrigenDbSer = new DmlDbSer((BaseDbMdl)memCache.Get("BD_ORIGEN"));
            _sitDmlDestinoDbSer = new DmlDbSer((BaseDbMdl)memCache.Get("BD_DESTINO"));            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportarCatalogos()
        {
            SesaiDatosImpMdl datosImpMdl = (SesaiDatosImpMdl) _sitDmlOrigenDbSer.operEjecutar<SesaiSer>(nameof(SesaiSer.ExportarDatosCat ), null);
            
            if (datosImpMdl.Areas.Count > 0)
            {
                object bResultado =  _sitDmlDestinoDbSer.operEjecutarTransaccion<SesaiSer>(nameof(SesaiSer.ImportarDatosCat),  datosImpMdl);
                //object bResultado = _sitDmlDestinoDbSer.operEjecutar<SesaiSer>(nameof(SesaiSer.ImportarDatosCat), datosImpMdl);
            }

            /*
                AQUI DEBO DE CREAR EL PRIMER ORGANIGRAMA 
                AQUI DEBO DE CREAR EL PRIMER ORGANIGRAMA
                AQUI DEBO DE CREAR EL PRIMER ORGANIGRAMA
                AQUI DEBO DE CREAR EL PRIMER ORGANIGRAMA
                AQUI DEBO DE CREAR EL PRIMER ORGANIGRAMA
             
                INSERT INTO SIT_ADM_AREAORG(AGNCLAVE, ARACLAVE, ORGCLAVEREPORTA, ORGORDEN)
                SELECT 15,  ARACLAVE, ARHREPORTA,1 FROM SIT_ADM_AREAHIST WHERE TO_DATE ('2017/07/27', 'yyyy/mm/dd') BETWEEN ARHFECINI AND ARHFECFIN
                commit;
            */


            return View();
        }
        //////[HttpPost]
        //////public ActionResult ImportarSolicitudes()
        //////{
        //////    InfoSFP.App_Start.CacheWeb.LoadStaticCache();

        //////    SesaiSer sesaiSer = new SesaiSer();
        //////    DmlOracleSer oracleSer = new DmlOracleSer(_connString);

        //////    SolicitudImportar solImportar = new SolicitudImportar();
        //////    solImportar._dicDiaNoLaboral = (Dictionary<long, char>)oracleSer.operEjecutar(typeof(SIT_ADM_DIANOLABORALDao), SIT_ADM_DIANOLABORALDao.OPE_SELECT_DICCIONARIO, null);
        //////    solImportar._dicTipoArchivoExt = (Dictionary<string, int>)oracleSer.operEjecutar(typeof(SIT_DOC_EXTENSIONDao), SIT_DOC_EXTENSIONDao.OPE_SELECT_DICCIONARIO, null);
        //////    solImportar._lstUsuarioArea = (List<AdmUsuarioAreaMdl>)oracleSer.operEjecutar(typeof(AdmUsuarioAreaDao), AdmUsuarioAreaDao.OPE_SELECT_LISTA_MDL, null);

        //////    // Buscamos todas las solicitudes a importar 
        //////    Dictionary<string, Object> dicParametros = new Dictionary<string, Object>();
        //////    dicParametros.Add(SesaiDao.COL_AÑO_INI, 2003);
        //////    dicParametros.Add(SesaiDao.COL_AÑO_FIN, 2004);

        //////    DataTable dtSolicitudes = (DataTable)sesaiSer.Operacion(SesaiDao.OPE_SELECT_SOLICITUDES, dicParametros);

        //////    List<BaseOperacion> lstOperacion = new List<BaseOperacion>();
        //////    List<string> lstCmd = new List<string>();
        //////    lstCmd.Add("ALTER SESSION SET NLS_SORT = BINARY");

        //////    foreach (DataRow drSolicitud in dtSolicitudes.Rows)
        //////    {
        //////        // Busco los datos de cada solicitud
        //////        dicParametros.Clear();
        //////        dicParametros.Add(SesaiDao.COL_FOLIO, drSolicitud["NO_FOLIO"].ToString());

        //////        solImportar._sesaiSolicitante = (SesaiDatosMdl)sesaiSer.Operacion(SesaiDao.OPE_SELECT_DATOS_GENERALES, dicParametros);
        //////        solImportar._sesaiSolicitud = (SesaiSolicitudMdl)sesaiSer.Operacion(SesaiDao.OPE_SELECT_SOLICITUD, dicParametros);                
        //////        solImportar._dicSesaiTurnada = (Dictionary<int, SesaiTurnadaMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_TURNADA,  dicParametros);
        //////        solImportar._dicSesaiResolucion = (Dictionary<int, SesaiResolucionMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RESOLUCION,  dicParametros);
        //////        solImportar._dicSesaiDocumento = (Dictionary<int, SesaiDocMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_DOCUMENTOS,  dicParametros);
        //////        solImportar._dicSesaiResComite = (Dictionary<int, SesaiResComiteMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMITE,  dicParametros);
        //////        solImportar._dicSesaiRevForma = (Dictionary<int, SesaiRevFormaMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMSOC,  dicParametros);
        //////        solImportar.procesarSolicitud();

        //////        // Generar  datos para la transaccion
        //////        lstOperacion.Clear();
        //////        lstOperacion.Add(new BaseOperacion(typeof(SntSolicitanteDao), SntSolicitanteDao.OPE_INSERTAR, solImportar._infoSolicitanteMdl));
        //////        lstOperacion.Add(new BaseOperacion(typeof(SolicitudDao), SolicitudDao.OPE_INSERTAR, solImportar._infoSolicitudMdl));
        //////        lstOperacion.Add(new BaseOperacion(typeof(SIT_SOL_SEGUIMIENTODao), DocAristaDao.OPE_INSERTAR, solImportar.solSeg));
        //////        if (solImportar.solSegAclaracion != null)
        //////            lstOperacion.Add(new BaseOperacion(typeof(SIT_SOL_SEGUIMIENTODao), DocAristaDao.OPE_INSERTAR, solImportar.solSegAclaracion));
        //////        lstOperacion.Add(new BaseOperacion(typeof(RedNodoDao), RedNodoDao.OPE_IMPORTAR, solImportar._lstRedNodos));
        //////        lstOperacion.Add(new BaseOperacion(typeof(RedAristaDao), RedAristaDao.OPE_IMPORTAR, solImportar._lstRedAristas));
        //////        lstOperacion.Add(new BaseOperacion(typeof(RedAristaResolucionDao), RedAristaResolucionDao.OPE_IMPORTAR, solImportar._lstRedAristaResolucion));
        //////        lstOperacion.Add(new BaseOperacion(typeof(RedAristaComiteDao), RedAristaComiteDao.OPE_IMPORTAR, solImportar._lstRedAristaComite));
        //////        lstOperacion.Add(new BaseOperacion(typeof(RedAristaRevFormaDao), RedAristaRevFormaDao.OPE_IMPORTAR, solImportar._lstRedAristaRevForma));
        //////        lstOperacion.Add(new BaseOperacion(typeof(DocumentoDao), DocumentoDao.OPE_IMPORTAR, solImportar._lstDocumento));
        //////        lstOperacion.Add(new BaseOperacion(typeof(DocAristaDao), DocAristaDao.OPE_IMPORTAR, solImportar._lstDocArista));

        //////        Boolean bResultado = oracleSer.operEjecutarTransaccion(lstCmd, lstOperacion);
        //////    }
        //////    return View();
        //////}


        [HttpPost]
        public IActionResult ImportarSolicitudesNuevo()
        {
            /////////////// InfoSFP.App_Start.CacheWeb.LoadStaticCache();
            //////////SesaiSer sesaiSer = new SesaiSer();
            //////////DmlOracleSer oracleSer = new DmlOracleSer(_connString);

            //////////// Buscamos todas las solicitudes a importar 
            //////////Dictionary<string, Object> dicParametros = new Dictionary<string, Object>();
            //////////dicParametros.Add(SesaiDao.COL_AÑO_INI, 2003);
            //////////dicParametros.Add(SesaiDao.COL_AÑO_FIN, 2004);

            //////////DataTable dtSolicitudes = (DataTable)sesaiSer.Operacion(SesaiDao.OPE_SELECT_SOLICITUDES, dicParametros);

            //////////List<BaseOperacion> lstOperacion = new List<BaseOperacion>();
            //////////List<string> lstCmd = new List<string>();
            //////////lstCmd.Add("ALTER SESSION SET NLS_SORT = BINARY");

            //Dictionary<string, object> dicParam = new Dictionary<string, object>();
            //dicParam.Add(SolicitudImpSer.VAL_AREA_INAI, CacheWeb.GetAreaINAI());
            //dicParam.Add(SolicitudImpSer.VAL_AREA_UT, CacheWeb.GetAreaUT());
            //dicParam.Add(SolicitudImpSer.VAL_AREA_CI, CacheWeb.GetAreaCI());
            //dicParam.Add(SolicitudImpSer.VAL_FECHA_RECEPCION, DateTime.Now);            
            //dicParam.Add(SolicitudImpSer.TAB_PROCESO_PLAZOS, CacheWeb.GetProcesoPlazos());
            //dicParam.Add(SolicitudImpSer.DIC_AREA_PERFIL, CacheWeb.GetAreaPerfil());
            //dicParam.Add(SolicitudImpSer.DIC_DIA_NO_LABORAL, CacheWeb.GetDicDiaNoLaboral());
            //dicParam.Add(SolicitudImpSer.DIC_DOC_EXTENSION, CacheWeb.GetDicTipoExtension());

            //dicParam.Add(SolicitudImpSer.VAL_AREA_GRAL, 0);
            //dicParam.Add(SolicitudImpSer.VAL_USUARIO_ID, 0);
            //dicParam.Add(SolicitudImpSer.OBJ_ARISTA_RESOLUCION, null);
            //dicParam.Add(SolicitudImpSer.OBJ_SOLICITANTE, "Inicio");
            //dicParam.Add(SolicitudImpSer.OBJ_SOLICITUD, "Inicio");            
            //dicParam.Add(SolicitudImpSer.COL_rtpclave, 0);
            //dicParam.Add(SolicitudImpSer.COL_prcclave, 0);
            //dicParam.Add(SolicitudImpSer.COL_solclave, 0);
            //dicParam.Add(SolicitudImpSer.TBL_ARSE_TURNAR, null);


            // Busco los datos de cada solicitud
            //////////dicParametros.Clear();
            //////////dicParametros.Add(SesaiDao.COL_FOLIO, "Inicio");

            //foreach (DataRow drSolicitud in dtSolicitudes.Rows)
            //{
            //    dicParametros[SesaiDao.COL_FOLIO] = drSolicitud["NO_FOLIO"].ToString();

            //    dicParam[SolicitudImpSer.VAL_AREA_GRAL] = 0;
            //    dicParam[SolicitudImpSer.VAL_USUARIO_ID] = 0;
            //    dicParam[SolicitudImpSer.OBJ_ARISTA_RESOLUCION] = null;
            //    dicParam[SolicitudImpSer.OBJ_SOLICITANTE] = "Inicio";
            //    dicParam[SolicitudImpSer.OBJ_SOLICITUD] = "Inicio";
            //    dicParam[SolicitudImpSer.COL_rtpclave] = 0;
            //    dicParam[SolicitudImpSer.COL_prcclave] = 0;
            //    dicParam[SolicitudImpSer.COL_solclave] = 0;
            //    dicParam[SolicitudImpSer.TBL_ARSE_TURNAR] = null;

            //    dicParam[SolicitudImpSer.OBJ_SESAI_SOLICITANTE] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_DATOS_GENERALES, dicParametros);
            //    dicParam[SolicitudImpSer.OBJ_SESAI_SOLICITUD] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_SOLICITUD, dicParametros);
            //    dicParam[SolicitudImpSer.OBJ_SESAI_TURNADA] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_TURNADA, dicParametros);
            //    dicParam[SolicitudImpSer.OBJ_SESAI_RESOLUCION] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_RESOLUCION, dicParametros);
            //    dicParam[SolicitudImpSer.OBJ_SESAI_DOCUMENTO] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_DOCUMENTOS, dicParametros);
            //    dicParam[SolicitudImpSer.OBJ_SESAI_RESCOMITE] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMITE, dicParametros);
            //    dicParam[SolicitudImpSer.OBJ_SESAI_REVFORMA] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMSOC, dicParametros);

            //    lstOperacion.Add(new BaseOperacion(typeof(SolicitudImpSer), SolicitudImpSer.OPE_IMPORTAR_SESAI, dicParam));
            //    Boolean bResultado = oracleSer.operEjecutarTransaccion(lstCmd, lstOperacion);
            //    lstOperacion.Clear();
            //}


            //////////AfdImpCoreDataMdl impDatosMdl = new AfdImpCoreDataMdl();
            //////////impDatosMdl.AreaInai = CacheWeb.GetAreaINAI();
            //////////impDatosMdl.AreaUT = CacheWeb.GetAreaUT();
            //////////impDatosMdl.AreaCI = CacheWeb.GetAreaCI();
            //////////impDatosMdl.IDUsuario = 0;
            //////////impDatosMdl.IDfolio = 0;
            //////////impDatosMdl.TablaAreaTurnar = null;
            //////////////////////impDatosMdl.FechaRecepcion = DateTime.Now;
            //////////impDatosMdl.TablaProcesoPlazos = CacheWeb.GetProcesoPlazos();
            //////////impDatosMdl.dicAreaPerfil = CacheWeb.GetAreaPerfil();
            //////////impDatosMdl.dicDiaNoLaboral = CacheWeb.GetDicDiaNoLaboral();
            //////////impDatosMdl.dicDocExtension = CacheWeb.GetDicTipoExtension();

            //////////Dictionary<string, object> dicParam = new Dictionary<string, object>();

            //////////foreach (DataRow drSolicitud in dtSolicitudes.Rows)
            //////////{
            //////////    dicParametros[SesaiDao.COL_FOLIO] = drSolicitud["NO_FOLIO"].ToString();

            //////////    //2700007304
            //////////    //2700008104
            //////////    //2700009703

            //////////    ////dicParametros[SesaiDao.COL_FOLIO] = "0002700003503";  "0002700000104"
            //////////    //dicParametros[SesaiDao.COL_FOLIO] = "0002700000103";  NO COMPETENCIA...

            //////////    //dicParametros[SesaiDao.COL_FOLIO] = "0002700039305";
            //////////    //dicParametros[SesaiDao.COL_FOLIO] = "0002700292208";
            //////////    //dicParametros[SesaiDao.COL_FOLIO] = "0002700146905";       // REGRSA NO COMPETENCIA

            //////////    Int64 lFolio = Convert.ToInt64(dicParametros[SesaiDao.COL_FOLIO]);

            //////////    SesaiDatosMdl sesaiSntMdl = (SesaiDatosMdl)sesaiSer.Operacion(SesaiDao.OPE_SELECT_DATOS_GENERALES, dicParametros);

            //////////    SIT_SNT_SOLICITANTE _solicitanteMdl = new SIT_SNT_SOLICITANTE(
            //////////            lFolio, sesaiSntMdl.rfc, sesaiSntMdl.apepat, sesaiSntMdl.apemat, sesaiSntMdl.nombre,
            //////////            sesaiSntMdl.curp, sesaiSntMdl.calle, sesaiSntMdl.numext, sesaiSntMdl.numint, sesaiSntMdl.colonia,
            //////////            sesaiSntMdl.codpos, sesaiSntMdl.telefono, sesaiSntMdl.email, null, null,
            //////////            sesaiSntMdl.sexo, sesaiSntMdl.fecha_nac, null, sesaiSntMdl.idpais, sesaiSntMdl.claest,
            //////////            sesaiSntMdl.clamun, sesaiSntMdl.replegal, null, null, null,
            //////////            sesaiSntMdl.idtipo, sesaiSntMdl.idocupacion);

            //////////    SesaiSolicitudMdl sesaiSolMdl = (SesaiSolicitudMdl)sesaiSer.Operacion(SesaiDao.OPE_SELECT_SOLICITUD, dicParametros);

            //////////    SolicitudMdl _solicitudMdl = new SolicitudMdl(lFolio, sesaiSolMdl.no_folio, sesaiSolMdl.fecha_reg_sfp, sesaiSolMdl.otra_modalidad,
            //////////        sesaiSolMdl.arch_desc, sesaiSolMdl.info_buscar, sesaiSolMdl.otros_datos, sesaiSolMdl.fecha_recepcion_sisi, sesaiSolMdl.fecha_captura_solicitud, new DateTime(), new DateTime(),
            //////////        null, sesaiSolMdl.metclave, 0, null, 0, 1, lFolio, 27, 20,
            //////////        sesaiSolMdl.id_medio_entrega, 1, new DateTime(), Constantes.ProcesoTipo.SOLICITUD, new DateTime());


            //////////    impDatosMdl.solicitante = _solicitanteMdl;
            //////////    impDatosMdl.solicitud = _solicitudMdl;
            //////////    impDatosMdl.sesaiTurnada = (Dictionary<int, SesaiTurnadaMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_TURNADA, dicParametros);
            //////////    impDatosMdl.sesaiResolucion = (Dictionary<int, SesaiResolucionMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RESOLUCION, dicParametros);
            //////////    impDatosMdl.sesaiDocumento = (Dictionary<int, SesaiDocMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_DOCUMENTOS, dicParametros);
            //////////    impDatosMdl.sesaiRecComite = (Dictionary<int, SesaiResComiteMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMITE, dicParametros);
            //////////    impDatosMdl.sesaiResForma = (Dictionary<int, SesaiRevFormaMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMSOC, dicParametros);

            //////////    //////oracleSer.operEjecutar(typeof(AfdImportarSer), AfdImportarSer.OPE_IMPORTAR_SESAI, impDatosMdl);

            //////////    lstOperacion.Add(new BaseOperacion(typeof(AfdImportarSer), AfdImportarSer.OPE_IMPORTAR_SESAI, impDatosMdl));
            //////////    object bResultado = oracleSer.operEjecutarTransaccion(lstCmd, lstOperacion);
            //////////    lstOperacion.Clear();
            //////////}

            return View();
        }



    public IActionResult Error()
        {
            return View();
        }
    }
}


/*
    ESCRIBIR QUERY PARA BORRAR LOS DATOS

delete from info_doc_arista;
delete from info_documento;
delete from info_red_arista_FLUJO;
delete from info_red_arista_revforma;
delete from info_red_arista_comite;
delete from info_red_arista_resolucion;
delete from info_red_arista_recurso;
delete from info_red_arista;
delete from info_red_nodo;
delete from info_sol_seguimiento;
delete from info_solicitud;
delete from INFO_SNT_SOLICITANTE;
commit;


select nodorigen, noddestino, ariclave, rtpclave, ori.ka_claarea as origen, des.ka_claarea as destino
from info_red_arista ari, info_red_nodo ori, info_red_nodo des
where 
ari.solclave = 2700000103
and ori.nodclave = ari.nodorigen
and des.nodclave = ari.noddestino
order by nodorigen;


    2700001304

    ESCRIBIR QUERY PARA BORRAR LOS DATOS

select nodorigen, noddestino, ariclave, rtpclave, ori.ka_claarea as origen, des.ka_claarea as destino
from info_red_arista ari, info_red_nodo ori, info_red_nodo des
where 
ari.solclave = 0002700292208
and ori.nodclave = ari.nodorigen
and des.nodclave = ari.noddestino;




select id_area_origen, re.solrespclave, id_area_destino, nombre
from respuesta res, turnada tu
left join resolucion re on re.id_turnada = tu.id_turnadaant
where res.solrespclave is not null
and res.solrespclave = re.solrespclave
group by id_area_origen, re.solrespclave, id_area_destino, nombre
order by id_area_destino, re.solrespclave;



    Todo va al comite de informacion tener cuidado...

la prroroga como se autoriza..

QUE ONDA CON LA FECHA DE CANCELACION



AL SEGUIMIENTO --> CONVERTIRLO --> EL TIPO DE PROCESO

AJSUTAR LA PARTE DE TURNAR  SEA PAR ARF O CI con base a la base de datos

REVISAR LA ARISTA ACTUAL EN EL OBJETO QEU SE PASA A TODAS LOS ESTADOS.

NODOS FINALES...

*/
