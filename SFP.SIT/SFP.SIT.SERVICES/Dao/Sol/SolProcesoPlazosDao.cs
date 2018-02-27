using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Sol;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Sol
{
    public class SolProcesoPlazosDao : BaseDao
    {

        public const short OPE_SELECT_LISTA = 211;


        public SolProcesoPlazosDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {                    
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_LISTA] = new Func<Object, object>(dmlSelectLista);
            
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              

        private object dmlInsert(object oDatos)
        {
            SolProcesoPlazosMdl dtoDatos = (SolProcesoPlazosMdl)oDatos;
            string sqlQuery = " insert into SIT_SOL_KPROCESO_PLAZOS ( krp_claproceso, tso_clatiposol, kpz_tipoplazo, "
                 + " kpz_plazo, kpz_verde, kpz_amarillo ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5) ";

            return EjecutaDML(sqlQuery, dtoDatos.krp_claproceso, dtoDatos.tso_clatiposol, dtoDatos.kpz_tipoplazo,
                dtoDatos.kpz_plazo, dtoDatos.kpz_verde, dtoDatos.kpz_amarillo);
        }

        private object dmlUpdate(object oDatos)
        {
            SolProcesoPlazosMdl dtoDatos = (SolProcesoPlazosMdl)oDatos;
            string sqlQuery = " update SIT_SOL_KPROCESO_PLAZOS "
                    + " set kpz_plazo = :P0, kpz_verde = :P1, kpz_amarillo = :P2 "
                    + " where krp_claproceso = :P3 AND tso_clatiposol = :P4 AND kpz_tipoplazo = :P5 ";

            return EjecutaDML(sqlQuery, dtoDatos.kpz_plazo, dtoDatos.kpz_verde, dtoDatos.kpz_amarillo,
                 dtoDatos.krp_claproceso, dtoDatos.tso_clatiposol, dtoDatos.kpz_tipoplazo );
        }

        private object dmlDelete(object oDatos)
        {
            SolProcesoPlazosMdl dtoDatos = (SolProcesoPlazosMdl)oDatos;
            string sqlQuery = " delete from SIT_SOL_KPROCESO_PLAZOS where KRP_CLAPROCESO = :P0 and TSO_CLATIPOSOL = :P1 and KPZ_TIPOPLAZO = :P2 ";
            return EjecutaDML(sqlQuery, dtoDatos.krp_claproceso, dtoDatos.tso_clatiposol, dtoDatos.kpz_tipoplazo);
        }

        private Object dmlImportar(object oDatos)
        {
            Int16 iContador = 0;
            List<SolProcesoPlazosMdl> lstDatos = (List<SolProcesoPlazosMdl>)oDatos;

            string sqlQuery = " insert into SIT_SOL_KPROCESO_PLAZOS ( krp_claproceso, tso_clatiposol, kpz_tipoplazo, "
                 + " kpz_plazo, kpz_verde, kpz_amarillo ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5 ) ";

            foreach (SolProcesoPlazosMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.krp_claproceso, dtoDatos.tso_clatiposol, dtoDatos.kpz_tipoplazo,
                    dtoDatos.kpz_plazo, dtoDatos.kpz_verde, dtoDatos.kpz_amarillo );
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
                + " SELECT PLZ.krp_claproceso,krp_descripcion,  PLZ.tso_clatiposol, tso_descripcion, kpz_tipoplazo, kpz_plazo, kpz_verde, kpz_amarillo "
                + " FROM SIT_SOL_KPROCESO_PLAZOS PLZ, SIT_SOL_KPROCESO PRC, SIT_SOL_KTIPO_SOLICITUD TSOL "
                + " WHERE PRC.krp_claproceso = PLZ.krp_claproceso "
                + " AND TSOL.tso_clatiposol = PLZ.tso_clatiposol "
                + " ORDER BY krp_claproceso "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup); ;
        }

        private List<SolProcesoPlazosMdl> dmlSelectLista(object oDatos)
        {
            string sqlQuery = "SELECT  krp_claproceso, tso_clatiposol, kpz_tipoplazo, kpz_plazo, kpz_verde, kpz_amarillo from SIT_SOL_KPROCESO_PLAZOS order by krp_claproceso ";

            return CrearLista(ConsultaDML(sqlQuery));
        }


        private List<SolProcesoPlazosMdl> CrearLista(DataTable dtDatos)
        {

            List<SolProcesoPlazosMdl> lstDatos = new List<SolProcesoPlazosMdl>();

            foreach (DataRow drDato in dtDatos.Rows)
            {
                try
                {
                    SolProcesoPlazosMdl solMdl = new SolProcesoPlazosMdl(
                        Convert.ToInt32(drDato["krp_claproceso"]), Convert.ToInt32(drDato["tso_clatiposol"]), Convert.ToInt32(drDato["kpz_tipoplazo"]),
                        Convert.ToInt32(drDato["kpz_plazo"]), Convert.ToInt32(drDato["kpz_verde"]), Convert.ToInt32(drDato["kpz_amarillo"]) );
                    lstDatos.Add(solMdl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (lstDatos.Count == 0)
                return null;
            else
                return lstDatos;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}