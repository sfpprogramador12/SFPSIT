using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFP.Persistencia.Dao;
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.SOL;
 
namespace SFP.SIT.SERV.Dao.SOL
{
	 public class SIT_SOL_MODOENTREGADao : BaseDao
	 {
	 	 public long iSecuencia { get; set; } 	
	 	 public SIT_SOL_MODOENTREGADao(DbConnection cn, DbTransaction transaction, String dataAdapter) : base(cn, transaction, dataAdapter)
	 	 {
	 	}
 
 
	 	 public Object dmlAgregar(SIT_SOL_MODOENTREGA oDatos)
	 	 {
	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_SOL_MODOENTREGA"); 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_MODOENTREGA( megmostrar, megdescripcion, megclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  EjecutaDML ( sSQL,  oDatos.megmostrar, oDatos.megdescripcion, iSecuencia  ); 
	 	 	  return iSecuencia; 
	 	 }
 
 
	 	 public int dmlImportar( List<SIT_SOL_MODOENTREGA> lstDatos)
	 	 {
	 	 	 int iTotReg = 0; 
	 	 	  String  sSQL = " INSERT INTO SIT_SOL_MODOENTREGA( megmostrar, megdescripcion, megclave) VALUES (  :P0, :P1, :P2) ";  
	 	 	  foreach (SIT_SOL_MODOENTREGA oDatos in lstDatos) 
	 	 	  { 
	 	 	 	  iSecuencia = SecuenciaDML("SEC_SIT_SOL_MODOENTREGA"); 
	 	 	 	  EjecutaDML ( sSQL,  oDatos.megmostrar, oDatos.megdescripcion, iSecuencia  ); 
	 	 	 	  iTotReg++; 
	 	 	  } 
	 	 	  return iTotReg; 
	 	 }
 
 
	 	 public int dmlEditar(SIT_SOL_MODOENTREGA oDatos)
	 	 {
	 	 	  String  sSQL = " UPDATE SIT_SOL_MODOENTREGA SET  megmostrar = :P0, megdescripcion = :P1 WHERE  megclave = :P2 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.megmostrar, oDatos.megdescripcion, oDatos.megclave ); 
	 	 }
 
 
	 	 public int dmlBorrar(SIT_SOL_MODOENTREGA oDatos)
	 	 {
	 	 	  String  sSQL = " DELETE FROM SIT_SOL_MODOENTREGA WHERE  megclave = :P0 ";  
	 	 	  return (int) EjecutaDML ( sSQL,  oDatos.megclave ); 
	 	 }
 
 
	 	 public List<SIT_SOL_MODOENTREGA> dmlSelectTabla( )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_MODOENTREGA ";
	 	 	  return CrearListaMDL<SIT_SOL_MODOENTREGA>(ConsultaDML(sSQL) as DataTable); 
	 	 }
 
 
	 	 public List<ComboMdl> dmlSelectCombo( )
	 	 {
	 	 	  String  sSQL = " SELECT megclave as ID, MEGDESCRIPCION as DESCRIP FROM SIT_SOL_MODOENTREGA";
	 	 	  return CrearListaMDL<ComboMdl> (ConsultaDML (sSQL) as DataTable); 
	 	 }
 
 
	 	 public Dictionary<int, string> dmlSelectDiccionario( )
	 	 {
	 	 	  String  sSQL = " SELECT megclave, MEGDESCRIPCION FROM SIT_SOL_MODOENTREGA";
	 	 	  DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
	 	 	  Dictionary<int, string> dicCatClases = new Dictionary<int, string>(); 
 
	 	 	  foreach (DataRow drDatos in dtDatos.Rows)
	 	 	 	 dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString()); 
	 	 	  return dicCatClases; 
	 	 }
 
 
	 	 public SIT_SOL_MODOENTREGA dmlSelectID(SIT_SOL_MODOENTREGA oDatos )
	 	 {
	 	 	  String  sSQL = " SELECT * FROM SIT_SOL_MODOENTREGA WHERE  megclave = :P0 ";  
	 	 	  return CrearListaMDL<SIT_SOL_MODOENTREGA>(ConsultaDML ( sSQL,  oDatos.megclave ) as DataTable)[0]; 
	 	 }
 
	 	 public object dmlCRUD( Dictionary<string, object> dicParam )
	 	 {
	 	 	 int iOper = (int)dicParam[CMD_OPERACION]; 
 
	 	 	 if (iOper == OPE_INSERTAR) 
	 	 	 	 return dmlAgregar(dicParam[CMD_ENTIDAD] as SIT_SOL_MODOENTREGA );
 
	 	 	 else if (iOper == OPE_EDITAR)
	 	 	 	 return dmlEditar(dicParam[CMD_ENTIDAD] as SIT_SOL_MODOENTREGA );
 
	 	 	 else if (iOper == OPE_BORRAR)
	 	 	 	 return dmlBorrar(dicParam[CMD_ENTIDAD] as SIT_SOL_MODOENTREGA );
	 	 	 else 
	 	 	 	 return 0;
 
	 	 }
 
 
        /*INICIO*/        
        public int dmlImportarAux(List<SIT_SOL_MODOENTREGA> lstDatos)
        {
            int iTotReg = 0;
            String sSQL = " INSERT INTO SIT_SOL_MODOENTREGA( megclave, megDescripcion, megmostrar) VALUES (  :P0, :P1, :P2) ";
            foreach (SIT_SOL_MODOENTREGA oDatos in lstDatos)
            {
                EjecutaDML(sSQL, oDatos.megclave, oDatos.megdescripcion, oDatos.megmostrar);
                iTotReg++;
            }
            return iTotReg;
        }

        public DataTable dmlSelectGrid(BasePagMdl baseMdl)
        {
            String sqlQuery = " WITH Resultado AS( select COUNT(*) OVER() RESULT_COUNT, rownum recid, a.* from ( "
                + " SELECT megclave, megdescripcion, megmostrar  from SIT_sol_modoentrega  order by megclave "
                + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";
            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }

        public Dictionary<int, string> dmlSelectDicMostrar()
        {
            String sSQL = " SELECT megclave, megdescripcion FROM SIT_SOL_MODOENTREGA WHERE megclave  <> 1";
            DataTable dtDatos = (DataTable)ConsultaDML(sSQL);
            Dictionary<int, string> dicCatClases = new Dictionary<int, string>();

            foreach (DataRow drDatos in dtDatos.Rows)
                dicCatClases.Add(Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString());
            return dicCatClases;
        }

        /*FIN*/
 
	 }
}
