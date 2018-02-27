using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Util;
using System;
using System.Data.Common;

namespace SFP.SIT.AFD.MIGRAR
{
    class EdoUTrecibirAcl2 : AfdNodoBase
    {
        public EdoUTrecibirAcl2(DbConnection cn, DbTransaction transaction, string sDataAdapter) : base(cn, transaction, sDataAdapter)
        {
        }
        public Object Accion(Object oDatos)
        {
            int iClaveProceso = (int)_afdEdoDataMdl.solicitud.prcclave;

            _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;

            //ACTUALIZAR LA TABLA ACLARACION PARA SABER EN QUE PROCESO ESTA
            _afdEdoDataMdl.solicitud.prcclave = Constantes.ProcesoTipo.ACLARACION;
            _afdEdoDataMdl.solicitud.solfecacl = _afdEdoDataMdl.FechaRecepcion;
            new SIT_SOL_SOLICITUDDao(_cn, _transaction, _sDataAdapter).dmlEditar(_afdEdoDataMdl.solicitud);

            //MODIFICAR SE DEBE DE REGRESAR LA SOLICITUD AL ÁREA ORIGINAL
            Object bResultado = AccionBase(true);

            // CREAR DE FORMA ANTICIPADA EL NODO DE UT ANALIZAR
            if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
            {
                SIT_RED_NODO nodoNvoUTanalizar = ExisteNodo(_afdEdoDataMdl.solClave, Constantes.NodoEstado.UT_RESPUESTA_ANALIZAR, _afdEdoDataMdl.ID_AreaUT, _afdEdoDataMdl.ID_Capa + 1);
                if (nodoNvoUTanalizar == null)
                {
                    // CREAR NODO ACTUAL SIGUIENTE DE LA UTanalizar que es la barrera
                   
                    nodoNvoUTanalizar = new SIT_RED_NODO { prcclave= iClaveProceso, solclave= _afdEdoDataMdl.solClave, araclave= _afdEdoDataMdl.ID_AreaUT,
                        nodcapa= _afdEdoDataMdl.ID_Capa + 1, nodatendido= AfdConstantes.NODO.INDETERMINADO, nodclave= Constantes.NodoEstado.UT_RESPUESTA_ANALIZAR,
                        nodfeccreacion= _afdEdoDataMdl.FechaRecepcion, nedclave= Constantes.General.ID_PENDIENTE,
                        usrclave = _afdEdoDataMdl.usrClaveDestino};

                    _nodoDao.dmlAgregar(nodoNvoUTanalizar);
                }
            }


            //hubo una respuesta
            if (_afdEdoDataMdl.rtpclave != Constantes.Respuesta.TURNAR )
            {

                bResultado = _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);
            }

            return bResultado;
        }
    }
}