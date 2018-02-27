using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.App
{
    public class CfgCorreoMdl
    {
        public String servidor { set; get; }
        public String usuario { set; get; }
        public String contraseña { set; get; }
        public int puerto { set; get; }
        public CfgCorreoMdl() {}

        public CfgCorreoMdl(String Servidor, String Puerto, String Usuario, String Contraseña)
        {
            this.servidor = Servidor;
            this.usuario = Usuario;
            this.contraseña = Contraseña;
            this.puerto = Convert.ToInt32(Puerto);
        }
    }
}
