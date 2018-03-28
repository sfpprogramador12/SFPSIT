using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFP.SIT.WEB.Injection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using SFP.SIT.WEB.Util;
using System.Data;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.edit;
using org.apache.pdfbox.pdmodel.font;
using org.apache.pdfbox.pdmodel.graphics.xobject;
using org.apache.pdfbox.exceptions;
using java.io;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Dao.REP;
using SFP.Persistencia.Model;
using ClosedXML;
using System.IO;
using OfficeOpenXml;

namespace SFP.SIT.WEB.Controllers
{
    public class ReporteController : SitBaseCtlr
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReporteController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<ReporteController> logger, IHostingEnvironment app, IHostingEnvironment hostingEnvironment) 
            : base(memCache, httpContextAccessor, logger, app)
        {
            _iUsuario = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Reporte
        [HttpGet]
        public ActionResult Seleccion()
        {            
            ViewBag.areas = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectComboFecActual), DateTime.Now) as List<ComboMdl>);
            ViewBag.tipoArista = JsonTransform.convertJsonDicToTable(_memCacheSIT.ObtenerDato(CacheWebSIT.DIC_RESP_TIPO) as Dictionary<int, string>);
            ViewBag.SolicitudTipo = JsonTransform.convertJson(_memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_TIPO) as List<ComboMdl>);            
            ViewBag.ProcesoTipo = JsonTransform.convertJson(_memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_PROCESO) as List<ComboMdl>);


            return View();
        }

        [HttpPost]
        public String CrearReporte(string rep, string status, string area, string tipsol, string tipres, string folio, string fecsolini, string fecsolfin, string semaforo)
        {
            ////Dictionary<string, string> dicParam = new Dictionary<string, string>();
            ////dicParam.Add(ReporteDao.COL_NUM_REPORTE, rep);
            ////dicParam.Add(ReporteDao.COL_STATUS_SOLICITUD, status);
            ////dicParam.Add(ReporteDao.COL_AREA, area);
            ////dicParam.Add(ReporteDao.COL_TIPO_SOLICITUD, tipsol);
            ////dicParam.Add(ReporteDao.COL_TIPO_RESPUESTA, tipres);
            ////dicParam.Add(ReporteDao.COL_FOLIO, folio);
            ////dicParam.Add(ReporteDao.COL_FECSOL_INI, fecsolini);
            ////dicParam.Add(ReporteDao.COL_FECSOL_FIN, fecsolfin);
            ////dicParam.Add(ReporteDao.COL_SEMAFORO, semaforo);

            ////DataTable dtReporte = (DataTable)_infoSfpSer.operEjecutar(typeof(ReporteDao), ReporteDao.OPE_SELECT_RECIBIDAS_SFP, dicParam);

            ////crearReporte(dtReporte);
            return "";
        }

        // Sera necesario leer un layout para los reportes
        private void crearPaginaReport1(String escudoPath, PDDocument document, DataTable dtReporteMdl, String sTitulo)
        {
            int posY = 50;
            int iCuenta = 0;
            int iParrafoY = 0;
            int iLimiteInferior = 56;
            int iContador = 0;
            int iParrafoWidth = 570;
            int iPagina = 1;
            PdfBoxParrafo parrafo;

            PDPageContentStream contentStream = null;
            PDFont font = PDType1Font.HELVETICA_BOLD;
            PDFont fontDatos = PDType1Font.HELVETICA;


            ////select solclave, CAST(segfecini AS DATE) segfecini,  eto_descripcion, sotdescripcion, rtpdescripcion, segdiassemaforo,  ");
            ////sbQuery.Append(" segsemaforocolor, LISTAGG(ka_sigla, ' | ') WITHIN GROUP (ORDER BY ka_sigla)  as Areas, soldes, soldat

            for (var skp = 0; skp < 15; skp++)
            {

                foreach (DataRow drDatos in dtReporteMdl.Rows)
                {
                    iContador++;

                    /* E N C A B E Z A D O */
                    if (posY < iLimiteInferior)
                    {
                        if (contentStream != null)
                            contentStream.close();

                        PDPage page = new PDPage();
                        document.addPage(page);
                        contentStream = new PDPageContentStream(document, page);
                        agregarEncabezado(document, contentStream, font, sTitulo, escudoPath);
                        posY = 710;

                        contentStream.beginText();
                        contentStream.setFont(fontDatos, 8);
                        contentStream.moveTextPositionByAmount(520, 15);
                        contentStream.drawString("Página : " + iPagina.ToString());
                        contentStream.endText();
                        iPagina++;
                    }

                    /* R E N G L O N   1 */

                    /* C O N T A D O R*/
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(20, posY);
                    contentStream.drawString("No.");
                    contentStream.endText();

                    contentStream.beginText();
                    contentStream.setFont(fontDatos, 8);
                    contentStream.moveTextPositionByAmount(34, posY);
                    contentStream.drawString(iContador.ToString());
                    contentStream.endText();

                    /* F O L I O */
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(56, posY);
                    contentStream.drawString("Folio:");
                    contentStream.endText();

                    contentStream.beginText();
                    contentStream.setFont(fontDatos, 8);
                    contentStream.moveTextPositionByAmount(80, posY);
                    contentStream.drawString(drDatos["solclave"].ToString());
                    contentStream.endText();

                    /* F E C H A    R E C E P C I O N */
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(135, posY);
                    contentStream.drawString("Fecha Recepción:");
                    contentStream.endText();

                    contentStream.beginText();
                    contentStream.setFont(fontDatos, 8);
                    contentStream.moveTextPositionByAmount(205, posY);
                    DateTime Fecha = (DateTime)drDatos["segfecini"];
                    contentStream.drawString(Fecha.ToString("dd/MM/yyyy hh:mm"));
                    contentStream.endText();

                    /* T I P O    D E   S O L I C I T U D  */
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(270, posY);
                    contentStream.drawString("Tipo de solicitud:");
                    contentStream.endText();

                    contentStream.beginText();
                    contentStream.setFont(fontDatos, 8);
                    contentStream.moveTextPositionByAmount(338, posY);
                    contentStream.drawString(drDatos["sotdescripcion"].ToString());
                    contentStream.endText();

                    /* D I A S   T R A N S C U R R I D O S  */
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(450, posY);
                    contentStream.drawString("Días:");
                    contentStream.endText();

                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(475, posY);
                    contentStream.drawString(drDatos["segdiassemaforo"].ToString());
                    contentStream.endText();

                    /* S E M A F O R O */
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(500, posY);
                    contentStream.drawString("Semaforo:");
                    contentStream.endText();

                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(545, posY);

                    if (Convert.ToInt32(drDatos["segdiassemaforo"]) == Constantes.Semaforo.SOLICITUD_SEMAFORO_IROJO)
                        contentStream.drawString("ROJO");
                    else if (Convert.ToInt32(drDatos["segdiassemaforo"]) == Constantes.Semaforo.SOLICITUD_SEMAFORO_IAMARILLO)
                        contentStream.drawString("AMARILLO");
                    else
                        contentStream.drawString("VERDE");

                    contentStream.endText();

                    /* R E N G L O N   2 */
                    posY = posY - 11;

                    /* D E S C R I P C I O N */
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(20, posY);
                    contentStream.drawString("Descripción:");
                    contentStream.endText();
                   
                    if (drDatos["soldes"] != null)
                    {
                        //parrafo = new PdfBoxParrafo(70, posY, iParrafoWidth, drDatos["soldes"].ToString()); /*Cambiar  DE NUEVO A TEXTO ORIGINAL*/
                        parrafo = new PdfBoxParrafo(70, posY, iParrafoWidth, "dada la respuestacon numero 0002700250316 se solicita la escolaridad primaria secundaria preparatoria licenciatura etc del la funcionaria dada la respuestacon numero 0002700250316 se solicita la escolaridad primaria secundaria preparatoria licenciatura etc del la funcionaria" +
                            "dada la respuestacon numaria preoria licenciatura etc del la funcionaria");
                        iParrafoY = 0;
                        List<String> lstLineas1 = parrafo.getLines();
                        foreach (string element in lstLineas1)
                        {
                            contentStream.beginText();
                            contentStream.setFont(fontDatos, 8);
                            contentStream.moveTextPositionByAmount(parrafo.getX(), (posY) - (11 * iParrafoY));
                            contentStream.drawString(element.TrimEnd());
                            contentStream.endText();
                            iParrafoY++;

                            if ((posY-(11*iParrafoY)) <= 30)
                            {
                                if (contentStream != null)
                                    contentStream.close();

                                PDPage page = new PDPage();
                                document.addPage(page);
                                contentStream = new PDPageContentStream(document, page);
                                agregarEncabezado(document, contentStream, font, sTitulo, escudoPath);
                                posY = 715;
                                iParrafoY = 0;

                                contentStream.beginText();
                                contentStream.setFont(fontDatos, 8);
                                contentStream.moveTextPositionByAmount(520, 15);
                                contentStream.drawString("Página : " + iPagina.ToString());
                                contentStream.endText();
                                iPagina++;
                            }
                        }
                        posY = (posY) - (11 * (iParrafoY));
                    }

                    /* E N C A B E Z A D O */
                    if (posY < iLimiteInferior)
                    {
                        if (contentStream != null)
                            contentStream.close();

                        PDPage page = new PDPage();
                        document.addPage(page);
                        contentStream = new PDPageContentStream(document, page);
                        agregarEncabezado(document, contentStream, font, sTitulo, escudoPath);
                        posY = 710;

                        contentStream.beginText();
                        contentStream.setFont(fontDatos, 8);
                        contentStream.moveTextPositionByAmount(520, 15);
                        contentStream.drawString("Página : " + iPagina.ToString());
                        contentStream.endText();
                        iPagina++;
                    }

                    if (drDatos["soldat"] != null)
                    {

                        /* O T R O */
                        contentStream.beginText();
                        contentStream.setFont(font, 8);
                        contentStream.moveTextPositionByAmount(20, posY);
                        contentStream.drawString("Otros datos:");
                        contentStream.endText();

                        parrafo = new PdfBoxParrafo(70, posY, iParrafoWidth, drDatos["soldat"].ToString());
                        iParrafoY = 0;
                        List<String> lstLinesB = parrafo.getLines();
                        foreach (string element in lstLinesB)
                        {
                            contentStream.beginText();
                            contentStream.setFont(fontDatos, 8);
                            contentStream.moveTextPositionByAmount(parrafo.getX(), (posY) - (11 * iParrafoY));
                            contentStream.drawString(element.TrimEnd());
                            contentStream.endText();
                            iParrafoY++;
                        }
                        posY = (posY) - (11 * iParrafoY);
                    }

                    /* E N C A B E Z A D O */
                    if (posY < iLimiteInferior)
                    {
                        if (contentStream != null)
                            contentStream.close();

                        PDPage page = new PDPage();
                        document.addPage(page);
                        contentStream = new PDPageContentStream(document, page);
                        agregarEncabezado(document, contentStream, font, sTitulo, escudoPath);
                        posY = 710;

                        contentStream.beginText();
                        contentStream.setFont(fontDatos, 8);
                        contentStream.moveTextPositionByAmount(520, 15);
                        contentStream.drawString("Página : " + iPagina.ToString());
                        contentStream.endText();
                        iPagina++;
                    }

                    /* R E N G L O N   3 */
                    /* M U L T I P L E   */
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(20, posY);
                    contentStream.drawString("Múltiple:");
                    contentStream.endText();

                    parrafo = new PdfBoxParrafo(70, posY, iParrafoWidth, drDatos["areas"].ToString());
                    iParrafoY = 0;
                    List<String> lstLinesC = parrafo.getLines();
                    foreach (string element in lstLinesC)
                    {
                        contentStream.beginText();
                        contentStream.setFont(fontDatos, 8);
                        contentStream.moveTextPositionByAmount(parrafo.getX(), (posY) - (11 * iParrafoY));
                        contentStream.drawString(element.TrimEnd());
                        contentStream.endText();
                        iParrafoY++;
                    }
                    posY = (posY) - (11 * iParrafoY);

                    /* E N C A B E Z A D O */
                    if (posY < iLimiteInferior)
                    {
                        if (contentStream != null)
                            contentStream.close();

                        PDPage page = new PDPage();
                        document.addPage(page);
                        contentStream = new PDPageContentStream(document, page);
                        agregarEncabezado(document, contentStream, font, sTitulo, escudoPath);
                        posY = 710;

                        contentStream.beginText();
                        contentStream.setFont(fontDatos, 8);
                        contentStream.moveTextPositionByAmount(520, 15);
                        contentStream.drawString("Página : " + iPagina.ToString());
                        contentStream.endText();
                        iPagina++;
                    }

                    /* R E N G L O N   4 */
                    /* R E S P U E S T A */
                    contentStream.beginText();
                    contentStream.setFont(font, 8);
                    contentStream.moveTextPositionByAmount(20, posY);
                    contentStream.drawString("Respuesta:");
                    contentStream.endText();

                    parrafo = new PdfBoxParrafo(70, posY, iParrafoWidth, "La solicitud no es de competencia de la unidad de enlace" +
                        "La solicitud no es de competencia de la unidad de enlace" +
                        "La solicide la unidad de enlace"); /*Cambiar  DE NUEVO A TEXTO ORIGINAL*/
                    iParrafoY = 0;
                    List<String> lstLineas = parrafo.getLines();
                    foreach (string element in lstLineas)
                    {
                        contentStream.beginText();
                        contentStream.setFont(fontDatos, 8);
                        contentStream.moveTextPositionByAmount(parrafo.getX(), (posY) - (11 * iParrafoY));
                        contentStream.drawString(element.TrimEnd());
                        contentStream.endText();
                        iParrafoY++;

                        if ((posY - (11 * iParrafoY)) <= 30)
                        {
                            if (contentStream != null)
                                contentStream.close();

                            PDPage page = new PDPage();
                            document.addPage(page);
                            contentStream = new PDPageContentStream(document, page);
                            agregarEncabezado(document, contentStream, font, sTitulo, escudoPath);
                            posY = 715;
                            iParrafoY = 0;

                            contentStream.beginText();
                            contentStream.setFont(fontDatos, 8);
                            contentStream.moveTextPositionByAmount(520, 15);
                            contentStream.drawString("Página : " + iPagina.ToString());
                            contentStream.endText();
                            iPagina++;
                        }
                    }
                    posY = (posY) - (11 * (iParrafoY));
                   
                    posY = posY - 6;

                    contentStream.setStrokingColor(30, 30, 30);
                    contentStream.drawLine(20, posY, 590, posY);
                    posY = posY - 15;
                    iCuenta++;
                }




                ////        /*  T O T A L E S*/ 
                ////        contentStream.beginText();
                ////        contentStream.setFont( font, 8 );
                ////        contentStream.moveTextPositionByAmount( 50, posY );
                ////        contentStream.drawString( "TOTAL DE DOCUMENTOS : " + iCuenta);
                ////        contentStream.endText();

                if (contentStream != null) contentStream.close();
            }
        }

        [HttpGet]
        public ActionResult crearReporte(string rep, string status, string area, string tipsol, string tipres, string folio, string fecsolini, string fecsolfin, string semaforo, string formato)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(ReporteDao.COL_NUM_REPORTE, rep);
            if (Convert.ToInt32(status) > 0)
                dicParam.Add(ReporteDao.COL_STATUS_SOLICITUD, status);

            if (Convert.ToInt32(area) > 0)
                dicParam.Add(ReporteDao.COL_AREA, area);

            if (Convert.ToInt32(tipsol) > 0)
                dicParam.Add(ReporteDao.COL_TIPO_SOLICITUD, tipsol);

            if (Convert.ToInt32(tipres) > 0)
                dicParam.Add(ReporteDao.COL_TIPO_RESPUESTA, tipres);

            if (folio != "undefined")
                dicParam.Add(ReporteDao.COL_FOLIO, folio);

            if (fecsolini != "undefined")
                dicParam.Add(ReporteDao.COL_FECSOL_INI, fecsolini);

            if (fecsolfin != "undefined")
                dicParam.Add(ReporteDao.COL_FECSOL_FIN, fecsolfin);

            //if (semaforo != "undefined")
            //    dicParam.Add(ReporteDao.COL_SEMAFORO, semaforo);


            /// CAMBIAR AREA
            /// /// CAMBIAR AREA
            /// v
            /// /// CAMBIAR AREA
            //////dicParam.Add(ReporteDao.PARAM_NO_AREAS, "(" + _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI) + 
            //////    "," + _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT) + "," + _memCacheSIT.ObtenerDato(CacheWebSIT.AREA_UTN) + ")");

            DataTable dtReporte = (DataTable) _sitDmlDbSer.operEjecutar<ReporteDao>(nameof(ReporteDao.dmlSelectRecibidasSFP), dicParam);
            if (dtReporte == null)
                return null;


            String escudoPath = "";  ////// Server.MapPath("/Content/image") + "\\LogoSfp.jpg";
            String sNombre = DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
            string sFile = sNombre; ////// Server.MapPath("\\reporte\\") + sNombre;
            ByteArrayOutputStream bos = new ByteArrayOutputStream();

            if (formato == "0")
            {
                try
                {
                    PDDocument document = new PDDocument();
                    //crearPaginaReport1(escudoPath, document, dtReporte, "SOLICITUDES DE ACCESO A LA INFORMACIÓN RECIBIDAS EN LA SFP");
                    crearPaginaReport1("", document, dtReporte, "SOLICITUDES DE ACCESO A LA INFORMACIÓN RECIBIDAS EN LA SFP");
                    document.save(bos);
                    document.close();
                }
                catch (COSVisitorException ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
                return File(bos.toByteArray(), "application/pdf", "Reporte.pdf");
            }
            else
            {
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = @"demo.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee");
                    //First add the headers
                    worksheet.Cells[1, 1].Value = "No.";
                    worksheet.Cells[1, 2].Value = "Folio";
                    worksheet.Cells[1, 3].Value = "Fecha de Recepción";
                    worksheet.Cells[1, 4].Value = "Tipo de Solicitud";
                    worksheet.Cells[1, 5].Value = "Días Transcurridos";
                    worksheet.Cells[1, 6].Value = "Semaforo";
                    worksheet.Cells[1, 7].Value = "Descripción";

                    int cont=0;
                    for (var j = 0; j < 20; j++)
                    {
                        foreach (DataRow drDatos in dtReporte.Rows)
                        {
                            worksheet.Cells["A" + cont + 2].Value = cont + 1;
                            worksheet.Cells["B" + cont + 2].Value = drDatos["solclave"].ToString();
                            DateTime Fecha = (DateTime)drDatos["segfecini"];
                            worksheet.Cells["C" + cont + 2].Value = Fecha.ToString("dd/MM/yyyy hh:mm");
                            worksheet.Cells["D" + cont + 2].Value = drDatos["sotdescripcion"].ToString();
                            worksheet.Cells["E" + cont + 2].Value = drDatos["segdiassemaforo"].ToString();
                            worksheet.Cells["F" + cont + 2].Value = "Verde";
                            worksheet.Cells["G" + cont + 2].Value = drDatos["soldes"].ToString();
                            //worksheet.Cells["H" + cont + 2].Value = 5000;
                        }
                        cont++;
                    }


                    package.Save(); //Save the workbook.
                }
                return Redirect(URL);
                //return URL;
                //return File(bos.toByteArray(), "application/pdf", "Reporte.pdf");
            }
        }

        private void agregarEncabezado(PDDocument document, PDPageContentStream contentStream, PDFont font, String sTitulo, String sEscudo)
        {
            PDXObjectImage image = null;
            sEscudo = sEscudo.Replace("\\", "//");

            //java.io.File javaFile = new java.io.File(sEscudo);
            //image = new PDJpeg(document, new FileInputStream(javaFile));
            //contentStream.drawImage(image,20,740);

            contentStream.beginText();
            contentStream.setFont(font, 12);
            contentStream.moveTextPositionByAmount(210, 770);
            contentStream.drawString("Secretaría de la Función Pública");
            contentStream.endText();

            DateTime cal = DateTime.Now;
            contentStream.beginText();
            contentStream.setFont(font, 8);
            contentStream.moveTextPositionByAmount(495, 780);
            contentStream.drawString("Fecha: " + cal.ToString("dd-MMMM-yyyy"));
            contentStream.endText();

            contentStream.beginText();
            contentStream.setFont(font, 10);
            contentStream.moveTextPositionByAmount(150, 747);
            contentStream.drawString(sTitulo);
            contentStream.endText();
        }

    }
}
