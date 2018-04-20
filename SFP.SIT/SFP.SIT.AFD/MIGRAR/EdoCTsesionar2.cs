using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using System;
using System.Data.Common;

namespace SFP.SIT.AFD.MIGRAR
{
    public class EdoCTsesionar2 : AfdNodoBase
    {        
        public EdoCTsesionar2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {

        }

        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;


            if ( _afdEdoDataMdl.rtpclave == Constantes.Respuesta.CORREGIR)
            {
                // CREAR NODO UT y MARCARLO COMO FINALIZADO
                SIT_RED_NODO nodoUT = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.ID_AreaUT,
                    nodcapa= _afdEdoDataMdl.ID_Capa, nodatendido= AfdConstantes.NODO.FINALIZADO, nodclave= Constantes.NodoEstado.UT_REQ_RIA,
                    nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                    usrclave = _afdEdoDataMdl.usrClaveDestino};
                _nodoDao.dmlAgregar(nodoUT);
                nodoUT.nodclave = _nodoDao.iSecuencia;

                // CREAR ARISTA CT ( Sesionar) --(18)--> UT (requerimiento)
                int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(_afdEdoDataMdl.AFDnodoActMdl.nodfeccreacion, _afdEdoDataMdl.FechaRecepcion);
                SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA { 
                    arihito = Constantes.RespuestaHito.NO,
                    aridiasnat = aiDias[CalcularPlazoNeg.DIAS_NATURALES],
                    aridiaslab = aiDias[CalcularPlazoNeg.DIAS_LABORALES],
                    arifecenvio = _afdEdoDataMdl.AFDnodoActMdl.nodfeccreacion, ariclave = Constantes.General.ID_PENDIENTE,
                    noddestino = nodoUT.nodclave, nodorigen = _afdEdoDataMdl.AFDnodoActMdl.nodclave };

                _redAristaDao.dmlEditar(aristaMdl);
                aristaMdl.ariclave = _redAristaDao.iSecuencia;

                // CREAR NODO UA PARA REQUERIMIENTO
                SIT_RED_NODO nodoUA = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.ID_AreaDestino,
                    nodcapa= _afdEdoDataMdl.ID_Capa, nodatendido= AfdConstantes.NODO.EN_PROCESO, nodclave= Constantes.NodoEstado.SOLICITAR_RIA,
                    nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                    usrclave = _afdEdoDataMdl.usrClaveDestino};

                _nodoDao.dmlAgregar(nodoUA);
                nodoUA.nodclave = _nodoDao.iSecuencia;

                // CREAR ARISTA UT ( requerimiento) --(18)--> UA (requerimiento)
                aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoUA.nodfeccreacion, _afdEdoDataMdl.FechaRecepcion);
                SIT_RED_ARISTA aristaNvoMdl = new SIT_RED_ARISTA
                {
                    arihito = Constantes.RespuestaHito.NO,
                    aridiasnat = 0,
                    aridiaslab = 0,
                    arifecenvio = _afdEdoDataMdl.FechaRecepcion,
                    ariclave = Constantes.General.ID_PENDIENTE,
                    noddestino = nodoUA.nodclave,
                    nodorigen = nodoUT.nodclave
                };
                _redAristaDao.dmlEditar(aristaNvoMdl);

                /* ACTUALIZAR EL SEGUIMIENTO */
                _afdEdoDataMdl.AFDseguimientoMdl = _calcularPlazoNeg.CalcularSeguimiento((int)_afdEdoDataMdl.solicitud.sotclave,
                    nodoUA.nodfeccreacion, _afdEdoDataMdl.lstProcesoPlazos, _afdEdoDataMdl.AFDseguimientoMdl);

                _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);
            }
            else
            {
                // Creamos una capa nueva para contestar 
                _afdEdoDataMdl.ID_Capa = _afdEdoDataMdl.ID_Capa + 1;
                AccionBase(true);

                /* GUARDAMOS EN EL SEGUIMIENTO LA RESPUESTA INTERNA Y EXTERNA*/
                ////////_afdEdoDataMdl.AFDseguimientoMdl.ariclave = _afdEdoDataMdl.ID_ClaAristaActual;
                ////////_segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

                ////////_afdEdoDataMdl.AFDrescomiteMdl.ariclave = _afdEdoDataMdl.ID_ClaAristaActual;

                //////new SIT_ARISTA_COMITEDao(_cn, _transaction, _sDataAdapter).dmlAgregar(_afdEdoDataMdl.AFDrescomiteMdl);

                // ACUTALIZAMOS EL HITO
                SIT_RED_ARISTA aristaHito = new SIT_RED_ARISTA();
                aristaHito.ariclave = _afdEdoDataMdl.ID_ClaAristaActual;
                aristaHito.arihito = Constantes.RespuestaHito.SI;
                _redAristaDao.dmlUpdateAristaHito(aristaHito);
            }
            return true;
        }
    }
}
