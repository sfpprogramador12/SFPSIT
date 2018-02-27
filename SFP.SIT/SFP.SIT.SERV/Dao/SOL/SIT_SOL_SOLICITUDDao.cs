using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.SOL;
 
namespace SFP.SIT.SERV.Dao.SOL
{
	 public class SIT_SOL_SOLICITUDDao : BaseDao
	 {
	 	 public SIT_SOL_SOLICITUDDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SOL_SOLICITUD oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_SOLICITUD( prcclave, solfecrecrev, solfecacl, megclave, sntclave, sotclave, solnotificado, solmotdesecha, solrespclave, metclave, solotroderacc, solfecresp, solfecenv, solfecent, solfecsol, soldat, soldes, solarcdes, solotromod, solfecrec, solclave, solfolio) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15, :P16, :P17, :P18, :P19, :P20, :P21) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.prcclave, oDatos.solfecrecrev, oDatos.solfecacl, oDatos.megclave, oDatos.sntclave, oDatos.sotclave, oDatos.solnotificado, oDatos.solmotdesecha, oDatos.solrespclave, oDatos.metclave, oDatos.solotroderacc, oDatos.solfecresp, oDatos.solfecenv, oDatos.solfecent, oDatos.solfecsol, oDatos.soldat, oDatos.soldes, oDatos.solarcdes, oDatos.solotromod, oDatos.solfecrec, oDatos.solclave, oDatos.solfolio ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SOL_SOLICITUD> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_SOLICITUD( prcclave, solfecrecrev, solfecacl, megclave, sntclave, sotclave, solnotificado, solmotdesecha, solrespclave, metclave, solotroderacc, solfecresp, solfecenv, solfecent, solfecsol, soldat, soldes, solarcdes, solotromod, solfecrec, solclave, solfolio) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15, :P16, :P17, :P18, :P19, :P20, :P21) ";  
	 	 	  foreach (SIT_SOL_SOLICITUD oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.prcclave, oDatos.solfecrecrev, oDatos.solfecacl, oDatos.megclave, oDatos.sntclave, oDatos.sotclave, oDatos.solnotificado, oDatos.solmotdesecha, oDatos.solrespclave, oDatos.metclave, oDatos.solotroderacc, oDatos.solfecresp, oDatos.solfecenv, oDatos.solfecent, oDatos.solfecsol, oDatos.soldat, oDatos.soldes, oDatos.solarcdes, oDatos.solotromod, oDatos.solfecrec, oDatos.solclave, oDatos.solfolio ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SOL_SOLICITUD oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SOL_SOLICITUD SET  prcclave = :P0, solfecrecrev = :P1, solfecacl = :P2, megclave = :P3, sntclave = :P4, sotclave = :P5, solnotificado = :P6, solmotdesecha = :P7, solrespclave = :P8, metclave = :P9, solotroderacc = :P10, solfecresp = :P11, solfecenv = :P12, solfecent = :P13, solfecsol = :P14, soldat = :P15, soldes = :P16, solarcdes = :P17, solotromod = :P18, solfecrec = :P19, solfolio = :P20 WHERE  solclave = :P21 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.prcclave, oDatos.solfecrecrev, oDatos.solfecacl, oDatos.megclave, oDatos.sntclave, oDatos.sotclave, oDatos.solnotificado, oDatos.solmotdesecha, oDatos.solrespclave, oDatos.metclave, oDatos.solotroderacc, oDatos.solfecresp, oDatos.solfecenv, oDatos.solfecent, oDatos.solfecsol, oDatos.soldat, oDatos.soldes, oDatos.solarcdes, oDatos.solotromod, oDatos.solfecrec, oDatos.solfolio, oDatos.solclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SOL_SOLICITUD oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SOL_SOLICITUD WHERE  solclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.solclave ); 
	 	 }
 
 
	 	 public List<SIT_SOL_SOLICITUD> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_SOLICITUD ";
	 	 	  return CrearListaMDL<SIT_SOL_SOLICITUD>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_SOL_SOLICITUD dmlSelectID(SIT_SOL_SOLICITUD oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_SOLICITUD WHERE  solclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SOL_SOLICITUD>(ConsultaDML ( sSQL,  oDatos.solclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SOL_SOLICITUD );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SOL_SOLICITUD );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SOL_SOLICITUD );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public const string PARAM_FECHA = "PARAM_FECHA ";


        public object dmlUpdateID(SIT_SOL_SOLICITUD dtoDatos)
        {
            String sqlQuery = " update SIT_SOL_SOLICITUD SET "
                + " metclave =:P0, sotclave=:P1 "
                + " where solclave = :P2";

            return EjecutaDML(sqlQuery, dtoDatos.metclave, dtoDatos.sotclave, dtoDatos.solclave);
        }



        public DataTable dmlSelectAreasFaltantes(Dictionary<string, Object> dicParam)
        {
            String sqlQuery = " SELECT usrnombre || ' ' || usrPATERNO || ' ' || usrMATERNO as NOMBRE, aah.arhsiglas as SIGLA, aah.arhdescripcion as DESCRIPCION " +
            " FROM SIT_RED_NODO NODO, SIT_ADM_USUARIO USU, SIT_ADM_AREAHIST aah " +
            " WHERE NODO.solclave = :P0 " +
            " AND nodo.nodatendido = 0 " +
            " AND aah.araclave = nodo.araclave " +
            " AND NODO.usrClave = USU.usrClave " +
            " AND :P1 BETWEEN arhFecIni and arhFecFin " +
            " ORDER BY DESCRIPCION ";

            return ConsultaDML(sqlQuery, dicParam[DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE], dicParam[PARAM_FECHA]);
        }

        public SIT_SOL_SOLICITUD dmlSelectIDxFolio(Int64 iClaFolio)
        {
            String sSQL = " SELECT * FROM SIT_SOL_SOLICITUD WHERE  solclave = :P0 ";

            List<SIT_SOL_SOLICITUD> lstSolicitud = CrearListaMDL<SIT_SOL_SOLICITUD>(ConsultaDML(sSQL, iClaFolio) as DataTable);

            if (lstSolicitud.Count > 0)
                return lstSolicitud[0];
            else
                return null;
        }

        public DataTable dmlSelectSolicitudTranspuestaID(Int64 iClaFolio)
        {
            DataTable catalogoRet = new DataTable();
            catalogoRet.Columns.Add("titulo", typeof(string));
            catalogoRet.Columns.Add("valor", typeof(string));

            String sqlQuery = " SELECT solfecsol, metdescripcion, megDescripcion, solarcdes, solotromod, segdiassemaforo, "
               + " segsemaforocolor, soldes, soldat, segfecamp, plaz.pczplazo, solfecacl, solfecrecrev, segfecestimada "
               + " FROM SIT_SOL_PROCESOPLAZOS plaz, SIT_sol_seguimiento seg, SIT_SOL_SOLICITUD sol  "
               + " LEFT JOIN SIT_SOL_MEDIOENTRADA me ON sol.metclave = me.metclave "
               + " LEFT JOIN SIT_sol_modoentrega mt ON sol.megclave = mt.megclave "
               + " LEFT JOIN SIT_SOL_SOLICITUDTIPO ts ON sol.sotclave = ts.sotclave "
               + " WHERE sol.solclave = seg.solclave  and seg.prcclave = sol.prcclave  and sol.solclave = :P0 "
               + " and plaz.sotclave = sol.sotclave and plaz.prcclave = sol.prcclave and "
               + "  plaz.pczclave = nvl2(segfecamp, 2, 1) ";

            /* SOLO DEBE EXISTIR UN REGISTRO */

            DataTable dtDatos = ConsultaDML(sqlQuery, iClaFolio);
            foreach (DataRow row in dtDatos.Rows)
            {
                catalogoRet.Rows.Add("Fecha solicitud", ((DateTime)row["solfecsol"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA

                if (row["segfecestimada"].ToString() == "")
                    catalogoRet.Rows.Add("Fecha limite", "");
                else
                    catalogoRet.Rows.Add("Fecha limite", ((DateTime)row["segfecestimada"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA

                if (row["segfecamp"].ToString() == "")
                {
                    catalogoRet.Rows.Add("Fecha prorroga", "");
                    catalogoRet.Rows.Add("Plazo", row["pczplazo"].ToString());
                }
                else
                {
                    catalogoRet.Rows.Add("Fecha prorroga", ((DateTime)row["segfecamp"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA    
                    catalogoRet.Rows.Add("Plazo", row["pczplazo"].ToString());
                }

                if (row["solfecacl"].ToString() != "")
                {
                    catalogoRet.Rows.Add("Fecha aclaración", ((DateTime)row["solfecacl"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA    
                }

                if (row["solfecrecrev"].ToString() != "")
                {
                    catalogoRet.Rows.Add("Fecha recurso", ((DateTime)row["solfecrecrev"]).ToString("dd/MM/yyyy")); // DAR FORMATO A LA FECHA    
                }

                catalogoRet.Rows.Add("Días transcurridos", row["segdiassemaforo"].ToString());
                catalogoRet.Rows.Add("Semáforo", row["segsemaforocolor"].ToString());
                catalogoRet.Rows.Add("Medio de recepción", row["metdescripcion"].ToString());
                catalogoRet.Rows.Add("Modo de entrega", row["megDescripcion"].ToString());
                catalogoRet.Rows.Add("Otra modalidad", row["solotromod"].ToString());
                catalogoRet.Rows.Add("Descripción", row["soldes"].ToString());
                catalogoRet.Rows.Add("Otros datos", row["soldat"].ToString());
            }
            return catalogoRet;
        }

        public DataTable dmlSelectSolicitudPendiente(SolBuscarMdl solBuscarMdl)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT sol.solclave,  solfolio, segfecini, nodfeccreacion, segfecestimada, ");
            sbQuery.Append(" sol.sotclave AS  carac1, NVL2(solfecacl, 1,0)  AS  carac2, segmultiple AS  carac3,  ");
            sbQuery.Append(" segedoproceso AS  carac4,  NVL2(segfecamp, 1,0) AS  carac5,  NVL2(solfecrecrev, 1, 0) AS  carac6, ");
            sbQuery.Append(" segdiassemaforo, segsemaforocolor, soldes, megDescripcion, soldat, ");
            sbQuery.Append(" nod.nedclave, seg.prcclave, nod.nodclave, seg.AFDCLAVE, metclave,");
            sbQuery.Append(" repclave as CLAVERESP, nod.nodfeclectura");
            sbQuery.Append(" FROM  SIT_SOL_SOLICITUD sol,  SIT_SOL_SEGUIMIENTO seg, SIT_red_nodo nod, ");
            sbQuery.Append(" SIT_SOL_SOLICITUDTIPO ts, SIT_sol_modoentrega te");
            sbQuery.Append(" WHERE  seg.solclave = sol.solclave  ");
            sbQuery.Append(" AND seg.prcclave = sol.prcclave ");
            sbQuery.Append(" AND ts.sotclave = sol.sotclave ");
            sbQuery.Append(" AND sol.megclave = te.megclave ");
            sbQuery.Append(" AND nod.solclave = sol.solclave ");
            sbQuery.Append(" AND nod.nodatendido = 0 ");
            sbQuery.Append(" AND nod.usrClave = :P0 ");
            sbQuery.Append(" AND nod.perclave = :P1 ");

            if (solBuscarMdl.araClave > 0)
            {
                sbQuery.Append(" AND  nod.araClave = :P2 ");
            }
            sbQuery.Append(" ORDER BY segfecini,  sol.solclave ");


            if (solBuscarMdl.araClave == 0)
            {
                return ConsultaDML(sbQuery.ToString(), solBuscarMdl.usrclave, solBuscarMdl.perClave);
            }
            else
            {
                return ConsultaDML(sbQuery.ToString(), solBuscarMdl.usrclave, solBuscarMdl.perClave, solBuscarMdl.araClave);
            }
        }

        public DataTable dmlSelectSolicitudSeguimiento(SolBuscarMdl solBuscarMdl)
        {
            StringBuilder sbQuery = new StringBuilder();
            ///////DateTime dtAux = new DateTime();

            //////sbQuery.Append(" WITH Resultado AS( ");
            //////sbQuery.Append(" select COUNT(*) OVER() RESULT_COUNT, rownum renID, a.* from ( ");
            //////sbQuery.Append(" SELECT sol.solclave,  solclave, segfecini, nodfeccreacion, segfecestimada, ");
            //////sbQuery.Append(" sol.sotclave AS  carac1, NVL2(solfecacl, 1,0)  AS  carac2, segmultiple AS  carac3,  ");
            //////sbQuery.Append(" segedoproceso AS  carac4,  NVL2(segfecamp, 1,0) AS  carac5,  NVL2(solfecrecrev, 1, 0) AS  carac6, ");
            //////sbQuery.Append(" segdiassemaforo, segsemaforocolor, soldes, megDescripcion, soldat, ");
            //////sbQuery.Append(" nod.nedclave, seg.prcclave, nod.nodclave, seg.AFDCLAVE, metclave, ariclave as CLAVERESP ");
            //////sbQuery.Append(" FROM  SIT_SOL_SOLICITUD sol,  SIT_SOL_SEGUIMIENTO seg, SIT_red_nodo nod,  ");
            //////sbQuery.Append(" SIT_SOL_SOLICITUDTIPO ts, SIT_sol_modoentrega te, SIT_ADM_PERFILNODO apn");
            //////sbQuery.Append(" WHERE  seg.solclave = sol.solclave  ");
            //////sbQuery.Append(" AND seg.prcclave = sol.prcclave ");
            //////sbQuery.Append(" AND ts.sotclave = sol.sotclave ");
            //////sbQuery.Append(" AND sol.megclave = te.megclave ");
            //////sbQuery.Append(" AND nod.solclave = sol.solclave ");

            if (solBuscarMdl.SolicitudEstado == Constantes.SolicitudEstado.CONCLUIDO)
            {
                sbQuery.Append(" SELECT solclave FROM SIT_SOL_SEGUIMIENTO ");
                sbQuery.Append(" WHERE solclave in ( ");
                sbQuery.Append(" SELECT solclave FROM SIT_red_nodo nod ");
                sbQuery.Append(" WHERE usrClave = :P0 AND araClave = :P1 GROUP BY solclave ) ");
                sbQuery.Append(" AND segfecfin is not null ");
            }
            else
            {
                sbQuery.Append(" WITH USR_SOLICITUD AS(");
                sbQuery.Append(" SELECT solclave FROM SIT_red_nodo nod");
                sbQuery.Append(" WHERE nod.solclave in (");
                sbQuery.Append(" SELECT solclave FROM SIT_red_nodo nod");
                sbQuery.Append(" WHERE  nod.nodatendido = 0 GROUP BY solclave)");
                sbQuery.Append(" AND usrClave = :P0 AND araClave = :P1)");
            }

            sbQuery.Append(" SELECT sol.solclave,  solclave, segfecini, nodfeccreacion, segfecestimada,  sol.sotclave AS  carac1, NVL2(solfecacl, 1, 0)  AS carac2,");
            sbQuery.Append(" segmultiple AS carac3, segedoproceso AS carac4, NVL2(segfecamp, 1, 0) AS carac5, NVL2(solfecrecrev, 1, 0) AS carac6,");
            sbQuery.Append(" segdiassemaforo, segsemaforocolor, soldes, megDescripcion, soldat, nod.nedclave, seg.prcclave, nod.nodclave, seg.AFDCLAVE, ");
            sbQuery.Append(" metclave, ariclave as CLAVERESP");
            sbQuery.Append(" FROM SIT_SOL_SOLICITUD sol,  SIT_SOL_SEGUIMIENTO seg, SIT_red_nodo nod,   SIT_SOL_SOLICITUDTIPO ts, SIT_sol_modoentrega te,");
            sbQuery.Append(" USR_SOLICITUD usrsol");
            sbQuery.Append(" WHERE seg.solclave = sol.solclave   AND seg.prcclave = sol.prcclave  AND ts.sotclave = sol.sotclave");
            sbQuery.Append(" AND sol.megclave = te.megclave  AND nod.solclave = sol.solclave  AND");
            sbQuery.Append(" seg.segfecfin is  null  and");
            sbQuery.Append(" sol.sotclave = 1 and seg.prcclave = 1 and usrsol.solclave = sol.solclave and nod.nodatendido = 0");

            if (solBuscarMdl.FolioIni > 0 && solBuscarMdl.FolioFin == 0)
            {
                sbQuery.Append(" AND seg.solclave = ");
                sbQuery.Append(solBuscarMdl.FolioIni);
            }
            else if (solBuscarMdl.FolioIni > 0 && solBuscarMdl.FolioFin > 0)
            {
                sbQuery.Append(" AND seg.solclave  between ");
                sbQuery.Append(solBuscarMdl.FolioIni);
                sbQuery.Append(" and ");
                sbQuery.Append(solBuscarMdl.FolioFin);
            }

            if (solBuscarMdl.FecIngresoIni != DateTime.MinValue && solBuscarMdl.FecIngresoFin != DateTime.MinValue)
            {
                sbQuery.Append(" and seg.segfecini between to_date('");
                sbQuery.Append(((DateTime)solBuscarMdl.FecIngresoIni).ToString("yyyy/MM/dd"));
                sbQuery.Append("','yyyy/mm/dd') and to_date('");
                sbQuery.Append(((DateTime)solBuscarMdl.FecIngresoFin).ToString("yyyy/MM/dd"));
                sbQuery.Append("','yyyy/mm/dd')");
            }
            else if (solBuscarMdl.FecIngresoFin != DateTime.MinValue)
            {
                sbQuery.Append(" and seg.segfecini >= to_date('");
                sbQuery.Append(((DateTime)solBuscarMdl.FecIngresoFin).ToString("yyyy/MM/dd"));
                sbQuery.Append("','yyyy/mm/dd') ");
            }

            if (solBuscarMdl.FecConcIni != DateTime.MinValue)
            {
                sbQuery.Append(" and TRUNC(seg.segfecfin) = to_date('");
                sbQuery.Append(((DateTime)solBuscarMdl.FecConcIni).ToString("yyyy/MM/dd"));
                sbQuery.Append("','yyyy/mm/dd') ");
            }

            if (solBuscarMdl.Periodo > 0)
            {
                sbQuery.Append(" and EXTRACT(YEAR FROM solfecsol) = ");
                sbQuery.Append(solBuscarMdl.Periodo);
            }

            if (solBuscarMdl.SolicitudEstado > 0)
                sbQuery.Append(" and seg.segfecfin is not null ");
            else
                sbQuery.Append(" and seg.segfecfin is  null ");

            if (solBuscarMdl.SolicitudTipo > 0)
            {
                sbQuery.Append(" and sol.sotclave = ");
                sbQuery.Append(solBuscarMdl.SolicitudTipo);
            }


            if (solBuscarMdl.ProcesoTipo > 0)
            {
                sbQuery.Append(" and seg.prcclave = ");
                sbQuery.Append(solBuscarMdl.ProcesoTipo);
            }

            if (solBuscarMdl.Descripcion != null)
            {
                sbQuery.Append(" and sol.soldes like  '%");
                sbQuery.Append(solBuscarMdl.Descripcion);
                sbQuery.Append("%'");
            }

            sbQuery.Append(" ORDER BY sol.solclave");


            ////sbQuery.Append(" ) a ) SELECT * from Resultado ");
            ////sbQuery.Append(" WHERE  renID  between :P1 and :P2 ");

            ////solBuscar.CalcularLimites();
            ////return ConsultaDML(sbQuery.ToString(), solBuscar.TipoPerfil, solBuscar.LimInf, solBuscar.LimSup);


            return ConsultaDML(sbQuery.ToString(), solBuscarMdl.usrclave, solBuscarMdl.araClave);

        }

        public DataTable dmlSelectGridPerfil(Object oDatos)
        {
            string sqlQuery = " SELECT usrClave, usrcorreo, usrnombre, usrpaterno, usrMATERNO, arhSIGLAs  "
                + " from SIT_ADM_USUARIO u, SIT_adm_areahist a "
                + " where u.KA_CLAAREA_ORIGEN = a.KA_CLAAREA "
                + " order by KU_CORREO ";
            return ConsultaDML(sqlQuery);
        }


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + "SELECT   solclave,  solclave, solfecrec,         solotromod,     solarcdes, "
                    + " soldes,         soldat,                 solfecsol,    solfecent,      solfecenv, "
                    + " solfecresp,                           afdDataMdl.solicitud.solotroderacc = regArchivoSol.us_otroderechoacceso;,   metclave, solrespclave,    solmotdesecha, "
                    + " solnotificado,     sotclave,         sntclave,      US_UNIENL,      IDFORMAENTREGA, megclave, "
                    + " KRT_CLATEMA,            solfecacl,   prcclave, solfecrecrev "
                    + " from SIT_SOL_SOLICITUD ORDER BY prcclave "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
