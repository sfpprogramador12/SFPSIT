using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using System;
using System.Data.Common;

namespace SFP.SIT.AFD.WF2
{
    public class EdoUAanalizar2 : AfdNodoBase
    {
        public EdoUAanalizar2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {
        }

        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            /// FALTA GRABAR LA RESOLUCION DE LA UA...
            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            {
                return base.Turnar(_afdEdoDataMdl);
            }
            else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.AMPLIACION_PLAZO)
            {
                // SE CREA UN NUEVO FLUJO DE TRABAJO EN PARALELO
                _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);
                base.AccionBase(false);
            }
            else  // DEFINO UNA RESPUESTA
            {
                return base.AccionBase(true);

                //////_calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);
                //////// como es una respueta verficamos si existe el nodo siguiente que es la UT.
                //////SIT_RED_NODO nodoAnt = _afdEdoDataMdl.AFDnodoActMdl;
                //////nodoAnt.nodatendido = AfdConstantes.NODO.FINALIZADO;
                //////_nodoDao.dmlEditar(nodoAnt);


                //////SIT_SOL_SEGUIMIENTO solSegBuscar = new SIT_SOL_SEGUIMIENTO();
                //////solSegBuscar.solclave = _afdEdoDataMdl.solClave;
                //////solSegBuscar.prcclave = iClaveProceso;

                //////SIT_SOL_SEGUIMIENTO segAux = _segDao.dmlSelectID(solSegBuscar);
                //////_afdEdoDataMdl.AFDseguimientoMdl.usrclave = segAux.usrclave;


                //////_afdEdoDataMdl.ID_EstadoSiguiente = _afdEdoDataMdl.dicAfdFlujo[_afdEdoDataMdl.ID_EstadoActual].dicAccionEstado[_afdEdoDataMdl.rtpclave];

                ////////////// VERIFICAR SI EXISTE EL NODO SIGUIENTE UT, ya que solo puede existir uno...  y actualizar su estado.. ya que no lo veo...              
                //////_afdEdoDataMdl.AFDnodoActMdl = ExisteNodo((int)nodoAnt.solclave, _afdEdoDataMdl.ID_EstadoSiguiente, (int)segAux.usrclave, nodoAnt.nodcapa + 1);
                //////if (_afdEdoDataMdl.AFDnodoActMdl == null)
                //////    throw new System.ArgumentException("NO EXISTE NODO DE BARRERA", "original");

                //////_afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.EN_PROCESO;
                //////_nodoDao.dmlUpdateNodoAtendido(_afdEdoDataMdl.AFDnodoActMdl);


                ///////* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
                //////int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoAnt.nodfeccreacion, DateTime.Now);

                ///////* EN LA ARISTA SE GUARDA LA RESOLUCION DEL AREA */
                //////SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA {arihito= Constantes.RespuestaHito.SI,
                //////    aridiasnat= aiDias[CalcularPlazoNeg.DIAS_NATURALES], 
                //////    aridiaslab= aiDias[CalcularPlazoNeg.DIAS_LABORALES], arifecenvio= nodoAnt.nodfeccreacion,
                //////    ariclave= Constantes.General.ID_PENDIENTE, 
                //////    noddestino= _afdEdoDataMdl.AFDnodoActMdl.nodclave, nodorigen= nodoAnt.nodclave };

                //////_redAristaDao.dmlAgregar(aristaMdl);
                //////aristaMdl.ariclave = _redAristaDao.iSecuencia;

                ///////* ACTUALIZAR EL SEGUIMIENTO */
                //////_afdEdoDataMdl.AFDseguimientoMdl = _calcularPlazoNeg.CalcularSeguimiento((int)_afdEdoDataMdl.solicitud.sotclave, DateTime.Now,
                //////    _afdEdoDataMdl.lstProcesoPlazos, _afdEdoDataMdl.AFDseguimientoMdl);

                ////////////GrabarDocumentos(aristaMdl.ariclave);
            }
            return true;
        }
    }
}
