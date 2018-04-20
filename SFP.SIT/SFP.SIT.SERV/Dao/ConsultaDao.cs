using SFP.Persistencia.Dao;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Model._Consultas;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Dao
{
    public class ConsultaDao : BaseDao
    {
        public ConsultaDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
        {

        }

        public DataTable SelPerfilCatalogos(string sPerfiles)
        {
            // , claNombre 
            String sqlQuery = " SELECT AC.claClave as id, claDescripcion as text "
                    + " FROM SIT_Adm_Clases AC, SIT_Adm_PerfilClases APC  "
                    + " WHERE AC.claClave = APC.claClave "
                    + " AND APC.perClave in ( " + sPerfiles  + ") "
                    + " GROUP BY  AC.claClave, claDescripcion"
                    + " ORDER BY claDescripcion ";

            return ConsultaDML(sqlQuery);
        }

        //////public DataTable dmlSelectAreasActual(Dictionary<string, object> dicParam)
        //////{
        //////    String sSQL = " SELECT AP.PERCLAVE, AAH.ARACLAVE, AU.usrclave, ARHSIGLAS,  usrnombre || ' ' || usrpaterno || ' ' ||usrmaterno as NOMBRE, usrpuesto, ARHDESCRIPCION, PERSIGLA     "
        //////        + " FROM SIT_ADM_USUARIO AU, SIT_ADM_USUARIOAREA AUA, SIT_ADM_AREAHIST AAH, SIT_ADM_USRPERFIL AUP, SIT_ADM_PERFIL AP  "
        //////        + " WHERE  "
        //////        + " AU.usrclave = AUA.usrclave  "
        //////        + " AND AAH.araclave = AUA.araclave  "
        //////        + " AND AU.usrclave = AUP.usrclave  "
        //////        + " AND AUP.perClave = AP.perClave  "
        //////        + " AND AU.usrfecbaja is null  "
        //////        + " AND :P0 between arhfecini and arhfecfin "
        //////        + " AND arhReporta = :P1 "
        //////        + " UNION ALL"
        //////        + " SELECT AP.PERCLAVE, AAH.ARACLAVE, AU.usrclave, ARHSIGLAS,  usrnombre || ' ' ||usrpaterno || ' ' ||usrmaterno as NOMBRE, usrpuesto, ARHDESCRIPCION, PERSIGLA "
        //////        + " FROM SIT_ADM_USUARIO AU, SIT_ADM_USUARIOAREA AUA, SIT_ADM_AREAHIST AAH, SIT_ADM_USRPERFIL AUP, SIT_ADM_PERFIL AP  "
        //////        + " WHERE  "
        //////        + " AU.usrclave = AUA.usrclave  "
        //////        + " AND AAH.araclave = AUA.araclave  "
        //////        + " AND AU.usrclave = AUP.usrclave  "
        //////        + " AND AUP.perClave = AP.perClave "
        //////        + " AND AU.usrfecbaja is null "
        //////        + " AND :P2 between arhfecini and arhfecfin "
        //////        + " AND AP.PERCLAVE = " + Constantes.Perfil.UA
        //////        + " ORDER BY PERCLAVE, ARACLAVE, usrclave ";

        //////    return ConsultaDML(sSQL, dicParam[Constantes.Parametro.FECHA], dicParam[DButil.SIT_ADM_AREAHIST_COL.ARHREPORTA], dicParam[Constantes.Parametro.FECHA]);
        //////}

        public DataTable dmlSelectAreasActual(DateTime dtFecha)
        {
            String sSQL = " SELECT AP.PERCLAVE, AAH.ARACLAVE, AU.usrclave, ARHSIGLAS,  usrnombre || ' ' ||usrpaterno || ' ' ||usrmaterno as NOMBRE, usrpuesto, ARHDESCRIPCION, PERSIGLA "
                + " FROM SIT_ADM_USUARIO AU, SIT_ADM_USUARIOAREA AUA, SIT_ADM_AREAHIST AAH, SIT_ADM_USRPERFIL AUP, SIT_ADM_PERFIL AP  "
                + " WHERE  "
                + " AU.usrclave = AUA.usrclave  "
                + " AND AAH.araclave = AUA.araclave  "
                + " AND AU.usrclave = AUP.usrclave  "
                + " AND AUP.perClave = AP.perClave "
                + " AND AU.usrfecbaja is null "
                + " AND :P0 between arhfecini and arhfecfin "
                + " AND AP.PERCLAVE = " +  Constantes.Perfil.UA
                + " ORDER BY PERCLAVE, ARACLAVE, usrclave ";

            return ConsultaDML(sSQL, dtFecha);
        }

        public DataTable dmlSelectPersonasPerfil(Dictionary<string, object> dicParam)
        {
            String sSQL = " SELECT AU.usrclave as id,  usrnombre || ' ' || usrpaterno || ' ' ||usrmaterno as text, usrpuesto"
                    + " FROM SIT_ADM_USUARIO AU,  SIT_ADM_USRPERFIL AUP, SIT_ADM_PERFIL AP  "
                    + " WHERE  "
                    + " AU.usrclave = AUP.usrclave  "
                    + " AND AUP.perClave = AP.perClave  "
                    + " AND AU.usrfecbaja is null  "
                    + " AND AUP.PERCLAVE = :P0" ;

            return ConsultaDML(sSQL, dicParam[DButil.SIT_ADM_PERFIL_COL.PERCLAVE] );
        }

        public DataTable dmlSelectAreasActualReporta(Dictionary<string, object> dicParam)
        {
            String sSQL = " SELECT AU.usrclave as id,  usrnombre || ' ' || usrpaterno || ' ' ||usrmaterno as text, usrpuesto"
                    + " FROM SIT_ADM_USUARIO AU, SIT_ADM_USUARIOAREA AUA, SIT_ADM_AREAHIST AAH, SIT_ADM_USRPERFIL AUP, SIT_ADM_PERFIL AP  "
                    + " WHERE  "
                    + " AU.usrclave = AUA.usrclave  "
                    + " AND AAH.araclave = AUA.araclave  "
                    + " AND AU.usrclave = AUP.usrclave  "
                    + " AND AUP.perClave = AP.perClave  "
                    + " AND AU.usrfecbaja is null  "
                    + " AND :P0 between arhfecini and arhfecfin "
                    + " start with arhreporta = :P1  "
                    + " connect by prior AAH.araclave = arhreporta";

            return ConsultaDML(sSQL, dicParam[Constantes.Parametro.FECHA], dicParam[DButil.SIT_ADM_AREAHIST_COL.ARHREPORTA]);
        }


        // CONSULTA DE LAS RESPUESTAS

        public List<NodoRespuestaMdl> dmlSelectRespuestaNodo(long iNodoClave)
        {

            // ESTO ES UNA UNION DE LAS TABLAS QUE EXISTEN
            String sSQL = " SELECT R.repclave, R.rtpclave,  R.docclave, R.repoficio as dattitulo, GRAL.gracontenido as datdescripcion, rtpdescripcion, rtpforma "
                    + " FROM SIT_RED_NODORESP NR, SIT_RESP_GRAL GRAL, SIT_RESP_TIPO RT, SIT_RESP_RESPUESTA R "
                    + " LEFT JOIN SIT_DOC_DOCUMENTO DOC ON  DOC.docclave = R.docclave "
                    + " WHERE NR.nodClave = :P0 "
                    + " AND NR.sdoclave = 3 "
                    + " AND R.repclave = NR.repclave "
                    + " AND GRAL.repclave = NR.repclave"
                    + " AND RT.rtpclave = R.rtpclave "
                    + " UNION ALL"
                    + " SELECT R.repclave, R.rtpclave,  R.docclave, R.repoficio as dattitulo, 'Respuesta (Modo,tiempo,Lugar)' as datdescripcion, rtpdescripcion, rtpforma "
                    + " FROM SIT_RED_NODORESP NR, SIT_RESP_INEXISTENCIA INX, SIT_RESP_TIPO RT, SIT_RESP_RESPUESTA R LEFT JOIN SIT_DOC_DOCUMENTO DOC ON  DOC.docclave = R.docclave"
                    + " WHERE NR.nodClave = :P1  AND NR.sdoclave = 3  AND R.repclave = NR.repclave  AND INX.repclave = NR.repclave  AND RT.rtpclave = R.rtpclave"
                    + " UNION ALL"
                    + " SELECT R.repclave, R.rtpclave,  R.docclave, R.repoficio as dattitulo, 'Respuesta (Modo,tiempo,Lugar)' as datdescripcion, rtpdescripcion, rtpforma "
                    + " FROM SIT_RED_NODORESP NR, SIT_RESP_RESERVA RSV, SIT_RESP_TIPO RT, SIT_RESP_RESPUESTA R LEFT JOIN SIT_DOC_DOCUMENTO DOC ON  DOC.docclave = R.docclave"
                    + " WHERE NR.nodClave = :P2  AND NR.sdoclave = 3  AND R.repclave = NR.repclave  AND RSV.repclave = NR.repclave  AND RT.rtpclave = R.rtpclave "
                    + " UNION ALL "
                    + " SELECT R.repclave, R.rtpclave,  R.docclave, R.repoficio as dattitulo, '' as datdescripcion, rtpdescripcion, rtpforma "
                    + " FROM SIT_RED_NODORESP NR,  SIT_RESP_TIPO RT, SIT_RESP_RESPUESTA R LEFT JOIN SIT_DOC_DOCUMENTO DOC ON  DOC.docclave = R.docclave WHERE NR.nodClave = :P3 "
                    + " AND NR.sdoclave = 3 AND R.repclave = NR.repclave AND RT.rtpclave = R.rtpclave AND RT.rtpclave in ( " + Constantes.Respuesta.INFO_CONFIDENCIAL
                    + " ," + Constantes.Respuesta.INFO_CONFIDENCIAL_PARCIAL + ")";


            ////     + " AND RD.repclave = R.repclave "
            ////     + " AND ctpclave in (1, 2) "
            ////     + " AND rtptipo = 3"
            ////     + " ORDER BY R.repclave ";

            // REPCANTIDAD,
            // + " (SELECT megdescripcion FROM SIT_SOL_MODOENTREGA ME WHERE ME.megclave = R.megclave )as megdescripcion, rtpforma "

            return CrearListaMDL<NodoRespuestaMdl>(ConsultaDML(sSQL, iNodoClave, iNodoClave, iNodoClave, iNodoClave) as DataTable);
        }

        public DataTable dmlSelectRespuestaTipoDatos (long repClave)
        {
            String sSQL = " SELECT RR.repclave, RD.DETCLAVE, RD.DETDESCRIPCION, RD.DOCCLAVE "
                 + " FROM  SIT_RESP_RESPUESTA RR, SIT_RESP_DETALLE RD "
                 + " WHERE RR.repclave =  :P0 "
                 + " AND RD.repclave = RR.repclave ";

            return ConsultaDML(sSQL, repClave) as DataTable;
        }


        ////////// CONSULTA DE LAS RESPUESTAS
        ////////public DataTable dmlSelectNodoEstadoResp(Dictionary<string, object> dicParam)
        ////////{           
        ////////    String sSQL = " SELECT  AF.RTPCLAVE, RTPDESCRIPCION, RTPFORMA "
        ////////         + " FROM SIT_RESP_TIPO RT, SIT_RED_AFDFLUJO AF "
        ////////         + " WHERE AFFORIGEN = :P0  "
        ////////         + " AND RT.RTPCLAVE  = AF.RTPCLAVE "
        ////////         + " AND RTPTIPO = :P1 ";   

        ////////    return ConsultaDML(sSQL, dicParam[DButil.SIT_RED_AFDFLUJO_COL.AFFORIGEN],
        ////////        dicParam[DButil.SIT_RESP_TIPO_COL.RTPTIPO] ) as DataTable;
        ////////}


        public String dmlSelectInstrucciones(Dictionary<string, object> dicParam)
        {
            String sInstr = null;
            String sSQL = " SELECT TURINSTRUCCION FROM SIT_RESP_TURNAR "
                    + " WHERE REPCLAVE = (SELECT REPCLAVE FROM SIT_RED_NODORESP WHERE nodclave = :P0 AND SDOCLAVE = :P1 ) "
                    + " AND USRCLAVE = :P2 ";

            DataTable dtDatos = ConsultaDML(sSQL, dicParam[DButil.SIT_RED_NODO_COL.NODCLAVE], dicParam[DButil.SIT_RED_NODORESP_COL.SDOCLAVE],
                dicParam[DButil.SIT_RESP_TURNAR_COL.USRCLAVE]);

            if (dtDatos.Rows.Count > 0)
            {
                sInstr = dtDatos.Rows[0][0].ToString();
            }

            return sInstr;
        }

        /* 
         * CONSULTAS PARAS LAS RESPUESTAS
         */

        public List<SIT_RESP_RESPUESTA> dmlSelectRespEdo(SIT_RED_NODORESP nodoResp)
        {
            String sSQL = " select * from SIT_RESP_RESPUESTA WHERE repclave in ( select repclave from SIT_RED_NODORESP where nodclave = :P0 and sdoclave = :P1)	 ";

            return CrearListaMDL<SIT_RESP_RESPUESTA>(ConsultaDML(sSQL, nodoResp.nodclave, nodoResp.sdoclave));
        }

        
        public List<NodoRespDetalle> dmlSelectRespNodoAnterior(Int64 iNodo)
        {

            String sSQL = " SELECT AU.USRCLAVE, USRNOMBRE, USRPATERNO, USRMATERNO , ARHDESCRIPCION,  nodorigen,RR.RTPCLAVE, REPEDOFEC, REPOFICIO, DOCCLAVE, REPCANTIDAD, RTPDESCRIPCION, RTPFORMA, RR.repclave, NR.sdoClave, rn.nodclave     "
                + " FROM SIT_RED_NODO RN, SIT_ADM_USUARIO AU, SIT_ADM_AREAHIST AH, SIT_RED_ARISTA RA, SIT_RED_NODORESP NR, SIT_RESP_RESPUESTA RR, SIT_RESP_TIPO RT "
                + " WHERE RN.nodclave = nodorigen "
                + " AND RN.USRCLAVE = AU.USRCLAVE "
                + " AND RN.ARACLAVE = AH.ARACLAVE "
                + " AND :P0 BETWEEN arhfecini and arhfecfin "
                + " AND noddestino = :P1 "
                + " AND NR.nodclave = :P2"
                + " AND RR.repclave = NR.repclave "
                + " AND RT.RTPCLAVE = RR.RTPCLAVE "
                + " AND sdoclave not in (8) "
                + " UNION ALL "
                + " SELECT AU.USRCLAVE, USRNOMBRE, USRPATERNO, USRMATERNO , ARHDESCRIPCION, 0, 0, null, ' ', 0, 0, 'PENDIENTE', ' ', REPCLAVE, 0, rn.nodclave     "
                + " FROM SIT_RED_NODO RN, SIT_ADM_USUARIO AU, SIT_ADM_AREAHIST AH, SIT_RED_NODORESP NR "
                + " WHERE  RN.USRCLAVE = AU.USRCLAVE  AND RN.ARACLAVE = AH.ARACLAVE "
                + " AND :P3 BETWEEN arhfecini and arhfecfin "
                + " AND RN.nodclave in (SELECT nodDestino FROM SIT_RED_ARISTA RD, SIT_RED_NODO RN WHERE nodOrigen = :P4 and rn.nodclave = nodOrigen and nodatendido = 0) "
                + " AND NR.nodclave = RN.nodclave and SDOCLAVE = 3 "
                + " order by 1,2,3,4,5 ";


            DateTime dtDiaActual = DateTime.Now;
            return CrearListaMDL<NodoRespDetalle>(ConsultaDML(sSQL, dtDiaActual, iNodo, iNodo, dtDiaActual,  iNodo));
        }


        public DataTable dmlSelectRespuestaTranspuesta(Int64 repClave)
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta.Columns.Add("titulo", typeof(string));
            dtRespuesta.Columns.Add("valor", typeof(string));

            String sSQL = " SELECT R.RTPCLAVE, R.REPEDOFEC, ME.MEGDESCRIPCION,  R.REPOFICIO, R.DOCCLAVE, R.REPCANTIDAD,  RG.graContenido "
                + "  FROM SIT_RESP_GRAL RG,  SIT_RESP_RESPUESTA R "
                + "  LEFT JOIN SIT_SOL_MODOENTREGA ME ON ME.megclave = R.megclave  "
                + "  LEFT JOIN SIT_DOC_DOCUMENTO DOC ON DOC.docclave = R.docclave "
                + "  WHERE RG.REPCLAVE = R.REPCLAVE "
                + "  AND R.REPCLAVE = :P0 ";

            DataTable dtRes = ConsultaDML(sSQL, repClave);

            int iTipoRespuesta = 0;

            foreach (DataRow drDatos in dtRes.Rows)
            {
                dtRespuesta.Rows.Add("Fecha de respuesta", ((DateTime)drDatos["REPEDOFEC"]).ToString("dd/MM/yyyy"));
                dtRespuesta.Rows.Add("Modo de Entrega", drDatos["MEGDESCRIPCION"].ToString());
                dtRespuesta.Rows.Add("Documento", drDatos["REPOFICIO"].ToString());
                dtRespuesta.Rows.Add("DocClave", drDatos["DOCCLAVE"].ToString());
                dtRespuesta.Rows.Add("Cantidad", drDatos["REPCANTIDAD"].ToString());

                if (drDatos["graContenido"].ToString().Length > 0)
                {
                    dtRespuesta.Rows.Add("Descripcion", drDatos["graContenido"].ToString());
                }
                    
                iTipoRespuesta =  Convert.ToInt32(drDatos["RTPCLAVE"]);
            }

            if (iTipoRespuesta == Constantes.Respuesta.INEXISTENCIA_EN_AREA)
            {
                sSQL = " SELECT inxResponsable, inxCargo FROM SIT_RESP_INEXISTENCIA WHERE repClave = :P0 ";
                dtRes = ConsultaDML(sSQL, repClave);


                foreach (DataRow drDatos in dtRes.Rows)
                {
                    dtRespuesta.Rows.Add("Responsable", drDatos["inxResponsable"].ToString());
                    dtRespuesta.Rows.Add("Cargo", drDatos["inxCargo"].ToString());
                }
            }
            else if (iTipoRespuesta == Constantes.Respuesta.INFO_RESERVADA || iTipoRespuesta == Constantes.Respuesta.INFO_RESERVADA_PARCIAL)
            {
                sSQL = " select ArhDescripcion, rsvExpediente, rsvTipoReserva, rsvTipoClasif, rsvPlazo, rsvFecIni, rsvFecfin, sdoDescripcion "
                    + " from SIT_RESP_RESERVA RSV, SIT_ADM_AREAHIST AH, SIT_RESP_ESTADO EDO  "
                    + "  where repclave = :P0  "
                    + "  AND AH.araClave = RSV.araClave "
                    + "  AND :P1 between arhFecini amd arhFecfin "
                    + "  and EDO.sdoclave = RSV.sdoclave ";

                dtRes = ConsultaDML(sSQL, repClave, DateTime.Now);


                foreach (DataRow drDatos in dtRes.Rows)
                {
                    dtRespuesta.Rows.Add("Área", drDatos["ArhDescripcion"].ToString());
                    dtRespuesta.Rows.Add("Expediente", drDatos["rsvExpediente"].ToString());
                    dtRespuesta.Rows.Add("Tipo de Reserva", drDatos["rsvTipoReserva"].ToString());
                    dtRespuesta.Rows.Add("Tipo de Clasif.", drDatos["rsvTipoClasif"].ToString());
                    dtRespuesta.Rows.Add("Plazo", drDatos["rsvPlazo"].ToString());
                    dtRespuesta.Rows.Add("Fecha de inicial", ((DateTime)drDatos["rsvFecIni"]).ToString("dd/MM/yyyy"));
                    dtRespuesta.Rows.Add("Fecha de final", ((DateTime)drDatos["rsvFecfin"]).ToString("dd/MM/yyyy"));
                    dtRespuesta.Rows.Add("Descripcion", drDatos["sdoDescripcion"].ToString());                    
                }
            }

            sSQL = " Select detClave, detdescripcion, docNombre, RD.docClave  "
                + " FROM SIT_RESP_DETALLE RD LEFT JOIN SIT_DOC_DOCUMENTO DOC ON DOC.docclave = RD.docclave "
                + "  WHERE repclave = :P0 ";

            dtRes = ConsultaDML(sSQL, repClave);

            foreach (DataRow drDatos in dtRes.Rows)
            {
                dtRespuesta.Rows.Add(drDatos["detClave"].ToString(), drDatos["detdescripcion"].ToString());
                if (drDatos["docClave"].ToString() != "")
                {
                    dtRespuesta.Rows.Add("docClave", drDatos["docClave"].ToString());
                    dtRespuesta.Rows.Add("docNombre", drDatos["docNombre"].ToString());
                }
            }

            return dtRespuesta;
        }

    }
}

