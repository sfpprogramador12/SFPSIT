using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Red;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Red
{
    public class RedNodoEstadoDao : BaseDao
    {
        public const short OPE_SELECT_NODO_ESTADO = 211;
        public const short OPE_SELECT_DIC_NODO_ESTADO_URL = 212;
        public const short OPE_SELECT_DIC_NODO_ESTADO_PERFIL = 213;

        public const string COL_PERFIL = "PERFIL";

        int iSecuencia { get; set; }
        public RedNodoEstadoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_TABLE] = new Func<Object, object>(dmlSelectTable);
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
            dicOperacion[OPE_SELECT_NODO_ESTADO] = new Func<Object, object>(dmlSelectNodoEstado);
            dicOperacion[OPE_SELECT_DIC_NODO_ESTADO_URL] = new Func<Object, object>(dmlSelectNodoEstadoUrl);
            dicOperacion[OPE_SELECT_DIC_NODO_ESTADO_PERFIL] = new Func<Object, object>(dmlSelectNodoEstadoPerfil);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                          
        private Object dmlInsert(Object oDatos)
        {
            RedNodoEstadoMdl dtoDatos = (RedNodoEstadoMdl)oDatos;
            iSecuencia = SecuenciaDML("SEC_SIT_NODO_ESTADO");

            String sqlQuery = " insert into SIT_RED_KNODO_ESTADO ( KNE_CLANODO_EDO, KNE_DESCRIPCION, KP_CLAPERFIL, KNE_URL, KNE_TIPO  ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4 ) ";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.kne_descripcion, dtoDatos.kp_claperfil, dtoDatos.kne_url, dtoDatos.kne_tipo);
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedNodoEstadoMdl dtoDatos = (RedNodoEstadoMdl)oDatos;
            String sqlQuery = " update SIT_RED_KNODO_ESTADO set KNE_DESCRIPCION = :P0, KP_CLAPERFIL = :P1, KNE_URL = :P2, KNE_TIPO = :P3"
                    + " where KNE_CLANODO_EDO = :P4 ";

            return EjecutaDML(sqlQuery,
                    dtoDatos.kne_descripcion, dtoDatos.kp_claperfil, dtoDatos.kne_url, dtoDatos.kne_tipo,  dtoDatos.kne_clanodo_edo);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedNodoEstadoMdl dtoDatos = (RedNodoEstadoMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_KNODO_ESTADO where KNE_CLANODO_EDO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kne_clanodo_edo);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedNodoEstadoMdl> lstDatos = (List<RedNodoEstadoMdl>)oDatos;

            String sqlQuery = " insert into SIT_RED_KNODO_ESTADO ( KNE_CLANODO_EDO, KNE_DESCRIPCION, KP_CLAPERFIL, KNE_URL, KNE_TIPO  ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4 ) ";

            foreach (RedNodoEstadoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.kne_clanodo_edo, dtoDatos.kne_descripcion, dtoDatos.kp_claperfil, dtoDatos.kne_url, dtoDatos.kne_tipo);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     
        private Object dmlSelectTable(Object oDatos)
        {
            String sqlQuery = " SELECT KNE_CLANODO_EDO, KNE_DESCRIPCION, PER.KP_CLAPERFIL, KP_DESCRIPCION, KNE_URL, KNE_TIPO "
                + " from SIT_RED_KNODO_ESTADO NOE, SIT_ADM_KPERFIL PER WHERE PER.KP_CLAPERFIL = NOE.KP_CLAPERFIL ORDER BY  KNE_CLANODO_EDO";
            return CrearListaMDL(ConsultaDML(sqlQuery));
        }

        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT KNE_CLANODO_EDO, KNE_DESCRIPCION, PER.KP_CLAPERFIL, KP_DESCRIPCION, KNE_URL, KNE_TIPO "
                + " from SIT_RED_KNODO_ESTADO NOE, SIT_ADM_KPERFIL PER WHERE PER.KP_CLAPERFIL = NOE.KP_CLAPERFIL ORDER BY  KNE_CLANODO_EDO"
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select KNE_CLANODO_EDO as id, KNE_DESCRIPCION as text FROM SIT_RED_KNODO_ESTADO";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select KNE_CLANODO_EDO, KNE_DESCRIPCION FROM SIT_RED_KNODO_ESTADO";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["KNE_CLANODO_EDO"]), row["KNE_DESCRIPCION"].ToString());
            }
            return dicParametros;
        }
        
        private Dictionary<int, string> dmlSelectNodoEstado(Object oDatos)
        {
            int iPerfil = 0;            
            Dictionary<int, string> dicDatos = new Dictionary<int, string>();
            List<RedNodoEstadoMdl> lstNodoEdoMdl = (List <RedNodoEstadoMdl>) dmlSelectTable(null);

            if (lstNodoEdoMdl == null)
                return null;

            foreach (RedNodoEstadoMdl nodoEdoMdl in lstNodoEdoMdl)
            {
                iPerfil = nodoEdoMdl.kp_claperfil;

                if (dicDatos.ContainsKey(iPerfil) == true)
                {
                    string sNodoEdo = dicDatos[iPerfil];
                    sNodoEdo = sNodoEdo + "," + nodoEdoMdl.kne_clanodo_edo.ToString();
                    dicDatos[iPerfil] = sNodoEdo;
                }
                else
                {
                    dicDatos.Add(iPerfil, nodoEdoMdl.kne_clanodo_edo.ToString());
                }
            }
            return dicDatos;
        }

        private Dictionary<int, string> dmlSelectNodoEstadoUrl(Object oDatos)
        {
            Dictionary<int, string> dicDatos = new Dictionary<int, string>();

            String sqlQuery = " Select KNE_CLANODO_EDO, KNE_URL FROM SIT_RED_KNODO_ESTADO";
            DataTable dtDatos = ConsultaDML(sqlQuery);

            if (dtDatos != null)
                foreach (DataRow drDato in dtDatos.Rows)
                {
                    dicDatos.Add( Convert.ToInt32(drDato["KNE_CLANODO_EDO"]), drDato["KNE_URL"].ToString());
                }

            return dicDatos;
        }

        private Dictionary<int, int> dmlSelectNodoEstadoPerfil(Object oDatos)
        {
            Dictionary<int, int> dicDatos = new Dictionary<int, int>();

            String sqlQuery = " Select KNE_CLANODO_EDO, KP_CLAPERFIL FROM SIT_RED_KNODO_ESTADO";
            DataTable dtDatos = ConsultaDML(sqlQuery);

            if (dtDatos != null)
                foreach (DataRow drDato in dtDatos.Rows)
                {
                    dicDatos.Add(Convert.ToInt32(drDato["KNE_CLANODO_EDO"]), Convert.ToInt32(drDato["KP_CLAPERFIL"]));
                }

            return dicDatos;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            List<RedNodoEstadoMdl> lstNodoEdoMdl = new List<RedNodoEstadoMdl>();

            for (int iIdx = 0; iIdx < dtDatos.Rows.Count; iIdx++)
            {
                RedNodoEstadoMdl redNodoEdoMdl = new RedNodoEstadoMdl();
                redNodoEdoMdl.kne_clanodo_edo = Convert.ToInt32(dtDatos.Rows[iIdx]["KNE_CLANODO_EDO"]);
                redNodoEdoMdl.kne_descripcion = dtDatos.Rows[iIdx]["KNE_DESCRIPCION"].ToString();
                redNodoEdoMdl.kne_tipo = Convert.ToInt32(dtDatos.Rows[iIdx]["KNE_TIPO"]);
                redNodoEdoMdl.kne_url = dtDatos.Rows[iIdx]["KNE_URL"].ToString();
                redNodoEdoMdl.kp_claperfil = Convert.ToInt32(dtDatos.Rows[iIdx]["KP_CLAPERFIL"]);

        //String sqlQuery = " SELECT KNE_CLANODO_EDO, KNE_DESCRIPCION, PER.KP_CLAPERFIL, KP_DESCRIPCION, KNE_URL, KNE_TIPO "
        //            + " from SIT_RED_KNODO_ESTADO NOE, SIT_ADM_KPERFIL PER WHERE PER.KP_CLAPERFIL = NOE.KP_CLAPERFIL ORDER BY  KNE_CLANODO_EDO";


                lstNodoEdoMdl.Add(redNodoEdoMdl);
            }
            return lstNodoEdoMdl;
        }
    }
}
