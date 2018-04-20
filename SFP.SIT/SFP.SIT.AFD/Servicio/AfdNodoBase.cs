using SFP.Persistencia.Dao;
using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Dao.DOC;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;


namespace SFP.SIT.AFD.Servicio
{
    public class AfdNodoBase : BaseDao
    {
        public const string PARAM_AFD_EDO_DATA_MDL = "afdEdoDataMdl";
        public const int OPE_RESPUESTA_IGUAL = 1;
        public const int OPE_RESPUESTA_DIFERENTE = 2;


        /* ESTRUCTURA DE DATOS */
        // TINE VALOR CUANDO SE EJECUTA EN LA CLASE DERIVADA
        protected AfdEdoDataMdl _afdEdoDataMdl;

        /* GRABAR A BASE DE DATOS */
        protected SIT_RED_NODODao   _nodoDao;
        protected SIT_RED_ARISTADao     _redAristaDao;
        protected SIT_SOL_SEGUIMIENTODao _segDao;

        protected SIT_RED_NODORESPDao _redNodoRespDao;                        
        protected SIT_RESP_RESPUESTADao _respRespDao;
       
        protected SIT_DOC_DOCUMENTODao _docDocumentoDao;

        /* Para amdinistrar las respuestas */
        protected ProcesoGralDao _prcGralDao;

        /* CLASE PAR ALA ADMINSITRACIÓN DE DOCUMENTOS*/
        protected AdmDocContNeg _admDocNeg;

        /* RUTINAS DE VALIDACION DE DATOS*/
        protected CalcularPlazoNeg _calcularPlazoNeg;

        /* SIQUEINTE ESTADO DEL AFD */
        public Dictionary<int, int> dicAccionEstado;
        protected int Indice { get; set; }
        public AfdNodoBase(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            _nodoDao = new SIT_RED_NODODao(_cn, _transaction, _sDataAdapter);
            _redAristaDao = new SIT_RED_ARISTADao(_cn, _transaction, _sDataAdapter);
            _segDao = new SIT_SOL_SEGUIMIENTODao(_cn, _transaction, _sDataAdapter);

            // RESPUESTA
            _redNodoRespDao = new SIT_RED_NODORESPDao(_cn, _transaction, _sDataAdapter);            
            _respRespDao = new SIT_RESP_RESPUESTADao(_cn, _transaction, _sDataAdapter);

            ////////////_respRespDatosDao = new SIT_RESP_DATOSDao(_cn, _transaction, _sDataAdapter);
            ////////////_docDocumentoDao = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter);

            _prcGralDao = new ProcesoGralDao(_cn, _transaction, _sDataAdapter);
        }

        //////////public Object Accion(Object oDatos)
        //////////{
        //////////    _afdEdoDataMdl = (AfdEdoDataMdl)oDatos;
        //////////    _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);
        //////////    return AccionBase(true);
        //////////}

        public void AsignarID(int iIndice)
        {
            Indice = iIndice;
        }

        protected Boolean AccionBase(bool bAvanzar)
        {
            try
            {
                SIT_RED_NODO nodoActual = null;


                _afdEdoDataMdl.AFDnodoOrigen =  _afdEdoDataMdl.AFDnodoActMdl;

                if (bAvanzar == true)
                {
                    _afdEdoDataMdl.AFDnodoOrigen.nodatendido = AfdConstantes.NODO.FINALIZADO;
                    _nodoDao.dmlEditar(_afdEdoDataMdl.AFDnodoOrigen);
                    _afdEdoDataMdl.ID_EstadoSiguiente = _afdEdoDataMdl.dicAfdFlujo[_afdEdoDataMdl.ID_EstadoActual].dicAccionEstado[_afdEdoDataMdl.rtpclave];
                    _afdEdoDataMdl.ID_EstadoActual = _afdEdoDataMdl.ID_EstadoSiguiente;
                }

                if (_calcularPlazoNeg == null)
                    _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);

                if (_afdEdoDataMdl.AFDnodoOrigen.nodregresar > 0)
                {
                    nodoActual = _nodoDao.dmlSelectNodoID(_afdEdoDataMdl.AFDnodoOrigen.nodregresar);
                    nodoActual.nodatendido = AfdConstantes.NODO.EN_PROCESO;
                    _nodoDao.dmlUpdateNodoAtendido(nodoActual);
                }
                else
                {
                    nodoActual = new SIT_RED_NODO
                    {
                        prcclave = _afdEdoDataMdl.AFDseguimientoMdl.prcclave,
                        solclave = _afdEdoDataMdl.solClave,
                        araclave = _afdEdoDataMdl.ID_AreaDestino,
                        nodcapa = _afdEdoDataMdl.ID_Capa,
                        nodatendido = AfdConstantes.NODO.EN_PROCESO,
                        nedclave = _afdEdoDataMdl.ID_EstadoActual,
                        nodfeccreacion = _afdEdoDataMdl.FechaRecepcion,
                        nodclave = Constantes.General.ID_PENDIENTE,
                        usrclave = _afdEdoDataMdl.usrClaveDestino,
                        perclave = _afdEdoDataMdl.ID_PerfilDestino
                    };
                    _nodoDao.dmlAgregar(nodoActual);
                    nodoActual.nodclave = _nodoDao.iSecuencia;
                }
                // CREAR NODO ACTUAL             


                // NODO ACTUAL EL NUEVO CREADO
                _afdEdoDataMdl.AFDnodoActMdl = nodoActual;

                /* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
                int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(_afdEdoDataMdl.AFDnodoOrigen.nodfeccreacion, nodoActual.nodfeccreacion);

                SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA
                {
                    ariclave = Constantes.General.ID_PENDIENTE,
                    nodorigen = _afdEdoDataMdl.AFDnodoOrigen.nodclave,
                    noddestino = nodoActual.nodclave,
                    arifecenvio = _afdEdoDataMdl.AFDnodoOrigen.nodfeccreacion,
                    aridiasnat = aiDias[CalcularPlazoNeg.DIAS_NATURALES],
                    aridiaslab = aiDias[CalcularPlazoNeg.DIAS_LABORALES],
                    arihito = _afdEdoDataMdl.ID_Hito,                    
                    rtpclave = _afdEdoDataMdl.rtpclave,
                    solclave = _afdEdoDataMdl.solClave
                };

                _redAristaDao.dmlAgregar(aristaMdl);
                aristaMdl.ariclave = _redAristaDao.iSecuencia;

                /* ACTUALIZAR EL SEGUIMIENTO */
                _afdEdoDataMdl.AFDseguimientoMdl = _calcularPlazoNeg.CalcularSeguimiento((int)_afdEdoDataMdl.solicitud.sotclave, nodoActual.nodfeccreacion,
                    _afdEdoDataMdl.lstProcesoPlazos, _afdEdoDataMdl.AFDseguimientoMdl);
                _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

                _afdEdoDataMdl.ID_ClaAristaActual = aristaMdl.ariclave;
                ////////RespMoverSigNodo(_afdEdoDataMdl.AFDnodoOrigen.nodclave, _afdEdoDataMdl.AFDnodoActMdl.nodclave);
            }
            catch (Exception ex)
            {
                _sMsjError = ex.ToString();
                throw new Exception("Error en el método AccionBase " + ex.ToString());
            }

            return true;            
        }

        protected Boolean RespMoverSigNodo(Int64 iNodoOrigen, Int64 iNodoDestino, int iRespEdoAct, int iRespEdoNvo )
        {
            try
            {
                List<SIT_RED_NODORESP> lstResp = new List<SIT_RED_NODORESP>();
                lstResp = _redNodoRespDao.dmlSelectObtenerRespNodo(iNodoOrigen, Constantes.RespuestaEstado.PROPUESTA);
                foreach (SIT_RED_NODORESP nodoRedAnt in lstResp)
                {
                    SIT_RED_NODORESP nodoRespAct = new SIT_RED_NODORESP
                    { nodclave = iNodoDestino, repclave = nodoRedAnt.repclave, sdoclave = iRespEdoNvo };
                    _redNodoRespDao.dmlAgregar(nodoRespAct);
                    nodoRedAnt.sdoclave = iRespEdoAct;
                    _redNodoRespDao.dmlEditar(nodoRedAnt);
                }
            }

            catch (Exception exp)
            {
                throw new Exception("Error al mover las respuestas " + exp.Message);
            }
            return true;
        }




        protected bool GrabarDocSol(Int64 isolClave)
        {
            long iDocId = 0;
            SIT_DOC_DOCUMENTODao docDao;
            AdmDocContNeg admDocNeg;

            if (_afdEdoDataMdl.lstDocContenidoMdl == null)
                return true;

            // SIEMPRE TENER A LA SOLICITUD
            if (_afdEdoDataMdl.lstDocContenidoMdl.Count > 0)
            {
                docDao = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter);
                SIT_SOL_DOCDao soldocDao = new SIT_SOL_DOCDao(_cn, _transaction, _sDataAdapter);
                admDocNeg = new AdmDocContNeg(_afdEdoDataMdl.SharePointCxn, _afdEdoDataMdl.solicitud.solfecsol);

                try
                {
                    foreach (DocContenidoMdl docContMdl in _afdEdoDataMdl.lstDocContenidoMdl)
                    {
                        if (docContMdl != null)
                        {
                            if (docContMdl.docnombre != " ")
                            {
                                docContMdl.docmd5 = admDocNeg.ObtenerMD5(docContMdl);
                                iDocId = (int)docDao.dmlSelectDocNombreMD5(docContMdl.docmd5);
                            }
                            else
                                iDocId = 0;

                            // VERIFICAR QUE NO EXISTA EL DOCUMENTO
                            if (iDocId == 0)
                            {
                                if (docContMdl.docnombre != " " && docContMdl.doc_contenido != null)
                                    docContMdl.docruta = admDocNeg.Grabar(docContMdl);

                                docDao.dmlAgregar(docContMdl);
                                iDocId = docDao.iSecuencia;

                                SIT_SOL_DOC docSol = new SIT_SOL_DOC(docclave: iDocId, solclave: isolClave);
                                soldocDao.dmlAgregar(docSol);
                            }
                            else
                            {
                                // REEMPLAZAR
                                docDao.dmlEditar(docContMdl);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _sMsjError = ex.ToString();
                    throw new Exception("Error al grabar al grabar un documento " + ex.ToString());
                }
            }
            return true;
        }

        protected SIT_RED_NODO ExisteNodo(long lFolio, int iNodoEstado, int iUsuario, int iCapa)
        {
            SIT_RED_NODO oNodoBuscar = new SIT_RED_NODO();

            oNodoBuscar.solclave = lFolio;
            oNodoBuscar.nodclave = iNodoEstado;
            oNodoBuscar.usrclave = iUsuario;
            oNodoBuscar.nodcapa = iCapa;

            // verificar si bien en nulo           
            return _nodoDao.dmlSelectNodoExiste(oNodoBuscar);
        }

        protected Object Turnar(AfdEdoDataMdl afdDataMdl, long nodRegresar)
        {
            object oResultado = null;            
            Int32 iTipoAristaIni = afdDataMdl.rtpclave;

            SIT_RED_NODO nodoInicial = _nodoDao.dmlSelectNodoID(afdDataMdl.AFDnodoActMdl.nodclave);

            SIT_RESP_TURNARDao turnarDao = new SIT_RESP_TURNARDao(_cn, _transaction, _sDataAdapter);
            List<SIT_RESP_TURNAR> lstTurnar = turnarDao.dmlSelectTurnarResp(afdDataMdl.repClave);
            foreach (SIT_RESP_TURNAR usrTurnar in lstTurnar)
            {
                afdDataMdl.ID_EstadoActual = (int)nodoInicial.nedclave;
                afdDataMdl.AFDnodoActMdl = nodoInicial;
                afdDataMdl.rtpclave = iTipoAristaIni;
                afdDataMdl.usrClaveDestino = usrTurnar.usrclave;
                afdDataMdl.Observacion = usrTurnar.turinstruccion;
                afdDataMdl.ID_AreaDestino = usrTurnar.araclave;
                afdDataMdl.ID_Capa = nodoInicial.nodcapa;
                oResultado = AccionBase(true);

                afdDataMdl.AFDnodoActMdl.nodregresar = nodRegresar;
                _nodoDao.dmlUpdateNodoRegresar(afdDataMdl.AFDnodoActMdl);

                // sera uno especial para el aerea que estos turnando
                RespMoverSigNodo(nodoInicial.nodclave, _afdEdoDataMdl.AFDnodoActMdl.nodclave, Constantes.RespuestaEstado.TURNADO, Constantes.RespuestaEstado.ANALIZAR);
            }

            if (lstTurnar.Count > 1)
            {
                _segDao.dmlUpdMultiple( afdDataMdl.AFDseguimientoMdl);
            }
            

            //List<Tuple<int, string, int>> lstPersonasTurnar = _afdEdoDataMdl.dicAuxRespuesta[ProcesoGralDao.PARAM_LISTA_TURNAR] as List<Tuple<int, string, int>>;
            //foreach (Tuple<int, string, int> areaTurnar in lstPersonasTurnar )
            //{                
            //    afdDataMdl.ID_EstadoActual = (int)nodoInicial.nedclave;
            //    afdDataMdl.AFDnodoActMdl = nodoInicial;
            //    afdDataMdl.rtpclave = iTipoAristaIni;
            //    afdDataMdl.usrClaveDestino = areaTurnar.Item1;
            //    afdDataMdl.Observacion = areaTurnar.Item2;
            //    afdDataMdl.ID_AreaDestino = areaTurnar.Item3;                
            //    afdDataMdl.ID_Capa = nodoInicial.nodcapa;
            //    oResultado = AccionBase(true);
                
            //    afdDataMdl.AFDnodoActMdl.nodregresar = nodRegresar;
            //    _nodoDao.dmlUpdateNodoRegresar(afdDataMdl.AFDnodoActMdl);

            //    RespMoverSigNodo(nodoInicial.nodclave, _afdEdoDataMdl.AFDnodoActMdl.nodclave, Constantes.RespuestaEstado.TURNADO, Constantes.RespuestaEstado.ANALIZAR);
            //}        
            return oResultado;
        }


        protected int BorrarRepuestas(long nodClave, int rtpClave, int tipo)
        {
            int iBorrar = 0;
            List<SIT_RESP_RESPUESTA> lstResp;


            if (tipo == OPE_RESPUESTA_IGUAL)
            {
                lstResp = _respRespDao.dmlSelectRespNodoRtpIgual(_afdEdoDataMdl.AFDnodoActMdl.nodclave, _afdEdoDataMdl.rtpclave,
                    Constantes.RespuestaEstado.PROPUESTA) as List<SIT_RESP_RESPUESTA>;
            }
            else
            {
                lstResp = _respRespDao.dmlSelectRespNodoRtpDif(_afdEdoDataMdl.AFDnodoActMdl.nodclave, _afdEdoDataMdl.rtpclave,
                    Constantes.RespuestaEstado.PROPUESTA) as List<SIT_RESP_RESPUESTA>;
            }

            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(DButil.SIT_RED_NODORESP_COL.NODCLAVE, _afdEdoDataMdl.AFDnodoActMdl.nodclave);
            dicDatos.Add(DButil.SIT_RESP_RESPUESTA_COL.REPCLAVE, 0);
            dicDatos.Add(DButil.SIT_RESP_RESPUESTA_COL.RTPCLAVE, 0);


            foreach (SIT_RESP_RESPUESTA Nodoresp in lstResp)
            {
                dicDatos[DButil.SIT_RESP_RESPUESTA_COL.REPCLAVE] = Nodoresp.repclave;
                dicDatos[DButil.SIT_RESP_RESPUESTA_COL.RTPCLAVE] = Nodoresp.rtpclave;

                _prcGralDao.BorrarRespuesta(dicDatos);
            }

            return iBorrar;
        }


        protected int ResponderSol()
        {
            SIT_RED_NODORESP oNodoResp = _redNodoRespDao.dmlSelectRespUnica(_afdEdoDataMdl.AFDnodoActMdl.nodclave);

            _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;
            _afdEdoDataMdl.ID_AreaDestino = _afdEdoDataMdl.ID_AreaInai;
            AccionBase(true);

            //RespMoverSigNodo(Int64 iNodoOrigen, Int64 iNodoDestino, int iRespEdoAct, int iRespEdoNvo)

            // ACTUALIZAMOS LOS DATOS DEL SEGUIMIENTO
            _afdEdoDataMdl.AFDseguimientoMdl.segfecfin = _afdEdoDataMdl.AFDnodoActMdl.nodfeccreacion;
            _afdEdoDataMdl.AFDseguimientoMdl.segultimonodo = _afdEdoDataMdl.AFDnodoActMdl.nodclave;
            _afdEdoDataMdl.AFDseguimientoMdl.segedoproceso = AfdConstantes.PROCESO_ESTADO.TERMINADO;
            _afdEdoDataMdl.AFDseguimientoMdl.usrclave = _afdEdoDataMdl.usrClaveOrigen;

            // SE ACTUALZIA LA ARISTA DE RESPUESTA...
            _afdEdoDataMdl.AFDseguimientoMdl.repclave = oNodoResp.repclave;
            _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

            // FINALIZAMOS EL NODOD CREADO CON LA RESPUESTA
            _afdEdoDataMdl.AFDnodoActMdl.nodatendido = AfdConstantes.NODO.FINALIZADO;
            return _nodoDao.dmlEditar(_afdEdoDataMdl.AFDnodoActMdl);
        }


        protected int Asignar()
        {
            Int64 iNodoOrigen = _afdEdoDataMdl.AFDnodoActMdl.nodclave;
            _afdEdoDataMdl.ID_Hito = Constantes.RespuestaHito.NO;

            // ACTUALIZAMOS QUE PERSONA LE VA A DAR SEGUIMIENTO
            _afdEdoDataMdl.AFDseguimientoMdl.usrclave = _afdEdoDataMdl.usrClaveDestino;
            _afdEdoDataMdl.ID_AreaDestino = _afdEdoDataMdl.ID_AreaUT;
            AccionBase(true);

            RespMoverSigNodo(iNodoOrigen, _afdEdoDataMdl.AFDnodoActMdl.nodclave, Constantes.RespuestaEstado.TURNADO, Constantes.RespuestaEstado.ANALIZAR);
            return 1;
        }

    }
}