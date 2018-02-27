using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP.SIT.WEB.Models
{
    public class LogViewModel
    {
        public string usuario { set; get; }
        public string accion { set; get; }
        public string objeto { set; get; }
        public string opdesc { set; get; }
        public string data { set; get; }
        public string direccionIP { set; get; }

        public LogViewModel() { }
        public LogViewModel(string direccionIP, string usuario, string objeto, string accion, string opdesc, string data)
        {
            this.direccionIP = direccionIP;
            this.usuario = usuario;
            this.accion = accion;
            this.objeto = objeto;
            this.opdesc = opdesc;
            this.data = data;
        }

        public override string ToString()
        {
            return "{\"ip\":\"" + direccionIP + "\",\"usuario\":\"" + usuario + "\",\"objeto\":\"" + objeto + "\",\"accion\":\"" + accion  +
                "\",\"opdesc\":\"" + opdesc + "\",\"data\":\"" + data + "\"}";
        }
    }
}