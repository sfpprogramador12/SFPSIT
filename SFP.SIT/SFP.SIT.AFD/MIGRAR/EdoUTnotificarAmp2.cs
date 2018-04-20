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
    class EdoUTnotificarAmp2 : AfdNodoBase
    {
        public EdoUTnotificarAmp2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {
        }
        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;

            SIT_RED_NODO nodoAnterior = _afdEdoDataMdl.AFDnodoActMdl;            
            nodoAnterior.nodatendido = AfdConstantes.NODO.FINALIZADO;
            _nodoDao.dmlEditar(nodoAnterior);
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            try
            {
                if (_calcularPlazoNeg == null)
                    _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);

                _afdEdoDataMdl.ID_EstadoActual = _afdEdoDataMdl.ID_EstadoSiguiente;

                // CREAR NODO ACTUAL             
                SIT_RED_NODO nodoActual = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.ID_AreaDestino,
                    nodcapa= _afdEdoDataMdl.ID_Capa, nodatendido= AfdConstantes.NODO.FINALIZADO, nodclave= _afdEdoDataMdl.ID_EstadoActual,
                    nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                usrclave = _afdEdoDataMdl.usrClaveDestino};

                _nodoDao.dmlAgregar(nodoActual);
                nodoActual.nodclave = _nodoDao.iSecuencia;

                // NODO ACTUAL EL NUEVO CREADO
                _afdEdoDataMdl.AFDnodoActMdl = nodoActual;

                /* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
                int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoAnterior.nodfeccreacion, nodoActual.nodfeccreacion);
                SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA { arihito= _afdEdoDataMdl.ID_Hito,
                          aridiasnat= aiDias[CalcularPlazoNeg.DIAS_NATURALES], 
                          aridiaslab= aiDias[CalcularPlazoNeg.DIAS_LABORALES], arifecenvio= nodoActual.nodfeccreacion,
                          ariclave= Constantes.General.ID_PENDIENTE,
                          noddestino= nodoActual.nodclave, nodorigen= nodoAnterior.nodclave };



                _redAristaDao.dmlEditar(aristaMdl);
                aristaMdl.ariclave = _redAristaDao.iSecuencia;

                /* ACTUALIZAR EL SEGUIMIENTO */
                _afdEdoDataMdl.AFDseguimientoMdl = _calcularPlazoNeg.CalcularSeguimiento((int)_afdEdoDataMdl.solicitud.sotclave, nodoActual.nodfeccreacion,
                    _afdEdoDataMdl.lstProcesoPlazos, _afdEdoDataMdl.AFDseguimientoMdl);

                _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

                //////GrabarDocumentos(aristaMdl.ariclave);
                _afdEdoDataMdl.ID_ClaAristaActual = aristaMdl.ariclave;
            }
            catch (Exception ex)
            {
                MsjError = ex.ToString();
                throw new Exception("Error en el método AccionCrearNodoAristaSeg " + ex.ToString());
            }
            return true;
        }
    }
}
