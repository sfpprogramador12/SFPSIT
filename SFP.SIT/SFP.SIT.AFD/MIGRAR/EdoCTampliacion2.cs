using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace SFP.SIT.AFD.MIGRAR
{
    public class EdoCTampliacion2 : AfdNodoBase
    {
        public EdoCTampliacion2(DbConnection cn, DbTransaction transaction, string sDataAdapter) 
            : base(cn, transaction, sDataAdapter)
        {

        }

        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);

            SIT_RED_NODO nodoActual = null;
            SIT_RED_ARISTA aristaMdl = null;

            SIT_RED_NODO nodoAnterior = _afdEdoDataMdl.AFDnodoActMdl;
            int iEdoSig = _afdEdoDataMdl.ID_EstadoSiguiente;    // SiguienteEstado(afdEdoDataMdl.rptclave);

            _afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
            _nodoDao.dmlEditar(_afdEdoDataMdl.AFDnodoActMdl);
            _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;

            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.AMPLIACION_PLAZO)
            {
                // CREAR NUEVO NODO ACTUAL             
                nodoActual = new SIT_RED_NODO
                {
                    prcclave = iClaveProceso,
                    solclave = _afdEdoDataMdl.solClave,
                    araclave = _afdEdoDataMdl.ID_AreaUTN,
                    nodcapa = _afdEdoDataMdl.ID_Capa,
                    nodatendido = AfdConstantes.NODO.EN_PROCESO,
                    nedclave = iEdoSig,
                    nodfeccreacion = _afdEdoDataMdl.FechaRecepcion,
                    nodclave = Constantes.General.ID_PENDIENTE,
                    usrclave = _afdEdoDataMdl.usrClaveDestino
                };

                _nodoDao.dmlEditar(nodoActual);
                nodoActual.nodclave = _nodoDao.iSecuencia;

                /* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
                int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoAnterior.nodfeccreacion, nodoActual.nodfeccreacion);

                aristaMdl = new SIT_RED_ARISTA {  arihito = _afdEdoDataMdl.ID_Hito,
                    aridiasnat = aiDias[CalcularPlazoNeg.DIAS_NATURALES],
                    aridiaslab = aiDias[CalcularPlazoNeg.DIAS_LABORALES], arifecenvio = nodoActual.nodfeccreacion,
                    ariclave = Constantes.General.ID_PENDIENTE,
                    noddestino = nodoActual.nodclave, nodorigen = nodoAnterior.nodclave };

                _redAristaDao.Agregar(aristaMdl);
                aristaMdl.ariclave = _redAristaDao.iSecuencia;
                ////////////GrabarDocumentos(aristaMdl.ariclave);

                // BUSCAR SI EXISTEN OTRAS SOLICITUDES DE AMPLIACION Y CERRARLAS DEBIDO QUE SE AUTORIZO LA AMPLIACION
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add(DButil.SIT_RED_NODO_COL.SOLCLAVE, _afdEdoDataMdl.solClave);
                dicParam.Add(DButil.SIT_RESP_TIPO_COL.RTPCLAVE, Constantes.Respuesta.AMPLIACION_PLAZO);
                dicParam.Add(DButil.SIT_RED_ARISTA_COL.NODDESTINO, _afdEdoDataMdl.AFDnodoActMdl.nodclave);

                List<SIT_RED_NODO> lstSIT_RED_NODO = _nodoDao.dmlSelectMdlAmpPendiente(dicParam) as List<SIT_RED_NODO>;
                foreach (SIT_RED_NODO nodoPendienteMdl in lstSIT_RED_NODO)
                {
                    nodoPendienteMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
                    _nodoDao.dmlEditar(nodoPendienteMdl);

                    aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoPendienteMdl.nodfeccreacion, nodoActual.nodfeccreacion);
                    aristaMdl = new SIT_RED_ARISTA { arihito = _afdEdoDataMdl.ID_Hito,
                        aridiasnat = aiDias[CalcularPlazoNeg.DIAS_NATURALES],
                        aridiaslab = aiDias[CalcularPlazoNeg.DIAS_LABORALES], arifecenvio = nodoActual.nodfeccreacion,
                        ariclave = Constantes.General.ID_PENDIENTE,
                        noddestino = nodoActual.nodclave, nodorigen = nodoPendienteMdl.nodclave };

                    _redAristaDao.dmlAgregar(aristaMdl);

                    ////////GrabarDocumentos(aristaMdl.ariclave);
                }

                // GENERAMOS UN MENSAJE A TODAS LAS DEMAS ÁREAS PENDIENTES
                iEdoSig = dicAccionEstado[Constantes.Respuesta.CREAR_MENSAJE];

                List<Int32> lstOmitir = new List<Int32>();
                lstOmitir.Add(_afdEdoDataMdl.ID_AreaCT);
                lstOmitir.Add(_afdEdoDataMdl.ID_AreaUTN);
                lstOmitir.Add(_afdEdoDataMdl.ID_AreaInai);  // El perfil y el area tienen el mismo valor..

                dicParam.Add(DButil.SIT_RED_NODO_COL.PRCCLAVE, _afdEdoDataMdl.solicitud.prcclave);
                dicParam.Add(SIT_RED_NODODao.COL_AREAS_EXCLUIR, lstOmitir);



                DataTable dtAreasTurnar = _nodoDao.dmlSelectNodosGrafo(dicParam) as DataTable;

                foreach (DataRow drArea in dtAreasTurnar.Rows)
                {
                    nodoActual = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= Convert.ToInt32(drArea["ka_claarea"]),
                        nodcapa= _afdEdoDataMdl.ID_Capa, nodatendido= AfdConstantes.NODO.EN_PROCESO, nodclave= iEdoSig,
                        nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                        usrclave = _afdEdoDataMdl.usrClaveDestino};
                    _nodoDao.dmlAgregar(nodoActual);
                    nodoActual.nodclave = _nodoDao.iSecuencia;

                    aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoAnterior.nodfeccreacion, nodoActual.nodfeccreacion);
                    aristaMdl = new SIT_RED_ARISTA { arihito = _afdEdoDataMdl.ID_Hito,
                        aridiasnat = aiDias[CalcularPlazoNeg.DIAS_NATURALES],
                        aridiaslab = aiDias[CalcularPlazoNeg.DIAS_LABORALES], arifecenvio = nodoActual.nodfeccreacion,
                        ariclave = Constantes.General.ID_PENDIENTE,
                        noddestino = nodoActual.nodclave, nodorigen = nodoAnterior.nodclave };
                    _redAristaDao.dmlAgregar(aristaMdl);
                }

            }
            else
            {
                // MANDAMOS UN MENSAJE AL AREA ANTERIOR NOTIFICANDO LA CAUSA
                iEdoSig = dicAccionEstado[Constantes.RespuestaEstado.NEGAR];

                // CREAR NODO ACTUAL             
                nodoActual = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.ID_AreaDestino,
                    nodcapa= _afdEdoDataMdl.ID_Capa, nodatendido= AfdConstantes.NODO.EN_PROCESO, nodclave= iEdoSig,
                    nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                    usrclave = _afdEdoDataMdl.usrClaveDestino};



                _nodoDao.dmlAgregar(nodoActual);
                nodoActual.nodclave = _nodoDao.iSecuencia;

                /* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
                int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoAnterior.nodfeccreacion, nodoActual.nodfeccreacion);

                aristaMdl = new SIT_RED_ARISTA {  arihito= _afdEdoDataMdl.ID_Hito,
                    aridiasnat= aiDias[CalcularPlazoNeg.DIAS_NATURALES],
                    aridiaslab= aiDias[CalcularPlazoNeg.DIAS_LABORALES],
                    arifecenvio= nodoActual.nodfeccreacion,ariclave= Constantes.General.ID_PENDIENTE,
                    noddestino= nodoActual.nodclave, nodorigen= nodoAnterior.nodclave };


                _redAristaDao.dmlAgregar(aristaMdl);
                aristaMdl.ariclave = _redAristaDao.iSecuencia;
            }
            return true;
        }
    }
}
