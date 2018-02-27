using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.Persistencia.Model
{
    
    public class ComboMdl
    {
        public string ID { set; get; }
        public string DESCRIP { set; get; }

        public ComboMdl() { }
        public ComboMdl(string ID, string DESCRIP)
        {
            this.ID = ID;
            this.DESCRIP = DESCRIP;
        }
    }
}
