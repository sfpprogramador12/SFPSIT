using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao.RED;
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
    public class EdoINAIacl2 : AfdNodoBase
    {
        public EdoINAIacl2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {

        }

        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            if (_calcularPlazoNeg == null)
                _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_SOL_SEGUIMIENTO_COL.SOLCLAVE, _afdEdoDataMdl.solClave);
            dicParam.Add(DButil.SIT_SOL_SEGUIMIENTO_COL.PRCCLAVE, Constantes.ProcesoTipo.SOLICITUD);

            _afdEdoDataMdl.AFDseguimientoMdl = _segDao.dmlSelectSeguimientoPorID(dicParam) as SIT_SOL_SEGUIMIENTO;

            _afdEdoDataMdl.AFDnodoActMdl = (SIT_RED_NODO)_nodoDao.dmlSelectNodoID(_afdEdoDataMdl.AFDseguimientoMdl.segultimonodo);
            _afdEdoDataMdl.ID_Capa = _afdEdoDataMdl.AFDnodoActMdl.nodcapa + 1;
            _afdEdoDataMdl.rtpclave = Constantes.Respuesta.RECEPCION_INFO_ADICIONAL;

            /* CREAR UN NUEVO SEGUIMIENTO */
            SIT_SOL_SEGUIMIENTO solSegMdl = new SIT_SOL_SEGUIMIENTO { repclave= Constantes.Respuesta.SIN_RESPUESTA,
                segfecestimada= _afdEdoDataMdl.ID_FecEstimada, segultimonodo= 0, segfecini= _afdEdoDataMdl.FechaRecepcion,
                afdclave= _afdEdoDataMdl.ID_ClaAfd, segedoproceso= AfdConstantes.PROCESO_ESTADO.EN_EJECUCION,
                prcclave= Constantes.ProcesoTipo.ACLARACION, segfeccalculo= new DateTime(), segdiasnolab= 0, segmultiple= AfdConstantes.MULTIPLE.NO,
                segfecfin= new DateTime(), segfecamp= new DateTime(), segsemaforocolor= 0, segdiassemaforo= 0, 
                solclave= _afdEdoDataMdl.solClave, usrclave = _afdEdoDataMdl.usrClaveOrigen };

            _segDao.dmlAgregar(solSegMdl);


            // CREAR NODO UT-RECIBIR_SOLICITUD
            SIT_RED_NODO nodoUT = new SIT_RED_NODO { prcclave= Constantes.ProcesoTipo.ACLARACION, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.ID_AreaUT,
                nodcapa= _afdEdoDataMdl.ID_Capa, nodatendido= AfdConstantes.NODO.EN_PROCESO, nodclave= Constantes.NodoEstado.UT_REQ_RIA,
                nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                usrclave = _afdEdoDataMdl.usrClaveDestino};

            _nodoDao.dmlAgregar(nodoUT);
            nodoUT.nodclave = _nodoDao.iSecuencia;

            /* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
            int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(_afdEdoDataMdl.FechaRecepcion, DateTime.Now);
            
            SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA
            {
                arihito = Constantes.RespuestaHito.NO,
                aridiasnat = aiDias[CalcularPlazoNeg.DIAS_NATURALES],
                aridiaslab = aiDias[CalcularPlazoNeg.DIAS_LABORALES],
                arifecenvio = _afdEdoDataMdl.FechaRecepcion,
                ariclave = Constantes.General.ID_PENDIENTE,
                noddestino = nodoUT.nodclave,
                nodorigen = _afdEdoDataMdl.AFDnodoActMdl.nodclave
            };

            _redAristaDao.dmlEditar(aristaMdl);
            aristaMdl.ariclave = _redAristaDao.iSecuencia;

            // NODO ACTUAL EL NUEVO CREADO PARA PROCESAR EL SIGUIENTE
            _afdEdoDataMdl.AFDnodoActMdl = nodoUT;

            // ACTUALIZAR EL REGISTRO DE LA SOLICICTUD CON UNA ACLARACION
            _afdEdoDataMdl.solicitud.solfecacl = _afdEdoDataMdl.FechaRecepcion;
            _afdEdoDataMdl.solicitud.prcclave = Constantes.ProcesoTipo.ACLARACION;

            SIT_SOL_SOLICITUDDao solDao = new SIT_SOL_SOLICITUDDao(_cn, _transaction, _sDataAdapter);
            solDao.dmlEditar(_afdEdoDataMdl.solicitud);

            return _afdEdoDataMdl;
        }
    }
}