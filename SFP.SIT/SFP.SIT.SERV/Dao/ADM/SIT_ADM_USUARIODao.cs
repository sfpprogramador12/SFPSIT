using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.Persistencia.Util;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.ADM;
 
namespace SFP.SIT.SERV.Dao.ADM
{
	 public class SIT_ADM_USUARIODao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_ADM_USUARIODao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_ADM_USUARIO oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_USUARIO"); 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_USUARIO( usractivo, usrauxcorreo, usrfecmod, usrdesignacion, usrtitulo, usrbloquearfin, usrintentos, usrextension, usrcorreo, usrcontraseña, usrfecbaja, usrpuesto, usrmaterno, usrpaterno, usrnombre, usrclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.usractivo, oDatos.usrauxcorreo, oDatos.usrfecmod, oDatos.usrdesignacion, oDatos.usrtitulo, oDatos.usrbloquearfin, oDatos.usrintentos, oDatos.usrextension, oDatos.usrcorreo, oDatos.usrcontraseña, oDatos.usrfecbaja, oDatos.usrpuesto, oDatos.usrmaterno, oDatos.usrpaterno, oDatos.usrnombre, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_ADM_USUARIO> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_ADM_USUARIO( usractivo, usrauxcorreo, usrfecmod, usrdesignacion, usrtitulo, usrbloquearfin, usrintentos, usrextension, usrcorreo, usrcontraseña, usrfecbaja, usrpuesto, usrmaterno, usrpaterno, usrnombre, usrclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14, :P15) ";  
	 	 	  foreach (SIT_ADM_USUARIO oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_ADM_USUARIO"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.usractivo, oDatos.usrauxcorreo, oDatos.usrfecmod, oDatos.usrdesignacion, oDatos.usrtitulo, oDatos.usrbloquearfin, oDatos.usrintentos, oDatos.usrextension, oDatos.usrcorreo, oDatos.usrcontraseña, oDatos.usrfecbaja, oDatos.usrpuesto, oDatos.usrmaterno, oDatos.usrpaterno, oDatos.usrnombre, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_ADM_USUARIO oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_ADM_USUARIO SET  usractivo = :P0, usrauxcorreo = :P1, usrfecmod = :P2, usrdesignacion = :P3, usrtitulo = :P4, usrbloquearfin = :P5, usrintentos = :P6, usrextension = :P7, usrcorreo = :P8, usrcontraseña = :P9, usrfecbaja = :P10, usrpuesto = :P11, usrmaterno = :P12, usrpaterno = :P13, usrnombre = :P14 WHERE  usrclave = :P15 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.usractivo, oDatos.usrauxcorreo, oDatos.usrfecmod, oDatos.usrdesignacion, oDatos.usrtitulo, oDatos.usrbloquearfin, oDatos.usrintentos, oDatos.usrextension, oDatos.usrcorreo, oDatos.usrcontraseña, oDatos.usrfecbaja, oDatos.usrpuesto, oDatos.usrmaterno, oDatos.usrpaterno, oDatos.usrnombre, oDatos.usrclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_ADM_USUARIO oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_ADM_USUARIO WHERE  usrclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.usrclave ); 
	 	 }
 
 
	 	 public List<SIT_ADM_USUARIO> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_USUARIO ";
	 	 	  return CrearListaMDL<SIT_ADM_USUARIO>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_ADM_USUARIO dmlSelectID(SIT_ADM_USUARIO oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_ADM_USUARIO WHERE  usrclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_ADM_USUARIO>(ConsultaDML ( sSQL,  oDatos.usrclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_ADM_USUARIO );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_ADM_USUARIO );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_ADM_USUARIO );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public int dmlImportarAux(List<SIT_ADM_USUARIO> lstDatos)
        {
            int iTotReg = 0;
            String sSQL = " INSERT INTO SIT_ADM_USUARIO( usrauxcorreo, usrfecmod, usrdesignacion, usrtitulo, usrbloquearfin, usrintentos, usrextension, usrcorreo, usrcontraseña, usrfecbaja, usrpuesto, usrmaterno, usrpaterno, usrnombre, usrclave) VALUES (  :P0, :P1, :P2, :P3, :P4, :P5, :P6, :P7, :P8, :P9, :P10, :P11, :P12, :P13, :P14) ";
            foreach (SIT_ADM_USUARIO oDatos in lstDatos)
            {
                EjecutaDML(sSQL, oDatos.usrauxcorreo, oDatos.usrfecmod, oDatos.usrdesignacion, oDatos.usrtitulo, oDatos.usrbloquearfin, oDatos.usrintentos, oDatos.usrextension, oDatos.usrcorreo, oDatos.usrcontraseña, oDatos.usrfecbaja, oDatos.usrpuesto, oDatos.usrmaterno, oDatos.usrpaterno, oDatos.usrnombre, oDatos.usrclave);
                iTotReg++;
            }
            return iTotReg;
        }

        public SIT_ADM_USUARIO dmlSelectExisteUsr(string sCorreo)
        {
            string sSQL = " SELECT usrclave, usrNOMBRE, usrPATERNO, usrMATERNO, usrPUESTO, usrFECBAJA, "
                    + " usrCONTRASEÑA, ARACLAVE_origen, usrCORREO, usrEXTENSION, usrINTENTOS, usrbloquearfin, "
                    + " usrTITULO, usrDESIGNACION, usrFECMOD, usrAUXCORREO "
                    + " FROM SIT_ADM_USUARIO WHERE usrCORREO = :P0 ";

            List<SIT_ADM_USUARIO> lstAdmUsuMdl = CrearListaMDL<SIT_ADM_USUARIO>(ConsultaDML(sSQL, sCorreo) as DataTable);
            if (lstAdmUsuMdl.Count > 0)
                return lstAdmUsuMdl[0];
            else
                return null;
        }


        public SIT_ADM_USUARIO dmlSelectValidarCorreo(Dictionary<string, object> dicParametros)
        {

            //Aqui cambiamos el valor a MDD
            String sContraseñaMD5 = EncriptarUtil.ObtenerMD5Hash(dicParametros[DButil.SIT_ADM_USUARIO_COL.USRCONTRASEÑA].ToString());


            string sqlQuery = "SELECT usrclave, usrNOMBRE, usrPATERNO, usrMATERNO, usrPUESTO, usrFECBAJA, "
                    + " usrCONTRASEÑA, usrCORREO, usrEXTENSION, usrINTENTOS, usrbloquearfin, "
                    + " usrTITULO, usrDESIGNACION, usrFECMOD, usrAUXCORREO "
                    + " FROM SIT_ADM_USUARIO WHERE usrCORREO = :P0 and usrCONTRASEÑA = :P1 AND usrFECBAJA is null";

            List<SIT_ADM_USUARIO> lstAdmUsuMdl = CrearListaMDL<SIT_ADM_USUARIO>(
                    ConsultaDML(sqlQuery, dicParametros[DButil.SIT_ADM_USUARIO_COL.USRCORREO].ToString(), sContraseñaMD5));


            if (lstAdmUsuMdl.Count > 0)
                return lstAdmUsuMdl[0];
            else
                return null;
        }

        /*
         // BUSQUEDA DE USUARIO A TRAVES DE SU CLAVE 
        */
        public SIT_ADM_USUARIO dmlSelectEncontrarUsuario(Dictionary<string, object> dicParametros)
        {
            string sqlQuery = "SELECT usrclave, usrNOMBRE, usrPATERNO, usrMATERNO, usrPUESTO, usrFECBAJA, "
                    + " usrCONTRASEÑA, usrCORREO, usrEXTENSION, usrINTENTOS, usrbloquearfin, "
                    + " usrTITULO, usrDESIGNACION, usrFECMOD, usrAUXCORREO "
                    + " FROM SIT_ADM_USUARIO WHERE USRCLAVE = :P0  AND usrFECBAJA is null";

            List<SIT_ADM_USUARIO> lstAdmUsuMdl = CrearListaMDL<SIT_ADM_USUARIO>(
                    ConsultaDML(sqlQuery, dicParametros[DButil.SIT_ADM_USUARIO_COL.USRCLAVE].ToString()));


            if (lstAdmUsuMdl.Count > 0)
                return lstAdmUsuMdl[0];
            else
                return null;
        }




        public Object dmlUpdContraseña(SIT_ADM_USUARIO admUsr)
        {
            String sContraseñaMD5 = EncriptarUtil.ObtenerMD5Hash(admUsr.usrcontraseña);

            string sqlQuery = " UPDATE SIT_ADM_USUARIO SET usrCONTRASEÑA = :P0 WHERE usrclave = :P1 ";
            return EjecutaDML(sqlQuery, sContraseñaMD5, admUsr.usrclave);
        }


        public List<SIT_ADM_USUARIO> dmlGetSharedUsers(string usrclave)
        {
            //String sContraseñaMD5 = EncriptarUtil.ObtenerMD5Hash(admUsr.usrcontraseña);
            string sqlQuery = "SELECT USRCLAVE, USRNOMBRE, USRPATERNO, USRMATERNO FROM SIT_ADM_USUARIO WHERE USRCLAVE IN(SELECT C.COMUSR FROM SIT_ADM_USUARIO U INNER JOIN SIT_ADM_USRCOMPARTIR C ON U.USRCLAVE = C.USRCLAVE and U.USRCLAVE = "+usrclave+" ) ";

            Dictionary<string, object> dicParam = new Dictionary<string, object>();

            dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, usrclave);
            DataTable dt = ConsultaDML(sqlQuery, dicParam);
            List<SIT_ADM_USUARIO> lstAdmUsuMdl = CrearListaMDL<SIT_ADM_USUARIO>(dt);


            if (lstAdmUsuMdl.Count > 0)
                return lstAdmUsuMdl;
            else
                return null;
            
            //string sqlQuery = "";
            //return EjecutaDML(sqlQuery, usrclave);
        }


        public DataTable dmlSelectGridPerfil(Object oDatos)
        {
            string sqlQuery = " SELECT usrclave, usrCORREO, usrNOMBRE, usrPATERNO, usrMATERNO, a.KA_SIGLA  "
                + " from SIT_ADM_USUARIO u, SIT_adm_areahist a "
                + " where u.KA_CLAAREA_ORIGEN = a.KA_CLAAREA "
                + " order by usrCORREO ";
            return ConsultaDML(sqlQuery);
        }


        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = "WITH Resultado AS(select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from(" +
                " SELECT u.usrclave, u.usrNOMBRE, u.usrPATERNO, u.usrMATERNO, u.usrPUESTO, u.usrCONTRASEÑA, " +
                " a.ARACLAVE, u.usrCORREO, u.usrEXTENSION, USUA.UARORIGEN, u.usrFECBAJA, " +
                " u.usrINTENTOS, u.usrbloquearfin, u.usrTITULO, u.usrDESIGNACION, u.usrFECMOD, u.usrAUXCORREO " +
                " from SIT_ADM_USUARIO u, SIT_ADM_AREA a, SIT_ADM_USUARIOAREA USUA where USUA.ARACLAVE = a.ARACLAVE " +
                " order by  usrPATERNO, usrMATERNO, usrNOMBRE) a) SELECT* from Resultado WHERE recid between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }


        public List<SIT_ADM_USUARIO> dmlSelectUsuarioComite()
        {
            String sSQL = " SELECT AU.usrclave, usrNOMBRE, usrPATERNO, usrMATERNO "
                    + " FROM sit_adm_Usuario au, sit_adm_usrperfil aup "
                    + " where au.usrclave = aup.usrclave "
                    + " AND aup.perClave = " + Constantes.Perfil.CT;

            return CrearListaMDL<SIT_ADM_USUARIO>(ConsultaDML(sSQL));
        }
        /*FIN*/
 
	 }
}
