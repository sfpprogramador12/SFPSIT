using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Dao.DOC;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Dao.SNT;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model.SNT;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using SFP.SIT.WEB.Injection;
using SFP.SIT.WEB.Models;
using SFP.SIT.WEB.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace SFP.SIT.WEB.Controllers
{
    public class CatalogoController : SitBaseCtlr
    {
        CatViewModel _catViewMdl;
        public CatalogoController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<CatalogoController> logger, IHostingEnvironment env) 
            : base(memCache, httpContextAccessor, logger, env)
        {
            _catViewMdl = new CatViewModel();
        }
    
        [HttpGet]
        public IActionResult Catalogo()
        {
            _appLog.opdesc = "CatalogoSer.OPE_OBTENER_LISTA_CAT";

            string sPerfil= "";            
            List<SIT_ADM_PERFIL> lstUsrPerfil = JsonConvert.DeserializeObject<List<SIT_ADM_PERFIL>>(_httpContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.PERFILES).Value);
            foreach (SIT_ADM_PERFIL perfilMdl in lstUsrPerfil)
            {
                sPerfil += "," + perfilMdl.perclave;
            }

            _catViewMdl.lstCatalogos = JsonTransform.convertJson(
                _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.SelPerfilCatalogos), sPerfil.Substring(1)) as DataTable);
            return View(_catViewMdl);
        }

        [HttpGet]
        public IActionResult CatalogoGrid()
        {
            return View();
        }

        /* **************************************
             MANEJO DE TODOS LOS CATALOGOS  
         ************************************** */
        [HttpGet]
        public IActionResult PVarea()
        {            
            _catViewMdl.lstTipoArea = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_AREATIPODao>(nameof(SIT_ADM_AREATIPODao.dmlSelectCombo), null));
            _catViewMdl.lstTipoPerfil = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlSelectCombo),  null));
            _appLog.opdesc = "AdmTipoAreaDao.OPE_SELECT_COMBO - AdmPerfilDao.OPE_SELECT_COMBO ";
            return View(_catViewMdl);
        }

        [HttpGet]
        public IActionResult PVareaArbol()
        {
            _catViewMdl.lstTipoArea = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_AREATIPODao>(nameof(SIT_ADM_AREATIPODao.dmlSelectCombo), null));
            _catViewMdl.lstTipoPerfil = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlSelectCombo), null));
            _appLog.opdesc = "AdmTipoAreaDao.OPE_SELECT_COMBO - AdmPerfilDao.OPE_SELECT_COMBO ";
            return View(_catViewMdl);
        }

        [HttpPost]        
        public String PVarea( SIT_ADM_AREA SIT_ADM_AREA, Int32 cmd)
        {
            string sRes = "";
            ////  ESTO DEBE DE CAMBIAR TODO
            ////  ESTO DEBE DE CAMBIAR TODO
            ///////  ESTO DEBE DE CAMBIAR TODO
            ////  ESTO DEBE DE CAMBIAR TODO
            //////////// string sRes = _sitDmlDbSer.operEjecutar<SIT_ADM_AREADao>(nameof(SIT_ADM_AREADao), cmd, SIT_ADM_AREA).ToString();

            //////////// if (sRes != "0")
            //////////// {
            ////////////     //// REVISAR 
            ////////////     //// REVISAR 
            ////////////     //// REVISAR 
            ////////////     Dictionary<string, object> dicParam = new Dictionary<string, object>();
            ////////////     dicParam.Add(AdmAreaHistDao.PARAM_FECHA, DateTime.Now);

            ////////////     _memCacheSIT.AgregarDato("SIT_ADM_AREADao", _sitDmlDbSer.operEjecutar(nameof(AdmAreaHistDao), AdmAreaHistDao.OPE_SELECT_COMBO, dicParam) as DataTable);

            ////////////     _memCacheSIT.AgregarDato(CacheWebSIT.DIC_AREA_PERFIL, 
            ////////////         _sitDmlDbSer.operEjecutar(nameof(AdmAreaHistDao), AdmAreaHistDao.OPE_SELECT_AREAS_PERFIL, dicParam) as Dictionary<int, int> );
            ////////////}
            //////////// _appLog.opdesc = "SIT_ADM_AREADao - CMD " + cmd + " Actualizar _memCacheSIT: [ SIT_ADM_AREADao.OPE_SELECT_COMBO, SIT_ADM_AREADao.OPE_SELECT_AREAS_PERFIL]";
            //////////// _appLog.data = JsonTransform.convertJson(SIT_ADM_AREA).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVcatalogo()
        {
            _catViewMdl.lstTipoPerfil = JsonTransform.convertJson( (DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlSelectCombo), null));
            _appLog.opdesc = "AdmPerfilDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVcatalogo( SIT_ADM_CLASES admCatMdl, Int32 cmd)
        {
            Dictionary<string,object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, admCatMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_ADM_CLASESDao>(nameof(SIT_ADM_CLASESDao.dmlCRUD), dicParam).ToString();

            if (sRes != "0")
            {
                lock (_memCacheSIT)
                {
                    _memCacheSIT.AgregarDato(CacheWebSIT.DIC_CATALOGOS_CLASE,
                        _sitDmlDbSer.operEjecutar<SIT_ADM_CLASESDao>(nameof(SIT_ADM_CLASESDao.dmlSelectDiccionario), null) as Dictionary<int, string>);
                }
            }
            _appLog.opdesc = "SIT_ADM_CONFIGURACIONDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_ADM_CONFIGURACIONDao.OPE_SELECT_DICCIONARIO]";
            _appLog.data = JsonTransform.convertJson(admCatMdl).ToString();

            
            return sRes;
        }

        [HttpGet]
        public IActionResult PVconfiguracion()
        {
            return View();
        }

        [HttpPost]
        public String PVconfiguracion( SIT_ADM_CONFIGURACION admConfigMdl, Int32 cmd)
        {
            if (admConfigMdl.cfgfecbaja.Ticks == Constantes.Tiempo.INICIAL)
                admConfigMdl.cfgfecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, admConfigMdl);

            String iRes = _sitDmlDbSer.operEjecutar<SIT_ADM_CONFIGURACIONDao>(nameof(SIT_ADM_CONFIGURACIONDao.dmlCRUD), dicParam).ToString();
            //////if (iRes != "0")
            //////{
            //////if (Constantes.Configuracion.HORA_PROCESO == admConfigMdl.cfgsubclave)
            ////////// HORA PARA EL PROCESOSO
            ///////AspNetTimerSIT.ActualizarHora(Convert.ToInt32(admConfigMdl.cfgvalor));
            ////////// HORA PARA EL PROCESOSO
            //////}

            _appLog.opdesc = "SIT_ADM_CONFIGURACIONDao - CMD :" + cmd ;
            _appLog.data = JsonTransform.convertJson(admConfigMdl).ToString();

            
            return iRes;
        }

        [HttpGet]
        public IActionResult PVdiaLab()
        {
            return View();
        }

        [HttpPost]
        public String PVdiaLab( SIT_ADM_DIANOLABORAL  admDiaLabMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, admDiaLabMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_ADM_DIANOLABORALDao>(nameof(SIT_ADM_DIANOLABORALDao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_DIA_NO_LABORAL, 
                    _sitDmlDbSer.operEjecutar<SIT_ADM_DIANOLABORALDao>(nameof(SIT_ADM_DIANOLABORALDao.dmlSelectDiccionario),  null) as Dictionary<int, string> );
            }
            _appLog.opdesc = "SIT_ADM_DIANOLABORALDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_SNT_SOLICITANTEDao.OPE_SELECT_DICCIONARIO ]";
            _appLog.data = JsonTransform.convertJson(admDiaLabMdl).ToString();

            
            return sRes;
        }

        [HttpGet]
        public IActionResult PVmodulo()
        {            
            _catViewMdl.lstModulo = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_MODULODao>(nameof(SIT_ADM_MODULODao.dmlSelectCombo), null));
            _appLog.opdesc = "AdmModuloDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVmodulo(SIT_ADM_MODULO SIT_ADM_MODULO, int cmd)
        {            
            if (SIT_ADM_MODULO.modfecbaja.Ticks == Constantes.Tiempo.INICIAL)
                SIT_ADM_MODULO.modfecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_ADM_MODULO);

            _appLog.opdesc = "AdmModuloDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(SIT_ADM_MODULO).ToString();
            _sitDmlDbSer.operEjecutar<SIT_ADM_MODULODao>(nameof(SIT_ADM_MODULODao.dmlCRUD), dicParam).ToString();           
            return "";
        }

        [HttpGet]
        public IActionResult PVperfil()
        {
            return View();
        }
        [HttpPost]
        public String PVperfil( SIT_ADM_PERFIL SIT_ADM_PERFIL, int cmd)
        {
            if (SIT_ADM_PERFIL.perfecbaja.Ticks == Constantes.Tiempo.INICIAL)
                SIT_ADM_PERFIL.perfecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_ADM_PERFIL);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlCRUD), dicParam).ToString();

            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato("AdmPerfilDao", 
                    _sitDmlDbSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlSelectDicEntidad), null) as Dictionary<int, SIT_ADM_PERFIL>);
            }
            _appLog.opdesc = "AdmPerfilDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ AdmPerfilDao.OPE_SELECT_DICCIONARIO_PERFIL ]";
            _appLog.data = JsonTransform.convertJson(SIT_ADM_PERFIL).ToString();

            
            return sRes;
        }

        [HttpGet]
        public IActionResult PVperfilModulo()
        {            
            _catViewMdl.lstTipoPerfil = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlSelectCombo), null));
            _catViewMdl.lstModulo = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_MODULODao>(nameof(SIT_ADM_MODULODao.dmlSelectCombo), null));
            _appLog.opdesc = "AdmPerfilDao.OPE_SELECT_COMBO - AdmModuloDao.OPE_SELECT_COMBO";

            return View(_catViewMdl);
        }
        [HttpPost]
        public String PVperfilModulo(SIT_ADM_PERFILMOD admPerfilModMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, admPerfilModMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_ADM_PERFILMODDao>(nameof(SIT_ADM_PERFILMODDao.dmlCRUD), dicParam).ToString();
            _appLog.opdesc = "AdmPerfilModDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(admPerfilModMdl).ToString();

            return "";
        }

        [HttpGet]
        public IActionResult PVtipoArea()
        {
            return View();
        }

        [HttpPost]
        public String PVtipoArea(SIT_ADM_AREATIPO SIT_ADM_AREATIPO, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_ADM_AREATIPO);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_ADM_AREATIPODao>(nameof(SIT_ADM_AREATIPODao.dmlCRUD), dicParam).ToString();
            _appLog.opdesc = "AdmTipoAreaDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(SIT_ADM_AREATIPO).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVusuario()
        {
            _catViewMdl.lstArea = JsonTransform.convertJson(
                (DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectComboFecActual), DateTime.Now));
            _appLog.opdesc = "SIT_ADM_AREADao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public string PVusuario(SIT_ADM_USUARIO SIT_ADM_USUARIO, int cmd)
        {
            if (SIT_ADM_USUARIO.usrfecbaja.Ticks == Constantes.Tiempo.INICIAL)
                SIT_ADM_USUARIO.usrfecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_ADM_USUARIO);


            if (cmd == BaseDao.OPE_INSERTAR)
            {
                SIT_ADM_USUARIO usuMdl  = (SIT_ADM_USUARIO) _sitDmlDbSer.operEjecutar<SIT_ADM_USUARIODao>(
                    nameof(SIT_ADM_USUARIODao.dmlSelectExisteUsr), SIT_ADM_USUARIO.usrcorreo) as SIT_ADM_USUARIO;

                if (usuMdl != null)
                    return "La cuenta de correo ya existe";
            }
            String sRes = _sitDmlDbSer.operEjecutar<SIT_ADM_USUARIODao>(nameof(SIT_ADM_USUARIODao.dmlCRUD), dicParam).ToString();

            _appLog.opdesc = "AdmUsuarioDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(SIT_ADM_USUARIO).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVusuArea()
        {
            Dictionary<String, Object> dicParam = new Dictionary<String, Object>();

            _catViewMdl.lstArea = JsonTransform.convertJson( _sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectComboFecActual), DateTime.Now) as List<ComboMdl>);
            _catViewMdl.lstTipoPerfil = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlSelectCombo), null));
            _catViewMdl.lstUsuario = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_USUARIODao>(nameof(SIT_ADM_USUARIODao.dmlSelectCombo),  null ));
            _appLog.opdesc = "SIT_ADM_AREADao.OPE_SELECT_COMBO - AdmPerfilDao.OPE_SELECT_COMBO - AdmUsuarioDao.OPE_SELECT_COMBO";

            return View(_catViewMdl);
        }


        [HttpPost]
        public String PVusuArea( SIT_ADM_USUARIOAREA admUsuPerMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, admUsuPerMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_ADM_USUARIOAREADao>(nameof(SIT_ADM_USUARIOAREADao), dicParam).ToString();
            _appLog.opdesc = "AdmUsuAreaDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(admUsuPerMdl).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVdocArista()
        {
            return View();
        }

        ////////[HttpPost]
        //////////////public String PVdocArista(SIT_DOC_ARISTA admDocAristaMdl, int cmd)
        //////////////{
        //////////////    Dictionary<string, object> dicParam = new Dictionary<string, object>();
        //////////////    dicParam.Add(BaseDao.CMD_OPERACION, cmd);
        //////////////    dicParam.Add(BaseDao.CMD_ENTIDAD, admDocAristaMdl);
        //////////////    String sRes = _sitDmlDbSer.operEjecutar<SIT_DOC_ARISTADao>(nameof(SIT_DOC_ARISTADao.dmlCRUD), dicParam).ToString();
        //////////////    _appLog.opdesc = "DocAristaDao - CMD :" + cmd;
        //////////////    _appLog.data = JsonTransform.convertJson(admDocAristaMdl).ToString();

        //////////////    return sRes;
        //////////////}

        [HttpGet]
        public IActionResult PVtipoDoc()
        {
            return View();
        }
        [HttpPost]
        public String PVtipoDoc( SIT_DOC_EXTENSION  admTipoDocMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, admTipoDocMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_DOC_EXTENSIONDao>(nameof(SIT_DOC_EXTENSIONDao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato (CacheWebSIT.DIC_DOC_EXTENSION,
                    _sitDmlDbSer.operEjecutar<SIT_DOC_EXTENSIONDao>(nameof(SIT_DOC_EXTENSIONDao.dmlSelectDiccionario), null) as Dictionary<string, int>);
            }
            _appLog.opdesc = "SIT_DOC_EXTENSIONDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_DOC_EXTENSIONDao.OPE_SELECT_DICCIONARIO ]";
            _appLog.data = JsonTransform.convertJson(admTipoDocMdl).ToString();
            
            return sRes;
        }

        [HttpGet]
        public IActionResult PVdoc()
        {
            _appLog.opdesc = "SIT_DOC_EXTENSIONDao.OPE_SELECT_COMBO";
            _catViewMdl.lstTipoExt = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_DOC_EXTENSIONDao>(nameof(SIT_DOC_EXTENSIONDao.dmlSelectCombo),  null));
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVdoc(SIT_DOC_DOCUMENTO docMdl, int cmd)
        {
            if (docMdl.docfecha.Ticks == Constantes.Tiempo.INICIAL)
                docMdl.docfecha = DateTime.MinValue;

            if (docMdl.docfilesystem.Ticks == Constantes.Tiempo.INICIAL)
                docMdl.docfilesystem = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, docMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_DOC_DOCUMENTODao>(nameof(SIT_DOC_DOCUMENTODao.dmlCRUD), dicParam).ToString();
            _appLog.opdesc = "SIT_DOC_EXTENSIONDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(docMdl).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVafd()
        {
            return View();
        }
        [HttpPost]
        public String PVafd(SIT_RED_AFD SIT_RED_AFD, int cmd)
        {
            if (SIT_RED_AFD.afdfecbaja.Ticks == Constantes.Tiempo.INICIAL)
                SIT_RED_AFD.afdfecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_RED_AFD);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_RED_AFDDao>(nameof(SIT_RED_AFDDao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_AFD_PREFIJO, _sitDmlDbSer.operEjecutar<SIT_RED_AFDDao>(nameof(SIT_RED_AFDDao.dmlSelectDicPrefijo), null) as Dictionary<int, string>);
            }
            _appLog.opdesc = "RedAfdDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ RedAfdDao.OPE_SELECT_FLUJO_PREFIJO ]";
            _appLog.data = JsonTransform.convertJson(SIT_RED_AFD).ToString();
            
            return sRes;
        }

        ////////[HttpGet]
        //////////public IActionResult PVafdFlujo()
        //////////{
        //////////    _catViewMdl.lstTipoAri = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectCombo), null));
        //////////    _appLog.opdesc = "SIT_RESP_TIPODao.OPE_SELECT_COMBO";
        //////////    return View(_catViewMdl);
        //////////}

        [HttpPost]
        public String PVafdFlujo( SIT_RED_AFDFLUJO SIT_RED_AFD, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_RED_AFD);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_RED_AFDFLUJODao>( nameof(SIT_RED_AFDFLUJODao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                // aCTUALZIAR EL FLUJO DE TRABAJO
                // aCTUALZIAR EL FLUJO DE TRABAJO
                // aCTUALZIAR EL FLUJO DE TRABAJO
                // aCTUALZIAR EL FLUJO DE TRABAJO
                //////_memCacheSIT.AgregarDato(CacheWebSIT.DT_AFD_NODO, _sitDmlDbSer.operEjecutar<SIT_RED_AFDFLUJODao>(nameof(SIT_RED_AFDFLUJODao.dmlSelectTabla), null) as DataTable );
                //////_dicCacheSIT.Add(DIC_AFD_FLUJO, sitOperSer.operEjecutar<AfdServicio>(nameof(AfdServicio.Inicializar), dicParam) as Dictionary<Int32, AfdNodoFlujo>);
            }
            _appLog.opdesc = "SIT_RED_AFDFLUJODao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_RED_AFDFLUJODao.OPE_SELECT_TABLE ]";
            _appLog.data = JsonTransform.convertJson(SIT_RED_AFD).ToString();
            
            return sRes;
        }

        [HttpGet]
        public IActionResult PVarista()
        {
            _catViewMdl.lstModoEntrega = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_MODOENTREGADao>(nameof(SIT_SOL_MODOENTREGADao.dmlSelectCombo), null));
            _catViewMdl.lstUsuario = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_USUARIODao>(nameof(SIT_ADM_USUARIODao.dmlSelectCombo), null));
            _appLog.opdesc = "SIT_SOL_MODOENTREGADao.OPE_SELECT_COMBO, AdmUsuarioDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVarista( SIT_RED_ARISTA  SIT_RED_ARISTA, int cmd)
        {
            if (SIT_RED_ARISTA.arifecenvio.Ticks == Constantes.Tiempo.INICIAL)
                SIT_RED_ARISTA.arifecenvio = DateTime.MinValue;


            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_RED_ARISTA);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_RED_ARISTADao>(nameof(SIT_RED_ARISTADao.dmlCRUD), dicParam).ToString();
            _appLog.opdesc = "SIT_RED_ARISTADao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(SIT_RED_ARISTA).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVaristaComite()
        {
            _catViewMdl.lstComiteRubro = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_COMITERUBRODao>(nameof(SIT_RESP_COMITERUBRODao.dmlSelectCombo), null));
            _appLog.opdesc = "SIT_RESP_COMITERUBRODao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        ////////////[HttpPost]
        ////////////////public String PVaristaComite( SIT_ARISTA_COMITE  redArisComMdl, int cmd)
        ////////////////{
        ////////////////    Dictionary<string, object> dicParam = new Dictionary<string, object>();
        ////////////////    dicParam.Add(BaseDao.CMD_OPERACION, cmd);
        ////////////////    dicParam.Add(BaseDao.CMD_ENTIDAD, redArisComMdl);

        ////////////////    String sRes = _sitDmlDbSer.operEjecutar<SIT_ARISTA_COMITEDao>(nameof(SIT_ARISTA_COMITEDao.dmlCRUD), dicParam).ToString();
        ////////////////    _appLog.opdesc = "RedAristaComiteDao - CMD :" + cmd;
        ////////////////    _appLog.data = JsonTransform.convertJson(redArisComMdl).ToString();

        ////////////////    return sRes;
        ////////////////}

        ////////[HttpGet]
        //////////////public IActionResult PVaristaRes()
        //////////////{            
        //////////////    _catViewMdl.lstTipoInfo = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_TIPOINFODao>(nameof(SIT_RESP_TIPOINFODao.dmlSelectCombo), null));
        //////////////    _appLog.opdesc = "SIT_RESP_TIPOINFODao.OPE_SELECT_COMBO";
        //////////////    return View(_catViewMdl);
        //////////////}

        ////////[HttpPost]
        ////////public String PVaristaRes( SIT_ARISTA_RESOLUCION redAriResMdl, int cmd)
        ////////{
        ////////    Dictionary<string, object> dicParam = new Dictionary<string, object>();
        ////////    dicParam.Add(BaseDao.CMD_OPERACION, cmd);
        ////////    dicParam.Add(BaseDao.CMD_ENTIDAD, redAriResMdl);
        ////////    String sRes = _sitDmlDbSer.operEjecutar<SIT_ARISTA_RESOLUCIONDao>(nameof(SIT_ARISTA_RESOLUCIONDao.dmlCRUD), dicParam).ToString();
        ////////    _appLog.opdesc = "RedAristaResolucionDao - CMD :" + cmd;
        ////////    _appLog.data = JsonTransform.convertJson(redAriResMdl).ToString();

        ////////    return sRes;
        ////////}

        [HttpGet]
        public IActionResult PVaristaRev()
        {
            _catViewMdl.lstTipoInfo = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_RREVISIONDao>(nameof(SIT_RESP_RREVISIONDao.dmlSelectCombo), null));
            _appLog.opdesc = "SIT_RESP_TIPOINFODao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost] 
        public String PVaristaRev(SIT_RESP_RREVISION redAriRrevMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, redAriRrevMdl);
            String sRes = _sitDmlDbSer.operEjecutar<SIT_RESP_RREVISIONDao>(nameof(SIT_RESP_RREVISIONDao.dmlCRUD), dicParam).ToString();
            _appLog.opdesc = "SIT_RESP_RREVISIONDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(redAriRrevMdl).ToString();

            return sRes;
        }


        [HttpGet]
        public IActionResult PVcomiteRubro()
        {           
            return View();
        }

        [HttpPost]
        public String PVcomiteRubro( SIT_RESP_COMITERUBRO  redComiteRubMdl, int cmd)
        {
            if (redComiteRubMdl.corfecbaja.Ticks == Constantes.Tiempo.INICIAL)
                redComiteRubMdl.corfecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, redComiteRubMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_RESP_COMITERUBRODao>(nameof(SIT_RESP_COMITERUBRODao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DT_CI_RUBRO,
                     _sitDmlDbSer.operEjecutar<SIT_RESP_COMITERUBRODao>(nameof(SIT_RESP_COMITERUBRODao.dmlSelectCombo), null) as DataTable);
            }

            _appLog.opdesc = "SIT_RESP_COMITERUBRODao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_RESP_COMITERUBRODao.OPE_SELECT_COMBO ]";
            _appLog.data = JsonTransform.convertJson(redComiteRubMdl).ToString();
            
            return sRes;
        }

        [HttpGet]
        public IActionResult PVnodo()
        {
            _catViewMdl.lstArea = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<SIT_ADM_AREAHISTDao>(nameof(SIT_ADM_AREAHISTDao.dmlSelectComboFecActual), DateTime.Now) as List<ComboMdl>);
            _catViewMdl.lstNodoEstado = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RED_NODOESTADODao>(nameof(SIT_RED_NODOESTADODao.dmlSelectCombo), null));
            _catViewMdl.lstTipoProceso = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_PROCESODao>(nameof(SIT_SOL_PROCESODao.dmlSelectCombo), null));
            _appLog.opdesc = " SIT_ADM_AREADao.OPE_SELECT_COMBO,  SIT_RED_NODOESTADODao.OPE_SELECT_COMBO, SIT_SOL_PROCESODao.OPE_SELECT_COMBO";

            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVnodo(SIT_RED_NODO SIT_RED_NODO, int cmd)
        {
            if (SIT_RED_NODO.nodfeccreacion.Ticks == Constantes.Tiempo.INICIAL)
                SIT_RED_NODO.nodfeccreacion = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_RED_NODO);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlCRUD), dicParam).ToString();
            _appLog.opdesc = "SIT_RED_NODODao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(SIT_RED_NODO).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVnodoEstado()
        {
            _catViewMdl.lstTipoPerfil = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlSelectCombo), null));
            _appLog.opdesc = "AdmPerfilDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }
        [HttpPost]
        public String PVnodoEstado( SIT_RED_NODOESTADO SIT_RED_NODOESTADO, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_RED_NODOESTADO);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_RED_NODOESTADODao>(nameof(SIT_RED_NODOESTADODao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                // VERIFICAR SI ES NECESARIO OTRA ENTIDAD DEL NODO
                //REVISAR
                //REVISAR
                //REVISAR
                //REVISAR
                //////_memCacheSIT.AgregarDato(CacheWebSIT.DIC_NODO_ESTADO,
                //////    _sitDmlDbSer.operEjecutar<SIT_RED_NODOESTADODao>(nameof(SIT_RED_NODOESTADODao.), SIT_RED_NODOESTADODao.OPE_SELECT_NODO_ESTADO, null) as Dictionary<int, string>);
  


                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_NODO_ESTADO_URL,
                    _sitDmlDbSer.operEjecutar<SIT_RED_NODOESTADODao>(nameof(SIT_RED_NODOESTADODao.dmlSelectNodoEstadoUrl), null) as Dictionary<int, string> );

                //REVISAR
                //REVISAR
                //REVISAR
                //REVISAR
                //REVISAR
                //REVISAR
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_NODO_ESTADO_PERFIL,
                    _sitDmlDbSer.operEjecutar<SIT_RED_NODOESTADODao>(nameof(SIT_RED_NODOESTADODao.dmlSelectNodoEstado), null) as Dictionary<int, int> );
                //REVISAR
                //REVISAR
                //REVISAR
                //REVISAR
                //REVISAR
                //REVISAR
            }

            _appLog.opdesc = "SIT_RED_NODOESTADODao - CMD :" + cmd + " Actualizar _memCacheSIT: [SIT_RED_NODOESTADODao.OPE_SELECT_NODO_ESTADO, SIT_RED_NODOESTADODao.OPE_SELECT_DIC_NODO_ESTADO_URL, SIT_RED_NODOESTADODao.OPE_SELECT_DIC_NODO_ESTADO_PERFIL ]";
            _appLog.data = JsonTransform.convertJson(SIT_RED_NODOESTADO).ToString();

            
            return sRes;
        }

        [HttpGet]
        public IActionResult PVtipoArista()
        {
            return View();
        }

        [HttpPost]
        ////////////public String PVtipoArista( SIT_RESP_TIPO  tipoAriMdl, int cmd)
        ////////////{
        ////////////    Dictionary<string, object> dicParam = new Dictionary<string, object>();
        ////////////    dicParam.Add(BaseDao.CMD_OPERACION, cmd);
        ////////////    dicParam.Add(BaseDao.CMD_ENTIDAD, tipoAriMdl);

        ////////////    String sRes = _sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlCRUD), dicParam).ToString();
        ////////////    if (sRes != "0")
        ////////////    {
        ////////////        _memCacheSIT.AgregarDato("SIT_RESP_TIPODao",
        ////////////             _sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectCombo), null) as DataTable);

        ////////////        _memCacheSIT.AgregarDato(CacheWebSIT.DT_TIPO_ARISTA_DOC,
        ////////////             _sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectDicTabla), null) as DataTable);

        ////////////        _memCacheSIT.AgregarDato(CacheWebSIT.DIC_RESP_TIPO,
        ////////////             _sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectDiccionario), null) as Dictionary<int, string>);
        ////////////    }

        ////////////    _appLog.opdesc = "SIT_RESP_TIPODao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_RESP_TIPODao.OPE_SELECT_COMBO, SIT_RESP_TIPODao.OPE_SELECT_DT_DOCUMENTO, SIT_RESP_TIPODao.OPE_SELECT_DICCIONARIO]";
        ////////////    _appLog.data = JsonTransform.convertJson(tipoAriMdl).ToString();
            
        ////////////    return sRes;
        ////////////}

        //////////[HttpGet]
        //////////public IActionResult PVtipoInfo()
        //////////{
        //////////    _catViewMdl.lstTipoAri = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectCombo), null));
        //////////    _appLog.opdesc = "SIT_RESP_TIPODao.OPE_SELECT_COMBO";
        //////////    return View(_catViewMdl);
        //////////}

        //////////[HttpPost]
        //////////public String PVtipoInfo( SIT_RESP_TIPOINFO SIT_ARISTA_COMITERUBROl, int cmd)
        //////////{
        //////////    Dictionary<string, object> dicParam = new Dictionary<string, object>();
        //////////    dicParam.Add(BaseDao.CMD_OPERACION, cmd);
        //////////    dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_ARISTA_COMITERUBROl);

        //////////    String sRes = _sitDmlDbSer.operEjecutar<SIT_RESP_TIPOINFODao>(nameof(SIT_RESP_TIPOINFODao.dmlCRUD), dicParam).ToString();
        //////////    if (sRes != "0")
        //////////    {
        //////////        _memCacheSIT.AgregarDato(CacheWebSIT.DIC_RESP_CLASINFO,
        //////////            _sitDmlDbSer.operEjecutar<SIT_RESP_TIPOINFODao>(nameof(SIT_RESP_TIPOINFODao.dmlSelectDiccionario), null) as Dictionary<int, string>);
        //////////    }

        //////////    _appLog.opdesc = "SIT_RESP_TIPOINFODao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_RESP_TIPOINFODao.OPE_SELECT_DICCIONARIO ]";
        //////////    _appLog.data = JsonTransform.convertJson(SIT_ARISTA_COMITERUBROl).ToString();
            
        //////////    return sRes;
        //////////}

        [HttpGet]
        public IActionResult PVentFed()
        {
            _catViewMdl.lstPais = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_PAISDao>(nameof(SIT_SNT_PAISDao.dmlSelectCombo), null));
            _appLog.opdesc = "SIT_SNT_PAISDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVentFed(SIT_SNT_ESTADO SIT_SNT_ESTADO, int cmd)
        {
            if (SIT_SNT_ESTADO.edofecbaja.Ticks == Constantes.Tiempo.INICIAL)
                SIT_SNT_ESTADO.edofecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_SNT_ESTADO);


            String sRes = _sitDmlDbSer.operEjecutar<SIT_SNT_ESTADODao>(nameof(SIT_SNT_ESTADODao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_SNT_ESTADO,
                    _sitDmlDbSer.operEjecutar<SIT_SNT_ESTADODao>(nameof(SIT_SNT_ESTADODao.dmlSelectDiccionario),null) as Dictionary<int, string>);
            }
            _appLog.opdesc = "SIT_SNT_ESTADODao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_SNT_ESTADODao.OPE_SELECT_DICCIONARIO ]";
            _appLog.data = JsonTransform.convertJson(SIT_SNT_ESTADO).ToString();
            
            return sRes;
        }

        [HttpGet]
        public IActionResult PVmunicipio()
        {
            _catViewMdl.lstPais = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_PAISDao>(nameof(SIT_SNT_PAISDao.dmlSelectCombo),  null));
            _catViewMdl.lstEstado = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_ESTADODao>(nameof(SIT_SNT_ESTADODao.dmlSelectCombo),  null));
            _appLog.opdesc = "SIT_SNT_PAISDao.OPE_SELECT_COMBO, SIT_SNT_PAISDao.OPE_SELECT_COMBO";
                     
            return View(_catViewMdl);
        }
        [HttpPost]
        public String PVmunicipio( SIT_SNT_MUNICIPIO SIT_SNT_MUNICIPIO, int cmd)
        {
            if (SIT_SNT_MUNICIPIO.munfecbaja.Ticks == Constantes.Tiempo.INICIAL)
                SIT_SNT_MUNICIPIO.munfecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_SNT_MUNICIPIO);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SNT_MUNICIPIODao>(nameof(SIT_SNT_MUNICIPIODao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_SNT_MUNICIPIO,
                    _sitDmlDbSer.operEjecutar<SIT_SNT_MUNICIPIODao>(nameof(SIT_SNT_MUNICIPIODao.dmlSelectDiccionario), null) as Dictionary<int, string>);
            }
            _appLog.opdesc = "SIT_SNT_MUNICIPIODaoao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_SNT_MUNICIPIODaoao.OPE_SELECT_DICCIONARIO ]";
            _appLog.data = JsonTransform.convertJson(SIT_SNT_MUNICIPIO).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVocupacion()
        {
            _catViewMdl.lstPais = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_OCUPACIONDao>(nameof(SIT_SNT_OCUPACIONDao.dmlSelectCombo), null));
            _appLog.opdesc = "SIT_SNT_PAISDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVocupacion(SIT_SNT_OCUPACION SIT_SNT_MUNICIPIO, int cmd)
        {
            if (SIT_SNT_MUNICIPIO.ocufecbaja.Ticks == Constantes.Tiempo.INICIAL)
                SIT_SNT_MUNICIPIO.ocufecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_SNT_MUNICIPIO);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SNT_OCUPACIONDao>(nameof(SIT_SNT_OCUPACIONDao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_SNT_OCUPACION,
                     _sitDmlDbSer.operEjecutar<SIT_SNT_OCUPACIONDao>(nameof(SIT_SNT_OCUPACIONDao.dmlSelectDiccionario), null));
            }
            _appLog.opdesc = "SntOcupacionDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SntOcupacionDao.OPE_SELECT_DICCIONARIO ]";
            _appLog.data = JsonTransform.convertJson(SIT_SNT_MUNICIPIO).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVpais()
        {
            return View();
        }
        [HttpPost]
        public String PVpais( SIT_SNT_PAIS SIT_SNT_PAIS, int cmd)
        {
            if (SIT_SNT_PAIS.paifecbaja.Ticks == Constantes.Tiempo.INICIAL)
                SIT_SNT_PAIS.paifecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_SNT_PAIS);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SNT_PAISDao>(nameof(SIT_SNT_PAISDao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_SNT_PAIS,
                     _sitDmlDbSer.operEjecutar<SIT_SNT_PAISDao>(nameof(SIT_SNT_PAISDao.dmlSelectDiccionario), null) as Dictionary<int, string>);
            }
            _appLog.opdesc = "SIT_SNT_PAISDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_SNT_PAISDao.OPE_SELECT_DICCIONARIO ]";
            _appLog.data = JsonTransform.convertJson(SIT_SNT_PAIS).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVtipoSnt()
        {
            return View();
        }

        [HttpPost]
        public String PVtipoSnt( SIT_SNT_SOLICITANTETIPO sntTipoSntMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, sntTipoSntMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SNT_SOLICITANTETIPODao>(nameof(SIT_SNT_SOLICITANTETIPODao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_SNT_TIPO,
                    _sitDmlDbSer.operEjecutar<SIT_SNT_SOLICITANTETIPODao>(nameof(SIT_SNT_SOLICITANTETIPODao.dmlSelectDiccionario), null) as Dictionary<int, string>);
            }
            _appLog.opdesc = "SIT_SNT_SOLICITANTEDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_SNT_SOLICITANTEDao.OPE_SELECT_DICCIONARIO ]";
            _appLog.data = JsonTransform.convertJson(sntTipoSntMdl).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVsolicitante()
        {
            _catViewMdl.lstPais = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_PAISDao>(nameof(SIT_SNT_PAISDao.dmlSelectCombo),  null));
            _catViewMdl.lstEstado = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_ESTADODao>(nameof(SIT_SNT_ESTADODao.dmlSelectCombo),  null));
            _catViewMdl.lstMunicipio = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_MUNICIPIODao>(nameof(SIT_SNT_MUNICIPIODao.dmlSelectCombo),  null));
            _catViewMdl.lstOcupacion = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_OCUPACIONDao>(nameof(SIT_SNT_OCUPACIONDao.dmlSelectCombo),  null));
            _catViewMdl.lstTipoSolicitante = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_SOLICITANTETIPODao>(nameof(SIT_SNT_SOLICITANTETIPODao.dmlSelectCombo),  null));
            _appLog.opdesc = "SIT_SNT_PAISDao.OPE_SELECT_COMBO, SIT_SNT_ESTADODao.OPE_SELECT_COMBO,  SIT_SNT_MUNICIPIODaoao.OPE_SELECT_COMBO, SntOcupacionDao.OPE_SELECT_COMBO, SIT_SNT_SOLICITANTEDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }
        [HttpPost]
        public String PVsolicitante( SIT_SNT_SOLICITANTE solicitanteMDL, int cmd)
        {
            if (solicitanteMDL.sntfecnac.Ticks == Constantes.Tiempo.INICIAL)
                solicitanteMDL.sntfecnac = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, solicitanteMDL);


            String sRes = _sitDmlDbSer.operEjecutar<SIT_SNT_SOLICITANTEDao>(nameof(SIT_SNT_SOLICITANTEDao), dicParam).ToString();
            _appLog.opdesc = "SIT_SNT_SOLICITANTEDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(solicitanteMDL).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVproceso()
        {
            _catViewMdl.lstPais = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_PAISDao>(nameof(SIT_SNT_PAISDao.dmlSelectCombo), null));
            _appLog.opdesc = "SIT_SNT_PAISDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVproceso( SIT_SOL_SOLICITUD SIT_SOL_PROCESO, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, SIT_SOL_PROCESO);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.LST_SOL_PROCESO,
                     _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectCombo),  null) as List<ComboMdl>);
            }
            _appLog.opdesc = "SIT_SOL_PROCESODao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_SOL_PROCESODao.OPE_SELECT_COMBO ]";
            _appLog.data = JsonTransform.convertJson(SIT_SOL_PROCESO).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVprocesoPlazo()
        {
            _catViewMdl.lstTipoProceso = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_PROCESODao>(nameof(SIT_SOL_PROCESODao.dmlSelectCombo), null));
            _catViewMdl.lstTipoSolicitud = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDTIPODao>(nameof(SIT_SOL_SOLICITUDTIPODao.dmlSelectCombo), null));
            _catViewMdl.lstAFD = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RED_AFDDao>(nameof(SIT_RED_AFDDao.dmlSelectCombo), null));

            _appLog.opdesc = "SIT_SOL_PROCESODao.OPE_SELECT_COMBO, SolTipoSIT_SOL_SOLICITUDDao.OPE_SELECT_COMBO, RedAfdDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVprocesoPlazo( SIT_SOL_PROCESOPLAZOS solProcesoPlazoMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, solProcesoPlazoMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SOL_PROCESOPLAZOSDao>(nameof(SIT_SOL_PROCESOPLAZOSDao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.LST_SOL_PROCESOPLAZOS,
                     _sitDmlDbSer.operEjecutar<SIT_SOL_PROCESOPLAZOSDao>(nameof(SIT_SOL_PROCESOPLAZOSDao.dmlSelectTabla),  null) as List<SIT_SOL_PROCESOPLAZOS>);
            }
            _appLog.opdesc = "SIT_SOL_PROCESOPLAZOSDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_SOL_PROCESOPLAZOSDao.OPE_SELECT_LISTA ]";
            _appLog.data = JsonTransform.convertJson(solProcesoPlazoMdl).ToString();
            return sRes;
        }

        [HttpGet]
        public IActionResult PVtipoModoEntrega()
        {
            _catViewMdl.lstPais = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SNT_PAISDao>(nameof(SIT_SNT_PAISDao.dmlSelectCombo),  null));
            _appLog.opdesc = "SIT_SNT_PAISDao.OPE_SELECT_COMBO";
            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVtipoModoEntrega( SIT_SOL_MODOENTREGA modoEntregaMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, modoEntregaMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SOL_MODOENTREGADao>(nameof(SIT_SOL_MODOENTREGADao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato(CacheWebSIT.DIC_SOL_MODOENTREGA_VISIBLE,
                    _sitDmlDbSer.operEjecutar<SIT_SOL_MODOENTREGADao>(nameof(SIT_SOL_MODOENTREGADao.dmlSelectDiccionario), null) as Dictionary<int, string>);
            }
            _appLog.opdesc = "SIT_SOL_MODOENTREGADao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SIT_SOL_MODOENTREGADao.OPE_SELECT_DICCIONARIO ]";
            _appLog.data = JsonTransform.convertJson(modoEntregaMdl).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVtipoSol()
        {
            return View();
        }

        [HttpPost]
        public String PVtipoSol( SIT_SOL_SOLICITUDTIPO tipoSolMdl, int cmd)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, tipoSolMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDTIPODao>(nameof(SIT_SOL_SOLICITUDTIPODao.dmlCRUD), dicParam).ToString();
            if (sRes != "0")
            {
                _memCacheSIT.AgregarDato("SolTipoSIT_SOL_SOLICITUDDao",
                    _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDTIPODao>(nameof(SIT_SOL_SOLICITUDTIPODao.dmlSelectCombo), null) as DataTable);
            }
            _appLog.opdesc = "SolTipoSIT_SOL_SOLICITUDDao - CMD :" + cmd + " Actualizar _memCacheSIT: [ SolTipoSIT_SOL_SOLICITUDDao.OPE_SELECT_COMBO ]";
            _appLog.data = JsonTransform.convertJson(tipoSolMdl).ToString();

            return sRes;
        }

        //////////////[HttpGet]
        //////////////public IActionResult PVseguimiento()
        //////////////{
        //////////////    _catViewMdl.lstTipoProceso = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_PROCESODao>(nameof(SIT_SOL_PROCESODao.dmlSelectCombo), null));
        //////////////    _catViewMdl.lstTipoAri = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectCombo),  null));
        //////////////    _catViewMdl.lstAFD = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_RED_AFDDao>(nameof(SIT_RED_AFDDao.dmlSelectCombo), null));
        //////////////    _appLog.opdesc = "SIT_SOL_PROCESODao.OPE_SELECT_COMBO, SIT_RESP_TIPODao.OPE_SELECT_COMBO, RedAfdDao.OPE_SELECT_COMBO";
        //////////////    return View(_catViewMdl);
        //////////////}

        [HttpPost]
        public String PVseguimiento( SIT_SOL_SEGUIMIENTO solSeguimiento, int cmd)
        {
            if (solSeguimiento.segfecamp.Ticks == Constantes.Tiempo.INICIAL)
                solSeguimiento.segfecamp = DateTime.MinValue;
            if (solSeguimiento.segfeccalculo.Ticks == Constantes.Tiempo.INICIAL)
                solSeguimiento.segfeccalculo = DateTime.MinValue;
            if (solSeguimiento.segfecestimada.Ticks == Constantes.Tiempo.INICIAL)
                solSeguimiento.segfecestimada = DateTime.MinValue;
            if (solSeguimiento.segfecfin.Ticks == Constantes.Tiempo.INICIAL)
                solSeguimiento.segfecfin = DateTime.MinValue;
            if (solSeguimiento.segfecini.Ticks == Constantes.Tiempo.INICIAL)
                solSeguimiento.segfecini = DateTime.MinValue;
            _appLog.opdesc = "SIT_SOL_SEGUIMIENTODao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(solSeguimiento).ToString();

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, solSeguimiento);

            return _sitDmlDbSer.operEjecutar<SIT_SOL_SEGUIMIENTODao>(nameof(SIT_SOL_SEGUIMIENTODao.dmlCRUD), dicParam).ToString();
        }

        [HttpGet]
        public IActionResult PVmedioEntrada()
        {
            return View();
        }

        [HttpPost]
        public String PVmedioEntrada(SIT_SOL_MEDIOENTRADA  medioEntradaMdl, int cmd)
        {           
            if (medioEntradaMdl.metfecbaja.Ticks == Constantes.Tiempo.INICIAL)
                medioEntradaMdl.metfecbaja = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, medioEntradaMdl);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SOL_MEDIOENTRADADao>(nameof(SIT_SOL_MEDIOENTRADADao.dmlCRUD), dicParam).ToString();
            _appLog.opdesc = "SolTipoMedioEntradaDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(medioEntradaMdl).ToString();

            return sRes;
        }

        [HttpGet]
        public IActionResult PVsolicitud()
        {
            _catViewMdl.lstMedioEntrada = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_MEDIOENTRADADao>(nameof(SIT_SOL_MEDIOENTRADADao.dmlSelectCombo),  null));
            _catViewMdl.lstTipoSolicitud = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDTIPODao>(nameof(SIT_SOL_SOLICITUDTIPODao.dmlSelectCombo),  null));
            _catViewMdl.lstModoEntrega = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_MODOENTREGADao>(nameof(SIT_SOL_MODOENTREGADao.dmlSelectCombo), null));
            _catViewMdl.lstTipoProceso = JsonTransform.convertJson((DataTable)_sitDmlDbSer.operEjecutar<SIT_SOL_PROCESODao>(nameof(SIT_SOL_PROCESODao.dmlSelectCombo),  null));
            _appLog.opdesc = "SolTipoMedioEntradaDao.OPE_SELECT_COMBO, SolTipoSIT_SOL_SOLICITUDDao.OPE_SELECT_COMBO,SIT_SOL_MODOENTREGADao.OPE_SELECT_COMBO, SolTipoRubroTematicoDao.OPE_SELECT_COMBO, SIT_SOL_PROCESODao.OPE_SELECT_COMBO, SolUEnlaceDao.OPE_SELECT_COMBO ";

            return View(_catViewMdl);
        }

        [HttpPost]
        public String PVsolicitud( SIT_SOL_SOLICITUD  solSolicitud, int cmd)
        {
            if (solSolicitud.solfecacl.Ticks == Constantes.Tiempo.INICIAL)
                solSolicitud.solfecacl = DateTime.MinValue;
            if (solSolicitud.solfecent.Ticks == Constantes.Tiempo.INICIAL)
                solSolicitud.solfecent = DateTime.MinValue;
            if (solSolicitud.solfecenv.Ticks == Constantes.Tiempo.INICIAL)
                solSolicitud.solfecenv = DateTime.MinValue;
            if (solSolicitud.solfecresp.Ticks == Constantes.Tiempo.INICIAL)
                solSolicitud.solfecresp = DateTime.MinValue;
            if (solSolicitud.solfecsol.Ticks == Constantes.Tiempo.INICIAL)
                solSolicitud.solfecsol = DateTime.MinValue;
            if (solSolicitud.solfecrec.Ticks == Constantes.Tiempo.INICIAL)
                solSolicitud.solfecrec = DateTime.MinValue;
            if (solSolicitud.solfecrecrev.Ticks == Constantes.Tiempo.INICIAL)
                solSolicitud.solfecrecrev = DateTime.MinValue;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(BaseDao.CMD_OPERACION, cmd);
            dicParam.Add(BaseDao.CMD_ENTIDAD, solSolicitud);

            String sRes = _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlCRUD), dicParam).ToString();
            _appLog.opdesc = "SIT_SOL_SOLICITUDDao - CMD :" + cmd;
            _appLog.data = JsonTransform.convertJson(solSolicitud).ToString();
            return sRes;
        }

        /* ******************************
            CONSULTAR LOS DATOS DE LA TABLA 
        ****************************** */
        [HttpGet]
        public String Consulta(string Entidad, int PagAct, int PagMaxReg)
        {
            Dictionary<int, string> dicCatalogos = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_CATALOGOS_CLASE) as Dictionary<int, string>;
            BasePagMdl baseMdl = new BasePagMdl(PagAct, PagMaxReg);

            string sJson = null;
            string sObj = dicCatalogos[Convert.ToInt32(Entidad)];
            {                
                sJson = JsonTransform.convertJsonNoRecID((DataTable)_sitDmlDbSer.operEjecutar(sObj,  BaseDao.CMD_GRID, baseMdl));
                _httpContextAccessor.HttpContext.Response.Headers["Expires"] = "-1";
                _httpContextAccessor.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store";
                _httpContextAccessor.HttpContext.Response.Headers["Pragma"] = "no-cache";
                _httpContextAccessor.HttpContext.Response.ContentType = "application/json; charAgregarDato=UTF-8";
            }

            _appLog.opdesc = "CacheWebSIT.DIC_CATALOGOS_CLASE";

            
            return sJson;
        }

        [HttpGet]
        public String ConsultaArbol(string Entidad, string Fecha)
        {
            // TODO CAMBIA..


            ////////Dictionary<int, string> dicCatalogos = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_CATALOGOS_CLASE) as Dictionary<int, string>;
            ////////Dictionary<string, object> dicParam = new Dictionary<string, object>();


            ////////// ACTUALZIAR CON BASE A LA FECHA..
            ////////// ACTUALZIAR CON BASE A LA FECHA..
            ////////// ACTUALZIAR CON BASE A LA FECHA..
            ////////// ACTUALZIAR CON BASE A LA FECHA..
            ////////// ACTUALZIAR CON BASE A LA FECHA..
            ////////dicParam.Add(AdmAreaHistDao.PARAM_FECHA, DateTime.Now);


            string sJson = null;
            ////////Type tyObj = Type.GetType(dicCatalogos[Convert.ToInt32(Entidad)]);

            ////////if (tyObj != null)
            ////////{
            ////////    sJson = JsonTransform.convertJsonNoRecID((DataTable)_sitDmlDbSer.operEjecutar(tyObj, BaseDao.OPE_SELECT_TABLE, dicParam));
            ////////    _httpContextAccessor.HttpContext.Response.Headers["Expires"] = "-1";
            ////////    _httpContextAccessor.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store";
            ////////    _httpContextAccessor.HttpContext.Response.Headers["Pragma"] = "no-cache";
            ////////    _httpContextAccessor.HttpContext.Response.ContentType = "application/json; charAgregarDato=UTF-8";
            ////////}

            ////////_appLog.opdesc = "CacheWebSIT.DIC_CATALOGOS_CLASE";

            ////////// IActionResult   Json( (DataTable) _sitDmlDbSer.operEjecutar(tyObj, BaseDao.OPE_SELECT_GRID, baseMdl) );
            return sJson;
        }        
    }
}