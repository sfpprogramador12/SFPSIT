using SFP.Persistencia;
using SFP.Persistencia.Model;
using SFP.Persistencia.Util;
using SFP.SIT.SERV.Dao.DOC;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Model.DOC;
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

namespace SFP.SIT.SERV.Dao
{
    public class ProcesoGralDao : BaseFunc
    {
        public ProcesoGralDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        { }

        public const string PARAM_NODCLAVE = "NODCLAVE";

         
        public const string PARAM_DICRESPDOC = "DICRESPDOC";
        public const string PARAM_SHAPOINTMDL = "SHAPOINTMDL";
        public const string PARAM_FECHA = "PARAM_FECHA";
        public const string PARAM_RESP_ESTADO = "RESP_ESTADO";

        public const string PARAM_RUTA_ARCHIVOS = "RUTA_ARCHIVO";

        /* NUEVO NOMENCLATURA */
        public const string PARAM_RESP_RESPUESTA = "RESP_RESPUESTA";
        public const string PARAM_RESP_TURNAR = "RESP_TURNAR";
        public const string PARAM_RESP_GENERAL = "RESP_GENERAL";
        public const string PARAM_RESP_INEXISTENCIA = "RESP_INEXISTENCIA";
        public const string PARAM_RESP_RESERVA = "RESP_RESERVA";
        public const string PARAM_RESP_CONFIDENCIAL = "RESP_CONFIDENCIAL";
        

        public const string PARAM_RESP_LSTDETALLE = "RESP_LSTDETALLE";
        public const string PARAM_RESP_ASIGNAR = "RESP_ASIGNAR";
        public const string PARAM_RESP_CONFIDENCIAL_ITEM = "RESP_CONFIDENCIAL_ITEM";
        public const string PARAM_RESP_DETALLE = "RESP_DETALLE";
        public const string PARAM_DOC_CONTENIDO = "DOC_CONTENIDO";
        public const string PARAM_DOC_CONTENIDO_ITEM = "DOC_CONTENIDO_ITEM";

        public const string PARAM_OPERACION = "OPERACION";
        public const string PARAM_ENTIDAD = "ENTIDAD";
        public const string PARAM_LISTA_TURNAR = "LISTA_TURNAR";

        public const string PARAM_DIC_RESP_DETALLE = "DIC_RESP_DETALLE";
        public const string PARAM_DIC_DOCUMENTO = "DIC_DOCUMENTO";
        public const string PARAM_RED_NODORESP = "RED_NODORESP";
        
        public const int OP_INSERTAR = 1;
        public const int OP_EDITAR = 2;
        public const int OP_BORRAR = 3;

   
        public int BorrarRespuesta(Dictionary<string, object> dicParam)
        {
            Int64 repClave = (Int64)dicParam[DButil.SIT_RESP_RESPUESTA_COL.REPCLAVE];
            int rtpClave = (int)dicParam[DButil.SIT_RESP_RESPUESTA_COL.RTPCLAVE];
            Int64 nodClave = (int)dicParam[DButil.SIT_RED_NODORESP_COL.NODCLAVE];

            SIT_RED_NODORESPDao nodoRespDao = new SIT_RED_NODORESPDao(_cn, _transaction, _sDataAdapter);
            SIT_RESP_RESPUESTADao respRespDao = new SIT_RESP_RESPUESTADao(_cn, _transaction, _sDataAdapter);

            if (rtpClave == Constantes.Respuesta.RIA_AREA ||
                rtpClave == Constantes.Respuesta.AMPLIACION_PLAZO || 
                rtpClave == Constantes.Respuesta.INCOMPETENCIA_PARCIAL_AREA ||
                rtpClave == Constantes.Respuesta.INCOMPETENCIA_TOTAL_AREA)
            {
                SIT_RESP_GRALDao respDatosDao = new SIT_RESP_GRALDao(_cn, _transaction, _sDataAdapter);
                respDatosDao.dmlBorrarID(repClave);
            }
            else if ( rtpClave == Constantes.Respuesta.INFO_CONFIDENCIAL  ||
               rtpClave == Constantes.Respuesta.INFO_CONFIDENCIAL_PARCIAL )
            {
                SIT_RESP_DETALLEDao respDetalleDao = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);
                respDetalleDao.dmlBorrarRegistros(new SIT_RESP_DETALLE { repclave = repClave });
            }
            else if (rtpClave == Constantes.Respuesta.INFO_RESERVADA ||
                rtpClave == Constantes.Respuesta.INFO_RESERVADA_PARCIAL)
            {
                SIT_RESP_DETALLEDao respDetalleDao = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);
                respDetalleDao.dmlBorrarRegistros(new SIT_RESP_DETALLE { repclave = repClave });

                SIT_RESP_RESERVADao respReservaDao = new SIT_RESP_RESERVADao(_cn, _transaction, _sDataAdapter);
                respReservaDao.dmlBorrar(new SIT_RESP_RESERVA { repclave = repClave });

            }
            else if (rtpClave == Constantes.Respuesta.INEXISTENCIA_EN_AREA)
            {
                SIT_RESP_DETALLEDao respDetalleDao = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);
                respDetalleDao.dmlBorrarRegistros(new SIT_RESP_DETALLE { repclave = repClave });

                SIT_RESP_INEXISTENCIADao respInexistenciaDao = new SIT_RESP_INEXISTENCIADao(_cn, _transaction, _sDataAdapter);
                respInexistenciaDao.dmlBorrar(new SIT_RESP_INEXISTENCIA { repclave = repClave });
            }
            else if (rtpClave == Constantes.Respuesta.PUBLICA)
            {
                SIT_RESP_GRALDao respGralDao = new SIT_RESP_GRALDao(_cn, _transaction, _sDataAdapter);
                respGralDao.dmlBorrar(new SIT_RESP_GRAL { repclave = repClave });
            }



            nodoRespDao.dmlBorrar(new SIT_RED_NODORESP { repclave = repClave, nodclave = nodClave });
            respRespDao.dmlBorrar(new SIT_RESP_RESPUESTA { repclave = repClave });

            // EL ARCHIVO QUEDA PENDIENTE......
            // EL ARCHIVO QUEDA PENDIENTE......
            // EL ARCHIVO QUEDA PENDIENTE......
            // EL ARCHIVO QUEDA PENDIENTE......
            // EL ARCHIVO QUEDA PENDIENTE......
            // EL ARCHIVO QUEDA PENDIENTE......
            return 1;
        }

        /* *****************************  FUNCIONES NUEVAS  *****************************  */
        /* Y DEPENDEN DE LA ENTIDAD SEGUNSEA LA RESPUESTA */
        /* *****************************    *****************************  */
        public long AdmConfItemRegistro(Dictionary<string, object> dicDatos)
        {
            long lRes = 0;
            int iOperacion = (int)dicDatos[PARAM_OPERACION];
            SIT_RESP_DETALLE respDetalle = dicDatos[PARAM_RESP_DETALLE] as SIT_RESP_DETALLE;
            SIT_RESP_DETALLEDao respDetalleDao = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);

            DocContenidoMdl oDocContenido = null;
            if (dicDatos.ContainsKey(PARAM_DOC_CONTENIDO_ITEM))
            {
                oDocContenido = dicDatos[PARAM_DOC_CONTENIDO_ITEM] as DocContenidoMdl;
            }

            if (iOperacion == OP_INSERTAR)
            {                
                SIT_RESP_RESPUESTA respRespuesta = dicDatos[PARAM_RESP_RESPUESTA] as SIT_RESP_RESPUESTA;
                if (respRespuesta.repclave == 0)
                {
                    lRes = InsertarRegistro(dicDatos);
                    respRespuesta.repclave = lRes;
                    dicDatos[PARAM_RESP_RESPUESTA] = respRespuesta;
                    respDetalle.repclave = lRes;
                }

                if (oDocContenido != null)
                {
                    oDocContenido = GrabarArchivo(dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], oDocContenido);
                    respDetalle.docclave = oDocContenido.docclave;
                    respDetalle.repclave = respRespuesta.repclave;
                    lRes = respRespuesta.repclave;
                }
                
                respDetalleDao.dmlAgregar(respDetalle);
            }
            ////else if (iOperacion == OP_EDITAR)
            ////{
            ////    NO EXISTE LA EDICIÓN..
            ////}
            else if (iOperacion == OP_BORRAR)
            {
                CfgSharePointMdl oSharePoint = dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl;
                DateTime dtFecha = (DateTime)dicDatos[PARAM_FECHA];

                if (oDocContenido != null)
                {
                    BorrarDocAnterior(respDetalle.docclave, oSharePoint, dtFecha, dicDatos[PARAM_RUTA_ARCHIVOS].ToString());
                }
                respDetalleDao.dmlBorrar(respDetalle);
            }
            return lRes;
        }

        public long AdmRegistro(Dictionary<string, object> dicDatos)
        {
            int iOperacion = (int)dicDatos[PARAM_OPERACION];
            long lRes = 0;

            if (iOperacion == OP_INSERTAR)
            {
                lRes = InsertarRegistro(dicDatos);
            }
            else if (iOperacion == OP_EDITAR)
            {
                lRes= EditarRegistro(dicDatos);
            }
            else if (iOperacion == OP_BORRAR)
            {
                lRes = BorrarRegistro(dicDatos);
            }
            return lRes; 
        }

        // ME FALTA LA LISTA
        public long InsertarRegistro(Dictionary<string, object> dicDatos )
        {
            long lRespClave = 0;

            // DOCUMENTO
            DocContenidoMdl oDocContenido = null;
            string sTipo = dicDatos[PARAM_ENTIDAD] as string;

            if (dicDatos.ContainsKey(PARAM_DOC_CONTENIDO))
            {
                oDocContenido = dicDatos[PARAM_DOC_CONTENIDO] as DocContenidoMdl;
                oDocContenido = GrabarArchivo(dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], oDocContenido);
            }

            SIT_RESP_RESPUESTADao respRespDao = new SIT_RESP_RESPUESTADao(_cn, _transaction, _sDataAdapter);
            SIT_RESP_RESPUESTA respRespuesta = dicDatos[PARAM_RESP_RESPUESTA] as SIT_RESP_RESPUESTA;
            if (oDocContenido != null)
            {
                respRespuesta.docclave = oDocContenido.docclave;
            }

            respRespDao.dmlAgregar(respRespuesta);
            lRespClave = respRespDao.iSecuencia;

            SIT_RED_NODORESP oNodoResp = dicDatos[PARAM_RED_NODORESP] as SIT_RED_NODORESP;
            oNodoResp.repclave = lRespClave;
            new SIT_RED_NODORESPDao(_cn, _transaction, _sDataAdapter).dmlAgregar(oNodoResp);

            // ************************************************************
            //          AQUI GRABAMOS CON BASE A LAS TABLAS
            // ************************************************************
            if (sTipo == PARAM_RESP_GENERAL)
            {
                SIT_RESP_GRAL oRespGral = dicDatos[PARAM_RESP_GENERAL] as SIT_RESP_GRAL;
                oRespGral.repclave = lRespClave;
                new SIT_RESP_GRALDao(_cn, _transaction, _sDataAdapter).dmlAgregar(oRespGral);
            }
            else if (sTipo == PARAM_RESP_ASIGNAR)
            {
                SIT_RESP_TURNAR oFinal = dicDatos[PARAM_RESP_TURNAR] as SIT_RESP_TURNAR;
                oFinal.repclave = lRespClave;
                new SIT_RESP_TURNARDao(_cn, _transaction, _sDataAdapter).dmlAgregar(oFinal);
            }
            else if (sTipo == PARAM_RESP_TURNAR)
            {
                List<Tuple<int, string, int>> lstPersonasTurnar = dicDatos[PARAM_LISTA_TURNAR] as List<Tuple<int, string, int>>;

                SIT_RESP_TURNARDao respTurnarlDao = new SIT_RESP_TURNARDao(_cn, _transaction, _sDataAdapter);
                // ITERATE para los datos,
                foreach (Tuple<int, string, int> tpDatos in lstPersonasTurnar)
                {
                    SIT_RESP_TURNAR oTurnar = new SIT_RESP_TURNAR();
                    oTurnar.araclave = tpDatos.Item3;
                    oTurnar.turinstruccion = tpDatos.Item2;
                    oTurnar.usrclave = tpDatos.Item1;
                    oTurnar.repclave = lRespClave;
                    respTurnarlDao.dmlAgregar(oTurnar);
                }
            }
            else if (sTipo == PARAM_RESP_INEXISTENCIA)
            {
                SIT_RESP_INEXISTENCIA resInex = dicDatos[PARAM_RESP_INEXISTENCIA] as SIT_RESP_INEXISTENCIA;
                resInex.repclave = lRespClave;
                new SIT_RESP_INEXISTENCIADao(_cn, _transaction, _sDataAdapter).dmlAgregar(resInex);

                Dictionary<String, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>> dicRespDatos = dicDatos[PARAM_RESP_LSTDETALLE] as Dictionary<String, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>>;
                SIT_RESP_DETALLEDao detalleDao = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);

                foreach (KeyValuePair<string, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>> entry in dicRespDatos)
                {
                    // do something with entry.Value or entry.Key
                    SIT_RESP_DETALLE oDetalle = entry.Value.Item1;
                    oDetalle.repclave = lRespClave;
                    if (entry.Value.Item2 != null)
                    {
                        oDocContenido = GrabarArchivo(dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], entry.Value.Item2);
                        oDetalle.docclave = oDocContenido.docclave;
                    }
                    detalleDao.dmlAgregar(oDetalle);
                }
            }
            else if (sTipo == PARAM_RESP_RESERVA)
            {
                SIT_RESP_RESERVA resReserva = dicDatos[PARAM_RESP_RESERVA] as SIT_RESP_RESERVA;
                resReserva.repclave = lRespClave;
                new SIT_RESP_RESERVADao(_cn, _transaction, _sDataAdapter).dmlAgregar(resReserva);
                SIT_RESP_DETALLEDao oResDetalle = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);

                Dictionary<string, DocContenidoMdl> dicDocumento = dicDatos[PARAM_DIC_DOCUMENTO] as Dictionary<string, DocContenidoMdl>;

                foreach (KeyValuePair<string, SIT_RESP_DETALLE> entry in dicDatos[PARAM_DIC_RESP_DETALLE] as Dictionary<string, SIT_RESP_DETALLE>)
                {
                    SIT_RESP_DETALLE oDetalle = entry.Value;
                    oDetalle.repclave = lRespClave;
                    // BUSCAMOS SI EXISTE UN DOCUMENTO
                    if (dicDocumento.ContainsKey(entry.Key))
                    {
                        //Existe el documneto                        
                        oDocContenido = GrabarArchivo(dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], dicDocumento[entry.Key]);
                        oDetalle.docclave = oDocContenido.docclave;
                    }
                    oResDetalle.dmlAgregar(oDetalle);
                }
            }


            return lRespClave;
        }

        private int BorrarDocAnterior(long? docClave, CfgSharePointMdl oSharePoint, DateTime dtFecha, String sCarpeta)
        {
            if (docClave!= null)
            {
                SIT_DOC_DOCUMENTO docAntes = new SIT_DOC_DOCUMENTO() { docclave = (long)docClave };
                docAntes = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter).dmlSelectID(docAntes) as SIT_DOC_DOCUMENTO;
                docAntes.docruta = sCarpeta + docAntes.docruta;
                return BorrarArchivo(oSharePoint, dtFecha, docAntes);
            }
            return 0;
        }

        private int ActualizarDetalle( Dictionary<String, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>> dicDatos, CfgSharePointMdl oSharePoint, DateTime dtFecha, String sCarpeta)
        {
            int iEditTotal = 0;

            DocContenidoMdl oDocContenido = null;
            SIT_RESP_DETALLEDao detalleDao = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);

            if (dicDatos != null)
            {
                foreach (KeyValuePair<string, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>> entry in dicDatos)
                {
                    // do something with entry.Value or entry.Key
                    SIT_RESP_DETALLE oDetalle = entry.Value.Item1;
                    if (entry.Value.Item2 != null)
                    {
                        BorrarDocAnterior(oDetalle.docclave, oSharePoint, dtFecha, sCarpeta);
                        oDocContenido = GrabarArchivo(oSharePoint, dtFecha, entry.Value.Item2);
                        oDetalle.docclave = oDocContenido.docclave;
                    }
                    detalleDao.dmlEditar(oDetalle);
                    iEditTotal++;
                }
            }

            return iEditTotal;
        }


        private long BorrarRegistro(Dictionary<string, object> dicDatos)
        {
            string sTipo = dicDatos[PARAM_ENTIDAD] as string;

            SIT_RESP_RESPUESTA oRespuesta = dicDatos[PARAM_RESP_RESPUESTA] as SIT_RESP_RESPUESTA;
            if (sTipo == PARAM_RESP_GENERAL)
            {
                SIT_RESP_GRALDao respGralDao = new SIT_RESP_GRALDao(_cn, _transaction, _sDataAdapter);
                SIT_RESP_GRAL oFinal = dicDatos[PARAM_RESP_GENERAL] as SIT_RESP_GRAL;
                oFinal.repclave = oRespuesta.repclave;                
                respGralDao.dmlBorrarID(oFinal.repclave);   // borrrar RESPUESTA GENERAL
            }
            else if (sTipo == PARAM_RESP_INEXISTENCIA)
            {
                SIT_RESP_DETALLEDao respDetalleDao = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);
                List<SIT_RESP_DETALLE> lstDetalle = respDetalleDao.dmlSelectRespID(oRespuesta.repclave);

                SIT_DOC_DOCUMENTODao documentoDao = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter);


                foreach (SIT_RESP_DETALLE resDetalle in lstDetalle)
                {
                    SIT_DOC_DOCUMENTO oDocContAct = documentoDao.dmlSelectID( new SIT_DOC_DOCUMENTO { docclave  = (long)resDetalle.docclave });
                    BorrarArchivo(dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], oDocContAct);
                }

                SIT_RESP_INEXISTENCIADao respInexDao = new SIT_RESP_INEXISTENCIADao(_cn, _transaction, _sDataAdapter);
                SIT_RESP_INEXISTENCIA oFinal = dicDatos[PARAM_RESP_INEXISTENCIA] as SIT_RESP_INEXISTENCIA;
                oFinal.repclave = oRespuesta.repclave;
                respInexDao.dmlBorrar( oFinal.repclave);   // borrrar RESPUESTA GENERAL

            }
                        
            SIT_RED_NODORESP oNodoResp = new SIT_RED_NODORESP();
            oNodoResp.repclave = oRespuesta.repclave;
            oNodoResp.nodclave = (long)dicDatos[PARAM_NODCLAVE];

            // borrar Nodo Respueta
            SIT_RED_NODORESPDao nodoRespDao = new SIT_RED_NODORESPDao(_cn, _transaction, _sDataAdapter);
            nodoRespDao.dmlBorrar(oNodoResp);

            DocContenidoMdl oDocContenido = dicDatos[PARAM_DOC_CONTENIDO] as DocContenidoMdl;
            BorrarArchivo(dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], oDocContenido);

            return oRespuesta.repclave;
        }
  
        private long EditarRegistro(Dictionary<string, object> dicDatos)
        {
            long lRespClave = 0;            
            string sTipo = dicDatos[PARAM_ENTIDAD] as string;
            SIT_RESP_RESPUESTA respRespuesta = dicDatos[PARAM_RESP_RESPUESTA] as SIT_RESP_RESPUESTA;

            // DOCUMENTO            
            DocContenidoMdl oDocContenido = null;

            if (sTipo == PARAM_RESP_GENERAL)
            {
                SIT_RESP_GRALDao respGralDao = new SIT_RESP_GRALDao(_cn, _transaction, _sDataAdapter);
                SIT_RESP_GRAL oFinal = dicDatos[PARAM_RESP_GENERAL] as SIT_RESP_GRAL;
                oFinal.repclave = respRespuesta.repclave;
                respGralDao.dmlEditar(oFinal);
            }
            else if (sTipo == PARAM_RESP_INEXISTENCIA)
            {                
                SIT_RESP_INEXISTENCIA resInex = dicDatos[PARAM_RESP_INEXISTENCIA] as SIT_RESP_INEXISTENCIA;
                resInex.repclave = respRespuesta.repclave;
                new SIT_RESP_INEXISTENCIADao(_cn, _transaction, _sDataAdapter).dmlEditar(resInex);

                ActualizarDetalle( dicDatos[PARAM_RESP_LSTDETALLE] as Dictionary<String, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>>,
                    dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], dicDatos[PARAM_RUTA_ARCHIVOS].ToString());
               
            }
            else if (sTipo == PARAM_RESP_RESERVA)
            {
                SIT_RESP_RESERVA resRsv = dicDatos[PARAM_RESP_RESERVA] as SIT_RESP_RESERVA;
                resRsv.repclave = respRespuesta.repclave;
                new SIT_RESP_RESERVADao(_cn, _transaction, _sDataAdapter).dmlEditar(resRsv);

                ActualizarDetalle(dicDatos[PARAM_DIC_RESP_DETALLE] as Dictionary<String, Tuple<SIT_RESP_DETALLE, DocContenidoMdl>>,
                    dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], dicDatos[PARAM_RUTA_ARCHIVOS].ToString());

            }


            SIT_RESP_RESPUESTADao respRespDao = new SIT_RESP_RESPUESTADao(_cn, _transaction, _sDataAdapter);            
            SIT_RESP_RESPUESTA respRespAnt = respRespDao.dmlSelectID(new SIT_RESP_RESPUESTA { repclave = respRespuesta.repclave }) as SIT_RESP_RESPUESTA;

            if (dicDatos.ContainsKey(PARAM_DOC_CONTENIDO))
            {
                if (dicDatos[PARAM_DOC_CONTENIDO] != null)
                {
                    oDocContenido = dicDatos[PARAM_DOC_CONTENIDO] as DocContenidoMdl;
                    oDocContenido = GrabarArchivo(dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], oDocContenido);

                    if (oDocContenido != null)
                    {
                        if (respRespAnt.docclave != null)
                        {
                            SIT_DOC_DOCUMENTO docAntes = new SIT_DOC_DOCUMENTO() { docclave = (long)respRespAnt.docclave };
                            docAntes = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter).dmlSelectID(docAntes) as SIT_DOC_DOCUMENTO;
                            docAntes.docruta = dicDatos[PARAM_RUTA_ARCHIVOS].ToString() + docAntes.docruta;
                            BorrarArchivo(dicDatos[PARAM_SHAPOINTMDL] as CfgSharePointMdl, (DateTime)dicDatos[PARAM_FECHA], docAntes);
                        }
                        respRespuesta.docclave = oDocContenido.docclave;

                        respRespDao.dmlEditar(respRespuesta);
                    }
                }
                else
                {
                    respRespDao.dmlEditarSinDoc(respRespuesta);
                }
            }
            else
            {
                respRespDao.dmlEditarSinDoc(respRespuesta);
            }


            return lRespClave;
        }

        private DocContenidoMdl GrabarArchivo(CfgSharePointMdl shaPointMdl, DateTime dtFolioCarpeta, DocContenidoMdl oDocContenido)
        {
            if (oDocContenido != null)
            {
                AdmDocContNeg admDocNeg = new AdmDocContNeg(shaPointMdl, dtFolioCarpeta);
                oDocContenido.docmd5 = admDocNeg.ObtenerMD5(oDocContenido);
                SIT_DOC_DOCUMENTODao documentoDao = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter);
                int iClaveDoc = (int)documentoDao.dmlSelectDocNombreMD5(oDocContenido.docmd5);
                if (iClaveDoc == 0)
                {
                    oDocContenido.docruta = admDocNeg.Grabar(oDocContenido);
                    documentoDao.dmlAgregar(oDocContenido);
                    oDocContenido.docclave = documentoDao.iSecuencia;
                }
                else
                {
                    oDocContenido.docclave = iClaveDoc;
                }

                return oDocContenido;
            }
            return null;
        }

        private int BorrarArchivo(CfgSharePointMdl shaPointMdl, DateTime dtFolioCarpeta, SIT_DOC_DOCUMENTO oDocContenido)
        {
            SIT_DOC_DOCUMENTODao documentoDao = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter);
            int docTotal = documentoDao.dmlBorrarDocTodos( oDocContenido.docclave);
            int iRes = 0;

            // Borramos el documento fisico
            if (docTotal > 0)
            {
                AdmDocContNeg admDocNeg = new AdmDocContNeg(shaPointMdl, dtFolioCarpeta);
                iRes = admDocNeg.BorrarArchivoFileSystem(oDocContenido.docruta, oDocContenido.docnombre);
            }
            return iRes;
        }

        public int BorrarArchivo(Dictionary<string, object> dicParam)
        {
            long repClaveAux = (long)dicParam[DButil.SIT_RESP_RESPUESTA_COL.REPCLAVE];
            long docClaveAux = (long)dicParam[DButil.SIT_RESP_RESPUESTA_COL.DOCCLAVE];
            string detClaveAux = dicParam[DButil.SIT_RESP_DETALLE_COL.DETCLAVE].ToString();
            string sRutaArchivo = dicParam[PARAM_RUTA_ARCHIVOS].ToString();

            

            if (dicParam[DButil.SIT_RESP_DETALLE_COL.DETCLAVE].ToString() == "")
            {
                SIT_RESP_RESPUESTADao respuestaDao = new SIT_RESP_RESPUESTADao(_cn, _transaction, _sDataAdapter);
                respuestaDao.dmlEditarArchivo(new SIT_RESP_RESPUESTA { docclave = null, repclave = repClaveAux });
            }
            else
            {
                SIT_RESP_DETALLEDao resDetalleDao = new SIT_RESP_DETALLEDao(_cn, _transaction, _sDataAdapter);


                SIT_RESP_DETALLE respDetAct = resDetalleDao.dmlSelectID(new SIT_RESP_DETALLE { repclave = repClaveAux, detclave = detClaveAux } ) as SIT_RESP_DETALLE;
                docClaveAux = (long) respDetAct.docclave;

                resDetalleDao.dmlEditarArchivo(new SIT_RESP_DETALLE { docclave = null, repclave = repClaveAux, detclave = detClaveAux });

                //  aqui busco el registro anterior



            }

            SIT_DOC_DOCUMENTODao documentoDao = new SIT_DOC_DOCUMENTODao(_cn, _transaction, _sDataAdapter);
            SIT_DOC_DOCUMENTO docAux = documentoDao.dmlSelectID(new SIT_DOC_DOCUMENTO {  docclave = docClaveAux } ) as SIT_DOC_DOCUMENTO;
            string[] aiFecha = docAux.docruta.Split('\\');

            docAux.docruta = sRutaArchivo + docAux.docruta;

            DateTime dtFecAux = new DateTime( Convert.ToInt32(aiFecha[1]), Convert.ToInt32(aiFecha[2]), Convert.ToInt32(aiFecha[3]));
            return BorrarArchivo(dicParam[PARAM_SHAPOINTMDL] as CfgSharePointMdl, dtFecAux, docAux);


            // ME FALTA BORRar el archivo... del la ruta de documentos viene en blanco

        }
   }
}
