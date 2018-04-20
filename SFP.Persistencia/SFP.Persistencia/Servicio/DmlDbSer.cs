using SFP.Persistencia.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SFP.Persistencia.Servicio
{
    public class DmlDbSer : BaseFunc
    {
        private BaseDbMdl _objDbModel { set; get; }

        public DmlDbSer(BaseDbMdl objDbMode) : base (null, null, null)
        {
            _objDbModel = objDbMode;
        }

        public object operEjecutar<T>(string sNombre, Object objParam)
        {
            Object oEntidad = null;
            Object oResultado = null;

            DbConnection connection = null;
            try
            {
                using (connection = ObtenerObjeto<DbConnection>(_objDbModel.objDbConnection))
                {
                    connection.ConnectionString = _objDbModel.connectionString;
                    connection.Open();

                    oEntidad = Activator.CreateInstance(typeof(T), connection, null, _objDbModel.objDbDataAdapter);
                    if (objParam == null)
                        oResultado = oEntidad.GetType().GetMethod(sNombre).Invoke(oEntidad, null);
                    else
                        oResultado = oEntidad.GetType().GetMethod(sNombre).Invoke(oEntidad, new[] { objParam });
                }
            }
            catch (Exception e)
            {
                if (oEntidad != null)
                    MsjError = ((BaseFunc)oEntidad).MsjError;
                else
                    MsjError = e.Message;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return oResultado;
        }
    
        public Object operEjecutar(string sClase, string sMetodo, Object objParam)
        {
            Object oEntidad = null;
            Object oResultado = null;
            Type tyObjeto = Type.GetType(sClase);

            if (tyObjeto != null)
            {
                DbConnection connection = null;
                try
                {
                    using (connection = ObtenerObjeto<DbConnection>(_objDbModel.objDbConnection))
                    {
                        connection.ConnectionString = _objDbModel.connectionString;
                        connection.Open();

                        oEntidad = Activator.CreateInstance(tyObjeto, connection, null, _objDbModel.objDbDataAdapter);
                        if (objParam == null)
                            oResultado = oEntidad.GetType().GetMethod(sMetodo).Invoke(oEntidad, null);
                        else
                            oResultado = oEntidad.GetType().GetMethod(sMetodo).Invoke(oEntidad, new[] { objParam });                        
                    }
                }
                catch (Exception e)
                {
                    MsjError = ((BaseFunc)oEntidad).MsjError;
                    Console.Write(e.Message);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return oResultado;
        }

        public Object operEjecutarTransaccion<T>(string sNombre, object objParam)
        {
            BaseFunc bfEntidad = null;
            Object oResultado = null;
            DbConnection connection = null;
            DbTransaction transaction = null;

            try
            {
                using (connection = ObtenerObjeto<DbConnection>(_objDbModel.objDbConnection))
                {
                    connection.ConnectionString = _objDbModel.connectionString;
                    connection.Open();
                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        bfEntidad = Activator.CreateInstance(typeof(T), connection, transaction, _objDbModel.objDbDataAdapter) as BaseFunc;
                        if (objParam == null)
                            oResultado = bfEntidad.GetType().GetMethod(sNombre).Invoke(bfEntidad, null);
                        else
                            oResultado = bfEntidad.GetType().GetMethod(sNombre).Invoke(bfEntidad, new[] { objParam });

                        // si el objeto es de cierto tipo.. omitir la transaccion..
                        if (bfEntidad.MsjError == "" || bfEntidad.MsjError == null)
                            transaction.Commit();
                        else
                        {
                            // HUbo un error lógico o existia un try catch que lo resolvio..
                            MsjError = bfEntidad.MsjError;
                            transaction.Rollback();
                        }
                    }
                    catch (Exception e)
                    {
                        if (bfEntidad.MsjError != null)
                            MsjError = bfEntidad.MsjError;
                        else
                            MsjError = e.Message;

                        oResultado = null;
                        transaction.Rollback();
                        Console.WriteLine("Operación cancelada : " + e.ToString());
                    }
                }
            }
            catch (Exception e)
            {                
                Console.WriteLine("Error al conextarse a la Base de Datos  : " + e.ToString());
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return oResultado;
        }

        public Object operEjecutarTransaccion(string sClase, string sMetodo, Object objParam)
        {
            object oEntidad = null;
            Object oResultado = null;
            DbConnection connection = null;
            DbTransaction transaction = null;
            BaseFunc bfEntidad = null;

            Type tyObjeto = Type.GetType(sClase);

            if (tyObjeto == null)
            {
                return null;
            }

            try
            {
                using (connection = ObtenerObjeto<DbConnection>(_objDbModel.objDbConnection))
                {
                    connection.ConnectionString = _objDbModel.connectionString;
                    connection.Open();
                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        oEntidad = Activator.CreateInstance(tyObjeto, connection, null, _objDbModel.objDbDataAdapter);
                        if (objParam == null)
                        {
                            bfEntidad = (BaseFunc)oEntidad.GetType().GetMethod(sMetodo).Invoke(oEntidad, null);
                        }
                        else
                        {
                            bfEntidad = (BaseFunc)oEntidad.GetType().GetMethod(sMetodo).Invoke(oEntidad, new[] { objParam });
                        }

                        if (bfEntidad.MsjError == "" || bfEntidad.MsjError == null)
                        {
                            transaction.Commit();
                        }                            
                        else
                        {
                            MsjError = bfEntidad.MsjError;
                            transaction.Rollback();
                        }
                    }
                    catch (Exception e)
                    {
                        if (bfEntidad.MsjError != null)
                            MsjError = bfEntidad.MsjError;
                        else
                            MsjError = e.Message;

                        oResultado = null;
                        transaction.Rollback();
                        Console.WriteLine("Operación cancelada : " + e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MsjError = "Error al conextarse a la Base de Datos";
                Console.WriteLine("Error en la conexión  : " + e.ToString());
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return oResultado;
        }
    }
}