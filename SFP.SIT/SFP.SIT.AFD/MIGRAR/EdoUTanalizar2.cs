
using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SFP.SIT.AFD.MIGRAR
{
    class EdoUTanalizar2 : AfdNodoBase
    {
        public EdoUTanalizar2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {

        }
        public Object Accion(Object oDatos)
        {
            // MODIFICAR SE DEBE DE REGRESAR LA SOLICITUD AL ÁREA ORIGINAL
            Boolean bResultado = false;

            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;
            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.CORREGIR)
            {
                // CREAR NODO UT y MARCARLO COMO FINALIZADO 
                SIT_RED_NODO nodoUTnuevo = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.AFDnodoActMdl.araclave,
                    nodcapa= _afdEdoDataMdl.AFDnodoOrigen.nodcapa, nodatendido= AfdConstantes.NODO.FINALIZADO, nodclave= _afdEdoDataMdl.AFDnodoActMdl.nodclave,
                    nodfeccreacion= _afdEdoDataMdl.AFDnodoActMdl.nodfeccreacion, nedclave= Constantes.General.ID_PENDIENTE,
                    usrclave = _afdEdoDataMdl.usrClaveDestino};

                _nodoDao.dmlAgregar(nodoUTnuevo);
                nodoUTnuevo.nodclave = _nodoDao.iSecuencia;

                // ACTUALIZAR LA ARISTA CON BASE AL NUEVO NODO UT
                Dictionary<string, object> dicParam = new Dictionary<string, object>();

                SIT_RED_ARISTA aristaMdl = (SIT_RED_ARISTA)_redAristaDao.dmlSelectAristaID(dicParam);

                aristaMdl.noddestino = nodoUTnuevo.nodclave;
                aristaMdl.arifecenvio = _afdEdoDataMdl.AFDnodoActMdl.nodfeccreacion;
                aristaMdl.arihito = Constantes.RespuestaHito.NO;
                _redAristaDao.dmlEditar(aristaMdl);

                // ACTUALIZAMOS EL NODO  Y CONTINUAMOS..
                _afdEdoDataMdl.AFDnodoActMdl = nodoUTnuevo;
                _afdEdoDataMdl.ID_Capa = nodoUTnuevo.nodcapa;
            }
            else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            {
                // BUSCAR EL NODO
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add(DButil.SIT_RED_NODO_COL.SOLCLAVE, _afdEdoDataMdl.solClave);

                if (_afdEdoDataMdl.solicitud.prcclave == Constantes.ProcesoTipo.SOLICITUD)
                    dicParam.Add(DButil.SIT_RED_NODOESTADO_COL.NEDCLAVE, Constantes.NodoEstado.UT_SOLICITUD_RECIBIR);                    
                else if ( _afdEdoDataMdl.solicitud.prcclave == Constantes.ProcesoTipo.ACLARACION )
                    // VERIFICAR EL PROCESO
                    dicParam.Add(DButil.SIT_RED_NODOESTADO_COL.NEDCLAVE, Constantes.NodoEstado.UT_SOLICITUD_RECIBIR);                    
                else
                    // VERIFICAR EL PROCESO
                    dicParam.Add(DButil.SIT_RED_NODOESTADO_COL.NEDCLAVE, Constantes.NodoEstado.UT_SOLICITUD_RECIBIR); // MAS ADELANTE REVISAR EL SEGUNDO RECURSO DE REVISION


                dicParam.Add(DButil.SIT_SOL_SEGUIMIENTO_COL.PRCCLAVE, _afdEdoDataMdl.solicitud.prcclave);

                _afdEdoDataMdl.AFDnodoActMdl = _nodoDao.dmlSelectNodoFolioEstadoPrc(dicParam) as SIT_RED_NODO;
                _afdEdoDataMdl.ID_Capa = _afdEdoDataMdl.AFDnodoActMdl.nodcapa;
            }


            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.CORREGIR || 
                _afdEdoDataMdl.rtpclave == Constantes.Respuesta.PARA_COMITE || _afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
                _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;
            else
                _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.SI;

            base.AccionBase(true);

            if ( _afdEdoDataMdl.rtpclave != Constantes.Respuesta.TURNAR && 
                _afdEdoDataMdl.rtpclave != Constantes.Respuesta.CORREGIR
                )
            {
                /////////////////&& _afdEdoDataMdl.rtpclave != Constantes.Respuesta.RETURNAR
                _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);
            }

            return bResultado;
        }
    }
}