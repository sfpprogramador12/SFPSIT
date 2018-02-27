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
    public class EdoUTnotificar2 : AfdNodoBase
    {
        public EdoUTnotificar2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {
        }
        public Object Accion(Object oDatos)
        {
            // MODIFICAR SE DEBE DE REGRESAR LA SOLICITUD AL ÁREA ORIGINAL            
            SIT_RED_NODO nodoActual = null;
            SIT_RED_ARISTA aristaMdl = null;
            Boolean bResultado;

            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;

            SIT_RED_NODO nodoAnterior = _afdEdoDataMdl.AFDnodoActMdl;
            // Finalizamos el proceso
            int? iClaveProceso = _afdEdoDataMdl.solicitud.prcclave;

            if (_afdEdoDataMdl.AFDseguimientoMdl.repclave != Constantes.Respuesta.NOTIFICACION_PRORROGA)
                _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.SI;

            bResultado = AccionBase(true);

            // obtenemos el ultimo nodo de la respuesta
            if (_afdEdoDataMdl.AFDseguimientoMdl.repclave == Constantes.Respuesta.NOTIFICACION_PRORROGA)
            {
                //actualizar el seguimiento para la fecha de notificaion
                _afdEdoDataMdl.AFDseguimientoMdl.segfecamp = _afdEdoDataMdl.FechaRecepcion;
            }
            else
            {
                _afdEdoDataMdl.AFDseguimientoMdl.segedoproceso = AfdConstantes.PROCESO_ESTADO.TERMINADO;
                _afdEdoDataMdl.AFDseguimientoMdl.segultimonodo = _afdEdoDataMdl.AFDnodoActMdl.nodclave;

                List<Int32> lstOmitir = new List<Int32>();
                lstOmitir.Add(_afdEdoDataMdl.ID_AreaCT);
                lstOmitir.Add(_afdEdoDataMdl.ID_AreaUT);
                lstOmitir.Add(_afdEdoDataMdl.ID_AreaUTN);
                lstOmitir.Add(_afdEdoDataMdl.ID_AreaInai);  // El perfil y el area tienen el mismo valor..

                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add(DButil.SIT_RED_NODO_COL.SOLCLAVE, _afdEdoDataMdl.solClave);
                dicParam.Add(DButil.SIT_RED_NODO_COL.PRCCLAVE, _afdEdoDataMdl.solicitud.prcclave);
                dicParam.Add(SIT_RED_NODODao.COL_AREAS_EXCLUIR, lstOmitir);

                DataTable dtAreasTurnar = _nodoDao.dmlSelectNodosGrafo(dicParam) as DataTable;

                int[] aiDias;

                foreach (DataRow drArea in dtAreasTurnar.Rows)
                {
                    nodoActual = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= Convert.ToInt32(drArea["ka_claarea"]),
                        nodcapa= _afdEdoDataMdl.ID_Capa, nodatendido= AfdConstantes.NODO.EN_PROCESO, nodclave= _afdEdoDataMdl.ID_EstadoMensaje,
                        nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                        usrclave = _afdEdoDataMdl.usrClaveDestino};

                    _nodoDao.dmlAgregar(nodoActual);
                    nodoActual.nodclave = _nodoDao.iSecuencia;

                    aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoAnterior.nodfeccreacion, nodoActual.nodfeccreacion);
                    aristaMdl = new SIT_RED_ARISTA
                    {
                        arihito = Constantes.RespuestaHito.NO,
                        aridiasnat = aiDias[CalcularPlazoNeg.DIAS_NATURALES],
                        aridiaslab = aiDias[CalcularPlazoNeg.DIAS_LABORALES],
                        arifecenvio = nodoActual.nodfeccreacion,
                        ariclave = Constantes.General.ID_PENDIENTE,
                        noddestino = nodoActual.nodclave,
                        nodorigen = nodoAnterior.nodclave
                    };

                    _redAristaDao.dmlEditar(aristaMdl);
                }
            }

            _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);
            // dejamos al nodo de sistema terminado..
            _afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
            _nodoDao.dmlEditar(_afdEdoDataMdl.AFDnodoActMdl);

            return bResultado;
        }
    }
}