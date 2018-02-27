using SFP.Persistencia;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Dao.DOC;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Dao.SNT;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model.SNT;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SESAI.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SFP.SIT.SESAI.Servicio
{
    public class SesaiSer : BaseFunc
    {
        public const int OPE_EXPORTAR_DATOS_CAT = 1;

        public SesaiSer(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {

        }        
        
        public Object ExportarDatosCat() {

            SesaiDatosImpMdl datosImpMdl = new SesaiDatosImpMdl();
            SesaiDao sesaiDao = new SesaiDao(_cn, _transaction, _sDataAdapter);

            datosImpMdl.Areas = sesaiDao.dmlSelectAreas() as List<SIT_ADM_AREA>;
            datosImpMdl.AreasTipo = sesaiDao.dmlSelectAreaTipo() as List<SIT_ADM_AREATIPO>;
            datosImpMdl.AreasNivel = sesaiDao.dmlSelectAreasNivel() as List<SIT_ADM_AREANIVEL>;
            datosImpMdl.AreasHist = sesaiDao.dmlSelectAreasHist() as List<SIT_ADM_AREAHIST>;
            datosImpMdl.AreasGestion = sesaiDao.dmlSelectAreaGestion() as List<SIT_ADM_AREAGESTION>;

            datosImpMdl.Perfiles = sesaiDao.dmlSelectPerfil() as List<SIT_ADM_PERFIL>;
            datosImpMdl.Usuarios = sesaiDao.dmlSelectUsuario() as List<SIT_ADM_USUARIO>;
            datosImpMdl.UsrArea = sesaiDao.dmlSelectUsuarioArea() as List<SIT_ADM_USUARIOAREA>;
            datosImpMdl.UserPerfil = sesaiDao.dmlSelectUsuarioPerfil() as List<SIT_ADM_USRPERFIL>;
            datosImpMdl.Modulo = sesaiDao.dmlSelectModulo() as List<SIT_ADM_MODULO>;
            datosImpMdl.PerfilMod = sesaiDao.dmlSelectPerfilModulo() as List<SIT_ADM_PERFILMOD>;
            datosImpMdl.Clases = sesaiDao.dmlSelectClases() as List<SIT_ADM_CLASES>;
            datosImpMdl.PerfilClases = sesaiDao.dmlSelectPerfilClases() as List<SIT_ADM_PERFILCLASES>;
            datosImpMdl.Afd = sesaiDao.dmlSelectRedAfd() as List<SIT_RED_AFD>;

            /* SE AGREGARON LOS SIGUIENTES*/
            datosImpMdl.RespClasInfo = sesaiDao.dmlSelectRespClasInfo() as List<SIT_RESP_CLASINFO>;            
            datosImpMdl.RespTipo = sesaiDao.dmlSelectRespTipo() as List<SIT_RESP_TIPO>;
            datosImpMdl.RespTipoInfo = sesaiDao.dmlSelectRespTipoInfo() as List<SIT_RESP_TIPOINFO>;
            datosImpMdl.RespEstado = sesaiDao.dmlSelectRespEstado() as List<SIT_RESP_ESTADO>;
            ///////////datosImpMdl.RespContenidoTipo = sesaiDao.dmlSelectRespContenidoTipo() as List<SIT_RESP_CONTENIDOTIPO>;
            datosImpMdl.RespMomento = sesaiDao.dmlSelectRespMomento() as List<SIT_RESP_MOMENTO>;
            datosImpMdl.RespTema = sesaiDao.dmlSelectRespTema() as List<SIT_RESP_TEMA> ;
            datosImpMdl.RespReproduccion = sesaiDao.dmlSelectRespReproduccion() as List<SIT_RESP_REPRODUCCION>;

            datosImpMdl.NodoEstado = sesaiDao.dmlSelectNodoEstado() as List<SIT_RED_NODOESTADO>;
            datosImpMdl.PerfilNodo = sesaiDao.dmlSelectPerfilNodo() as List<SIT_ADM_PERFILNODO>;
            datosImpMdl.AfdFlujo = sesaiDao.dmlSelectRedAfdFlujo() as List<SIT_RED_AFDFLUJO>;
            datosImpMdl.Pais = sesaiDao.dmlSelectPais() as List<SIT_SNT_PAIS>;
            datosImpMdl.Estado = sesaiDao.dmlSelectEstado() as List<SIT_SNT_ESTADO>;
            datosImpMdl.Municipio = sesaiDao.dmlSelectMunicipio() as List<SIT_SNT_MUNICIPIO>;
            datosImpMdl.MedioEntrada = sesaiDao.dmlSelectMedioEntrada() as List<SIT_SOL_MEDIOENTRADA>;
            datosImpMdl.Ocupacion = sesaiDao.dmlSelectOcupacion() as List<SIT_SNT_OCUPACION>;

            
            datosImpMdl.TipoSolicitante = sesaiDao.dmlSelectTipoSolicitante() as List<SIT_SNT_SOLICITANTETIPO>;
            datosImpMdl.TipoSolicitud = sesaiDao.dmlSelectTipoSolicitud() as List<SIT_SOL_SOLICITUDTIPO>;
            datosImpMdl.ProcesoMod = sesaiDao.dmlSelectProceso() as List<SIT_SOL_PROCESO>;
            datosImpMdl.ProcesoPlazosMod = sesaiDao.dmlSelectProcesoPlazos() as List<SIT_SOL_PROCESOPLAZOS>;
            datosImpMdl.SolModoEntrega = sesaiDao.dmlSelectTipoModoEntrega() as List<SIT_SOL_MODOENTREGA>;
            datosImpMdl.DiaNoLaboral = sesaiDao.dmlSelectDiaNoLaboral() as List<SIT_ADM_DIANOLABORAL>;
            datosImpMdl.TipoExtension = sesaiDao.dmlSelectTipoExtension() as List<SIT_DOC_EXTENSION>;
            datosImpMdl.ComiteRubro = sesaiDao.dmlSelectComiteRubro() as List<SIT_RESP_COMITERUBRO>;
            datosImpMdl.Configuracion = sesaiDao.dmlSelectConfiguracion() as List<SIT_ADM_CONFIGURACION>;

    
            return datosImpMdl;
        }

        public Object ImportarDatosCat(Object oDatos)
        {
            SesaiDatosImpMdl datosImpMdl = (SesaiDatosImpMdl) oDatos;

            new SesaiDao(_cn, _transaction, _sDataAdapter).dmlCommand();
            new SIT_ADM_AREADao(_cn, _transaction, _sDataAdapter).dmlImportarAux(datosImpMdl.Areas);
            new SIT_ADM_AREATIPODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.AreasTipo);
            new SIT_ADM_AREANIVELDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.AreasNivel);
            new SIT_ADM_AREAHISTDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.AreasHist);
            new SIT_ADM_AREAGESTIONDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.AreasGestion);
            new SIT_ADM_PERFILDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.Perfiles);
            new SIT_ADM_USUARIODao(_cn, _transaction, _sDataAdapter).dmlImportarAux(datosImpMdl.Usuarios);            
            new SIT_ADM_USUARIOAREADao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.UsrArea);
            new SIT_ADM_USRPERFILDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.UserPerfil);
            new SIT_ADM_MODULODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.Modulo);
            new SIT_ADM_PERFILMODDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.PerfilMod);
            new SIT_ADM_CLASESDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.Clases);
            new SIT_ADM_PERFILCLASESDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.PerfilClases);

            new SIT_RED_AFDDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.Afd);

            new SIT_RESP_CLASINFODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.RespClasInfo);
            new SIT_RESP_TIPODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.RespTipo);
            new SIT_RESP_TIPOINFODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.RespTipoInfo);
            new SIT_RESP_ESTADODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.RespEstado);
            ////////new SIT_RESP_CONTENIDOTIPODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.RespContenidoTipo);
            new SIT_RESP_MOMENTODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.RespMomento);
            new SIT_RESP_TEMADao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.RespTema);
            new SIT_RESP_REPRODUCCIONDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.RespReproduccion);

            new SIT_RED_NODOESTADODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.NodoEstado);
            new SIT_ADM_PERFILNODODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.PerfilNodo);
            new SIT_RED_AFDFLUJODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.AfdFlujo);
            new SIT_SNT_PAISDao(_cn, _transaction, _sDataAdapter).dmlImportarAux(datosImpMdl.Pais);
            new SIT_SNT_ESTADODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.Estado);
            new SIT_SNT_MUNICIPIODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.Municipio);
            new SIT_SOL_MEDIOENTRADADao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.MedioEntrada);
            new SIT_SNT_OCUPACIONDao(_cn, _transaction, _sDataAdapter).dmlImportarAux(datosImpMdl.Ocupacion);
            
            new SIT_SNT_SOLICITANTETIPODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.TipoSolicitante);
            new SIT_SOL_SOLICITUDTIPODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.TipoSolicitud);
            new SIT_SOL_PROCESODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.ProcesoMod);
            new SIT_SOL_PROCESOPLAZOSDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.ProcesoPlazosMod);
            new SIT_SOL_MODOENTREGADao(_cn, _transaction, _sDataAdapter).dmlImportarAux(datosImpMdl.SolModoEntrega);
            new SIT_ADM_DIANOLABORALDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.DiaNoLaboral);
            new SIT_DOC_EXTENSIONDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.TipoExtension);
            new SIT_RESP_COMITERUBRODao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.ComiteRubro);
            new SIT_ADM_CONFIGURACIONDao(_cn, _transaction, _sDataAdapter).dmlImportar(datosImpMdl.Configuracion);
            return null;
        }


        ////public void ImportarEstructura()
        ////{

        ////    int iIdxAgnClave = 1;

        ////    DateTime dtFechaRecorre;

        ////    List<SIT_ADM_AREAHIST> lstAreaAnterior = null;
        ////    SIT_ADM_AREAGESTION areGestionMdl;
        ////    List<SIT_ADM_AREAORG> lstAreaOrg = null;

        ////    DateTime dtTempAux = new DateTime(2004, 01, 01);
        ////    areGestionMdl = new SIT_ADM_AREAGESTION { agnclave= iIdxAgnClave, agnfecini= dtTempAux, agnfecfin= dtTempAux, agndescripcion = "Estructura Orgánica SFP del : " + dtTempAux.ToString("dd/MM/yyyy") };

        ////    dtFechaRecorre = new DateTime(2003, 12, 31);

        ////    while (dtFechaRecorre.Year < 2018)
        ////    {
        ////        dtFechaRecorre = dtFechaRecorre.AddDays(1);

        ////        List<SIT_ADM_AREAHIST> lstAreaActual = new SIT_ADM_AREAHISTDao(_cn, _transaction, _sDataAdapter).dmlSelectAreasPeriodo(dtFechaRecorre) as List<SIT_ADM_AREAHIST>;

        ////        if (lstAreaAnterior == null)
        ////            lstAreaAnterior = new List<SIT_ADM_AREAHIST>(lstAreaActual);

        ////        List<Tuple<int, int>> listDiferencia = ListaComparar(lstAreaActual, lstAreaAnterior);

        ////        if (listDiferencia.Count > 0)
        ////        {
        ////            //Asignar la fecha donde cambia el organigrama
        ////            areGestionMdl.agnfecfin = dtFechaRecorre.AddDays(-1);

        ////            //Grabar el registro en la parte de Gestión.
        ////            dmlSerDest.operEjecutar<SIT_ADM_AREAGESTIONDao>(nameof(SIT_ADM_AREAGESTIONDao.dmlAgregar), areGestionMdl);

        ////            //Recorrer la lista y generar un lista comn los objetos a grabar
        ////            lstAreaOrg = new List<SIT_ADM_AREAORG>();

        ////            foreach (SIT_ADM_AREAHIST areaHist in lstAreaAnterior)
        ////            {
        ////                // Aqui buscar el historico....
        ////                Areas areaBuscar = new Areas();
        ////                areaBuscar.Area_Año = dtFechaRecorre.Year;
        ////                areaBuscar.Ct_ID = areaHist.ArhClaveUA;

        ////                areaBuscar = dmlSerOri.operEjecutar<AreasHistDao>(nameof(SIT_ADM_AREADao.dmlSelectPeriodoCT), areaBuscar) as Areas;


        ////                lstAreaOrg.Add(new SIT_ADM_AREAORG(areaHist.ArhReporta, areaHist.AraClave, iIdxAgnClave, 1,
        ////                    areaBuscar.Area_Presupuesto, areaBuscar.Area_Personas, areaBuscar.Area_PersonasGpo, areaBuscar.Area_PresupuestoGpo));
        ////            }

        ////            dmlSerDest.operEjecutar<SIT_ADM_AREAORGDao>(nameof(SIT_ADM_AREAORGDao.dmlImportar), lstAreaOrg);

        ////            iIdxAgnClave++;
        ////            // Creamos el siguiente registro 
        ////            areGestionMdl = new SIT_ADM_AREAGESTION(iIdxAgnClave, dtFechaRecorre, dtTempAux, "Estructura Orgánica SFP del : " + dtFechaRecorre.ToString("dd/MM/yyyy"));
        ////            lstAreaAnterior = new List<SIT_ADM_AREAHIST>(lstAreaActual);
        ////        }
        ////    }

        ////    //CERRAMOS EL ULTIMO ORGANIGRAMA
        ////    areGestionMdl.AgnFecFin = new DateTime(2030, 12, 31);
        ////    dmlSerDest.operEjecutar<SIT_ADM_AREAGESTIONDao>(nameof(SIT_ADM_AREAGESTIONDao.dmlAgregar), areGestionMdl);
        ////    lstAreaOrg = new List<SIT_ADM_AREAORG>();

        ////    foreach (SIT_ADM_AREAHIST areaHist in lstAreaAnterior)
        ////    {
        ////        // Aqui buscar el historico....
        ////        SIT_ADM_AREA areaBuscar = new SIT_ADM_AREA();
        ////        areaBuscar.Area_Año = dtFechaRecorre.Year;
        ////        areaBuscar.Ct_ID = areaHist.ArhClaveUA;

        ////        areaBuscar = dmlSerOri.operEjecutar<SIT_ADM_AREADao>(nameof(SIT_ADM_AREADao.dmlSelectPeriodoCT), areaBuscar) as Areas;


        ////        lstAreaOrg.Add(new SIT_ADM_AREAORG(areaHist.ArhReporta, areaHist.AraClave, iIdxAgnClave, 1,
        ////            areaBuscar.Area_Presupuesto, areaBuscar.Area_Personas, areaBuscar.Area_PersonasGpo, areaBuscar.Area_PresupuestoGpo));

        ////    }
        ////    dmlSerDest.operEjecutar<SIT_ADM_AREAORGDao>(nameof(SIT_ADM_AREAORGDao.dmlImportar), lstAreaOrg);
        ////}


        private List<Tuple<int, int>> ListaComparar(List<SIT_ADM_AREAHIST> Origen, List<SIT_ADM_AREAHIST> Destino)
        {
            List<Tuple<int, int>> lstDiferencia = new List<Tuple<int, int>>();
            Boolean bExiste = false;
            foreach (SIT_ADM_AREAHIST AreaOri in Origen)
            {

                foreach (SIT_ADM_AREAHIST AreaDest in Destino)
                {
                    if (AreaOri.araclave == AreaDest.araclave && AreaOri.arhreporta == AreaDest.arhreporta)
                    {
                        bExiste = true;
                        break;
                    }
                }

                if (bExiste == false)
                {
                    lstDiferencia.Add(new Tuple<int, int>(AreaOri.araclave, AreaOri.arhreporta));
                }
                bExiste = false;
            }

            return lstDiferencia;
        }
    }


    /// IMPORTAR SOLCITUDES
    ////////public ActionResult ImportarSolicitudesNuevo()
    ////////{
    ////////    /////////////// InfoSFP.App_Start.CacheWeb.LoadStaticCache();
    ////////    SesaiSer sesaiSer = new SesaiSer();
    ////////    DmlOracleSer oracleSer = new DmlOracleSer(_connString);

    ////////    // Buscamos todas las solicitudes a importar 
    ////////    Dictionary<string, Object> dicParametros = new Dictionary<string, Object>();
    ////////    dicParametros.Add(SesaiDao.COL_AÑO_INI, 2003);
    ////////    dicParametros.Add(SesaiDao.COL_AÑO_FIN, 2004);

    ////////    DataTable dtSolicitudes = (DataTable)sesaiSer.Operacion(SesaiDao.OPE_SELECT_SOLICITUDES, dicParametros);

    ////////    List<BaseOperacion> lstOperacion = new List<BaseOperacion>();
    ////////    List<string> lstCmd = new List<string>();
    ////////    lstCmd.Add("ALTER SESSION SET NLS_SORT = BINARY");

    ////////    //Dictionary<string, object> dicParam = new Dictionary<string, object>();
    ////////    //dicParam.Add(SolicitudImpSer.VAL_AREA_INAI, CacheWeb.GetAreaINAI());
    ////////    //dicParam.Add(SolicitudImpSer.VAL_AREA_UT, CacheWeb.GetAreaUT());
    ////////    //dicParam.Add(SolicitudImpSer.VAL_AREA_CI, CacheWeb.GetAreaCI());
    ////////    //dicParam.Add(SolicitudImpSer.VAL_FECHA_RECEPCION, DateTime.Now);            
    ////////    //dicParam.Add(SolicitudImpSer.TAB_PROCESO_PLAZOS, CacheWeb.GetProcesoPlazos());
    ////////    //dicParam.Add(SolicitudImpSer.DIC_AREA_PERFIL, CacheWeb.GetAreaPerfil());
    ////////    //dicParam.Add(SolicitudImpSer.DIC_DIA_NO_LABORAL, CacheWeb.GetDicDiaNoLaboral());
    ////////    //dicParam.Add(SolicitudImpSer.DIC_DOC_EXTENSION, CacheWeb.GetDicTipoExtension());

    ////////    //dicParam.Add(SolicitudImpSer.VAL_AREA_GRAL, 0);
    ////////    //dicParam.Add(SolicitudImpSer.VAL_USUARIO_ID, 0);
    ////////    //dicParam.Add(SolicitudImpSer.OBJ_ARISTA_RESOLUCION, null);
    ////////    //dicParam.Add(SolicitudImpSer.OBJ_SOLICITANTE, "Inicio");
    ////////    //dicParam.Add(SolicitudImpSer.OBJ_SOLICITUD, "Inicio");            
    ////////    //dicParam.Add(SolicitudImpSer.COL_rtpclave, 0);
    ////////    //dicParam.Add(SolicitudImpSer.COL_prcclave, 0);
    ////////    //dicParam.Add(SolicitudImpSer.COL_solclave, 0);
    ////////    //dicParam.Add(SolicitudImpSer.TBL_ARSE_TURNAR, null);


    ////////    // Busco los datos de cada solicitud
    ////////    dicParametros.Clear();
    ////////    dicParametros.Add(SesaiDao.COL_FOLIO, "Inicio");

    ////////    //foreach (DataRow drSolicitud in dtSolicitudes.Rows)
    ////////    //{
    ////////    //    dicParametros[SesaiDao.COL_FOLIO] = drSolicitud["NO_FOLIO"].ToString();

    ////////    //    dicParam[SolicitudImpSer.VAL_AREA_GRAL] = 0;
    ////////    //    dicParam[SolicitudImpSer.VAL_USUARIO_ID] = 0;
    ////////    //    dicParam[SolicitudImpSer.OBJ_ARISTA_RESOLUCION] = null;
    ////////    //    dicParam[SolicitudImpSer.OBJ_SOLICITANTE] = "Inicio";
    ////////    //    dicParam[SolicitudImpSer.OBJ_SOLICITUD] = "Inicio";
    ////////    //    dicParam[SolicitudImpSer.COL_rtpclave] = 0;
    ////////    //    dicParam[SolicitudImpSer.COL_prcclave] = 0;
    ////////    //    dicParam[SolicitudImpSer.COL_solclave] = 0;
    ////////    //    dicParam[SolicitudImpSer.TBL_ARSE_TURNAR] = null;

    ////////    //    dicParam[SolicitudImpSer.OBJ_SESAI_SOLICITANTE] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_DATOS_GENERALES, dicParametros);
    ////////    //    dicParam[SolicitudImpSer.OBJ_SESAI_SOLICITUD] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_SOLICITUD, dicParametros);
    ////////    //    dicParam[SolicitudImpSer.OBJ_SESAI_TURNADA] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_TURNADA, dicParametros);
    ////////    //    dicParam[SolicitudImpSer.OBJ_SESAI_RESOLUCION] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_RESOLUCION, dicParametros);
    ////////    //    dicParam[SolicitudImpSer.OBJ_SESAI_DOCUMENTO] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_DOCUMENTOS, dicParametros);
    ////////    //    dicParam[SolicitudImpSer.OBJ_SESAI_RESCOMITE] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMITE, dicParametros);
    ////////    //    dicParam[SolicitudImpSer.OBJ_SESAI_REVFORMA] = sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMSOC, dicParametros);

    ////////    //    lstOperacion.Add(new BaseOperacion(typeof(SolicitudImpSer), SolicitudImpSer.OPE_IMPORTAR_SESAI, dicParam));
    ////////    //    Boolean bResultado = oracleSer.operEjecutarTransaccion(lstCmd, lstOperacion);
    ////////    //    lstOperacion.Clear();
    ////////    //}


    ////////    AfdImpCoreDataMdl impDatosMdl = new AfdImpCoreDataMdl();
    ////////    impDatosMdl.AreaInai = CacheWeb.GetAreaINAI();
    ////////    impDatosMdl.AreaUT = CacheWeb.GetAreaUT();
    ////////    impDatosMdl.AreaCI = CacheWeb.GetAreaCI();
    ////////    impDatosMdl.IDUsuario = 0;
    ////////    impDatosMdl.IDfolio = 0;
    ////////    impDatosMdl.TablaAreaTurnar = null;
    ////////    ////////////impDatosMdl.FechaRecepcion = DateTime.Now;
    ////////    impDatosMdl.TablaProcesoPlazos = CacheWeb.GetProcesoPlazos();
    ////////    impDatosMdl.dicAreaPerfil = CacheWeb.GetAreaPerfil();
    ////////    impDatosMdl.dicDiaNoLaboral = CacheWeb.GetDicDiaNoLaboral();
    ////////    impDatosMdl.dicDocExtension = CacheWeb.GetDicTipoExtension();

    ////////    Dictionary<string, object> dicParam = new Dictionary<string, object>();


    ////////    foreach (DataRow drSolicitud in dtSolicitudes.Rows)
    ////////    {
    ////////        dicParametros[SesaiDao.COL_FOLIO] = drSolicitud["NO_FOLIO"].ToString();

    ////////        //2700007304
    ////////        //2700008104
    ////////        //2700009703

    ////////        ////dicParametros[SesaiDao.COL_FOLIO] = "0002700003503";  "0002700000104"
    ////////        //dicParametros[SesaiDao.COL_FOLIO] = "0002700000103";  NO COMPETENCIA...

    ////////        //dicParametros[SesaiDao.COL_FOLIO] = "0002700039305";
    ////////        //dicParametros[SesaiDao.COL_FOLIO] = "0002700292208";
    ////////        //dicParametros[SesaiDao.COL_FOLIO] = "0002700146905";       // REGRSA NO COMPETENCIA

    ////////        Int64 lFolio = Convert.ToInt64(dicParametros[SesaiDao.COL_FOLIO]);

    ////////        SesaiDatosMdl sesaiSntMdl = (SesaiDatosMdl)sesaiSer.Operacion(SesaiDao.OPE_SELECT_DATOS_GENERALES, dicParametros);

    ////////        SIT_SNT_SOLICITANTE _solicitanteMdl = new SIT_SNT_SOLICITANTE(
    ////////                lFolio, sesaiSntMdl.rfc, sesaiSntMdl.apepat, sesaiSntMdl.apemat, sesaiSntMdl.nombre,
    ////////                sesaiSntMdl.curp, sesaiSntMdl.calle, sesaiSntMdl.numext, sesaiSntMdl.numint, sesaiSntMdl.colonia,
    ////////                sesaiSntMdl.codpos, sesaiSntMdl.telefono, sesaiSntMdl.email, null, null,
    ////////                sesaiSntMdl.sexo, sesaiSntMdl.fecha_nac, null, sesaiSntMdl.idpais, sesaiSntMdl.claest,
    ////////                sesaiSntMdl.clamun, sesaiSntMdl.replegal, null, null, null,
    ////////                sesaiSntMdl.idtipo, sesaiSntMdl.idocupacion);

    ////////        SesaiSolicitudMdl sesaiSolMdl = (SesaiSolicitudMdl)sesaiSer.Operacion(SesaiDao.OPE_SELECT_SOLICITUD, dicParametros);

    ////////        SolicitudMdl _solicitudMdl = new SolicitudMdl(lFolio, sesaiSolMdl.no_folio, sesaiSolMdl.fecha_reg_sfp, sesaiSolMdl.otra_modalidad,
    ////////            sesaiSolMdl.arch_desc, sesaiSolMdl.info_buscar, sesaiSolMdl.otros_datos, sesaiSolMdl.fecha_recepcion_sisi, sesaiSolMdl.fecha_captura_solicitud, new DateTime(), new DateTime(),
    ////////            null, sesaiSolMdl.metclave, 0, null, 0, 1, lFolio, 27, 20,
    ////////            sesaiSolMdl.id_medio_entrega, 1, new DateTime(), Constantes.ProcesoTipo.SOLICITUD, new DateTime());


    ////////        impDatosMdl.solicitante = _solicitanteMdl;
    ////////        impDatosMdl.solicitud = _solicitudMdl;
    ////////        impDatosMdl.sesaiTurnada = (Dictionary<int, SesaiTurnadaMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_TURNADA, dicParametros);
    ////////        impDatosMdl.sesaiResolucion = (Dictionary<int, SesaiResolucionMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RESOLUCION, dicParametros);
    ////////        impDatosMdl.sesaiDocumento = (Dictionary<int, SesaiDocMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_DOCUMENTOS, dicParametros);
    ////////        impDatosMdl.sesaiRecComite = (Dictionary<int, SesaiResComiteMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMITE, dicParametros);
    ////////        impDatosMdl.sesaiResForma = (Dictionary<int, SesaiRevFormaMdl>)sesaiSer.Operacion(SesaiDao.OPE_SELECT_RES_COMSOC, dicParametros);

    ////////        //////oracleSer.operEjecutar(typeof(AfdImportarSer), AfdImportarSer.OPE_IMPORTAR_SESAI, impDatosMdl);

    ////////        lstOperacion.Add(new BaseOperacion(typeof(AfdImportarSer), AfdImportarSer.OPE_IMPORTAR_SESAI, impDatosMdl));
    ////////        object bResultado = oracleSer.operEjecutarTransaccion(lstCmd, lstOperacion);
    ////////        lstOperacion.Clear();
    ////////    }

    ////////    return View();
    ////////}

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




