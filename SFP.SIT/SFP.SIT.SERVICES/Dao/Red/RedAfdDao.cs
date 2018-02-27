using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.SIT.SERVICES.Model.Red;
using SFP.Persistencia;
using System.Data;
using SFP.SIT.SERVICES.Model;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Red
{
    public class RedAfdDao : BaseDao
    {
        public const int OPE_SELECT_FLUJO_PREFIJO = 211;

        int iSecuencia { get; set; }
        public RedAfdDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            dicOperacion[OPE_SELECT_FLUJO_PREFIJO] = new Func<Object, object>(dmlSelectFlujoPrefijo);            
        }
       
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            RedAfdMdl dtoDatos = (RedAfdMdl)oDatos;
            iSecuencia = SecuenciaDML("SEC_SIT_AFD");

            String sqlQuery = " insert into SIT_RED_AFD ( AFD_CLAAFD, AFD_DESCRIPCION, AFD_FECBAJA, AFD_PREFIJO ) "
                    + " VALUES ( :P0, :P1, :P2, :P3 ) ";

            return EjecutaDML(sqlQuery, iSecuencia, dtoDatos.afd_descripcion, dtoDatos.afd_fecbaja, dtoDatos.afd_prefijo);
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedAfdMdl dtoDatos = (RedAfdMdl)oDatos;
            String sqlQuery = " update SIT_RED_AFD set AFD_DESCRIPCION = :P0, AFD_FECBAJA = :P1, AFD_PREFIJO = :P2"
                    + " where AFD_CLAAFD = :P3 ";

            return EjecutaDML(sqlQuery, dtoDatos.afd_descripcion, dtoDatos.afd_fecbaja, dtoDatos.afd_prefijo, dtoDatos.afd_claAfd);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedAfdMdl dtoDatos = (RedAfdMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_AFD where AFD_CLAAFD = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.afd_claAfd);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedAfdMdl> lstDatos = (List<RedAfdMdl>)oDatos;

            String sqlQuery = " insert into SIT_RED_AFD ( AFD_CLAAFD, AFD_DESCRIPCION, AFD_FECBAJA, AFD_PREFIJO ) "
                    + " VALUES ( :P0, :P1, :P2, :P3 ) ";

            foreach (RedAfdMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.afd_claAfd, dtoDatos.afd_descripcion, dtoDatos.afd_fecbaja, dtoDatos.afd_prefijo);
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
                + " SELECT AFD_CLAAFD, AFD_DESCRIPCION, AFD_FECBAJA, AFD_PREFIJO from SIT_RED_AFD "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select AFD_CLAAFD as id, AFD_DESCRIPCION as text, AFD_FECBAJA  FROM SIT_RED_AFD";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select AFD_CLAAFD, AFD_DESCRIPCION FROM SIT_RED_AFD";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["AFD_CLAAFD"]), row["AFD_DESCRIPCION"].ToString());
            }
            return dicParametros;
        }

        private Dictionary<int, string> dmlSelectFlujoPrefijo(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select AFD_CLAAFD, AFD_PREFIJO FROM SIT_RED_AFD";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["AFD_CLAAFD"]), row["AFD_PREFIJO"].ToString());
            }
            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}