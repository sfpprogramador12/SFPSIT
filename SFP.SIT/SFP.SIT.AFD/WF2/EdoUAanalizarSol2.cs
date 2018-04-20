using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.AFD.WF2
{
    class EdoUAanalizarSol2 : AfdNodoBase
    {
        public EdoUAanalizarSol2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {
        }

        private void Borrar()
        {
            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.INCOMPETENCIA_PARCIAL_AREA)
            {
                BorrarRepuestas(_afdEdoDataMdl.AFDnodoActMdl.nodclave, _afdEdoDataMdl.rtpclave, OPE_RESPUESTA_DIFERENTE);
            }
            else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.RIA_AREA)
            {
                BorrarRepuestas(_afdEdoDataMdl.AFDnodoActMdl.nodclave, _afdEdoDataMdl.rtpclave, OPE_RESPUESTA_DIFERENTE);

            }
            else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            {
                BorrarRepuestas(_afdEdoDataMdl.AFDnodoActMdl.nodclave, _afdEdoDataMdl.rtpclave, OPE_RESPUESTA_DIFERENTE);

            }
            else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.RESPUESTA_MULTIPLE)
            {
                BorrarRepuestas(_afdEdoDataMdl.AFDnodoActMdl.nodclave, Constantes.Respuesta.INCOMPETENCIA_PARCIAL_AREA, OPE_RESPUESTA_IGUAL);
                BorrarRepuestas(_afdEdoDataMdl.AFDnodoActMdl.nodclave, Constantes.Respuesta.RIA_AREA, OPE_RESPUESTA_IGUAL);
            }
        }

        public Object Accion(Object oDatos)
        {
            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            Borrar();

            /// FALTA GRABAR LA RESOLUCION DE LA UA...
            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            {                
                return base.Turnar(_afdEdoDataMdl, _afdEdoDataMdl.AFDnodoActMdl.nodregresar);
            }
            else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.AMPLIACION_PLAZO)
            {
                // SE CREA UN NUEVO FLUJO DE TRABAJO EN PARALELO
                _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);
                base.AccionBase(false);
            }                
            else  // DEFINO UNA RESPUESTA
            {
                long iNodoOrigen = _afdEdoDataMdl.AFDnodoActMdl.nodclave;

                // Constantes.Respuesta.RIA_AREA
                // DEFINO UNA RESPUESTA UNA RESPUESTA QUE ES MULTIPLE
                if (_afdEdoDataMdl.rtpclave != Constantes.Respuesta.INCOMPETENCIA_TOTAL_AREA &&
                    _afdEdoDataMdl.rtpclave != Constantes.Respuesta.RIA_AREA)
                {
                    _afdEdoDataMdl.rtpclave = Constantes.Respuesta.RESPUESTA_MULTIPLE;
                }
                        
                base.AccionBase(true);
                //Regreso las repusta al nodo que se creo previamente....
                RespMoverSigNodo(iNodoOrigen, _afdEdoDataMdl.AFDnodoActMdl.nodclave, Constantes.RespuestaEstado.PROPUESTA, Constantes.RespuestaEstado.ANALIZAR);                
            }
            return true;
        }
    }
}
