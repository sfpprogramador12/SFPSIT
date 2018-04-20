using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.SNT;
 
namespace SFP.SIT.SERV.Dao.SNT
{
	 public class SIT_SNT_SOLICITANTEDao : BaseDao
	 {
	 	 public SIT_SNT_SOLICITANTEDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SNT_SOLICITANTE oDatos)
	 	 {
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_SOLICITANTE( paiclave, ocuclave, tslclave, sntotroniveledu, sntotraocup, sntniveduc, sntrepleg, munclave, edoclave, sntusuario, sntfecnac, sntsexo, sntciudadext, sntedoext, sntcorele, snttel, sntcodpos, sntcol, sntnumint, sntnumext, sntcalle, sntcurp, sntnombre, sntapemat, sntapepat, sntrfc, sntclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15, :P16, :P17, :P18, :P19, :P20, :P21, :P22, :P23, :P24, :P25, :P26) ";  
	 	 	  return EjecutaDML ( sSQL,  oDatos.paiclave, oDatos.ocuclave, oDatos.tslclave, oDatos.sntotroniveledu, oDatos.sntotraocup, oDatos.sntniveduc, oDatos.sntrepleg, oDatos.munclave, oDatos.edoclave, oDatos.sntusuario, oDatos.sntfecnac, oDatos.sntsexo, oDatos.sntciudadext, oDatos.sntedoext, oDatos.sntcorele, oDatos.snttel, oDatos.sntcodpos, oDatos.sntcol, oDatos.sntnumint, oDatos.sntnumext, oDatos.sntcalle, oDatos.sntcurp, oDatos.sntnombre, oDatos.sntapemat, oDatos.sntapepat, oDatos.sntrfc, oDatos.sntclave ); 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SNT_SOLICITANTE> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SNT_SOLICITANTE( paiclave, ocuclave, tslclave, sntotroniveledu, sntotraocup, sntniveduc, sntrepleg, munclave, edoclave, sntusuario, sntfecnac, sntsexo, sntciudadext, sntedoext, sntcorele, snttel, sntcodpos, sntcol, sntnumint, sntnumext, sntcalle, sntcurp, sntnombre, sntapemat, sntapepat, sntrfc, sntclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15, :P16, :P17, :P18, :P19, :P20, :P21, :P22, :P23, :P24, :P25, :P26) ";  
	 	 	  foreach (SIT_SNT_SOLICITANTE oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.paiclave, oDatos.ocuclave, oDatos.tslclave, oDatos.sntotroniveledu, oDatos.sntotraocup, oDatos.sntniveduc, oDatos.sntrepleg, oDatos.munclave, oDatos.edoclave, oDatos.sntusuario, oDatos.sntfecnac, oDatos.sntsexo, oDatos.sntciudadext, oDatos.sntedoext, oDatos.sntcorele, oDatos.snttel, oDatos.sntcodpos, oDatos.sntcol, oDatos.sntnumint, oDatos.sntnumext, oDatos.sntcalle, oDatos.sntcurp, oDatos.sntnombre, oDatos.sntapemat, oDatos.sntapepat, oDatos.sntrfc, oDatos.sntclave ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SNT_SOLICITANTE oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SNT_SOLICITANTE SET  paiclave = :P0, ocuclave = :P1, tslclave = :P2, sntotroniveledu = :P3, sntotraocup = :P4, sntniveduc = :P5, sntrepleg = :P6, munclave = :P7, edoclave = :P8, sntusuario = :P9, sntfecnac = :P10, sntsexo = :P11, sntciudadext = :P12, sntedoext = :P13, sntcorele = :P14, snttel = :P15, sntcodpos = :P16, sntcol = :P17, sntnumint = :P18, sntnumext = :P19, sntcalle = :P20, sntcurp = :P21, sntnombre = :P22, sntapemat = :P23, sntapepat = :P24, sntrfc = :P25 WHERE  sntclave = :P26 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.paiclave, oDatos.ocuclave, oDatos.tslclave, oDatos.sntotroniveledu, oDatos.sntotraocup, oDatos.sntniveduc, oDatos.sntrepleg, oDatos.munclave, oDatos.edoclave, oDatos.sntusuario, oDatos.sntfecnac, oDatos.sntsexo, oDatos.sntciudadext, oDatos.sntedoext, oDatos.sntcorele, oDatos.snttel, oDatos.sntcodpos, oDatos.sntcol, oDatos.sntnumint, oDatos.sntnumext, oDatos.sntcalle, oDatos.sntcurp, oDatos.sntnombre, oDatos.sntapemat, oDatos.sntapepat, oDatos.sntrfc, oDatos.sntclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SNT_SOLICITANTE oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SNT_SOLICITANTE WHERE  sntclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.sntclave ); 
	 	 }
 
 
	 	 public List<SIT_SNT_SOLICITANTE> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_SOLICITANTE ";
	 	 	  return CrearListaMDL<SIT_SNT_SOLICITANTE>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_SNT_SOLICITANTE dmlSelectID(SIT_SNT_SOLICITANTE oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SNT_SOLICITANTE WHERE  sntclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SNT_SOLICITANTE>(ConsultaDML ( sSQL,  oDatos.sntclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SNT_SOLICITANTE );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SNT_SOLICITANTE );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SNT_SOLICITANTE );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public DataTable dmlSelectSolicitanteTranspuestoPorID(Int64 iClaFolio)
        {
            DataTable catalogoRet = new DataTable();
            catalogoRet.Columns.Add("titulo", typeof(string));
            catalogoRet.Columns.Add("valor", typeof(string));
            object oValue;

            String sqlQuery = " SELECT A.sntUsuario, sntRFC, sntApePat, sntApeMat, sntNombre, "
               + " sntcurp, sntCalle, sntNumExt, sntNumInt, sntCol, "
               + " sntCodPos, sntTel, sntCorEle, sntedoext, sntciudadext,"
               + " sntSexo, sntfecnac, sntusuario, P.paiclave, paiDescripcion, edodescripcion, "
               + " mundescripcion, sntrepleg, sntniveduc, sntotraocup, sntotroniveledu, "
               + " tsldescripcion, ocudescripcion "
               + " from SIT_SNT_SOLICITANTE A, SIT_SOL_SOLICITUD B, "
               + " SIT_SNT_PAIS P, SIT_SNT_ESTADO E, SIT_SNT_MUNICIPIO M,"
               + " SIT_SNT_SOLICITANTETIPO S, SIT_SNT_OCUPACION O "
               + " where A.sntClave  =  B.sntClave AND B.solclave = :P0 "
               + " AND A.paiclave = P.paiclave AND  A.edoclave = E.edoclave AND  A.munclave = M.munclave "
               + " AND A.tslclave = S.tslclave AND A.ocuClave = O.ocuClave ";

            DataTable dtDatos = ConsultaDML(sqlQuery, iClaFolio);
            foreach (DataRow row in dtDatos.Rows)
            {
                catalogoRet.Rows.Add("Solicitante", row["tsldescripcion"].ToString());
                catalogoRet.Rows.Add("Nombre", row["sntNombre"].ToString());
                catalogoRet.Rows.Add("Paterno", row["sntApePat"].ToString());
                catalogoRet.Rows.Add("Materno", row["sntApeMat"].ToString());
                catalogoRet.Rows.Add("Sexo", row["sntSexo"].ToString());

                oValue = row["sntfecnac"];
                if (oValue == DBNull.Value)
                    catalogoRet.Rows.Add("Fec. Nacim.", "");
                else
                    catalogoRet.Rows.Add("Fec. Nacim.", Convert.ToDateTime(row["sntfecnac"]).ToShortDateString());

                catalogoRet.Rows.Add("RFC", row["sntRFC"].ToString());
                catalogoRet.Rows.Add("CURP", (row["sntcurp"].ToString()));
                catalogoRet.Rows.Add("Rep. Legal", row["sntrepleg"].ToString());
                catalogoRet.Rows.Add("Ocupación", (row["ocudescripcion"].ToString()));

                // Mexico
                oValue = row["paiclave"];
                if (oValue == DBNull.Value)
                {
                    catalogoRet.Rows.Add("País", "");
                    catalogoRet.Rows.Add("Estado", "");
                    catalogoRet.Rows.Add("Municipio", "");
                }
                else
                {
                    if (Convert.ToInt32(row["paiclave"]) != 131)
                    {
                        catalogoRet.Rows.Add("País", (row["sntedoext"].ToString()));
                        catalogoRet.Rows.Add("Ciudad", (row["sntciudadext"].ToString()));
                    }
                    else
                    {
                        catalogoRet.Rows.Add("País", (row["paiDescripcion"].ToString()));
                        catalogoRet.Rows.Add("Estado", (row["mundescripcion"].ToString()));
                        catalogoRet.Rows.Add("Municipio", (row["tsldescripcion"].ToString()));
                    }
                }

                String sCalle = row["sntCalle"].ToString();
                String sInt = row["sntNumExt"].ToString();
                String sExt = row["sntNumInt"].ToString();

                if (sCalle == null) { sCalle = ""; }
                if (sInt == null) { sInt = ""; }
                if (sExt == null) { sExt = ""; }

                catalogoRet.Rows.Add("Dirección", sCalle + " " + sInt + " " + sExt);
                catalogoRet.Rows.Add("Colonia", (row["sntCol"].ToString()));
                catalogoRet.Rows.Add("C.P.", (row["sntCodPos"].ToString()));
                catalogoRet.Rows.Add("Tel.", (row["sntTel"].ToString()));
                catalogoRet.Rows.Add("Correo Elec.", (row["sntCorEle"].ToString()));
            }
            return catalogoRet;
        }



        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT sntUsuario, sntRFC, sntApePat, sntApeMat, sntNombre, sntcurp, sntCalle, "
                    + " sntNumExt, sntNumInt, sntCol, sntCodPos, sntTel, sntCorEle, sntedoext, sntciudadext, "
                    + " sntSexo, sntfecnac, sntusuario, PAIS.paiclave, paiDescripcion,  "
                    + " SNT.edoclave, edodescripcion,  SNT.munclave, mundescripcion, sntrepleg, "
                    + " sntniveduc, sntotraocup, sntotroniveledu, SNT.tslclave, tsldescripcion,  SNT.ocuClave, ocudescripcion "
                    + " FROM SIT_SNT_SOLICITANTE SNT, SIT_SNT_MUNICIPIO MUN,  SIT_SNT_PAIS PAIS, SIT_SNT_ESTADO EDO,  "
                    + " SIT_SNT_OCUPACION OCU, SIT_SNT_SOLICITANTETIPO TSNT "
                    + " WHERE PAIS.paiclave = SNT.paiclave AND EDO.edoclave = SNT.edoclave"
                    + " AND MUN.munclave = SNT.munclave AND OCU.ocuClave = SNT.ocuClave"
                    + " AND TSNT.tslclave = SNT.tslclave "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
