using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.SIT.SERVICES.Model.Sol;
using SFP.Persistencia;
using System.Data;
using SFP.SIT.SERVICES.Model;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Sol
{
    public class SolTipoModoEntregaDao : BaseDao
    {
        Int64 lSecuencia { get; set; }
        public const string PARAM_COL_MEN_MOSTRAR = "MEN_MOSTRAR";

        public SolTipoModoEntregaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
        }        

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private object dmlInsert(object oDatos)
        {
            SolTipoModoEntregaMdl dtoDatos = (SolTipoModoEntregaMdl)oDatos;
            String sqlQuery = ""
                + " insert into SIT_SOL_KTIPO_MODO_ENTREGA ( US_MODENT, MEN_DESCRIPCION, MEN_MOSTRAR ) "
                + " VALUES ( :P0 , :P1, :P2 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.us_modent, dtoDatos.men_descripcion, dtoDatos.men_mostrar);
        }

        private object dmlUpdate(object oDatos)
        {
            SolTipoModoEntregaMdl dtoDatos = (SolTipoModoEntregaMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KTIPO_MODO_ENTREGA set MEN_DESCRIPCION = :P0, MEN_MOSTRAR = :P1 where US_MODENT = :P2 ";
            return EjecutaDML(sqlQuery, dtoDatos.men_descripcion, dtoDatos.men_mostrar, dtoDatos.us_modent);
        }

        private object dmlDelete(object oDatos)
        {
            SolTipoModoEntregaMdl dtoDatos = (SolTipoModoEntregaMdl)oDatos;
            String sqlQuery = " delete from SIT_SOL_KTIPO_MODO_ENTREGA where US_MODENT = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_modent);
        }

        private object dmlImportar(object oDatos)
        {
            Int16 iContador = 0;
            List<SolTipoModoEntregaMdl> lstDatos = (List<SolTipoModoEntregaMdl>)oDatos;

            String sqlQuery = ""
                + " insert into SIT_SOL_KTIPO_MODO_ENTREGA ( US_MODENT, MEN_DESCRIPCION, MEN_MOSTRAR ) "
                + " VALUES ( :P0, :P1, :P2 ) ";

            foreach (SolTipoModoEntregaMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.us_modent, dtoDatos.men_descripcion, dtoDatos.men_mostrar);
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
                + " SELECT US_MODENT, MEN_DESCRIPCION, MEN_MOSTRAR  from SIT_SOL_KTIPO_MODO_ENTREGA  order by US_MODENT "
                +" ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(object oDatos)
        {
            Dictionary<String, Object> dicParam = oDatos as Dictionary<String, Object>;

            String sqlQuery = " Select US_MODENT as id, MEN_DESCRIPCION as text FROM SIT_SOL_KTIPO_MODO_ENTREGA WHERE MEN_MOSTRAR = :P0 ORDER BY US_MODENT";
            return ConsultaDML(sqlQuery, dicParam[PARAM_COL_MEN_MOSTRAR] );
        }

        private Object dmlSelectHashMap(object oDatos)
        {
            Dictionary<String, Object> dicParam = oDatos as Dictionary<String, Object>;
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;
            String sqlQuery;

            if (dicParam == null)
            {
                sqlQuery = " Select US_MODENT, MEN_DESCRIPCION FROM SIT_SOL_KTIPO_MODO_ENTREGA  ORDER BY US_MODENT";
                dtDatos = ConsultaDML(sqlQuery);
            }
            else
            {
                if (dicParam.ContainsKey(PARAM_COL_MEN_MOSTRAR))
                {
                    sqlQuery = " Select US_MODENT, MEN_DESCRIPCION FROM SIT_SOL_KTIPO_MODO_ENTREGA WHERE MEN_MOSTRAR = :P0 ORDER BY US_MODENT";
                    dtDatos = ConsultaDML(sqlQuery, dicParam[PARAM_COL_MEN_MOSTRAR]);
                }
                else
                {
                    sqlQuery = " Select US_MODENT, MEN_DESCRIPCION FROM SIT_SOL_KTIPO_MODO_ENTREGA  ORDER BY US_MODENT";
                    dtDatos = ConsultaDML(sqlQuery);
                }
            }

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["US_MODENT"]), row["MEN_DESCRIPCION"].ToString());
            }
            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
