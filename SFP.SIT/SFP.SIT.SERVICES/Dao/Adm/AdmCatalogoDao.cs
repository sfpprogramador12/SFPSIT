using SFP.SIT.SERVICES.Model;
using SFP.SIT.SERVICES.Model.Adm;
using SFP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SFP.Persistencia.Dao;

namespace SFP.SIT.SERVICES.Dao.Adm
{
    public class AdmCatalogoDao : BaseDao
    {
        public const int OPE_SELECT_PERFIL_USUARIO = 211;
        public const int OPE_SELECT_SISTEMA = 212;
        public const string COL_KU_CLAUSU = "KU_CLAUSU";

        public AdmCatalogoDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
            // A D M I N I S T R A C I O N
            dicOperacion[OPE_INSERTAR] = new Func<Object, object>(dmlInsert);
            dicOperacion[OPE_EDITAR] = new Func<Object, object>(dmlUpdate);
            dicOperacion[OPE_BORRAR] = new Func<Object, object>(dmlDelete);
            dicOperacion[OPE_IMPORTAR] = new Func<Object, object>(dmlImportar);

            // B U S Q U E D A S 
            dicOperacion[OPE_SELECT_GRID] = new Func<Object, object>(dmlSelectGrid);
            dicOperacion[OPE_SELECT_PERFIL_USUARIO] = new Func<Object, object>(dmlSelectPerfilUsuario);
            dicOperacion[OPE_SELECT_SISTEMA] = new Func<Object, object>(dmlSelectPerfilSistema);
            
            dicOperacion[OPE_SELECT_DICCIONARIO] = new Func<Object, object>(dmlSelectDiccionario);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////        A D M I N I S T R A C I O N
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                          
        private Object dmlInsert(Object oDatos)
        {
            AdmCatalogoMdl dtoDatos = (AdmCatalogoMdl)oDatos;
            String sqlQuery = " insert into SIT_ADM_CATALOGO ( CAT_CLACAT, CAT_DESCRIPCION, CAT_CLASE, KP_CLAPERFIL ) VALUES ( :P0, :P1, :P2, :P3 )";
            return EjecutaDML(sqlQuery, dtoDatos.cat_clacat, dtoDatos.cat_descripcion, dtoDatos.cat_clase, dtoDatos.kp_claperfil);
        }

        private Object dmlUpdate(Object oDatos)
        {
            AdmCatalogoMdl dtoDatos = (AdmCatalogoMdl)oDatos;
            String sqlQuery = " update SIT_ADM_CATALOGO "
                    + " set CAT_DESCRIPCION= :P0, CAT_CLASE= :P1, KP_CLAPERFIL = :P2 "
                    + " where CAT_CLACAT = :P3 ";

            return EjecutaDML(sqlQuery, dtoDatos.cat_descripcion, dtoDatos.cat_clase, dtoDatos.kp_claperfil, dtoDatos.cat_clacat);
        }

        private Object dmlDelete(Object oDatos)
        {
            AdmCatalogoMdl dtoDatos = (AdmCatalogoMdl)oDatos;
            String sqlQuery = " delete from SIT_ADM_CATALOGO where CAT_CLACAT = :P0 ";
            return EjecutaDML(sqlQuery, dtoDatos.cat_clacat);
        }

        private Object dmlImportar(Object oDatos)
        {
            Int16 iContador = 0;
            List<AdmCatalogoMdl> lstDatos = (List<AdmCatalogoMdl>)oDatos;

            String sqlQuery = " insert into SIT_ADM_CATALOGO ( CAT_CLACAT, CAT_DESCRIPCION, CAT_CLASE, KP_CLAPERFIL ) VALUES ( :P0, :P1, :P2, :P3 )";

            foreach (AdmCatalogoMdl dtoDatos in lstDatos)
            {
                EjecutaDML(sqlQuery, dtoDatos.cat_clacat, dtoDatos.cat_descripcion, dtoDatos.cat_clase, dtoDatos.kp_claperfil);
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
            + " SELECT CAT_CLACAT, CAT_DESCRIPCION, CAT_CLASE, CAT.kp_claperfil, KP_DESCRIPCION  "
            + " from SIT_ADM_CATALOGO CAT, SIT_ADM_KPERFIL KPERFIL "
            + " WHERE  KPERFIL.kp_claperfil = CAT.kp_claperfil "
            + " ) a ) SELECT * from Resultado  WHERE recid  between :P0 and :P1 ";

            return (DataTable)ConsultaDML(sqlQuery, baseMdl.LimInf, baseMdl.LimSup);
        }
        private Dictionary<int, string> dmlSelectDiccionario(Object oDatos)
        {
            String sqlQuery = "SELECT CAT_CLACAT, CAT_CLASE from SIT_ADM_CATALOGO ";
            DataTable dtDatos = (DataTable)ConsultaDML(sqlQuery);
            Dictionary<int, string> dicCatClases = new Dictionary<int, string>();

            foreach (DataRow drDatos in dtDatos.Rows)
                dicCatClases.Add( Convert.ToInt32(drDatos[0].ToString()), drDatos[1].ToString());

            return dicCatClases;
        }        
        private DataTable dmlSelectPerfilUsuario(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>) oDatos;
            String sqlQuery;

            sqlQuery = " select CAT_CLACAT as id, CAT_DESCRIPCION as text"
                + " from sit_ADM_kAREA a, SIT_ADM_USER_AREA ua, SIT_ADM_CATALOGO c "
                + " where a.ka_claarea = ua.ka_claarea "
                + " and a.kp_claperfil = c.kp_claperfil "
                + " and ku_clausu = :P0 "
                + " ORDER BY CAT_DESCRIPCION";

            return (DataTable)ConsultaDML(sqlQuery, dicParam[COL_KU_CLAUSU] );
        }

        private DataTable dmlSelectPerfilSistema(Object oDatos)
        {
            Dictionary<string, object> dicParam = (Dictionary<string, object>)oDatos;
            String sqlQuery = " SELECT CAT_CLACAT as id, CAT_DESCRIPCION as text FROM SIT_ADM_CATALOGO CAT  ORDER BY CAT_DESCRIPCION";
            return (DataTable)ConsultaDML(sqlQuery);
        }

        protected override object CrearListaMDL(DataTable dtDatos)
        {
            throw new NotImplementedException();
        }
    }
}