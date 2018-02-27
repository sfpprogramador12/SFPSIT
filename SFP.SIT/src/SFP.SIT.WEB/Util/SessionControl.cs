using Microsoft.AspNetCore.Http;
using SFP.SIT.WEB.Models;
using System;

namespace SFP.SIT.WEB.Util
{
    public static class SessionControl
    {
        /// <summary> 
        /// Get value. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="session"></param> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        public static T GetDataFromSession<T>(this HttpContext session, string key)
        {
            //////return (T)session[key];
            return (T)session.Items[key];
        }
        /// <summary> 
        /// Set value. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="session"></param> 
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        public static void SetDataToSession<T>(this HttpContext session, string key, object value)
        {
            ////////session[key] = value;
        }

        public static void EliminarArchivos(ref UsuarioViewModel sesUsrMdl)
        {
            if (sesUsrMdl != null)
            {
                ////////foreach (SIT_DOC_DOCUMENTO docMdl in sesUsrMdl.lstSIT_DOC_DOCUMENTO)
                ////////{
                ////////    File.Delete(docMdl.docruta + "/" + docMdl.docnombre);
                ////////}
            }
        }

        public static void RutaArchivo(ref UsuarioViewModel sesUsrMdl, string Nombre, ref Int32 iTipoExt, ref string sRuta)
        {
            if (sesUsrMdl != null)
            {
                //////////////foreach (SIT_DOC_DOCUMENTO docMdl in sesUsrMdl.lstSIT_DOC_DOCUMENTO)
                //////////////    if (docMdl.docnombre == Nombre)
                //////////////    {
                //////////////        sRuta = docMdl.docruta + "\\" + docMdl.docnombre;
                //////////////        iTipoExt = docMdl.extclave;
                //////////////        return;
                //////////////    }
                        
            }
            return ;
        }


        
    }
}