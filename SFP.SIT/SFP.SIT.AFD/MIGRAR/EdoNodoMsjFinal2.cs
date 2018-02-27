using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Model.RED;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.AFD.MIGRAR
{
    class EdoNodoMsjFinal2 : AfdNodoBase
    {
        public EdoNodoMsjFinal2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {

        }

        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            ////////////SIT_RED_ARISTALECTURADao _redAristaLecturaDao = new SIT_RED_ARISTALECTURADao(_cn, _transaction, _sDataAdapter);
            ////////////_redAristaLecturaDao.dmlAgregar(new SIT_RED_ARISTALECTURA(DateTime.Now, _afdEdoDataMdl.usrClaveOrigen, _afdEdoDataMdl.ID_ClaAristaActual));

            //ACTUALIZAMOS EL NODO ACTUAL
            _afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
            _nodoDao.dmlUpdateNodoAtendido(_afdEdoDataMdl.AFDnodoActMdl);

            return true;
        }
    }
}
