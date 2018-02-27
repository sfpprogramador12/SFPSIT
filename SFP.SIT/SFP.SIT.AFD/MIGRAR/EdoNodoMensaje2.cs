using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Model.RED;
using System;
using System.Data.Common;

namespace SFP.SIT.AFD.MIGRAR
{
    class EdoNodoMensaje2 :  AfdNodoBase
    {
        public EdoNodoMensaje2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {

        }

        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            // ACTUALIZAMOS LA ARISTA CON LOS DATOS DEL USUARIO Y LA FECHA
            SIT_RED_ARISTA aristaAct = new SIT_RED_ARISTA();
            aristaAct.ariclave = _afdEdoDataMdl.ID_ClaAristaActual;
            //////aristaAct.solclave = _afdEdoDataMdl.ID_folio;
            aristaAct.arifecenvio = _afdEdoDataMdl.FechaRecepcion;


            //ACTUALIZAMOS EL NODO ACTUAL
            _afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
            _nodoDao.dmlUpdateNodoAtendido(_afdEdoDataMdl.AFDnodoActMdl);
        
            return true;
        }
    }
}