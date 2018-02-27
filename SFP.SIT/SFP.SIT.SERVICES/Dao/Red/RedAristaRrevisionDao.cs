using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Red;
using SFP.SIT.SERVICES.Util;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Red
{
    public class RedAristaRrevisionDao : BaseDao
    {
        public const int OPE_SELECT_REC_REVISION_TRANSPUESTA = 211;


        public const string COL_CLAFOLIO = "US_CLAFOLIO";

        public RedAristaRrevisionDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_REC_REVISION_TRANSPUESTA] = new Func<Object, object>(dmlSelectTranspuesta);
            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private object dmlInsert(object oDatos)
        {
            RedAristaRrevisionMdl dtoDatos = (RedAristaRrevisionMdl)oDatos;

            String sqlQuery = ""
                + " insert into SIT_RED_ARISTA_RREVISION ( NRE_CLAARISTA, US_CLAFOLIO, REV_EXPEDIENTE, REV_RESPONSABLE, REV_CORREO) "
                + " VALUES ( :P0, :P1, :P2, :P3, :P4  ) ";

            return EjecutaDML(sqlQuery, dtoDatos.nre_claarista, dtoDatos.us_clafolio,
                dtoDatos.rev_expediente, dtoDatos.rev_responsable, dtoDatos.rev_correo);
        }

        private object dmlUpdate(object oDatos)
        {
            RedAristaRrevisionMdl dtoDatos = (RedAristaRrevisionMdl)oDatos;

            String sqlQuery = " update SIT_RED_ARISTA_RREVISION set  "
                + " REV_EXPEDIENTE = :P0, REV_RESPONSABLE= :P1, REV_CORREO = :P2 "
                + " where NRE_CLAARISTA = :P3 AND US_CLAFOLIO = :P4 ";

            return EjecutaDML(sqlQuery, dtoDatos.rev_expediente, dtoDatos.rev_responsable, dtoDatos.rev_correo,
                dtoDatos.nre_claarista, dtoDatos.us_clafolio);
        }

        private object dmlDelete(object oDatos)
        {
            RedAristaRrevisionMdl dtoDatos = (RedAristaRrevisionMdl)oDatos;
            String sqlQuery = " delete from SIT_RED_ARISTA_RREVISION where NRE_CLAARISTA = :P0 AND US_CLAFOLIO = :P1 ";
            return EjecutaDML(sqlQuery, dtoDatos.nre_claarista, dtoDatos.us_clafolio);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        B U S Q U E D A S
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
        private object dmlSelectGrid(object oDatos)
        {
            BaseMdl baseMdl = (BaseMdl)oDatos;
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT  NRE_CLAARISTA, US_CLAFOLIO, REV_EXPEDIENTE, REV_RESPONSABLE, REV_CORREO "
                + " from SIT_RED_ARISTA_RREVISION "
                + " order by NRE_CLAARISTA "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private object dmlSelectTranspuesta(object oDatos)
        {
            Dictionary<string, Object> dicParametros = (Dictionary<string, Object>)oDatos;

            DataTable dtDatosTrans = new DataTable();
            dtDatosTrans.Columns.Add("titulo", typeof(string));
            dtDatosTrans.Columns.Add("valor", typeof(string));
            object oValue;

            String sqlQuery = " SELECT REV_EXPEDIENTE, REV_RESPONSABLE, seg_fecini, seg_fecEstimada, NRE_OBSERVACION, DOC_NOMBRE "
               + " from sit_sol_seguimiento seg, sit_red_arista_rrevision rev, sit_red_arista ari  "
               + " LEFT JOIN sit_doc_arista docari ON  docari.NRE_CLAARISTA = ari.NRE_CLAARISTA  "
               + " LEFT JOIN sit_documento doc ON docari.doc_cladoc = doc.doc_cladoc "
               + " where seg.us_clafolio = :P0 "
               + " and seg.us_clafolio = rev.us_clafolio "
               + " and ari.NRE_CLAARISTA = rev.NRE_CLAARISTA "
               + " AND seg.krp_claproceso = :P1";

            DataTable dtDatos = ConsultaDML(sqlQuery, dicParametros[COL_CLAFOLIO], Constantes.ProcesoTipo.RECURSO_REVISION );

            int iRegistro = 1;
            foreach (DataRow row in dtDatos.Rows)
            {
                if (iRegistro == 1)
                {
                    dtDatosTrans.Rows.Add("Expediente", row["REV_EXPEDIENTE"].ToString());
                    dtDatosTrans.Rows.Add("Responsable", row["REV_RESPONSABLE"].ToString());

                    oValue = row["seg_fecini"];
                    if (oValue == DBNull.Value)
                        dtDatosTrans.Rows.Add("Fec. Recurso", "");
                    else
                        dtDatosTrans.Rows.Add("Fec. Recurso", Convert.ToDateTime(row["seg_fecini"]).ToShortDateString());

                    oValue = row["seg_fecEstimada"];
                    if (oValue == DBNull.Value)
                        dtDatosTrans.Rows.Add("Día a responder", "");
                    else
                        dtDatosTrans.Rows.Add("Día a responder", Convert.ToDateTime(row["seg_fecEstimada"]).ToShortDateString());

                    dtDatosTrans.Rows.Add("Acto", row["NRE_OBSERVACION"].ToString());

                    if (row["DOC_NOMBRE"].ToString() != "")
                        dtDatosTrans.Rows.Add("Documento " + iRegistro, row["DOC_NOMBRE"].ToString());
                }
                else
                {
                    dtDatosTrans.Rows.Add("Documento " + iRegistro, row["DOC_NOMBRE"].ToString());
                }
                iRegistro++;
            }
            return dtDatosTrans;
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
