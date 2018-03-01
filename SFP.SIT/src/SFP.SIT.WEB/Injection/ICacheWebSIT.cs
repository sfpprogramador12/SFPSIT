using Microsoft.Extensions.Configuration;
using SFP.Persistencia.Model;
using SFP.Persistencia.Servicio;
using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Dao.DOC;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Dao.SNT;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Injection
{
    public interface ICacheWebSIT
    {
        Object ObtenerDato(String Clave);
        void AgregarDato(String Clave, Object Valor);
        void Inicializar(IConfigurationRoot config);
        int EstadoFinal(Int32 iEstadoInicial, Int32 iArista);
        string BuscarLista(string sColeccion, string sValor);
        string BuscarDiccionario(string sColeccion, int iValor);
    }

    public class CacheWebSIT : ICacheWebSIT
    {
        public const string APP_BD_CONFIG = "BD_CONFIG";
        public const string APP_SIT_CONEXION_BD = "SIT_DB";
        public const string APP_SESSION_TIMEOUT = "SessionTimeout";
        public const string APP_SHAREPOINT_CONFIG = "SHAREPOINT_CONFIG";
        public const string APP_CORREO_CONFIG = "CORREO_CONFIG";

        public const String CONEXION = "CONEXION";

        public const int AREA_ORIGEN = 1;

        public const String DIC_CATALOGOS_CLASE = "DIC_CATALOGOS_CLASE";
        public const String DIC_DIA_NO_LABORAL = "DIC_DIA_NO_LABORAL";

        public const String DIC_AFD_PREFIJO = "DIC_AFD_PREFIJO";
        public const String DIC_AFD_FLUJO = "DIC_AFD_FLUJO";

        public const String DIC_SOL_MODOENTREGA = "DIC_SOL_MODOENTREGA";
        public const String DIC_SOL_MODOENTREGA_VISIBLE = "DIC_SOL_MODOENTREGA_VISIBLE";

        public const String DIC_NODO_ESTADO = "DIC_NODO_ESTADO";
        public const String DIC_NODO_ESTADO_URL = "DIC_NODO_ESTADO_URL";
        public const String DIC_NODO_ESTADO_PERFIL = "DIC_NODO_ESTADO_PERFIL";

        
        
        public const String DIC_DOC_EXTENSION = "DIC_DOC_EXTENSION";          
        
               
        public const String DIC_SNT_ESTADO = "DIC_SNT_ESTADO";
        public const String DIC_SNT_MUNICIPIO = "DIC_SNT_MUNICIPIO";
        public const String DIC_SNT_OCUPACION = "DIC_SNT_OCUPACION";
        public const String DIC_SNT_PAIS = "DIC_SNT_PAIS";
        public const String DIC_SNT_TIPO = "DIC_SNT_TIPO";

        public const String DIC_RESP_CLASINFO = "DIC_RESP_CLASINFO";
        public const String DIC_RESP_TIPO = "DIC_RESP_TIPO";
        public const string DIC_RESP_MOMENTO = "DIC_RESP_MOMENTO";
        public const String DIC_RESP_REPRODUCCION = "DIC_RESP_REPRODUCCION";
        public const String DIC_RESP_INAI = "DIC_RESP_INAI";


        public const String DT_TIPO_ARISTA_DOC = "DT_TIPO_ARISTA_DOC";
        public const string DT_CI_RUBRO = "DT_CI_RUBRO";

        public const string LST_SOL_PROCESOPLAZOS = "LST_SOL_PROCESOPLAZOS";
        public const string LST_SOL_TIPO = "LST_SOL_TIPO";
        public const string LST_SOL_PROCESO = "LST_SOL_PROCESO";
        public const string LST_PERFIL = "LST_PERFIL";
        

        Dictionary<String, Object> _dicCacheSIT;
        public CacheWebSIT()
        {
            _dicCacheSIT = new Dictionary<String, Object>();
        }

        public void AgregarDato(string Clave, object Valor)
        {
            if (_dicCacheSIT.ContainsKey(Clave) == false)
            {
                _dicCacheSIT.Add(Clave, Valor);
            }
            else
            {
                _dicCacheSIT[Clave] =  Valor;
            }
        }

        public object ObtenerDato(string Clave)
        {
            return _dicCacheSIT[Clave];
        }

        private string ObtenerConfiguracion(ref DmlDbSer oracleSer, string sClave)
        {
            String sConsultaClave;
            sConsultaClave = (String)oracleSer.operEjecutar<SIT_ADM_CONFIGURACIONDao>(nameof(SIT_ADM_CONFIGURACIONDao.dmlSelectClave), sClave);
            return sConsultaClave;
        }

        public void Inicializar(IConfigurationRoot config)
        {
            try
            {
                BaseDbMdl baseDbSit = new BaseDbMdl();
                baseDbSit.connectionString = config.GetConnectionString(CacheWebSIT.APP_SIT_CONEXION_BD);
                baseDbSit.objDbConnection = config.GetSection("DataBaseObj")["objCxn"].ToString();
                baseDbSit.objDbTransaccion = config.GetSection("DataBaseObj")["objTran"].ToString();
                baseDbSit.objDbDataAdapter = config.GetSection("DataBaseObj")["objDataAdapter"].ToString();

                _dicCacheSIT.Add(CacheWebSIT.APP_BD_CONFIG, baseDbSit);

                CfgSharePointMdl spModel = new CfgSharePointMdl(
                    config.GetSection("SharePoint")["Servidor"].ToString(),
                    config.GetSection("SharePoint")["Usuario"].ToString(),
                    config.GetSection("SharePoint")["Contraseña"].ToString(),
                    config.GetSection("SharePoint")["Dominio"].ToString(),
                    config.GetSection("SharePoint")["Folder"].ToString()
                );
                _dicCacheSIT.Add(APP_SHAREPOINT_CONFIG, spModel);

                CfgCorreoMdl sCorreo = new CfgCorreoMdl(
                    config.GetSection("correo")["Servidor"].ToString(),
                    config.GetSection("correo")["Puerto"].ToString(),
                    config.GetSection("correo")["Usuario"].ToString(),
                    config.GetSection("correo")["Contraseña"].ToString()
                    );
                _dicCacheSIT.Add(APP_CORREO_CONFIG, sCorreo);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error al leer archivo JSON de configuración : " + ex.ToString());
            }

            try
            {
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                DmlDbSer sitOperSer = new DmlDbSer( _dicCacheSIT[CacheWebSIT.APP_BD_CONFIG] as BaseDbMdl );

                // ADMINISTRACION
                _dicCacheSIT.Add(DIC_CATALOGOS_CLASE, sitOperSer.operEjecutar<SIT_ADM_CLASESDao>(nameof(SIT_ADM_CLASESDao.dmlSelectDiccionarioNombre), null) as  Dictionary<int, string>);               
                _dicCacheSIT.Add(DIC_DIA_NO_LABORAL, sitOperSer.operEjecutar<SIT_ADM_DIANOLABORALDao>(nameof(SIT_ADM_DIANOLABORALDao.dmlSelectDiccionarioDiaLaboral),  null) as Dictionary<Int64, char> );
                _dicCacheSIT.Add(LST_PERFIL, sitOperSer.operEjecutar<SIT_ADM_PERFILDao>(nameof(SIT_ADM_PERFILDao.dmlSelectCombo), null) as List<ComboMdl>);
                
                // ADM _CONFIGURACION
                _dicCacheSIT.Add(Constantes.CfgClavesRegistro.CLAVE_INST, sitOperSer.operEjecutar<SIT_ADM_CONFIGURACIONDao>(nameof(SIT_ADM_CONFIGURACIONDao.dmlSelectClave), Constantes.CfgClavesRegistro.CLAVE_INST) as string);
                Int32 iFlujoTrabajo = Convert.ToInt32(sitOperSer.operEjecutar<SIT_ADM_CONFIGURACIONDao>(nameof(SIT_ADM_CONFIGURACIONDao.dmlSelectClave), Constantes.CfgClavesRegistro.FLUJO));
                _dicCacheSIT.Add(Constantes.CfgClavesRegistro.FLUJO, Convert.ToInt32(iFlujoTrabajo));
                _dicCacheSIT.Add(Constantes.CfgClavesRegistro.HORA_PROCESO, sitOperSer.operEjecutar<SIT_ADM_CONFIGURACIONDao>(nameof(SIT_ADM_CONFIGURACIONDao.dmlSelectClave), Constantes.CfgClavesRegistro.HORA_PROCESO) as string);
                _dicCacheSIT.Add(Constantes.CfgClavesRegistro.INAI, Convert.ToInt32(sitOperSer.operEjecutar<SIT_ADM_CONFIGURACIONDao>(nameof(SIT_ADM_CONFIGURACIONDao.dmlSelectClave), Constantes.CfgClavesRegistro.INAI) as string));
                _dicCacheSIT.Add(Constantes.CfgClavesRegistro.UT, Convert.ToInt32(sitOperSer.operEjecutar<SIT_ADM_CONFIGURACIONDao>(nameof(SIT_ADM_CONFIGURACIONDao.dmlSelectClave), Constantes.CfgClavesRegistro.UT) as string));
                _dicCacheSIT.Add(Constantes.CfgClavesRegistro.USR_TRANSPARENCIA, Convert.ToInt32(sitOperSer.operEjecutar<SIT_ADM_CONFIGURACIONDao>(nameof(SIT_ADM_CONFIGURACIONDao.dmlSelectClave), Constantes.CfgClavesRegistro.USR_TRANSPARENCIA) as string));

                // AFD
                dicParam.Add(AfdServicio.PARAM_FLUJO_TRABAJO_ID, iFlujoTrabajo);
                _dicCacheSIT.Add(DT_CI_RUBRO, sitOperSer.operEjecutar<SIT_RESP_COMITERUBRODao>(nameof(SIT_RESP_COMITERUBRODao.dmlSelectCombo), null) as DataTable);                
                _dicCacheSIT.Add(DIC_AFD_FLUJO, sitOperSer.operEjecutar<AfdServicio>(nameof(AfdServicio.Inicializar), dicParam) as Dictionary<Int32, AfdNodoFlujo>);

                // DOCUMENTO
                _dicCacheSIT.Add(DIC_DOC_EXTENSION, sitOperSer.operEjecutar<SIT_DOC_EXTENSIONDao>(nameof(SIT_DOC_EXTENSIONDao.dmlSelectDicTipoExtension), null) as Dictionary<string, SIT_DOC_EXTENSION> );

                // RESPUESTA
                _dicCacheSIT.Add(DIC_RESP_CLASINFO, sitOperSer.operEjecutar<SIT_RESP_CLASINFODao>(nameof(SIT_RESP_CLASINFODao.dmlSelectDiccionario), null) as Dictionary<int, String> );
                _dicCacheSIT.Add(DIC_RESP_TIPO, sitOperSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectDiccionario), null) as Dictionary<int, String>);
                _dicCacheSIT.Add(DIC_RESP_MOMENTO, sitOperSer.operEjecutar<SIT_RESP_MOMENTODao>(nameof(SIT_RESP_MOMENTODao.dmlSelectDiccionario), null) as Dictionary<int, String>);
                _dicCacheSIT.Add(DIC_RESP_REPRODUCCION, sitOperSer.operEjecutar<SIT_RESP_REPRODUCCIONDao>(nameof(SIT_RESP_REPRODUCCIONDao.dmlSelectDiccionario), null) as Dictionary<int, String>);
                _dicCacheSIT.Add(DIC_RESP_INAI, sitOperSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectRespTipo), 1) as Dictionary<int, String>);




                // SOLICITANTE
                _dicCacheSIT.Add(DIC_SNT_PAIS, sitOperSer.operEjecutar<SIT_SNT_PAISDao>(nameof(SIT_SNT_PAISDao.dmlSelectDiccionario), null) as  Dictionary<int, String> );
                _dicCacheSIT.Add(DIC_SNT_ESTADO, sitOperSer.operEjecutar<SIT_SNT_ESTADODao>(nameof(SIT_SNT_ESTADODao.dmlSelectDiccionario),  null) as Dictionary<int, String> );
                _dicCacheSIT.Add(DIC_SNT_MUNICIPIO,sitOperSer.operEjecutar<SIT_SNT_MUNICIPIODao>( nameof(SIT_SNT_MUNICIPIODao.dmlSelectDiccionario), null) as  Dictionary<int, String> );
                _dicCacheSIT.Add(DIC_SNT_OCUPACION, sitOperSer.operEjecutar<SIT_SNT_OCUPACIONDao>(nameof(SIT_SNT_OCUPACIONDao.dmlSelectDiccionario), null) as Dictionary<int, String> );
                _dicCacheSIT.Add(DIC_SNT_TIPO, sitOperSer.operEjecutar<SIT_SNT_SOLICITANTETIPODao>(nameof(SIT_SNT_SOLICITANTETIPODao.dmlSelectDiccionario), null) as Dictionary<int, String> );

                // SOLICITUD
                _dicCacheSIT.Add(DIC_SOL_MODOENTREGA, sitOperSer.operEjecutar<SIT_SOL_MODOENTREGADao>(nameof(SIT_SOL_MODOENTREGADao.dmlSelectDiccionario), null) as Dictionary<int, String>);
                _dicCacheSIT.Add(DIC_SOL_MODOENTREGA_VISIBLE, sitOperSer.operEjecutar<SIT_SOL_MODOENTREGADao>(nameof(SIT_SOL_MODOENTREGADao.dmlSelectDicMostrar), null) as Dictionary<int, String>);
                _dicCacheSIT.Add(LST_SOL_PROCESOPLAZOS, sitOperSer.operEjecutar<SIT_SOL_PROCESOPLAZOSDao>(nameof(SIT_SOL_PROCESOPLAZOSDao.dmlSelectLista), null) as List<SIT_SOL_PROCESOPLAZOS>);
                _dicCacheSIT.Add(LST_SOL_TIPO, sitOperSer.operEjecutar<SIT_SOL_SOLICITUDTIPODao>(nameof(SIT_SOL_SOLICITUDTIPODao.dmlSelectCombo),  null) as List<ComboMdl>);
                _dicCacheSIT.Add(LST_SOL_PROCESO, sitOperSer.operEjecutar<SIT_SOL_PROCESODao>(nameof(SIT_SOL_PROCESODao.dmlSelectCombo), null) as List<ComboMdl>);

                // NODOS
                _dicCacheSIT.Add(DIC_NODO_ESTADO, sitOperSer.operEjecutar<SIT_RED_NODOESTADODao>(nameof(SIT_RED_NODOESTADODao.dmlSelectDiccionario), null) as Dictionary<int, string>);
                _dicCacheSIT.Add(DIC_NODO_ESTADO_URL, sitOperSer.operEjecutar<SIT_RED_NODOESTADODao>(nameof(SIT_RED_NODOESTADODao.dmlSelectNodoEstadoUrl), null) as Dictionary<int, string>);
                _dicCacheSIT.Add(DIC_NODO_ESTADO_PERFIL, sitOperSer.operEjecutar<SIT_ADM_PERFILNODODao>(nameof(SIT_ADM_PERFILNODODao.dmlSelectDicPerfilNodo), null) as Dictionary<int, List<int>>);
                
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error al leer en la Base de Datos : " + ex.ToString());
            }
        }

        public string BuscarLista(string sColeccion, string sValor)
        {
            string sResultado = "";
            List<ComboMdl> lstDatos = _dicCacheSIT[sColeccion] as List<ComboMdl>;

            var busqueda = lstDatos.Find(r => r.ID == sValor).DESCRIP;
            if (busqueda != null)
                sResultado = busqueda.ToString();

            return sResultado;
        }

        public string BuscarDiccionario(string sColeccion, int iValor)
        {
            string sResultado = "";
            Dictionary<int, string> dicDatos = _dicCacheSIT[sColeccion] as Dictionary<int, string>;

            if (dicDatos.ContainsKey(iValor))
                return dicDatos[iValor];
            else
                return sResultado;
        }

        public int EstadoFinal(Int32 iEstadoInicial, Int32 iArista)
        {
            int iEstadoFinal = 0;

            Dictionary<Int32, AfdNodoFlujo> dicAfdFlujo = _dicCacheSIT[DIC_AFD_FLUJO] as Dictionary<Int32, AfdNodoFlujo>;
            AfdNodoFlujo nodoFlujo = dicAfdFlujo[iEstadoInicial];
            iEstadoFinal = nodoFlujo.dicAccionEstado[iArista];
            return iEstadoFinal;
        }

        ////////public int EstadoFinal(Int32 iEstadoInicial, Int32 iArista)
        ////////{
        ////////    DataTable dtAfdNodo = _dicCacheSIT[DT_AFD_NODO] as DataTable;
        ////////    int iEstadoFinal = 0;

        ////////    //afdclave, afforigen, rtpclave, affdestino
        ////////    foreach (DataRow drDatos in dtAfdNodo.Rows)
        ////////    {
        ////////        if (Convert.ToInt32(drDatos["afforigen"]) == iEstadoInicial && Convert.ToInt32(drDatos["rtpclave"]) == iArista)
        ////////        {
        ////////            iEstadoFinal = Convert.ToInt32(drDatos["affdestino"]);
        ////////            break;
        ////////        }
        ////////    }
        ////////    return iEstadoFinal;
        ////////}
    }
}
