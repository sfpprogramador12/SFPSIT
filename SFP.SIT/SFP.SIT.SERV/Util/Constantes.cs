using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Util
{
    public class Constantes
    {
        public static class Capa
        {
            public const int NIVEL_CERO = 0;
        }

        public static class CfgClavesRegistro
        {
            public const string CLAVE_INST = "CLAVE_INST";
            public const string FLUJO = "FLUJO";            
            public const string HORA_PROCESO = "HORA_PROCESO";            
            public const string INAI = "INAI";
            public const string UT = "UT";            
            public const string USR_TRANSPARENCIA = "USR_UT";
        }

        public static class ConfiguracionArchivo
        {
            public const string SHAREPOINT_SERVER = "Server";
            public const string SHAREPOINT_USUARIO = "Usuario";
            public const string SHAREPOINT_CONTRASEÑA = "Contraseña";
            public const string SHAREPOINT_DOMINIO = "Dominio";
        }


        public static class Comite
        {
            public const int CONFIRMAR = 1;
            public const int MODIFICAR = 2;
            public const int REVOCAR = 3;
        }

        public static class DocumentoTipo
        {
            public const int OFICIO = 1;
        }

        public static class General
        {
            public const int ID_PENDIENTE = 0;
        }

        public static class Infomex
        {
            public const int ARCHIVO_ERROR = 0;
            public const int ARCHIVO_ACLARACION = 1;
            public const int ARCHIVO_SOLICITUD_VER2 = 2;
            public const int ARCHIVO_SOLICITUD_VER3 = 3;

            public const int ARCHIVO_CAMPOS_ACLARACION = 6;
            public const int ARCHIVO_CAMPOS_SOLICITUD_VER2 = 30;
            public const int ARCHIVO_CAMPOS_SOLICITUD_VER3 = 59;
        }

        public static class ModoEntrada
        {
            public const int NO_ESPECIFICADO = 0;
        }

        public static class NodoEstado
        {
            public const string PREFIJO = "Edo";

            public const int INDEFINIDO = 0;

            public const int INAI_SOLICITUD = 1;
            public const int INAI_RESPUESTA = 2;
            public const int INAI_RECURSO_REVISION = 11;

            public const int UT_RECIBIR_SOLICITUD = 3;

            public const int PRUD_RECIBIR_SOLICITUD = 4;
            public const int PRUD_REVISARRESP_SOLICITUD = 5;
            public const int PRUD_NOTIFICAR = 6;

            public const int UA_ANALIZAR_SOLICITUD = 7;
            public const int UA_CORREGIR_SOLICITUD = 8;
            public const int UA_INFO_COMITE_SOLICITUD = 9;

            public const int CT_SESION_SOLICITUD = 10;
        }

        public static class NodoEstadoTipo
        {

            public const int NINGUNO = 0;
        }

        public static class Paquetes
        {
            public const string AFD_CORE = "SFP.AFD.Core";
            public const string DAO_ADM = "SFP.SIT.SERVICES.Dao.Adm.";

        }

        public static class Parametro
        {
            public const string FECHA = "FECHA";
        }



        /* SE DEBE DE ELINAR ESTE TIPO DE PERFILES */
        public static class Perfil
        {
            public const int SISTEMAS = 1;
            public const int INAI = 2;
            public const int UT = 3;
            public const int PRUT = 4;
            public const int UA = 5;
            public const int CT = 6;
            public const int RF = 7;
        }

        public static class ProcesoTipo
        {
            public const int SOLICITUD = 1;
            public const int ACLARACION = 2;
            public const int RECURSO_REVISION = 3;
        }

        public static class ProcesoTiempo
        {
            public const int NO_AMPLIACION = 1;
            public const int AMPLIACION = 2;
        }


        public static class Respuesta
        {
            public const int SIN_RESPUESTA = 0;
            public const int SOLICITUD_NO_COMPENTENCIA_UE = 1;
            public const int SOLICITUD_NO_TRAMITE = 2;
            public const int SOLICITUD_NO_LFTAIPG = 3;
            public const int INEXISTENCIA_INFORMACION = 4;
            public const int INFORMACION_DISPONIBLE_PUBLICA = 5;
            public const int INFORMACION_INMEDIATA = 6;
            public const int NEGATIVA_RESERVADA_CONFIDENCIAL = 7;
            public const int REQ_INFO_ADICIONAL = 8;
            public const int NOTIFICACION_PRORROGA = 9;
            public const int NOTIFICACION_DISPONIBILIDAD_PUBLICA = 10;
            public const int NOTIFICACION_DISPONIBILIDAD_PARCIAL_RESERVADA_CONFIDENCIAL = 11;
            public const int NOTIFICACION_ENVIO_SOLICITANTE = 12;
            public const int NOTIFICACION_ACCESO_SOLICIANTE = 13;
            public const int RECEPCION_INFO_ADICIONAL = 14;
            public const int NOTIFICACION_SOLICITANTE_INFO_SIN_COSTO = 15;
            public const int NOTIFICACION_SOLICITANTE_INFO_CON_COSTO = 16;
            public const int NOTIFICACION_PAGO = 17;
            public const int ENVIAR = 18;
            public const int RESPONDER = 19;
            public const int ASIGNAR = 20;
            public const int TURNAR = 21;
            public const int RESPUESTA_MULTIPLE = 22;
            public const int PUBLICA = 23;
            public const int PUBLICA_VISTA = 24;
            public const int INCOMPETENCIA_PARCIAL_AREA = 25;
            public const int INCOMPETENCIA_TOTAL_AREA = 26;
            public const int INEXISTENCIA_EN_AREA = 27;
            public const int INFO_RESERVADA = 28;
            public const int INFO_RESERVADA_PARCIAL = 29;
            public const int INFO_CONFIDENCIAL = 30;
            public const int INFO_CONFIDENCIAL_PARCIAL = 31;
            public const int AMPLIACION_PLAZO = 32;
            public const int PARA_COMITE = 33;
            public const int CORREGIR = 34;
            public const int RIA_AREA = 35;




            ////////////////////////////////////////////////
            ///////////R E V I S A R
            ////////////////////////////////////////////////
            public const int INCOMPETENCIA_TOTAL = 50;            
            public const int ACLARACION = 51;                            
            public const int RESPUESTA_RIA = 52;
            public const int SOLICITAR_RIA = 53;
            public const int CREAR_RESPUESTA = 54;
            public const int RECURSO_REVISION = 55;
            public const int DATOS_ACLARACION = 56;
            public const int ASIGNAR_REC_REVISION = 57;
            public const int CREAR_MENSAJE = 58;
        }

        public static class RespuestaHito
        {
            public const int NO = 0;
            public const int SI = 1;
        }

        public static class RespuestaEstado
        {
            public const int TURNAR = 1;
            public const int TURNADO = 2;
            public const int PROPUESTA = 3;
            public const int ANALIZAR = 4;
            public const int RIA = 5;
            public const int RESPONDER = 6;
            public const int ACEPTADA = 7;
            public const int CORREGIR = 8;
            public const int NEGAR = 9;
        }

        public static class RespuestaTipo
        {
            public const int INAI = 1;
            public const int INAI_NOTIFICAR = 2;
            public const int INAI_PAGO = 3;
            public const int INTERNA = 4;
            public const int INTERNA_NOVISIBLE = 5;
            public const int INTERNA_MULTIPLE = 6;

        }

        public static class RespuestaTipoContenido
        {
            public const string TIEMPO = "Tiempo";
            public const string MODO = "Modo";
            public const string LUGAR = "Lugar";

            public const string CONTENIDO = "Contenido";
            public const string FUNDAMENTO_LEGAL = "Fundamento_legal";
            public const string JUSTIFICACION = "Justificacion";
            public const string MOTIVOS = "Motivos";
            public const string RESERVA_PARTES = "Partes";

        }

       
        public static class Semaforo
        {
            public const int SOLICITUD_SEMAFORO_IROJO = 3;
            public const int SOLICITUD_SEMAFORO_IAMARILLO = 2;
            public const int SOLICITUD_SEMAFORO_IVERDE = 1;
        }

        public static class SolicitudEstado
        {
            public const int EN_PROCESO = 0;
            public const int CONCLUIDO = 1;
        }

        public static class SubClasificar
        {
            public const int SI = 1;
            public const int NO = 0;
        }

        public static class Tiempo
        {
            public const int DURACION_CERO = 0;
            public const long INICIAL = 599608224000000000;
        }

        public static class Usuario
        {
            public const int OMISION = 0;
        }

        public static class WebCarpetas
        {
            public const string ARCHIVO = "Archivo";
            public const string TEMPORAL = "Temporal";
        }

        public enum AutentificarEstado
        {
            exito,
            error,
            bloqueado
        };
    }
}

