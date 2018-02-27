using SFP.Persistencia.Dao;
using SFP.SIT.SERV.Dao.RESP;
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

    }
}

