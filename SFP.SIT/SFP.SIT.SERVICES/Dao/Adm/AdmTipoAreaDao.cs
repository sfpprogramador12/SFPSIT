using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Adm;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Adm
{
    public class AdmTipoAreaDao : BaseDao
    {
        int iSecuencia { get; set; }

        public AdmTipoAreaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] =  new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_HABILITAR] = new Func<Object, object>(dmlHabilitar);
            dicOperacion[OPE_DESHABILITAR] = new Func<Object, object>(dmlDeshabilitar);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_COMBO] = new Func<Object, object>(dmlSelectCombo);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                          
        private Object dmlInsert(Object oDatos)
        {
            AdmTipoAreaMdl dtoDatos = (AdmTipoAreaMdl) oDatos;
            

            iSecuencia = SecuenciaDML("SEC_SIT_KTIPO_AREA");

            String sqlQuery = ""
                    + " insert into SIT_ADM_KTIPO_AREA ( KTA_CLATIPO_AREA, KTA_DESCRIPCION, KTA_SIGLAS, KTA_FECBAJA ) "
                    + " VALUES ( :P0, :P1, :P2, :P3 ) ";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.kta_descripcion, dtoDatos.kta_siglas, dtoDatos.kta_fecbaja);
        }

        private Object dmlUpdate(Object oDatos)
        {
            AdmTipoAreaMdl dtoDatos = (AdmTipoAreaMdl) oDatos;
            String sqlQuery = " update SIT_ADM_KTIPO_AREA "
                    + " set KTA_DESCRIPCION = :P0,  KTA_SIGLAS = :P1, KTA_FECBAJA = :P2  "
                    + " where KTA_CLATIPO_AREA = :P3 ";

            return EjecutaDML(sqlQuery, dtoDatos.kta_descripcion, dtoDatos.kta_siglas, dtoDatos.kta_fecbaja, dtoDatos.kta_clatipo_area);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmTipoAreaMdl dtoDatos = (AdmTipoAreaMdl) oDatos;
            String sqlQuery = " delete from SIT_ADM_KTIPO_AREA where KTA_CLATIPO_AREA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kta_clatipo_area);
        }

        public Object dmlHabilitar(Object oDatos)
        {
            AdmTipoAreaMdl dtoDatos = (AdmTipoAreaMdl) oDatos;
            String sqlQuery = " update SIT_ADM_KTIPO_AREA set KTA_FECBAJA = null where KTA_CLATIPO_AREA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kta_clatipo_area);
        }

        public Object dmlDeshabilitar(Object oDatos)
        {
            AdmTipoAreaMdl dtoDatos = (AdmTipoAreaMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KTIPO_AREA set KTA_FECBAJA = sysdate where KTA_CLATIPO_AREA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kta_clatipo_area);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmTipoAreaMdl> lstDatos = (List<AdmTipoAreaMdl>) oDatos;
            String sqlQuery = ""
                    + " insert into SIT_ADM_KTIPO_AREA ( KTA_CLATIPO_AREA, KTA_DESCRIPCION, KTA_SIGLAS ) "
                    + " VALUES ( :P0 , :P1, :P2 ) ";

            foreach (AdmTipoAreaMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.kta_clatipo_area, dtoDatos.kta_descripcion, dtoDatos.kta_siglas);
                iContador++;
            }
            return iContador;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   

        private DataTable dmlSelectGrid(object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT KTA_CLATIPO_AREA, KTA_DESCRIPCION, KTA_SIGLAS, KTA_FECBAJA   "
                + " from SIT_ADM_KTIPO_AREA "
                + " order by KTA_CLATIPO_AREA "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(object oDatos)
        {
            String sqlQuery = " Select KTA_CLATIPO_AREA as id, KTA_DESCRIPCION as text FROM SIT_ADM_KTIPO_AREA ORDER BY KTA_CLATIPO_AREA";
            return (DataTable) ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select KTA_CLATIPO_AREA, KTA_DESCRIPCION FROM SIT_ADM_KTIPO_AREA where FECHA_BAJA IS NULL  ORDER BY KTA_CLATIPO_AREA";
            dtDatos = (DataTable) ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["KTA_CLATIPO_AREA"]), row["KTA_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }

}
