using System.Data.Common;
using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Snt;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Snt
{
    public class SntSolicitanteDao : BaseDao
    {

        public const int OPE_SELECT_VALIDAR_USUARIO = 211;

        public const string COL_CLAUSU = "US_CLAUSU";
        public const string COL_CLAFOLIO = "US_CLAFOLIO";

        public const int OPE_SELECT_SOLICITANTE_POR_ID = 211;
        public const int OPE_SELECT_SOLICITANTE_TRANSPUESTA_POR_ID = 212;

        int iSecuencia { get; set; }

        public SntSolicitanteDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectHashMap);
            dicOperacion[OPE_SELECT_SOLICITANTE_POR_ID] = new Func<Object, object>(dmlSelectSolicitantePorID);
            dicOperacion[OPE_SELECT_SOLICITANTE_TRANSPUESTA_POR_ID] = new Func<Object, object>(dmlSelectSolicitanteTranspuestoPorID);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                              
        private Object dmlInsert(Object oDatos)
        {
            SntSolicitanteMdl dtoDatos = (SntSolicitanteMdl)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_SNT_SOLICITANTE (  "
                    + " US_CLAUSU, US_RFC, US_APEPAT, US_APEMAT, US_NOMBRE, "
                    + " US_CURP, US_CALLE, US_NUMEXT, US_NUMINT, US_COL,"
                    + " US_CODPOS, US_TEL, US_CORELE, US_EDOEXT, US_CIUDADEXT, "
                    + " US_SEXO, US_FECNAC, US_USUARIO, KPA_CLAPAI, KE_CLAEST, "
                    + " KMU_CLAMUN, US_REPLEG, NIVEDU_CLAVE, US_OTRAOCUPACION, US_OTRONIVELEDUC,  "
                    + " tsl_clatiposolte, US_OCUPACION ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, "
                    + " :P7, :P8, :P9, :P10, :P11, :P12, :P13,  "
                    + " :P14, :P15, :P16, :P17, :P18, :P19, :P20,  "
                    + " :P21, :P22, :P23, :P24, :P25, :P26 ) ";

            return EjecutaDML(sqlQuery,
                    dtoDatos.US_CLAUSU, dtoDatos.US_RFC, dtoDatos.US_APEPAT, dtoDatos.US_APEMAT, dtoDatos.US_NOMBRE,
                    dtoDatos.US_CURP, dtoDatos.US_CALLE, dtoDatos.US_NUMEXT, dtoDatos.US_NUMINT, dtoDatos.US_COL,
                    dtoDatos.US_CODPOS, dtoDatos.US_TEL, dtoDatos.US_CORELE, dtoDatos.US_EDOEXT, dtoDatos.US_CIUDADEXT,
                    dtoDatos.US_SEXO, dtoDatos.US_FECNAC, dtoDatos.US_USUARIO, dtoDatos.KPA_CLAPAI, dtoDatos.KE_CLAEST,
                    dtoDatos.KMU_CLAMUN, dtoDatos.US_REPLEG, dtoDatos.NIVEDU_CLAVE, dtoDatos.US_OTRAOCUPACION, dtoDatos.US_OTRONIVELEDUC,
                    dtoDatos.TSL_CLATIPOSOLTE, dtoDatos.US_OCUPACION);
        }

        private Object dmlUpdate(Object oDatos)
        {
            SntSolicitanteMdl dtoDatos = (SntSolicitanteMdl)oDatos;
            String sqlQuery = " update SIT_SNT_SOLICITANTE set "
                    + " US_RFC = :P0, US_APEPAT = :P1, US_APEMAT = :P2, US_NOMBRE = :P3, "
                    + " US_CURP = :P4, US_CALLE = :P5, US_NUMEXT = :P6, US_NUMINT = :P7, US_COL = :P8,"
                    + " US_CODPOS= :P9, US_TEL = :P10, US_CORELE = :P11, US_EDOEXT = :P12, US_CIUDADEXT = :P13, "
                    + " US_SEXO = :P14, US_FECNAC = :P15, US_USUARIO = :P16, KPA_CLAPAI = :P17, KE_CLAEST = :P18, "
                    + " KMU_CLAMUN = :P19, US_REPLEG = :P20, NIVEDU_CLAVE = :P21, US_OTRAOCUPACION = :P22, US_OTRONIVELEDUC = :P23,  "
                    + " tsl_clatiposolte = :P24, US_OCUPACION = :P25 "
                    + "  WHERE US_CLAUSU = :P26 ";

            return EjecutaDML(sqlQuery,
                    dtoDatos.US_RFC, dtoDatos.US_APEPAT, dtoDatos.US_APEMAT, dtoDatos.US_NOMBRE,
                    dtoDatos.US_CURP, dtoDatos.US_CALLE, dtoDatos.US_NUMEXT, dtoDatos.US_NUMINT, dtoDatos.US_COL,
                    dtoDatos.US_CODPOS, dtoDatos.US_TEL, dtoDatos.US_CORELE, dtoDatos.US_EDOEXT, dtoDatos.US_CIUDADEXT,
                    dtoDatos.US_SEXO, dtoDatos.US_FECNAC, dtoDatos.US_USUARIO, dtoDatos.KPA_CLAPAI, dtoDatos.KE_CLAEST,
                    dtoDatos.KMU_CLAMUN, dtoDatos.US_REPLEG, dtoDatos.NIVEDU_CLAVE, dtoDatos.US_OTRAOCUPACION, dtoDatos.US_OTRONIVELEDUC,
                    dtoDatos.TSL_CLATIPOSOLTE, dtoDatos.US_OCUPACION, dtoDatos.US_CLAUSU);
        }

        private Object dmlDelete(Object oDatos)
        {
            SntSolicitanteMdl dtoDatos = (SntSolicitanteMdl)oDatos;
            // se requiere un borrado en cascada...
            String sqlQuery = " delete from SIT_SNT_SOLICITANTE where US_CLAUSU = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.US_CLAUSU);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<SntSolicitanteMdl> lstDatos = (List<SntSolicitanteMdl>)oDatos;

            String sqlQuery = ""
                    + " insert into SIT_SNT_SOLICITANTE (  "
                    + " US_CLAUSU, US_RFC, US_APEPAT, US_APEMAT, US_NOMBRE, "
                    + " US_CURP, US_CALLE, US_NUMEXT, US_NUMINT, US_COL,"
                    + " US_CODPOS, US_TEL, US_CORELE, US_EDOEXT, US_CIUDADEXT, "
                    + " US_SEXO, US_FECNAC, US_USUARIO, KPA_CLAPAI, KE_CLAEST, "
                    + " KMU_CLAMUN, US_REPLEG, NIVEDU_CLAVE, US_OTRAOCUPACION, US_OTRONIVELEDUC,  "
                    + " tsl_clatiposolte, US_OCUPACION ) "
                    + " VALUES ( :P0, :P1, :P2, :P3, :P4, :P5, :P6, "
                    + " :P7, :P8, :P9, :P10, :P11, :P12, :P13,  "
                    + " :P14, :P15, :P16, :P17, :P18, :P19, :P20,  "
                    + " :P21, :P22, :P23, :P24, :P25, :P26 ) ";

            foreach (SntSolicitanteMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery,
                    iSecuencia, dtoDatos.US_RFC, dtoDatos.US_APEPAT, dtoDatos.US_APEMAT, dtoDatos.US_NOMBRE,
                    dtoDatos.US_CURP, dtoDatos.US_CALLE, dtoDatos.US_NUMEXT, dtoDatos.US_NUMINT, dtoDatos.US_COL,
                    dtoDatos.US_CODPOS, dtoDatos.US_TEL, dtoDatos.US_CORELE, dtoDatos.US_EDOEXT, dtoDatos.US_CIUDADEXT,
                    dtoDatos.US_SEXO, dtoDatos.US_FECNAC, dtoDatos.US_USUARIO, dtoDatos.KPA_CLAPAI, dtoDatos.KE_CLAEST,
                    dtoDatos.KMU_CLAMUN, dtoDatos.US_REPLEG, dtoDatos.NIVEDU_CLAVE, dtoDatos.US_OTRAOCUPACION, dtoDatos.US_OTRONIVELEDUC,
                    dtoDatos.TSL_CLATIPOSOLTE, dtoDatos.US_OCUPACION);
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
                            + " SELECT US_CLAUSU, US_RFC, US_APEPAT, US_APEMAT, US_NOMBRE, US_CURP, US_CALLE, "
                    + " US_NUMEXT, US_NUMINT, US_COL, US_CODPOS, US_TEL, US_CORELE, US_EDOEXT, US_CIUDADEXT, "
                    + " US_SEXO, US_FECNAC, US_USUARIO, PAIS.KPA_CLAPAI, KPA_DESCRIPCION,  "
                    + " SNT.KE_CLAEST, KE_DESCRIPCION,  SNT.KMU_CLAMUN, KMU_DESCRIPCION, US_REPLEG, "
                    + " NIVEDU_CLAVE, US_OTRAOCUPACION, US_OTRONIVELEDUC, SNT.tsl_clatiposolte, TSL_DESCRIPCION,  SNT.US_OCUPACION, OCU_DESCRIPCION "
                    + " FROM SIT_SNT_SOLICITANTE SNT, SIT_SNT_KMUNICIPIO MUN,  SIT_SNT_KPAIS PAIS, SIT_SNT_KESTADO EDO,  "
                    + " SIT_SNT_KOCUPACION OCU, SIT_SNT_KTIPO_SOLICITANTE TSNT "
                    + " WHERE PAIS.KPA_CLAPAI = SNT.KPA_CLAPAI AND EDO.KE_CLAEST = SNT.KE_CLAEST"
                    + " AND MUN.KMU_CLAMUN = SNT.KMU_CLAMUN AND OCU.US_OCUPACION = SNT.US_OCUPACION"
                    + " AND TSNT.tsl_clatiposolte = SNT.tsl_clatiposolte "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        private Dictionary<int, SntSolicitanteMdl> dmlSelectHashMap(Object oDatos)
        {
            DataTable dtDatos;
            Dictionary<int, SntSolicitanteMdl> catalogoRet = new Dictionary<int, SntSolicitanteMdl>();
            int iIdx = 1;

            String sqlQuery = "SELECT US_CLAUSU, US_RFC, US_APEPAT, US_APEMAT, US_NOMBRE, "
                + " US_CURP, US_CALLE, US_NUMEXT, US_NUMINT, US_COL, "
                + " US_CODPOS, US_TEL, US_CORELE, US_EDOEXT, US_CIUDADEXT,"
                + " US_SEXO, US_FECNAC, US_USUARIO, KPA_CLAPAI, KE_CLAEST, "
                + " KMU_CLAMUN, US_REPLEG, NIVEDU_CLAVE, US_OTRAOCUPACION, US_OTRONIVELEDUC, "
                + "  tsl_clatiposolte, US_OCUPACION"
                + " from SIT_SNT_SOLICITANTE ";

            SntSolicitanteMdl dtoDatos;
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)       
            {
                dtoDatos = new SntSolicitanteMdl(
                    Convert.ToInt64(row["US_CLAUSU"]),      row["US_RFC"].ToString(),           row["US_APEPAT"].ToString(),        row["US_APEMAT"].ToString(),
                    row["US_NOMBRE"].ToString(),            row["US_CURP"].ToString(),          row["US_CALLE"].ToString(),         row["US_NUMEXT"].ToString(),
                    row["US_NUMINT"].ToString(),            row["US_COL"].ToString(),           row["US_CODPOS"].ToString(),        row["US_TEL"].ToString(),
                    row["US_CORELE"].ToString(),            row["US_EDOEXT"].ToString(),        row["US_CIUDADEXT"].ToString(),     row["US_SEXO"].ToString(),
                    Convert.ToDateTime(row["US_FECNAC"]),   row["US_USUARIO"].ToString(),       Convert.ToInt32(row["KPA_CLAPAI"]), Convert.ToInt32(row["KE_CLAEST"]),
                    Convert.ToInt32(row["KMU_CLAMUN"]),     row["US_REPLEG"].ToString(),        row["NIVEDU_CLAVE"].ToString(),     row["US_OTRAOCUPACION"].ToString(),
                    row["US_OTRONIVELEDUC"].ToString(),     Convert.ToInt32("tsl_clatiposolte"),Convert.ToInt32("US_OCUPACION"));
                catalogoRet.Add(iIdx, dtoDatos);
            }
            return catalogoRet;
        }

        private DataTable dmlSelectSolicitantePorID(Object oDatos)
        {
            Dictionary<string, Object> dicParametros = (Dictionary<string, Object>)oDatos;

            String sqlQuery = "SELECT A.US_CLAUSU, US_RFC, US_APEPAT, US_APEMAT, US_NOMBRE, "
                + " US_CURP, US_CALLE, US_NUMEXT, US_NUMINT, US_COL, "
                + " US_CODPOS, US_TEL, US_CORELE, US_EDOEXT, US_CIUDADEXT,"
                + " US_SEXO, US_FECNAC, US_USUARIO, KPA_CLAPAI, KE_CLAEST, "
                + " KMU_CLAMUN, US_REPLEG, NIVEDU_CLAVE, US_OTRAOCUPACION, US_OTRONIVELEDUC, "
                + "  tsl_clatiposolte, US_OCUPACION "
                + " from SIT_SNT_SOLICITANTE A, SIT_SOLICITUD B "
                + " where A.US_CLAUSU  =  B.US_CLAUSU AND B.US_CLAFOLIO = :P0 ";

            return ConsultaDML(sqlQuery, Convert.ToInt64(dicParametros[COL_CLAFOLIO]));
        }

        private DataTable dmlSelectSolicitanteTranspuestoPorID(Object oDatos)
        {
            Dictionary<string, Object> dicParametros = (Dictionary<string, Object>)oDatos;
           
            DataTable catalogoRet = new DataTable();
            catalogoRet.Columns.Add("titulo", typeof(string));
            catalogoRet.Columns.Add("valor", typeof(string));
            object oValue;

            String sqlQuery = " SELECT A.US_CLAUSU, US_RFC, US_APEPAT, US_APEMAT, US_NOMBRE, "
               + " US_CURP, US_CALLE, US_NUMEXT, US_NUMINT, US_COL, "
               + " US_CODPOS, US_TEL, US_CORELE, US_EDOEXT, US_CIUDADEXT,"
               + " US_SEXO, US_FECNAC, US_USUARIO, P.KPA_CLAPAI, KPA_DESCRIPCION, KE_DESCRIPCION, "
               + " KMU_DESCRIPCION, US_REPLEG, NIVEDU_CLAVE, US_OTRAOCUPACION, US_OTRONIVELEDUC, "
               + " TSL_DESCRIPCION, OCU_DESCRIPCION "
               + " from SIT_SNT_SOLICITANTE A, SIT_SOLICITUD B, "
               + " SIT_SNT_KPAIS P, SIT_SNT_KESTADO E, SIT_SNT_KMUNICIPIO M,"
               + " SIT_SNT_KTIPO_SOLICITANTE S, SIT_SNT_KOCUPACION O "
               + " where A.US_CLAUSU  =  B.US_CLAUSU AND B.US_CLAFOLIO = :P0 "
               + " AND A.KPA_CLAPAI = P.KPA_CLAPAI AND  A.KE_CLAEST = E.KE_CLAEST AND  A.KMU_CLAMUN = M.KMU_CLAMUN "
               + " AND A.tsl_clatiposolte = S.tsl_clatiposolte AND A.US_OCUPACION = O.US_OCUPACION ";

            DataTable dtDatos = ConsultaDML(sqlQuery, Convert.ToInt64(dicParametros[COL_CLAFOLIO]));
            foreach (DataRow row in dtDatos.Rows)
            {
                catalogoRet.Rows.Add("Solicitante", row["TSL_DESCRIPCION"].ToString());
                catalogoRet.Rows.Add("Nombre", row["US_NOMBRE"].ToString());
                catalogoRet.Rows.Add("Paterno", row["US_APEPAT"].ToString());
                catalogoRet.Rows.Add("Materno", row["US_APEMAT"].ToString());
                catalogoRet.Rows.Add("Sexo", row["US_SEXO"].ToString());

                oValue = row["US_FECNAC"];
                if (oValue == DBNull.Value)
                    catalogoRet.Rows.Add("Fec. Nacim.", "");
                else
                    catalogoRet.Rows.Add("Fec. Nacim.", Convert.ToDateTime(row["US_FECNAC"]).ToShortDateString());

                catalogoRet.Rows.Add("RFC", row["US_RFC"].ToString());
                catalogoRet.Rows.Add("CURP", (row["US_CURP"].ToString()));
                catalogoRet.Rows.Add("Rep. Legal", row["US_REPLEG"].ToString());
                catalogoRet.Rows.Add("Ocupación", (row["OCU_DESCRIPCION"].ToString()));

                // Mexico
                oValue = row["KPA_CLAPAI"];
                if (oValue == DBNull.Value)
                {
                    catalogoRet.Rows.Add("País", "");
                    catalogoRet.Rows.Add("Estado", "");
                    catalogoRet.Rows.Add("Municipio", "");
                }
                else
                {
                    if (Convert.ToInt32(row["KPA_CLAPAI"]) != 131)
                    {
                        catalogoRet.Rows.Add("País", (row["US_EDOEXT"].ToString()));
                        catalogoRet.Rows.Add("Ciudad", (row["US_CIUDADEXT"].ToString()));
                    }
                    else
                    {
                        catalogoRet.Rows.Add("País", (row["KPA_DESCRIPCION"].ToString()));
                        catalogoRet.Rows.Add("Estado", (row["KMU_DESCRIPCION"].ToString()));
                        catalogoRet.Rows.Add("Municipio", (row["TSL_DESCRIPCION"].ToString()));
                    }
                }

                String sCalle = row["US_CALLE"].ToString();
                String sInt = row["US_NUMEXT"].ToString();
                String sExt = row["US_NUMINT"].ToString();

                if (sCalle == null) { sCalle = ""; }
                if (sInt == null) { sInt = ""; }
                if (sExt == null) { sExt = ""; }

                catalogoRet.Rows.Add("Dirección", sCalle + " " + sInt + " " + sExt);
                catalogoRet.Rows.Add("Colonia", (row["US_COL"].ToString()));
                catalogoRet.Rows.Add("C.P.", (row["US_CODPOS"].ToString()));
                catalogoRet.Rows.Add("Tel.", (row["US_TEL"].ToString()));
                catalogoRet.Rows.Add("Correo Elec.", (row["US_CORELE"].ToString()));
            }
            return catalogoRet;
        }


        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}
