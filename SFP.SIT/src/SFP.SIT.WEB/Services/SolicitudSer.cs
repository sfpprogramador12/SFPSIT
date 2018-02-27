using SFP.Persistencia.Servicio;
using SFP.SIT.AFD.Core;
using SFP.SIT.SERV.Dao.DOC;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Dao.SNT;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using SFP.SIT.WEB.Models;
using SFP.SIT.WEB.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Services
{
    public class SolicitudSer
    {
        public const int RENGLON = 0;
        public const int COLUMNA = 1;


        protected DmlDbSer _sitDmlDbSer;
        public SolicitudSer(DmlDbSer sitSfpSer)
        {
            _sitDmlDbSer = sitSfpSer;
        }

        public object[] DatosSeguimiento(Int64 iFolio)
        {
            object[] aoDatos = new object[3];
            Dictionary<string, object> dicParam = new Dictionary<string, object>();

            dicParam.Add(DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE, iFolio);
            dicParam.Add(SIT_RED_ARISTADao.PARAM_FECHA, DateTime.Now);

            Dictionary<Int64, RedAristaSegMdl> dicArista = (Dictionary<Int64, RedAristaSegMdl>)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectSeguimiento), dicParam);
            

             aoDatos[0] = new Dictionary<Int64, RedAristaSegMdl>(dicArista);

            Dictionary<Int64, FrmSegNodoMdl> dicSegNodo = new Dictionary<Int64, FrmSegNodoMdl>();

            FrmSegNodoMdl segNodoPadreMdl = null;
            Int64 iArista = 0;

            foreach (KeyValuePair<Int64, RedAristaSegMdl> entry in dicArista)
            {
                RedAristaSegMdl aristaSegMdl = entry.Value;

                iArista = aristaSegMdl.Arista;

                dicSegNodo.Add(aristaSegMdl.Origen, new FrmSegNodoMdl(aristaSegMdl.Origen, 0, 0, aristaSegMdl.OrigenSigla, 1, aristaSegMdl.FecIni.ToString("dd-MM-yyyy"), aristaSegMdl.Atendido, aristaSegMdl.NodoEstado));
                segNodoPadreMdl = new FrmSegNodoMdl(aristaSegMdl.Destino, 0, 1, aristaSegMdl.DestinoSigla, 1, aristaSegMdl.FecIni.ToString("dd-MM-yyyy"), aristaSegMdl.Atendido, aristaSegMdl.NodoEstado);
                dicSegNodo.Add(aristaSegMdl.Destino, segNodoPadreMdl);
                break;
            }

            if (segNodoPadreMdl != null)
            {
                HashSet<long> hssNodoVisitado = new HashSet<long>();
                hssNodoVisitado.Add(segNodoPadreMdl.ID);

                int[] iCoordXY = new int[2];
                iCoordXY = GenerarEstructura(ref dicArista, ref dicSegNodo, iArista, segNodoPadreMdl.ID, 0, 1, ref hssNodoVisitado, iCoordXY);
                aoDatos[1] = dicSegNodo;
                aoDatos[2] = iCoordXY;

            }

            return aoDatos;
        }

        public int[] GenerarEstructura(ref Dictionary<Int64, RedAristaSegMdl> dicSeguimiento, ref Dictionary<Int64, FrmSegNodoMdl> dicSegNodo, Int64 iArista, Int64 iPadre, Int32 iRen, Int32 iCol, ref HashSet<long> hssNodoVisitado, int[] iCoordXY)
        {
            iCol++;
            int iCuenta = 1;
            dicSeguimiento.Remove(iArista);
            List<RedAristaSegMdl> lstNodoProd = BuscarNodosHijos(ref dicSeguimiento, iPadre);
            foreach (RedAristaSegMdl aristaSegMdl in lstNodoProd)
            {
                if (iCuenta > 1)
                    iRen++;

                iCuenta++;
                hssNodoVisitado.Add(iPadre);

                // Si cambia de proceso agregar una columna mas
                // Si cambia de proceso agregar una columna mas

                if (dicSegNodo.ContainsKey(aristaSegMdl.Destino) == false)
                    dicSegNodo.Add(aristaSegMdl.Destino, new FrmSegNodoMdl(aristaSegMdl.Destino, iRen, iCol, aristaSegMdl.DestinoSigla, 1, aristaSegMdl.FecFin.ToString("dd-MM-yyyy"), aristaSegMdl.Atendido, aristaSegMdl.NodoEstado));

                if (hssNodoVisitado.Contains(aristaSegMdl.Destino) == false)
                    iCoordXY = GenerarEstructura(ref dicSeguimiento, ref dicSegNodo, aristaSegMdl.Arista, aristaSegMdl.Destino, iRen, iCol, ref hssNodoVisitado, iCoordXY);

                if (iRen < iCoordXY[RENGLON])
                    iRen = iCoordXY[RENGLON];

                if (iCol > iCoordXY[COLUMNA])
                    iCoordXY[COLUMNA] = iCol;

            }
            hssNodoVisitado.Remove(iPadre);
            iCoordXY[RENGLON] = iRen;

            if (iCoordXY[COLUMNA] < iCol)
                iCoordXY[COLUMNA] = iCol;

            return iCoordXY;
        }

        public List<RedAristaSegMdl> BuscarNodosHijos(ref Dictionary<Int64, RedAristaSegMdl> dicSeguimiento, Int64 iPadre)
        {
            List<RedAristaSegMdl> lstAristaSeg = new List<RedAristaSegMdl>();

            foreach (KeyValuePair<Int64, RedAristaSegMdl> entry in dicSeguimiento)
            {
                if (entry.Value.Origen == iPadre)
                    lstAristaSeg.Add(entry.Value);
            }
            return lstAristaSeg;
        }

        /* *********************************************************************************************************
         * 
         *          GENERAR CONSULTAS
         * 
         *********************************************************************************************************** */
        public int ObtenerAreaPerfil(int iPerfil)
        {
            ////DataTable dtResultado;


            // NECESITO CREAR UN AREA POR PERFIL??????
            // NECESITO CREAR UN AREA POR PERFIL??????
            // NECESITO CREAR UN AREA POR PERFIL??????
            // NECESITO CREAR UN AREA POR PERFIL??????
            // NECESITO CREAR UN AREA POR PERFIL??????
            // NECESITO CREAR UN AREA POR PERFIL??????
            // NECESITO CREAR UN AREA POR PERFIL??????
            // NECESITO CREAR UN AREA POR PERFIL??????
            // NECESITO CREAR UN AREA POR PERFIL??????


            // O REALMENTE NO ESNECESAIRO
            // O REALMENTE NO ESNECESAIRO
            // O REALMENTE NO ESNECESAIRO
            // O REALMENTE NO ESNECESAIRO
            // O REALMENTE NO ESNECESAIRO
            // O REALMENTE NO ESNECESAIRO
            // O REALMENTE NO ESNECESAIRO



            /// ACTUALEMTNE MANEJAMSO UN ESTADO.. TINENE UN PERFIL ACCESO A UN ESTADO..
            /// ACTUALEMTNE MANEJAMSO UN ESTADO.. TINENE UN PERFIL ACCESO A UN ESTADO..
            /// /// ACTUALEMTNE MANEJAMSO UN ESTADO.. TINENE UN PERFIL ACCESO A UN ESTADO..
            /// /// ACTUALEMTNE MANEJAMSO UN ESTADO.. TINENE UN PERFIL ACCESO A UN ESTADO..
            /// /// ACTUALEMTNE MANEJAMSO UN ESTADO.. TINENE UN PERFIL ACCESO A UN ESTADO..
            /// 
            //////Dictionary<string, object> dicParam = new Dictionary<string, object>();
            //////dicParam.Add(DButil.SIT_ADM_PERFIL_COL.PERCLAVE, iPerfil);
            //////dicParam.Add(.PARAM_FECHA, DateTime.Now);
            //////dtResultado = (DataTable)_sitDmlDbSer.operEjecutar(nameof(SIT_ADM_AREADao), SIT_ADM_AREADao.OPE_SELECT_AREAS_POR_TIPO, dicParam);

            //////// Esta funcion solo trae la priemra area con el perfil bvuscado..
            //////return Convert.ToInt32(dtResultado.Rows[0]["araclave"]);

            return 0;
        }




            
        public SIT_RED_NODO ObtenerNodoFolioEdoPrc(long lFolio, int Edo, int iPrc)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            // BUSCAMOS EL NODO EL PROCESO ACTUAL
            dicParam.Add(DButil.SIT_RED_NODO_COL.SOLCLAVE, lFolio);
            //////dicParam.Add(DButil.SIT_RED_NODO_COL.prcclave, iPrc);
            //////dicParam.Add(DButil.SIT_RED_NODO_COL.nodclave, Edo);

            return  _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoFolioEstadoPrc), dicParam) as SIT_RED_NODO;
        }
        public RespNotificarViewModel ObtenerRespNotificar(long lFolio,  int iProcActual, string sSelect)
        {
            RespNotificarViewModel respNotificar = new RespNotificarViewModel("", 0, "", "", 0);

            // BUSCAMOS EL SEGUIMIENTO
            ////////Dictionary<string, object> dicParam = new Dictionary<string, object>();



            ////////DataTable dtDatos = null;

            ////////if (sSelect == nameof(SIT_RED_ARISTADao.dmlSelectRespDoc))
            ////////{
            ////////    dtDatos = (DataTable)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectRespDoc), dicParam);
            ////////}
            ////////else
            ////////    dtDatos = (DataTable)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectRespDocAristaAnt), dicParam);


            ////////if (dtDatos != null)
            ////////    if (dtDatos.Rows.Count > 0)
            ////////    {
            ////////        long iDocIdx = 0;

            ////////        if (dtDatos.Rows[0]["docclave"].ToString() != "")
            ////////        {
            ////////            iDocIdx = Convert.ToInt64(dtDatos.Rows[0]["docclave"]);
            ////////        }
            ////////        respNotificar = new RespNotificarViewModel(
            ////////            dtDatos.Rows[0]["rtpdescripcion"].ToString(), iDocIdx,
            ////////            dtDatos.Rows[0]["arimensaje"].ToString().Replace('\n', '\u0020').Replace('\r', '\u0020'),
            ////////            dtDatos.Rows[0]["docnombre"].ToString(), Convert.ToInt32(dtDatos.Rows[0]["rtpclave"]));
            ////////    }
            return respNotificar;
        }
        public SIT_SOL_SEGUIMIENTO ObtenerSeguimiento(long lFolio, int iClaProceso)
        {
            // BUSCAMOS EL SEGUIMIENTO
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE, lFolio);
            dicParam.Add(DButil.SIT_SOL_SOLICITUD_COL.PRCCLAVE, iClaProceso);
            return (SIT_SOL_SEGUIMIENTO)_sitDmlDbSer.operEjecutar<SIT_SOL_SEGUIMIENTODao>(nameof(SIT_SOL_SEGUIMIENTODao.dmlSelectSeguimientoPorID),  dicParam);
        }


        public string ObtenerRecRevision(long lFolio)
        {
            return JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_RREVISIONDao>(nameof(SIT_RESP_RREVISIONDao.dmlSelectTranspuesta), lFolio));
        }

        public string ObtenerAclaracion(long lFolio, int TipoAriOri, int TipoAriDes )
        {
            string sResultado = null;

            Dictionary<string, object> dcParam = new Dictionary<string, object>();
            dcParam.Add(DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE, lFolio);
            dcParam.Add(SIT_RED_ARISTADao.PARAM_ARITIPO_INFOADI, TipoAriOri);
            dcParam.Add(SIT_RED_ARISTADao.PARAM_ARITIPO_INFOADI_RESP, TipoAriDes);

            ////////Dictionary<int, string> dicAcl = _sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectDicInfoAdicResp), dcParam) as Dictionary<int,string>;
            Dictionary<int, string> dicAcl = new Dictionary<int, string>();

            if (dicAcl != null)
            {
                DataTable catalogoRet = new DataTable();
                catalogoRet.Columns.Add("titulo", typeof(string));
                catalogoRet.Columns.Add("valor", typeof(string));
                catalogoRet.Rows.Add("Info. solicitada", dicAcl[1].Replace("\"", "\\\""));
                catalogoRet.Rows.Add("Info. adicional", dicAcl[2].Replace("\"","\\\""));
                sResultado = JsonTransform.convertJson(catalogoRet);
            }
            else
                sResultado = JsonTransform.NO_RECORDS;

            return sResultado;
        }


        public string ObtenerSolicitudTranspuesta(long lSolicitud, out DateTime FecSolicitud)
        {            
            String sJsonDatos = null;
            DataTable dtSolicitud = (DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectSolicitudTranspuestaID), lSolicitud);

            FecSolicitud = new DateTime();
            for (int iIdx = 0; iIdx < dtSolicitud.Rows.Count; iIdx++)
            {                
                if (dtSolicitud.Rows[iIdx][0].ToString() == "Fecha aclaración")
                {
                    FecSolicitud = Convert.ToDateTime(dtSolicitud.Rows[iIdx][1]);
                    break;
                }
            }

            // Buscar documentos
            List<SIT_DOC_DOCUMENTO> lstDocIni = _sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectDocInicial), lSolicitud) as List<SIT_DOC_DOCUMENTO>;
            foreach (SIT_DOC_DOCUMENTO docMdl in lstDocIni) 
            {
                DataRow newRow = dtSolicitud.NewRow();
                newRow[0] = "Anexo";
                newRow[1] = docMdl.docnombre;
                dtSolicitud.Rows.InsertAt(newRow, 0);
            }

            sJsonDatos = JsonTransform.convertJson(dtSolicitud);
            return sJsonDatos;
        }


        
        public SIT_SOL_SOLICITUD ObtenerSolicitudID(long lSolicitud)
        {
            SIT_SOL_SOLICITUD solParam = new SIT_SOL_SOLICITUD();
            solParam.solclave = lSolicitud;
            return (SIT_SOL_SOLICITUD)_sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectID), solParam);
        }

        public string ObtenerAclaracion(long lSolicitud)
        {
            //////Dictionary<string, object> dcParam = new Dictionary<string, object>();
            //////dcParam.Add(DButil.SIT_RED_ARISTA_COL.solclave, lSolicitud);
            //////dcParam.Add(DButil.SIT_RED_ARISTA_COL.rtpclave, AfdConstantes.RESPUESTA.RECEPCION_INFO_ADICIONAL_SOLICITANTE);
            //////return JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectAristaFolioTipoAristaTrans),  dcParam));

            return "";
        }
        public SIT_RED_ARISTA ObtenerAristaFolioID(long lSolicitud, long lArista)
        {
            //////Dictionary<string, object> dcParam = new Dictionary<string, object>();
            //////dcParam.Add(DButil.SIT_RED_ARISTA_COL.solclave, lSolicitud);
            //////dcParam.Add(DButilSIT_RED_ARISTA_COL.ARICLAVE, lArista);
            //////return (SIT_RED_ARISTA)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectAristaID),  dcParam);
            return null;
        }

        public List<SIT_RED_ARISTA> ObtenerAristasIDnodoDestino(long lSolicitud, long lNodo)
        {
            ////////Dictionary<string, object> dcParam = new Dictionary<string, object>();
            ////////dcParam.Add(DButil.SIT_RED_ARISTA_COL.solclave, lSolicitud);
            ////////dcParam.Add(DButil.SIT_RED_ARISTA_COL.noddestino, lNodo);
            ////////return (List<SIT_RED_ARISTA>)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectAristaFolioDestino), dcParam);
            return null;
        }

        public string ObtenerProrrogaMensaje(long lFolio, long lNodoDestino)
        {
            ////////Dictionary<string, object> dcParam = new Dictionary<string, object>();
            ////////dcParam.Add(SIT_RED_ARISTADao.COL_solclave, lFolio);
            ////////dcParam.Add(SIT_RED_ARISTADao.COL_noddestino, lNodoDestino);
            ////////dcParam.Add(SIT_RED_ARISTADao.COL_rtpclave, AfdConstantes.RESPUESTA.CI_PRORROGA_AUTORIZAR);

            ////////SIT_RED_ARISTA aristaMdl = (SIT_RED_ARISTA)_sitDmlDbSer.operEjecutar(nameof(SIT_RED_ARISTADao), SIT_RED_ARISTADao.OPE_SELECT_ARISTA_POR_NODO_DESTNO, dcParam);

            ////////String sMensaje;
            ////////if (aristaMdl != null)
            ////////    sMensaje = "{\"records\":[{\"mensaje\":\"El Comité de Información concedio la ampliación de tiempos para esta solicitud. <br /> Por lo que se le solicita entrar al sistema INFOMEX y ejecutar la Ampliación\"}]}";
            ////////else
            ////////    sMensaje = "{\"records\":[{\"mensaje\":\"El Comité de Información rechazó una ampliación de tiempos\"}]}";

            ////////return sMensaje;
            return "";
        }

        public string ObtenerUTpendientes(long lFolio, long lNodo, int iPrcIdx, int iArea, int iPerfil)
        {
            ////Dictionary<string, object> dcParam = new Dictionary<string, object>();           
            ////dcParam.Add(SIT_RED_ARISTADao.PARAM_NO_ESTADOS, AfdConstantes.ESTADO.RECIBIR_NOTIFICACION + "," + AfdConstantes.ESTADO.UT_REGISTRAR_INAI);
            ////dcParam.Add(DButil.SIT_RED_ARISTA_COL.solclave, lFolio);
            ////dcParam.Add(DButil.SIT_RED_NODO_COL.nodclave, lNodo);
            ////dcParam.Add(SIT_RED_ARISTADao.PARAM_FECHA, DateTime.Now);

            ////return JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectAristaPendientes),  dcParam));
            return null;
        }
        public string ObtenerRespParaRecurso(long lFolio, long lNodo, int iPrcIdx)
        {
            //////Dictionary<string, object> dcParam = new Dictionary<string, object>();
            //////dcParam.Add(DButil.SIT_RED_ARISTA_COL.solclave, lFolio);
            //////dcParam.Add(DButil.SIT_RED_ARISTA_COL.noddestino, lNodo);
            //////dcParam.Add(DButil.SIT_RED_ARISTA_COL.prcclave, iPrcIdx);
            //////return JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectRespPrevRecRevision),  dcParam));

            return null;
        }


        public int ExisteAmpliacionPrevia(long lFolio, long lNodo, int iTipoArista)
        {
            //////Dictionary<string, object> dcParam = new Dictionary<string, object>();
            //////dcParam.Add(DButil.SIT_RED_ARISTA_COL.solclave, lFolio);
            //////dcParam.Add(DButil.SIT_RED_NODO_COL.nodclave, lNodo);
            //////dcParam.Add(DButil.SIT_RED_ARISTA_COL.rtpclave, iTipoArista);

            //////return (int)_sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlSelectAristaAccionPreviaArea), dcParam);

            return 1;
        }
        public string ObtenerDocumentosFolio(long lFolio)
        {
            return JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectDocID), lFolio));
        }

        public DocContenidoMdl ObtenerDocumentoID(int lClaDoc, String sRuta)
        {
            DataTable dtDatos = (DataTable)_sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlSelectDocID), lClaDoc);

            /// CAMBIAR POR OTRA ESTRUCTURA DE DATOS...
            if (dtDatos.Rows.Count == 1)
            {
                // verificar si existe el archivo esta en el FILESSYTEM o en SHAREPOINT
                string fileName = dtDatos.Rows[0].ItemArray[0].ToString();
                String sArchivoPath = sRuta + dtDatos.Rows[0].ItemArray[1].ToString() + "\\" + fileName;

                if (System.IO.File.Exists(sArchivoPath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(sArchivoPath);                    
                    return new DocContenidoMdl(fileBytes, dtDatos.Rows[0].ItemArray[2].ToString(), fileName);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public DocContenidoMdl ObtenerDocumentoNombre(string sArchivoRuta)
        {
            if (System.IO.File.Exists(sArchivoRuta))
            {
                string sId = DateTime.Now.ToString("HHMISS");
                string sNvoArchivo = sArchivoRuta + sId;
                byte[] fileBytes = null;

                try
                {
                    fileBytes = System.IO.File.ReadAllBytes(sNvoArchivo);
                }
                catch (IOException)
                {
                    File.Copy(sArchivoRuta, sNvoArchivo);
                    fileBytes = System.IO.File.ReadAllBytes(sNvoArchivo);
                    File.Delete(sNvoArchivo);
                }
                return new DocContenidoMdl(fileBytes, "application/octet-stream", "Logs");
            }
            else
                return null;
        }

        public string ObtenerAristaSubClasificacion(int iValor)
        {

            return "QUITE ESTA TABLA...";
            ////Dictionary<string, object> dcParam = new Dictionary<string, object>();
            ////dcParam.Add(DButil.SIT_RESP_TIPO_COL.KAR_SUBCLASIFICAR, iValor);
            ////return JsonTransform.convertJsonW2UI((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.), .OPE_SELECT_DT_SUBCLASIFICAR, dcParam));
        }
        public string ObtenerAristaTipo(int iValor, string sOmitir)
        {
            ////////Dictionary<string, object> dcParam = new Dictionary<string, object>();
            ////////dcParam.Add(DButil.SIT_RESP_TIPO_COL.KAR_TIPO, iValor);
            ////////dcParam.Add(SIT_RESP_TIPODao.PARAM_OMITIR, sOmitir);

            ////////return JsonTransform.convertJsonW2UI((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectComboTipoID),  dcParam));
            return null;
        }
        public string ObtenerAristaTipoID(int iValor)
        {
            ////SIT_RESP_TIPO aristaTipo = new SIT_RESP_TIPO();
            ////aristaTipo.rtpclave = iValor;
            ////return JsonTransform.convertJsonW2UI((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectID), aristaTipo));

            return null;
        }



    }
}
