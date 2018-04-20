using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao.SNT;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using System;
using System.Data.Common;

namespace SFP.SIT.AFD.MIGRAR
{
    public class EdoINAIsol2 : AfdNodoBase
    {        
        public EdoINAIsol2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {
        }

        public  Object Accion(Object oDatos)
        {
            Object oResultado = null;
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = Constantes.ProcesoTipo.SOLICITUD;

            oResultado = new SIT_SNT_SOLICITANTEDao(_cn, _transaction, _sDataAdapter).dmlAgregar(_afdEdoDataMdl.solicitante);
            oResultado = new SIT_SOL_SOLICITUDDao(_cn, _transaction, _sDataAdapter).dmlAgregar(_afdEdoDataMdl.solicitud);

            _afdEdoDataMdl.solClave = _afdEdoDataMdl.solicitud.solclave;

            /* CREAMOS EL SEGUIMIENTO DE LA SOLICITUD */

            SIT_SOL_SEGUIMIENTO solSegMdl = new SIT_SOL_SEGUIMIENTO(
                solclave: _afdEdoDataMdl.solClave,
                prcclave: Constantes.ProcesoTipo.SOLICITUD,
                segmultiple: AfdConstantes.MULTIPLE.NO,
                segfecini: new DateTime(_afdEdoDataMdl.solicitud.solfecsol.Ticks),
                segfecfin: new DateTime(),
                segfecestimada: _afdEdoDataMdl.ID_FecEstimada,                
                segfeccalculo: new DateTime(), 
                segfecamp: new DateTime(), 
                segdiassemaforo: 0,
                segdiasnolab: 0,
                segsemaforocolor: 0,
                segultimonodo: 0,
                segedoproceso: AfdConstantes.PROCESO_ESTADO.EN_EJECUCION,
                afdclave: _afdEdoDataMdl.ID_ClaAfd,
                repclave: null,
                usrclave: _afdEdoDataMdl.usrClaveOrigen);


            oResultado = new SIT_SOL_SEGUIMIENTODao(_cn, _transaction, _sDataAdapter).dmlAgregar(solSegMdl);
            _afdEdoDataMdl.AFDseguimientoMdl = solSegMdl;

            /* CREAR NODO INAI CREAR SOLICITUD CERO*/
            SIT_RED_NODO nodoINAI = new SIT_RED_NODO {
                prcclave = iClaveProceso, solclave = _afdEdoDataMdl.solClave,
                araclave = _afdEdoDataMdl.ID_AreaInai, nodcapa = _afdEdoDataMdl.ID_Capa,
                nodatendido = AfdConstantes.NODO.FINALIZADO, nodclave = _afdEdoDataMdl.ID_EstadoActual,
                nodfeccreacion = _afdEdoDataMdl.solicitud.solfecsol,
                nedclave = Constantes.NodoEstado.INAI_SOLICITUD,
                usrclave = _afdEdoDataMdl.usrClaveDestino
            };

            oResultado = _nodoDao.dmlAgregar(nodoINAI);
            nodoINAI.nodclave = _nodoDao.iSecuencia;

            _afdEdoDataMdl.AFDnodoActMdl = nodoINAI;
           //////////////////////////////////////////// _afdEdoDataMdl.ID_EstadoActual = Constantes.NodoEstado.UT_RECIBIR_SOL;

            // Aqui voy a crear el Nodo de la UT
            _afdEdoDataMdl.ID_Capa = _afdEdoDataMdl.ID_Capa + 1;

            //GRABAMOS los archivos que vienen en la solicitud
            GrabarDocSol(_afdEdoDataMdl.solicitud.solclave);

            return AccionBase(true);
        }
    }
}