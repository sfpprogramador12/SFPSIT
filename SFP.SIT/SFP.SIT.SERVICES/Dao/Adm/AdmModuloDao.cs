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
    public class AdmModuloDao : BaseDao
    {
        int iSecuencia { get; set; }

        public AdmModuloDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            AdmModuloMdl dtoDatos = (AdmModuloMdl) oDatos;            

            String sqlQuery = ""
                    + " insert into SIT_ADM_KMODULO ( KM_CLAMODULO, KM_CLAMODULO_PADRE, KM_CONSECUTIVO, KM_DESCRIPCION, KM_CONTROL, KM_METODO, KM_FECBAJA ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6 ) ";

            iSecuencia = dtoDatos.km_clamodulo;

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.km_clamodulo_padre,
                    dtoDatos.km_consecutivo, dtoDatos.km_descripcion, dtoDatos.km_control, dtoDatos.km_metodo, dtoDatos.km_fecbaja );
        }

        private Object dmlUpdate(Object oDatos)
        {
            AdmModuloMdl dtoDatos = (AdmModuloMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KMODULO "
                    + " set KM_CLAMODULO_PADRE = :P0, KM_CONSECUTIVO= :P1, KM_DESCRIPCION= :P2, KM_CONTROL= :P3, KM_METODO = :P4,  KM_FECBAJA = :P5 "
                    + " where KM_CLAMODULO = :P6 ";

            return EjecutaDML(sqlQuery, dtoDatos.km_clamodulo_padre, dtoDatos.km_consecutivo, dtoDatos.km_descripcion,
                dtoDatos.km_control, dtoDatos.km_metodo, dtoDatos.km_fecbaja, dtoDatos.km_clamodulo);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmModuloMdl dtoDatos = (AdmModuloMdl)oDatos;
            String sqlQuery = " delete from SIT_ADM_KMODULO where KM_CLAMODULO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.km_clamodulo);
        }

        private Object dmlHabilitar(Object oDatos)
        {
            AdmModuloMdl dtoDatos = (AdmModuloMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KMODULO set KM_FECBAJA = null where KM_CLAMODULO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.km_clamodulo);
        }

        private Object dmlDeshabilitar(Object oDatos)
        {
            AdmModuloMdl dtoDatos = (AdmModuloMdl)oDatos;
            String sqlQuery = " update SIT_ADM_KMODULO set KM_FECBAJA = sysdate where KM_CLAMODULO = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.km_clamodulo);
        }


        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmModuloMdl> lstDatos = (List<AdmModuloMdl>)oDatos;

            String sqlQuery = ""
                + " insert into SIT_ADM_KMODULO ( KM_CLAMODULO, KM_CLAMODULO_PADRE, KM_CONSECUTIVO, KM_DESCRIPCION, KM_CONTROL, KM_METODO, KM_FECBAJA ) "
                + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6 ) ";

            foreach (AdmModuloMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.km_clamodulo, dtoDatos.km_clamodulo_padre,
                    dtoDatos.km_consecutivo, dtoDatos.km_descripcion, dtoDatos.km_control, dtoDatos.km_metodo, dtoDatos.km_fecbaja);
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
                + " SELECT KM_CLAMODULO, KM_CLAMODULO_PADRE, KM_CONSECUTIVO, KM_DESCRIPCION, KM_CONTROL, KM_METODO, KM_FECBAJA   "
                + " from SIT_ADM_KMODULO "
                + " order by KM_CLAMODULO "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select KM_CLAMODULO as id, KM_DESCRIPCION as text FROM SIT_ADM_KMODULO where KM_FECBAJA IS NULL ORDER BY KM_CLAMODULO";
            return (DataTable) ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select KM_CLAMODULO, KM_DESCRIPCION FROM SIT_ADM_KMODULO where FECBAJA IS NULL ORDER BY KM_CLAMODULO";
            dtDatos = (DataTable) ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["KM_CLAMODULO"]), row["KM_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
