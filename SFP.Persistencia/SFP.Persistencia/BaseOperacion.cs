using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.Persistencia
{
    public class BaseOperacion
    {
        public Type tEntidad { get; set; }
        public int iOper { get; set; }
        public Object oDatos { get; set; }

        public BaseOperacion(Type tEntidad, int iOper, Object oDatos) {
            this.tEntidad = tEntidad;
            this.iOper = iOper;
            this.oDatos = oDatos;
        }
    }
}
