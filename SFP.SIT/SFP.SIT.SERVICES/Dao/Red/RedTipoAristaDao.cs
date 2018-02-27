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
    public class RedTipoAristaDao : BaseDao
    {
        int iSecuencia { get; set; }

        public const int OPE_SELECT_DT_DOCUMENTO = 211;
        public const int OPE_SELECT_DT_SUBCLASIFICAR = 212;
        public const int OPE_SELECT_DT_ARISTA_TIPO = 213;
        public const int OPE_SELECT_DT_ARISTA_TIPO_ID = 214;

        public const string COL_KAR_SUBCLASIFICAR = "KAR_SUBCLASIFICAR";
        public const string COL_KAR_TIPO = "KAR_TIPO";
        public const string COL_KAR_CLATIPOARI = "KAR_CLATIPOARI";
        public const string PARAM_OMITIR = "PARAM_OMITIR";

        public RedTipoAristaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
            dicOperacion[OPE_SELECT_DT_DOCUMENTO] = new Func<Object, object>(dmlSelectDicFormato);
            dicOperacion[OPE_SELECT_DT_SUBCLASIFICAR] = new Func<Object, object>(dmlSelectComboSubClasificar);
            dicOperacion[OPE_SELECT_DT_ARISTA_TIPO] = new Func<Object, object>(dmlSelectComboTipo);
            dicOperacion[OPE_SELECT_DT_ARISTA_TIPO_ID] = new Func<Object, object>(dmlSelectComboTipoID);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            iSecuencia = SecuenciaDML("SEC_SIT_KTIPO_ARISTA");
            RedTipoAristaMdl dtoDatos = (RedTipoAristaMdl)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_RED_KTIPO_ARISTA ( kar_clatipoari, kar_descripcion, kar_aplicatipoinfo, kar_formato, kar_respuesta, "
                    + " kar_tipo, kar_sigla, kar_subclasificar, kar_mostrarsub, kar_forma, kar_nivel ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10) ";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.kar_descripcion, dtoDatos.kar_aplicatipoinfo, dtoDatos.kar_formato, dtoDatos.kar_respuesta,
                dtoDatos.kar_tipo, dtoDatos.kar_sigla, dtoDatos.kar_subclasificar, dtoDatos.kar_mostrarsub, dtoDatos.kar_forma, dtoDatos.kar_nivel);
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedTipoAristaMdl dtoDatos = (RedTipoAristaMdl)oDatos;

            String sqlQuery = " update SIT_RED_KTIPO_ARISTA "
                    + " set kar_descripcion = :P0,  kar_aplicatipoinfo = :P1, kar_formato = :P2,  kar_respuesta = :P3, kar_tipo = :P4, kar_sigla = :P5, "
                    + " kar_subclasificar = :P6, kar_mostrarsub= :P7,  kar_forma = :P8, kar_nivel = :P9 "
                    + " where kar_clatipoari = :P10 ";

            return EjecutaDML(sqlQuery,
                    dtoDatos.kar_descripcion, dtoDatos.kar_aplicatipoinfo , dtoDatos.kar_formato, dtoDatos.kar_respuesta, dtoDatos.kar_tipo,
                    dtoDatos.kar_sigla, dtoDatos.kar_subclasificar, dtoDatos.kar_mostrarsub, dtoDatos.kar_forma,  dtoDatos.kar_nivel, dtoDatos.kar_clatipoari);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedTipoAristaMdl dtoDatos = (RedTipoAristaMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_KTIPO_ARISTA where kar_clatipoari = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kar_clatipoari);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedTipoAristaMdl> lstDatos = (List<RedTipoAristaMdl>)oDatos;

            String sqlQuery = ""
                + " insert into SIT_RED_KTIPO_ARISTA ( kar_clatipoari, kar_descripcion, kar_aplicatipoinfo, kar_formato, kar_respuesta, "
                + " kar_tipo, kar_sigla, kar_subclasificar, kar_mostrarsub, kar_forma, kar_nivel ) "
                + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10 ) ";

            foreach (RedTipoAristaMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.kar_clatipoari, dtoDatos.kar_descripcion, dtoDatos.kar_aplicatipoinfo, dtoDatos.kar_formato, dtoDatos.kar_respuesta,
                    dtoDatos.kar_tipo, dtoDatos.kar_sigla, dtoDatos.kar_subclasificar, dtoDatos.kar_mostrarsub, dtoDatos.kar_forma, dtoDatos.kar_nivel);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   

        private DataTable dmlSelectGrid(Object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT kar_clatipoari, kar_descripcion, kar_aplicatipoinfo,  kar_formato, kar_respuesta, kar_tipo, kar_sigla, kar_subclasificar, kar_mostrarsub, kar_forma, kar_nivel "
                + "  FROM SIT_RED_KTIPO_ARISTA ORDER BY kar_clatipoari "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            //String sqlQuery = " Select kar_clatipoari as id , kar_descripcion as text FROM SIT_RED_KTIPO_ARISTA WHERE  kar_clatipoari > 0 and kar_clatipoari < 18 ";
            String sqlQuery = " Select kar_clatipoari as id , kar_descripcion as text FROM SIT_RED_KTIPO_ARISTA ";
            return ConsultaDML(sqlQuery);
        }

        private DataTable dmlSelectComboSubClasificar(Object oDatos)
        {
            Dictionary<string, object > dicParametros = (Dictionary<string, object>)oDatos;
            String sqlQuery = " Select kar_clatipoari as id , kar_descripcion as text FROM SIT_RED_KTIPO_ARISTA WHERE  KAR_SUBCLASIFICAR = :P0 ";
            return ConsultaDML(sqlQuery, dicParametros[COL_KAR_SUBCLASIFICAR]);
        }

        private DataTable dmlSelectComboTipo(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;

            String sqlQuery;

            if (dicParametros[PARAM_OMITIR].ToString().Length > 0 )
                sqlQuery = " Select kar_clatipoari as id , kar_descripcion as text FROM SIT_RED_KTIPO_ARISTA WHERE  KAR_TIPO = :P0 AND kar_clatipoari not in ( " + dicParametros[PARAM_OMITIR].ToString()  + ")";
            else
                sqlQuery = " Select kar_clatipoari as id , kar_descripcion as text FROM SIT_RED_KTIPO_ARISTA WHERE  KAR_TIPO = :P0 ";

            return ConsultaDML(sqlQuery, dicParametros[COL_KAR_TIPO]);
        }

        private DataTable dmlSelectComboTipoID(Object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            String sqlQuery = " Select kar_clatipoari as id , kar_descripcion as text FROM SIT_RED_KTIPO_ARISTA WHERE  kar_clatipoari = :P0 ";
            return ConsultaDML(sqlQuery, dicParametros[COL_KAR_CLATIPOARI]);
        }
        
        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select kar_clatipoari, kar_descripcion FROM SIT_RED_KTIPO_ARISTA";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["kar_clatipoari"]), row["kar_descripcion"].ToString());
            }

            return dicParametros;
        }

        private DataTable dmlSelectDicFormato(Object oDatos)
        {
            string sqlQuery = " Select kar_clatipoari, kar_formato FROM SIT_RED_KTIPO_ARISTA";            
            return ConsultaDML(sqlQuery);
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}

