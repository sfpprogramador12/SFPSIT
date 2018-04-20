using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using System;
using System.Data.Common;

namespace SFP.SIT.AFD.WF2
{
    public class EdoUTrecibirSol2 : AfdNodoBase
    {
        public EdoUTrecibirSol2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {

        }
        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.RESPONDER)
            {                
                return ResponderSol();  

            }
            ////else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            ////{
            ////    // ESTE HAY QUE MODIFICAR...
            ////    // ESTE HAY QUE MODIFICAR...
            ////    // ESTE HAY QUE MODIFICAR...
            ////    // ESTE HAY QUE MODIFICAR...
            ////    // ESTE HAY QUE MODIFICAR...

            ////    _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.SI;

            ////    //BUSCAMOS QUIEN ES EL QUE ESTA ATENIDNEDO LA SOLICITUD.. PARA CREAR LA BARRERA
            ////    SIT_SOL_SEGUIMIENTO solSegBuscar = new SIT_SOL_SEGUIMIENTO();
            ////    solSegBuscar.solclave = _afdEdoDataMdl.solClave;
            ////    solSegBuscar.prcclave = iClaveProceso;

            ////    SIT_SOL_SEGUIMIENTO segAux = _segDao.dmlSelectID(solSegBuscar);
            ////    _afdEdoDataMdl.AFDseguimientoMdl.usrclave = segAux.usrclave;

            ////    SIT_RED_NODO nodoNvoUTanalizar = ExisteNodo(_afdEdoDataMdl.solClave, Constantes.NodoEstado.UT_SOLICITUD_RECIBIR, (int)segAux.usrclave, _afdEdoDataMdl.ID_Capa + 1);

            ////    // BUSCAR QUIEN ATIENDE LA SOLICUTD.... APRA BUSCAR LE NODO...
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
            ////            usrclave = segAux.usrclave
            ////        };
            ////        _nodoDao.dmlAgregar(nodoNvoUTanalizar);
            ////    }

            ////    // Aqui me falta agregar el proceso Gral Dao..
            ////    //////////////_prcGralDao.InsertarRegistro(_afdEdoDataMdl.dicAuxRespuesta);
            ////    return Turnar(_afdEdoDataMdl);
            ////}
            else if(_afdEdoDataMdl.rtpclave == Constantes.Respuesta.ASIGNAR)
            {
                Asignar();
            }

            return 0;        
        }
    }
}