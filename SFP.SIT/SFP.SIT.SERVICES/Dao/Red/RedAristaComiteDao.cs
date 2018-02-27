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
    public class RedAristaComiteDao : BaseDao
    {
        public RedAristaComiteDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            RedAristaComiteMdl dtoDatos = (RedAristaComiteMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_RED_ARISTA_COMITE (us_clafolio, nre_claarista,  "
                    + " com_motivo,  rbc_clacomiterubro  ) "
                    + " VALUES (:P0, :P1, :P2, :P3) ";

            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nre_claarista, dtoDatos.com_motivo, dtoDatos.rbc_clacomiterubro);
        }

        private Object dmlUpdate(Object oDatos)
        {
            RedAristaComiteMdl dtoDatos = (RedAristaComiteMdl)oDatos;
            String sqlQuery = " update  SIT_RED_ARISTA_COMITE"
                    + " SET com_motivo = :P0,  rbc_clacomiterubro= :P1 "
                    + " WHERE us_clafolio= :P2 AND  nre_claarista = :P3 ";

            return EjecutaDML(sqlQuery,
                    dtoDatos.com_motivo, dtoDatos.rbc_clacomiterubro, 
                    dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }

        private Object dmlDelete(Object oDatos)
        {
            RedAristaComiteMdl dtoDatos = (RedAristaComiteMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_ARISTA_COMITE where us_clafolio= :P0 AND  nre_claarista = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nre_claarista);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<RedAristaComiteMdl> lstDatos = (List<RedAristaComiteMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_RED_ARISTA_COMITE (us_clafolio, nre_claarista,  "
                    + " com_motivo, rbc_clacomiterubro ) "
                    + " VALUES ( :P0, :P1, :P2, :P3) ";

            foreach (RedAristaComiteMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.us_clafolio, dtoDatos.nre_claarista, dtoDatos.com_motivo, dtoDatos.rbc_clacomiterubro);
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
                            + " SELECT us_clafolio, nre_claarista, com_motivo, COM.rbc_clacomiterubro, rub.rbc_descripcion  "
                + " from SIT_RED_ARISTA_COMITE COM, SIT_RED_KCOMITE_RUBRO RUB "
                + " WHERE COM.rbc_clacomiterubro = RUB.rbc_clacomiterubro "
                + " ORDER BY us_clafolio, nre_claarista "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
