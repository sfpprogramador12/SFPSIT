﻿using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Adm;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Adm
{
    public class AdmAreaHistDao : BaseDao
    {
        public const int OPE_SELECT_AREAS_POR_TIPO = 211;
        public const int OPE_SELECT_AREAS_PARA_TURNAR = 212;
        public const int OPE_SELECT_AREAS_PERFIL = 213;

        public const string COL_KTA_CLATIPO_AREA = "KTA_CLATIPO_AREA";
        public const string COL_KP_CLAPERFIL = "KP_CLAPERFIL";
        public const string COL_KA_CLAAREA = "KA_CLAAREA";
        public const string PARAM_FECHA = "FECHA";


        int iSecuencia { get; set; }

        public AdmAreaHistDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_HABILITAR] = new Func<Object, object>(dmlHabilitar);
            dicOperacion[OPE_DESHABILITAR] = new Func<Object, object>(dmlDeshabilitar);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            //dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
            dicOperacion[OPE_SELECT_AREAS_POR_TIPO] = new Func<Object, object>(dmlSelectAreasPorTipo);
            dicOperacion[OPE_SELECT_AREAS_PARA_TURNAR] = new Func<Object, object>(dmlSelectAreasTurnar);
            dicOperacion[OPE_SELECT_AREAS_PERFIL] = new Func<Object, object>(dmlSelectAreaPerfil);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                          
        private Object dmlInsert(Object oDatos)
        {
            AdmAreaMdl dtoDatos = (AdmAreaMdl)oDatos;
            String sqlQuery = " insert into SIT_ADM_KAREA_HIST ( ka_claarea, ka_descripcion, ka_clave, ka_sigla, ka_reporta, ka_orden, ka_nivel, kta_clatipo_area, ka_fecini, ka_fecfin, ka_ubicacion, kp_claperfil ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11 )";

            EjecutaDML(sqlQuery, dtoDatos.ka_claarea, dtoDatos.ka_descripcion, dtoDatos.ka_clave, dtoDatos.ka_sigla, dtoDatos.ka_reporta, dtoDatos.ka_orden, dtoDatos.ka_nivel, 
                dtoDatos.kta_clatipo_area, dtoDatos.ka_fecini, dtoDatos.ka_fecfin, dtoDatos.ka_ubicacion, dtoDatos.kp_claperfil );

            return iSecuencia;
        }

        private Object dmlUpdate(Object oDatos)
        {
            AdmAreaMdl dtoDatos = (AdmAreaMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KAREA_HIST set ka_descripcion = :P0, ka_clave = :P1, ka_sigla = :P2,  "
                    + "  ka_reporta = :P3, ka_orden = :P4, ka_nivel = :P5, kta_clatipo_area = :P6,  "
                    + " ka_fecini = :P7, ka_fecfin = :P8, ka_ubicacion = :P9, kp_claperfil = :P10 "
                    + " where KA_CLAAREA = :P11 ";

            return EjecutaDML(sqlQuery,
                dtoDatos.ka_descripcion, dtoDatos.ka_clave, dtoDatos.ka_sigla, 
                dtoDatos.ka_reporta, dtoDatos.ka_orden, dtoDatos.ka_nivel, dtoDatos.kta_clatipo_area,
                dtoDatos.ka_fecini, dtoDatos.ka_fecfin, dtoDatos.ka_ubicacion, dtoDatos.kp_claperfil,
                dtoDatos.ka_claarea);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmAreaMdl dtoDatos = (AdmAreaMdl)oDatos;
            String sqlQuery = " delete from SIT_ADM_KAREA_HIST where KA_CLAAREA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.ka_claarea);
        }

        private Object dmlHabilitar(Object oDatos)
        {
            AdmAreaMdl dtoDatos = (AdmAreaMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KAREA_HIST set KA_FECBAJA = null where KA_CLAAREA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.ka_claarea);
        }

        private Object dmlDeshabilitar(Object oDatos)
        {
            AdmAreaMdl dtoDatos = (AdmAreaMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KAREA_HIST set KA_FECBAJA = sysdate where KA_CLAAREA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.ka_claarea);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmAreaMdl> lstDatos = (List<AdmAreaMdl>)oDatos;

            String sqlQuery = " insert into SIT_ADM_KAREA_HIST ( ka_claarea, ka_descripcion, ka_clave, ka_sigla, ka_reporta, ka_orden, ka_nivel, kta_clatipo_area, ka_fecini, ka_fecfin, ka_ubicacion, kp_claperfil ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11) ";

            foreach (AdmAreaMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.ka_claarea, dtoDatos.ka_descripcion, dtoDatos.ka_clave, dtoDatos.ka_sigla, dtoDatos.ka_reporta, dtoDatos.ka_orden, dtoDatos.ka_nivel,
                    dtoDatos.kta_clatipo_area, dtoDatos.ka_fecini, dtoDatos.ka_fecfin, dtoDatos.ka_ubicacion, dtoDatos.kp_claperfil);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                  

        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl) oDatos;

            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + "SELECT KA_CLAAREA,  KA_DESCRIPCION, KA_CLAVE, KA_SIGLA, KA_REPORTA, KA_ORDEN, KA_NIVEL,  AREA.KTA_CLATIPO_AREA, KTA_DESCRIPCION, KP_PERFIL  "
                + " from SIT_ADM_KAREA_HIST AREA, SIT_ADM_KTIPO_AREA TAREA "
                + " WHERE AREA.KTA_CLATIPO_AREA = TAREA.KTA_CLATIPO_AREA "
                + " order by KA_CLAAREA"
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";

            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private object dmlSelectCombo(Object oDatos)
        {
            Dictionary<string, object> dicParam = oDatos as Dictionary<string, object>;

            String sqlQuery = " Select KA_CLAAREA as id, KA_DESCRIPCION as text FROM SIT_ADM_KAREA_HIST where :P0  between KA_FECINI AND KA_FECFIN ORDER BY KA_DESCRIPCION";
            return (DataTable)ConsultaDML(sqlQuery, dicParam[PARAM_FECHA]);
        }

        //private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        //{
        //    Dictionary<int, string> dicParametros = new Dictionary<int, string>();
        //    DataTable dtDatos;

        //    string sqlQuery = " Select KA_CLAAREA, KA_DESCRIPCION FROM SIT_ADM_KAREA_HIST where FECBAJA IS NULL ORDER BY KA_DESCRIPCION";
        //    dtDatos = (DataTable)ConsultaDML(sqlQuery);

        //    foreach (DataRow row in dtDatos.Rows)
        //    {
        //        dicParametros.Add(Convert.ToInt32(row["KA_CLAAREA"]), row["KA_DESCRIPCION"].ToString());
        //    }

        //    return dicParametros;
        //}

        private DataTable dmlSelectAreasPorTipo(Object oDatos)
        {
            Dictionary<string, Object> dicParametros = (Dictionary<string, Object>)oDatos;

            String sqlQuery = "Select KA_CLAAREA, KA_DESCRIPCION from SIT_ADM_KAREA_HIST where KA_FECBAJA IS NULL and KP_CLAPERFIL = :P0 ORDER BY KA_CLAAREA";

            return (DataTable)ConsultaDML(sqlQuery, Convert.ToInt32(dicParametros[COL_KP_CLAPERFIL]));
        }

        private DataTable dmlSelectAreasTurnar(Object oDatos)
        {
            Dictionary<string, Object> dicParametros = (Dictionary<string, Object>)oDatos;

            ////String sqlQuery = " SELECT ka_claarea, ka_sigla, ka_descripcion, kta_siglas"
            ////        + " FROM SIT_ADM_KAREA_HIST area, SIT_adm_ktipo_area tiparea "
            ////        + " WHERE area.kta_clatipo_area = tiparea.kta_clatipo_area AND ka_fecBaja is null AND ka_claarea > 0 ";


            String sqlQuery = " SELECT area.KA_CLAAREA, KA_DESCRIPCION,  ka_sigla, kta_siglas ";
                //+ " FROM SIT_ADM_KAREA_HIST area, SIT_adm_ktipo_area tiparea  "                
                //+ " WHERE area.kta_clatipo_area = tiparea.kta_clatipo_area AND ka_fecBaja is null  "
                //+ " AND area.KA_CLAAREA <> :P0 "
                //+ " AND area.ka_claarea > 0 "
                //+ " AND area.kp_claperfil = " + Util.Constantes.Perfil.UA 
                //+ " START WITH KA_CLAAREA = :P1 "
                //+ " CONNECT BY PRIOR KA_CLAAREA = KA_REPORTA "
                //+ " ORDER BY kta_siglas, KA_REPORTA,  area.ka_descripcion";

            return (DataTable)ConsultaDML(sqlQuery, dicParametros[COL_KA_CLAAREA], dicParametros[COL_KA_CLAAREA]);
        }

       
        private Dictionary<int, int> dmlSelectAreaPerfil( Object oDatos)
        {
            Dictionary<string, object> dicParam  = (Dictionary<string, object>)oDatos;
            Dictionary<int, int> dicDatos = new Dictionary<int, int>();
            DataTable dtDatos;


            String sqlQuery = " SELECT ka_claarea, kp_claperfil "
                    + " FROM SIT_ADM_KAREA_HIST area  WHERE :P0 between KA_FECINI AND KA_FECFIN AND ka_claarea > 0 GROUP BY ka_claarea, kp_claperfil ";

            dtDatos = (DataTable)ConsultaDML(sqlQuery, dicParam[PARAM_FECHA]);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicDatos.Add(Convert.ToInt32(row["KA_CLAAREA"]), Convert.ToInt32(row["kp_claperfil"]));
            }
            return dicDatos;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
