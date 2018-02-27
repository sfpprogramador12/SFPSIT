using SFP.Persistencia;
using SFP.Persistencia.Model;
using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Services
{
    public class RespuestaSer : BaseFunc
    {
        public const string PARAM_AFDEDODATADML = "AFDEDODATADML";
        public RespuestaSer(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
        }

        public long GrabarRespAvanzar(Dictionary<string, object> dicDatos)
        {
            ProcesoGralDao prcGralDao = new ProcesoGralDao( _cn, _transaction, _sDataAdapter);
            AfdServicio afdServ  = new AfdServicio(_cn, _transaction, _sDataAdapter);

            ////long lrepClave = prcGralDao.GrabarRespuesta((Int64)dicDatos[ProcesoGralDao.PARAM_NODCLAVE], dicDatos[ProcesoGralDao.PARAM_RESP_RESPUESTA] as SIT_RESP_RESPUESTA,
            ////        dicDatos[ProcesoGralDao.PARAM_DICRESPDOC] as Dictionary<int, Tuple<SIT_RESP_DATOS, DocContenidoMdl>>, dicDatos[ProcesoGralDao.PARAM_SHAPOINTMDL] as CfgSharePointMdl,
            ////        (DateTime)dicDatos[ProcesoGralDao.PARAM_FECHA], (int)dicDatos[ProcesoGralDao.PARAM_OPERACION], (int)dicDatos[ProcesoGralDao.PARAM_RESP_ESTADO]);

            ////if (lrepClave > 0)
            ////{
            ////    object oResultado = afdServ.Accion(dicDatos[PARAM_AFDEDODATADML] as AfdEdoDataMdl);
            ////}

            ////return lrepClave;
            return 0;
        }
    }
}
