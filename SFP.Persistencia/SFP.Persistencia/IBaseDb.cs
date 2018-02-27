using System;
using System.Data;

namespace SFP.Persistencia
{
    public interface IBaseDb
    {
        Object EjecutaDML(string psQuery, params Object[] aoParametros);
        DataTable ConsultaDML(string psQuery, params Object[] aoParametros);
        Boolean EjecutarBulkCopy(DataTable dtDatos, string sTabla);
    }
}
