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
        public const int OPE_ACCION = 211;

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
            ////////////_respRespDao = new SIT_RESP_RESPUESTADao(_cn, _transaction, _sDataAdapter);
            ////////////_respRespDatosDao = new SIT_RESP_DATOSDao(_cn, _transaction, _sDataAdapter);
            ////////////_docDocumentoDao = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter);
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

                // CREAR NODO ACTUAL             
                SIT_RED_NODO nodoActual = new SIT_RED_NODO
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
                RespMoverSigNodo(_afdEdoDataMdl.AFDnodoOrigen.nodclave, _afdEdoDataMdl.AFDnodoActMdl.nodclave);
            }
            catch (Exception ex)
            {
                _sMsjError = ex.ToString();
                throw new Exception("Error en el método AccionBase " + ex.ToString());
            }

            return true;            
        }


        protected Boolean RespMoverSigNodo( Int64 iNodoOrigen, Int64 iNodoDestino )
        {
            try
            {
                SIT_RED_NODORESP nodoRespAct = null;

                List<SIT_RED_NODORESP> lstResp = new List<SIT_RED_NODORESP>();


                //SI ES AMPLIACIÓN DE PLAZO NO APLICAR ESTA OPCION..
                if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.AMPLIACION_PLAZO)
                {
                    lstResp = _redNodoRespDao.dmlSelectRespNodoAmpPlazo(iNodoOrigen);

                    foreach (SIT_RED_NODORESP nodoRedAnt in lstResp)
                    {
                        if (nodoRedAnt.sdoclave == Constantes.RespuestaEstado.PROPUESTA)
                        {
                            nodoRespAct = new SIT_RED_NODORESP
                            { nodclave = iNodoDestino, repclave = nodoRedAnt.repclave, sdoclave = Constantes.RespuestaEstado.ANALIZAR };
                        }

                        if (nodoRespAct != null)
                        {
                            _redNodoRespDao.dmlAgregar(nodoRespAct);
                            nodoRespAct = null;
                        }
                    }
                }
                else if (_afdEdoDataMdl.rtpclave == Constantes.Respuesta.TURNAR)
                {
                    lstResp = _redNodoRespDao.dmlSelectRespNodoTurnar(iNodoOrigen,(int) _afdEdoDataMdl.AFDnodoActMdl.usrclave);
                    foreach (SIT_RED_NODORESP nodoRedAnt in lstResp)
                    {
                        if (nodoRedAnt.sdoclave == Constantes.RespuestaEstado.PROPUESTA)
                        {
                            nodoRespAct = new SIT_RED_NODORESP
                            { nodclave = iNodoDestino, repclave = nodoRedAnt.repclave, sdoclave = Constantes.RespuestaEstado.ANALIZAR };
                        }

                        if (nodoRespAct != null)
                        {
                            _redNodoRespDao.dmlAgregar(nodoRespAct);
                            nodoRespAct = null;
                        }
                    }

                }
                else
                {
                    lstResp = _redNodoRespDao.dmlSelectRespNodo(iNodoOrigen);
                    foreach (SIT_RED_NODORESP nodoRedAnt in lstResp)
                    {
                        if (nodoRedAnt.sdoclave == Constantes.RespuestaEstado.TURNAR)
                        {
                            nodoRespAct = new SIT_RED_NODORESP
                            { nodclave = iNodoDestino, repclave = nodoRedAnt.repclave, sdoclave = Constantes.RespuestaEstado.TURNADO };
                        }
                        else if (nodoRedAnt.sdoclave == Constantes.RespuestaEstado.PROPUESTA)
                        {
                            nodoRespAct = new SIT_RED_NODORESP
                            { nodclave = iNodoDestino, repclave = nodoRedAnt.repclave, sdoclave = Constantes.RespuestaEstado.ANALIZAR };
                        }

                        if (nodoRespAct != null)
                        {
                            _redNodoRespDao.dmlAgregar(nodoRespAct);
                            nodoRespAct = null;
                        }
                    }
                }


                
            }
            catch (Exception exp)
            {
                throw new Exception("Error al mover las respuestas " + exp.Message );
            }
            return true;
        }
        
        //////protected bool AccionCrearNodoAristaSeg()
        //////{
        //////    try
        //////    {
        //////        if (_calcularPlazoNeg == null)
        //////            _calcularPlazoNeg = new CalcularPlazoNeg(_afdEdoDataMdl.dicDiaNoLaboral);

        //////        // CREAR NODO ACTUAL             
        //////        SIT_RED_NODO nodoActual = new SIT_RED_NODO
        //////        {
        //////            prcclave = _afdEdoDataMdl.AFDseguimientoMdl.prcclave,
        //////            solclave = _afdEdoDataMdl.ID_folio,
        //////            araclave = _afdEdoDataMdl.ID_AreaDestino,
        //////            nodcapa = _afdEdoDataMdl.ID_Capa,
        //////            nodatendido = AfdConstantes.NODO.EN_PROCESO,
        //////            nedclave = _afdEdoDataMdl.ID_EstadoActual,
        //////            nodfeccreacion = _afdEdoDataMdl.FechaRecepcion,                    
        //////            nodclave = Constantes.General.ID_PENDIENTE,
        //////            usrclave = _afdEdoDataMdl.usrClaveDestino
        //////        };
        //////        _nodoDao.dmlAgregar(nodoActual);
        //////        nodoActual.nodclave = _nodoDao.iSecuencia;

        //////        // NODO ACTUAL EL NUEVO CREADO
        //////        _afdEdoDataMdl.AFDnodoActMdl = nodoActual;

        //////        /* CREAR ARISTA NODO_ANTERIOR --> NODO_NUEVO  */
        //////        int[] aiDias = _calcularPlazoNeg.obtenerDiasNaturalesLaborales(nodoAnterior.nodfeccreacion, nodoActual.nodfeccreacion);

        //////        SIT_RED_ARISTA aristaMdl = new SIT_RED_ARISTA {
        //////            ariclave = Constantes.General.ID_PENDIENTE,
        //////            nodorigen = nodoAnterior.nodclave,
        //////            noddestino = nodoActual.nodclave,
        //////            arifecenvio = nodoAnterior.nodfeccreacion,
        //////            aridiasnat = aiDias[CalcularPlazoNeg.DIAS_NATURALES],
        //////            aridiaslab = aiDias[CalcularPlazoNeg.DIAS_LABORALES],
        //////            arihito = _afdEdoDataMdl.ID_Hito,
        //////            arimensaje = _afdEdoDataMdl.Observacion,
        //////            rsbclave = };

        //////        _redAristaDao.dmlAgregar(aristaMdl);
        //////        aristaMdl.ariclave = _redAristaDao.iSecuencia;

        //////        /* ACTUALIZAR EL SEGUIMIENTO */
        //////        _afdEdoDataMdl.AFDseguimientoMdl = _calcularPlazoNeg.CalcularSeguimiento(_afdEdoDataMdl.solicitud.sotclave, nodoActual.nodfeccreacion,
        //////            _afdEdoDataMdl.lstProcesoPlazos, _afdEdoDataMdl.AFDseguimientoMdl);
        //////        _segDao.dmlEditar(_afdEdoDataMdl.AFDseguimientoMdl);

        //////        GrabarDocumentos(aristaMdl.ariclave);

        //////        _afdEdoDataMdl.ID_ClaAristaActual = aristaMdl.ariclave;
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        _sMsjError = ex.ToString();
        //////        throw new Exception("Error en el método AccionCrearNodoAristaSeg " + ex.ToString());
        //////    }
        //////    return true;
        //////}

        //////protected bool GrabarDocumentos(Int64 iNodo)
        //////{
        //////    int iDocId = 0;

        //////    SIT_DOC_DOCUMENTODao docDao;

        //////    AdmDocContNeg admDocNeg;

        //////    if (_afdEdoDataMdl.lstDocContenidoMdl == null)
        //////        return true; 

        //////    // SIEMPRE TENER A LA SOLICITUD
        //////    if (_afdEdoDataMdl.lstDocContenidoMdl.Count > 0)
        //////    {
        //////        docDao = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter);

        //////        admDocNeg = new AdmDocContNeg(_afdEdoDataMdl.SharePointCxn, _afdEdoDataMdl.solicitud.solfecsol);

        //////        try
        //////        {
        //////            foreach (DocContenidoMdl docContMdl in _afdEdoDataMdl.lstDocContenidoMdl)
        //////            {
        //////                if (docContMdl != null)
        //////                {
        //////                    if (docContMdl.docnombre != " ")
        //////                    {
        //////                        Dictionary<string, object> dicParam = new Dictionary<string, object>();
        //////                        dicParam.Add(DButil.SIT_DOC_DOCUMENTO_COL.DOCNOMBRE, docContMdl.docnombre);
        //////                        dicParam.Add(DButil.SIT_DOC_DOCUMENTO_COL.DOCMD5, docContMdl.docmd5);
        //////                        iDocId = (int)docDao.dmlSelectDocNombreMD5(dicParam);
        //////                    }
        //////                    else
        //////                        iDocId = 0;

        //////                    // VERIFICAR QUE NO EXISTA EL DOCUMENTO
        //////                    if (iDocId == 0)
        //////                    {
        //////                        if (docContMdl.docnombre != " " && docContMdl.doc_contenido != null)
        //////                            docContMdl.docruta = admDocNeg.Grabar(docContMdl);

        //////                        docDao.dmlAgregar(docContMdl);
        //////                        iDocId = docDao.iSecuencia;
        //////                    }
        //////                    else
        //////                    {
        //////                        // REEMPLAZAR
        //////                        docDao.dmlEditar(docContMdl);
        //////                    }
        //////                    ////////SIT_DOC_ARISTA docAriMdl = new SIT_DOC_ARISTA( solclave: _afdEdoDataMdl.ID_folio, docclave: iDocId, ariclave: iClaArista);
        //////                    ////////docAristaDao.dmlAgregar(docAriMdl);
        //////                }
        //////            }
        //////        }
        //////        catch (Exception ex)
        //////        {
        //////            _sMsjError = ex.ToString();
        //////            throw new Exception("Error al grabar al grabar un documento " + ex.ToString());
        //////        }
        //////    }
        //////    return true;
        //////}


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
                                //Dictionary<string, object> dicParam = new Dictionary<string, object>();
                                //dicParam.Add(DButil.SIT_DOC_DOCUMENTO_COL.DOCNOMBRE, docContMdl.docnombre);
                                //dicParam.Add(DButil.SIT_DOC_DOCUMENTO_COL.DOCMD5, docContMdl.docmd5);
                                //iDocId = (int)docDao.dmlSelectDocNombreMD5(dicParam);

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

        protected Object Turnar(AfdEdoDataMdl afdDataMdl)
        {
            object oResultado = null;

            
            Int32 iTipoAristaIni = afdDataMdl.rtpclave;
            
            SIT_RED_NODO nodoInicial = _nodoDao.dmlSelectNodoID(afdDataMdl.AFDnodoActMdl.nodclave);
            List<Tuple<int, string, int>> lstPersonasTurnar = _afdEdoDataMdl.dicAuxRespuesta[ProcesoGralDao.PARAM_LISTA_TURNAR] as List<Tuple<int, string, int>>;

            //foreach (Tuple<int, string, int> areaTurnar in afdDataMdl.lstPersonasTurnar)
            foreach (Tuple<int, string, int> areaTurnar in lstPersonasTurnar )
            {                
                afdDataMdl.ID_EstadoActual = (int)nodoInicial.nedclave;
                afdDataMdl.AFDnodoActMdl = nodoInicial;
                afdDataMdl.rtpclave = iTipoAristaIni;
                afdDataMdl.usrClaveDestino = areaTurnar.Item1;
                afdDataMdl.Observacion = areaTurnar.Item2;
                ////afdDataMdl.ID_AreaDestPerfil = areaTurnar.;
                afdDataMdl.ID_AreaDestino = areaTurnar.Item3;                
                afdDataMdl.ID_Capa = nodoInicial.nodcapa;


                oResultado = AccionBase(true);
                ///oResultado = NodoEjecutarProceso(ref afdDataMdl);
            }

            return oResultado;
        }
    }
}

