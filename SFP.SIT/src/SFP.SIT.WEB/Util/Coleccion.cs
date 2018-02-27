using SFP.SIT.SERV.Model.ADM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Util
{
    static public class Coleccion
    {
        /// <summary>
        ///  BORRAR
        /// </summary>
        /// <param name="sSigla"></param>
        /// <returns></returns>
        ////////////////static public string DataTableToString(DataTable dtDatos)
        ////////////////{
        ////////////////    if (dtDatos.Rows.Count > 0)
        ////////////////    {
        ////////////////        StringBuilder sbDatos = new StringBuilder();

        ////////////////        foreach (DataRow drDato in dtDatos.Rows)
        ////////////////        {
        ////////////////            sbDatos.Append(",");
        ////////////////            sbDatos.Append(drDato[0].ToString());
        ////////////////        }

        ////////////////        return sbDatos.ToString(1, sbDatos.Length - 1);
        ////////////////    }
        ////////////////    else
        ////////////////    {
        ////////////////        return "";
        ////////////////    }            
        ////////////////}

        static public int PerfilBuscar(string sSigla)
        {
            ///////ARREGLAR 
            ////////////////Dictionary<int, SIT_ADM_PERFIL> dicPerfil = CacheWeb.GetPerfil();
            Dictionary<int, SIT_ADM_PERFIL> dicPerfil = new Dictionary<int, SIT_ADM_PERFIL>();

            int iPerfil = -1;

            foreach (KeyValuePair<int, SIT_ADM_PERFIL> entry in dicPerfil)
            {
                SIT_ADM_PERFIL perfilMdl = entry.Value;
                if (perfilMdl.persigla == sSigla)
                    return perfilMdl.perclave;

                // do something with entry.Value or entry.Key
            }
            return iPerfil;
        }

        static public DataTable DicPerfilDataTable()
        {
            DataTable dtDatos = new DataTable();
            dtDatos.Columns.Add("value", typeof(int));
            dtDatos.Columns.Add("text", typeof(string));

            /////////////////// ARREGLAR
            ///////////////////Dictionary<int, SIT_ADM_PERFIL> dicPerfil = CacheWeb.GetPerfil();
            Dictionary<int, SIT_ADM_PERFIL> dicPerfil = new Dictionary<int, SIT_ADM_PERFIL>();

            foreach (KeyValuePair<int, SIT_ADM_PERFIL> entry in dicPerfil)
            {
                SIT_ADM_PERFIL perfilMdl = entry.Value;
                dtDatos.Rows.Add(perfilMdl.perclave, perfilMdl.perdescripcion);
            }

            return dtDatos;
        }

    }
}
