using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using System;
using System.Data.Common;

namespace SFP.SIT.AFD.MIGRAR
{
    public class EdoUArequerir2 : AfdNodoBase
    {
        public EdoUArequerir2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {
        }

        public Object Accion(Object oDatos)
        {

            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            //AL NODO ACTGUAL LO ACTUALIZAMOS
            _afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
            _nodoDao.dmlEditar(_afdEdoDataMdl.AFDnodoActMdl);

            // BUSCAR EL NODO ANTERIOR DE CT por FOLIO - CAPA - AREA
            SIT_RED_NODO nodoCT = ExisteNodo(_afdEdoDataMdl.solClave, Constantes.NodoEstado.INDEFINIDO, _afdEdoDataMdl.ID_AreaCT, _afdEdoDataMdl.ID_Capa);

            /* CREAR ARISTA NODO_ACTUAL UA --> CT  */
            int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(_afdEdoDataMdl.AFDnodoActMdl.nodfeccreacion, _afdEdoDataMdl.FechaRecepcion);
            SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA {  arihito= Constantes.RespuestaHito.SI,
                aridiasnat= aiDias[CalcularPlazoNeg.DIAS_NATURALES], 
                aridiaslab= aiDias[CalcularPlazoNeg.DIAS_LABORALES], arifecenvio= _afdEdoDataMdl.FechaRecepcion,
                ariclave= Constantes.General.ID_PENDIENTE,
                noddestino= nodoCT.nodclave, nodorigen= _afdEdoDataMdl.AFDnodoActMdl.nodclave };

            _redAristaDao.dmlEditar(aristaMdl);
            aristaMdl.ariclave = _redAristaDao.iSecuencia;

            /* ACTUALIZAR EL SEGUIMIENTO */
            _afdEdoDataMdl.AFDseguimientoMdl = _calcularPlazoNeg.CalcularSeguimiento((int)_afdEdoDataMdl.solicitud.sotclave, _afdEdoDataMdl.FechaRecepcion,
                _afdEdoDataMdl.lstProcesoPlazos, _afdEdoDataMdl.AFDseguimientoMdl);

            _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

            //////GrabarDocumentos(aristaMdl.ariclave);

            return true;
        }
    }
}
