using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.Persistencia.Model
{
    public class CfgSharePointMdl
    {
        public String servidor { set; get; }
        public String usuario { set; get; }
        public String contraseña { set; get; }
        public String dominio { set; get; }
        public String folder { set; get; }

        public CfgSharePointMdl(String Servidor, String Usuario, String Contraseña, String Dominio, String Folder)
        {
            this.servidor = Servidor;
            this.usuario = Usuario;
            this.contraseña = Contraseña;
            this.dominio = Dominio;
            this.folder = Folder;
        }
    }
}
