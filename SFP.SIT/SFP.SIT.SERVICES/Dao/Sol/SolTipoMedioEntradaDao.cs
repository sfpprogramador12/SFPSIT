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
    public class SolTipoMedioEntradaDao : BaseDao
    {
        int iSecuencia { get; set; }

        public SolTipoMedioEntradaDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            SolTipoMedioEntradaMdl dtoDatos = (SolTipoMedioEntradaMdl)oDatos;
            String sqlQuery = ""
                + " insert into SIT_SOL_KTIPO_MEDIO_ENTRADA ( IDMEDIOENTRADA, MET_DESCRIPCION, MET_FECBAJA ) "
                + " VALUES ( :P0, :P1 ) ";

            return EjecutaDML(sqlQuery, dtoDatos.idmedioentrada, dtoDatos.met_descripcion, dtoDatos.met_fecbaja);
        }

        private object dmlUpdate(object oDatos)
        {
            SolTipoMedioEntradaMdl dtoDatos = (SolTipoMedioEntradaMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KTIPO_MEDIO_ENTRADA  set MET_DESCRIPCION = :P0, MET_FECBAJA = :P1 "
                + " where IDMEDIOENTRADA = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.met_descripcion, dtoDatos.met_fecbaja, dtoDatos.idmedioentrada);
        }

        private object dmlDelete(object oDatos)
        {
            SolTipoMedioEntradaMdl dtoDatos = (SolTipoMedioEntradaMdl)oDatos;
            String sqlQuery = " delete from SIT_SOL_KTIPO_MEDIO_ENTRADA where IDMEDIOENTRADA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.idmedioentrada);
        }

        private object dmlHabilitar(object oDatos)
        {
            SolTipoMedioEntradaMdl dtoDatos = (SolTipoMedioEntradaMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KTIPO_MEDIO_ENTRADA set FECBAJA = null where IDMEDIOENTRADA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.idmedioentrada);
        }

        private object dmlDeshabilitar(object oDatos)
        {
            SolTipoMedioEntradaMdl dtoDatos = (SolTipoMedioEntradaMdl)oDatos;
            String sqlQuery = " update SIT_SOL_KTIPO_MEDIO_ENTRADA set FECBAJA = sysdate where IDMEDIOENTRADA = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.idmedioentrada);
        }

        private object dmlImportar(object oDatos)
        {
            Int16 iContador = 0;
            List<SolTipoMedioEntradaMdl> lstDatos = (List<SolTipoMedioEntradaMdl>)oDatos;

            String sqlQuery = ""
                + " insert into SIT_SOL_KTIPO_MEDIO_ENTRADA ( IDMEDIOENTRADA, MET_DESCRIPCION ) "
                + " VALUES ( :P0, :P1 ) ";

            foreach (SolTipoMedioEntradaMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.idmedioentrada, dtoDatos.met_descripcion);
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
                + "SELECT IDMEDIOENTRADA, MET_DESCRIPCION, MET_FECBAJA   "
                + " from SIT_SOL_KTIPO_MEDIO_ENTRADA "
                + " order by IDMEDIOENTRADA "
                +" ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(object oDatos)
        {
            String sqlQuery = " Select IDMEDIOENTRADA as id, MET_DESCRIPCION as text FROM SIT_SOL_KTIPO_MEDIO_ENTRADA ORDER BY IDMEDIOENTRADA";
            return ConsultaDML(sqlQuery);
        }

        private Object dmlSelectHashMap(object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select IDMEDIOENTRADA, MET_DESCRIPCION FROM SIT_SOL_KTIPO_MEDIO_ENTRADA where MET_FECBAJA IS NULL  ORDER BY IDMEDIOENTRADA";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["IDMEDIOENTRADA"]), row["MET_DESCRIPCION"].ToString());
            }
            return dicParametros;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}

