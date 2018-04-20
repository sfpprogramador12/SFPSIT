using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using System;
using System.Data.Common;

namespace SFP.SIT.AFD.MIGRAR
{
    public class EdoUTrecibirSol2 : AfdNodoBase
    {
        public EdoUTrecibirSol2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {
            
        }
        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iArista = _afdEdoDataMdl.rtpclave;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;


            if (iArista == Constantes.Respuesta.INCOMPETENCIA_TOTAL ||
                iArista == Constantes.Respuesta.RECEPCION_INFO_ADICIONAL)
            {
                if (iArista == Constantes.Respuesta.INCOMPETENCIA_TOTAL)
                    _afdEdoDataMdl.rtpclave = Constantes.Respuesta.INCOMPETENCIA_TOTAL;
                else
                    _afdEdoDataMdl.rtpclave = Constantes.Respuesta.RECEPCION_INFO_ADICIONAL;

                _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;

                // ACTUALIZMAOS LOS DATOS DEL SEGUIMIENTO
                _afdEdoDataMdl.AFDseguimientoMdl.segfecfin = _afdEdoDataMdl.AFDnodoActMdl.nodfeccreacion;
                _afdEdoDataMdl.AFDseguimientoMdl.segultimonodo = _afdEdoDataMdl.AFDnodoActMdl.nodclave;
                _afdEdoDataMdl.AFDseguimientoMdl.segedoproceso = AfdConstantes.PROCESO_ESTADO.TERMINADO;
                _afdEdoDataMdl.AFDseguimientoMdl.usrclave = _afdEdoDataMdl.usrClaveOrigen;

                _afdEdoDataMdl.ID_AreaDestino = _afdEdoDataMdl.ID_AreaInai;

                AccionBase(true);

                // SE ACTUALZIA LA ARISTA DE RESPUESTA...
                _afdEdoDataMdl.AFDseguimientoMdl.repclave = _afdEdoDataMdl.ID_ClaAristaActual;
                _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

                // FINALIZAMOS EL NODOD CREADO CON LA RESPUESTA
                _afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
                return _nodoDao.dmlEditar(_afdEdoDataMdl.AFDnodoActMdl);
            }
            else if (iArista == Constantes.Respuesta.TURNAR)
            {
                _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.SI;
                _afdEdoDataMdl.rtpclave= Constantes.Respuesta.TURNAR;

                //BUSCAMOS QUIEN ES EL QUE ESTA ATENIDNEDO LA SOLICITUD.. PARA CREAR LA BARRERA
                SIT_SOL_SEGUIMIENTO solSegBuscar = new SIT_SOL_SEGUIMIENTO();
                solSegBuscar.solclave = _afdEdoDataMdl.solClave;
                solSegBuscar.prcclave = iClaveProceso;

                SIT_SOL_SEGUIMIENTO segAux = _segDao.dmlSelectID(solSegBuscar);
                _afdEdoDataMdl.AFDseguimientoMdl.usrclave = segAux.usrclave;

                SIT_RED_NODO nodoNvoUTanalizar = ExisteNodo(_afdEdoDataMdl.solClave, Constantes.NodoEstado.UT_SOLICITUD_RECIBIR, (int)segAux.usrclave, _afdEdoDataMdl.ID_Capa + 1);

                // BUSCAR QUIEN ATIENDE LA SOLICUTD.... APRA BUSCAR LE NODO...
                if (nodoNvoUTanalizar == null)
                {
                    // CREAR NODO ACTUAL SIGUIENTE DE LA UTanalizar que es la barrera
                    nodoNvoUTanalizar = new SIT_RED_NODO
                    {
                        prcclave = iClaveProceso,
                        solclave = _afdEdoDataMdl.solClave,
                        araclave = _afdEdoDataMdl.ID_AreaUT,
                        nodcapa = _afdEdoDataMdl.ID_Capa + 1,
                        nodatendido = AfdConstantes.NODO.INDETERMINADO,
                        nedclave = Constantes.NodoEstado.UT_SOLICITUD_RECIBIR,
                        nodfeccreacion = _afdEdoDataMdl.FechaRecepcion,
                        nodclave = Constantes.General.ID_PENDIENTE,
                        usrclave = segAux.usrclave
                    };
                    _nodoDao.dmlAgregar(nodoNvoUTanalizar);
                }

                // Aqui me falta agregar el proceso Gral Dao..
                ProcesoGralDao prcGralDao = new ProcesoGralDao(_cn, _transaction, _sDataAdapter);
                prcGralDao.AdmRegistro(_afdEdoDataMdl.dicAuxRespuesta);

                return Turnar(_afdEdoDataMdl);
            }
            else if (iArista == Constantes.Respuesta.TURNAR)
            {
                _afdEdoDataMdl.ID_EstadoSiguiente = _afdEdoDataMdl.dicAfdFlujo[_afdEdoDataMdl.ID_EstadoActual].dicAccionEstado[_afdEdoDataMdl.rtpclave];
                _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;
                _afdEdoDataMdl.rtpclave = Constantes.Respuesta.TURNAR;

                // ACTUALIZMAOS LOS DATOS DEL SEGUIMIENTO
                _afdEdoDataMdl.AFDseguimientoMdl.usrclave = _afdEdoDataMdl.usrClaveDestino;
                _afdEdoDataMdl.ID_AreaDestino = _afdEdoDataMdl.ID_AreaUT;                
                AccionBase(true);

                return null;
            }
            else
            {
                // SE SOLCIITA UJNA AMPLCIACON DE PLAZO

                _afdEdoDataMdl.AFDseguimientoMdl.repclave = _afdEdoDataMdl.rtpclave;
                _afdEdoDataMdl.AFDseguimientoMdl.usrclave = _afdEdoDataMdl.usrClaveOrigen;
                _afdEdoDataMdl.ID_EstadoSiguiente = _afdEdoDataMdl.dicAfdFlujo[_afdEdoDataMdl.ID_EstadoActual].dicAccionEstado[_afdEdoDataMdl.rtpclave];
                return base.AccionBase(false);
            }                   
        }
    }
}