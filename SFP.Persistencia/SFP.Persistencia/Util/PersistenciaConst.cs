using SFP.Persistencia.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.Persistencia.Util
{
    public static class PersistenciaConst
    {

        public static string NombreMetodo(int iOper)
        {
            String sMetodo;

            if (iOper == BaseDao.OPE_INSERTAR)
                sMetodo = "dmlInsert";

            else if (iOper == BaseDao.OPE_EDITAR)
                sMetodo = "dmlUpdate";
            else
                sMetodo = "dmlDelete";

            return sMetodo;
        }

        public static class OPERACION
        {
            public const int INSERTAR = 1;
            public const int EDITAR = 2;
            public const int BORRAR = 3;

        }

    }
}
