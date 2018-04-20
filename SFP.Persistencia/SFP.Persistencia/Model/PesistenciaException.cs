using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.Persistencia.Model
{
    public class PesistenciaException : Exception
    {
        public  PesistenciaException() 
        {
        }

        public PesistenciaException(string message)
            : base(message)
        {
        }

        public PesistenciaException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}