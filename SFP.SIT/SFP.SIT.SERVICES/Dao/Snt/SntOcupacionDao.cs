using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.SIT.SERVICES.Model.Snt;
using SFP.Persistencia;
using System.Data;
using SFP.SIT.SERVICES.Model;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Snt
{
    public class SntOcupacionDao : BaseDao
    {
        int iSecuencia { get; set; }

        public SntOcupacionDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            SntOcupacionMdl dtoDatos = (SntOcupacionMdl)oDatos;
            iSecuencia = SecuenciaDML("SEC_SIT_KOCUPACION");

            String sqlQuery = " insert into SIT_SNT_KOCUPACION ( US_OCUPACION, OCU_DESCRIPCION, OCU_FECBAJA ) "
                + " VALUES ( :P0, :P1, :P2 ) ";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.ocu_descripcion, dtoDatos.ocu_fecbaja );
        }

        private Object dmlUpdate(Object oDatos)
        {
            SntOcupacionMdl dtoDatos = (SntOcupacionMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KOCUPACION set OCU_DESCRIPCION = :P0, OCU_FECBAJA = :P1 where US_OCUPACION = :P2 ";
            return EjecutaDML(sqlQuery, dtoDatos.ocu_descripcion, dtoDatos.ocu_fecbaja, dtoDatos.us_ocupacion);
        }

        private Object dmlDelete(Object oDatos)
        {
            SntOcupacionMdl dtoDatos = (SntOcupacionMdl)oDatos;
            String sqlQuery = " delete from SIT_SNT_KOCUPACION where US_OCUPACION = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_ocupacion);
        }

        private Object dmlHabilitar(Object oDatos)
        {
            SntOcupacionMdl dtoDatos = (SntOcupacionMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KOCUPACION set DTFECHABAJA = null where US_OCUPACION = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_ocupacion);
        }

        private Object dmlDeshabilitar(Object oDatos)
        {
            SntOcupacionMdl dtoDatos = (SntOcupacionMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KOCUPACION set DTFECHABAJA = sysdate where US_OCUPACION = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_ocupacion);
        }

        private Object dmlImportar(Object oDatos)
        {

            Int16 iContador = 0;
            List<SntOcupacionMdl> lstDatos = (List<SntOcupacionMdl>)oDatos;

            String sqlQuery = " insert into SIT_SNT_KOCUPACION ( US_OCUPACION, OCU_DESCRIPCION, OCU_FECBAJA ) "
                + " VALUES ( :P0 , :P1, NULL ) ";

            foreach (SntOcupacionMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.us_ocupacion, dtoDatos.ocu_descripcion);
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
                            + "SELECT US_OCUPACION, OCU_DESCRIPCION, OCU_FECBAJA from SIT_SNT_KOCUPACION order by US_OCUPACION "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select US_OCUPACION as id, OCU_DESCRIPCION as text  FROM SIT_SNT_KOCUPACION where OCU_FECBAJA IS NULL ORDER BY US_OCUPACION";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select US_OCUPACION, OCU_DESCRIPCION FROM SIT_SNT_KOCUPACION where OCU_FECBAJA IS NULL ORDER BY US_OCUPACION";
            dtDatos = (DataTable)ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["US_OCUPACION"]), row["OCU_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}