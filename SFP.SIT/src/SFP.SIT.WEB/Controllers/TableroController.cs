using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SFP.SIT.WEB.Injection;
using SFP.SIT.WEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using SFP.SIT.WEB.Util;
using System.Text;
using SFP.SIT.SERV.Dao.TAB;
using SFP.SIT.SERV.Util;

namespace SFP.SIT.WEB.Controllers
{
    public class TableroController : SitBaseCtlr
    {
        public TableroController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<SolicitudController> logger, IHostingEnvironment app) 
            : base(memCache, httpContextAccessor, logger, app)
        {
            _iUsuario = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
        }

        [HttpGet]
        public ViewResult Estadistica()
        {
            return View();
        }

        [HttpGet]
        public string EstadisticaDatos(string fechaini, string fechafin, string renglon, string columna)
        {
            /* Datos para crear el menú del usuario */            
            StringBuilder sbDatos = new StringBuilder();

            if (renglon == null || columna== null)
                return "";

            if (fechaini == null || fechafin == null || renglon == "" || columna == "" )
                return "";

            if (_iUsuario > 0)
            {
                Response.ContentType = "application/json; charset=UTF-8";

                int iRenglon = Convert.ToInt32(columna.Substring(1));
                int iColumna = Convert.ToInt32(renglon.Substring(1));
                string _sOrden;

                int iOper = iTipoConsulta(iRenglon, iColumna, out _sOrden);

                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add(TabConsultaDao.COL_solfecsol_FECINI, fechaini);
                dicParam.Add(TabConsultaDao.COL_solfecsol_FECFIN, fechafin);
                dicParam.Add(TabConsultaDao.PARAM_COLUMNA, iColumna);
                dicParam.Add(TabConsultaDao.PARAM_RENGLON, iRenglon);
                dicParam.Add(TabConsultaDao.PARAM_NO_AREAS, "(" + _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI) + "," + _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT) + ")");                                
                dicParam.Add(TabConsultaDao.PARAM_ORDEN, _sOrden);
                dicParam.Add(TabConsultaDao.PARAM_OPERACION, iOper);

                object oDatos = _sitDmlDbSer.operEjecutar<TabConsultaDao>(nameof(TabConsultaDao.EjecutarOperacion), dicParam) as Object;

                if (oDatos != null)
                {
                    string sJson = JsonTransform.convertJsonNoRecords(oDatos);
                    return sJson;
                }
                else
                    return "";
            }
            else
            {
                return "";
            }
        }


        private int iTipoConsulta(int iRenglon, int iColumna, out string _sOrden)
        {
            int iOper = 0;
            _sOrden = "1";

            if (iRenglon == TabConsultaDao.DIMENSION_SOLICITUD)
            {
                if (iColumna == TabConsultaDao.DIMENSION_AREA)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_SOLICITUD_AREA;
                    _sOrden = "1";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_USUARIO)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_SOLICITUD_USUARIO;
                    _sOrden = "1";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_RESPUESTA)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_SOLICITUD_RESPUESTA;
                    _sOrden = "1";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_ESTADO)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_SOLICITUD_ESTADO;
                    _sOrden = "1";
                }
            }
            else if (iRenglon == TabConsultaDao.DIMENSION_AREA)
            {
                if (iColumna == TabConsultaDao.DIMENSION_SOLICITUD)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_SOLICITUD_AREA;
                    _sOrden = "2";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_USUARIO)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_AREA_USUARIO;
                    _sOrden = "1";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_RESPUESTA)
                {
                    _sOrden = "2";
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_AREA_RESPUESTA;
                }
                else if (iColumna == TabConsultaDao.DIMENSION_ESTADO)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_AREA_ESTADO;
                    _sOrden = "1";
                }
            }
            else if (iRenglon == TabConsultaDao.DIMENSION_USUARIO)
            {
                if (iColumna == TabConsultaDao.DIMENSION_SOLICITUD)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_SOLICITUD_USUARIO;
                    _sOrden = "2";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_AREA)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_AREA_USUARIO;
                    _sOrden = "2";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_RESPUESTA)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_USUARIO_RESPUESTA;
                    _sOrden = "2";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_ESTADO)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_USUARIO_ESTADO;
                    _sOrden = "1";
                }
            }
            else if (iRenglon == TabConsultaDao.DIMENSION_RESPUESTA)
            {
                if (iColumna == TabConsultaDao.DIMENSION_SOLICITUD)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_SOLICITUD_RESPUESTA;
                    _sOrden = "2";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_AREA)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_AREA_RESPUESTA;
                    _sOrden = "1";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_USUARIO)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_USUARIO_RESPUESTA;
                    _sOrden = "1";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_ESTADO)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_ESTADO_RESPUESTA;
                    _sOrden = "1";
                }
            }
            else if (iRenglon == TabConsultaDao.DIMENSION_ESTADO)
            {
                if (iColumna == TabConsultaDao.DIMENSION_SOLICITUD)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_SOLICITUD_ESTADO;
                    _sOrden = "2";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_AREA)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_AREA_ESTADO;
                    _sOrden = "2";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_USUARIO)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_USUARIO_ESTADO;
                    _sOrden = "2";
                }
                else if (iColumna == TabConsultaDao.DIMENSION_RESPUESTA)
                {
                    iOper = TabConsultaDao.OPE_SELECT_TABLERO_ESTADO_RESPUESTA;
                    _sOrden = "2";
                }
            }
            return iOper;
        }

    }
}
