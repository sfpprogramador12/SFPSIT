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
    public class SolTipoRubroTematicoDao : BaseDao
    {
        Int64 lSecuencia { get; set; }

        public SolTipoRubroTematicoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
        private object dmlInsert(object oDatos)
        {
            SolTipoRubroTematicoMdl dtoDatos = (SolTipoRubroTematicoMdl)oDatos;
            String sqlQuery = ""
                    + " insert into SIT_SOL_KTIPO_RUBRO_TEMATICO ( KRT_CLATEMA, KRT_RUBRO, KRT_PLAZO_RESERVA, KRT_FUNDAMENTO_LEGAL, KRT_FECBAJA ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4 )";

            return EjecutaDML(sqlQuery, dtoDatos.krt_clatema, dtoDatos.krt_rubro, dtoDatos.krt_plazo_reserva, dtoDatos.krt_fundamento_legal, dtoDatos.krt_fecbaja);
        }

        private object dmlUpdate(object oDatos)
        {
            SolTipoRubroTematicoMdl dtoDatos = (SolTipoRubroTematicoMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KTIPO_RUBRO_TEMATICO "
                    + " set KRT_RUBRO = :P0, KRT_PLAZO_RESERVA= :P1, KRT_FUNDAMENTO_LEGAL= :P2, KRT_FECBAJA= :P3 "
                    + " where KRT_CLATEMA = :P4 ";

            return EjecutaDML(sqlQuery, dtoDatos.krt_rubro, dtoDatos.krt_plazo_reserva, dtoDatos.krt_fundamento_legal, dtoDatos.krt_fecbaja, dtoDatos.krt_clatema);
        }

        private object dmlDelete(object oDatos)
        {
            SolTipoRubroTematicoMdl dtoDatos = (SolTipoRubroTematicoMdl)oDatos;
            String sqlQuery = " delete from SIT_SOL_KTIPO_RUBRO_TEMATICO where KRT_CLATEMA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.krt_clatema);
        }

        private object dmlHabilitar(object oDatos)
        {
            SolTipoRubroTematicoMdl dtoDatos = (SolTipoRubroTematicoMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KTIPO_RUBRO_TEMATICO set FECBAJA = null where KRT_CLATEMA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.krt_clatema);
        }

        private object dmlDeshabilitar(object oDatos)
        {
            SolTipoRubroTematicoMdl dtoDatos = (SolTipoRubroTematicoMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KTIPO_RUBRO_TEMATICO set FECBAJA = sysdate where KRT_CLATEMA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.krt_clatema);
        }

        private object dmlImportar(object oDatos)
        {
            Int16 iContador = 0;
            List<SolTipoRubroTematicoMdl> lstDatos = (List<SolTipoRubroTematicoMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_SOL_KTIPO_RUBRO_TEMATICO ( KRT_CLATEMA, KRT_RUBRO, KRT_PLAZO_RESERVA, KRT_FUNDAMENTO_LEGAL, KRT_FECBAJA ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, null )";

            foreach (SolTipoRubroTematicoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.krt_clatema, dtoDatos.krt_rubro, dtoDatos.krt_plazo_reserva, dtoDatos.krt_fundamento_legal);
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
            + "SELECT KRT_CLATEMA, KRT_RUBRO, KRT_PLAZO_RESERVA, KRT_FUNDAMENTO_LEGAL,  KRT_FECBAJA "
                + " from SIT_SOL_KTIPO_RUBRO_TEMATICO "
                + " order by KRT_CLATEMA "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(object oDatos)
        {
            String sqlQuery = " Select KRT_CLATEMA as id, KRT_RUBRO as text FROM SIT_SOL_KTIPO_RUBRO_TEMATICO ORDER BY KRT_CLATEMA";
            return ConsultaDML(sqlQuery);
        }

        private Object dmlSelectHashMap(object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            string sqlQuery = " Select KRT_CLATEMA, KRT_RUBRO FROM SIT_SOL_KTIPO_RUBRO_TEMATICO ORDER BY KRT_CLATEMA";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["KRT_CLATEMA"]), row["KRT_RUBRO"].ToString());
            }

            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
