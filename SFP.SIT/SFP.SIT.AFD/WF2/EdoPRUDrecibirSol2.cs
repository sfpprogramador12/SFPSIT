using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.AFD.WF2
{
    public class EdoPRUDrecibirSol2 : AfdNodoBase
    {
        public EdoPRUDrecibirSol2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
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
            else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            {
                _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.SI;

                //BUSCAMOS QUIEN ES EL QUE ESTA ATENIDNEDO LA SOLICITUD.. PARA CREAR LA BARRERA
                SIT_SOL_SEGUIMIENTO solSegBuscar = new SIT_SOL_SEGUIMIENTO();
                solSegBuscar.solclave = _afdEdoDataMdl.solClave;
                solSegBuscar.prcclave = iClaveProceso;

                SIT_SOL_SEGUIMIENTO segAux = _segDao.dmlSelectID(solSegBuscar);
                _afdEdoDataMdl.AFDseguimientoMdl.usrclave = segAux.usrclave;

                SIT_RED_NODO nodoNvoPRUDanalizar = ExisteNodo(_afdEdoDataMdl.solClave, Constantes.NodoEstado.PRUD_REVISARRESP_SOLICITUD, (int)segAux.usrclave, _afdEdoDataMdl.ID_Capa + 1);

                // BUSCAR QUIEN ATIENDE LA SOLICUTD.... PARA BUSCAR LE NODO...
                if (nodoNvoPRUDanalizar == null)
                {
                    // CREAR NODO ACTUAL SIGUIENTE DE LA PRUFanalizar que es la barrera
                    nodoNvoPRUDanalizar = new SIT_RED_NODO
                    {
                        prcclave = iClaveProceso,
                        solclave = _afdEdoDataMdl.solClave,
                        araclave = _afdEdoDataMdl.ID_AreaUT,
                        nodcapa = _afdEdoDataMdl.ID_Capa + 1,
                        nodatendido = AfdConstantes.NODO.INDETERMINADO,
                        nedclave = Constantes.NodoEstado.PRUD_REVISARRESP_SOLICITUD,
                        nodfeccreacion = _afdEdoDataMdl.FechaRecepcion,
                        nodclave = Constantes.General.ID_PENDIENTE,
                        usrclave = segAux.usrclave,
                        perclave = Constantes.Perfil.PRUT
                    };
                    _nodoDao.dmlAgregar(nodoNvoPRUDanalizar);
                }
                
                SIT_RED_NODORESP oNodoResp = _redNodoRespDao.dmlSelectRespUnica(_afdEdoDataMdl.AFDnodoActMdl.nodclave);
                _afdEdoDataMdl.repClave = oNodoResp.repclave;

                return Turnar(_afdEdoDataMdl, _nodoDao.iSecuencia);
            }
            else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.ASIGNAR)
            {
                Asignar();
            }

            return 0;
        }
    }
}