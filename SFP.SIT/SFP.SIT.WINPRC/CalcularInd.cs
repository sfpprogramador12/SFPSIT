using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using SFP.Persistencia.Servicio;
using SFP.Persistencia.Model;
using System.Data;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model;
using SFP.SIT.SERV.Util;

namespace SFP.SIT.WINPRC
{
    public partial class CalcularInd : ServiceBase
    {
        System.Timers.Timer timeDelay;
        int _HoraPrc;
        String _ArchivoLog = null;
        String _Carpeta = null;       
        DmlDbSer _DbSer = null;
        AdmCorreoNeg _AdmCorreo = null;
        Boolean _EnviarCorreo = false;
        String _OmitirAreas = "";
        string _CorreoMsjRuta = "";

        public CalcularInd()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer(3600000); //600 segundos = 10min 3600 cada hora se ejecuta
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(CalcularSolicitudes);
        }

        protected override void OnStart(string[] args)
        {
            //// System.Diagnostics.Debugger.Launch();
            _ArchivoLog = ConfigurationManager.AppSettings["ArchivoLog"];
            _Carpeta = ConfigurationManager.AppSettings["Carpeta"];

            BaseDbMdl dbMDL = new BaseDbMdl();
            dbMDL.connectionString = ConfigurationManager.AppSettings["ConnectionStrings"];
            dbMDL.objDbConnection = ConfigurationManager.AppSettings["objCxn"];
            dbMDL.objDbTransaccion = ConfigurationManager.AppSettings["objTran"];
            dbMDL.objDbDataAdapter = ConfigurationManager.AppSettings["objDataAdapter"];

            _HoraPrc = Convert.ToInt32(ConfigurationManager.AppSettings["HoraPrc"]);

            CfgCorreoMdl correoMdl = new CfgCorreoMdl();
            correoMdl.servidor = ConfigurationManager.AppSettings["ServidorMail"];
            correoMdl.puerto = Convert.ToInt32( ConfigurationManager.AppSettings["PuertoMail"]);
            correoMdl.usuario = ConfigurationManager.AppSettings["UsuarioMail"];
            correoMdl.contraseña = ConfigurationManager.AppSettings["ContraseñaMail"];

            _OmitirAreas = ConfigurationManager.AppSettings["OmitirUaMail"];
            _CorreoMsjRuta = ConfigurationManager.AppSettings["MensajeMail"];

            _DbSer = new DmlDbSer(dbMDL);

            if (ConfigurationManager.AppSettings["EnviarMail"].ToUpper() == "SI")
            {
                _EnviarCorreo = true;
            }
            
            _AdmCorreo = new AdmCorreoNeg(correoMdl);

            timeDelay.Enabled = true;
        }

        protected override void OnStop()
        {
            LogService("El servicio se detuvo");
            timeDelay.Enabled = false;
        }

        private void LogService(String content)
        {                        
            FileStream fs = new FileStream(_Carpeta + _ArchivoLog, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }

        public void CalcularSolicitudes(object sender, System.Timers.ElapsedEventArgs e)
        {
            int iHora = DateTime.Now.Hour;

            if (iHora == _HoraPrc)
            {
                try
                {
                    Dictionary<Int64, char> dicDiaNoLaboral = _DbSer.operEjecutar<SIT_ADM_DIANOLABORALDao> (nameof(SIT_ADM_DIANOLABORALDao.dmlSelectDiccionario),  null) as Dictionary<Int64, char>;
                    List<SIT_SOL_PROCESOPLAZOS> lstPrcPzoMdl = _DbSer.operEjecutar<SIT_SOL_PROCESOPLAZOSDao>(nameof(SIT_SOL_PROCESOPLAZOSDao.dmlSelectLista),  null) as List<SIT_SOL_PROCESOPLAZOS>;
                    CalcularPlazoNeg calPlazoSer = new CalcularPlazoNeg(dicDiaNoLaboral);
                    List<SolSegEnProcesoMdl> lstSeguimiento = _DbSer.operEjecutar<SIT_SOL_SEGUIMIENTODao>(nameof(SIT_SOL_SEGUIMIENTODao.dmlSelectOSolPendientesAreas), null) as List<SolSegEnProcesoMdl>;
                  
                    foreach (SolSegEnProcesoMdl segMdl in lstSeguimiento)
                    {
                        _DbSer.operEjecutar<SIT_SOL_SEGUIMIENTODao>(nameof(SIT_SOL_SEGUIMIENTODao.dmlEditar), 
                            calPlazoSer.CalcularSeguimiento(segMdl.sotclave, DateTime.Now, lstPrcPzoMdl, (SIT_SOL_SEGUIMIENTO)segMdl));
                    }

                    if (_EnviarCorreo == true)
                        EnviarCorreo();
                }
                catch (Exception ex)
                {
                    LogService( DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ") +  " se produjo el siguiente error : " + ex.Message);
                }                
            }           
        }

        public void EnviarCorreo()
        {
            string sMensaje = "";
            string sNvoMensaje = "";

            if (System.IO.File.Exists(_CorreoMsjRuta + "MensajeCorreo.html"))
                sMensaje = System.IO.File.ReadAllText(_CorreoMsjRuta + "MensajeCorreo.html");
            else
                sMensaje = FormatoMensaje();

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(SIT_SOL_SEGUIMIENTODao.PARAM_OMITIR_AREA, _OmitirAreas);
            List<int> lstArea = _DbSer.operEjecutar<SIT_SOL_SEGUIMIENTODao>(nameof(SIT_SOL_SEGUIMIENTODao.dmlSelectLiSolPendientesAreas), dicParam ) as List<int>;


            dicParam.Add(DButil.SIT_ADM_AREAHIST_COL.ARACLAVE, 0);
            StringBuilder sbDatos = new StringBuilder();

            foreach (int iArea in lstArea)
            {
                // BUSCAMOS TODOS LOS FOLIOS PENDIENTES..
                sbDatos.Clear();
                sbDatos.Append("<table>");
                sbDatos.Append("<tr>");
                sbDatos.Append("<td style='text-align: center;'> Folio</td>");
                sbDatos.Append("<td style='text-align: center;'> Días en proceso</td>");
                sbDatos.Append("<td style='text-align: center;'> Semáforo</td>");
                sbDatos.Append("<td style='text-align: center;'> Fecha Límite</td>");
                sbDatos.Append("</tr>");
                dicParam[DButil.SIT_ADM_AREAHIST_COL.ARACLAVE] = iArea;
                List<SIT_SOL_SEGUIMIENTO> lstSolSeg = _DbSer.operEjecutar<SIT_SOL_SEGUIMIENTODao>(nameof(SIT_SOL_SEGUIMIENTODao.dmlSelectOSolPendientesAreas), dicParam) as List<SIT_SOL_SEGUIMIENTO>;

                foreach (SIT_SOL_SEGUIMIENTO solSegMdl in lstSolSeg)
                {
                    sbDatos.Append("<tr>");
                    sbDatos.Append("<td>" + solSegMdl.solclave + "</td>");
                    sbDatos.Append("<td style='text-align: center;'>" + solSegMdl.segdiassemaforo + "</td>");

                    if (solSegMdl.segsemaforocolor == 3)
                        sbDatos.Append("<td style='text-align:center; background-color:red; width:20px; height:20px;'></td>");
                    else if (solSegMdl.segsemaforocolor == 2)
                        sbDatos.Append("<td style='text-align:center; background-color:yellow; width:20px; height:20px;'></td>");
                    else
                        sbDatos.Append("<td style='text-align:center; background-color:green; width:20px; height:20px;'></td>");

                    sbDatos.Append("<td>" + solSegMdl.segfecestimada.ToString("dd/MM/yyyy") + "</td>");
                    sbDatos.Append("</tr>");
                }
                sbDatos.Append("</table>");

                DataTable dtCorreos = (DataTable)_DbSer.operEjecutar<SIT_ADM_USUARIOAREADao>(nameof(SIT_ADM_USUARIOAREADao.dmlOUsuarioAreas), dicParam);
                    
                for (int iIdxCorreo = 0; iIdxCorreo < dtCorreos.Rows.Count; iIdxCorreo++)
                {
                    sNvoMensaje = sMensaje.Replace("|USUARIO|", dtCorreos.Rows[iIdxCorreo][0].ToString()).Replace("|FOLIOS|", sbDatos.ToString());
                    _AdmCorreo.Enviar(dtCorreos.Rows[iIdxCorreo][1].ToString(), "Solicitudes de información pendientes", sNvoMensaje );
                }
            }
        }

        private string FormatoMensaje( )
        {
            StringBuilder sbMensaje = new StringBuilder();

            sbMensaje.Append("<!DOCTYPE html>");
            sbMensaje.Append("<html>");
            sbMensaje.Append("<head>");
            sbMensaje.Append("<meta charset='utf-8' />");
            sbMensaje.Append("<title></title>");
            sbMensaje.Append("</head>");
            sbMensaje.Append("<body style='background-color:whitesmoke; '>");
            sbMensaje.Append("<div style='text-align: center;  '>");
            sbMensaje.Append("<div style='width: 70%; margin: 0 auto; border-bottom:1px solid black; margin-top:50px;'></div>");
            sbMensaje.Append("<div style='width: 70%; margin: 0 auto; text-align: center'>");
            sbMensaje.Append("<h1>Notificación de Solicitudes de información pendientes</h1>");
            sbMensaje.Append("</div>");
            sbMensaje.Append("<div style='width: 70%; margin: 0 auto; border-bottom:1px solid black;'></div>");
            sbMensaje.Append("<div style='width: 70%; margin: 0 auto; border-bottom:1px solid black; margin-top:50px; '>");
            sbMensaje.Append("<h3>Se le informa que en el \"Sistema de Transparencia\", tiene los siguientes turnos pendientes para su atención:</h3>");
            sbMensaje.Append("</div>");
            sbMensaje.Append("<div style='width: 70%; margin: 0 auto; border-bottom:1px solid black; margin-top:50px; '>");
            sbMensaje.Append("<h3>Usuario: |USUARIO|</h3>");           
            sbMensaje.Append("<br/>|FOLIOS|");
            sbMensaje.Append("</div>");
            sbMensaje.Append("<div style='width: 70%; margin: 0 auto; border-bottom:1px solid black; margin-top:50px; '>");
            sbMensaje.Append("<h4>Insurgentes Sur 1735, Col. Guadalupe Inn, Deleg. Álvaro Obregón</h4>");
            sbMensaje.Append("<h4>Distrito Federal CP. 01020, T. (55) 2000-3000</h4>");
            sbMensaje.Append("<br />");
            sbMensaje.Append("</div>");
            sbMensaje.Append("</div>");
            sbMensaje.Append("</body>");
            sbMensaje.Append("</html>");

            return sbMensaje.ToString();
        }
    }
}
