using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.DOC;
 
namespace SFP.SIT.SERV.Dao.DOC
{
	 public class SIT_DOC_EXTENSIONDao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_DOC_EXTENSIONDao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_DOC_EXTENSION oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_DOC_EXTENSION"); 
	 	 	  String  sSQL = " INSERT INTO SIT_DOC_EXTENSION( extterminacion, extmimetype, extclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.extterminacion, oDatos.extmimetype, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_DOC_EXTENSION> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_DOC_EXTENSION( extterminacion, extmimetype, extclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_DOC_EXTENSION oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_DOC_EXTENSION"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.extterminacion, oDatos.extmimetype, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_DOC_EXTENSION oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_DOC_EXTENSION SET  extterminacion = :P0, extmimetype = :P1 WHERE  extclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.extterminacion, oDatos.extmimetype, oDatos.extclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_DOC_EXTENSION oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_DOC_EXTENSION WHERE  extclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.extclave ); 
	 	 }
 
 
	 	 public List<SIT_DOC_EXTENSION> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_DOC_EXTENSION ";
	 	 	  return CrearListaMDL<SIT_DOC_EXTENSION>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	 throw new NotImplementedException(); 
	 	 }
 
 
	 	 public SIT_DOC_EXTENSION dmlSelectID(SIT_DOC_EXTENSION oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_DOC_EXTENSION WHERE  extclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_DOC_EXTENSION>(ConsultaDML ( sSQL,  oDatos.extclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_DOC_EXTENSION );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_DOC_EXTENSION );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_DOC_EXTENSION );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/

        public Dictionary<string, SIT_DOC_EXTENSION> dmlSelectDicTipoExtension()
        {
            Dictionary<string, SIT_DOC_EXTENSION> dicDatos = new Dictionary<string, SIT_DOC_EXTENSION>();
            DataTable dtDatos;

            string sqlQuery = " select extclave, extmimetype, extterminacion FROM SIT_DOC_EXTENSION ";
            dtDatos = ConsultaDML(sqlQuery);

            foreach (DataRow row in dtDatos.Rows)
                dicDatos.Add(row["extterminacion"].ToString(),
                    new SIT_DOC_EXTENSION(extterminacion: row["extterminacion"].ToString(), extmimetype: row["extmimetype"].ToString(), extclave: Convert.ToInt32(row["extclave"])));

            return dicDatos;
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                            + " SELECT extclave, extterminacion, extmimetype  "
                + " from SIT_DOC_EXTENSION "
                + " order by extclave "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        /*FIN*/
 
	 }
}
