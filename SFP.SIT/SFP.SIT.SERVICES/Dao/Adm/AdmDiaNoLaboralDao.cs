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
    public class AdmDiaNoLaboralDao : BaseDao
    {
        public AdmDiaNoLaboralDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {

            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                          
        private Object dmlInsert(Object oDatos)
        {
            AdmDiaNoLaboralMdl dtoDatos = (AdmDiaNoLaboralMdl)oDatos;
            String sqlQuery = " insert into SIT_ADM_KDIA_NO_LABORAL ( KDNL_DIA, KDNL_TIPODIA ) VALUES ( :P0, :P1 )";
            return EjecutaDML(sqlQuery, dtoDatos.kdnl_dia, dtoDatos.kdnl_tipodia);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmDiaNoLaboralMdl dtoDatos = (AdmDiaNoLaboralMdl)oDatos;
            String sqlQuery = " delete from SIT_ADM_KDIA_NO_LABORAL where KDNL_DIA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.kdnl_dia);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmDiaNoLaboralMdl> lstDatos = (List<AdmDiaNoLaboralMdl>)oDatos;

            String sqlQuery = " insert into SIT_ADM_KDIA_NO_LABORAL ( KDNL_DIA, KDNL_TIPODIA ) VALUES ( :P0, :P1 )";

            foreach (AdmDiaNoLaboralMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.kdnl_dia, dtoDatos.kdnl_tipodia);
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
                + " SELECT KDNL_DIA, KDNL_TIPODIA from SIT_ADM_KDIA_NO_LABORAL "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private Dictionary<Int64, char> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<Int64, char> dicParametros = new Dictionary<Int64, char>();
            DataTable dtDatos;
            DateTime dmDia;

            string sqlQuery = " SELECT KDNL_DIA, KDNL_TIPODIA from SIT_ADM_KDIA_NO_LABORAL ";
            dtDatos = (DataTable) ConsultaDML(sqlQuery);

            foreach (DataRow drDatos in dtDatos.Rows)
            {
                dmDia = (DateTime) drDatos["KDNL_DIA"];
                dicParametros.Add(dmDia.Ticks , Convert.ToChar( drDatos["KDNL_TIPODIA"]));
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
