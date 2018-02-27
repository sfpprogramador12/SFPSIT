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
    public class SntMunicipioDao : BaseDao
    {
        public const String COL_CLAMUN = "KMU_CLAMUN";

        public SntMunicipioDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
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
            SntMunicipioMdl dtoDatos = (SntMunicipioMdl)oDatos;
            
            String sqlQuery = ""
                + " insert into SIT_SNT_KMUNICIPIO ( KMU_CLAMUN, KE_CLAEST, KPA_CLAPAI, KMU_DESCRIPCION, KMU_FECBAJA ) "
                + " VALUES ( :P0, :P1, :P2, :P3, :P4 ) ";
                
            return EjecutaDML(sqlQuery, dtoDatos.kmu_clamun, dtoDatos.ke_claest, dtoDatos.kpa_clapai, 
                dtoDatos.kmu_descripcion, dtoDatos.kmu_fecbaja);
        }

        private Object dmlUpdate(Object oDatos) 
        {
            SntMunicipioMdl dtoDatos = (SntMunicipioMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KMUNICIPIO "
                + " set KE_CLAEST = :P0, KPA_CLAPAI = :P1,  KMU_DESCRIPCION = :P2,  KMU_FECBAJA= :P3 "
                + " where KMU_CLAMUN = :P4 ";

            return EjecutaDML(sqlQuery, dtoDatos.ke_claest, dtoDatos.kpa_clapai, 
                 dtoDatos.kmu_descripcion, dtoDatos.kmu_fecbaja, dtoDatos.kmu_clamun);
        }

        private Object dmlDelete(Object oDatos) 
        {
            SntMunicipioMdl dtoDatos = (SntMunicipioMdl)oDatos;
            String sqlQuery = " delete from SIT_SNT_KMUNICIPIO where KMU_CLAMUN = :P0 ";        
            return EjecutaDML(sqlQuery,dtoDatos.kmu_clamun);
        }

        private Object dmlHabilitar(Object oDatos) 
        {
            SntMunicipioMdl dtoDatos = (SntMunicipioMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KMUNICIPIO set KMU_FECBAJA = null where KMU_CLAMUN = :P0";                 
            return EjecutaDML(sqlQuery, dtoDatos.kmu_clamun);
        }

        private Object dmlDeshabilitar(Object oDatos) 
        {
            SntMunicipioMdl dtoDatos = (SntMunicipioMdl)oDatos;
            String sqlQuery = " update SIT_SNT_KMUNICIPIO set KMU_FECBAJA = sysdate where KMU_CLAMUN = :P0 ";                 
            return EjecutaDML(sqlQuery, dtoDatos.kmu_clamun);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<SntMunicipioMdl> lstDatos = (List<SntMunicipioMdl>)oDatos;

            String sqlQuery = ""
                + " insert into SIT_SNT_KMUNICIPIO ( KMU_CLAMUN, KE_CLAEST, KPA_CLAPAI, KMU_DESCRIPCION, KMU_FECBAJA ) "
                + " VALUES ( :P0, :P1, :P2, :P3, NULL ) ";

            foreach (SntMunicipioMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.kmu_clamun, dtoDatos.ke_claest, dtoDatos.kpa_clapai, dtoDatos.kmu_descripcion);
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
                            + " SELECT MUN.KPA_CLAPAI, KPA_DESCRIPCION, EDO.KE_CLAEST, KE_DESCRIPCION, KMU_CLAMUN, KMU_DESCRIPCION, KMU_FECBAJA "
                + " FROM SIT_SNT_KMUNICIPIO MUN, SIT_SNT_KPAIS PAIS, SIT_SNT_KESTADO EDO "
                + " WHERE PAIS.KPA_CLAPAI = MUN.KPA_CLAPAI "
                + " AND EDO.KE_CLAEST = MUN.KE_CLAEST "
                + " ORDER BY  KE_CLAEST, KMU_CLAMUN "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private DataTable dmlSelectCombo(Object oDatos)
        {
            String sqlQuery = " Select KMU_CLAMUN as id, KMU_DESCRIPCION as text FROM SIT_SNT_KMUNICIPIO where KMU_FECBAJA IS NULL ORDER BY KMU_CLAMUN";
            return ConsultaDML(sqlQuery);
        }

        private Dictionary<int, string> dmlSelectHashMap(Object oDatos)
        {
            Dictionary<int, string> dicParametros = new Dictionary<int, string>();
            DataTable dtDatos;

            String sqlQuery = " Select KMU_CLAMUN, KMU_DESCRIPCION FROM SIT_SNT_KMUNICIPIO where KMU_FECBAJA IS NULL ORDER BY KMU_CLAMUN";
            dtDatos = (DataTable)ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
            {
                dicParametros.Add(Convert.ToInt32(row["KMU_CLAMUN"]), row["KMU_DESCRIPCION"].ToString());
            }

            return dicParametros;
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
