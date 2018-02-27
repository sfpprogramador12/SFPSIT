using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Util
{
    public class Constantes
    {
        public static class Accion
        {
            public const int OMISION = 0;
            public const int RESPUESTA = 1;
            public const int TURNAR = 2;
            public const int NOTIFICACION = 3;
            public const int RESPUESTA_INFOMEX = 4;
        }

        public static class AristaOrigen
        {
            public const int INTERNO = 0;
            public const int EXTERNO = 1;
        }
        public static class AristaHito
        {
            public const int NO = 0;
            public const int SI = 1;
        }
        public static class Capa
        {
            public const int NIVEL_CERO = 0;
        }

        public static class ConfiguracionBaseDatos
        {
            public const string UT = "UT";
            public const string HORA_PROCESO = "HORA_PROCESO";
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

        public static class Paquetes
        {
            public const string AFD_CORE = "SFP.AFD.Core";
            public const string DAO_ADM = "SFP.SIT.SERVICES.Dao.Adm.";

        }
        

        /* SE DEBE DE ELINAR ESTE TIPO DE PERFILES */
        public static class Perfil
        {
            public const int SISTEMAS = 0;
            public const int INAI = 1;
            public const int UT = 2;
            public const int SE = 3;
            public const int UA = 3;
            public const int CT = 4;
            public const int RF = 5;
            public const int UTN = 6;
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

        public static class Tiempo
        {
            public const int DURACION_CERO = 0;
            public const long INICIAL = 599608224000000000;
        }

        public static class Semaforo
        {
            public const int SOLICITUD_SEMAFORO_IROJO = 3;
            public const int SOLICITUD_SEMAFORO_IAMARILLO = 2;
            public const int SOLICITUD_SEMAFORO_IVERDE = 1;
        }


        public static class SubClasificar
        {
            public const int SI = 1;
            public const int NO = 0;
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

