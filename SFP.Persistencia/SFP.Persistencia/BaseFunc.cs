using System;
using System.Data.Common;

namespace SFP.Persistencia
{
    public class BaseFunc
    {
        protected DbConnection _cn;
        protected DbTransaction _transaction;
        protected String _sDataAdapter;

        public String MsjError { set; get; }

        public BaseFunc(DbConnection cn, DbTransaction transaction, String sDataAdapter)
        {
            _cn = cn;
            _transaction = transaction;
            _sDataAdapter = sDataAdapter;
        }

        protected T ObtenerObjeto<T>(string sNombreClase)
        {
            Type type = Type.GetType(sNombreClase);
            Object objCmd = Activator.CreateInstance(type);
            return (T)objCmd;
        }
    }
}