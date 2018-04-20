using SFP.Persistencia;
using SFP.Persistencia.Model;
using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.WEB.Util;
using System;
using System.Collections.Generic;
using System.Data;
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

        //ESTA FUNCINON DESAPARECE ??????
        //Lo agergo aquoi para establecer una transaccion 
        public long GrabarRespAvanzar(Dictionary<string, object> dicDatos)
        {
            ProcesoGralDao prcGralDao = new ProcesoGralDao( _cn, _transaction, _sDataAdapter);
            AfdServicio afdServ  = new AfdServicio(_cn, _transaction, _sDataAdapter);

            long lrepClave = prcGralDao.InsertarRegistro(dicDatos);
            if (lrepClave > 0)
            {
                object oResultado = afdServ.Accion(dicDatos[PARAM_AFDEDODATADML] as AfdEdoDataMdl);
            }

            return lrepClave;
        }




    }
}
