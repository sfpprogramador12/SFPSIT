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
using SFP.SIT.SERV.Model.RED;
 
namespace SFP.SIT.SERV.Dao.RED
{
	 public class SIT_RED_ARISTADao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_RED_ARISTADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_RED_ARISTA oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RED_ARISTA"); 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_ARISTA( rtpclave, prcclave, solclave, arihito, aridiasnat, aridiaslab, arifecenvio, ariclave, noddestino, nodorigen) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.rtpclave, oDatos.prcclave, oDatos.solclave, oDatos.arihito, oDatos.aridiasnat, oDatos.aridiaslab, oDatos.arifecenvio, iSecuencia , oDatos.noddestino, oDatos.nodorigen ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_RED_ARISTA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_RED_ARISTA( rtpclave, prcclave, solclave, arihito, aridiasnat, aridiaslab, arifecenvio, ariclave, noddestino, nodorigen) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9) ";  
	 	 	  foreach (SIT_RED_ARISTA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_RED_ARISTA"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.rtpclave, oDatos.prcclave, oDatos.solclave, oDatos.arihito, oDatos.aridiasnat, oDatos.aridiaslab, oDatos.arifecenvio, iSecuencia , oDatos.noddestino, oDatos.nodorigen ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_RED_ARISTA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_RED_ARISTA SET  rtpclave = :P0, prcclave = :P1, solclave = :P2, arihito = :P3, aridiasnat = :P4, aridiaslab = :P5, arifecenvio = :P6, noddestino = :P7, nodorigen = :P8 WHERE  ariclave = :P9 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.rtpclave, oDatos.prcclave, oDatos.solclave, oDatos.arihito, oDatos.aridiasnat, oDatos.aridiaslab, oDatos.arifecenvio, oDatos.noddestino, oDatos.nodorigen, oDatos.ariclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_RED_ARISTA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_RED_ARISTA WHERE  ariclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.ariclave ); 
	 	 }
 
 
	 	 public List<SIT_RED_ARISTA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_ARISTA ";
	 	 	  return CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_RED_ARISTA dmlSelectID(SIT_RED_ARISTA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_RED_ARISTA WHERE  ariclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML ( sSQL,  oDatos.ariclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_RED_ARISTA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_RED_ARISTA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_RED_ARISTA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public const string PARAM_ARITIPO_INFOADI = "INFOADI";
        public const string PARAM_ARITIPO_INFOADI_RESP = "INFOADI_RESP";
        public const string PARAM_rtpclave_PRORROGA = "rtpclave_PRORROGA";
        public const string PARAM_rtpclave_SIN_RESPUESTA = "rtpclave_SIN_RESPUESTA";
        public const string PARAM_NO_ESTADOS = "NO_ESTADOS";
        public const string PARAM_ARJ_ORIGEN = "ARJ_ORIGEN";
        public const string PARAM_FECHA = "PARAM_FECHA";

        public SIT_RED_ARISTA dmlSelectAristaID(Object oDatos)
        {
            //////Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            //////String sqlQuery = " SELECT nodorigen, noddestino, usrClave, ariclave, NRE_FECINI, NRE_FECFIN,"
            //////        + " arimensaje, aridiaslab, aridiasnat, rtpclave, solclave, nre_feclectura, arihito, megclave, NRE_FECLECUSU "
            //////        + " FROM SIT_RED_ARISTA WHERE solclave = :P0 and ariclave = :P1";

            //////List<SIT_RED_ARISTA> lstDatos = CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML(sqlQuery, dicParametro[DButilSIT_RED_ARISTA_COL.ARICLAVE], dicParametro[DButilSIT_RED_ARISTA_COL.ARICLAVE]));
            //////if (lstDatos.Count == 0)
                return null;
            //////else
            //////{
            //////    return lstDatos[0];
            //////}
        }

        public Dictionary<Int64, RedAristaSegMdl> dmlSelectSeguimiento(Dictionary<string, object> dicParam)
        {
            Int64 iArista = 0;
            Dictionary<Int64, RedAristaSegMdl> dicDatos = new Dictionary<Int64, RedAristaSegMdl>();

            ////String sqlQuery = " WITH ADOC AS( SELECT ariclave, LISTAGG(docclave, ',') WITHIN GROUP(ORDER BY docclave) as docclave "
            ////    + " FROM  SIT_DOC_ARISTA "
            ////    + " WHERE solclave = :P0 "
            ////    + " GROUP BY ariclave ) "
            ////    + "SELECT R.ariclave, nodorigen, AREA.arhSiglas as ORIGEN, rtpdescripcion,   noddestino, AREA2.arhsiglas as DESTINO, "
            ////    + " TO_CHAR (N.nodfeccreacion, 'YYYY-MM-DD HH24:MI') as FECINI,"
            ////    + " TO_CHAR (R.arifecenvio, 'YYYY-MM-DD HH24:MI') as FECFIN,"
            ////    + " aridiaslab, arimensaje,   usrnombre || ' ' || usrPATERNO || ' ' || usrMATERNO as Responsable, docclave, N2.nodatendido, NRE_FECLECTURA, KNE_DESCRIPCION "
            ////    + " FROM SIT_RED_NODOESTADO RNE, SIT_ADM_USUARIO U, SIT_RESP_TIPO AEDO, SIT_RED_NODO N,  SIT_adm_areahist AREA, "
            ////    + " SIT_RED_NODO N2, SIT_adm_areahist AREA2, "
            ////    + " SIT_RED_ARISTA R LEFT JOIN ADOC ON ADOC.ariclave = R.ariclave "
            ////    + " WHERE R.solclave = :P1 "
            ////    + " AND N.nodclave = R.nodorigen AND "
            ////    + " R.solclave = N.solclave AND AREA.ARACLAVE = N.ARACLAVE AND "
            ////    + " N2.nodclave = R.noddestino AND "
            ////    + " R.solclave = N2.solclave AND AREA2.ARACLAVE = N2.ARACLAVE AND "
            ////    + " U.usrClave = N2.usrClave AND AEDO.rtpclave = R.rtpclave "
            ////    + " AND :P2 BETWEEN AREA.arhfecini and AREA.arhfecfin "
            ////    + " AND :P3 BETWEEN AREA2.arhfecini and AREA2.arhfecfin "
            ////    + " AND RNE.nedclave = N2.nedclave "
            ////    + " ORDER BY R.ariclave";



            String sqlQuery = "SELECT ariclave, nodorigen, AREA.arhSiglas as ORIGEN, rtpDescripcion, noddestino, AREA2.arhsiglas as DESTINO, "
                + " TO_CHAR(N.nodfeccreacion, 'YYYY-MM-DD HH24:MI') as FECINI, TO_CHAR(R.arifecenvio, 'YYYY-MM-DD HH24:MI') as FECFIN, aridiaslab, "
                + " usrnombre || ' ' || usrPATERNO || ' ' || usrMATERNO as Responsable, N2.nodatendido, TO_CHAR(N2.nodfeclectura, 'YYYY-MM-DD HH24:MI') as FECLECTURA , nedDescripcion "
                + " FROM SIT_RED_ARISTA R, "
                + " SIT_RESP_TIPO AEDO, SIT_ADM_USUARIO U, SIT_RED_NODOESTADO RNE,  "
                + " SIT_RED_NODO N, SIT_adm_areahist AREA, SIT_RED_NODO N2, SIT_adm_areahist AREA2 "
                + " WHERE  R.solclave = :P0  AND r.RTPCLAVE = AEDO.RTPCLAVE "
                + " AND N.nodclave = R.nodorigen  AND AREA.ARACLAVE = N.ARACLAVE  AND :P1  BETWEEN AREA.arhfecini and AREA.arhfecfin "
                + " AND  N2.nodclave = R.noddestino AND AREA2.ARACLAVE = N2.ARACLAVE AND :P2  BETWEEN AREA2.arhfecini and AREA2.arhfecfin "
                + " AND  U.usrClave = N2.usrClave "
                + " AND RNE.nedclave = N2.nedclave "
                + " ORDER BY R.ariclave ";

            DataTable dtoDatos = ConsultaDML(sqlQuery, dicParam[DButil.SIT_SOL_SOLICITUD_COL.SOLCLAVE],
                dicParam[PARAM_FECHA], dicParam[PARAM_FECHA]);

            foreach (DataRow dr in dtoDatos.Rows)
            {
                iArista = Convert.ToInt64(dr[0]);
                RedAristaSegMdl frmAristaMdl = new RedAristaSegMdl();
                frmAristaMdl.Arista = iArista;
                frmAristaMdl.Origen = Convert.ToInt64(dr[1]);
                frmAristaMdl.OrigenSigla = dr[2].ToString();
                frmAristaMdl.Accion = dr[3].ToString();
                frmAristaMdl.Destino = Convert.ToInt64(dr[4]);
                frmAristaMdl.DestinoSigla = dr[5].ToString();
                frmAristaMdl.FecIni = Convert.ToDateTime(dr[6]);
                frmAristaMdl.FecFin = Convert.ToDateTime(dr[7]);
                frmAristaMdl.DiasLaborales = Convert.ToInt32(dr[8]);
                //frmAristaMdl.Observacion = dr[9].ToString();
                frmAristaMdl.Responsable = dr[9].ToString();
                frmAristaMdl.Atendido = Convert.ToInt32(dr[10]);
                frmAristaMdl.FecLectura = Convert.ToDateTime(dr[11]);
                frmAristaMdl.NodoEstado = dr[12].ToString();
                dicDatos.Add(iArista, frmAristaMdl);
            }


            return dicDatos;
        }


        //public Object dmlUpdateFecLectura(SIT_RED_ARISTA dtoDatos)
        //{
        //    String sqlQuery = " update  SIT_RED_ARISTA SET nre_feclectura = :P0 "
        //            + " where  solclave = :P1 AND ariclave = :P2 ";

        //    return EjecutaDML(sqlQuery, dtoDatos.solclave, dtoDatos.ariclave);
        //}

        public Object dmlUpdateAristaHito(SIT_RED_ARISTA dtoDatos)
        {
            String sqlQuery = " update  SIT_RED_ARISTA SET arihito = :P0 "
                    + " where  solclave = :P1 AND ariclave = :P2 ";

            return EjecutaDML(sqlQuery, dtoDatos.arihito,
 dtoDatos.ariclave);
        }

        public Object dmlUpdateAristaMsj(SIT_RED_ARISTA dtoDatos)
        {
            String sqlQuery = " INSERT INTO  SIT_RED_ARISTALECTURA SET nre_feclectura = :P0 "
                    + " where  solclave = :P1 AND ariclave = :P2 ";

            return EjecutaDML(sqlQuery,  dtoDatos.ariclave);
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( " +
                " select ARI.solclave, ARI.ariclave, USU.usrClave, USU.usrNOMBRE || ' ' || USU.usrpaterno || ' ' || USU.usrmaterno as KU_USUARIO, " +
                " ARI.nodorigen, AORI.ARHDESCRIPCION as ORIGEN, ARI.rtpclave, TARI.rtpdescripcion, ARI.noddestino, ADES.ARHDESCRIPCION as DESTINO,   " +
                "AORI.ARHFECINI, AORI.ARHFECFIN, ARI.aridiaslab, DES.NODFECLECTURA, ARI.aridiasnat, ARI.arihito " +
                "from SIT_RED_ARISTA ARI, SIT_RESP_TIPO TARI, SIT_RED_NODO ORI, SIT_RED_NODO DES, SIT_adm_areahist AORI, SIT_adm_areahist ADES, SIT_ADM_USUARIO USU " +
                "WHERE TARI.rtpclave = ARI.rtpclave " +
                "AND ORI.nodclave = nodorigen " +
                "AND DES.nodclave = noddestino " +
                "AND AORI.ARACLAVE = ORI.ARACLAVE " +
                "AND ADES.ARACLAVE = Des.ARACLAVE " +
                "AND USU.usrClave = ORI.usrClave " +
                "ORDER BY ARI.solclave, ARI.ariclave " +
                ") a ) SELECT* from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }



        // , megclave, NRE_FECLECUSU
        // CREAR UNNUEVO AJSUTE DONDE SE UTILICE EL MODO DE ENTRADA....

        public DataTable dmlSelectAristaAclaracion(Int64 iClaFolio)
        {
            String sqlQuery = " SELECT nodorigen, noddestino, ariclave, arifecenvio,"
                    + " arimensaje, aridiaslab, aridiasnat, rtpclave, solclave, nre_feclectura, arihito"
                    + " FROM  SIT_RED_ARISTA WHERE rtpclave = " + Constantes.General.ID_PENDIENTE + " and solclave = :P0 ";
            return ConsultaDML(sqlQuery, iClaFolio);
        }


        //////public List<SIT_RED_ARISTA> dmlSelectAristaFolioDestino(Dictionary<string, Object> dicParametro)
        //////{
        //////    String sqlQuery = " SELECT nodorigen, noddestino,  ariclave,  arifecenvio,"
        //////            + " arimensaje, aridiaslab, aridiasnat, rtpclave, solclave, nre_feclectura, arihito "
        //////            + " FROM SIT_RED_ARISTA WHERE solclave = :P0 and noddestino = :P1";

        //////    return CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML(sqlQuery,  dicParametro[DButil.SIT_RED_ARISTA_COL.noddestino]) as DataTable);
        //////}


        //////public List<SIT_RED_ARISTA> dmlSelectAristaSinRespuesta(Object oDatos)
        //////{
        //////    Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
        //////    String sqlQuery = " SELECT nodorigen, noddestino, ariclave, arifecenvio, "
        //////            + " arimensaje, aridiaslab, aridiasnat, rtpclave, solclave, nre_feclectura, arihito "
        //////            + " FROM  SIT_RED_ARISTA WHERE solclave = :P0 and rtpclave = :P1";

        //////    // SERA SELECCIONAR UN TIPO DE NODO EN ESPECIFICO O CERRAR TODOS....                           

        //////    return CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML(sqlQuery, dicParametro[DButil.SIT_RED_ARISTA_COL.rtpclave]));
        //////}

        //////public List<SIT_RED_ARISTA> dmlSelectAristasEnProrroga(Dictionary<string, Object> dicParam)
        //////{


        //////    String sqlQuery = " select nodorigen, noddestino, usrClave, ariclave, NRE_FECINI, NRE_FECFIN, "
        //////        + " arimensaje, aridiaslab, aridiasnat, rtpclave, solclave, nre_feclectura, arihito, megclave, NRE_FECLECUSU "
        //////        + " from SIT_RED_ARISTA arista where arista.nodorigen in  "
        //////        + " (select nodo.nodclave from SIT_RED_ARISTA arista, SIT_RED_NODO nodo "
        //////        + " where arista.solclave = :P0 and nodo.nodclave = arista.noddestino "
        //////        + " and rtpclave = :P1 "
        //////        + " group by nodo.nodclave ) and arista.rtpclave = :P2 ";

        //////    return CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML(sqlQuery, dicParam[DButil.SIT_RED_ARISTA_COL.solclave],
        //////        dicParam[PARAM_rtpclave_PRORROGA], dicParam[PARAM_rtpclave_SIN_RESPUESTA]));
        //////}



        public DataTable dmlSelectAreasFaltantesCorreos(Int64 iClaFolio)
        {
            String sqlQuery = " SELECT usrNOMBRE || ' ' || usrPATERNO || ' ' || usrMATERNO as NOMBRE, usrCORREO " +
            " FROM SIT_adm_areahist AREA, SIT_RED_NODO NODO, SIT_ADM_USUARIO USU " +
            " WHERE NODO.solclave = :P0 " +
            " AND nodo.nodatendido = 0 " +
            " AND AREA.ARACLAVE = NODO.ARACLAVE " +
            " and USU.ARACLAVE_ORIGEN = NODO.ARACLAVE order by DESCRIPCION ";

            return ConsultaDML(sqlQuery, iClaFolio);
        }

        //////public SIT_RED_ARISTA dmlSelectAristaNodoDestino(Object oDatos)
        //////{
        //////    Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

        //////    String sqlQuery = "  "
        //////        + " select * from SIT_RED_ARISTA where solclave = :P0 and   "
        //////        + "  noddestino = :P1 and rtpclave = :P2 ";

        //////    List<SIT_RED_ARISTA> lstDatos = CrearListaMDL<SIT_RED_ARISTA>(
        //////        ConsultaDML(sqlQuery, dicParam[DButil.SIT_RED_ARISTA_COL.solclave], dicParam[DButil.SIT_RED_ARISTA_COL.noddestino],
        //////        dicParam[DButil.SIT_RED_ARISTA_COL.rtpclave]));

        //////    if (lstDatos.Count == 0)
        //////        return null;
        //////    else
        //////    {
        //////        return lstDatos[0];
        //////    }
        //////}

        //////public SIT_RED_ARISTA dmlSelectAristaNodoOrigen(Object oDatos)
        //////{
        //////    Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;

        //////    String sqlQuery = " select * from SIT_RED_ARISTA where solclave = :P0 AND nodorigen = :P1 ";
        //////    List<SIT_RED_ARISTA> lstDatos = CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML(sqlQuery, dicParam[DButil.SIT_RED_ARISTA_COL.solclave], dicParam[DButil.SIT_RED_ARISTA_COL.nodorigen]));
        //////    if (lstDatos.Count == 0)
        //////        return null;
        //////    else
        //////    {
        //////        return lstDatos[0];
        //////    }
        //////}


        ////////public DataTable dmlSelectAristaPendientes(Object oDatos)
        ////////{
        ////////    Dictionary<string, Object> dicParam = (Dictionary<string, Object>)oDatos;


        ////////    String sqlQuery = " SELECT 1 as Respuesta, ari.noddestino, TO_CHAR(arifecenvio, 'DD/MM/YYYY HH:MI') as Fecha, area.arhSiglas, tipari.rtpclave, tipari.rtpdescripcion,"
        ////////    + " doc.docclave, doc.docfecha, doc.docnombre, ari.ariclave, arimensaje, ' ' as Estado,  nod.nodcapa "
        ////////    + " FROM  SIT_RESP_TIPO tipari, SIT_adm_areahist area, SIT_RED_NODO nod, SIT_RED_ARISTA ari "
        ////////    + " LEFT JOIN SIT_DOC_ARISTA docari ON docari.ariclave = ari.ariclave and docari.solclave = ari.solclave "
        ////////    + " LEFT JOIN SIT_DOC_DOCUMENTO doc ON doc.docclave = docari.docclave "
        ////////    + " WHERE ari.solclave = :P0 "
        ////////    + " AND ari.noddestino = :P1 "
        ////////    + " AND ari.rtpclave = tipari.rtpclave "
        ////////    + " AND nod.nodclave = ari.nodorigen "
        ////////    + " AND nod.araClave = area.araClave "
        ////////    + " AND :P2 BETWEEN arhfecini and arhfecfin "
        ////////    + " UNION ALL "
        ////////    + " SELECT 0 as Respuesta, nodoO.nodclave, TO_CHAR(nodfeccreacion, 'DD/MM/YYYY HH:MI') as Fecha, areaO.arhSiglas, null, null,  "
        ////////    + " null,  null, null, null, null, ' ' as Estado,  nodoO.nodcapa "
        ////////    + " FROM SIT_RED_NODO nodoO, SIT_adm_areahist areaO "
        ////////    + " WHERE nodoO.solclave = :P3  AND nodoO.nodatendido = 0 AND areaO.ARACLAVE = nodoO.ARACLAVE "
        ////////    + " AND :P4 BETWEEN arhfecini and arhfecfin "
        ////////    + " AND nodoO.prcclave = 1 "
        ////////    + " AND nodoO.nedclave not in (" + dicParam[PARAM_NO_ESTADOS] + " ) "
        ////////    + " AND nodoO.nodclave <> :P5 ";

        ////////    return ConsultaDML(sqlQuery, dicParam[DButil.SIT_RED_ARISTA_COL.solclave], dicParam[DButil.SIT_RED_NODO_COL.nodclave], dicParam[PARAM_FECHA],
        ////////        dicParam[DButil.SIT_RED_ARISTA_COL.solclave], dicParam[PARAM_FECHA], dicParam[DButil.SIT_RED_NODO_COL.nodclave]);
        ////////}


        //////public List<SIT_RED_ARISTA> dmlSelectAristaCIpendientesNOprorroga(Object oDatos)
        //////{
        //////    // el primer tipo de arista es para obtener todos las pendients..SIN_RESPUESTA = 0;
        //////    // el seugndi tipo de arita es para encontrado todo aquello qu eno este en PRORROGA
        //////    // y que el área corresponda al COMITE DE INFOMRACION (4)

        //////    Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
        //////    String sqlQuery = " select arista.solclave, nodorigen, noddestino, usrClave, ariclave, NRE_FECINI, NRE_FECFIN, arimensaje, "
        //////        + " aridiaslab, rtpclave, NRE_FECLECTURA, aridiasnat, arihito,megclave, NRE_FECLECUSU "
        //////        + " from SIT_RED_ARISTA arista, SIT_red_nodo nodo "
        //////        + " where arista.solclave = :P0 and rtpclave = :P1 and nodorigen not "
        //////        + " in (Select noddestino from SIT_RED_ARISTA where solclave = :P2 and rtpclave = :P3 ) "
        //////        + " and arista.solclave = nodo.solclave "
        //////        + " and nodo.nodclave = arista.nodorigen "
        //////        + " and ARACLAVE = :P4 ";

        //////    return CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML(sqlQuery, dicParametro[DButil.SIT_RED_ARISTA_COL.solclave], dicParametro[PARAM_rtpclave_SIN_RESPUESTA],
        //////        dicParametro[DButil.SIT_RED_ARISTA_COL.solclave], dicParametro[PARAM_rtpclave_PRORROGA], dicParametro[DButil.SIT_ADM_AREA_COL.ARACLAVE]));
        //////}

        public SIT_RED_ARISTA dmlSelectAristaPorAristaOrigen(Object oDatos)
        {
            Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
            String sqlQuery = " Select solclave, nodorigen, noddestino, usrClave, ariclave, NRE_FECINI, NRE_FECFIN, arimensaje, "
                + " aridiaslab, rtpclave, NRE_FECLECTURA, aridiasnat, arihito, megclave, NRE_FECLECUSU FROM SIT_red_arista where ariclave =  "
                + " (Select ARJ_DESTINO FROM SIT_red_arista_flujo WHERE arj_origen = :P0 ) ";

            return CrearListaMDL<SIT_RED_ARISTA>(ConsultaDML(sqlQuery, dicParametro[PARAM_ARJ_ORIGEN]))[0];
        }

        //////public DataTable dmlSelectAristaFolioTipoAristaTrans(Object oDatos)
        //////{
        //////    Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;
        //////    String sqlQuery = " Select solclave, nodorigen, noddestino, usrClave, ariclave, NRE_FECINI, NRE_FECFIN, "
        //////        + " arimensaje, aridiaslab, rtpclave, NRE_FECLECTURA, aridiasnat FROM SIT_red_arista  "
        //////        + " where solclave = :P0 and rtpclave = :P1 ";

        //////    DataTable catalogoRet = new DataTable();
        //////    DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[DButil.SIT_RED_ARISTA_COL.solclave], dicParametro[DButil.SIT_RED_ARISTA_COL.rtpclave]);
        //////    catalogoRet.Columns.Add("titulo", typeof(string));
        //////    catalogoRet.Columns.Add("valor", typeof(string));

        //////    foreach (DataRow row in dtDatos.Rows)
        //////    {
        //////        catalogoRet.Rows.Add("Fecha recepción", ((DateTime)row["NRE_FECINI"]).ToString("dd/MM/yyyy"));
        //////        catalogoRet.Rows.Add("Info. adicional", row["arimensaje"].ToString());
        //////    }
        //////    return catalogoRet;
        //////}

        ////////public DataTable dmlSelectRespDoc(Object oDatos)
        ////////{
        ////////    Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

        ////////    String sqlQuery = " SELECT rtpdescripcion, doc.docclave, arimensaje, docnombre, tipo.rtpclave "
        ////////         + " FROM SIT_SOL_SEGUIMIENTO seg, SIT_RESP_TIPO tipo, sit_red_arista ari "
        ////////         + " LEFT JOIN SIT_DOC_ARISTA aridoc ON aridoc.ariclave = ari.ariclave "
        ////////         + " LEFT JOIN SIT_DOC_DOCUMENTO doc ON doc.docclave = aridoc.docclave "
        ////////         + " WHERE ari.solclave = :P0 "
        ////////         + " AND ari.ariclave = :P1 AND seg.prcclave = :P2 "
        ////////         + " AND seg.solclave = ari.solclave "
        ////////         + " AND tipo.rtpclave = seg.rtpclave ";

        ////////    return ConsultaDML(sqlQuery, dicParametro[DButil.SIT_RED_ARISTA_COL.solclave], dicParametro[DButilSIT_RED_ARISTA_COL.ARICLAVE],
        ////////        dicParametro[DButil.SIT_RED_ARISTA_COL.prcclave]);
        ////////}

        //////public DataTable dmlSelectRespDocAristaAnt(Object oDatos)
        //////{
        //////    Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

        //////    String sqlQuery = " SELECT rtpdescripcion, doc.docclave, arimensaje, docnombre, tipo.rtpclave "
        //////         + " FROM SIT_SOL_SEGUIMIENTO seg, SIT_RESP_TIPO tipo, sit_red_arista ari "
        //////         + " LEFT JOIN SIT_DOC_ARISTA aridoc ON aridoc.ariclave = ari.ariclave "
        //////         + " LEFT JOIN SIT_DOC_DOCUMENTO doc ON doc.docclave = aridoc.docclave "
        //////         + " WHERE ari.solclave = :P0 AND ari.ariclave = :P1 "
        //////         + " AND seg.solclave = ari.solclave "
        //////         + " AND prcclave = :P2 "
        //////         + " AND tipo.rtpclave = ari.rtpclave ";

        //////    return ConsultaDML(sqlQuery, dicParametro[DButil.SIT_RED_ARISTA_COL.solclave], dicParametro[DButilSIT_RED_ARISTA_COL.ARICLAVE],
        //////        dicParametro[DButil.SIT_RED_ARISTA_COL.prcclave]);
        //////}

        ////public Dictionary<int, string> dmlSelectDicInfoAdicResp(Object oDatos)
        ////{
        ////    Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

        ////    String sqlQuery = " select 1, arimensaje from SIT_RED_ARISTA ari "
        ////         + "  where ari.solclave = :P0 "
        ////         + "  and rtpclave = :P1 "
        ////         + "  UNION "
        ////         + "  select 2, arimensaje from SIT_RED_ARISTA ari "
        ////         + "  where ari.solclave = :P2 "
        ////         + "  and rtpclave = :P3 ";

        ////    DataTable dtDatos = ConsultaDML(sqlQuery, dicParametro[DButil.SIT_RED_ARISTA_COL.solclave], dicParametro[PARAM_ARITIPO_INFOADI],
        ////        dicParametro[DButil.SIT_RED_ARISTA_COL.solclave], dicParametro[PARAM_ARITIPO_INFOADI_RESP]);

        ////    Dictionary<int, string> dicRes = new Dictionary<int, string>();

        ////    for (int iIdx = 0; iIdx < dtDatos.Rows.Count; iIdx++)
        ////    {
        ////        dicRes.Add(Convert.ToInt32(dtDatos.Rows[iIdx][0]), dtDatos.Rows[iIdx][1].ToString());
        ////    }
        ////    return dicRes;
        ////}



        ////public object dmlSelectAristaAccionPreviaArea(Object oDatos)
        ////{
        ////    Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

        ////    String sqlQuery = " select count(*) "
        ////         + " from sit_red_arista arista, sit_red_nodo nodo  "
        ////         + " where arista.solclave = :P0 "
        ////         + " AND arista.nodorigen = nodo.nodclave "
        ////         + " AND nodo.ARACLAVE in (select ARACLAVE from sit_red_nodo where nodclave = :P1 ) "
        ////         + " AND rtpclave = :P2 ";

        ////    DataTable dtResultado = ConsultaDML(sqlQuery, dicParametro[DButil.SIT_RED_ARISTA_COL.solclave],
        ////        dicParametro[DButil.SIT_RED_NODO_COL.nodclave], dicParametro[DButil.SIT_RED_ARISTA_COL.rtpclave]);

        ////    return Convert.ToInt32(dtResultado.Rows[0].ItemArray[0]);
        ////}


        ////public DataTable dmlSelectRespPrevRecRevision(Object oDatos)
        ////{

        ////    Dictionary<string, Object> dicParametro = (Dictionary<string, Object>)oDatos;

        ////    ////String sqlQuery = "  WITH arista_resp AS "
        ////    ////+ " (select * from sit_red_arista where noddestino in   "
        ////    ////+ " (select nodorigen from sit_red_arista where ariclave in  "
        ////    ////+ "     (select  ariclave from sit_sol_seguimiento where solclave = :P0 and prcclave = :P1) ) "
        ////    ////+ " UNION "
        ////    ////+ " select* from sit_red_arista where ariclave in (select  ariclave from sit_sol_seguimiento where solclave = :P2 and prcclave = :P3)  "
        ////    ////+ " ) "
        ////    ////+ " SELECT 1 as Respuesta, nodoO.nodclave, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla,  tipari.rtpclave,  "
        ////    ////+ " tipari.rtpdescripcion, doc.docclave, doc.docfecha, doc.docnombre, ari.ariclave, arimensaje, ' ' as Estado,  ari.ariclave  "
        ////    ////+ " FROM  SIT_RESP_TIPO tipari, SIT_adm_areahist areaO, SIT_RED_NODO nodoO, arista_resp ari "
        ////    ////+ " LEFT JOIN SIT_DOC_ARISTA docari ON docari.ariclave = ari.ariclave and docari.solclave = ari.solclave "
        ////    ////+ " LEFT JOIN SIT_DOC_DOCUMENTO doc ON doc.docclave = docari.docclave "
        ////    ////+ " WHERE ari.solclave = :P4 "
        ////    ////+ " AND ari.rtpclave = tipari.rtpclave "
        ////    ////+ " AND nodoO.nodclave = ari.nodorigen "
        ////    ////+ " AND areaO.ARACLAVE = nodoO.ARACLAVE "
        ////    ////+ " ORDER BY ari.ariclave DESC ";


        ////    String sqlQuery = " SELECT 1 as Respuesta, nodoO.nodclave, TO_CHAR(NRE_FECFIN, 'DD/MM/YYYY HH:MI') as Fecha, areaO.ka_sigla,  tipari.rtpclave,    "
        ////        + " tipari.rtpdescripcion, doc.docclave, doc.docfecha, doc.docnombre, ari.ariclave, arimensaje, ' ' as Estado,  ari.ariclave "
        ////        + " FROM SIT_RESP_TIPO tipari, SIT_adm_areahist areaO, SIT_RED_NODO nodoO, sit_red_arista ari "
        ////        + " LEFT JOIN SIT_DOC_ARISTA docari ON docari.ariclave = ari.ariclave and docari.solclave = ari.solclave "
        ////        + " LEFT JOIN SIT_DOC_DOCUMENTO doc ON doc.docclave = docari.docclave "
        ////        + " where arihito = " + Constantes.RespuestaHito.SI + " and ari.solclave = :P0 "
        ////        + " and nodoO.prcclave = :P1 "
        ////        + " AND ari.rtpclave = tipari.rtpclave  AND nodoO.nodclave = ari.nodorigen "
        ////        + " AND areaO.ARACLAVE = nodoO.ARACLAVE "
        ////        + " ORDER BY ari.ariclave DESC ";

        ////    return ConsultaDML(sqlQuery, dicParametro[DButil.SIT_RED_ARISTA_COL.solclave], dicParametro[DButil.SIT_RED_ARISTA_COL.prcclave]);
        ////}

        /*FIN*/
 
	 }
}
