﻿using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.AFD.WF2
{                    
    public class EdoPRUDrevisarRespSol2 : AfdNodoBase
    {
        public EdoPRUDrevisarRespSol2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {

        }
        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;


            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.CORREGIR)
            {
                SIT_RED_NODORESP nodoResp = _afdEdoDataMdl.dicAuxRespuesta[ProcesoGralDao.PARAM_RED_NODORESP] as SIT_RED_NODORESP;
                _redNodoRespDao.dmlEditar(nodoResp);

                //revisar el usuario y el area a donde se dirije el nuevo--.-.
                AccionBase(false);

                _respRespDao = new SIT_RESP_RESPUESTADao(_cn, _transaction, _sDataAdapter);

                Dictionary<SIT_RESP_RESPUESTA, SIT_RESP_DETALLE> dicRespDet = _afdEdoDataMdl.dicAuxRespuesta[ProcesoGralDao.PARAM_DIC_RESP_DETALLE] as Dictionary<SIT_RESP_RESPUESTA, SIT_RESP_DETALLE>;
                foreach (KeyValuePair<SIT_RESP_RESPUESTA, SIT_RESP_DETALLE> entry in dicRespDet)
                {
                    Int64 irespClave = (long)_respRespDao.dmlAgregar(entry.Key );
                    SIT_RESP_DETALLEDao oRespDetalle = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);
                    entry.Value.repclave = irespClave;
                    oRespDetalle.dmlAgregar(entry.Value);

                    // al nuevo nodo le agregamos el esado..
                    SIT_RED_NODORESP oNodoRespCorregir = new SIT_RED_NODORESP
                    {
                        repclave = irespClave,
                        nodclave = _afdEdoDataMdl.AFDnodoActMdl.nodclave,
                        sdoclave = Constantes.RespuestaEstado.PROPUESTA
                    };
                    _redNodoRespDao.dmlAgregar(oNodoRespCorregir);
                }

            }


            ////    long repClave = (long)prcGralDao.InsertarRegistro(_afdEdoDataMdl.dicAuxRespuesta);

            ////    _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;
            ////    _afdEdoDataMdl.ID_AreaDestino = _afdEdoDataMdl.ID_AreaInai;
            ////    AccionBase(true);

            ////    // ACTUALIZAMOS LOS DATOS DEL SEGUIMIENTO
            ////    _afdEdoDataMdl.AFDseguimientoMdl.segfecfin = _afdEdoDataMdl.AFDnodoActMdl.nodfeccreacion;
            ////    _afdEdoDataMdl.AFDseguimientoMdl.segultimonodo = _afdEdoDataMdl.AFDnodoActMdl.nodclave;
            ////    _afdEdoDataMdl.AFDseguimientoMdl.segedoproceso = AfdConstantes.PROCESO_ESTADO.TERMINADO;
            ////    _afdEdoDataMdl.AFDseguimientoMdl.usrclave = _afdEdoDataMdl.usrClaveOrigen;

            ////    // SE ACTUALZIA LA ARISTA DE RESPUESTA...
            ////    _afdEdoDataMdl.AFDseguimientoMdl.repclave = repClave;
            ////    _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

            ////    // FINALIZAMOS EL NODOD CREADO CON LA RESPUESTA
            ////    _afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
            ////    return _nodoDao.dmlEditar(_afdEdoDataMdl.AFDnodoActMdl);
            ////}
            ////else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            ////{
            ////    _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.SI;

            ////    //BUSCAMOS QUIEN ES EL QUE ESTA ATENIDNEDO LA SOLICITUD.. PARA CREAR LA BARRERA
            ////    SIT_SOL_SEGUIMIENTO solSegBuscar = new SIT_SOL_SEGUIMIENTO();
            ////    solSegBuscar.solclave = _afdEdoDataMdl.solClave;
            ////    solSegBuscar.prcclave = iClaveProceso;

            ////    SIT_SOL_SEGUIMIENTO segAux = _segDao.dmlSelectID(solSegBuscar);
            ////    _afdEdoDataMdl.AFDseguimientoMdl.usrclave = segAux.usrclave;

            ////    SIT_RED_NODO nodoNvoUTanalizar = ExisteNodo(_afdEdoDataMdl.solClave, Constantes.NodoEstado.UT_SOLICITUD_RECIBIR, (int)segAux.usrclave, _afdEdoDataMdl.ID_Capa + 1);

            ////    // BUSCAR QUIEN ATIENDE LA SOLICUTD.... PARA BUSCAR LE NODO...
            ////    if (nodoNvoUTanalizar == null)
            ////    {
            ////        // CREAR NODO ACTUAL SIGUIENTE DE LA UTanalizar que es la barrera
            ////        nodoNvoUTanalizar = new SIT_RED_NODO
            ////        {
            ////            prcclave = iClaveProceso,
            ////            solclave = _afdEdoDataMdl.solClave,
            ////            araclave = _afdEdoDataMdl.ID_AreaUT,
            ////            nodcapa = _afdEdoDataMdl.ID_Capa + 1,
            ////            nodatendido = AfdConstantes.NODO.INDETERMINADO,
            ////            nedclave = Constantes.NodoEstado.UT_SOLICITUD_RECIBIR,
            ////            nodfeccreacion = _afdEdoDataMdl.FechaRecepcion,
            ////            nodclave = Constantes.General.ID_PENDIENTE,
            ////            usrclave = segAux.usrclave,
            ////            perclave = Constantes.Perfil.UT
            ////        };
            ////        _nodoDao.dmlAgregar(nodoNvoUTanalizar);
            ////    }

            ////    // Aqui me falta agregar el proceso Gral Dao..
            ////    long iRespClave = prcGralDao.AdmRegistro(_afdEdoDataMdl.dicAuxRespuesta);
            ////    _afdEdoDataMdl.repClave = iRespClave;

            ////    return Turnar(_afdEdoDataMdl);
            ////}
            ////else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.ASIGNAR)
            ////{
            ////    _afdEdoDataMdl.ID_EstadoSiguiente = _afdEdoDataMdl.dicAfdFlujo[_afdEdoDataMdl.ID_EstadoActual].dicAccionEstado[_afdEdoDataMdl.rtpclave];
            ////    _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;


            ////    long repClave = (long)prcGralDao.AdmRegistro(_afdEdoDataMdl.dicAuxRespuesta);

            ////    // ACTUALIZAMOS QUE PERSONA LE VA A DAR SEGUIMIENTO
            ////    _afdEdoDataMdl.AFDseguimientoMdl.usrclave = _afdEdoDataMdl.usrClaveDestino;
            ////    _afdEdoDataMdl.ID_AreaDestino = _afdEdoDataMdl.ID_AreaUT;
            ////    AccionBase(true);
            ////}

            return 0;
        }
    }
}