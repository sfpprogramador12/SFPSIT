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
    public class EdoUArecrevision2 : AfdNodoBase
    {
        public EdoUArecrevision2(DbConnection cn, DbTransaction transaction, string sDataAdapter) 
            : base(cn, transaction, sDataAdapter)
        {

        }

        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            if (_calcularPlazoNeg == null)
                _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);

            /// FALTA GRABAR LA RESOLUCION DE LA UA...
            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            {
                AccionBase(true);
            }
            else
            {
                // como es una respueta verficamos si existe el nodo siguiente que es la UT.
                SIT_RED_NODO nodoAnt = _afdEdoDataMdl.AFDnodoActMdl;
                nodoAnt.nodatendido = AfdConstantes.NODO.FINALIZADO;
                _nodoDao.dmlEditar(nodoAnt);

                // VERIFICAR SI EXISTE EL NODO SIGUIENTE CT, ya que solo puede existir uno...                
                // el nodo siemrpe existe....

                SIT_RED_NODO nodoActual = ExisteNodo((long)nodoAnt.solclave, _afdEdoDataMdl.ID_EstadoSiguiente, _afdEdoDataMdl.ID_AreaDestino, nodoAnt.nodcapa + 1);
                if (nodoActual == null)
                {
                    // CREAR NODO ACTUAL             

                    nodoActual = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.ID_AreaDestino,
                        nodcapa= _afdEdoDataMdl.ID_Capa + 1, nodatendido= AfdConstantes.NODO.EN_PROCESO, nodclave= _afdEdoDataMdl.ID_EstadoSiguiente,
                        nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                        usrclave = _afdEdoDataMdl.usrClaveDestino};
                    _nodoDao.dmlAgregar(nodoActual);
                    nodoActual.nodclave = _nodoDao.iSecuencia;
                }

                // NODO ACTUAL EL NUEVO CREADO
                _afdEdoDataMdl.AFDnodoActMdl = nodoActual;

                /* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
                int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoAnt.nodfeccreacion, nodoActual.nodfeccreacion);

                SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA { arihito= Constantes.RespuestaHito.SI,
                    aridiasnat= aiDias[CalcularPlazoNeg.DIAS_NATURALES], 
                    aridiaslab= aiDias[CalcularPlazoNeg.DIAS_LABORALES], arifecenvio= nodoActual.nodfeccreacion,
                    ariclave= Constantes.General.ID_PENDIENTE,
                    noddestino= nodoActual.nodclave, nodorigen= nodoAnt.nodclave };

                _redAristaDao.dmlEditar(aristaMdl);
                aristaMdl.ariclave = _redAristaDao.iSecuencia;

                /* ACTUALIZAR EL SEGUIMIENTO */
                _afdEdoDataMdl.AFDseguimientoMdl = _calcularPlazoNeg.CalcularSeguimiento((int)_afdEdoDataMdl.solicitud.sotclave, nodoActual.nodfeccreacion,
                    _afdEdoDataMdl.lstProcesoPlazos, _afdEdoDataMdl.AFDseguimientoMdl);
                _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

                ////////GrabarDocumentos(aristaMdl.ariclave);
            }
            return true;
        }
    }
}