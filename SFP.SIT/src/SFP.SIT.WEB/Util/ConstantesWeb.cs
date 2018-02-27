using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Util
{

    public static class ConstantesWeb
    {
        public static class Carpetas
        {
            public const string ARCHIVO = "Archivos";
            public const string TEMPORAL = "Temporal";
            public const string PLANTILLAS = "Plantillas";
        }

        public static class FLUJO
        {
            public const int RESPONDER = 1;
            public const int SEGUIMIENTO = 2;   // ESTE SOBRA???
            public const int TURNAR_ADICIONAL = 3;
            public const int AMPLIACION = 4;
        }

        public static class LogMensajes
        {
            public const string USUARIO_ERROR = "Error en el sistema";
            public const string USUARIO_SESSION = "Usuario en session";
            public const string USUARIO_SESSION_EXPIRO = "La session expiro";
            public const string USUARIO_NO_EXISTE = "No existe usuario o datos erroneos";
            public const string USUARIO_AUTENTIFICADO = "Usuario autentificado";
            public const string USUARIO_SALIR = "El usuario salio del sistema";
        }

        public static class Usuario
        {
            public const string CLAVE = "ClaveUsuario";
            public const string NOMBRE = "UsuNombre";
            public const string AREAS = "UsuAreas";
            public const string PERFILES = "UsuPerfiles";
            public const string MODULOS = "UsuModulos";
            public const string CORREO = "UsuCorreo";
            public const string CBOPERFILAREA = "UsuPerfilArea";
        }        
    }

    public static class Operacion
    {
        public static string TipoOperacion(int iValor)
        {
            string sMensaje = null;

            if (iValor == 1)
                sMensaje = "Insertar";
            else if (iValor == 2)
                sMensaje = "Editar";
            else if (iValor == 3)
                sMensaje = "Borrar";

            return sMensaje;
        }
    }
}