using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SFP.SIT.AFD.MIGRAR
{
    public class EdoUTrecibirRev2 : AfdNodoBase
    {
        public EdoUTrecibirRev2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {

        }
        public Object Accion(Object oDatos)
        {
            Object oResultado = null;
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;

            // ESTE ESTADO TIENE DOS ETAPAS 1) CREAR SU PROPIO ESTADO 2) TURNAR LA ACLARACION
            int iTipoProceso = 0;
            int? iClaveProceso = _afdEdoDataMdl.solicitud.prcclave;

            if (_afdEdoDataMdl.solicitud != null)
            {
                if (_afdEdoDataMdl.solicitud.solfecacl == DateTime.MinValue)
                    iTipoProceso = Constantes.ProcesoTipo.SOLICITUD;
                else
                    iTipoProceso = Constantes.ProcesoTipo.ACLARACION;

                _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;

                // CREAR MI PROPIO ESTADO
                // BUSCAR DATOS PARA PROCESAR LA ACCION                
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add(DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE, _afdEdoDataMdl.solicitud.solclave);
                dicParam.Add(DButil.SIT_SOL_SOLICITUD_COL.PRCCLAVE, iTipoProceso);

                _afdEdoDataMdl.AFDseguimientoMdl = _segDao.dmlSelectSeguimientoPorID(dicParam) as SIT_SOL_SEGUIMIENTO;

                if (_afdEdoDataMdl.AFDseguimientoMdl.segultimonodo > 0)
                {
                    _afdEdoDataMdl.AFDnodoActMdl = _nodoDao.dmlSelectNodoID(_afdEdoDataMdl.AFDseguimientoMdl.segultimonodo) as SIT_RED_NODO;
                    _afdEdoDataMdl.ID_Capa = _afdEdoDataMdl.AFDnodoActMdl.nodcapa + 1;
                    _afdEdoDataMdl.rtpclave = Constantes.Respuesta.RECURSO_REVISION;




                    //////_afdEdoDataMdl.dicAuxRespuesta[ProcesoGralDao.PARAM_LISTA_TURNAR]
                    //////if (_afdEdoDataMdl.lstPersonasTurnar.Count > 1)
                    //////    iMultiple = 1;



                    

                    /* CREAR UN NUEVO SEGUIMIENTO */
                    SIT_SOL_SEGUIMIENTO solSegMdl = new SIT_SOL_SEGUIMIENTO( repclave: Constantes.Respuesta.SIN_RESPUESTA,
                        segfecestimada: _afdEdoDataMdl.ID_FecEstimada, segultimonodo: 0, segfecini: new DateTime(),
                        afdclave: _afdEdoDataMdl.ID_ClaAfd, segedoproceso: AfdConstantes.PROCESO_ESTADO.EN_EJECUCION,
                        prcclave: Constantes.ProcesoTipo.RECURSO_REVISION, segfeccalculo: new DateTime(), segdiasnolab: 0, 
                        segmultiple: _afdEdoDataMdl.AFDseguimientoMdl.segmultiple,
                        segfecfin: new DateTime(), segfecamp: new DateTime(), segsemaforocolor: 0, segdiassemaforo: 0,
                        solclave: _afdEdoDataMdl.solClave, usrclave: _afdEdoDataMdl.usrClaveOrigen);


                    ////DEBEMSO DE CALCULAR EL SEGUIMINETO..
                    ////afdEdoDataMdl = afdDatos;
                    ////CalcularSeguimiento(afdEdoDataMdl.solicitud.sotclave, afdDatos.FechaRecepcion);

                    _segDao.dmlAgregar(solSegMdl);

                    //Actualziamos el seguimiento
                    _afdEdoDataMdl.AFDseguimientoMdl = solSegMdl;

                    // CREAR NODO UT-RECIBIR_SOLICITUD
                    SIT_RED_NODO nodoUT = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.ID_AreaUT,
                         nodcapa= _afdEdoDataMdl.ID_Capa, nodatendido= AfdConstantes.NODO.EN_PROCESO, nodclave= Constantes.NodoEstado.UT_SOLICITUD_RECIBIR,
                         nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                         usrclave = _afdEdoDataMdl.usrClaveDestino};
                    _nodoDao.dmlAgregar(nodoUT);
                    nodoUT.nodclave = _nodoDao.iSecuencia;

                    /* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
                    if (_calcularPlazoNeg == null)
                        _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);

                    int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(_afdEdoDataMdl.FechaRecepcion, DateTime.Now);

                    SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA {  arihito= _afdEdoDataMdl.ID_Hito,
                         aridiasnat= aiDias[CalcularPlazoNeg.DIAS_NATURALES], 
                        aridiaslab= aiDias[CalcularPlazoNeg.DIAS_LABORALES], arifecenvio= DateTime.Now,
                        ariclave= Constantes.General.ID_PENDIENTE,
                        noddestino = nodoUT.nodclave, nodorigen= _afdEdoDataMdl.AFDnodoActMdl.nodclave};


                    _redAristaDao.dmlEditar(aristaMdl);
                    aristaMdl.ariclave = _redAristaDao.iSecuencia;

                    // Guardamos los datos del recurso de revision
                    _afdEdoDataMdl.recRevisionMdl.repclave = aristaMdl.ariclave;
                    new SIT_RESP_RREVISIONDao(_cn, _transaction, _sDataAdapter).dmlAgregar(_afdEdoDataMdl.recRevisionMdl);

                    ////////////Grabamos los documentso en al nueva arista
                    ////////////GrabarDocumentos(aristaMdl.ariclave);
                    ////////////Borramos los docuemntso apra que no se graben nuevamente...

                    ////////////if (_afdEdoDataMdl.lstDocContenidoMdl != null)
                    ////////////    _afdEdoDataMdl.lstDocContenidoMdl.Clear();

                    // ACTUALIZAR EL REGISTRO DE LA SOLICICTUD CON UNA ACLARACION
                    _afdEdoDataMdl.solicitud.solfecrecrev = _afdEdoDataMdl.FechaRecepcion;
                    _afdEdoDataMdl.solicitud.prcclave = Constantes.ProcesoTipo.RECURSO_REVISION;
                    new SIT_SOL_SOLICITUDDao(_cn, _transaction, _sDataAdapter).dmlEditar(_afdEdoDataMdl.solicitud);

                    // AHORA QUE SE HA CREADO EL NODO NUEVO ES NECESARIO CREAR LOS TURNOS..
                    // Y SE EJECUTA LA FUNCION BASE

                    List<Tuple<int, string, int>> lstPersonasTurnar = _afdEdoDataMdl.dicAuxRespuesta[ProcesoGralDao.PARAM_LISTA_TURNAR] as List<Tuple<int, string, int>>;

                    foreach (Tuple<int, string, int> areaTurnar in lstPersonasTurnar)
                    {
                        _afdEdoDataMdl.AFDnodoActMdl = nodoUT;
                        _afdEdoDataMdl.ID_AreaDestino = Convert.ToInt32(areaTurnar.Item1);
                        _afdEdoDataMdl.Observacion = areaTurnar.Item2;
                        _afdEdoDataMdl.ID_PerfilDestino = areaTurnar.Item3;
                        _afdEdoDataMdl.rtpclave = Constantes.Respuesta.TURNAR;
                        _afdEdoDataMdl.ID_EstadoSiguiente = Constantes.NodoEstado.INAI_RECURSO_REVISION;

                        oResultado = AccionBase(true);
                    }
                }
                else
                    return false;
            }
            return true;
        }
    }
}