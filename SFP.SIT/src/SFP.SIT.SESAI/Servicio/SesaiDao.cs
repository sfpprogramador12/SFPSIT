
using SFP.Persistencia.Dao;
using SFP.Persistencia.Util;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model.SNT;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using SFP.SIT.SESAI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace SFP.SIT.SESAI.Servicio
{
    class SesaiDao : BaseDao
    {
        public static readonly string COL_FOLIO = "FOLIO";
        public static readonly string COL_AÑO_INI = "AÑO_INI";
        public static readonly string COL_AÑO_FIN = "AÑO_FIN";

        public static readonly int OPE_UPDATE_SELECT_MULTIPLE = 400;
        public Dictionary<int, Delegate> dicOperacion = new Dictionary<int, Delegate>();

        public SesaiDao(DbConnection cn, DbTransaction transaction, String sDataAdapter)
            : base(cn, transaction, sDataAdapter)
        {
        }


        public object dmlCommand()
        {
            return EjecutaDML("ALTER SESSION SET NLS_SORT = BINARY");
        }


        public object dmlSelectAreas()
        {
            List<SIT_ADM_AREA> lstArea = new List<SIT_ADM_AREA>();

            lstArea.Add(new SIT_ADM_AREA { araclave = 0, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 1, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 2, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 101, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 102, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 103, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 104, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 105, arafeccreacion = FechaUtil.Fecha("01/07/2002") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 106, arafeccreacion = FechaUtil.Fecha("01/01/2002") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 107, arafeccreacion = FechaUtil.Fecha("01/01/2002") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 108, arafeccreacion = FechaUtil.Fecha("01/01/2010") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 109, arafeccreacion = FechaUtil.Fecha("20/10/2015") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 110, arafeccreacion = FechaUtil.Fecha("13/01/2017") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 111, arafeccreacion = FechaUtil.Fecha("13/01/2017") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 112, arafeccreacion = FechaUtil.Fecha("13/01/2017") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 113, arafeccreacion = FechaUtil.Fecha("13/01/2017") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 114, arafeccreacion = FechaUtil.Fecha("13/01/2017") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 115, arafeccreacion = FechaUtil.Fecha("13/01/2017") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 200, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 201, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 202, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 203, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 204, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 205, arafeccreacion = FechaUtil.Fecha("01/01/2001") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 206, arafeccreacion = FechaUtil.Fecha("01/01/2002") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 207, arafeccreacion = FechaUtil.Fecha("01/01/2002") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 208, arafeccreacion = FechaUtil.Fecha("01/07/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 209, arafeccreacion = FechaUtil.Fecha("01/01/2010") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 300, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 301, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 302, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 303, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 304, arafeccreacion = FechaUtil.Fecha("01/01/2002") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 305, arafeccreacion = FechaUtil.Fecha("01/07/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 306, arafeccreacion = FechaUtil.Fecha("01/01/2010") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 307, arafeccreacion = FechaUtil.Fecha("01/01/2010") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 308, arafeccreacion = FechaUtil.Fecha("01/01/2013") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 400, arafeccreacion = FechaUtil.Fecha("01/01/2003") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 401, arafeccreacion = FechaUtil.Fecha("01/01/2003") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 402, arafeccreacion = FechaUtil.Fecha("01/01/2003") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 403, arafeccreacion = FechaUtil.Fecha("01/01/2003") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 404, arafeccreacion = FechaUtil.Fecha("01/07/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 405, arafeccreacion = FechaUtil.Fecha("01/01/2006") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 406, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 407, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 408, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 409, arafeccreacion = FechaUtil.Fecha("01/01/2010") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 410, arafeccreacion = FechaUtil.Fecha("01/01/2010") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 411, arafeccreacion = FechaUtil.Fecha("01/01/2006") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 412, arafeccreacion = FechaUtil.Fecha("01/01/2006") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 413, arafeccreacion = FechaUtil.Fecha("01/01/2006") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 500, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 501, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 502, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 503, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 504, arafeccreacion = FechaUtil.Fecha("01/01/2002") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 505, arafeccreacion = FechaUtil.Fecha("01/01/2010") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 506, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 507, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 600, arafeccreacion = FechaUtil.Fecha("01/01/2000") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 601, arafeccreacion = FechaUtil.Fecha("01/01/2007") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 602, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 603, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 604, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 605, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 606, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 607, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 608, arafeccreacion = FechaUtil.Fecha("01/01/2004") });

            lstArea.Add(new SIT_ADM_AREA { araclave =700, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =701, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =702, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =703, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =704, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =705, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =706, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =707, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =708, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =709, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =710, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =711, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =712, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =713, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =714, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =715, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =716, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =717, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =718, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =719, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =720, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =721, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =722, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =723, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =724, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =725, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =726, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =727, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =728, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =729, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =730, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =731, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =732, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =733, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =734, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =735, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =736, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =737, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =738, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =739, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =740, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =741, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =742, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =743, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =744, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =745, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =746, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =747, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =748, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =749, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =750, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =751, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =752, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =753, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =754, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =755, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =756, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =757, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =758, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =759, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =760, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =761, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =762, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =763, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =764, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =765, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =766, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =767, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =768, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =769, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =770, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =771, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =772, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =773, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =774, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =775, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =776, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =777, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =778, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =779, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =780, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =781, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =782, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =783, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =784, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =785, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =786, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =787, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =788, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =789, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =790, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =791, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =792, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =793, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =794, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =795, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =796, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =797, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =798, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =799, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =800, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =801, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =802, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =803, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =804, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =805, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =806, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =807, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =808, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =809, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =810, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =811, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =812, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =813, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =814, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =815, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =816, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =817, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =818, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =819, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =820, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =821, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =822, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =823, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =824, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =825, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =826, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =827, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =828, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =829, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =830, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =831, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =832, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =833, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =834, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =835, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =836, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =837, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =838, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =839, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =840, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =841, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =842, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =843, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =844, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =845, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =846, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =847, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =848, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =849, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =850, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =851, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =852, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =853, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =854, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =855, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =856, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =857, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =858, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =859, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =860, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =861, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =862, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =863, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =864, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =865, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =866, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =867, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =868, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =869, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =870, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =871, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =872, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =873, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =874, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =875, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =876, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =877, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =878, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =879, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =880, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =881, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =882, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =883, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =884, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =885, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =886, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =887, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =888, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =889, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =890, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =891, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =892, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =893, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =894, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =895, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =896, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =897, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =898, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =899, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =900, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =901, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =902, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =903, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =904, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =905, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =906, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =907, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =908, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =909, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =910, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =911, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =912, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =913, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =914, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =915, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =916, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =917, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =918, arafeccreacion = FechaUtil.Fecha("01/01/2004") });
            lstArea.Add(new SIT_ADM_AREA { araclave =919, arafeccreacion = FechaUtil.Fecha("01/01/2004") });


            // ARAS DE LA DIRECCIÓN GENERAL DE TRANSPARENCIA
            lstArea.Add(new SIT_ADM_AREA { araclave = 920, arafeccreacion = FechaUtil.Fecha("13/01/2017") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 921, arafeccreacion = FechaUtil.Fecha("13/01/2017") });
            lstArea.Add(new SIT_ADM_AREA { araclave = 922, arafeccreacion = FechaUtil.Fecha("13/01/2017") });



            //lstDatos.Add(new AdmAreaMdl(11, "Instituto Nacional de Acceso a la Información", "0", "INAI", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur 3211, Delegación Coyoacán, Col. Insurgentes Cuicuilco, 04530 Ciudad de México, CDMX", 1));
            //lstDatos.Add(new AdmAreaMdl(12, "Unidad de Transparencia", "N/A", "UT", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur #1735", 2));
            //lstDatos.Add(new AdmAreaMdl(13, "Comité de Transparencia", "N/A", "CT", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur#1735", 4));
            //lstDatos.Add(new AdmAreaMdl(14, "Revisión Forma", "N/A", "RF", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur#1735", 5));
            //lstDatos.Add(new AdmAreaMdl(15, "Unidad de Transparencia Notificar", "N/A", "UTN", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur#1735", 6));

            //lstDatos.Add(new AdmAreaMdl(100, "Oficina del Secretario", "100", "OFI. SEC.", 1, 1, 1, 1, new DateTime(2000, 01, 01), new DateTime(1999, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(101, "Unidad de Desarrollo Administrativo", "109", "UDA", 1, 1, 5, 1, new DateTime(2000, 01, 01), new DateTime(2003, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(102, "Unidad de Asuntos Jurídicos", "110", "UAJ", 1, 2, 5, 1, new DateTime(2015, 03, 27), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(103, "Dirección General de Comunicación Social", "116", "DGCS", 1, 2, 6, 1, new DateTime(2005, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(104, "Contraloría Interna", "112", "CI", 1, 3, 6, 1, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(105, "Unidad de Vinculación para la Transparencia", "111", "UVT", 1, 3, 5, 1, new DateTime(2002, 07, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(106, "Coordinación General de Órganos de Vigilancia y Control", "113", "CGOVC", 1, 4, 6, 1, new DateTime(2015, 03, 27), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(107, "Secretaría Ejecutiva de la Comisión Intersercretarial para la Transparencia y el Combate a la Corrupción en la Administración Pública Federal", "114", "SECI", 1, 5, 6, 1, new DateTime(2002, 01, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(108, "Unidad de Políticas de Transparencia y Cooperación Internacional", "117", "UPTCI", 1, 7, 5, 1, new DateTime(2017, 01, 13), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(109, "Unidad Especializada en Ética y Prevención de Conflictos de Interés", "419", "UEEPCI", 400, 16, 5, 1, new DateTime(2017, 01, 13), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(110, "Dirección General de Transparencia", "120", "DGT", 1, 10, 6, 1, new DateTime(2017, 01, 13), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(111, "Dirección General de Igualdad de Genero", "121", "DGIG", 1, 10, 6, 1, new DateTime(2017, 01, 13), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(112, "Unidad de Vinculación con el Sistema Nacional Anticorrupción", "122", "UVSNA", 1, 11, 5, 1, new DateTime(2017, 01, 13), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(113, "Dirección General de Vinculación con el Sistema Nacional Anticorrupción", "123", "DGVSNT", 112, 1, 6, 1, new DateTime(2017, 01, 13), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(114, "Dirección General de Vinculación con el Sistema Nacional de Fizcalización", "124", "DGVSNF", 112, 2, 6, 1, new DateTime(2017, 01, 13), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(115, "Dirección General de Análisis, Diagnóstico y Formulación de Proyectos en Materia Anticorrupción de la Administración Pública Federal", "125", "DGADFPMAAPF", 112, 3, 6, 1, new DateTime(2017, 01, 13), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(200, "SubSecretaría de Control y Auditoría de la Gestión Pública", "200", "SCAGP", 1, 1, 2, 1, new DateTime(2002, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(201, "Unidad de Atención Ciudadana", "310", "UAC", 300, 5, 5, 1, new DateTime(2011, 01, 01), new DateTime(2012, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(202, "Dirección General de Responsabilidades y Situación Patrimonial", "311", "DGRSP", 300, 7, 6, 1, new DateTime(2002, 07, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(203, "Unidad de Operación Regional y Contraloría Social", "211", "UORCS", 200, 7, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(204, "Dirección General de Inconformidades", "213", "DGI", 200, 8, 6, 1, new DateTime(2000, 01, 01), new DateTime(2001, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(205, "Unidad de Gobierno Digital", "409", "UGD", 400, 3, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(206, "Unidad de Auditoría Gubernamental", "210", "UAG", 200, 10, 5, 1, new DateTime(2002, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(207, "Unidad de Control de la Gestión Pública", "209", "UCGP", 200, 12, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(208, "Dirección General de Auditorías Externas", "212", "DGAE", 200, 13, 6, 1, new DateTime(2006, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(209, "Unidad de Control y Auditoría a Obra Pública", "208", "UCOP", 200, 14, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(300, "SubSecretaría de Responsabilidades Administrativas y Contrataciones Públicas", "300", "SRACP", 1, 1, 2, 1, new DateTime(2014, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(301, "Unidad de Normatividad de Contrataciones Públicas", "309", "UNCP", 300, 2, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(302, "Unidad de Seguimiento y Evaluación de la Gestión Pública", "310", "USEGP", 300, 3, 5, 1, new DateTime(2000, 01, 01), new DateTime(2001, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(303, "Dirección General de Auditoría Gubernamental", "311", "DGAG", 300, 6, 6, 1, new DateTime(2000, 01, 01), new DateTime(2002, 06, 30), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(304, "Dirección General de Inconformidades", "312", "DGI", 300, 8, 6, 1, new DateTime(2002, 01, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(305, "Dirección General de Información e Integración", "118", "DGII", 1, 8, 6, 1, new DateTime(2015, 03, 27), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(306, "Unidad de Política de Contrataciones Públicas", "308", "UPCP", 300, 10, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(307, "Dirección General de Controversias y Sanciones en Contrataciones Públicas", "312", "DGCSCP", 300, 11, 6, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(308, "Dirección General de Denuncias e Investigaciones", "310", "DGDI", 300, 12, 6, 1, new DateTime(2013, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(400, "SubSecretaría de la Función Pública", "400", "SSFP", 1, 1, 2, 1, new DateTime(2005, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(401, "Dirección General de Simplificación Regulatoria", "410", "DGSR", 400, 4, 6, 1, new DateTime(2003, 01, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(402, "Dirección General de Eficiencia Administrativa y Buen Gobierno", "411", "DGEABG", 400, 5, 6, 1, new DateTime(2005, 01, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(403, "Dirección General de Estructuras y Puestos", "412", "DGEP", 400, 6, 6, 1, new DateTime(2005, 01, 01), new DateTime(2005, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(404, "Unidad de Política de Recursos Humanos de la Administración Pública Federal", "408", "UPRHAPF", 400, 7, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(405, "Dirección General de Planeación, Organización y Compensaciones de la Administración Pública Federal", "412", "DGPOCAFP", 400, 8, 6, 1, new DateTime(2006, 01, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(406, "Dirección General de Ingreso, Capacitación y Certificación", "413", "DGICC", 400, 9, 6, 1, new DateTime(2006, 01, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(407, "Dirección General de Atención a Instituciones Públicas en Recursos Humanos", "414", "DGAIPRH", 400, 10, 6, 1, new DateTime(2006, 01, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(408, "Dirección General de Evaluación de Sistemas de Profesionalización", "415", "DGESP", 400, 11, 6, 1, new DateTime(2006, 01, 01), new DateTime(2009, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(409, "Unidad de Políticas de Mejora de la Gestión Pública", "411", "UPMGP", 400, 12, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(410, "Unidad de Evaluación de la Gestión y el Desempeño Gubernamental", "416", "UEGDG", 400, 13, 5, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(411, "Gabinete de apoyo SSFP", "400A", "GA-SSFP", 400, 14, 7, 1, new DateTime(2006, 01, 01), new DateTime(2007, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(412, "Dirección General de Desarrollo Humano y Servicio Profesional de Carrera", "408", "DGDHSPC", 400, 15, 6, 1, new DateTime(2016, 11, 07), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(413, "Dirección General de Organización y Remuneraciones de la Administracion Pública Federal", "408", "DGORAPF", 404, 16, 5, 1, new DateTime(2016, 11, 07), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(500, "Oficialía Mayor", "500", "OM", 1, 1, 3, 1, new DateTime(2003, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(501, "Dirección General de Recursos Humanos", "510", "DGRH", 500, 2, 6, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(502, "Dirección General de Tecnologías de Información", "511", "DGTI", 500, 3, 6, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(503, "Dirección General de Programación y Presupuesto", "512", "DGPYP", 500, 4, 6, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(504, "Dirección General de Modernización Administrativa y Procesos", "513", "DGMAP", 500, 5, 6, 1, new DateTime(2003, 01, 01), new DateTime(2011, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(505, "Dirección General de Recursos Materiales y Servicios Generales", "514", "DGRMSG", 500, 6, 6, 1, new DateTime(2010, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(506, "Coordinación Técnica", "50A", "CTOM", 500, 7, 7, 1, new DateTime(2004, 01, 01), new DateTime(2011, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(507, "Coordinación de Asesores", "50B", "CAOM", 500, 8, 7, 1, new DateTime(2004, 01, 01), new DateTime(2011, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(600, "Instituto de Administración y Avalúos de Bienes Nacionales", "A00", "INDAABIN", 1, 1, 4, 2, new DateTime(2005, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(601, "Instituto Nacional de Administración Pública, A.C.", "28S", "INAP", 1, 2, 4, 2, new DateTime(2007, 01, 01), new DateTime(2012, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(602, "Dirección General de Avalúos", "A01", "DGA", 600, 3, 6, 2, new DateTime(2004, 01, 01), new DateTime(2007, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(603, "Dirección General de Política de Gestión Inmobiliario", "A03", "DGPGI", 600, 5, 6, 2, new DateTime(2008, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(604, "Dirección General de Administración del Patrimonio Inmobiliario Federal", "A02", "DGAPIF", 600, 4, 6, 2, new DateTime(2008, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(605, "Dirección General Jurídica", "A04", "DGJ", 600, 6, 6, 2, new DateTime(2004, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(606, "Dirección General de Administración y Finanzas", "A05", "DGAF", 600, 7, 6, 2, new DateTime(2004, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(607, "Coordinación de delegaciones", "A06", "CD", 600, 8, 6, 2, new DateTime(2004, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(608, "Dirección de Informática", "A07", "CD", 600, 8, 6, 2, new DateTime(2004, 01, 01), new DateTime(2035, 12, 31), "Av. Insurgentes Sur", 3));
            //lstDatos.Add(new AdmAreaMdl(701, "Administración Federal de Servicios Educativos en el D.F.", "OIC1", "AFSEDF", 0, 1, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES CENTRO NO. 149 PISO 7 COLONIA SAN RAFAEL, DELEG. CUAUHTÉMOC MÉXICO, D.F. C.P. 06470", 3));
            //lstDatos.Add(new AdmAreaMdl(702, "Administración Portuaria Integral de Altamira S.A.de C.V.", "OIC2", "API ALTAMIRA", 0, 2, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE RÍO TAMESÍ KM.0 800 LADO SUR, COL. PUERTO INDUSTRIAL, C.P. 89603 ALTAMIRA TAMAULIPAS", 3));
            //lstDatos.Add(new AdmAreaMdl(703, "Administración Portuaria Integral de Coatzacoalcos S.A.de C.V.", "OIC3", "API COATZA", 0, 3, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INTERIOR DEL RECINTO PORTUARIO S/N, COL. CENTRO,  C.P. 96400 COATZACOALCOS, VERACRUZ", 3));
            //lstDatos.Add(new AdmAreaMdl(704, "Administración Portuaria Integral de Dos bocas S.A.de C.V.", "OIC4", "API DOS BOCAS", 0, 4, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA FEDERAL PUERTO CEIBA-PARAÍSO No. 414, COL. QUINTÍN ARAÚZ, C.P.  86600, PARAÍSO, TABASCO", 3));
            //lstDatos.Add(new AdmAreaMdl(705, "Administración Portuaria Integral de Ensenada S.A.de C.V.", "OIC5", "API ENSENADA", 0, 5, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD. TENIENTE AZUETA NO. 110, RECINTO PORTUARIO,  C.P. 22800 ENSENADA, B.C.", 3));
            //lstDatos.Add(new AdmAreaMdl(706, "Administración Portuaria Integral de Guaymas S.A.de C.V.", "OIC6", "API GUAYMAS", 0, 6, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "RECINTO PORTUARIO, ZONA FRANCA S/N, COL. PUNTA ARENA, C.P. 85430, GUAYMAS, SONORA.", 3));
            //lstDatos.Add(new AdmAreaMdl(707, "Administración Portuaria Integral de Lázaro cárdenas S.A.de C.V.", "OIC7", "API LÁZARO CÁRDENAS", 0, 7, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PROLONGACIÓN AV. LÁZARO CÁRDENAS No. 1, COL. CENTRO, C.P. 60950, LÁZARO CÁRDENAS, MICHOACÁN", 3));
            //lstDatos.Add(new AdmAreaMdl(708, "Administración Portuaria Integral de Manzanillo S.A.de C.V.", "OIC8", "API MANZANILLO", 0, 8, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. TENIENTE AZUETA No. 9, COL. BURÓCRATA, C.P. 28250,  MANZANILLO, COLIMA", 3));
            //lstDatos.Add(new AdmAreaMdl(709, "Administración Portuaria Integral de Mazatlan S.A.de C.V.", "OIC9", "API MAZATLÁN", 0, 9, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INTERIOR RECINTO FISCAL S/N, COL. CENTRO. C.P. 82000, MAZATLAN, SINALOA", 3));
            //lstDatos.Add(new AdmAreaMdl(710, "Administración Portuaria Integral de Progreso S.A.de C.V.", "OIC10", "API PROGRESO", 0, 10, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "VIADUCTO AL MUELLE FISCAL KM 2., PROGRESO, YUCATAN, C.P. 97320", 3));
            //lstDatos.Add(new AdmAreaMdl(711, "Administración Portuaria Integral de Puerto madero", "OIC11", "OIC", 0, 11, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "EDIFICIO OPERATIVO,  RECINTO  FISCAL   S/N PUERTO CHIAPAS, MUELLE FISCAL, TAPACHULA CHIAPAS, C. P. 30830", 3));
            //lstDatos.Add(new AdmAreaMdl(712, "Administración Portuaria Integral de Puerto vallarta S.A.de C.V.", "OIC12", "API VALLARTA", 0, 12, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD.  FRANCISCO MEDINA ASCENCIO, S/N, KM. 4.5, ZONA HOTELERA NORTE, C.P. 48333, PUERTO VALLARTA, JALISCO.", 3));
            //lstDatos.Add(new AdmAreaMdl(713, "Administración Portuaria Integral de Salina cruz S.A.de C.V.", "OIC13", "API SALINA CRUZ", 0, 13, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INTERIOR RECINTO FISCAL S/N, COL. CANTARRANAS, C.P. 70680, SALINA  CRUZ, OAXACA", 3));
            //lstDatos.Add(new AdmAreaMdl(714, "Administración Portuaria Integral de Tampico S.A.de C.V.", "OIC14", "API TAMPICO", 0, 14, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "EDIFICIO API TAMPICO, S/N, ZONA CENTRO, TAMPICO TAMAULIPAS C.P. 89000", 3));
            //lstDatos.Add(new AdmAreaMdl(715, "Administración Portuaria Integral de Topolobampo", "OIC15", "OIC", 0, 15, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "ACCESO AL PARQUE INDUSTRIAL PESQUERO S/N, TOPOLOBAMPO, AHOME, SINALOA, C.P. 81370", 3));
            //lstDatos.Add(new AdmAreaMdl(716, "Administración Portuaria Integral de Tuxpan S.A.de C.V.", "OIC16", "API TUXPAN", 0, 16, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA A LA BARRA NORTE KM. 6.5, COL. EJIDO LA CALZADA, TUXPAN VERACRUZ, C.P. 92800", 3));
            //lstDatos.Add(new AdmAreaMdl(717, "Administración Portuaria Integral de Veracruz S.A.de C.V.", "OIC17", "API VERACRUZ", 0, 17, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. MARINA  MERCANTE 210,   5to. PISO,    COL. CENTRO, C.P. 91700, VERACRUZ, VERACRUZ", 3));
            //lstDatos.Add(new AdmAreaMdl(718, "Aeropuerto InterNacional de la ciudad de méxico S.A.de C.V.", "OIC18", "AICM", 0, 18, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "OFICINA 94 MEZZANINE DE LA TERMINAL 1, DEL AEROPUERTO INTERNACIONAL BENITO JUARÉZ CIUDAD DE MÉXICO, AV. CAPITÁN CARLOS LEÓN S/N, COL. PEÑÓN DE LOS BAÑOS, DELEG. VENUSTIANO CARRANZA, C.P. 15620, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(719, "Aeropuertos y Servicios auxiliares", "OIC19", "ASA", 0, 19, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. 602, No. 161, COL.  ZONA FEDERAL AEROPUERTO INTERNACIONAL CIUDAD DE MÉXICO, DELEG. VENUSTIANO CARRANZA, C.P. 15620, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(720, "Agencia de Servicios a la comercialización y desarrollo de mercados agropecuarios", "OIC20", "ASERCA", 0, 20, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 489, 13° PISO, COL. HIPÓDROMO CONDESA,  DELEG. CUAUHTÉMOC, C.P. 06100, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(721, "Agencia Espacial Mexicana", "OIC21", "AEM", 0, 21, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR NO. 1685, PISO 2 Y 13, COL. GUADALUPE INN, DELEG. ALVARO OBREGÓN, C.P. 01020, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(722, "Agroasemex S.A.", "OIC22", "AGROASEMEX", 0, 22, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. CONSTITUYENTES No. 124, PONIENTE, 3ER PISO, COL. EL CARRIZAL, C.P. 76030, SANTIAGO DE QUERÉTARO, QUERÉTARO", 3));
            //lstDatos.Add(new AdmAreaMdl(723, "Banco del Ahorro Nacional y Servicios Financieros S.N.C.", "OIC23", "BANSEFI", 0, 23, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "RIO MAGDALENA NO. 115,  COL. TIZAPAN, SAN ÁNGEL, DELEG. ÁLVARO OBREGÓN, C.P. 01090, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(724, "Banco Nacional de comercio exterior", "OIC24", "BANCOMEXT", 0, 24, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PERIFÉRICO SUR. NO. 4333, PISO 5 ALA ORIENTE, COL. JARDINES DE LA MONTAÑA,  DELEG. TLALPAN C.P. 14210, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(725, "Banco Nacional de obras y servicios públicos", "OIC25", "BANOBRAS", 0, 25, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. JAVIER  BARRIOS SIERRA No. 515, PH,  COL. LOMAS DE SANTA FE, C.P. 01219, DELEG. ÁLVARO OBREGÓN, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(726, "Banco Nacional del ejército y armada S.N.C.", "OIC26", "BANJERCITO", 0, 26, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INDUSTRIA MILITAR No. 1055, 3ER PISO, COL. LOMAS DE SOTELO, DELEG. MIGUEL HIDALGO, C.P. 11200, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(727, "Caminos y Puentes Federales de ingresos y servicios conexos", "OIC27", "CAPUFE", 0, 27, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA CUERNAVACA-TEPOZTLÁN, No. 201, 1ER PISO, COL. CHAMILPA, C.P. 62210, CUERNAVACA, MORELOS", 3));
            //lstDatos.Add(new AdmAreaMdl(728, "Casa de Moneda de México", "OIC28", "CMM", 0, 28, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. COMISIÓN FEDRAL DE ELECTRICIDAD. No 200, MZA. 50 ZONA INDUSTRIAL 1ra. SECCIÓN, C.P. 78395, SAN LUIS POTOSÍ, S.L.P.", 3));
            //lstDatos.Add(new AdmAreaMdl(729, "Centro de capacitación cinematográfica A.C.", "OIC29", "CCC", 0, 29, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "ATLETAS No. 2, EDIFICIO HERMANOS SOLER, MEZANINE, COL. COUNTRY CLUB, DELEG. COYOACÁN, C.P. 04220, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(730, "Centro de enseñanza técnica industrial", "OIC30", "CETI", 0, 30, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE NUEVA ESCOCIA NO. 1885, EDIFICIO P.A., FRACC. PROVIDENCIA 5TA SECCIÓN, C.P. 44638, GUADALAJARA, JALISCO", 3));
            //lstDatos.Add(new AdmAreaMdl(731, "Centro de ingeniería y desarrollo industrial", "OIC31", "CIDESI", 0, 31, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PLAYA PIE DE LA CUESTA No. 702, FRACC.  HABITACIONAL SAN PABLO, C.P. 76130, QUERÉTARO, QUERÉTARO", 3));
            //lstDatos.Add(new AdmAreaMdl(732, "Centro de Investigación científica de yucatán A.C.", "OIC32", "CICY", 0, 32, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE 43, No. 130, COL. CHUBURNÁ DE HIDALGO, MÉRIDA, YUCATÁN C.P.  97200", 3));
            //lstDatos.Add(new AdmAreaMdl(733, "Centro de Investigación científica y de educación superior de ensenada baja california", "OIC33", "CICESE", 0, 33, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA ENSENADA -TIJUANA NO. 3918,  ZONA PLAYITAS, EDIFICIO ADMINISTRATIVO 3ER PISO, OFICINA 303, C.P. 22860, ENSENADA, B.C.", 3));
            //lstDatos.Add(new AdmAreaMdl(734, "Centro de Investigación e innovación en tecnología de la información y comunicación", "OIC34", "INFOTEC", 0, 34, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AVE. SAN FERNANDO No. 37,  COL. TORIELLO GUERRA, C.P. 14050, DELEG. TLALPAN, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(735, "Centro de Investigación en alimentación y desarrollo A.C.", "OIC35", "CIAD", 0, 35, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA A LA VICTORIA KM. 0.6, COL. LA VICTORIA, C.P. 83304, HERMOSILLO, SONORA", 3));
            //lstDatos.Add(new AdmAreaMdl(736, "Centro de Investigación en geografía y geomática  ing.jorge l.tamayo A.C.", "OIC36", "CIGGET", 0, 36, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE MÓNACO 276-A COL. SAN ANDRÉS ZACAHUITZCO, BENITO JUÁREZ C.P. 03550 MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(737, "Centro de Investigación en matemáticas A.C.", "OIC37", "CIMAT", 0, 37, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "JALISCO S/N, COL. VALENCIANA, C.P. 36023, GUANAJUATO, GUANAJUATO", 3));
            //lstDatos.Add(new AdmAreaMdl(738, "Centro de Investigación en materiales avanzados S.C.", "OIC38", "CIMAV", 0, 38, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. MIGUEL DE CERVANTES No. 120, COMPLEJO INDUSTRIAL CHIHUAHUA, C.P. 31136, CHIHUAHUA, CHIHUAHUA", 3));
            //lstDatos.Add(new AdmAreaMdl(739, "Centro de Investigación en química aplicada", "OIC39", "CIQA", 0, 39, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD ING. ENRIQUE REYNA HERMOSILLO No. 140, COL. SAN JOSÉ DE LOS CERRITOS,  C.P. 25294 SALTILLO, COAHUILA", 3));
            //lstDatos.Add(new AdmAreaMdl(740, "Centro de Investigación y asistencia en tecnología y diseño del estado de jalisco A.C.", "OIC40", "CIATEJ", 0, 40, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. NORMALISTAS 800, COL. COLINAS DE LA NORMAL, C.P. 44270, GUADALAJARA, JALISCO", 3));
            //lstDatos.Add(new AdmAreaMdl(741, "Centro de Investigación y de estudios avanzados del IPN", "OIC41", "CINVESTAV", 0, 41, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSTITUTO POLITÉCNICO NACIONAL No. 2508, COL. SAN PEDRO ZACATENCO, C.P. 07360, DELEG. GUSTAVO A. MADERO, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(742, "Centro de Investigación y desarrollo tecnológico en electroquímica", "OIC42", "CIDETEC", 0, 42, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CAMINO A LOS OLVERA NO. 44, P.B., OFICINA DE VINCULACIÓN, COL. LOS OLVERA, C.P. 76904, CORREGIDORA, QUERETARO", 3));
            //lstDatos.Add(new AdmAreaMdl(743, "Centro de Investigación y docencia económica A.C.", "OIC43", "CIDE", 0, 43, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA MÉXICO TOLUCA NO. 3655, COL. LOMAS DE SANTA FE, DELEG. ÁLVARO OBREGON, D.F., C.P. 01219", 3));
            //lstDatos.Add(new AdmAreaMdl(744, "Centro de Investigación y seguridad Nacional", "OIC44", "CISEN", 0, 44, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PATRICIO SANZ NO. 1609, TORRE A, PISO 2, COL. DEL VALLE, DELEG. BENITO JUÁREZ. C.P.  03100, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(745, "Centro de Investigaciones biológicas del noroeste S.C.", "OIC45", "CIBNOR", 0, 45, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "KM. 1 CARRETERA A SAN JUAN DE LA COSTA, PREDIO EL COMITÁN C.P. 23605 LA PAZ, BAJA CALIFORNIA SUR", 3));
            //lstDatos.Add(new AdmAreaMdl(746, "Centro de Investigaciones en óptica A.C.", "OIC46", "CIO", 0, 46, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "LOMA DEL BOSQUE No. 115, PISO 2, OFICINA 237, COL. LOMAS DEL CAMPESTRE; C.P. 37150, LEÓN GUANAJUATO", 3));
            //lstDatos.Add(new AdmAreaMdl(747, "Centro de Investigaciones y estudios superiores en antropología social", "OIC47", "CIESAS", 0, 47, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE NIÑO JESÚS No. 251,  COL. LA JOYA, C.P. 14090, DELEG. TLALPAN, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(748, "Centro Nacional de control de energía", "OIC48", "CENACE", 0, 48, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD. ADOLFO LÓPEZ MATEOS NO. 2157, PISO 10, COL. LOS ALPES, C.P. 01010, DELEG. ÁLVARO OBREGÓN, CIUDAD DE MÉXICO,", 3));
            //lstDatos.Add(new AdmAreaMdl(749, "Centro Nacional de control del gas natural", "OIC49", "CENAGAS", 0, 49, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR NO. 730, PISO 1 Y 2, COL. DEL VALLE, DELEG. BENITO JUÁREZ, C.P. 03104, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(750, "Centro Nacional de metrología", "OIC50", "CENAM", 0, 50, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "EDIFICIO A, DEL KM. 4.5, CARRETERA A LOS CUÉS, EL MARQUÉS, , QUERÉTARO, C.P. 76246", 3));
            //lstDatos.Add(new AdmAreaMdl(751, "Centro Regional de alta especialidad de chiapas", "OIC51", "CRAECHIS", 0, 51, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD. SU SANTIDAD JUAN PABLO II, S/N, 3er PISO, COL. JOSÉ CASTILLO TIELEMANS, TUXTLA GUTÍERREZ CHIAPAS, C.P. 29070", 3));
            //lstDatos.Add(new AdmAreaMdl(752, "Centros de integración juvenil", "OIC52", "CINJUVE", 0, 52, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "MIER Y PESADO No. 141,  PISO 4, COL. DEL VALLE, DELEG. BENITO JUAREZ, C.P. 03100, MEXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(753, "Ciatec, A.C.", "OIC53", "CIATEC", 0, 53, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "OMEGA No. 201, FRACC.  INDUSTRIAL DELTA, C.P. 37545, LEÓN, GUANAJUATO", 3));
            //lstDatos.Add(new AdmAreaMdl(754, "Ciateq A.C.", "OIC54", "CIATEQ", 0, 54, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALZADA  DE RETABLO No. 150, COL. CONSTITUYENTES FOVISSSTE, C.P. 76150,  QUERETARO, QUERETARO", 3));
            //lstDatos.Add(new AdmAreaMdl(755, "Colegio de bachilleres", "OIC55", "COLBACH", 0, 55, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PROLONGACIÓN RANCHO VISTA HERMOSA, No. 105, 2o PISO,  ALA NORTE, COL. LOS GIRASOLES, DELEG. COYOACÁN, C.P. 04920, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(756, "Colegio de postgraduados", "OIC56", "COLPOS", 0, 56, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "KM. 36.5 CARRETERA MÉXICO TEXCOCO, 3ER PISO EDIFICIO FRANCISCO MERINO RABAGO, MONTECILLO, TEXCOCO, ESTADO DE MÉXICO, C.P. 56230", 3));
            //lstDatos.Add(new AdmAreaMdl(757, "Colegio Nacional de educación profesional técnica", "OIC57", "CONALEP", 0, 57, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE 16 DE SEPTIEMBRE NO. 147 NORTE, COLONIA LÁZARO CÁRDENAS METEPEC, MÉXICO, D.F.C.P.  52148", 3));
            //lstDatos.Add(new AdmAreaMdl(758, "Comisión de operación y fomento de actividades académicas del IPN", "OIC58", "COFAA", 0, 58, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE TRES GUERRAS No. 27,  COL. CENTRO, C.P. 06040, DELEG. CUAUHTÉMOC, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(759, "Comisión ejecutiva de atención a victimas", "OIC59", "CEAV", 0, 59, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD ADOLFO LÓPEZ MATEOS No. 101,  PISO 7, COL., TIZAPÁN SAN ÁNGEL, DELEG. ÁLVARO OBREGÓN ,  C.P. 01090, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(760, "Comisión Federal de electricidad", "OIC60", "CFE", 0, 60, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. CUAUHTÉMOC NO. 536, PISO 4, COL. NARVARTE, C.P. 03020, DELEG. BENITO JUÁREZ, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(761, "Comisión Federal para la protección  contra riesgos sanitarios", "OIC61", "COFEPRIS", 0, 61, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "MONTERREY NO. 33, PISO 9, COL. ROMA, DELG. CUAHUTÉMOC, C.P. 06700, CIUDAD DE MÉXICO", 3));
            //lstDatos.Add(new AdmAreaMdl(762, "Comisión Nacional bancaria y de valores", "OIC62", "CNBV", 0, 62, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 1971, TORRE SUR, 3er PISO, COL. GUADALUPE INN, DELEG. ÁLVARO OBREGÓN, C.P. 01020, CIUDAD DE MÉXICO", 3));
            //lstDatos.Add(new AdmAreaMdl(763, "Comisión Nacional de acuacultura y pesca", "OIC63", "CONAPESCA", 0, 63, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. CAMARÓN SÁBALO S/N, ESQ. AV. TIBURON,  FRACC. SÁBALO COUNTRY CLUB,  MAZATLÁN, SINALOA.   CP.82110", 3));
            //lstDatos.Add(new AdmAreaMdl(764, "Comisión Nacional de arbitraje médico", "OIC64", "CONAMED", 0, 64, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "MITLA No. 250, COL. VERTIZ-NARVARTE, DELEG. BENITO JUAREZ, C. P. 03020, MEXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(765, "Comisión Nacional de cultura física y deporte", "OIC65", "CONADE", 0, 65, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CAMINO A SANTA TERESA No. 482, COL. PEÑA POBRE, DELEG. TLALPAN, C.P. 14060, MEXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(766, "Comisión Nacional de hidrocarburos", "OIC66", "CNH", 0, 66, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. VITO ALESSIO ROBLES 174-PISO 8, DELEG. ÁLVARO OBREGÓN, CP.01020 MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(767, "Comisión Nacional de las zona áridas", "OIC67", "CONAZA", 0, 67, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD. VITO ALESSIO ROBLES NO. 2565, COL. NAZARIO S. ORTIZ GARZA, C.P. 25100, SALTILLO COAHUILA", 3));
            //lstDatos.Add(new AdmAreaMdl(768, "Comisión Nacional de libros de texto gratuitos", "OIC68", "CONALITEG", 0, 68, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "RAFAEL CHECA No. 2,  PISO 2. COL. HUERTA DEL CARMEN, DELEG. ÁLVARO OBREGÓN, C.P. 01000, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(769, "Comisión Nacional de los salarios mínimos", "OIC69", "CONASAMI", 0, 69, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. CUAUHTÉMOC No. 14, 3er PISO, COL. DOCTORES, DELEG. CUAUHTÉMOC, C.P. 06720, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(770, "Comisión Nacional de protección social en salud", "OIC70", "CNPSS", 0, 70, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "GUSTAVO E CAMPA NO. 54, 1ER PISO, COL. GUADALUPE INN, DELEG. ÁLVARO OBREGPON, C.P. 01020, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(771, "Comisión Nacional de seguridad nuclear y salvaguardias", "OIC71", "CNSNS", 0, 71, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "DR. BARRAGÁN No. 779, 5o PISO, COL. NARVARTE, DELEG. BENITO JUÁREZ, C.P. 03020, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(772, "Comisión Nacional de seguros y fianzas", "OIC72", "CNSF", 0, 72, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 1971, CONJUNTO INMOBILIARIO PLAZA INN, TORRE SUR, 2o PISO, COL. GUADALUPE INN, DELEG. ÁLVARO OBREGÓN, C.P. 01020, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(773, "Comisión Nacional de vivienda", "OIC73", "CONAVI", 0, 73, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. PRESIDENTE MASARYK NO. 214, PISO 1, COL. BOSQUE DE CHAPULTEPEC, DELEG. MIGUEL HIDALGO, MÉXICO, D.F. 11580.", 3));
            //lstDatos.Add(new AdmAreaMdl(774, "Comisión Nacional del agua", "OIC74", "CONAGUA", 0, 74, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR No. 2416,  2° PISO ALA PONIENTE, COL. COPILCO EL BAJO, DELEG. COYOACÁN   C.P. 04340, CIUDAD DE MÉXICO.", 3));
            //lstDatos.Add(new AdmAreaMdl(775, "Comisión Nacional del sistema de ahorro para el retiro", "OIC75", "CONSAR", 0, 75, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CAMINO A SANTA TERESA NO. 1040, 4° PISO, COL. JARDINES EN LA MONTAÑA, DELEG. TLALPAN, C.P. 14210, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(776, "Comisión Nacional forestal", "OIC76", "CONAFOR", 0, 76, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. VALLARTA EJE PONIENTE NO. 6503, LOCAL B-22B, COL. CIUDAD GRANJA, ZAPOPAN, JALISCO, C.P. 45010.", 3));
            //lstDatos.Add(new AdmAreaMdl(777, "Comisión Nacional para el desarrollo de los pueblos indígenas", "OIC77", "CDI", 0, 77, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. MÉXICO COYOACÁN No. 343, 2° PISO, COL. XOCO, DELEG. BENITO JUÁREZ, C.P. 03330, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(778, "Comisión Nacional para la protección y defensa de los usuarios de servicios financieros", "OIC78", "CONDUSEF", 0, 78, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 762, PISO 9, COL. DEL VALLE, DELEG. BENITO JUÁREZ, C.P. 03100, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(779, "Comisión para la Regularización de la Tenencia de la Tierra", "OIC79", "CORETT", 0, 79, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "LIVERPOOL NO. 80, 5o PISO, COL. JUÁREZ, DELEGACIÓN CUAUHTÉMOC, C.P. 06000, CIUDAD DE MÉXICO", 3));
            //lstDatos.Add(new AdmAreaMdl(780, "Comisión reguladora de energia", "OIC80", "CRE", 0, 80, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "", 3));
            //lstDatos.Add(new AdmAreaMdl(781, "Compañía mexicana de exploraciones S.A.de C.V.", "OIC81", "COMESA", 0, 81, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. MARIANO ESCOBEDO NO. 366, PISO 2, COL. ANZURES, DELEG. MIGUEL HIDALGO, C.P. 11590, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(782, "Compañía operadora del centro cultural y turístico de tijuana", "OIC82", "CECUTT", 0, 82, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. PASEO DE LOS HÉROES No. 9350 ZONA URBANA RIÓ, TIJUANA, TIJUANA, B.C., C.P. 22010", 3));
            //lstDatos.Add(new AdmAreaMdl(783, "Consejeria jurídica del ejecutivo federal", "OIC83", "CJEF", 0, 83, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PALACIO NACIONAL, EDIFICIO XII ANEXO, 3ER. PISO, COL. CENTRO HISTÓRICO, DELEG. CUAUHTÉMOC, C.P. 06020, MÉXICO, D. F.", 3));
            //lstDatos.Add(new AdmAreaMdl(784, "Consejo de promoción turística de méxico S.A.de C.V.", "OIC84", "CPTM", 0, 84, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "VIADUCTO MIGUEL ALEMÁN No. 105. P.B. COL.  ESCANDÓN, DELEG. MIGUEL HIDALGO, C.P. 11800, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(785, "Consejo Nacional de ciencia y tecnología", "OIC85", "CONACYT", 0, 85, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR  No 1582,  4o PISO, ALA NORTE,   COL. CREDITO  CONSTRUCTOR, DELEG. BENITO JUAREZ, C.P. 03940, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(786, "Consejo Nacional de evaluación de la política de desarrollo social", "OIC86", "CONEVAL", 0, 86, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD. ADOLFO LÓPEZ MATEOS, NO. 160 PISO 1, COL. SAN ÁNGEL INN, DELEG. ÁLVARO OBREGÓN, C.P. 01060, MÉXICO., D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(787, "Consejo Nacional de fomento educativo", "OIC87", "CONAFE", 0, 87, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR NO. 421, TORRE B, PISO 4,  COL. HIPÓDROMO, DELEG. CUAUHTÉMOC C.P. 06100, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(788, "Consejo Nacional para el desarrollo y la inclusión de las personas con discapacidad", "OIC88", "CONADIS", 0, 88, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "THIERS NO. 251, P.B., COL. ANZURES, DELEG. MIGUEL HIDALGO, C.P. 11590, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(789, "Consejo Nacional para prevenir la discriminación", "OIC89", "CONAPRED", 0, 89, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE DANTE No 14 , COL. ANZURES, DELEG. MIGUEL HIDALGO, CP. 11590, MEXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(790, "Coordinación  Nacional de prospera programa  de inclusión social", "OIC90", "PROPERA", 0, 90, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR NO. 1480, PISO 13, COL.  BARRIO ACTIPAN DELEG. BENITO JUÁREZ   C.P. 03230, MÉXICO, .F.", 3));
            //lstDatos.Add(new AdmAreaMdl(791, "Corporación mexicana de Investigaciones en materiales S.A.de C.V.", "OIC91", "COMIMSA", 0, 91, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE CIENCIA Y TECNOLOGíA No. 790,  FRACC. SALTILLO 400, C.P. 25290, SALTILLO, COAHUILA", 3));
            //lstDatos.Add(new AdmAreaMdl(792, "Diconsa", "OIC92", "DICONSA", 0, 92, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR No. 3483,  P.B., COL. MIGUEL HIDALGO, DELEG. TLALPAN, C.P. 14020, MÉXICO,  D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(793, "Educal S.A.de C.V.", "OIC93", "EDUCAL", 0, 93, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. CEYLAN No. 450, COL. EUZKADI, C.P. 02660, DELEG. AZCAPOTZALCO, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(794, "El colegio de la frontera norte A.C.", "OIC94", "COLEF", 0, 94, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "KM 18.5 CARRETERA ESCÉNICA TIJUANA-ENSENADA S/N, SAN ANTONIO DEL MAR, TIJUANA, B.C. C.P. 22560", 3));
            //lstDatos.Add(new AdmAreaMdl(795, "El colegio de la frontera sur", "OIC95", "ECOSUR", 0, 95, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA PANAMERICANA Y PERIFÉRICO SUR S/N, BARRIO DE MARIA AUXILIADORA C.P. 29290, SAN CRISTÓBAL DE LAS CASAS CHIAPAS", 3));
            //lstDatos.Add(new AdmAreaMdl(796, "El colegio de michoacán A.C.", "OIC96", "COLMICH", 0, 96, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "MARTÍNEZ DE NAVARRETE No. 505, FRACC. LAS FUENTES, C.P. 59699 ZAMORA MICHOACÁN", 3));
            //lstDatos.Add(new AdmAreaMdl(797, "El colegio de San luis, A.C.", "OIC97", "COLSAN", 0, 97, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PARQUE DE MACUL NO. 155 , EDIFICIO 3, CURVA 1, NIVEL 2, FRACC. COLINAS DEL PARQUE, SAN LUIS POTOSI, S.L.P. C.P. 78299", 3));
            //lstDatos.Add(new AdmAreaMdl(798, "Estudios churubusco azteca, S.A.", "OIC98", "ECHASA", 0, 98, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "ATLETAS No. 2, EDIFICIO HERMANOS SOLER, MEZANINE, COL. COUNTRY CLUB, DELEG. COYOACÁN, C.P. 04220, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(799, "Exportadora de sal S.A.de C.V.y tranportadora de sal S.A.de C.V.", "OIC99", "ESSA", 0, 99, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BAJA CALIFORNIA S/N, COL. CENTRO, GUERRERO NEGRO, MULEGE, 23940,  BAJA CALIFORNIA SUR", 3));
            //lstDatos.Add(new AdmAreaMdl(800, "Ferrocarril del istmo de tehuantepec S.A.de C.V.", "OIC100", "FIT", 0, 100, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. EUGENIA No. 197, PISO 5 -B, COL. NARVARTE, DELEG. BENITO JUÁREZ, C.P. 03020, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(801, "Fideicomiso de fomento minero", "OIC101", "FIFOMI", 0, 101, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PUENTE DE TECAMACHALCO, No. 26, 3er PISO, COL. LOMAS DE CHAPULTEPEC, C.P. 11000, DELEG. MIGUEL HIDALGO, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(802, "Fideicomiso de formación y capacitación para el personal de la marina mercante Nacional", "OIC102", "FIDENA", 0, 102, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CUERNAVACA No. 5, COL. CONDESA,  DELEG. CUAUHTÉMOC, C.P. 06140, MÉXICO; D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(803, "Fideicomiso de los sistemas normalizado de competencia laboral y de certificación de competencia laboral", "OIC103", "CONOCER", 0, 103, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BARRANCA DEL MUERTO No. 275 3ER PISO, COL. SAN JOSÉ INSURGENTES DELEG. BENITO JUÁREZ, C.P. 03900, MÉXICO,D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(804, "Fideicomiso de riesgo compartido", "OIC104", "FIRCO", 0, 104, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES No. 489.  PISO 5, COL. HIPÓDROMO CONDESA, DELEG. CUAUHTEMOC, C. P. 06170, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(805, "Fideicomiso fondo Nacional de fomento ejidal", "OIC105", "FIFONAFE", 0, 105, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. REVOLUCIÓN No. 828, COL. MIXCOAC, DELEG.  BENITO JUÁREZ, C.P. 03910, MÉXICO,  D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(806, "Fideicomiso fondo Nacional de habitaciones populares", "OIC106", "FONHAPO", 0, 106, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 3483, P.B,  COL. MIGUEL HIDALGO, DELEG. TLALPAN, C.P. 14020, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(807, "Fideicomiso para la cineteca Nacional", "OIC107", "FICINE", 0, 107, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. PASEO DE LA REFORMA. No. 175, PISO 15, COL. CUAUHTEMOC, DELEG. CUAUHTEMOC, C.P. 06500, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(808, "Fideicomiso público promexico", "OIC108", "PROMEXICO", 0, 108, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CAMINO A SANTA TERESA NO. 1679, PISO 2, ALA NORTE,  COL. JARDINES DEL PEDREGAL, C.P. 01900, DELEG. ÁLVARO OBREGÓN,  CIUDAD DEMÉXICO.", 3));
            //lstDatos.Add(new AdmAreaMdl(809, "Financiera Nacional de desarrollo agropecuario,  rural, forestal y pesquero", "OIC109", "FINARURAL", 0, 109, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AGRARISMO 227,  COL. ESCANDÓN, C.P. 11800, DELEG. MIGUL HIDALGO,  MÉXICO, D. F.", 3));
            //lstDatos.Add(new AdmAreaMdl(810, "Fondo de capitalización e inversión del sector rural", "OIC110", "FOCIR", 0, 110, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CIRCUITO GUILLERMO GONZÁLEZ CAMARENA NO. 1000, PISO 3, COL. CENTRO DE CIUDAD SANTA FE, DELEG. ÁLVARO OBREGÓN C.P. 01210, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(811, "Fondo de cultura económica", "OIC111", "FCE", 0, 111, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA PICACHO AJUSCO No. 227, 6° PISO, COL. BOSQUES DEL PEDREGAL, DELEG. TLALPAN, C.P. 14738, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(812, "Fondo de garantía y fomento para la agricultura ganadería y avicultura(fondo) fondo especial para financiamientos agropecuarios(fefa) fondo especial de asistencia tecnica y garantia para creditos agropecuarios(fega) y fondo  de garantia y fomento para las actividades pesqueras(fopesca)", "OIC112", "FONDOS", 0, 112, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "ANTIGUA CARRETERA  A PÁTZCUARO, No. 8555, EDIF. C1, COL. EX-HACIENDA SAN JOSÉ LA HUERTA, C.P. 58342, MORELIA, MICHOACÁN", 3));
            //lstDatos.Add(new AdmAreaMdl(813, "Fondo de la vivienda del issste", "OIC113", "FOVISSSTE", 0, 113, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "MIGUEL NOREÑA No. 28, SÉPTIMO PISO, COL. SAN JOSÉ INSURGENTES, DELEG. BENITO JUÁREZ, C.P. 03900, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(814, "Fondo Nacional de fomento al turismo y empresas de participación accionaria", "OIC114", "FONATUR", 0, 114, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "TECOYOTITLA No.100. PRIMER PISO,  COL. FLORIDA, DELEG. ÁLVARO OBREGON, C. P. 01030, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(815, "Fondo Nacional para el fomento de las artesanías", "OIC115", "FONART", 0, 115, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE RÍO RHIN NO. 77, PISO 4, COL. CUAUHTÉMOC, DELEG. CUAUHTÉMOC, C.P. 06500, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(816, "Grupo aeroportuario de la ciudad de méxico S.A.de C.V.", "OIC116", "GACM", 0, 116, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR NO. 2453, TORRE MURANO, PISO 2, COL. TIZAPAN, C.P. 01090, DELEG. ÁLVARO OBREGÓN, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(817, "Hospital general de méxico", "OIC117", "HGM", 0, 117, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "DR. BALMIS No. 148, UNIDAD  206-E, COL. DOCTORES, DELEG. CUAUHTÉMOC, C.P. 06720, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(818, "Hospital general dr.manuel gea gonzález", "OIC118", "HGMGG", 0, 118, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALZADA DE TLALPAN 4800, COL. SECCIÓN XVI, DELEG. TLALPAN, C.P. 14080, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(819, "Hospital infantil de méxico federico gómez", "OIC119", "HIMFG", 0, 119, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "DR. MÁRQUEZ 162, EDIFICIO ARTURO MUNDET,  2o PISO, COL. DOCTORES, C.P. 06720, DELEG. CUAUHTEMOC, MÉXICO, D. F.", 3));
            //lstDatos.Add(new AdmAreaMdl(820, "Hospital juárez de méxico", "OIC120", "HJM", 0, 120, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV.  INSTITUTO POLITÉCNICO NACIONAL NO. 5160, EDIFICIO B 1ER PISO, ÁREA DE GOBIERNO,  COL. MAGDALENA DE LAS SALINAS DELEG. GUSTAVO A. MADERO C.P, 07760, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(821, "Hospital Regional de alta especialidad de ciudad victoria", "OIC121", "HRAECV", 0, 121, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "LIBRAMIENTO GUADALUPE VICTORIA S/N, COL. ÁREA DE PAJARITOS, C.P. 87087, CD. VICTORIA, TAMPS.", 3));
            //lstDatos.Add(new AdmAreaMdl(822, "Hospital Regional de alta especialidad de ixtapaluca", "OIC122", "HRAEI", 0, 122, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA FEDERAL MEXICO - PUEBLA KM. 34.5, PUEBLO DE ZOQUIAPAN, IXTAPALUCA, ESTADO DE MEXICO, C.P. 56530", 3));
            //lstDatos.Add(new AdmAreaMdl(823, "Hospital Regional de alta especialidad de la península de yucatán", "OIC123", "HRAEPY", 0, 123, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE 7 NO. 433, POR 20 Y 22, FRACC. ALTABRISA, MÉRIDA, YUCATÁN, C.P. 97130", 3));
            //lstDatos.Add(new AdmAreaMdl(824, "Hospital Regional de alta especialidad de oaxaca", "OIC124", "HRAEO", 0, 124, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "ALDAMA S/N , P.A., SAN BARTOLO COYOTEPEC, C.P. 71256,  OAXACA", 3));
            //lstDatos.Add(new AdmAreaMdl(825, "Hospital Regional de alta especialidad del bajío", "OIC125", "HRAEB", 0, 125, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD. MILENIO N° 130, COL. SAN CARLOS LA RONCHA, LEON, GUANAJUATO, CP.  37660", 3));
            //lstDatos.Add(new AdmAreaMdl(826, "Impresora y encuadernadora progreso S.A.de C.V.", "OIC126", "IEPSA", 0, 126, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. SAN LORENZO No. 244, 1ER PISO, COL. PARAJE SAN JUAN DELEG. IZTAPALAPA, C.P. 09830, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(827, "Instituto de ecologia A.C.", "OIC127", "INECOL", 0, 127, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA ANTIGUA A COATEPEC No. 351, EDIFI. B, 1ER PISO, COL. EL HAYA. C.P. 91070, XALAPA VERACRUZ", 3));
            //lstDatos.Add(new AdmAreaMdl(828, "Instituto de Investigaciones Dr. José María Luis Mora", "OIC128", "Instituto MORA", 0, 128, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BUFALO No. 172, COL. DEL VALLE,  BARRIO DE ACTIPAN, C.P. 03100, DELEG. BENITO JUÁREZ, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(829, "Instituto de Investigaciones eléctricas", "OIC129", "IIE", 0, 129, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE REFORMA NO. 113,EDIFICIO 12, 3ER PISO, COL. PALMIRA,  C.P. 62490, CUERNAVACA, MORELOS", 3));
            //lstDatos.Add(new AdmAreaMdl(830, "Instituto de seguridad de las fuerzas armadas mexicanas", "OIC130", "ISSFAM", 0, 130, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV.  INDUSTRIA MILITAR No. 1053, COL. LOMAS DE SOTELO, DELEG. MIGUEL HIDALGO, C.P. 11200, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(831, "Instituto de seguridad y servicios sociales de los trabajadores del estado", "OIC131", "ISSSTE", 0, 131, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. REVOLUCIÓN NO. 642, 2o Piso, COL. SAN PEDRO DE LOS PINOS, DELEG. ABENITO JUÁREZ C.P. 03800, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(832, "Instituto del fondo Nacional para el consumo de los trabajadores", "OIC132", "INFONACOT", 0, 132, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 452, 3er PISO, COL. ROMA SUR, DELEG. CUAUHTÉMOC, C. P. 06760, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(833, "Instituto Mexicano de cinematografía", "OIC133", "IMCINE", 0, 133, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. DIVISION DEL NORTE 2462, 5o. PISO, COL. PORTALES, DELEG. BENITO JUÁREZ, C.P. 03300. MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(834, "Instituto Mexicano de la juventud", "OIC134", "IMJUVE", 0, 134, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "SERAPIO RENDÓN No.76, MEZZANINE, COL. SAN RAFAEL, DELEG. CUAUHTÉMOC, C.P. 06470, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(835, "Instituto Mexicano de la propiedad industrial", "OIC135", "IMPI", 0, 135, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PERIFÉRICO SUR, No. 3106,  PISO 3, COL. JARDINES DEL PEDREGAL, C.P. 01900,  DELEG. ÁLVARO OBREGÓN, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(836, "Instituto Mexicano de la radio", "OIC136", "IMER", 0, 136, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE MARGARITAS No. 18, EDIFICIO 2, COL. FLORIDA, DELEG. ÁLVARO OBREGÓN, C.P. 01030, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(837, "Instituto Mexicano de tecnología del agua", "OIC137", "IMTA", 0, 137, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PASEO CUAUHNÁHUAC 8532, COL. PROGRESO, JIUTEPEC, MORELOS, C.P. 62550", 3));
            //lstDatos.Add(new AdmAreaMdl(838, "Instituto Mexicano del petróleo", "OIC138", "IMP", 0, 138, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "EJE CENTRAL LÁZARO CÁRDENAS  NORTE No. 152, COL. SAN BARTOLO ATEPEHUACAN, DELEG. GUSTAVO A. MADERO, C.P. 07730, MÉXICO D.F. EDIF. 13, P.A.,OFICINA 13-101E", 3));
            //lstDatos.Add(new AdmAreaMdl(839, "Instituto Mexicano del seguro social", "OIC139", "IMSS", 0, 139, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. REVOLICIÓN NO. 1586, COL. SAN ÁNGEL, DELG. ÁLVARO OBREGÓN, MÉXICO D.F.., C.P. 01000", 3));
            //lstDatos.Add(new AdmAreaMdl(840, "Instituto Nacional de antropología e historia", "OIC140", "INAH", 0, 140, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR 421 -3ER PISO, COL. HIPÓDROMO, DELEG. CUAHTÉMOC, C.P. 06100, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(841, "Instituto Nacional de astrofísica, Óptica y Electrónica", "OIC141", "INAOP", 0, 141, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE LUIS ENRIQUE ERRO NO. 1, SANTA MARÍA TONANTZINTLA,  SAN ANDRÉS CHOLULA, PUEBLA C.P. 72840", 3));
            //lstDatos.Add(new AdmAreaMdl(842, "Instituto Nacional de bellas artes y literatura", "OIC142", "INBAL", 0, 142, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "NUEVA YORK No. 224, COL. NÁPOLES, DELEG. BENITO JUAREZ, C.P. 03810, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(843, "Instituto Nacional de cancerología", "OIC143", "INCAN", 0, 143, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. SAN FERNANDO No. 2, PUERTA 1, EDIFICIO DE CONTRALORÍA, COL. BARRIO DEL NIÑO JESÚS, DELEG. TLALPAN, C.P. 14080, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(844, "Instituto Nacional de cardiología ignacio chávez", "OIC144", "CARDIOLOGÍA", 0, 144, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE JUAN BADIANO No.1,  COL. SECCIÓN XVI, DELG. TLALPAN, C.P. 14080, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(845, "Instituto Nacional de ciencias médicas y nutrición  salvador zubiran", "OIC145", "INCMNSZ", 0, 145, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "VASCO DE QUIROGA No. 15,  PLANTA ALTA,, COL. BELISARIO DOMÍNGUEZ SECCIÓN XVI, DELEG. TLALPAN, C.P. 14000, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(846, "Instituto Nacional de ciencias penales", "OIC146", "INACIPE", 0, 146, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "MAGISTERIO NACIONAL No. 113, EDIFICIO A, COL. TLALPAN, DELEG. TLALPAN, C.P. 14000, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(847, "Instituto Nacional de enfermedades respiratorias ismael cosío villegas", "OIC147", "INER", 0, 147, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALZADA DE TLALPAN No. 4502, COL. SECCIÓN XVI, DELEG. TLALPAN, C.P. 14080, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(848, "Instituto Nacional de Investigaciones forestales, agricolas y pecuarias", "OIC148", "INIFAP", 0, 148, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. PROGRESO NO. 5, 1ER PISO,  COL. BARRIO DE SANTA CATARINA, DELEG. COYOACÁN, C.P. 04010, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(849, "Instituto Nacional de Investigaciones Nucleares", "OIC149", "ININ", 0, 149, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "KM. 36.5 CARRETERA FEDERAL MÉXICO-TOLUCA, S/N,  CASETA G LA MARQUESA OCOYOACAC  EDO. DE MEX. CP.52750", 3));
            //lstDatos.Add(new AdmAreaMdl(850, "Instituto Nacional de la economia social", "OIC150", "INAES", 0, 150, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. PATRIOTISMO No. 711, EDIFICIO B, PISO 1, COL. SAN JUAN MIXCOAC, DELEG. BENITO JUÁREZ, MÉXICO, D.F.,", 3));
            //lstDatos.Add(new AdmAreaMdl(851, "Instituto Nacional de la infraestructura física educativa", "OIC151", "INIFED", 0, 151, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "VITO ALESSIO ROBLES No. 380, COL. FLORIDA, DELEG. ÁLVARO OBREGÓN, C.P. 01030,  MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(852, "Instituto Nacional de las mujeres", "OIC152", "INMUJERES", 0, 152, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PERIFERICO SUR NO. 3325, PISO 5, DELEGACIÓN MAGDALENA CONTRERAS, MEXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(853, "Instituto Nacional de las personas adultas mayores", "OIC153", "INAPAM", 0, 153, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "SAN FRANCISCO No. 1825, P.A., COL. ACTIPAN DEL VALLE, DELEG. BENITO JUÁREZ, C.P. 03230, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(854, "Instituto Nacional de lenguas indígenas", "OIC154", "INALI", 0, 154, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PRIVADA DE RELOX NO. 16-A 3ER PISO COL. CHIMALISTAC  C.P.  DEL ÁLVARO OBREGÓN C.P. 01070 MEXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(855, "Instituto Nacional de medicina genómica", "OIC155", "INMEGEN", 0, 155, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PERIFÉRICO SUR NO. 4809,   COL. ARENAL TEPEPAN, DELEG. TLALPAN, C.P. 14610, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(856, "Instituto Nacional de migración", "OIC156", "INAMI", 0, 156, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "HOMERO 1832, PISO 9, COL. LOS MORALES, POLANCO, DELEG. MIGUEL HIDALGO, C.P. 11510, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(857, "Instituto Nacional de neurología y neurocirugía manuel velasco suárez", "OIC157", "INNN", 0, 157, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 3877, COL.LA FAMA, DELEG. TLALPAN, C.P. 14269, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(858, "Instituto Nacional de pediatría", "OIC158", "INP", 0, 158, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 3700-C, COL. INSURGENTES CUICUILCO, DELEG. COYOACÁN, C.P. 04530, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(859, "Instituto Nacional de perinatología isidro espinosa de los reyes", "OIC159", "INPER", 0, 159, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "MONTES URALES No. 800, EDIFICIO C, COL. LOMAS DE VIRREYES, DELEG. MIGUEL HIDALGO, C.P. 11000; MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(860, "Instituto Nacional de psiquiatría ramon de la fuente muñiz", "OIC160", "PSIQUIATRÍA", 0, 160, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALZADA MÉXICO-XOCHIMILCO No. 101, P.A., COL. SAN LORENZO HUÍPULCO, C.P. 14370, DELEG. TLALPAN, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(861, "Instituto Nacional de rehabilitación", "OIC161", "INR", 0, 161, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALZADA MÉXICO XOHIMILCO NO. 289 CUERPO 8, 2DO PISO COL. ARENAL DE GUADALUPE, DELEG. TLALPÁN  MÉXICO, D.F. C.P. 14389", 3));
            //lstDatos.Add(new AdmAreaMdl(862, "Instituto Nacional de salud pública", "OIC162", "INSP", 0, 162, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. UNIVERSIDAD No. 655, OFICINA S-25, COL. SANTA MARIA AHUACATITLÁN, CUERNAVACA, MORELOS C.P. 62100", 3));
            //lstDatos.Add(new AdmAreaMdl(863, "Instituto Nacional para el desarrollo de capacidades del sector rural A.C.", "OIC163", "INCA RURAL", 0, 163, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. CUAUHTÉMOC NO.1230, 3ER PISO, COL. SANTA CRUZ ATOYAC, C.P. 03310,  DELEG. BENITO JUÁREZ, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(864, "Instituto Nacional para la educación de los adultos", "OIC164", "INEA", 0, 164, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE FRANCISCO MARQUES NO. 160, 3ER PISO, COL. CONDESA, DELEG.  CUAUHTÉMOC C.P. 06140 MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(865, "Instituto para la protección al ahorro bancario", "OIC165", "IPAB", 0, 165, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "HAMBURGO No. 213, PISO 6, COL. JUÁREZ,  DELEG. CUAUHTÉMOC,  C.P. 06600, MEXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(866, "Instituto politécnico Nacional", "OIC166", "IPN", 0, 166, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. MIGUEL OTHON DE MENDIZÁBAL S/N., ESQ.  MIGUEL BERNARD, COL. RESIDENCIAL LA ESCALERA, C.P. 07738, DELEG.GUSTAVO A. MADERO; MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(867, "Instituto potosino de Investigación cientifica y tecnologica A.C.", "OIC167", "IPICYT", 0, 167, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CAMINO A LA PRESA SAN JOSE NO. 2055 COL. LOMAS 4a SECCIÓN, CP.78216  SAN LUIS POTOSÍ, SAN LUIS POTOSI", 3));
            //lstDatos.Add(new AdmAreaMdl(868, "Laboratorios de biológicos y reactivos de méxico S.A.de C.V.", "OIC168", "BIRMEX", 0, 168, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. EJE CENTRAL LÁZARO CÁRDENAS NO. 911, COL. VÉRTIZ NÁRVARTE, DELEG. BENITO JUÁREZ, C.P. 03600", 3));
            //lstDatos.Add(new AdmAreaMdl(869, "Liconsa s.a de C.V.", "OIC169", "LICONSA", 0, 169, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "RICARDO TORRES No. 1,  TERCER PISO, ALA  B, FRACC. LOMAS DE SOTELO, NAUCALPAN DE JUÁREZ. ESTADO. DE MÉXICO, C.P. 53390.", 3));
            //lstDatos.Add(new AdmAreaMdl(870, "Lotería Nacional para la asistencia pública", "OIC170", "LOTENAL", 0, 170, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. DE LA REPÚBLICA No. 117, PISO 2o PISO, COL. TABACALERA, DELEG. CUAUHTÉMOC, C. P. 06030, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(871, "Nacional financiera S.N.C., IBD", "OIC171", "NAFIN", 0, 171, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR No. 1971, TORRE IV, PISO 9, COL. GUADALUPE INN, DELEG. ÁLVARO OBREGÓN, C.P. 01020, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(872, "Notimex agencia de noticias del estado Mexicano", "OIC172", "NOTIMEX ANEM", 0, 172, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "EJE 3 SUR BAJA CALIFORNIA NO. 200, PISO 6, COL. ROMA SUR, DELEG. CUAUHTÉMOC, C.P. 06700, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(873, "Órgano administrativo desconcentrado prevención y readaptación social", "OIC173", "PYRS", 0, 173, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE TUXPAN No.85, , 3ER PISO, COL. ROMA SUR, DELEG. CUAUHTEMOC, C,P, 06760 MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(874, "Patronato de obras e instalaciones del Instituto politécnico Nacional", "OIC174", "POI", 0, 174, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "JUAN O´ GORMAN No. 283, COL. LA ESCALERA,  C.P. 07310, UNIDAD PROFESIONAL ADOLFO LÓPEZ MATEOS,  DELEG. GUSTAVO A. MADERO, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(875, "Petróleos Mexicanos", "OIC175", "PEMEX", 0, 175, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. MARINA NACIONAL No. 329, TORRE EJECUTIVA PISO 9, COL. PETRÓLEOS MEXICANOS, DELEG. MIGUEL HIDALGO, C.P. 11311, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(876, "Policía federal", "OIC176", "PF", 0, 176, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE SIETE NO. 95, PISO 1, ESQ. VIADUCTO RÍO BECERRA, COL. SAN PEDRO DE LOS PINOS, DELEG. BENITO JUÁREZ, C.P. 03800, MEXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(877, "Rresidencia de la República", "OIC177", "PRESIDENCIA", 0, 177, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. NUEVO LEÓN NO. 210, PISO 3, COL. HIPÓDROMO, DELEG. CUAUHTÉMOC, C.P. 06100, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(878, "Procuraduría agraria", "OIC178", "P.A.", 0, 178, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. 5 DE MAYO NO. 19 2o PISO, COL. CENTRO, C.P. 06000, DELEG. CUAUHTÉMOC, MEXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(879, "Procuraduría de la defensa del contribuyente", "OIC179", "PFC", 0, 179, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "", 3));
            //lstDatos.Add(new AdmAreaMdl(880, "Procuraduría Federal del consumidor", "OIC180", "PROFECO", 0, 180, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. JOSÉ VASCONCELOS No. 208, 9o PISO, COL. CONDESA, C.P. 06140, DELEG. CUAUHTÉMOC, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(881, "Procuraduría general de la república", "OIC181", "PGR", 0, 181, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD ADOLFO LÓPEZ MATEOS No. 101,  PISO 7, COL., TIZAPÁN SAN ÁNGEL, DELEG. ÁLVARO OBREGÓN ,  C.P. 01090, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(882, "Productora Nacional de biológicos veterinarios", "OIC182", "PRONABIVE", 0, 182, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "IGNACIO ZARAGOZA No. 75, COL. LOMAS ALTAS, DELEG. MIGUEL HIDALGO, C.P. 11950, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(883, "Pronósticos para la asistencia pública", "OIC183", "PRONOSTICOS", 0, 183, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR No. 1397, COL. INSURGENTES MIXCOAC, DELEG. BENITO JUÁREZ, C.P. 03920, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(884, "Registro agrario Nacional", "OIC184", "RAN", 0, 184, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. 20 DE NOVIEMBRE No. 195, PRIMER PISO, COL. CENTRO, DELEG. CUAUHTÉMOC, C.P. 06080 MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(885, "Secretaria de agricultura ganadería y desarrollo rural, pesca y alimentación", "OIC185", "SAGARPA", 0, 185, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 489, PH3, COL. HIPÓDROMO CONDESA, DELEG. CUAUHTEMOC, C.P. 06170, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(886, "Secretaría de comunicaciones y transportes", "OIC186", "SCT", 0, 186, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. UNIVERSIDAD Y  XOLA S/N, CUERPO A, 2o PISO ALA PONIENTE, COL. NARVARTE, DELEG. BENITO JUÁREZ, C.P. 03028, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(887, "Secretaría de cultura", "OIC187", "SC", 0, 187, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. PASEO DE LA REFORMA. No. 175, PISO 15, COL. CUAUHTEMOC, DELEG. CUAUHTEMOC, C.P. 06500, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(888, "Secretaría de desarrollo agrario territorial y urbano", "OIC188", "SEDATU", 0, 188, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AVE. PASEO DE LA REFORMA NO. 99, PISO 14, COL. TABACALERA, DELEG. CUAUHTÉMOC, C.P. 06030, CIUDAD DE MÉXICO", 3));
            //lstDatos.Add(new AdmAreaMdl(889, "Secretaría de desarrollo social", "OIC189", "SEDESOL", 0, 189, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. PASEO DE LA REFORMA No. 116, PISO 11°, COL. JUÁREZ, DELEG. CUAUHTÉMOC, C.P. 06600, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(890, "Secretaría de economía", "OIC190", "SE", 0, 190, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD ADOLFO LÓPEZ MATEOS No. 3025, SÉPTIMO PISO, COL. SAN JERÓNIMO ACULCO, DELEG. MAGDALENA CONTRERAS. C.P 10400, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(891, "secretaria de educación pública", "OIC191", "SEP", 0, 191, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. UNIVERSIDAD No. 1074, 2o PISO, COL. XOCO, DELEG. BENITO JUÁREZ, C. P. 03330, CIUDAD DE MÉXICO", 3));
            //lstDatos.Add(new AdmAreaMdl(892, "Secretaría de energía", "OIC192", "SENER", 0, 192, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR No. 890, 5 PISO, OFICINA 504, COL. DEL VALLE, DELEG. BENITO JUÁREZ, C.P. 03100  MÉXICO , D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(893, "Secretaría de gobernación", "OIC193", "SEGOB", 0, 193, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BAHÍA DE SANTA BARBARA 193, PISO 1, COL. VERÓNICA ANZURES, DELEG. MIGUEL HIDALGO, C.P. 11300, MEXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(894, "Secretaría de hacienda y crédito publico", "OIC194", "SHCP", 0, 194, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. MÉXICO COYOACÁN No. 318, 4° PISO, COL.GENERAL ANAYA, DELEG. BENITO JUÁREZ., C.P. 03340, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(895, "Secretaría de la defensa Nacional", "OIC195", "SEDENA", 0, 195, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD MANUEL ÁVILA CAMACHO S/N, ESQ. INDUSTRIA MILITAR  COL. LOMAS DE SOTELO, DELEG. MIGUEL HIDALGO; C.P. 11640, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(896, "Secretaría de medio ambiente y recursos naturales", "OIC196", "SEMARNAT", 0, 196, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. EJERCITO NACIONAL NO. 223, PISO 20, COL. ANÁHUAC, DELEG. MIGUEL HIDALGO, C.P. 11320, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(897, "Secretaría de relaciones exteriores", "OIC197", "SRE", 0, 197, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PLAZA JUÁREZ No. 20, PISO 19, COL. CENTRO, DELEG. CUAUHTÉMOC, C.P. 06010, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(898, "Secretaría de salud", "OIC198", "SSA", 0, 198, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR NO. 1685 4o PISO COL.GUADALUPE INN DELEG.ALVARO OBREGÓN, MÉXICO, D.F.C.P., 01020", 3));
            //lstDatos.Add(new AdmAreaMdl(899, "Secretaría de turismo", "OIC199", "SECTUR", 0, 199, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE SCHILLER 138, 1ER PISO, COL. CHAPULTEPEC MORALES, DELEG. MIGUEL HIDALGO; C.P. 11587, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(900, "Secretaría del trabajo y prevision social", "OIC200", "STYPS", 0, 200, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. FÉLIX CUEVAS, No. 301, 7° PISO, COL. DEL VALLE SUR, DELEG. BENITO JUÁREZ, C.P. 03100, MÉXICO,  D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(901, "Secretariado ejecutivo del sistema Nacional de seguridad pública", "OIC201", "SESNSP", 0, 201, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALZADA GENERAL MARIANO ESCOBEDO NO. 456,  COL. ANZURES, DELEG. MIGUEL HIDALGO MÉXICO, D.F.  C.P. 11590", 3));
            //lstDatos.Add(new AdmAreaMdl(902, "Servicio de Administración  tributaria", "OIC202", "SAT", 0, 202, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. HIDALGO 77, MÓDULO IV, 5o PISO, COL, GUERRERO, DELEG. CUAUHTÉMOC C.P. 06300 MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(903, "Servicio de Administración  y enajenación  de bienes", "OIC203", "SAE", 0, 203, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "INSURGENTES SUR 1931, PISO 3, COL. GUADALUPE INN, C. P. 01020, DELEG. ÁLVARO OBREGÓN, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(904, "Servicio de protección federal", "OIC204", "SEPROFED", 0, 204, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "MIGUEL ANGEL DE QUEVEDO NO. 915, SUBSOTANO, COL ROSEDAL, DELEG. COYOACAN, C.P. 04330, MEXICO DF.", 3));
            //lstDatos.Add(new AdmAreaMdl(905, "Servicio Geológico Mexicano", "OIC205", "SGM", 0, 205, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "BLVD. FELIPE ÁNGELES KM. 93.50-4; COL. VENTA PRIETA, 42080, PACHUCA, HIDALGO", 3));
            //lstDatos.Add(new AdmAreaMdl(906, "Servicio Nacional de sanidad inocuidad y calidad alimentaria", "OIC206", "SENASICA", 0, 206, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV, INSURGENTES SUR 489, PH1, COL. HIPÓDROMO, DELEG. CUAUHTEMOC, C.P. 06100, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(907, "Servicio postal Mexicano", "OIC207", "SEPOMEX", 0, 207, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE VENUSTIANO CARRANZA NO. 32, 4o PISO, COL. CENTRO, DELEG. CUAUHTÉMOC, MÉXICO, D.F. C.P. 06000", 3));
            //lstDatos.Add(new AdmAreaMdl(908, "Servicios a la navegación en el espacio aéreo Mexicano", "OIC208", "SENEAM", 0, 208, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. 602, NO. 161, P.B.,  ZONA FEDERAL DEL AEROPUERTO INTERNACIONAL DE LA CIUDAD DE MÉXICO,  DELEG. VENUSTIANO CARRANZA, C.P. 15620, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(909, "Servicios aeroportuarios de la ciudad de méxico S.A.de.C.V.", "OIC209", "SACM", 0, 209, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "OFICINA 91 MEZZANINE DEL AEROPUERTO INTERNACIONAL BENITO JUARÉZ CIUDAD DE MÉXICO, AV. CAPITÁN CARLOS LEÓN S/N, COL. PEÑÓN DE LOS BAÑOS, DELEG. VENUSTIANO CARRANZA, C.P. 15620, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(910, "Sistema Nacional para el desarrollo Integral de la familia", "OIC210", "DIF", 0, 210, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "PROLONGACIÓN XOCHICALCO 947, P.B., COL. SANTA CRUZ ATOYAC, DELEG. BENITO JUÁREZ, C.P. 03310, MÉXICO D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(911, "Sociedad hipotecaria Federal S.N.C.", "OIC211", "SHF", 0, 211, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV.EJERCITO NACIONAL N0 180,  COL. ANZURES, DELEG.  MIGUEL HIDALGO,  C.P. 11590, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(912, "Superissste", "OIC212", "SUPERISSSTE", 0, 212, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. INSURGENTES SUR NO. 899, 4o PISO,  COL. AMPLIACIÓN NÁPOLES C.P. 03840 DELEG. BENITO JUÁREZ, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(913, "Talleres gráficos de méxico", "OIC213", "TGM", 0, 213, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "AV. CANAL DEL NORTE No. 80, 2o PISO, COL. FELIPE PESCADOR, DELEG. CUAUHTÉMOC, C.P. 06280,  MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(914, "Telecomunicaciones de méxico", "OIC214", "TELECOM", 0, 214, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "TORRE CENTRAL DE TELECOMUNICACIONES; EJE CENTRAL LÁZARO CÁRDENAS No. 567, PISO 5 ALA SUR,  COL. NARVARTE, C.P. 03020; DELEG. BENITO JUÁREZ, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(915, "Televisión metropolitana S.A.de C.V.", "OIC215", "TVMETRO", 0, 215, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CALLE ATLETAS, No. 2, EDIFICIO PEDRO INFANTE, PRIMER PISO, COL. COUNTRY CLUB, DELEG. COYOACAN, C.P. 04220, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(916, "Universidad pedagógica Nacional", "OIC216", "UPN", 0, 216, 6, 3, new DateTime(2000, 01, 01), new DateTime(2035, 12, 31), "CARRETERA AL AJUSCO No. 24, COL. HÉROES DE PADIERNA, DELEG. TLALPAN, C.P. 14200, MÉXICO, D.F.", 3));
            //lstDatos.Add(new AdmAreaMdl(917, "Instituto Nacional de Geografía e Historia", "OIC217", "INEGI", 0, 217, 6, 3, new DateTime(2000, 01, 01), new DateTime(2015, 12, 31), "N/A", 3));
            //lstDatos.Add(new AdmAreaMdl(918, "Luz y Fuerza del Centro", "OIC218", "LYC", 0, 218, 6, 3, new DateTime(2000, 01, 01), new DateTime(2009, 10, 11), "N/A", 3));
            //lstDatos.Add(new AdmAreaMdl(919, "Fideicomiso de Ferrocarriles Nacionales de Mexico", "OIC", "FNM", 0, 219, 6, 3, new DateTime(2000, 01, 01), new DateTime(2012, 12, 31), "N/A", 3));

            return lstArea;
        }
        public object dmlSelectAreaTipo( )
        {
            List<SIT_ADM_AREATIPO> lstAreaTipo = new List<SIT_ADM_AREATIPO>();

            lstAreaTipo.Add(new SIT_ADM_AREATIPO { atpclave = 1, atpdescripcion = "Estructura Orgánica" });
            lstAreaTipo.Add(new SIT_ADM_AREATIPO { atpclave = 2, atpdescripcion = "Estructura Interna Aux" });
            lstAreaTipo.Add(new SIT_ADM_AREATIPO { atpclave = 3, atpdescripcion = "Estructura Externa" });

            return lstAreaTipo;
        }
        public object dmlSelectAreasNivel( )
        {
            List<SIT_ADM_AREANIVEL> lstAreaNivel = new List<SIT_ADM_AREANIVEL>();
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 1, anldescripcion = "Secretaria de Estado" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 2, anldescripcion = "SubSecretario" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 3, anldescripcion = "Oficial Mayor" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 4, anldescripcion = "Titular de Unidad" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 5, anldescripcion = "Director General" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 6, anldescripcion = "Director General Adjunto" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 7, anldescripcion = "Director de Área" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 8, anldescripcion = "Subdirector de Área" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 9, anldescripcion = "Jefe de Departamneto" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 10, anldescripcion = "Enlace" });
            lstAreaNivel.Add(new SIT_ADM_AREANIVEL { anlclave = 11, anldescripcion = "Operativo" });
            return lstAreaNivel;
        }
        public object dmlSelectAreasHist( )
        {
            List<SIT_ADM_AREAHIST> lstAreaHist = new List<SIT_ADM_AREAHIST>();
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 1, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/03/2003"), arhclaveua = "27", arhdescripcion = "Secretaría de la Contaloría y Desarrollo Administrativo", arhsiglas = "SECODAM", arhreporta = 0, anlclave = 1, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 1, arhfecini = FechaUtil.Fecha("01/04/2003"), arhfecfin = FechaUtil.Fecha("31/12/2099"), arhclaveua = "27", arhdescripcion = "Secretaría de la Función Pública", arhsiglas = "SFP", arhreporta = 0, anlclave = 1, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 2, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2099"), arhclaveua = "100", arhdescripcion = "OFICINA DEL SECRETARIO", arhsiglas = "SECRETARIO", arhreporta = 1, anlclave = 1, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 101, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2003"), arhclaveua = "109", arhdescripcion = "Unidad de Desarrollo Administrativo", arhsiglas = "UDA", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 102, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("12/06/2013"), arhclaveua = "110", arhdescripcion = "Unidad de Asuntos Jurídicos", arhsiglas = "UAJ", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 102, arhfecini = FechaUtil.Fecha("13/06/2013"), arhfecfin = FechaUtil.Fecha("26/03/2015"), arhclaveua = "315", arhdescripcion = "Unidad de Asuntos Jurídicos", arhsiglas = "UAJ", arhreporta = 300, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 102, arhfecini = FechaUtil.Fecha("27/03/2015"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "110", arhdescripcion = "Unidad de Asuntos Jurídicos", arhsiglas = "UAJ", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 103, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("30/06/2002"), arhclaveua = "111", arhdescripcion = "Dirección General de Comunicación Social", arhsiglas = "DGCS", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 105, arhfecini = FechaUtil.Fecha("01/07/2002"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "111", arhdescripcion = "Unidad de Vinculación para la Transparencia", arhsiglas = "UVT", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 104, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "112", arhdescripcion = "Contraloría Interna", arhsiglas = "CI", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 106, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("12/06/2013"), arhclaveua = "113", arhdescripcion = "Coordinación General de Órganos de Vigilancia y Control", arhsiglas = "CGOVC", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 106, arhfecini = FechaUtil.Fecha("27/03/2015"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "113", arhdescripcion = "Coordinación General de Órganos de Vigilancia y Control", arhsiglas = "CGOVC", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 107, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "114", arhdescripcion = "Secretaría Ejecutiva de la Comisión Intersercretarial para la Transparencia y el Combate a la Corrupción en la Administración Pública Federal", arhsiglas = "SECI", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 205, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2004"), arhclaveua = "115", arhdescripcion = "Unidad de Servicios Electrónicos Gubernamentales", arhsiglas = "USEG", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 103, arhfecini = FechaUtil.Fecha("01/07/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "116", arhdescripcion = "Dirección General de Comunicación Social", arhsiglas = "DGCS", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 108, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("12/01/2017"), arhclaveua = "117", arhdescripcion = "Unidad de Políticas de Transparencia y Cooperación Internacional", arhsiglas = "UPTCI", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 108, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "117", arhdescripcion = "Unidad de Políticas de Apertura Gubernamental y Cooperación Internacional", arhsiglas = "UPAGCI", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 305, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("12/06/2013"), arhclaveua = "118", arhdescripcion = "Dirección General de Información e Integración", arhsiglas = "DGII", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 305, arhfecini = FechaUtil.Fecha("27/03/2015"), arhfecfin = FechaUtil.Fecha("12/01/2017"), arhclaveua = "118", arhdescripcion = "Dirección General de Información e Integración", arhsiglas = "DGII", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 305, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "118", arhdescripcion = "Dirección General de Información e Integración", arhsiglas = "DGII", arhreporta = 102, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 109, arhfecini = FechaUtil.Fecha("20/10/2015"), arhfecfin = FechaUtil.Fecha("12/01/2017"), arhclaveua = "119", arhdescripcion = "Unidad Especializada en Ética y Prevención de Conflictos de Interés", arhsiglas = "UEEPCI", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 109, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "419", arhdescripcion = "Unidad Especializada en Ética y Prevención de Conflictos de Interés", arhsiglas = "UEEPCI", arhreporta = 1, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 110, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "120", arhdescripcion = "Dirección General de Transparencia", arhsiglas = "DGT", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 111, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "121", arhdescripcion = "Dirección General de Igualdad de Genero", arhsiglas = "DGIG", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 112, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "122", arhdescripcion = "Unidad de Vinculación con el Sistema Nacional Anticorrupción", arhsiglas = "UVSNA", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 113, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "123", arhdescripcion = "Dirección General de Vinculación con el Sistema Nacional Anticorrupción", arhsiglas = "DGVSNT", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 114, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "124", arhdescripcion = "Dirección General de Vinculación con el Sistema Nacional de Fizcalización", arhsiglas = "DGVSNF", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 115, arhfecini = FechaUtil.Fecha("13/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "125", arhdescripcion = "Dirección General de Análisis, Diagnóstico y Formulación de Proyectos en Materia Anticorrupción de la Administración Pública Federal", arhsiglas = "DGADFPMAAPF", arhreporta = 1, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 200, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2001"), arhclaveua = "200", arhdescripcion = "Subsecretaría de Atención Ciudadana y Contraloría Social", arhsiglas = "SACCS", arhreporta = 1, anlclave = 2, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 200, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "200", arhdescripcion = "Subsecretaría de Control y Auditoría de la Gestión Pública", arhsiglas = "SCAGP", arhreporta = 1, anlclave = 2, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 209, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "208", arhdescripcion = "Unidad de Control y Auditoría a Obra Pública", arhsiglas = "UCAOP", arhreporta = 200, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 205, arhfecini = FechaUtil.Fecha("01/01/2001"), arhfecfin = FechaUtil.Fecha("31/12/2001"), arhclaveua = "209", arhdescripcion = "Unidad de Servicios Electrónicos Gubernamentales", arhsiglas = "USEG", arhreporta = 200, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 207, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "209", arhdescripcion = "Unidad de Control y Evaluación de la Gestión Pública", arhsiglas = "UCEGP", arhreporta = 200, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 207, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "209", arhdescripcion = "Unidad de Control de la Gestión Pública", arhsiglas = "UCGP", arhreporta = 200, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 201, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2001"), arhclaveua = "210", arhdescripcion = "Dirección General de Atención Ciudadana", arhsiglas = "DGAC", arhreporta = 200, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 206, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "210", arhdescripcion = "Unidad de Auditoría Gubernamental", arhsiglas = "UAG", arhreporta = 200, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 202, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2001"), arhclaveua = "211", arhdescripcion = "Dirección General de Responsabilidades y Situación Patrimonial", arhsiglas = "DGRSP", arhreporta = 200, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 203, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "211", arhdescripcion = "Dirección General de Operación Regional y Contraloría Social", arhsiglas = "DGOPCS", arhreporta = 200, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 203, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "211", arhdescripcion = "Unidad de Operación Regional y Contraloría Social", arhsiglas = "UORCS", arhreporta = 200, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 203, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2001"), arhclaveua = "212", arhdescripcion = "Dirección General de Operación Regional y Contraloría Social", arhsiglas = "DGOPCS", arhreporta = 200, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 208, arhfecini = FechaUtil.Fecha("01/07/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "212", arhdescripcion = "Dirección General de Auditorías Externas", arhsiglas = "DGAE", arhreporta = 200, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 204, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2001"), arhclaveua = "213", arhdescripcion = "Dirección General de Inconformidades", arhsiglas = "DGI", arhreporta = 200, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 300, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2001"), arhclaveua = "300", arhdescripcion = "Subsecretaría de Normatividad y Control de la Gestión Pública", arhsiglas = "SNCGP", arhreporta = 1, anlclave = 2, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 300, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2012"), arhclaveua = "300", arhdescripcion = "Subsecretaría de Atención Ciudadana y Normatividad", arhsiglas = "SACN", arhreporta = 1, anlclave = 2, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 300, arhfecini = FechaUtil.Fecha("01/01/2014"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "300", arhdescripcion = "Subsecretaría de Responsabilidades Administrativas y Contrataciones Públicas", arhsiglas = "SRACP", arhreporta = 1, anlclave = 2, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 306, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "308", arhdescripcion = "Unidad de Política de Contrataciones Públicas", arhsiglas = "UPCP", arhreporta = 300, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 301, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "309", arhdescripcion = "Unidad de Normatividad de Adquisiciones, Obras Públicas, Servicios y Patrimonio Federal", arhsiglas = "UNAOPSPF", arhreporta = 300, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 301, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "309", arhdescripcion = "Unidad de Normatividad de Contrataciones Públicas", arhsiglas = "UNCP", arhreporta = 300, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 302, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2001"), arhclaveua = "310", arhdescripcion = "Unidad de Seguimiento y Evaluación de la Gestión Pública", arhsiglas = "USEGP", arhreporta = 300, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 201, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2010"), arhclaveua = "310", arhdescripcion = "Dirección General de Atención Ciudadana", arhsiglas = "DGAC", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 201, arhfecini = FechaUtil.Fecha("01/01/2011"), arhfecfin = FechaUtil.Fecha("31/12/2012"), arhclaveua = "310", arhdescripcion = "Unidad de Atención Ciudadana", arhsiglas = "UAC", arhreporta = 300, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 308, arhfecini = FechaUtil.Fecha("01/01/2013"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "310", arhdescripcion = "Dirección General de Denuncias e Investigaciones", arhsiglas = "DGDI", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 303, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("30/06/2002"), arhclaveua = "311", arhdescripcion = "Dirección General de Auditoría Gubernamental", arhsiglas = "DGAG", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 202, arhfecini = FechaUtil.Fecha("01/07/2002"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "311", arhdescripcion = "Dirección General de Responsabilidades y Situación Patrimonial", arhsiglas = "DGRSP", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 304, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "312", arhdescripcion = "Dirección General de Inconformidades", arhsiglas = "DGI", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 307, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "312", arhdescripcion = "Dirección General de Controversias y Sanciones en Contrataciones Públicas", arhsiglas = "DGCSCP", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 305, arhfecini = FechaUtil.Fecha("01/07/2004"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "313", arhdescripcion = "Dirección General de Información e Integración", arhsiglas = "DGII", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 106, arhfecini = FechaUtil.Fecha("13/06/2013"), arhfecfin = FechaUtil.Fecha("26/03/2015"), arhclaveua = "313", arhdescripcion = "Coordinación General de Órganos de Vigilancia y Control", arhsiglas = "CGOVC", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 305, arhfecini = FechaUtil.Fecha("13/06/2013"), arhfecfin = FechaUtil.Fecha("26/03/2015"), arhclaveua = "314", arhdescripcion = "Dirección General de Información e Integración", arhsiglas = "DGII", arhreporta = 300, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 400, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2004"), arhclaveua = "400", arhdescripcion = "Subsecretaría de Desarrollo y Simplificación Administrativa", arhsiglas = "SDSA", arhreporta = 1, anlclave = 2, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 400, arhfecini = FechaUtil.Fecha("01/01/2005"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "400", arhdescripcion = "Subsecretaría de la Función Pública", arhsiglas = "SSFP", arhreporta = 1, anlclave = 2, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 404, arhfecini = FechaUtil.Fecha("01/07/2004"), arhfecfin = FechaUtil.Fecha("31/12/2005"), arhclaveua = "408", arhdescripcion = "Unidad del Servicio Profesional y Recursos Humanos de la Administración Pública Federal", arhsiglas = "USPRHAPF", arhreporta = 400, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 404, arhfecini = FechaUtil.Fecha("01/01/2006"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "408", arhdescripcion = "Unidad de Recursos Humanos y Profesionalización de la Administración Pública Federal", arhsiglas = "URHPAPF", arhreporta = 400, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 404, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "408", arhdescripcion = "Unidad de Política de Recursos Humanos de la Administración Pública Federal", arhsiglas = "UPRHAPF", arhreporta = 400, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 205, arhfecini = FechaUtil.Fecha("01/01/2005"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "409", arhdescripcion = "Unidad de Gobierno Electrónico y Política de Tecnologías de la Información", arhsiglas = "UGEPTI", arhreporta = 400, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 205, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "409", arhdescripcion = "Unidad de Gobierno Digital", arhsiglas = "UGD", arhreporta = 400, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 401, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "410", arhdescripcion = "Dirección General de Simplificación Regulatoria", arhsiglas = "DGSR", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 402, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2004"), arhclaveua = "411", arhdescripcion = "Dirección General de Eficiencia Administrativa", arhsiglas = "DGEA", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 402, arhfecini = FechaUtil.Fecha("01/01/2005"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "411", arhdescripcion = "Dirección General de Eficiencia Administrativa y Buen Gobierno", arhsiglas = "DGEABG", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 409, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "411", arhdescripcion = "Unidad de Políticas de Mejora de la Gestión Pública", arhsiglas = "UPMGP", arhreporta = 400, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 403, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2004"), arhclaveua = "412", arhdescripcion = "Dirección General de Análisis de Estructura y Profesionalización del Servicio Público", arhsiglas = "DGAEP", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 403, arhfecini = FechaUtil.Fecha("01/01/2005"), arhfecfin = FechaUtil.Fecha("31/12/2005"), arhclaveua = "412", arhdescripcion = "Dirección General de Estructuras y Puestos", arhsiglas = "DGEP", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 405, arhfecini = FechaUtil.Fecha("01/01/2006"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "412", arhdescripcion = "Dirección General de Planeación, Organización y Compensaciones de la Administración Pública Federal", arhsiglas = "DGPOCAFP", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 406, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2005"), arhclaveua = "413A", arhdescripcion = "Dirección General de Ingreso, Capacitación y Certificación", arhsiglas = "DGICC", arhreporta = 400, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 406, arhfecini = FechaUtil.Fecha("01/01/2006"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "413", arhdescripcion = "Dirección General de Ingreso, Capacitación y Certificación", arhsiglas = "DGICC", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 407, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2005"), arhclaveua = "413B", arhdescripcion = "Dirección General de Atención a Instituciones Públicas en Recursos Humanos", arhsiglas = "DGAIPRH", arhreporta = 400, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 407, arhfecini = FechaUtil.Fecha("01/01/2006"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "414", arhdescripcion = "Dirección General de Atención a Instituciones Públicas en Recursos Humanos", arhsiglas = "DGAIPRH", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 408, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2005"), arhclaveua = "413C", arhdescripcion = "Dirección General de Desarrollo Tecnológico de Recursos Humanos", arhsiglas = "DGDTRH", arhreporta = 400, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 408, arhfecini = FechaUtil.Fecha("01/01/2006"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "415", arhdescripcion = "Dirección General de Evaluación de Sistemas de Profesionalización", arhsiglas = "DGESP", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 410, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "416", arhdescripcion = "Unidad de Evaluación de la Gestión y el Desempeño Gubernamental", arhsiglas = "UEGDG", arhreporta = 400, anlclave = 5, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 411, arhfecini = FechaUtil.Fecha("01/01/2006"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "400A", arhdescripcion = "Gabinete de apoyo SSFP", arhsiglas = "GASSFP", arhreporta = 400, anlclave = 7, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 412, arhfecini = FechaUtil.Fecha("01/01/2006"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "408", arhdescripcion = "Dirección General de Desarrollo Humano y Servicio Profesional de Carrera", arhsiglas = "DGDHSPC", arhreporta = 408, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 413, arhfecini = FechaUtil.Fecha("01/01/2006"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "408", arhdescripcion = "Dirección General de Organización y Remuneraciones de la Administracion Pública Federal", arhsiglas = "DGORAPF", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 500, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2002"), arhclaveua = "400", arhdescripcion = "Oficialía Mayor", arhsiglas = "OM", arhreporta = 1, anlclave = 3, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 500, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "500", arhdescripcion = "Oficialía Mayor", arhsiglas = "OM", arhreporta = 1, anlclave = 3, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 501, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2002"), arhclaveua = "410", arhdescripcion = "Dirección General de Administración", arhsiglas = "DGA", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 501, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "510", arhdescripcion = "Dirección General de Administración", arhsiglas = "DGA", arhreporta = 500, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 501, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "510", arhdescripcion = "Dirección General de Recursos Humanos", arhsiglas = "DGRH", arhreporta = 500, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 502, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2002"), arhclaveua = "411", arhdescripcion = "Dirección General de Informática", arhsiglas = "DGI", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 502, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "511", arhdescripcion = "Dirección General de Informática", arhsiglas = "DGI", arhreporta = 500, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 502, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "511", arhdescripcion = "Dirección General de Tecnologías de Información", arhsiglas = "DGTI", arhreporta = 500, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 503, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2002"), arhclaveua = "412", arhdescripcion = "Dirección General de Programación, Organización y Presupuesto", arhsiglas = "DGPOP", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 503, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2009"), arhclaveua = "512", arhdescripcion = "Dirección General de Programación, Organización y Presupuesto", arhsiglas = "DGPOP", arhreporta = 500, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 503, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "512", arhdescripcion = "Dirección General de Programación y Presupuesto", arhsiglas = "DGPYP", arhreporta = 500, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 504, arhfecini = FechaUtil.Fecha("01/01/2002"), arhfecfin = FechaUtil.Fecha("31/12/2002"), arhclaveua = "413", arhdescripcion = "Dirección General de Modernización Administrativa y Procesos", arhsiglas = "DGMAP", arhreporta = 400, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 504, arhfecini = FechaUtil.Fecha("01/01/2003"), arhfecfin = FechaUtil.Fecha("31/12/2011"), arhclaveua = "513", arhdescripcion = "Dirección General de Modernización Administrativa y Procesos", arhsiglas = "DGMAP", arhreporta = 500, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 505, arhfecini = FechaUtil.Fecha("01/01/2010"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "514", arhdescripcion = "Dirección General de Recursos Materiales y Servicios Generales", arhsiglas = "DGRMSG", arhreporta = 500, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 506, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "50A", arhdescripcion = "Coordinación Técnica", arhsiglas = "CTOM", arhreporta = 500, anlclave = 7, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 507, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "50B", arhdescripcion = "Coordinación de Asesores", arhsiglas = "CAOM", arhreporta = 500, anlclave = 7, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 507, arhfecini = FechaUtil.Fecha("01/01/2011"), arhfecfin = FechaUtil.Fecha("31/12/2012"), arhclaveua = "50B", arhdescripcion = "Coordinación de Asesores", arhsiglas = "CAOM", arhreporta = 500, anlclave = 7, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 600, arhfecini = FechaUtil.Fecha("01/01/2000"), arhfecfin = FechaUtil.Fecha("31/12/2004"), arhclaveua = "A00", arhdescripcion = "Comisión de Avalúos de Bienes Nacionales", arhsiglas = "CABIN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 600, arhfecini = FechaUtil.Fecha("01/01/2005"), arhfecfin = FechaUtil.Fecha("31/12/2016"), arhclaveua = "A00", arhdescripcion = "Instituto de Administración y Avalúos de Bienes Nacionales", arhsiglas = "INDAABIN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 601, arhfecini = FechaUtil.Fecha("01/01/2007"), arhfecfin = FechaUtil.Fecha("31/12/2012"), arhclaveua = "28S", arhdescripcion = "Instituo Nacional de Administración Pública, A.C.", arhsiglas = "INAP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 602, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "A01", arhdescripcion = "Dirección General de Avalúos", arhsiglas = "DGA", arhreporta = 600, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 603, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "A02", arhdescripcion = "Dirección General de Administración y Obras en Inmuebles Federales", arhsiglas = "DGAOIF", arhreporta = 600, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 604, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "A03", arhdescripcion = "Dirección General de Patrimonio Inmobiliario Federal", arhsiglas = "DGPIF", arhreporta = 600, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 605, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "A04", arhdescripcion = "Dirección General Jurídica", arhsiglas = "DGJ", arhreporta = 600, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 606, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "A05", arhdescripcion = "Dirección General de Administración y Finanzas", arhsiglas = "DGAF", arhreporta = 600, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 607, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "A06", arhdescripcion = "Coordinación de Delegaciones", arhsiglas = "CD", arhreporta = 600, anlclave = 6, atpclave = 2 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 608, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2007"), arhclaveua = "A07", arhdescripcion = "Dirección de Informática", arhsiglas = "DI", arhreporta = 600, anlclave = 6, atpclave = 2 });



            //lstDatos.Add(new AdmAreaMdl(12, "Unidad de Transparencia", "N/A", "UT", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur #1735", 2));
            //lstDatos.Add(new AdmAreaMdl(13, "Comité de Transparencia", "N/A", "CT", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur#1735", 4));
            //lstDatos.Add(new AdmAreaMdl(14, "Revisión Forma", "N/A", "RF", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur#1735", 5));
            //lstDatos.Add(new AdmAreaMdl(15, "Unidad de Transparencia Notificar", "N/A", "UTN", 0, 1, 0, 0, new DateTime(2003, 04, 11), new DateTime(2025, 12, 31), "Av. Insurgentes Sur#1735", 6));

            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 700, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("30/06/2010"), arhclaveua = "IFAI", arhdescripcion = "Instituto Federal de Acceso a la Información Pública", arhsiglas = "IFAI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 700, arhfecini = FechaUtil.Fecha("01/07/2010"), arhfecfin = FechaUtil.Fecha("30/04/2015"), arhclaveua = "IFAI", arhdescripcion = "Instituto Federal de Acceso a la Información y Protección de Datos", arhsiglas = "IFAI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 700, arhfecini = FechaUtil.Fecha("01/05/2015"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "IFAI", arhdescripcion = "Instituto Nacional de Transparencia, Acceso a la Información y Protección de Datos Personales", arhsiglas = "INAI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 701, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC1", arhdescripcion = "Administración Federal de Servicios Educativos en el D.F.", arhsiglas = "AFSEDF", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 702, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC2", arhdescripcion = "Administración Portuaria Integral de Altamira S.A.de C.V.", arhsiglas = "API ALTAMIRA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 703, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC3", arhdescripcion = "Administración Portuaria Integral de Coatzacoalcos S.A.de C.V.", arhsiglas = "API COATZA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 704, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC4", arhdescripcion = "Administración Portuaria Integral de Dos bocas S.A.de C.V.", arhsiglas = "API DOS BOCAS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 705, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC5", arhdescripcion = "Administración Portuaria Integral de Ensenada S.A.de C.V.", arhsiglas = "API ENSENADA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 706, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC6", arhdescripcion = "Administración Portuaria Integral de Guaymas S.A.de C.V.", arhsiglas = "API GUAYMAS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 707, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC7", arhdescripcion = "Administración Portuaria Integral de Lázaro cárdenas S.A.de C.V.", arhsiglas = "API LÁZARO CÁRDENAS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 708, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC8", arhdescripcion = "Administración Portuaria Integral de Manzanillo S.A.de C.V.", arhsiglas = "API MANZANILLO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 709, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC9", arhdescripcion = "Administración Portuaria Integral de Mazatlan S.A.de C.V.", arhsiglas = "API MAZATLÁN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 710, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC10", arhdescripcion = "Administración Portuaria Integral de Progreso S.A.de C.V.", arhsiglas = "API PROGRESO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 711, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC11", arhdescripcion = "Administración Portuaria Integral de Puerto madero", arhsiglas = "API MADERO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 712, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC12", arhdescripcion = "Administración Portuaria Integral de Puerto vallarta S.A.de C.V.", arhsiglas = "API VALLARTA", arhreporta = 1, anlclave = 4, atpclave = 3 });


            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 713, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC13", arhdescripcion = "Administración Portuaria Integral de Salina cruz S.A.de C.V.", arhsiglas = "API SALINA CRUZ", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 714, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC14", arhdescripcion = "Administración Portuaria Integral de Tampico S.A.de C.V.", arhsiglas = "API TAMPICO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 715, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC15", arhdescripcion = "Administración Portuaria Integral de Topolobampo", arhsiglas = "API TOPOLOBAMPO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 716, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC16", arhdescripcion = "Administración Portuaria Integral de Tuxpan S.A.de C.V.", arhsiglas = "API TUXPAN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 717, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC17", arhdescripcion = "Administración Portuaria Integral de Veracruz S.A.de C.V.", arhsiglas = "API VERACRUZ", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 718, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC18", arhdescripcion = "Aeropuerto InterNacional de la ciudad de méxico S.A.de C.V.", arhsiglas = "AICM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 719, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC19", arhdescripcion = "Aeropuertos y Servicios auxiliares", arhsiglas = "ASA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 720, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC20", arhdescripcion = "Agencia de Servicios a la comercialización y desarrollo de mercados agropecuarios", arhsiglas = "ASERCA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 721, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC21", arhdescripcion = "Agencia Espacial Mexicana", arhsiglas = "AEM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 722, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC22", arhdescripcion = "Agroasemex S.A.", arhsiglas = "AGROASEMEX", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 723, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC23", arhdescripcion = "Banco del Ahorro Nacional y Servicios Financieros S.N.C.", arhsiglas = "BANSEFI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 724, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC24", arhdescripcion = "Banco Nacional de comercio exterior", arhsiglas = "BANCOMEXT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 725, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC25", arhdescripcion = "Banco Nacional de obras y servicios públicos", arhsiglas = "BANOBRAS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 726, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC26", arhdescripcion = "Banco Nacional del ejército y armada S.N.C.", arhsiglas = "BANJERCITO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 727, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC27", arhdescripcion = "Caminos y Puentes Federales de ingresos y servicios conexos", arhsiglas = "CAPUFE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 728, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC28", arhdescripcion = "Casa de Moneda de México", arhsiglas = "CMM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 729, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC29", arhdescripcion = "Centro de capacitación cinematográfica A.C.", arhsiglas = "CCC", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 730, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC30", arhdescripcion = "Centro de enseñanza técnica industrial", arhsiglas = "CETI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 731, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC31", arhdescripcion = "Centro de ingeniería y desarrollo industrial", arhsiglas = "CIDESI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 732, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC32", arhdescripcion = "Centro de Investigación científica de yucatán A.C.", arhsiglas = "CICY", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 733, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC33", arhdescripcion = "Centro de Investigación científica y de educación superior de ensenada baja california", arhsiglas = "CICESE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 734, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC34", arhdescripcion = "Centro de Investigación e innovación en tecnología de la información y comunicación", arhsiglas = "INFOTEC", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 735, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC35", arhdescripcion = "Centro de Investigación en alimentación y desarrollo A.C.", arhsiglas = "CIAD", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 736, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC36", arhdescripcion = "Centro de Investigación en geografía y geomática  ing.jorge l.tamayo A.C.", arhsiglas = "CIGGET", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 737, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC37", arhdescripcion = "Centro de Investigación en matemáticas A.C.", arhsiglas = "CIMAT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 738, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC38", arhdescripcion = "Centro de Investigación en materiales avanzados S.C.", arhsiglas = "CIMAV", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 739, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC39", arhdescripcion = "Centro de Investigación en química aplicada", arhsiglas = "CIQA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 740, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC40", arhdescripcion = "Centro de Investigación y asistencia en tecnología y diseño del estado de jalisco A.C.", arhsiglas = "CIATEJ", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 741, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC41", arhdescripcion = "Centro de Investigación y de estudios avanzados del IPN", arhsiglas = "CINVESTAV", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 742, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC42", arhdescripcion = "Centro de Investigación y desarrollo tecnológico en electroquímica", arhsiglas = "CIDETEC", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 743, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC43", arhdescripcion = "Centro de Investigación y docencia económica A.C.", arhsiglas = "CIDE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 744, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC44", arhdescripcion = "Centro de Investigación y seguridad Nacional", arhsiglas = "CISEN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 745, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC45", arhdescripcion = "Centro de Investigaciones biológicas del noroeste S.C.", arhsiglas = "CIBNOR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 746, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC46", arhdescripcion = "Centro de Investigaciones en óptica A.C.", arhsiglas = "CIO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 747, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC47", arhdescripcion = "Centro de Investigaciones y estudios superiores en antropología social", arhsiglas = "CIESAS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 748, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC48", arhdescripcion = "Centro Nacional de control de energía", arhsiglas = "CENACE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 749, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC49", arhdescripcion = "Centro Nacional de control del gas natural", arhsiglas = "CENAGAS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 750, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC50", arhdescripcion = "Centro Nacional de metrología", arhsiglas = "CENAM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 751, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC51", arhdescripcion = "Centro Regional de alta especialidad de chiapas", arhsiglas = "CRAECHIS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 752, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC52", arhdescripcion = "Centros de integración juvenil", arhsiglas = "CINJUVE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 753, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC53", arhdescripcion = "Ciatec, A.C.", arhsiglas = "CIATEC", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 754, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC54", arhdescripcion = "Ciateq A.C.", arhsiglas = "CIATEQ", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 755, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC55", arhdescripcion = "Colegio de bachilleres", arhsiglas = "COLBACH", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 756, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC56", arhdescripcion = "Colegio de postgraduados", arhsiglas = "COLPOS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 757, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC57", arhdescripcion = "Colegio Nacional de educación profesional técnica", arhsiglas = "CONALEP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 758, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC58", arhdescripcion = "Comisión de operación y fomento de actividades académicas del IPN", arhsiglas = "COFAA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 759, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC59", arhdescripcion = "Comisión ejecutiva de atención a victimas", arhsiglas = "CEAV", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 760, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC60", arhdescripcion = "Comisión Federal de electricidad", arhsiglas = "CFE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 761, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC61", arhdescripcion = "Comisión Federal para la protección  contra riesgos sanitarios", arhsiglas = "COFEPRIS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 762, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC62", arhdescripcion = "Comisión Nacional bancaria y de valores", arhsiglas = "CNBV", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 763, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC63", arhdescripcion = "Comisión Nacional de acuacultura y pesca", arhsiglas = "CONAPESCA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 764, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC64", arhdescripcion = "Comisión Nacional de arbitraje médico", arhsiglas = "CONAMED", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 765, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC65", arhdescripcion = "Comisión Nacional de cultura física y deporte", arhsiglas = "CONADE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 766, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC66", arhdescripcion = "Comisión Nacional de hidrocarburos", arhsiglas = "CNH", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 767, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC67", arhdescripcion = "Comisión Nacional de las zona áridas", arhsiglas = "CONAZA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 768, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC68", arhdescripcion = "Comisión Nacional de libros de texto gratuitos", arhsiglas = "CONALITEG", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 769, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC69", arhdescripcion = "Comisión Nacional de los salarios mínimos", arhsiglas = "CONASAMI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 770, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC70", arhdescripcion = "Comisión Nacional de protección social en salud", arhsiglas = "CNPSS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 771, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC71", arhdescripcion = "Comisión Nacional de seguridad nuclear y salvaguardias", arhsiglas = "CNSNS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 772, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC72", arhdescripcion = "Comisión Nacional de seguros y fianzas", arhsiglas = "CNSF", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 773, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC73", arhdescripcion = "Comisión Nacional de vivienda", arhsiglas = "CONAVI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 774, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC74", arhdescripcion = "Comisión Nacional del agua", arhsiglas = "CONAGUA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 775, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC75", arhdescripcion = "Comisión Nacional del sistema de ahorro para el retiro", arhsiglas = "CONSAR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 776, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC76", arhdescripcion = "Comisión Nacional forestal", arhsiglas = "CONAFOR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 777, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC77", arhdescripcion = "Comisión Nacional para el desarrollo de los pueblos indígenas", arhsiglas = "CDI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 778, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC78", arhdescripcion = "Comisión Nacional para la protección y defensa de los usuarios de servicios financieros", arhsiglas = "CONDUSEF", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 779, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC79", arhdescripcion = "Comisión para la Regularización de la Tenencia de la Tierra", arhsiglas = "CORETT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 780, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC80", arhdescripcion = "Comisión reguladora de energia", arhsiglas = "CRE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 781, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC81", arhdescripcion = "Compañía mexicana de exploraciones S.A.de C.V.", arhsiglas = "COMESA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 782, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC82", arhdescripcion = "Compañía operadora del centro cultural y turístico de tijuana", arhsiglas = "CECUTT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 783, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC83", arhdescripcion = "Consejeria jurídica del ejecutivo federal", arhsiglas = "CJEF", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 784, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC84", arhdescripcion = "Consejo de promoción turística de méxico S.A.de C.V.", arhsiglas = "CPTM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 785, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC85", arhdescripcion = "Consejo Nacional de ciencia y tecnología", arhsiglas = "CONACYT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 786, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC86", arhdescripcion = "Consejo Nacional de evaluación de la política de desarrollo social", arhsiglas = "CONEVAL", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 787, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC87", arhdescripcion = "Consejo Nacional de fomento educativo", arhsiglas = "CONAFE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 788, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC88", arhdescripcion = "Consejo Nacional para el desarrollo y la inclusión de las personas con discapacidad", arhsiglas = "CONADIS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 789, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC89", arhdescripcion = "Consejo Nacional para prevenir la discriminación", arhsiglas = "CONAPRED", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 790, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC90", arhdescripcion = "Coordinación Nacional de prospera programa de inclusión social", arhsiglas = "PROPERA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 791, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC91", arhdescripcion = "Corporación mexicana de Investigaciones en materiales S.A.de C.V.", arhsiglas = "COMIMSA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 792, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC92", arhdescripcion = "Diconsa", arhsiglas = "DICONSA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 793, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC93", arhdescripcion = "Educal S.A.de C.V.", arhsiglas = "EDUCAL", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 794, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC94", arhdescripcion = "El colegio de la frontera norte A.C.", arhsiglas = "COLEF", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 795, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC95", arhdescripcion = "El colegio de la frontera sur", arhsiglas = "ECOSUR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 796, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC96", arhdescripcion = "El colegio de michoacán A.C.", arhsiglas = "COLMICH", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 797, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC97", arhdescripcion = "El colegio de San luis, A.C.", arhsiglas = "COLSAN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 798, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC98", arhdescripcion = "Estudios churubusco azteca, S.A.", arhsiglas = "ECHASA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 799, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC99", arhdescripcion = "Exportadora de sal S.A.de C.V.y tranportadora de sal S.A.de C.V.", arhsiglas = "ESSA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 800, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC100", arhdescripcion = "Ferrocarril del istmo de tehuantepec S.A.de C.V.", arhsiglas = "FIT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 801, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC101", arhdescripcion = "Fideicomiso de fomento minero", arhsiglas = "FIFOMI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 802, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC102", arhdescripcion = "Fideicomiso de formación y capacitación para el personal de la marina mercante Nacional", arhsiglas = "FIDENA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 803, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC103", arhdescripcion = "Fideicomiso de los sistemas normalizado de competencia laboral y de certificación de competencia laboral", arhsiglas = "CONOCER", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 804, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC104", arhdescripcion = "Fideicomiso de riesgo compartido", arhsiglas = "FIRCO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 805, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC105", arhdescripcion = "Fideicomiso fondo Nacional de fomento ejidal", arhsiglas = "FIFONAFE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 806, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC106", arhdescripcion = "Fideicomiso fondo Nacional de habitaciones populares", arhsiglas = "FONHAPO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 807, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC107", arhdescripcion = "Fideicomiso para la cineteca Nacional", arhsiglas = "FICINE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 808, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC108", arhdescripcion = "Fideicomiso público promexico", arhsiglas = "PROMEXICO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 809, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC109", arhdescripcion = "Financiera Nacional de desarrollo agropecuario,  rural, forestal y pesquero", arhsiglas = "FINARURAL", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 810, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC110", arhdescripcion = "Fondo de capitalización e inversión del sector rural", arhsiglas = "FOCIR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 811, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC111", arhdescripcion = "Fondo de cultura económica", arhsiglas = "FCE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 812, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC112", arhdescripcion = "Fondo de garantía y fomento para la agricultura ganadería y avicultura(fondo) fondo especial para financiamientos agropecuarios(fefa) fondo especial de asistencia tecnica y garantia para creditos agropecuarios(fega) y fondo  de garantia y fomento para las actividades pesqueras(fopesca)", arhsiglas = "FONDOS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 813, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC113", arhdescripcion = "Fondo de la vivienda del issste", arhsiglas = "FOVISSSTE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 814, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC114", arhdescripcion = "Fondo Nacional de fomento al turismo y empresas de participación accionaria", arhsiglas = "FONATUR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 815, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC115", arhdescripcion = "Fondo Nacional para el fomento de las artesanías", arhsiglas = "FONART", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 816, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC116", arhdescripcion = "Grupo aeroportuario de la ciudad de méxico S.A.de C.V.", arhsiglas = "GACM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 817, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC117", arhdescripcion = "Hospital general de méxico", arhsiglas = "HGM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 818, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC118", arhdescripcion = "Hospital general dr.manuel gea gonzález", arhsiglas = "HGMGG", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 819, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC119", arhdescripcion = "Hospital infantil de méxico federico gómez", arhsiglas = "HIMFG", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 820, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC120", arhdescripcion = "Hospital juárez de méxico", arhsiglas = "HJM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 821, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC121", arhdescripcion = "Hospital Regional de alta especialidad de ciudad victoria", arhsiglas = "HRAECV", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 822, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC122", arhdescripcion = "Hospital Regional de alta especialidad de ixtapaluca", arhsiglas = "HRAEI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 823, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC123", arhdescripcion = "Hospital Regional de alta especialidad de la península de yucatán", arhsiglas = "HRAEPY", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 824, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC124", arhdescripcion = "Hospital Regional de alta especialidad de oaxaca", arhsiglas = "HRAEO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 825, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC125", arhdescripcion = "Hospital Regional de alta especialidad del bajío", arhsiglas = "HRAEB", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 826, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC126", arhdescripcion = "Impresora y encuadernadora progreso S.A.de C.V.", arhsiglas = "IEPSA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 827, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC127", arhdescripcion = "Instituto de ecologia A.C.", arhsiglas = "INECOL", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 828, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC128", arhdescripcion = "Instituto de Investigaciones Dr. José María Luis Mora", arhsiglas = "Instituto MORA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 829, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC129", arhdescripcion = "Instituto de Investigaciones eléctricas", arhsiglas = "IIE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 830, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC130", arhdescripcion = "Instituto de seguridad de las fuerzas armadas mexicanas", arhsiglas = "ISSFAM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 831, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC131", arhdescripcion = "Instituto de seguridad y servicios sociales de los trabajadores del estado", arhsiglas = "ISSSTE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 832, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC132", arhdescripcion = "Instituto del fondo Nacional para el consumo de los trabajadores", arhsiglas = "INFONACOT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 833, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC133", arhdescripcion = "Instituto Mexicano de cinematografía", arhsiglas = "IMCINE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 834, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC134", arhdescripcion = "Instituto Mexicano de la juventud", arhsiglas = "IMJUVE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 835, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC135", arhdescripcion = "Instituto Mexicano de la propiedad industrial", arhsiglas = "IMPI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 836, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC136", arhdescripcion = "Instituto Mexicano de la radio", arhsiglas = "IMER", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 837, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC137", arhdescripcion = "Instituto Mexicano de tecnología del agua", arhsiglas = "IMTA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 838, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC138", arhdescripcion = "Instituto Mexicano del petróleo", arhsiglas = "IMP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 839, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC139", arhdescripcion = "Instituto Mexicano del seguro social", arhsiglas = "IMSS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 840, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC140", arhdescripcion = "Instituto Nacional de antropología e historia", arhsiglas = "INAH", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 841, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC141", arhdescripcion = "Instituto Nacional de astrofísica, Óptica y Electrónica", arhsiglas = "INAOP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 842, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC142", arhdescripcion = "Instituto Nacional de bellas artes y literatura", arhsiglas = "INBAL", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 843, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC143", arhdescripcion = "Instituto Nacional de cancerología", arhsiglas = "INCAN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 844, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC144", arhdescripcion = "Instituto Nacional de cardiología ignacio chávez", arhsiglas = "CARDIOLOGÍA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 845, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC145", arhdescripcion = "Instituto Nacional de ciencias médicas y nutrición  salvador zubiran", arhsiglas = "INCMNSZ", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 846, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC146", arhdescripcion = "Instituto Nacional de ciencias penales", arhsiglas = "INACIPE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 847, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC147", arhdescripcion = "Instituto Nacional de enfermedades respiratorias ismael cosío villegas", arhsiglas = "INER", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 848, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC148", arhdescripcion = "Instituto Nacional de Investigaciones forestales, agricolas y pecuarias", arhsiglas = "INIFAP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 849, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC149", arhdescripcion = "Instituto Nacional de Investigaciones Nucleares", arhsiglas = "ININ", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 850, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC150", arhdescripcion = "Instituto Nacional de la economia social", arhsiglas = "INAES", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 851, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC151", arhdescripcion = "Instituto Nacional de la infraestructura física educativa", arhsiglas = "INIFED", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 852, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC152", arhdescripcion = "Instituto Nacional de las mujeres", arhsiglas = "INMUJERES", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 853, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC153", arhdescripcion = "Instituto Nacional de las personas adultas mayores", arhsiglas = "INAPAM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 854, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC154", arhdescripcion = "Instituto Nacional de lenguas indígenas", arhsiglas = "INALI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 855, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC155", arhdescripcion = "Instituto Nacional de medicina genómica", arhsiglas = "INMEGEN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 856, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC156", arhdescripcion = "Instituto Nacional de migración", arhsiglas = "INAMI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 857, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC157", arhdescripcion = "Instituto Nacional de neurología y neurocirugía manuel velasco suárez", arhsiglas = "INNN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 858, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC158", arhdescripcion = "Instituto Nacional de pediatría", arhsiglas = "INP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 859, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC159", arhdescripcion = "Instituto Nacional de perinatología isidro espinosa de los reyes", arhsiglas = "INPER", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 860, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC160", arhdescripcion = "Instituto Nacional de psiquiatría ramon de la fuente muñiz", arhsiglas = "PSIQUIATRÍA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 861, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC161", arhdescripcion = "Instituto Nacional de rehabilitación", arhsiglas = "INR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 862, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC162", arhdescripcion = "Instituto Nacional de salud pública", arhsiglas = "INSP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 863, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC163", arhdescripcion = "Instituto Nacional para el desarrollo de capacidades del sector rural A.C.", arhsiglas = "INCA RURAL", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 864, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC164", arhdescripcion = "Instituto Nacional para la educación de los adultos", arhsiglas = "INEA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 865, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC165", arhdescripcion = "Instituto para la protección al ahorro bancario", arhsiglas = "IPAB", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 866, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC166", arhdescripcion = "Instituto politécnico Nacional", arhsiglas = "IPN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 867, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC167", arhdescripcion = "Instituto potosino de Investigación cientifica y tecnologica A.C.", arhsiglas = "IPICYT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 868, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC168", arhdescripcion = "Laboratorios de biológicos y reactivos de méxico S.A.de C.V.", arhsiglas = "BIRMEX", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 869, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC169", arhdescripcion = "Liconsa s.a de C.V.", arhsiglas = "LICONSA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 870, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC170", arhdescripcion = "Lotería Nacional para la asistencia pública", arhsiglas = "LOTENAL", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 871, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC171", arhdescripcion = "Nacional financiera S.N.C., IBD", arhsiglas = "NAFIN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 872, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC172", arhdescripcion = "Notimex agencia de noticias del estado Mexicano", arhsiglas = "NOTIMEX ANEM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 873, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC173", arhdescripcion = "Órgano administrativo desconcentrado prevención y readaptación social", arhsiglas = "PYRS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 874, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC174", arhdescripcion = "Patronato de obras e instalaciones del Instituto politécnico Nacional", arhsiglas = "POI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 875, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC175", arhdescripcion = "Petróleos Mexicanos", arhsiglas = "PEMEX", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 876, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC176", arhdescripcion = "Policía federal", arhsiglas = "PF", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 877, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC177", arhdescripcion = "Presidencia de la República", arhsiglas = "PRESIDENCIA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 878, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC178", arhdescripcion = "Procuraduría agraria", arhsiglas = "P.A.", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 879, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC179", arhdescripcion = "Procuraduría de la defensa del contribuyente", arhsiglas = "PFC", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 880, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC180", arhdescripcion = "Procuraduría Federal del consumidor", arhsiglas = "PROFECO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 881, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC181", arhdescripcion = "Procuraduría general de la república", arhsiglas = "PGR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 882, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC182", arhdescripcion = "Productora Nacional de biológicos veterinarios", arhsiglas = "PRONABIVE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 883, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC183", arhdescripcion = "Pronósticos para la asistencia pública", arhsiglas = "PRONOSTICOS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 884, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC184", arhdescripcion = "Registro agrario Nacional", arhsiglas = "RAN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 885, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC185", arhdescripcion = "Secretaria de agricultura ganadería y desarrollo rural, pesca y alimentación", arhsiglas = "SAGARPA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 886, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC186", arhdescripcion = "Secretaría de comunicaciones y transportes", arhsiglas = "SCT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 887, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC187", arhdescripcion = "Secretaría de cultura", arhsiglas = "SC", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 888, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC188", arhdescripcion = "Secretaría de desarrollo agrario territorial y urbano", arhsiglas = "SEDATU", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 889, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC189", arhdescripcion = "Secretaría de desarrollo social", arhsiglas = "SEDESOL", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 890, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC190", arhdescripcion = "Secretaría de economía", arhsiglas = "SE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 891, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC191", arhdescripcion = "Secretaria de educación pública", arhsiglas = "SEP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 892, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC192", arhdescripcion = "Secretaría de energía", arhsiglas = "SENER", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 893, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC193", arhdescripcion = "Secretaría de gobernación", arhsiglas = "SEGOB", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 894, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC194", arhdescripcion = "Secretaría de hacienda y crédito publico", arhsiglas = "SHCP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 895, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC195", arhdescripcion = "Secretaría de la defensa Nacional", arhsiglas = "SEDENA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 896, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC196", arhdescripcion = "Secretaría de medio ambiente y recursos naturales", arhsiglas = "SEMARNAT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 897, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC197", arhdescripcion = "Secretaría de relaciones exteriores", arhsiglas = "SRE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 898, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC198", arhdescripcion = "Secretaría de salud", arhsiglas = "SSA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 899, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC199", arhdescripcion = "Secretaría de turismo", arhsiglas = "SECTUR", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 900, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC200", arhdescripcion = "Secretaría del trabajo y prevision social", arhsiglas = "STYPS", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 901, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC201", arhdescripcion = "Secretariado ejecutivo del sistema Nacional de seguridad pública", arhsiglas = "SESNSP", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 902, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC202", arhdescripcion = "Servicio de Administración  tributaria", arhsiglas = "SAT", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 903, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC203", arhdescripcion = "Servicio de Administración  y enajenación  de bienes", arhsiglas = "SAE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 904, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC204", arhdescripcion = "Servicio de protección federal", arhsiglas = "SEPROFED", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 905, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC205", arhdescripcion = "Servicio Geológico Mexicano", arhsiglas = "SGM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 906, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC206", arhdescripcion = "Servicio Nacional de sanidad inocuidad y calidad alimentaria", arhsiglas = "SENASICA", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 907, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC207", arhdescripcion = "Servicio postal Mexicano", arhsiglas = "SEPOMEX", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 908, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC208", arhdescripcion = "Servicios a la navegación en el espacio aéreo Mexicano", arhsiglas = "SENEAM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 909, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC209", arhdescripcion = "Servicios aeroportuarios de la ciudad de méxico S.A.de.C.V.", arhsiglas = "SACM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 910, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC210", arhdescripcion = "Sistema Nacional para el desarrollo Integral de la familia", arhsiglas = "DIF", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 911, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC211", arhdescripcion = "Sociedad hipotecaria Federal S.N.C.", arhsiglas = "SHF", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 912, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC212", arhdescripcion = "Superissste", arhsiglas = "SUPERISSSTE", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 913, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC213", arhdescripcion = "Talleres gráficos de méxico", arhsiglas = "TGM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 914, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC214", arhdescripcion = "Telecomunicaciones de méxico", arhsiglas = "TELECOM", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 915, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC215", arhdescripcion = "Televisión metropolitana S.A.de C.V.", arhsiglas = "TVMETRO", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 916, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC216", arhdescripcion = "Universidad pedagógica Nacional", arhsiglas = "UPN", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 917, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC217", arhdescripcion = "Instituto Nacional de Geografía e Historia", arhsiglas = "INEGI", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 918, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC218", arhdescripcion = "Luz y Fuerza del Centro", arhsiglas = "LYC", arhreporta = 1, anlclave = 4, atpclave = 3 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 919, arhfecini = FechaUtil.Fecha("01/01/2004"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "OIC219", arhdescripcion = "Fideicomiso de Ferrocarriles Nacionales de Mexico", arhsiglas = "FNM", arhreporta = 1, anlclave = 4, atpclave = 3 });

            // INFOMRACION DE LAS UNIDDES DE TRANSPARENCIA..

            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 920, arhfecini = FechaUtil.Fecha("01/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "120DA1", arhdescripcion = "Dirección de transparencia y asesoria", arhsiglas = "DGT - DTA", arhreporta = 110, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 921, arhfecini = FechaUtil.Fecha("01/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "120SD1", arhdescripcion = "Subdireccción de gestión de información", arhsiglas = "DGT - SGI", arhreporta = 920, anlclave = 6, atpclave = 1 });
            lstAreaHist.Add(new SIT_ADM_AREAHIST { araclave = 922, arhfecini = FechaUtil.Fecha("01/01/2017"), arhfecfin = FechaUtil.Fecha("31/12/2035"), arhclaveua = "120JD1", arhdescripcion = "Jefatura de departamento de tratamiento de datos personales", arhsiglas = "DGT - JDTDP", arhreporta = 921, anlclave = 6, atpclave = 1 });

            
            return lstAreaHist;
        }
        public object dmlSelectAreaGestion( )
        {
            List<SIT_ADM_AREAGESTION> lstAreaGestion = new List<SIT_ADM_AREAGESTION>();

            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 1, agndescripcion = "Estructura Orgánica SFP 2004", agnfecini = FechaUtil.Fecha("01/01/2004"), agnfecfin = FechaUtil.Fecha("31/12/2004") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 2, agndescripcion = "Estructura Orgánica SFP 2005", agnfecini = FechaUtil.Fecha("01/01/2005"), agnfecfin = FechaUtil.Fecha("31/12/2005") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 3, agndescripcion = "Estructura Orgánica SFP 2006", agnfecini = FechaUtil.Fecha("01/01/2006"), agnfecfin = FechaUtil.Fecha("31/12/2006") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 4, agndescripcion = "Estructura Orgánica SFP 2007", agnfecini = FechaUtil.Fecha("01/01/2007"), agnfecfin = FechaUtil.Fecha("31/12/2007") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 5, agndescripcion = "Estructura Orgánica SFP 2008", agnfecini = FechaUtil.Fecha("01/01/2008"), agnfecfin = FechaUtil.Fecha("31/12/2008") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 6, agndescripcion = "Estructura Orgánica SFP 2009", agnfecini = FechaUtil.Fecha("01/01/2009"), agnfecfin = FechaUtil.Fecha("31/12/2009") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 7, agndescripcion = "Estructura Orgánica SFP 2010", agnfecini = FechaUtil.Fecha("01/01/2010"), agnfecfin = FechaUtil.Fecha("31/12/2010") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 8, agndescripcion = "Estructura Orgánica SFP 2011", agnfecini = FechaUtil.Fecha("01/01/2011"), agnfecfin = FechaUtil.Fecha("31/12/2011") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 9, agndescripcion = "Estructura Orgánica SFP 2012", agnfecini = FechaUtil.Fecha("01/01/2012"), agnfecfin = FechaUtil.Fecha("31/12/2012") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 10, agndescripcion = "Estructura Orgánica SFP 2013", agnfecini = FechaUtil.Fecha("01/01/2013"), agnfecfin = FechaUtil.Fecha("31/12/2013") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 11, agndescripcion = "Estructura Orgánica SFP 2014", agnfecini = FechaUtil.Fecha("01/01/2014"), agnfecfin = FechaUtil.Fecha("31/12/2014") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 12, agndescripcion = "Estructura Orgánica SFP 2015", agnfecini = FechaUtil.Fecha("01/01/2015"), agnfecfin = FechaUtil.Fecha("31/12/2015") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 13, agndescripcion = "Estructura Orgánica SFP 2016", agnfecini = FechaUtil.Fecha("01/01/2016"), agnfecfin = FechaUtil.Fecha("31/12/2016") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 14, agndescripcion = "Estructura Orgánica SFP 2017", agnfecini = FechaUtil.Fecha("01/01/2017"), agnfecfin = FechaUtil.Fecha("12/01/2017") });
            lstAreaGestion.Add(new SIT_ADM_AREAGESTION { agnclave = 15, agndescripcion = "Estructura Orgánica SFP 2017", agnfecini = FechaUtil.Fecha("13/01/2017"), agnfecfin = FechaUtil.Fecha("31/12/2017") });

            return lstAreaGestion;
        }

        public List<SIT_ADM_PERFIL> dmlSelectPerfil( )
        {
            List<SIT_ADM_PERFIL> lstDatos = new List<SIT_ADM_PERFIL>();
            lstDatos.Add(new SIT_ADM_PERFIL { perclave = 1, persigla = "Sistema", perdescripcion = "Sistema", perfecbaja = new DateTime(), permultiple = 0 });
            lstDatos.Add(new SIT_ADM_PERFIL { perclave = 2, persigla = "INAI", perdescripcion = "INAI", perfecbaja = new DateTime(), permultiple = 0 });
            lstDatos.Add(new SIT_ADM_PERFIL { perclave = 3, persigla = "UT", perdescripcion = "Unidad de Transparencia", perfecbaja = new DateTime(), permultiple = 0 });
            lstDatos.Add(new SIT_ADM_PERFIL { perclave = 4, persigla = "PRUT", perdescripcion = "Personal Responsable de la Unidad de Transparencia", perfecbaja = new DateTime(), permultiple = 0 });
            lstDatos.Add(new SIT_ADM_PERFIL { perclave = 5, persigla = "UA", perdescripcion = "Unidad Administrativa", perfecbaja = new DateTime(), permultiple = 1 });
            lstDatos.Add(new SIT_ADM_PERFIL { perclave = 6, persigla = "CT", perdescripcion = "Comite de Transparencia", perfecbaja = new DateTime(), permultiple = 0 });
            lstDatos.Add(new SIT_ADM_PERFIL { perclave = 7, persigla = "RF", perdescripcion = "Revisión Forma", perfecbaja = new DateTime(), permultiple = 0 });
            return lstDatos;
        }

        public List<SIT_ADM_USUARIO> dmlSelectUsuario( )
        {
            List<SIT_ADM_USUARIO> lstDatos = new List<SIT_ADM_USUARIO>();
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 3,  usrnombre = "LUIS", usrpaterno = "GÓMEZ", usrmaterno = "RIVERA", usrpuesto = "SUBDIRECTOR EN DESARROLLO DE TECNOLOGÍA WEB", usrcontraseña = "lgomezr", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lgomezr@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()   });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 92, usrnombre = "ALEJANDRO", usrpaterno = "DURAN", usrmaterno = "ZARATE", usrpuesto = "DIRECTOR GENERAL ADJUNTO DE PROCEDIMIENTOS Y SERVICIOS LEGALES", usrcontraseña = "aduranz", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "aduranz@funcionpublica.gob.mx", usrextension = "2382", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 93, usrnombre = "NAOMI CLAUDIA", usrpaterno = "ASATANI", usrmaterno = "KIMURA", usrpuesto = "DIRECTOR DE INNOVACIÓN JURÍDICA", usrcontraseña = "ncasatani", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ncasatani@funciojnpublica.gob.mx", usrextension = "0", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 101, usrnombre = "ISRAEL", usrpaterno = "SANCHEZ", usrmaterno = "COSS", usrpuesto = "SUBDIRECTOR DE PROCEDIMIENTOS Y SERVICIOS LEGALES", usrcontraseña = "isanchez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "isanchez@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 102, usrnombre = "ANAYELLI", usrpaterno = "JURADO", usrmaterno = "OVIEDO", usrpuesto = "SUBDIRECTOR DE OBLIGACIONES DE TRANSPARENCIA", usrcontraseña = "ajurado", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ajurado@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 103, usrnombre = "ANTONIO", usrpaterno = "REYES", usrmaterno = "HERNANDEZ", usrpuesto = "SUBDIRECTOR DE PROCEDIMIENTOS Y SERVICIOS LEGALES", usrcontraseña = "anreyes", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "anreyes@funcionpublica.gob.mx", usrextension = "2382", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 104, usrnombre = "ANA", usrpaterno = "SANTIAGO", usrmaterno = "MARTINEZ", usrpuesto = "SUBDIRECTOR DE PROCEDIMIENTOS Y SERVICIOS LEGALES", usrcontraseña = "asantiago", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "asantiago@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 105, usrnombre = "AGUSTIN", usrpaterno = "TORRES", usrmaterno = "IBARROLA", usrpuesto = "DIRECTOR GENERAL DE ATENCIÓN CIUDADANA", usrcontraseña = "atorres", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "atorres@funcionpublcia.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 106, usrnombre = "ELIAS", usrpaterno = "SANCHEZ", usrmaterno = "SANCHEZ", usrpuesto = "LIDER DE PROYECTO A", usrcontraseña = "esanchezs", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "esanchezs@funcionpublica.gob.mx", usrextension = "2266", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 107, usrnombre = "MA. CONCEPCION", usrpaterno = "CARLOS", usrmaterno = "BASURTO", usrpuesto = "SECRETARIO PARTICULAR DE SUBSECRETARIO", usrcontraseña = "ccarlos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ccarlos@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 108, usrnombre = "ANA LAURA", usrpaterno = "ORTÍZ", usrmaterno = "GARCÍA", usrpuesto = "ASESOR DE OFICIAL MAYOR SD", usrcontraseña = "aortiz", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "aortiz@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 109, usrnombre = "ELIZABETH OSWELIA", usrpaterno = "YAÑEZ", usrmaterno = "ROBLES", usrpuesto = "SUBSECRETARIO DE RESPONSABILIDADES ADMINISTRATIVAS Y CONTRATACIONES PUBLICAS", usrcontraseña = "eoyañez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "eoyañez@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 111, usrnombre = "ALFONSO", usrpaterno = "BRAVO", usrmaterno = "ALVAREZ MALO", usrpuesto = "SUBDIRECTOR DE RESOLUCION DE ASUNTOS EN MATERIA DE ETICA, INTEGRIDAD PUBLICA Y PREVENCION DE CONFLICTOS DE INTERES", usrcontraseña = "abravo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "abravo@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 112, usrnombre = "SALVADOR", usrpaterno = "VEGA", usrmaterno = "CASILLAS", usrpuesto = "SECRETARIO DE LA FUNCION PUBLICA", usrcontraseña = "svega", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "svega@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 113, usrnombre = "JORGE PABLO", usrpaterno = "BUTTANDA", usrmaterno = "CALDERÓN", usrpuesto = "Director de Gestión y Enlace", usrcontraseña = "jbuttanda", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jbuttanda@funcionpublica.gob.mx", usrextension = "1095", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 114, usrnombre = "CARLOS", usrpaterno = "CORRAL", usrmaterno = "VEALE", usrpuesto = "DIRECTOR GENERAL ADJUNTO DE CONTROL Y EVALUACION", usrcontraseña = "ccorral", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ccorral@funcionpublcia.gob.mx", usrextension = "1073", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 150, usrnombre = "JESÚS GUILLERMO", usrpaterno = "NUÑEZ", usrmaterno = "CURRY", usrpuesto = "DIRECTOR DE GESTION Y ENLACE", usrcontraseña = "jnuñez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jnuñez@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 200, usrnombre = "CARLOS ENRÍQUEZ", usrpaterno = "BEZARES", usrmaterno = "CASTREJÓN", usrpuesto = "ASESOR DE SUBSECRETARIO DA", usrcontraseña = "cbezares", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cbezares@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 201, usrnombre = "MARÍA GUADALUPE", usrpaterno = "YAN", usrmaterno = "RUBIO", usrpuesto = "SUBSECRETARIO DE CONTROL Y AUDITORIA DE LA GESTION PUBLICA", usrcontraseña = "myan", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "myan@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 240, usrnombre = "LIVIA ARACELI", usrpaterno = "FORTIZ", usrmaterno = "TORRES", usrpuesto = "DIRECTOR DE SEGUIMIENTO Y VERIFICACION", usrcontraseña = "lfortiz", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lfortiz@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 241, usrnombre = "EPIFANIO", usrpaterno = "MAMAHUA", usrmaterno = "ROMAN", usrpuesto = "DIRECTOR DE FORTALECIMIENTO DE CONTROL INTERNO", usrcontraseña = "emamahua", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "emaamhua@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 246, usrnombre = "DANIEL FRANCISCO", usrpaterno = "MOSQUEIRA", usrmaterno = "MONDRAGÓN", usrpuesto = "SUBDIRECTOR DE AUDITORIAS EXTERNAS", usrcontraseña = "dmosqueira", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "dmosqueira@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 251, usrnombre = "ZENAIDA ANTONIETA", usrpaterno = "PINTO", usrmaterno = "PALMER", usrpuesto = "SUBENLACE DE APOYO DE TRANSPARENCIA", usrcontraseña = "zapinto", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "zapinto@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 261, usrnombre = "MARIA ELENA", usrpaterno = "ZALDIVAR", usrmaterno = "SANCHEZ", usrpuesto = "DIRECTOR DE SEGUIMIENTO DE RESPONSABILIDADES", usrcontraseña = "mezaldivar", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mezaldivar@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 265, usrnombre = "DELIA", usrpaterno = "PÉREZ", usrmaterno = "OTERO", usrpuesto = "DIRECTOR DE CONTROL PRESUPUESTAL, CAPACITACION Y DESARROLLO EN AUDITORIA A OBRA PUBLICA", usrcontraseña = "dotero", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "dotero@funcionpublcia.gob.mx", usrextension = "3116", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 266, usrnombre = "JAIME", usrpaterno = "GONZÁLEZ", usrmaterno = "MONTES", usrpuesto = "DIRECTOR GENERAL ADJUNTO DE PLANEACION Y CONTROL DE RECURSOS PARA AUDITORIA A OBRA PUBLICA", usrcontraseña = "jgonzalez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jgonzalez@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 300, usrnombre = "AIDA", usrpaterno = "RODRÍGUEZ", usrmaterno = "CRUZ", usrpuesto = "COORDINADOR DE ASESORES DE SUBSECRETARIO", usrcontraseña = "arodriguez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "arodriguez@funcionpublica.gob.mx", usrextension = "2017", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 310, usrnombre = "LIZBETH", usrpaterno = "REYES", usrmaterno = "URQUIZA", usrpuesto = "COORDINADOR TECNICO DE UNIDAD", usrcontraseña = "lreyesu", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lreyesu@funcionpublica.gob.mx", usrextension = "2199", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 321, usrnombre = "MIGUEL GUILLERMO", usrpaterno = "ARAGÓN", usrmaterno = "LAGUNAS", usrpuesto = "DIRECTOR GENERAL DE DENUNCIAS E INVESTIGACIONES", usrcontraseña = "maragon", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "maragon@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 350, usrnombre = "FERNANDO", usrpaterno = "PEREA", usrmaterno = "COBOS", usrpuesto = "VISITADOR", usrcontraseña = "fperea", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "fperea@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 400, usrnombre = "PABLO", usrpaterno = "SANCHEZ", usrmaterno = "SERVITJE", usrpuesto = "SECRETARIO TECNICO DE SUBSECRETARIO", usrcontraseña = "psanchez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "psanchez@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 402, usrnombre = "EDUARDO", usrpaterno = "RODRÍGUEZ", usrmaterno = "CISNEROS", usrpuesto = "SUBDIRECTOR DE DISEÑO DE POLITICAS DE EVALUACION", usrcontraseña = "erodriguez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "erodriguez@funcionpublica.gob.mx", usrextension = "4139", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 405, usrnombre = "OMAR", usrpaterno = "GONZALEZ", usrmaterno = "MARTINEZ", usrpuesto = "DIRECTOR DE SEGUIMIENTO Y EVALUACION", usrcontraseña = "ogonzalez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ogonzalez@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 407, usrnombre = "NORMA LOURDES", usrpaterno = "HUERTA", usrmaterno = "BARRON", usrpuesto = "SUBDIRECTOR DE CULTURA Y CAMBIO ORGANIZACIONAL", usrcontraseña = "nhuerta", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "nhuerta@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 409, usrnombre = "MONICA FERNANDA", usrpaterno = "SOSAPAVON", usrmaterno = "PEREZ", usrpuesto = "SUBDIRECTOR DE CULTURA Y CAMBIO ORGANIZACIONAL", usrcontraseña = "fsosapavon", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "fsosapavon@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 410, usrnombre = "AMANDA", usrpaterno = "PEREZ", usrmaterno = "MORALES", usrpuesto = "SUBDIRECTOR DE REGULACION DE LA EVALUACION", usrcontraseña = "amperez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "amperez@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 412, usrnombre = "FRANCISCO", usrpaterno = "MALDONADO", usrmaterno = "VENEGAS", usrpuesto = "DIRECTOR DE SIMPLIFICACION REGULATORIA INSTITUCIONAL", usrcontraseña = "fmaldonado", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "fmaldonado@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 416, usrnombre = "ROBERTO EVARISTO", usrpaterno = "PATIÑO", usrmaterno = "GUAJARDO", usrpuesto = "DIRECTOR DE EVALUACION DE PROGRAMAS", usrcontraseña = "rpatino", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rpatino@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 440, usrnombre = "JOSÉ MANUEL", usrpaterno = "GARCÍA", usrmaterno = "BRAVO", usrpuesto = "JEFE DE DEPARTAMENTO DE GESTIÓN DE RECURSOS HUMANOS", usrcontraseña = "jgarciab", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jgarciab@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 500, usrnombre = "CECILIA", usrpaterno = "VILLEGAS", usrmaterno = "HERNANDEZ", usrpuesto = "SECRETARIO PARTICULAR DE OFICIAL MAYOR", usrcontraseña = "cvillegas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cvillegas@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 511, usrnombre = "CRISÓFORO", usrpaterno = "REYES", usrmaterno = "CASTREJÓN", usrpuesto = "DIRECTOR DE CONTABILIDAD", usrcontraseña = "creyes", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "creyes@funcionpublica.gob.mx", usrextension = "5004", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 520, usrnombre = "CLAUDIA LORENA", usrpaterno = "ISLAS", usrmaterno = "VARGAS", usrpuesto = "DIRECTOR DE PROCESOS", usrcontraseña = "clislas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "clislas@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 530, usrnombre = "JORGE LUIS", usrpaterno = "GORTAREZ", usrmaterno = "HERNÁNDEZ", usrpuesto = "DIRECTOR DE INGRESO Y CONTROL DE PLAZAS", usrcontraseña = "jgortarez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jgortarez@funcionpublica.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 630, usrnombre = "N/A", usrpaterno = "N/A", usrmaterno = "", usrpuesto = "COORDINACIÓN DE DELEGACIONES", usrcontraseña = "ioramirez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ioramirez@cabin.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 650, usrnombre = "M", usrpaterno = "ROSADO", usrmaterno = "", usrpuesto = "DIRECCIÓN GENERAL DE ADMINISTRACIÓN Y OBRAS EN EDIFICIOS PÚBLICOS", usrcontraseña = "mrosado", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mrosado@cabin.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 660, usrnombre = "M", usrpaterno = "VAZQUEZ", usrmaterno = "", usrpuesto = "DIRECCIÓN DE INFORMÁTICA", usrcontraseña = "vazquezm", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "vazquezm@cabin.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 672, usrnombre = "JUAN JOSÉ", usrpaterno = "BORTONI", usrmaterno = "GARZA", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "juan.bortoni", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "juan.bortoni@sep.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 673, usrnombre = "JOSÉ ANTONIO", usrpaterno = "VIZCAÍNO", usrmaterno = "RUIZ", usrpuesto = "COORDINADOR DE VINCULACIÓN OPERATIVA - OICIMSS", usrcontraseña = "jose.vizcaino", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jose.vizcaino@imss.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 675, usrnombre = "GABRIEL", usrpaterno = "PIEDRA", usrmaterno = "ROMERO", usrpuesto = "Titular del Área de Auditoria para Desarrollo y Mejora de la Gestión Pública - OICPEP", usrcontraseña = "gabriel.piedra", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gabriel.piedra@pemex.com", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 684, usrnombre = "DAVID", usrpaterno = "MAYA", usrmaterno = "JIMENEZ", usrpuesto = "ENLACE", usrcontraseña = "dmaya", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "dmaya@ipn.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 685, usrnombre = "LUIS", usrpaterno = "HERNANDEZ", usrmaterno = "URIBE", usrpuesto = "TITULAR DEL ÁREA DE AUITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "luis.huribe", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "luis.huribe@sagarpa.gob.mx", usrextension = "0", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 686, usrnombre = "GREGORIO", usrpaterno = "CASTILLA", usrmaterno = "MUÑOZ", usrpuesto = "DIRECTOR DE  INCONFORMIDADES Y SANCIONES  - OICSSP", usrcontraseña = "gcastilla", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gcastilla@segob.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 687, usrnombre = "RICARDO GABRIEL", usrpaterno = "GARCIA", usrmaterno = "ROJAS ALARCÓN", usrpuesto = "TITULAR DE LAS  ÁREAS DE RESPONSABILIDADES Y QUEJAS -OICSHCP", usrcontraseña = "ricardo_garciarojas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ricardo_garciarojas@hacienda.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 688, usrnombre = "ANTONIO HORACIO", usrpaterno = "GAMBOA", usrmaterno = "CHABBÁN", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "sandra.lara", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "sandra.lara@economia.gob.mx ", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 689, usrnombre = "FRANCISCO ROMÁN", usrpaterno = "PÉREZ", usrmaterno = "SOLÍS", usrpuesto = "N/A", usrcontraseña = "francisco.roman.perez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "francisco.roman.perez@pemex.com", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 692, usrnombre = "CARLOS", usrpaterno = "PATTERSON", usrmaterno = "OLIVAS", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA Y DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA -(OICREFINA)", usrcontraseña = "carlos.patterson", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "carlos.patterson@pemex.com", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 695, usrnombre = "EDITH", usrpaterno = "LARIOS", usrmaterno = "TAPIA", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y QUEJAS -", usrcontraseña = "edith.larios", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "edith.larios@bachilleres.edu.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 696, usrnombre = "IRIS LUCIA", usrpaterno = "RANGEL", usrmaterno = "VALDERRAMA", usrpuesto = "CONSULTOR DE EVALUACIÓN DE TRÁMITES Y PROCESOS -OICCNA", usrcontraseña = "Iris.rangel", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "Iris.rangel@conagua.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 698, usrnombre = "PABLO IGARTUA", usrpaterno = "Méndez", usrmaterno = "Padilla", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS Y RESPONSABILIDADES -OICINEGI", usrcontraseña = "pablo.igartua", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "pablo.igartua@inegi.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 699, usrnombre = "JUAN CARLOS", usrpaterno = "ROBLES", usrmaterno = "ROBLES", usrpuesto = "SUBDIRECTOR DE EVALUACIÓN DEL O.I.C. -OICFONAES", usrcontraseña = "jrobles@infonaes.gob.mx", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jrobles@infonaes.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 702, usrnombre = "ALFREDO", usrpaterno = "MEDINA", usrmaterno = "MENDEZ", usrpuesto = "COORDINADOR ADMINISTRATIVO -OICLYFC", usrcontraseña = "lyfc.gob", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "zirzu@lyf.gob", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 703, usrnombre = "YANELLI", usrpaterno = "LÓPEZ", usrmaterno = "BOTELLO", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS -OICPROFECO", usrcontraseña = "ylopezb", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ylopezb@profeco.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 706, usrnombre = "JESUS GUILLERMO", usrpaterno = "MOSQUEDA", usrmaterno = "MARTÍNEZ", usrpuesto = "TITULAR DEL O.I.C. -OICAPIMANZ", usrcontraseña = "jesus.mosqueda", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jesus.mosqueda@apimanzanillo.com.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 708, usrnombre = "VICTOR", usrpaterno = "NUÑEZ", usrmaterno = "MONTOYA", usrpuesto = "DIRECTOR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "victor.nunez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "victor.nunez@sedesol.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 714, usrnombre = "MA. DE LOS ÁNGELES", usrpaterno = "HERNÁNDEZ", usrmaterno = "BELLO", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA - SEPOMEX", usrcontraseña = "mdlahernandez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mdlahernandez@correosdemexico.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 716, usrnombre = "DULCE ANGÉLICA", usrpaterno = "MARTÍNEZ", usrmaterno = "CORTE", usrpuesto = "JEFA DE DEPARTAMENTO", usrcontraseña = "asa.gob.mx", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "damartinezc@asa.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 720, usrnombre = "NORMA", usrpaterno = "HERNÁNDEZ", usrmaterno = "CARMONA", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS - CORETT", usrcontraseña = "norma.hernandez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "norma.hernandez@corett.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 722, usrnombre = "MAURICIO", usrpaterno = "RAZO", usrmaterno = "SÁNCHEZ", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "mauricio.razo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mauricio.razo@sedatu.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 734, usrnombre = "MARCO ANTONIO JAIMES", usrpaterno = "Jaimes", usrmaterno = "RODRÍGUEZ", usrpuesto = "GERENTE DE RESPONSABILIDADES, QUEJAS, DENUNCIAS E INCONFORMIDADES -OICCPTM", usrcontraseña = "mjaimes", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mjaimes@promotur.com.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 735, usrnombre = "JOSÉ", usrpaterno = "Quintero", usrmaterno = "ARÉCHIGA", usrpuesto = "TITULAR DEL AREA DE AUDITORIA, CONTROL Y EVALUACION -OICFNM", usrcontraseña = "oicfnm_", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jquintero@fnm.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 739, usrnombre = "EZEQUIEL", usrpaterno = "RAMÍREZ", usrmaterno = "GÓMEZ", usrpuesto = "TITULAR", usrcontraseña = "ezequiel.ramirez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ezequiel.ramirez@imer.com.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 742, usrnombre = "BRAYAN SAID", usrpaterno = "NIETO", usrmaterno = "ORTEGA", usrpuesto = "SOPORTE ADMINISTRATIVO AS - OICINNN", usrcontraseña = "respo_innn", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "respo_innn@innn.edu.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 748, usrnombre = "MARIO", usrpaterno = "BLAS", usrmaterno = "ALVEAR", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA -OICP.G.P.B", usrcontraseña = "maria.veronica.massieu", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "maria.veronica.massieu@pemex.com", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 749, usrnombre = "GERARDO", usrpaterno = "ESPARZA", usrmaterno = "NEUMANN", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES -OICP.P.", usrcontraseña = "gerardo.esparza", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gerardo.esparza@pemex.com", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 750, usrnombre = "JORGE", usrpaterno = "PERALTA", usrmaterno = "PORRAS", usrpuesto = "TITULAR DEL AREA DE RESPONSABILIDADES -COFEPRIS", usrcontraseña = "jperaltap", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jperaltap@cofepris.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 753, usrnombre = "OSCAR", usrpaterno = "MALDONADO", usrmaterno = "PERALTA", usrpuesto = "JEFE DE DEPARTAMENTO", usrcontraseña = "oscar.maldonado", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oscar.maldonado@promexico.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 755, usrnombre = "JOSÉ ISMAEL", usrpaterno = "JACOBO", usrmaterno = "RODRÍGUEZ", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y QUEJAS -III-SERVICIOS", usrcontraseña = "jijacobo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jijacobo@iiiservicios.com", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 810, usrnombre = "C. MARGARITA LANDAVERDE", usrpaterno = "LANDAVERDE", usrmaterno = "CASANOVA", usrpuesto = "DIRECTOR DE PROMOCION E INTEGRACION DE GOBIERNO DIGITAL", usrcontraseña = "mlandaverde", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mlandaverde@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 811, usrnombre = "RONALD BRYAN", usrpaterno = "COOPER", usrmaterno = "MONTES", usrpuesto = "UNIDAD DE GOBIERNO ELECTRÓNICO Y POLITICA DE TECNOLOGÍAS DE LA INFORMACIÓN", usrcontraseña = "rcooper", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rcooper@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 815, usrnombre = "GUILLERMO SERAFIN", usrpaterno = "GUADARRAMA", usrmaterno = "BRITO", usrpuesto = "UNIDAD DE GOBIERNO ELECTRÓNICO Y POLITICA DE TECNOLOGÍAS DE LA INFORMACIÓN", usrcontraseña = "gguadarrama", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gguadarrama@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 820, usrnombre = "ROSALVA MARIA GUADALUPE", usrpaterno = "TRIGOS", usrmaterno = "RUIZ", usrpuesto = "UNIDAD DE VINCULACIÓN PARA LA TRANSPARENCIA", usrcontraseña = "rtrigos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rtrigos@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 826, usrnombre = "ARA ANTZ AZU", usrpaterno = "VERASTEGUI", usrmaterno = "SOLORIO", usrpuesto = "UNIDAD DE VINCULACIÓN PARA LA TRANSPARENCIA", usrcontraseña = "averastegui", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "averastegui@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 831, usrnombre = "Valentina", usrpaterno = "VALDEZ", usrmaterno = "JASSO", usrpuesto = "UPTCI", usrcontraseña = "vvaldez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "vvaldez@funcionpublica.gob.mx", usrextension = "", usrtitulo = "Mtra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 840, usrnombre = "BLANCA ALICIA", usrpaterno = "MENDOZA", usrmaterno = "VERA", usrpuesto = "COORDINADOR GENERAL DE ORGANOS DE VIGILANCIA Y CONTROL", usrcontraseña = "bamendoza", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bamendoza@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 845, usrnombre = "RUBICELIA", usrpaterno = "MARTÍNEZ", usrmaterno = "HERNÁNDEZ", usrpuesto = "COORDINADORA TÉCNICA DE ÓRGANOS DE VIGILANCIA Y CONTROL", usrcontraseña = "rmartinez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rmartinez@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 855, usrnombre = "ANGELICA", usrpaterno = "GONZALEZ", usrmaterno = "VALENCIA", usrpuesto = "DIRECCION GENERAL ADJUNTA DE QUEJAS, RESPONSABILIDADES  E INCONFORMIDADES", usrcontraseña = "valencia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "valencia@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 861, usrnombre = "RUPERTO", usrpaterno = "NARVÁEZ", usrmaterno = "BELLAZETIN", usrpuesto = "DIRECTOR GENERAL DE RECURSOS HUMANOS", usrcontraseña = "rnarvaez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rnarvaez@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 865, usrnombre = "ROBERTO", usrpaterno = "JUAREZ", usrmaterno = "SALINAS", usrpuesto = "COORDINADOR ADMINISTRATIVO DE SECRETARIO", usrcontraseña = "rjuarezs", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rjuarezs@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 875, usrnombre = "BLANCA ROSARIO", usrpaterno = "VARELA", usrmaterno = "NUÑEZ", usrpuesto = "SUBDIRECTOR DE ENLACE CON ESTADOS", usrcontraseña = "bvarela", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bvarela@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 911, usrnombre = "ROCÍO", usrpaterno = "PICAZO", usrmaterno = "PALOMINO", usrpuesto = "SOPORTE ADMINISTRATIVO C", usrcontraseña = "rocio.picazo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rocio.picazo@inper.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 950, usrnombre = "ERIKA", usrpaterno = "GARCÍA", usrmaterno = "PACHECO", usrpuesto = "SUBDIRECTORA DE ASESORIA Y CONSULTA A INSTITUCIONES EN MATERIA DE ÉTICA", usrcontraseña = "egarciap", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "egarciap@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 999, usrnombre = "PEDRO", usrpaterno = "PARADA", usrmaterno = "HERNANDEZ", usrpuesto = "SUBDELEGADO Y COMISARIO PUBLICO SUPLENTE", usrcontraseña = "ppherna", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ppherna@funcionpublica.gob.mx", usrextension = "", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1000, usrnombre = "Makdihel", usrpaterno = "Laudino", usrmaterno = "Santillán", usrpuesto = "DIRECTOR DE ARQUITECTURA DE SOFTWARE", usrcontraseña = "mlaudino", usrfecbaja = new DateTime(), usrcorreo = "mlaudino@funcionpublica.gob.mx", usrextension = "5066", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1001, usrnombre = "Juan José", usrpaterno = "Vela", usrmaterno = "Martínez", usrpuesto = "COORDINADOR DE LOGISTICA Y EVENTOS INSTITUCIONALES DE LA OFICINA DEL C. SECRETARIO", usrcontraseña = "jvela", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jvela@funcionpublica.gob.mx", usrextension = "1063", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1002, usrnombre = "Citlaly", usrpaterno = "Sandoval", usrmaterno = "Hernandez", usrpuesto = "SUBDIRECTORA DE SERVICIOS E INOVACION JURIDICOS", usrcontraseña = "csandova", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "csandoval@funcionpublica.gob.mx", usrextension = "4161", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1003, usrnombre = "P. Elizabeth", usrpaterno = "Jacobo", usrmaterno = "Guzmán", usrpuesto = "SUBDIRECTOR DE MEDIOS DE PERCEPCIÓN", usrcontraseña = "ejacobo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ejacobo@funcionpublica.gob.mx", usrextension = "1144", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1004, usrnombre = "Roberto Carlos", usrpaterno = "Corral", usrmaterno = "Veale", usrpuesto = "DIRECTOR GENERAL ADJUNTO", usrcontraseña = "rcorral", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rcorral@funcionpublica.gob.mx", usrextension = "1073", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1005, usrnombre = "Cynthia Viridiana", usrpaterno = "Castro", usrmaterno = "Martínez", usrpuesto = "SUBDIRECTORA DE INFORMACIÓN Y TRANSPARENCIA", usrcontraseña = "cvcastro", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cvcastro@funcionpublica.gob.mx", usrextension = "2210", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1006, usrnombre = "Xochitl", usrpaterno = "Escutia", usrmaterno = "Flores", usrpuesto = "SUBDIRECTORA DE VINCULACIÓN CON GOBIERNO Y SOCIEDAD", usrcontraseña = "xescutia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "xescutia@funcionpublica.gob.mx", usrextension = "1130", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1007, usrnombre = "Pamela Montserrat", usrpaterno = "Huizar", usrmaterno = "Garza", usrpuesto = "SUBDIRECTORA DE PROYECTOS DE INTEGRIDAD", usrcontraseña = "phuizar", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "phuizar@funcionpublica.gob.mx", usrextension = "1154", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1008, usrnombre = "Hugo Alejandro", usrpaterno = "Carbajal", usrmaterno = "Cruz", usrpuesto = "DIRECTOR DE CONTROL Y SEGUIMIENTO", usrcontraseña = "hcarbajal", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "hcarbajal@funcionpublica.gob.mx", usrextension = "5062", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1009, usrnombre = "Mayra", usrpaterno = "Arriaga", usrmaterno = "Mendoza", usrpuesto = "SUBDIRECTORA DE CONTROL Y SEGUIMIENTO", usrcontraseña = "marriaga", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "marriaga@funcionpublica.gob.mx", usrextension = "5422", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1010, usrnombre = "Héctor Rodrigo", usrpaterno = "Novelo", usrmaterno = "González", usrpuesto = "JEFE DE DEPARTAMENTO DE CONTROL Y SEGUIMIENTO", usrcontraseña = "hnovelo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "hnovelo@funcionpublica.gob.mx", usrextension = "5283", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1011, usrnombre = "Blanca", usrpaterno = "Vélez", usrmaterno = "Gallegos", usrpuesto = "DIRECTORA DE RESOLUCIÓN DE ASUNTOS EN MATERIA DE ÉTICA, INTEGRIDAD PÚBLICA Y PREVENCIÓN DE CONFLICTOS DE INTERÉS", usrcontraseña = "bvelez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bvelez@funcionpublica.gob.mx", usrextension = "1050", usrtitulo = "Mtra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1012, usrnombre = "Roberto", usrpaterno = "Najera", usrmaterno = "Vilchis", usrpuesto = "COORDINADOR DE ASESORES", usrcontraseña = "jnajera", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jnajera@funcionpublica.gob.mx", usrextension = "5007", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1014, usrnombre = "Salvador", usrpaterno = "Ramírez", usrmaterno = "Valles", usrpuesto = "SUBDIRECTOR DE PLANEACIÓN, EVALUACIÓN Y SEGUIMIENTO DE PROYECTOS DE TECNOLOGÍAS DE INFORMACIÓN", usrcontraseña = "sramirezv", usrfecbaja = new DateTime() , usrcorreo = "sramirezv@funcionpublica.gob.mx", usrextension = "5142", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1015, usrnombre = "Sonia", usrpaterno = "Ventura", usrmaterno = "Hilerio", usrpuesto = "DIRECTORA DE PROGRAMACIÓN Y EVALUACIÓN", usrcontraseña = "sventura", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "sventura@funcionpublica.gob.mx", usrextension = "5075", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1016, usrnombre = "María del Carmen", usrpaterno = "Ávila", usrmaterno = "Leyva", usrpuesto = "SUBDIRECTORA DE CONTROL DE PRESUPUESTO DE SERVICIOS GENERALES B", usrcontraseña = "mcavila", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mcavila@funcionpublica.gob.mx", usrextension = "5358", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1017, usrnombre = "Bertha Angelica", usrpaterno = "García", usrmaterno = "Cano", usrpuesto = "DIRECTORA DE PLANEACIÓN Y ATENCIÓN A CLIENTES", usrcontraseña = "bagarcia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bagarcia@funcionpublica.gob.mx", usrextension = "5040", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1018, usrnombre = "Luis Francisco", usrpaterno = "Oliveros", usrmaterno = "Ángeles", usrpuesto = "ASESOR DE SUBSECRETARIO DA", usrcontraseña = "loliveros", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "loliveros@funcionpublica.gob.mx", usrextension = "3036", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1019, usrnombre = "Delia", usrpaterno = "Pérez", usrmaterno = "Otero", usrpuesto = "DIRECTORA DE CAPACITACIÓN Y DESARROLLO EN AUDITORIA A OBRA PÚBLICA", usrcontraseña = "dotero", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "dotero@funcionpublica.gob.mx", usrextension = "3116", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1020, usrnombre = "Francisco Gustavo", usrpaterno = "López", usrmaterno = "Fuentes", usrpuesto = "SUBDIRECTOR DE SEGUIMIENTO Y VERIFICACIÓN", usrcontraseña = "fcoglopez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "fcoglopez@funcionpublica.gob.mx", usrextension = "3141", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1021, usrnombre = "María del Rocio", usrpaterno = "Portales", usrmaterno = "Monreal", usrpuesto = "SUBDIRECTORA DE NORMAS Y DISPOSICONES DE AUDITORIA", usrcontraseña = "mdrportales", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mdrportales@funcionpublica.gob.mx", usrextension = "3567", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1022, usrnombre = "David Fernando", usrpaterno = "Negrete", usrmaterno = "Castañón", usrpuesto = "DIRECTOR GENERAL ADJUNTO DE MEJORA DE LA GESTIÓN PÚBLICA ESTATAL", usrcontraseña = "dnegrete", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "dnegrete@funcionpublica.gob.mx", usrextension = "3019", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1023, usrnombre = "Gabriel", usrpaterno = "Montes de oca", usrmaterno = "Calderón", usrpuesto = "DIRECTORA DE AUDITORIAS EXTERNAS", usrcontraseña = "gmontesdeoca", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gmontesdeoca@funcionpublica.gob.mx", usrextension = "3486", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1024, usrnombre = "Selene Margarita", usrpaterno = "González", usrmaterno = "Súarez", usrpuesto = "ASESOR DE SUBSECRETARIO SD", usrcontraseña = "sgonzalez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "sgonzalez@funcionpublica.gob.mx", usrextension = "4258", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1025, usrnombre = "Lydia", usrpaterno = "Juárez", usrmaterno = "Aguilar", usrpuesto = "DIRECTOR DE CONTROL DE INFORMACION", usrcontraseña = "ljuarez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ljuarez@funcionpublica.gob.mx", usrextension = "4038", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1026, usrnombre = "Miguel Ángel", usrpaterno = "Contreras", usrmaterno = "García", usrpuesto = "DIRECCIÓN GENERAL DE DESARROLLO HUMANO Y SERVICIO PROFESIONAL DE CARRERA", usrcontraseña = "mcontreras", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mcontreras@funcionpublica.gob.mx", usrextension = "4162", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1027, usrnombre = "José", usrpaterno = "Huerta", usrmaterno = "Hernández", usrpuesto = "SUBDIRECTOR DE INFORMACIÓN ESTRATEGICA DE PERSONAL", usrcontraseña = "jhuerta", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jhuerta@funcionpublica.gob.mx", usrextension = "4171", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1028, usrnombre = "Victoria", usrpaterno = "Monroy", usrmaterno = "Carrillo", usrpuesto = "DIRECTORA DE ORGANIZACIÓN Y REMUNERACIONES", usrcontraseña = "vmonroy", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "vmonroy@funcionpublica.gob.mx", usrextension = "4045", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1029, usrnombre = "Rogelio", usrpaterno = "López", usrmaterno = "Acle", usrpuesto = "SUBDIRECTOR DE ORGANIZACIÓN Y REMUNERACIONES", usrcontraseña = "rlopeza", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rlopeza@funcionpublica.gob.mx", usrextension = "4151", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1030, usrnombre = "Arturo Omar", usrpaterno = "Solano", usrmaterno = "González", usrpuesto = "DIRECTOR DE PROMOCIÓN E INTEGRACIÓN DE GOBIERNO DIGITAL", usrcontraseña = "asolano", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "asolano@funcionpublica.gob.mx", usrextension = "4424", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1031, usrnombre = "Francisco Gustavo", usrpaterno = "Mier y Terán", usrmaterno = "Iza", usrpuesto = "DIRECTOR GENERAL ADJUNTO DE EFICIENCIA DE LA GESTIÓN PÚBLICA", usrcontraseña = "frmier", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "frmier@funcionpublica.gob.mx", usrextension = "4025", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1032, usrnombre = "Rocío Josefin", usrpaterno = "Ramos", usrmaterno = "Hernández", usrpuesto = "SUBDIRECTORA DE INNOVACIÓN Y POSICIONAMIENTO DE LAS POLÍTICAS DE MJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "rjramos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rjramos@funcionpublica.gob.mx", usrextension = "4155", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1033, usrnombre = "Bruno Rafael", usrpaterno = "Martínez", usrmaterno = "Villaseñor", usrpuesto = "COORDINADORA DE ASESORES DE SUBSECRETARIO", usrcontraseña = "brmartinez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "brmartinez@funcionpublica.gob.mx", usrextension = "2089", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1034, usrnombre = "María Guadalupe", usrpaterno = "Delgado", usrmaterno = "Torres", usrpuesto = "SUBDIRECTOR DE ADMINISTRACIÓN DE UNIDADES COMPRADORAS", usrcontraseña = "mgdelgado", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mgdelgado@funcionpublica.gob.mx", usrextension = "2398", usrtitulo = "Mtra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1035, usrnombre = "José Carlos Fabián", usrpaterno = "Ylizaliturri", usrmaterno = "Rodríguez", usrpuesto = "SUBDIRECTOR DE REINGENIERIA DE PROCESOS", usrcontraseña = "jylizaliturri", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jylizaliturri@funcionpublica.gob.mx", usrextension = "2430", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1037, usrnombre = "Lauro", usrpaterno = "Delgado", usrmaterno = "Terrón", usrpuesto = "DIRECTOR DE ASESORÍA Y CONSULTA", usrcontraseña = "ldelgado", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ldelgado@funcionpublica.gob.mx", usrextension = "2051", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1038, usrnombre = "José Gabriel", usrpaterno = "Carreño", usrmaterno = "Camacho", usrpuesto = "DIRECTOR GENERAL DE RESPONSABILIDADES Y SITUACIÓN PATRIMONIAL", usrcontraseña = "jcarreno", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jcarreno@funcionpublica.gob.mx", usrextension = "2003", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1039, usrnombre = "Heidi", usrpaterno = "Jimenez", usrmaterno = "Reyes", usrpuesto = "DIRECTORA DE ASESORIA Y CONSULTA", usrcontraseña = "hjimenez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "hjimenez@funcionpublica.gob.mx", usrextension = "2153", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1040, usrnombre = "Ana Laura", usrpaterno = "Gómez", usrmaterno = "Sánchez", usrpuesto = "ENLACE DE ALTO NIVEL DE RESPONSABILIDAD", usrcontraseña = "agomez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "agomez@funcionpublica.gob.mx", usrextension = "2376", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1041, usrnombre = "Veronica María", usrpaterno = "Uribe", usrmaterno = "Olvera", usrpuesto = "DIRECTORA DE CONTROL Y SEGUIMIENTO DE INFORMACIÓN", usrcontraseña = "vuribe", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "vuribe@funcionpublica.gob.mx", usrextension = "2122", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1042, usrnombre = "Gabriel", usrpaterno = "Cabrera", usrmaterno = "Hernández", usrpuesto = "ANALISTA DE DESEMPEÑO", usrcontraseña = "gcabrerah", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gcabrerah@funcionpublica.gob.mx", usrextension = "2253", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1043, usrnombre = "Manuel Fernando", usrpaterno = "Lizardi", usrmaterno = "Calderón", usrpuesto = "Titular de la Unidad Jurídica", usrcontraseña = "mlizardi", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mlizardi@funcionpublica.gob.mx", usrextension = "55-63-26-99 EXT. 499", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1044, usrnombre = "Guadalupe", usrpaterno = "Resendiz", usrmaterno = "Becerril", usrpuesto = "SUBDIRECTORA DE CONSULTA Y RECUPARACIÓN DE INMUEBLES", usrcontraseña = "bresendiz", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bresendiz@funcionpublica.gob.mx", usrextension = "55-63-26-99 EXT. 418", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1045, usrnombre = "Rudy", usrpaterno = "Albertos", usrmaterno = "Cámara", usrpuesto = "DIRECTOR  GENERAL DE ADMINISTRACIÓN Y FINANZAS", usrcontraseña = "ralbertos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ralbertos@funcionpublica.gob.mx", usrextension = "55-63-16-35", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1046, usrnombre = "Brenda Iradía", usrpaterno = "Mendoza", usrmaterno = "Pérez", usrpuesto = "SUBDIRECTORA DE RECURSOS HUMANOS Y CAPACITACIÓN", usrcontraseña = "bmendoza", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bmendoza@funcionpublica.gob.mx", usrextension = "55-63-26-99 EXT. 474", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1047, usrnombre = "Luis Enrique", usrpaterno = "Méndez", usrmaterno = "Ramírez", usrpuesto = "DIRECTOR GENERAL DE POLÍTICA DE GESTIÓN INMOBILIARIA", usrcontraseña = "lemendez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lemendez@funcionpublica.gob.mx", usrextension = "55-63-25-92", usrtitulo = "Urb.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1048, usrnombre = "Claudia Solange", usrpaterno = "Ayala", usrmaterno = "Sánchez", usrpuesto = "SUBDIRECTORA DE NORMATIVIDAD", usrcontraseña = "csayala", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "csayala@funcionpublica.gob.mx", usrextension = "55-63-26-99   Ext. 402", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1049, usrnombre = "Patricia", usrpaterno = "Ramos", usrmaterno = "Cortes", usrpuesto = "ENLACE", usrcontraseña = "pramos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "pramos@funcionpublica.gob.mx", usrextension = "50-25-37-47", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1050, usrnombre = "Luis Fernando", usrpaterno = "Morales", usrmaterno = "Núñez", usrpuesto = "DIRECTOR GENERAL DE ADMINISTRACIÓN DEL PATRIMONIO INMOBILIARIO FEDERAL", usrcontraseña = "lfmorales", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lfmorales@funcionpublica.gob.mx", usrextension = "55-54-36-78 Ext. 101", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1051, usrnombre = "Gabriela", usrpaterno = "Guerrero", usrmaterno = "Aguilar", usrpuesto = "SUBDIRECTORA DE EVALUACION Y USOS ALTERNOS DE INMUEBLES", usrcontraseña = "gguerrero", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gguerrero@funcionpublica.gob.mx", usrextension = "55-63-26-99   Ext. 214", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1052, usrnombre = "Carlos Alberto", usrpaterno = "De la fuente", usrmaterno = "Herrera", usrpuesto = "DIRECTOR GENERAL DE AVALÚOS Y OBRAS", usrcontraseña = "cdelafuente", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cdelafuente@funcionpublica.gob.mx", usrextension = "55-63-26-99 EXT. 104", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1053, usrnombre = "Daniel", usrpaterno = "Huerta", usrmaterno = "Conde", usrpuesto = "DIRECTOR DE SEGUIMIENTO Y GESTIÓN DE AVALÚOS", usrcontraseña = "dhuerta", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "dhuerta@funcionpublica.gob.mx", usrextension = "55-63-26-99 EXT. 104", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1054, usrnombre = "Fernando", usrpaterno = "Mas", usrmaterno = "Roa", usrpuesto = "DIRECTOR DE PROGRAMACIÓN Y CONTRATACIÓN DE OBRAS", usrcontraseña = "fmas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "fmas@funcionpublica.gob.mx", usrextension = "55-63-26-99 EXT.  106", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1055, usrnombre = "Gerardo", usrpaterno = "Gutiérrez", usrmaterno = "Hernández", usrpuesto = "SUBDIRECTOR DE RESNTAS Y CONTRATACIÓN DE PERITOS", usrcontraseña = "ggutierrez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ggutierrez@funcionpublica.gob.mx", usrextension = "55-63-26-99 EXT.  484", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1056, usrnombre = "Luis Giovanni", usrpaterno = "Santos", usrmaterno = "González", usrpuesto = "COORDINADOR DE DESARROLLO INSTITUCIONAL", usrcontraseña = "lgsantos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lgsantos@funcionpublica.gob.mx", usrextension = "55-63-27-98", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1057, usrnombre = "Marco Antonio", usrpaterno = "Brito", usrmaterno = "Vidales", usrpuesto = "SUBDIRECTOR DE MODERNIZACIÓN Y GESTIÓN ESTRATÉGICA", usrcontraseña = "mbrito", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mbrito@funcionpublica.gob.mx", usrextension = "55-63-26-99 EXT. 442", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1058, usrnombre = "Xavier", usrpaterno = "Castillo", usrmaterno = "Amezcua", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "xavier.castilloa", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "xavier.castilloa@sepdf.gob.mx", usrextension = "36-01-84-00 EXT. 48539 Y 48505", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1059, usrnombre = "Román Pablo", usrpaterno = "Rangel", usrmaterno = "Pinedo", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "rrangel", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rrangel@puertoaltamira.com.mx", usrextension = "01-833-260-60-60 Ext. 70003", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1060, usrnombre = "Marco Antonio", usrpaterno = "Sosa", usrmaterno = "Rodríguez", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oic@puertocoatzacoalcos.com.mx", usrextension = "DIRECTO 01-921-211-02-77 01-921-211-02-70 Ext. 70203", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1061, usrnombre = "Amparito del Camen", usrpaterno = "Escalante", usrmaterno = "Escalante", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "scontraloria", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "scontraloria@puertodosbocas.com.mx", usrextension = "01-933-333-5160 Ext. 70403 01-933-333-51-76", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1062, usrnombre = "Enrique", usrpaterno = "Capistran", usrmaterno = "Vázquez", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oic@puertoensenada.com.mx", usrextension = "01-646-178-03-08", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1063, usrnombre = "Hilda Cecilia", usrpaterno = "Peñuñuri", usrmaterno = "Castro", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oic@puertodeguaymas.com.mx", usrextension = "01-622-225-22-50  EXT. 70803", usrtitulo = "Mtra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1064, usrnombre = "Rafael Anotino", usrpaterno = "Gil", usrmaterno = "Ortiz", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "api-lazaroc_toic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "api-lazaroc_toic@funcionpublica.gob.mx", usrextension = "01-753-533-07-46", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1065, usrnombre = "Margarito", usrpaterno = "Ramírez", usrmaterno = "Cortés", usrpuesto = "TITUTAL DEL ÁREA DE AUDITORÍA INTERNA", usrcontraseña = "mramirezc", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mramirezc@puertomanzanillo.com.mx", usrextension = "01-314-331-14-00 Ext. 71373", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1066, usrnombre = "Adán Ernesto", usrpaterno = "Flores", usrmaterno = "Gómez", usrpuesto = "Auditor", usrcontraseña = "tauditoria", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "tauditoria@puertomazatlan.com.mx", usrextension = "DIRECTO 01-669- 915-60-98  01-669-982-36-11 Ext. 71616", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1067, usrnombre = "Adriana Guadalupe", usrpaterno = "García", usrmaterno = "Paredes", usrpuesto = "Auditor", usrcontraseña = "auditquejas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "auditquejas@puertosyucatan.com", usrextension = "01-969-934-32-50 Ext. 71814", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1068, usrnombre = "José Mario", usrpaterno = "Ramírez", usrmaterno = "Vázquez", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oic@puertochiapas.com.mx", usrextension = "01-962-628-68-41 al 43 Ext. 71903", usrtitulo = "L.A.E.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1069, usrnombre = "José Ramón", usrpaterno = "Flores", usrmaterno = "García", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oic@puertodevallarta.com.mx", usrextension = "01-322-22-410-00 Ext. 73203", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1070, usrnombre = "Gustavo", usrpaterno = "Bautista", usrmaterno = "Rosas", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oic@puertosalinacruz.com.mx", usrextension = "01-971-717-30-73 01-971-717-30-70 Ext. 72215", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1071, usrnombre = "Juan Aurelio", usrpaterno = "Galván", usrmaterno = "Llanez", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "tainterna", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "tainterna@puertodetampico.com.mx", usrextension = "01-833-241-14-00    01-723-737-23-03", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1072, usrnombre = "Rodolfo", usrpaterno = "Peinado", usrmaterno = "Arvallo", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oic@puertotopolobampo.com.mx", usrextension = "01-668-816-39-70  Ext. 72503", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1073, usrnombre = "José Luis", usrpaterno = "Fernández", usrmaterno = "Ricaño", usrpuesto = "AUDITOR INTERNO", usrcontraseña = "auditor", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "auditor@puertotuxpan.com.mx", usrextension = "01-783-102-30-30  Ext. 72808", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1074, usrnombre = "Eduardo", usrpaterno = "De la torre", usrmaterno = "Berea", usrpuesto = "JEFE DE DEPARTAMENTO", usrcontraseña = "etorre", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "etorre@puertodeveracruz.com.mx", usrextension = "01-229-923-21-70  Ext. 73118", usrtitulo = "M.A.C.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1075, usrnombre = "Claudia Yisel", usrpaterno = "Madrid", usrmaterno = "Castañón", usrpuesto = "CONSULTOR", usrcontraseña = "cmadrid", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cmadrid@aicm.com.mx", usrextension = "24-82-24-00 Ext. 2539", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1076, usrnombre = "Juana", usrpaterno = "Romero", usrmaterno = "Sánchez", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "jromeros", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jromeros@asa.gob.mx", usrextension = "51-33-10-00   Ext. 2533", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1077, usrnombre = "Martín Fernando", usrpaterno = "Castillo", usrmaterno = "Linares", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "martin.castillo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "martin.castillo@aserca.gob.mx", usrextension = "38-71-73-00 Ext. 50282", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1078, usrnombre = "Hector", usrpaterno = "Villanueva", usrmaterno = "Farquet", usrpuesto = "PROFESIONAL EJECUTIVO", usrcontraseña = "villanueva.farquet", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "villanueva.farquet@aem.gob.mx", usrextension = "36-91-13-10 ext 85297", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1079, usrnombre = "Naomí", usrpaterno = "Zuazo", usrmaterno = "Ríos", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "nzuazo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "nzuazo@agroasemex.gob.mx", usrextension = "01-442-238-19-00  Ext.4603", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1080, usrnombre = "Martha", usrpaterno = "Carbajal", usrmaterno = "Torres", usrpuesto = "SUBGERENTE DE MEJORA DE LA GESTIÓN", usrcontraseña = "mcarbajal", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mcarbajal@bansefi.gob.mx", usrextension = "54-81-50-36", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1081, usrnombre = "Anfrés", usrpaterno = "Espinosa", usrmaterno = "Cruz", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "aespinos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "aespinos@bancomext.gob.mx", usrextension = "54-49-93-13", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1082, usrnombre = "Fernando", usrpaterno = "Domínguez", usrmaterno = "Díaz", usrpuesto = "GERENTE DE QUEJAS, ATENCIÓN CIUDADANA Y APOYO JURÍDICO", usrcontraseña = "fernando.dominguez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "fernando.dominguez@banobras.gob.mx", usrextension = "52-70-12-53 EXT. 3942", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1083, usrnombre = "Enrique Javier", usrpaterno = "Rodríguez", usrmaterno = "Vázquez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "quejasyresp", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "quejasyresp@banjercito.com.mx", usrextension = "56-26-05-00 Ext.2713 y 355", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1084, usrnombre = "Patricia", usrpaterno = "PEÑA", usrmaterno = "JAIME", usrpuesto = "GERENTE DE INCONFORMIDADES Y NORMATIVIDAD", usrcontraseña = "ppena", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ppena@capufe.gob.mx", usrextension = "52-00-20-00    Ext. 7452 - 01-777-329-21-00  Y 01-777-311-53-05", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1085, usrnombre = "Elva Jazmín", usrpaterno = "Fadul", usrmaterno = "Guillén", usrpuesto = "Jefe de departamento", usrcontraseña = "efadul", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "efadul@capufe.gob.mx", usrextension = "52-00-20-00    Ext. 7454", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1086, usrnombre = "José", usrpaterno = "Rangel", usrmaterno = "Sánchez", usrpuesto = "GERENTE DE CONTROL Y APOYO AL BUEN GOBIERNO", usrcontraseña = "jrangel", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jrangel@cmm.gob.mx", usrextension = "01-444-834-60-11 y 01-444-834-60-00 ext. 6011", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1087, usrnombre = "Carmen Lorena", usrpaterno = "Torres", usrmaterno = "Orozco", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "cltorres", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cltorres@estudioschurubusco.com", usrextension = "55-49-30-60  Ext. 4728", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1088, usrnombre = "Leonel Antonio", usrpaterno = "Vázquez", usrmaterno = "Briceño", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "lvazquez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lvazquez@ceti.mx", usrextension = "01-33-36-41-32-50  Ext. 206", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1089, usrnombre = "Veronica del Carmen", usrpaterno = "Urbieta", usrmaterno = "Nieto", usrpuesto = "AUDITOR", usrcontraseña = "veronica.urbieta", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "veronica.urbieta@cidesi.edu.mx", usrextension = "01-442-211-98-03", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1090, usrnombre = "José Isidro", usrpaterno = "Zavala", usrmaterno = "Cárdenas", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "titular_oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "titular_oic@cicy.mx", usrextension = "01 -99-99- 42-83-62 01-99-99-42-82-30 Ext. 103", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1091, usrnombre = "Magda Elvia", usrpaterno = "Zamora", usrmaterno = "Lozano", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA INTERNA Y TITULAR DEL ÁREA DE AUDITORIA PARA DESARROLLO Y MEJORA DE LA GESTIÓN", usrcontraseña = "mzamora", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mzamora@cicese.mx", usrextension = "01-646-175-05-10 Y 175-05-11", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1092, usrnombre = "Alejandro", usrpaterno = "Arriaga", usrmaterno = "Reynoso", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "alejandro.arriaga", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "alejandro.arriaga@infotec.mx", usrextension = "56-24-28-00 EXT. 1101", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1093, usrnombre = "Ricardo", usrpaterno = "Rascón", usrmaterno = "Jaime", usrpuesto = "TITULAR DEL ORGANO INTERNO DE CONTROL", usrcontraseña = "halcon", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "halcon@ciad.mx", usrextension = "01-662-289-24-00 Ext. 310", usrtitulo = "C.P.y L.D.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1094, usrnombre = "Martha Laura", usrpaterno = "Salinas", usrmaterno = "Flores", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "msalinas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "msalinas@centrogeo.org.mx", usrextension = "56-74-62-28  Ext. 13", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1095, usrnombre = "Anna Arlete", usrpaterno = "García", usrmaterno = "Aguirre", usrpuesto = "AUDITOR", usrcontraseña = "anna.garcia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "anna.garcia@cimat.mx", usrextension = "01-473-732-71-55 Ext. 4610", usrtitulo = "Mtra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1096, usrnombre = "Federico Ricardo", usrpaterno = "Zamora", usrmaterno = "Apam", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "federico.zamora", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "federico.zamora@cimav.edu.mx", usrextension = "01-614-439-11-00  Ext. 1185  01-614-439-11-85", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1097, usrnombre = "Silvia", usrpaterno = "Rocha", usrmaterno = "Hernández", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "silvia.rocha", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "silvia.rocha@ciqa.edu.mx", usrextension = "01-844-438-98-30 Ext. 1435 DIRECTO 01-844-438-94-66", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1098, usrnombre = "José Marco Antonio", usrpaterno = "Olmedo", usrmaterno = "Arcega", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "marco.olmedo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "marco.olmedo@ciatej.mx", usrextension = "01-33-33-45-52-00 Ext. 1116", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1099, usrnombre = "Deysy Lisset", usrpaterno = "Ortega", usrmaterno = "Calderón", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "dortega", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "dortega@cinvestav.mx", usrextension = "57-47-38-00 EXT. 6650", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1100, usrnombre = "Marycarmen", usrpaterno = "Ferrer", usrmaterno = "Zamudio", usrpuesto = "AUDITOR", usrcontraseña = "mferrer", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mferrer@cideteq.mx", usrextension = "01-422-101-55-00 Ext. 5521", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1101, usrnombre = "Elsa Gabriela", usrpaterno = "Tovar", usrmaterno = "Bravo", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "elsa.tovar", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "elsa.tovar@cide.edu", usrextension = "57-27-98-00 Ext. 6030", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1102, usrnombre = "Ricardo", usrpaterno = "López", usrmaterno = "Lerín", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "rlopez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rlopez@entermas.net", usrextension = "52-00-89-09 Ext. 2239 y 2233", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1103, usrnombre = "Jovita", usrpaterno = "Olachea", usrmaterno = "De la Toba", usrpuesto = "ASISTENTE DEL OIC", usrcontraseña = "jovita04", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jovita04@cibnor.mx", usrextension = "(01) 612-123-84-84 EXT. 3932", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1104, usrnombre = "Miguel", usrpaterno = "Lozano", usrmaterno = "Baraja", usrpuesto = "AUDITOR", usrcontraseña = "mlozano", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mlozano@cio.mx", usrextension = "01-477-441-42-32", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1105, usrnombre = "Daniel Oswaldo", usrpaterno = "Venegas", usrmaterno = "Franco", usrpuesto = "AUDITOR", usrcontraseña = "daniel.venegas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "daniel.venegas@ciesas.edu.mx", usrextension = "54-87-36-90 Ext. 1053", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1106, usrnombre = "Montserrat", usrpaterno = "Maldonado", usrmaterno = "Armengual", usrpuesto = "ASISTENTE DEL TOIC", usrcontraseña = "monserrat.maldonado", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "monserrat.maldonado@cfe.gob.mx", usrextension = "57-24-58-45  55-95-54-00 ext. 51090", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1107, usrnombre = "Lilian", usrpaterno = "Morales", usrmaterno = "Patiño", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "lmorales", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lmorales@cenagas.gob.mx", usrextension = "80-00-67-06", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1108, usrnombre = "Ignacio Martín", usrpaterno = "Watson", usrmaterno = "Hernández", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "iwatson", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "iwatson@cenam.mx", usrextension = "01-442-211-05-00 Ext. 3114", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1109, usrnombre = "Ernesto", usrpaterno = "Domínguez", usrmaterno = "Gómez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "erdominguez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "erdominguez@crae.gob.mx", usrextension = "DIRECTO 01-961-617-07-35  01-961- 617-07-00  Ext.1232 y 1235", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1110, usrnombre = "Martha Elena", usrpaterno = "Santana", usrmaterno = "Quintana", usrpuesto = "SUBJEFA DE DEPARTAMENTO", usrcontraseña = "martha.santana", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "martha.santana@cij.gob.mx", usrextension = "55-36-00-16  EXT. 149", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1111, usrnombre = "Rubén Omar", usrpaterno = "Jiménez", usrmaterno = "Olivares", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "rjimenez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rjimenez@ciatec.mx", usrextension = "01-477-710-00-11    Ext. 11300", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1112, usrnombre = "Alfredo", usrpaterno = "Hidalgo", usrmaterno = "Suárez", usrpuesto = "AUDITOR", usrcontraseña = "alfredo.hidalgo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "alfredo.hidalgo@ciateq.mx", usrextension = "01-442-211-26-00 EXT. 2320", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1113, usrnombre = "LIC. JOSÉ ARMANDO", usrpaterno = "GARCÍA", usrmaterno = "REYES", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "josearmando.garciar", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "josearmando.garciar@bachilleres.edu.mx", usrextension = "5624 4100 ext 4443,5624 4109", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1114, usrnombre = "LIC. MARÍA GUADALUPE", usrpaterno = "MENDOZA", usrmaterno = "VÁZQUEZ", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "mgmendoza", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mgmendoza@colpos.mx", usrextension = "DIR. 58-04-59-06  -58-04-59-00 EXT. 1018", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1115, usrnombre = "Juan Carlos", usrpaterno = "González", usrmaterno = "Arzate", usrpuesto = "JEFE DE PROYECTO", usrcontraseña = "jgarzate", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jgarzate@conalep.edu.mx", usrextension = "01-722-271-08-00   54-80-37-66 Ext. 2060", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1116, usrnombre = "José Luis", usrpaterno = "Baltazar", usrmaterno = "Peña", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "baltazarjl", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "baltazarjl@cofaa.ipn.mx", usrextension = "57-29-60-00,57-29-63-00 Ext. 65040 y 65105", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1117, usrnombre = "César", usrpaterno = "Franco", usrmaterno = "Rul", usrpuesto = "DIRECTOR DE ÁREA", usrcontraseña = "cesar.francor", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cesar.francor@pgr.gob.mx", usrextension = "53-46-00-00  Ext. 0642", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1118, usrnombre = "Miguel Angel", usrpaterno = "Pérez", usrmaterno = "MARTELL", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "miguel.perez07", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "miguel.perez07@cfe.gob.mx", usrextension = "52-29-44-00 EXT. 90519 Y 9520", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1119, usrnombre = "Marco Antonio Alejandro", usrpaterno = "Rosas", usrmaterno = "Cervantes", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "maarosas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "maarosas@cofepris.gob.mx", usrextension = "50-80-52-00  Ext. 1170", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1120, usrnombre = "Albina Francista", usrpaterno = "Morales", usrmaterno = "Rojas", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "amoralesr", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "amoralesr@cnbv.gob.mx", usrextension = "14-54-66-64", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1121, usrnombre = "Celida Isabel", usrpaterno = "Gámez", usrmaterno = "Ramos", usrpuesto = "SUBDIRECTOR AUDITOR", usrcontraseña = "cgamezr", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cgamezr@conapesca.gob.mx", usrextension = "01-669-915-69-00  EXT. 58903", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1122, usrnombre = "Pedro", usrpaterno = "López", usrmaterno = "Flores", usrpuesto = "SUBCORDINADOR DE QUEJA Y RESPONSABILIDADES", usrcontraseña = "plopez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "plopez@conamed.gob.mx", usrextension = "54-20-70-42", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1123, usrnombre = "Wildebaldo", usrpaterno = "Salinas", usrmaterno = "Paredes", usrpuesto = "COORDINADOR DE QUEJAS", usrcontraseña = "wildebaldo.salinas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "wildebaldo.salinas@conade.gob.mx", usrextension = "59-27-52-00 Ext.4300 y 4320", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1124, usrnombre = "Jesus Misael", usrpaterno = "Mejia", usrmaterno = "Terrón", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "jesus.mejia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jesus.mejia@cnh.gob.mx", usrextension = "14-54-85-00 Ext. 8547", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1125, usrnombre = "Félix Alberto", usrpaterno = "Torres", usrmaterno = "Berúmen", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "felix.torres", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "felix.torres@conaza.gob.mx", usrextension = "01-844-450-52-00 Ext. 7155", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1126, usrnombre = "Arturo", usrpaterno = "Torres", usrmaterno = "Sánchez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "arturo_torres", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "arturo_torres@conaliteg.gob.mx", usrextension = "54-81-04-00 ext. 6407", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1127, usrnombre = "Pablo Francisco", usrpaterno = "Ruiz", usrmaterno = "Ávila", usrpuesto = "COORDINADOR DE ANALISTAS ADMINISTRATIVOS", usrcontraseña = "pruiz", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "pruiz@conasami.gob.mx", usrextension = "59-98-38-00 Ext. 33830", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1128, usrnombre = "Denisse", usrpaterno = "Romero", usrmaterno = "Gallegos", usrpuesto = "CONSULTOR", usrcontraseña = "denisse.romero", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "denisse.romero@salud.gob.mx", usrextension = "50-90-36-00  EXT. 57910 55-19-66-23", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1129, usrnombre = "Adriana", usrpaterno = "Cardenas", usrmaterno = "Islas", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "adriana.cadenas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "adriana.cadenas@cnsns.gob.mx", usrextension = "50-95-32-00 Ext. 6519 50-95-65-19", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1130, usrnombre = "LIC. RIGOBERTO", usrpaterno = "GALEANA", usrmaterno = "RODRÍGUEZ", usrpuesto = "ABOGADO", usrcontraseña = "rgaleana", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rgaleana@cnsf.gob.mx", usrextension = "57-24-75-64", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1131, usrnombre = "Olga", usrpaterno = "García", usrmaterno = "Santiago", usrpuesto = "SUBDIRECTORA DE ÁREA DE AUDITORÍA", usrcontraseña = "ogarcia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ogarcia@conavi.gob.mx", usrextension = "91-38-99-91 EXT. 041", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1132, usrnombre = "María de Lourdes", usrpaterno = "Magallanes", usrmaterno = "González", usrpuesto = "CONSULTOR DEL ÁREA DE AUDITORIA PARA DESARROLLO Y MEJORA DE LA GESTIÓN", usrcontraseña = "lourdes.magallanes.ci", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lourdes.magallanes.ci@conagua.gob.mx", usrextension = "51-74-40-00 Ext. 2206", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1133, usrnombre = "Juan", usrpaterno = "Rodríguez", usrmaterno = "Díaz", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA INTERNA", usrcontraseña = "jrodrigu", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jrodrigu@consar.gob.mx", usrextension = "30-00-26-88", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1134, usrnombre = "Iván Guillermo", usrpaterno = "Vega", usrmaterno = "Navarro", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "ivega", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ivega@conafor.gob.mx", usrextension = "01-33-37-77-70-00 Ext. 1820", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1135, usrnombre = "José Misael", usrpaterno = "Bretón", usrmaterno = "Ramírez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "jbreton", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jbreton@cdi.gob.mx", usrextension = "91-83-21-12            91- 83 21 00 Ext. 7252", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1136, usrnombre = "Itzamaray", usrpaterno = "López", usrmaterno = "Jiménez", usrpuesto = "JEFE DE DEPARTAMENTO", usrcontraseña = "iljimenez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "iljimenez@condusef.gob.mx", usrextension = "54-48-70-00 EXT. 6277", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1137, usrnombre = "Elizabeth Aglaya", usrpaterno = "Soriano", usrmaterno = "López", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "elizabeth.soriano", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "elizabeth.soriano@corett.gob.mx", usrextension = "50-80-96-49", usrtitulo = "Mtra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1138, usrnombre = "Victor Manuel", usrpaterno = "Cornejo", usrmaterno = "Coria", usrpuesto = "COORDINADOR DE QUEJAS, DENUNCIAS Y RESPONSABILIDADES", usrcontraseña = "vmcornejo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "vmcornejo@sprofesionales.com.mx", usrextension = "52-78-29-60 EXT1223", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1139, usrnombre = "Francisco Javier", usrpaterno = "Tapia", usrmaterno = "Campos", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "ftapia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ftapia@cecut.gob.mx", usrextension = "01-664-687-96-11", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1140, usrnombre = "Carolina", usrpaterno = "Rolón", usrmaterno = "Martell", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "crolon", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "crolon@cjef.gob.mx", usrextension = "36-88-40-00 EXT. 4514", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1141, usrnombre = "Jorge Porfirio", usrpaterno = "Casanova", usrmaterno = "Ibañez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "jcasanova", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jcasanova@promotur.com.mx", usrextension = "5278-4002 Ext. 1910", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1142, usrnombre = "Graciela", usrpaterno = "Popoca", usrmaterno = "Casto", usrpuesto = "Gerente de responsabilidades", usrcontraseña = "gpopoca", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gpopoca@promotur.com.mx", usrextension = "5278-4002 Ext. 1911", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1143, usrnombre = "Arturo", usrpaterno = "Ramírez", usrmaterno = "Ramírez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "aramirezr", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "aramirezr@conacyt.mx", usrextension = "53-22-77-00 Ext. 4330", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1144, usrnombre = "Pedro", usrpaterno = "Reyes", usrmaterno = "Cervantes", usrpuesto = "SUBDIRECTOR DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "preyes", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "preyes@coneval.org.mx", usrextension = "54-81-72-00  EXT 70107", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1145, usrnombre = "Lucia Margarita", usrpaterno = "Rivero", usrmaterno = "Vera", usrpuesto = "Jefa de departamento", usrcontraseña = "lrivero", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lrivero@conafe.gob.mx", usrextension = "52-41-74-00 EXT. 7364", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1146, usrnombre = "Juana Vanessa", usrpaterno = "Velázquez", usrmaterno = "Salgado", usrpuesto = "Técnico superior", usrcontraseña = "lrivero", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "vvelzquez@conafe.gob.mx", usrextension = "52-41-74-00 EXT. 7308", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1147, usrnombre = "Ana Clara", usrpaterno = "Fragoso", usrmaterno = "Pereida", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "clara.fragoso", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "clara.fragoso@conadis.gob.mx", usrextension = "55-31-88-16", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1148, usrnombre = "María de Jésus", usrpaterno = "Portilla", usrmaterno = "Rosas", usrpuesto = "JEFA DE DEPARTAMENTO DE AUDITORIA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "maria.portilla", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "maria.portilla@conapred.org.mx", usrextension = "52-62-14-90  Ext. 5807", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1149, usrnombre = "Mónica Isabel", usrpaterno = "Casa", usrmaterno = "Juárez", usrpuesto = "SUBDIRECTORA DE RESPONSABILIDADES", usrcontraseña = "monicai.casas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "monicai.casas@prospera.gob.mx", usrextension = "54-82-07-00  EXT. 60643", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1150, usrnombre = "Brenda Guadalupe", usrpaterno = "Narvaéz", usrmaterno = "Arellano", usrpuesto = "ANALISTA DE APOYO AL BUEN GOBIERNO", usrcontraseña = "bnarvaez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bnarvaez@comimsa.com", usrextension = "01-844-411-32-00 Ext. 1133", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1151, usrnombre = "Andrea Lizbeth", usrpaterno = "Soto", usrmaterno = "Arreguín", usrpuesto = "SUBGERENTE TÉCNICO", usrcontraseña = "lsoto", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lsoto@diconsa.gob.mx", usrextension = "52-29-07-00   Ext. 65914", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1152, usrnombre = "Amado", usrpaterno = "Carillo", usrmaterno = "González", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "acarrillo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "acarrillo@educal.com.mx", usrextension = "53-54-40-00  Ext. 4029", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1153, usrnombre = "Armando José", usrpaterno = "Valladares", usrmaterno = "Santiago", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "colef_toic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "valladares@colef.mx", usrextension = "01-664-631-63-47 01-664-631-63-00 ext. 3430", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1154, usrnombre = "Carmen Santiago", usrpaterno = "Domínguez", usrmaterno = "Barrios", usrpuesto = "PERSONAL DE APOYO", usrcontraseña = "csantiago", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "csantiago@ecosur.mx", usrextension = "01-967-674-90-00 Ext. 1197", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1155, usrnombre = "Leticia", usrpaterno = "Méndez", usrmaterno = "Hurtado", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "colmich_toic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "lmendez@colmich.edu.mx", usrextension = "01-351-515-71-00 Ext.  1841  Y 1840", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1156, usrnombre = "Rocío", usrpaterno = "García", usrmaterno = "Vázquez", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "rgarcia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rgarcia@colsan.edu.mx", usrextension = "01-444-811-01-01        Ext. 8240", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1157, usrnombre = "Carmen Lorena", usrpaterno = "Torres", usrmaterno = "Orozco", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "cltorres", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cltorres@estudioschurubusco.com", usrextension = "55-49-30-60   Ext. 4728", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1158, usrnombre = "Efrén", usrpaterno = "Policarp", usrmaterno = "Carlos", usrpuesto = "SUBGERENTE DEL ÁREA DE AUDITORÍA PARA DESARROLLO DE MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "epolicarpo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "epolicarpo@essa.com.mx", usrextension = "01-615-157 51-00 EXT. 1425", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1159, usrnombre = "Oscar", usrpaterno = "García", usrmaterno = "Ugalde", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA INTERNA", usrcontraseña = "ogarcia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ogarcia@ferroistmo.com.mx", usrextension = "56-82-24-93", usrtitulo = "L.C.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1160, usrnombre = "Belem", usrpaterno = "Mondragón", usrmaterno = "Escobar", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "bmondragon", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bmondragon@fifomi.gob.mx", usrextension = "52-49-95-00 Ext. 5400 5202", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1161, usrnombre = "Roberto", usrpaterno = "Rodríguez", usrmaterno = "Lavadore", usrpuesto = "COORINADOR DE QUEJAS", usrcontraseña = "rrodriguez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rrodriguez@fidena.gob.mx", usrextension = "52-41-62-00  Ext. 5001", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1162, usrnombre = "José Israel", usrpaterno = "Rivas", usrmaterno = "Barrera", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "jose.rivas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jose.rivas@conocer.gob.mx", usrextension = "22-82-02-00 Ext. 2022 Y 2037 22-82-02-37", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1163, usrnombre = "Miguel Ángel", usrpaterno = "Ponce", usrmaterno = "Morales", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "miguel.pmorales", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "miguel.pmorales@firco.sagarpa.gob.mx", usrextension = "38-71-83-00  Ext. 21096 y 20113", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1164, usrnombre = "Lilina Isabel", usrpaterno = "Arreguín", usrmaterno = "Mendieta", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "responsa", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "liliana.arreguin@fifonafe.gob.mx", usrextension = "54-82-32-64 54-82-32-00 ext. 174", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1165, usrnombre = "Esteban Alberto", usrpaterno = "Villafañe", usrmaterno = "Hernández", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "quejascontraloria", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "eavillafaneh@fonhapo.gob.mx", usrextension = "54-24-67-00 Ext. 66727", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1167, usrnombre = "María del Rosaio", usrpaterno = "Zapote", usrmaterno = "Rodríguez", usrpuesto = "JEFE DE DEPARTAMENTO DE RESPONSABILIDADES Y QUEJAS", usrcontraseña = "rosario.zapote", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rosario.zapote@promexico.gob.mx", usrextension = "54-47-70-00 Ext 1922", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1168, usrnombre = "Ernesto", usrpaterno = "Ocman", usrmaterno = "Cong", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "eocman", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "eocman@fnd.gob.mx", usrextension = "52-30-16-00 Ext. 2373 y 2374", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1169, usrnombre = "Gabriela", usrpaterno = "Zamora", usrmaterno = "Ocaña", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "gzamora", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gzamora@focir.gob.mx", usrextension = "50-81-09-50", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1170, usrnombre = "José Danel", usrpaterno = "Vázquez", usrmaterno = "Carmona", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "titular.responsabilidade", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "titular.responsabilidades@fondodeculturaeconomica.com", usrextension = "52-27-46-72 Ext. 4616", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1171, usrnombre = "Isral", usrpaterno = "Martínez", usrmaterno = "Lomelí", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "imartinez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "imartinez@fira.gob.mx", usrextension = "01-443-322-24-38", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1172, usrnombre = "Oscar Enrique", usrpaterno = "Sisniega", usrmaterno = "Muñoz", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "ci.quejas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oscar.sisniega@fovissste.gob.mx", usrextension = "53-22-04-97Ext. 85089, 85403", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1173, usrnombre = "Rosa Icela", usrpaterno = "Hernández", usrmaterno = "Mendoza", usrpuesto = "SUBGERENTE DE MEJORA DE LA GESTIÓN Y APOYO AL BUEN GOBIERNO", usrcontraseña = "rihernandez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rihernandez@fonatur.gob.mx", usrextension = "50-90-42-00 Ext. 4439", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1174, usrnombre = "Román Pablo", usrpaterno = "Manjarrez", usrmaterno = "Navarro", usrpuesto = "ASESOR", usrcontraseña = "rmanjarrez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rmanjarrez@fonart.gob.mx", usrextension = "50-93-60-00 Ext. 67615", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1175, usrnombre = "Ninfa Karina", usrpaterno = "Coutiño", usrmaterno = "Gómez", usrpuesto = "GERENTE ÁREA DE MEJORA DE LA GESTIÓN", usrcontraseña = "ninfa.coutino", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ninfa.coutino@gacm.mx", usrextension = "91-36-87-44", usrtitulo = "Mtra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1176, usrnombre = "Omar", usrpaterno = "Velázquez", usrmaterno = "Ortega", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "omar.velazquez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "omar.velazquez@salud.gob.mx", usrextension = "27-89-20-00 EXT.1761", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1177, usrnombre = "Alicia", usrpaterno = "Morales", usrmaterno = "Rivera", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA EL DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "controlgea", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "controlgea@yahoo.com.mx", usrextension = "40-00-30-00 Ext. 3138 Y 3354", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1178, usrnombre = "Elba Patricia", usrpaterno = "Luna", usrmaterno = "Pérez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "eluna", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "eluna@himfg.edu.mx", usrextension = "55-78-33-12  52-28-99-17  Ext. 2531", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1179, usrnombre = "María de los Angeles", usrpaterno = "Velázquez", usrmaterno = "Pe´rez", usrpuesto = "JEFE DE DEPARTAMENTO DE QUEJAS", usrcontraseña = "angeles.velazquez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "angeles.velazquez@salud.gob.mx", usrextension = "57-47-75-60 Ext. 7290", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1180, usrnombre = "Jorge Erasmo", usrpaterno = "Reyna", usrmaterno = "Acevedo", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "hraecv_toic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jorge.reyna@hraev.gob.mx", usrextension = "01-834-153-61-00 Ext. 1310", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1181, usrnombre = "Arturo Roberto", usrpaterno = "Calvo", usrmaterno = "Serrano", usrpuesto = "TITULAR DEL OIC", usrcontraseña = "hraei_toic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "arcalvo@hraei.gob.mx", usrextension = "59-72-98-00 ext. 1064", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1182, usrnombre = "Claudia Ysabel", usrpaterno = "Arjona", usrmaterno = "Contreras", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "claudia.arjona", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "claudia.arjona@salud.gob.mx", usrextension = "01-999-942-76-64", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1183, usrnombre = "Franciso", usrpaterno = "Carrera", usrmaterno = "Seano", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "hraeoax_toic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "fcarrera@hraeoaxaca.gob.mx", usrextension = "01-951-501-80-80 Ext.1227", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1184, usrnombre = "Jorge", usrpaterno = "Navarro", usrmaterno = "Alarcón", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "hraebaj_toic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jnavarroalarcon@hotmail.com", usrextension = "01-477-267-20-00Ext. 1509", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1185, usrnombre = "Rebeca", usrpaterno = "Pérez", usrmaterno = "Chacón", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "rebeca.perez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rebeca.perez@iepsa.gob.mx", usrextension = "59-70-26-48", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1186, usrnombre = "Agustina", usrpaterno = "Herrera", usrmaterno = "Espinoza", usrpuesto = "TITULAR DEL ORGANO INTERNO DE CONTROL", usrcontraseña = "agustina.herrera", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "agustina.herrera@inecol.mx", usrextension = "01-228-842-18-07 01-228-842-18-00 Ext. 1100", usrtitulo = "Dra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1187, usrnombre = "Carlos", usrpaterno = "Ladrón de Guevara", usrmaterno = "Rivero", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "cldeguevara", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cldeguevara@mora.edu.mx", usrextension = "55-34-27-83 EXT. 2230", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1188, usrnombre = "Cuauhtémoc", usrpaterno = "Popoca", usrmaterno = "Moctezuma", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "cuauhtemoc.popoca", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cuauhtemoc.popoca@iie.org.mx", usrextension = "01-777-362-38-41", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1189, usrnombre = "Alberto", usrpaterno = "Gijón", usrmaterno = "y Berrios", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "oic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oic@issfam.gob.mx", usrextension = "21-22-06-00 ext. 1690", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1191, usrnombre = "Raúl Rigoberto", usrpaterno = "Medina", usrmaterno = "Rodríguez", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "raul.medina", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "raul.medina@issste.gob.mx", usrextension = "54-81-68-00  Ext.26481", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1192, usrnombre = "Leonel Alejandro", usrpaterno = "Cruz", usrmaterno = "Labastida", usrpuesto = "JEFE DE DEPARTAMENTO", usrcontraseña = "raul.medina", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oiclcruz@issste.gob.mx", usrextension = "54-81-68-00 EXT. 26505", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1193, usrnombre = "José Francisco", usrpaterno = "Campos", usrmaterno = "García Zepeda", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS Y DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "jose.campos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jose.campos@fonacot.gob.mx", usrextension = "52-65-74-94 52-65-74-00  ext. 7494", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1194, usrnombre = "Verónica Beatriz", usrpaterno = "Vargas", usrmaterno = "López", usrpuesto = "COORDINADOR TÉCNICO ADMINISTRATIVO DE ALTA RESPONSABILIDAD", usrcontraseña = "veronica.vargas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "veronica.vargas@fonacot.gob.mx", usrextension = "52-65-74-49", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1195, usrnombre = "Carlos Iván", usrpaterno = "Solano", usrmaterno = "Becerril", usrpuesto = "JEFE DE DEPARTAMENTO", usrcontraseña = "carlos.solano", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "carlos.solano@imcine.gob.mx", usrextension = "65-52-05-16", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1196, usrnombre = "Ian", usrpaterno = "Nova", usrmaterno = "Ramirez", usrpuesto = "SUBDIRECTOR DE QUEJAS Y DENUNCIAS", usrcontraseña = "inova", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "inova@imjuventud.gob.mx", usrextension = "15-00-1300 EXT. 1391", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1197, usrnombre = "Juan José", usrpaterno = "Falomir", usrmaterno = "Orta", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "juan.falomir", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "juan.falomir@impi.gob.mx", usrextension = "56-24-04-12", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1198, usrnombre = "Juan Pablo", usrpaterno = "Téllez", usrmaterno = "Márquez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "pablo.tellez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "pablo.tellez@imer.com.mx", usrextension = "56-28-17-00                            Ext. 2530", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1199, usrnombre = "Romeo", usrpaterno = "Quintana", usrmaterno = "Sánchez", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "rquintana", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rquintana@tlaloc.imta.mx", usrextension = "01-777-329-36-00 EXT. 390", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1200, usrnombre = "Fracisco Javier", usrpaterno = "Acosta", usrmaterno = "Molina", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "facosta", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "facosta@imp.mx", usrextension = "91-75-80-84", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1201, usrnombre = "Fernando", usrpaterno = "Andrade", usrmaterno = "López", usrpuesto = "COORDINADOR DE VINCULACIÓN OPERATIVA", usrcontraseña = "fernando.andradel", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "fernando.andradel@imss.gob.mx", usrextension = "52-38-27-00 ext. 16615 y 16618 soriano  16523", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1202, usrnombre = "Roberto Arón", usrpaterno = "Sánchez", usrmaterno = "Salazar", usrpuesto = "JEFE DE DEPARTAMENTO", usrcontraseña = "roberto_sanchez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "roberto_sanchez@inah.gob.mx", usrextension = "40-40-46-24 Ext. 415921", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1203, usrnombre = "Armando", usrpaterno = "Soto", usrmaterno = "Tecualtl", usrpuesto = "AUDITOR", usrcontraseña = "sotoa", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "sotoa@inaoep.mx", usrextension = "01-222-266-31-00Ext. 2117", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1204, usrnombre = "Mónica", usrpaterno = "Marín", usrmaterno = "Zepeda", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "mmarin", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mmarin@inba.gob.mx", usrextension = "15-55-19-20   EXT. 6432", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1205, usrnombre = "María Cristina", usrpaterno = "Garduño", usrmaterno = "Bastida", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "cgarduno", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cgarduno@incan.edu.mx", usrextension = "56-28-04-00 Ext. 25001", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1206, usrnombre = "Jose Alberto", usrpaterno = "Monroy", usrmaterno = "Gómez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "alberto.monroy", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "alberto.monroy@cardiologia.org.mx", usrextension = "55-73-29-11  Ext.  1114   Y 1140", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1207, usrnombre = "Cecilia", usrpaterno = "González", usrmaterno = "Flores", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "cecilia.gonzalezf", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cecilia.gonzalezf@incmnsz.mx", usrextension = "55-73-20-78  54-87-09-00 EXT. 3143", usrtitulo = "M.A.O", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1208, usrnombre = "Sandra María de la Luz", usrpaterno = "Martínez", usrmaterno = "Millán", usrpuesto = "JEFE DE DEPARTAMENTO DE RESPONSABILIDADES", usrcontraseña = "sandra.martinez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "sandra.martinez@inacipe.gob.mx", usrextension = "54-87-15-00Ext.1688", usrtitulo = "Mtra.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1209, usrnombre = "Mayra Susana", usrpaterno = "Lobera", usrmaterno = "Caporal", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "mslobera", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mslobera@iner.gob.mx", usrextension = "54-87-17-00   Ext. 5193", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1210, usrnombre = "Marcos Abraham", usrpaterno = "Martínez", usrmaterno = "Sponholtz", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "martinez.marcos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "martinez.marcos@inifap.gob.mx", usrextension = "38-71-87-76 38-71-87-00 ext. 58896", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1211, usrnombre = "Alejandro", usrpaterno = "Chávez", usrmaterno = "Pérez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "alejandro.chavez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "alejandro.chavez@inin.gob.mx", usrextension = "53-29-72-00 Ext. 11403", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1212, usrnombre = "Ricardo", usrpaterno = "Jaime", usrmaterno = "Villa", usrpuesto = "SUBDIRECTOR DE TRANSPARENCIA", usrcontraseña = "rjaimev", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rjaimev@inaes.gob.mx", usrextension = "26-36-41-00 Ext. 4149", usrtitulo = "L.C.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1213, usrnombre = "Patricia Belem", usrpaterno = "Cercantes", usrmaterno = "Arana", usrpuesto = "SUPERVISOR ESPECIALIZADO EN CONSTRUCCIÓN", usrcontraseña = "bcervantes", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "bcervantes@inifed.gob.mx", usrextension = "54-80-47-00 EXT. 1348", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1214, usrnombre = "José Luis", usrpaterno = "García", usrmaterno = "Espinoza", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "jlgarcia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jlgarcia@inmujeres.gob.mx", usrextension = "53-22-60-51", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1215, usrnombre = "Eduardo", usrpaterno = "Córdoba", usrmaterno = "Zamudio", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "e.cordova", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "e.cordova@inapam.gob.mx", usrextension = "55-24-07-51", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1216, usrnombre = "Karla Patricia", usrpaterno = "Lastra", usrmaterno = "González", usrpuesto = "JEFE DE DEPARTAMENTO DE RESPONSABILIDADES", usrcontraseña = "karla.lastra", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "karla.lastra@inali.gob.mx", usrextension = "50-04-21-00 Ext. 109", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1217, usrnombre = "Olga Araceli", usrpaterno = "Labastida", usrmaterno = "Garcia", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "olabastida", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "olabastida@inmegen.gob.mx", usrextension = "53-50-19-38", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1218, usrnombre = "César Abdeel", usrpaterno = "Abitia", usrmaterno = "Collazo", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE GESTIÓN PÚBLICA", usrcontraseña = "cabitia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cabitia@inami.gob.mx", usrextension = "53-87-24-00 Ext. 18012", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1219, usrnombre = "Erika Alicia", usrpaterno = "Aguilera", usrmaterno = "Hernández", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "eaguilera", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "eaguilera@innn.edu.mx", usrextension = "56-06-38-22 Ext. 3090 Y  54-24-70-88", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1220, usrnombre = "Abel", usrpaterno = "Palma", usrmaterno = "Aparicio", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "responsa.inp", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "responsa.inp@gmail.com", usrextension = "10-84-09-00 Ext. 1107 y 1239", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1221, usrnombre = "Rocío", usrpaterno = "Pizaco", usrmaterno = "Palomino", usrpuesto = "SOPORTE ADMINISTRATIVO C", usrcontraseña = "rocio.palomino", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rocio.palomino@inper.mx", usrextension = "55-20-99-00 Ext. 479", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1222, usrnombre = "Patricia", usrpaterno = "Carranza", usrmaterno = "Armijo", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "oicrleyt", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "oicrleyt@imp.edu.mx", usrextension = "41-60-50-31", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1223, usrnombre = "César", usrpaterno = "Mendoza", usrmaterno = "Hernández", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y QUEJAS", usrcontraseña = "cmendoz", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cmendoza@inr.gob.mx", usrextension = "59-99-10-00 Ext. 18364", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1224, usrnombre = "Rafael", usrpaterno = "Reséndiz", usrmaterno = "Sánchez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "rafael.resendiz", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rafael.resendiz@insp.mx", usrextension = "01-777-101-29-06 01-777-329-30-00 EXT. 1961", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1225, usrnombre = "Filiberto", usrpaterno = "Sarmiento", usrmaterno = "García", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "filiberto.sarmiento", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "filiberto.sarmiento@inca.gob.mx", usrextension = "38-71-10-00 Ext. 46023 y 46024", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1226, usrnombre = "Rubén", usrpaterno = "Gómez", usrmaterno = "Montes de Oca", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "rgomez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rgomez@inea.gob.mx", usrextension = "52-41-28-4352-41-27-00 Ext. 22648", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1227, usrnombre = "Guillermo", usrpaterno = "González", usrmaterno = "Luna", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "ggonzalez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ggonzalez@ipab.org.mx", usrextension = "52-09-55-00 Ext.6514", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1228, usrnombre = "Claudia Mirla", usrpaterno = "Padilla", usrmaterno = "Cruz", usrpuesto = "TITULAR DEL ÁREA DE AUDITORIA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "cpadillac", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cpadillac@ipn.mx", usrextension = "57-29-60-00  Ext. 51782 y 51787", usrtitulo = "I.S.C.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1229, usrnombre = "Mayra Ave Margarita", usrpaterno = "Medina", usrmaterno = "Puente", usrpuesto = "AUDITORA", usrcontraseña = "mayra.medina", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mayra.medina@ipicyt.edu.mx", usrextension = "01-444-834-20-00     EXT. 6224", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1230, usrnombre = "Mario", usrpaterno = "Muñoz", usrmaterno = "Ortiz", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "mmunozo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mmunozo@birmex.gob.mx", usrextension = "54-22-28-40 Ext. 2686", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1231, usrnombre = "Jacobo", usrpaterno = "Mischine", usrmaterno = "Bass", usrpuesto = "TITULAR DEL OIC", usrcontraseña = "jmischneb", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jmischneb@liconsa.gob.mx", usrextension = "5237-9100 ext. 62005", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1232, usrnombre = "Ribogerto", usrpaterno = "Galeana", usrmaterno = "Rodríguez", usrpuesto = "GERENTE E ASESORIA JURIDICA", usrcontraseña = "rgaleana", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rgaleana@lotenal.gob.mx", usrextension = "51-40-70-00 Ext. 41-60", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1233, usrnombre = "Angélica", usrpaterno = "González", usrmaterno = "Valencia", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "agonzalezv", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "agonzalezv@nafin.gob.mx", usrextension = "53-25-62-04 53-25-62-05", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1234, usrnombre = "Ana Lilia", usrpaterno = "Ojeda", usrmaterno = "Miranda", usrpuesto = "JEFA DE DEPARTAMENTO", usrcontraseña = "liojeda", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "liojeda@notimex.com.mx", usrextension = "52-65-25-60 EXT. 2217", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1235, usrnombre = "Marina", usrpaterno = "Reyes", usrmaterno = "Guzmán", usrpuesto = "TITULAR DEL ÁREA DE AUDITORIA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "marina.reyes", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "marina.reyes@cns.gob.mx", usrextension = "51-28-41-00 ext. 18452", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1236, usrnombre = "Erik", usrpaterno = "Marcin", usrmaterno = "Lara", usrpuesto = "SUPERVISOR DE OBRA", usrcontraseña = "emarcin", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "emarcin@ipn.mx", usrextension = "57-29-60-00 EXT. 51703", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1237, usrnombre = "Alejandro", usrpaterno = "Pérez", usrmaterno = "Partida", usrpuesto = "RESPONSABLE DELA UNIDAD DE VINCULACIÓN", usrcontraseña = "transparencia.ur", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "transparencia.ur@pemex.com", usrextension = "1944-2500 Extensión 36626#", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1238, usrnombre = "Ricardo Gabriel", usrpaterno = "López", usrmaterno = "Ruiz", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "ricardo.lopezr", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ricardo.lopezr@cns.gob.mx", usrextension = "55-36-82-43 y 55-46-64-17", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1239, usrnombre = "Pablo", usrpaterno = "Granados", usrmaterno = "Reyes", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "pablo.granados", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "pablo.granados@presidencia.gob.mx", usrextension = "50-93-48-00 Ext. 4356", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1240, usrnombre = "María Citlalli", usrpaterno = "Ceballos", usrmaterno = "Hernández", usrpuesto = "JEFE DE DEPARTAMENTO", usrcontraseña = "mcceballos", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mcceballos@pa.gob.mx", usrextension = "15-00-39-00 EXT. 4929", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1241, usrnombre = "Miguel Angel", usrpaterno = "Trejo", usrmaterno = "Toral", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "matrejot", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "matrejot@profeco.gob.mx", usrextension = "56-25-68-83", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1243, usrnombre = "Liliana", usrpaterno = "Limones", usrmaterno = "Maldonado", usrpuesto = "TITULAR DE LAS ÁREAS DE QUEJAS Y RESPONSABILIDADES", usrcontraseña = "liliana.limones", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "liliana.limones@pronabive.gob.mx", usrextension = "36-18-04.-22 Ext. 263", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1244, usrnombre = "Georgina", usrpaterno = "Ramírez", usrmaterno = "Gómez", usrpuesto = "TITULAR DEL ÁREA DE AUITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "georgina.ramirez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "georgina.ramirez@pronosticos.gob.mx", usrextension = "54-82-00-00 Ext. 20061", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1245, usrnombre = "Martín Antonio", usrpaterno = "Martínez", usrmaterno = "Oliva", usrpuesto = "TITULAR DE LAS ÁREAS DE RESPONSABILIDADES", usrcontraseña = "mmoliva", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mmoliva@ran.gob.mx", usrextension = "50-62-14-00 Ext. 3135", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1246, usrnombre = "Marco Antonio", usrpaterno = "Flores", usrmaterno = "Camacho", usrpuesto = "DIRECTOR DE FORTALECIMIENTO INTERNO", usrcontraseña = "marco.fcamacho", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "marco.fcamacho@sagarpa.gob.mx", usrextension = "38-71-83-00 Ext. 21904", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1247, usrnombre = "Ana María Susana", usrpaterno = "Pérez", usrmaterno = "Jiménez", usrpuesto = "DIRECTORA DE COMPETITIVIDAD Y CALIDAD", usrcontraseña = "apjimene", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "apjimene@sct.gob.mx", usrextension = "57-23-93-00 Ext. 12023", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1248, usrnombre = "Luis Fernando", usrpaterno = "Diaz", usrmaterno = "Villareal", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "ldiazv", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ldiazv@conaculta.gob.mx", usrextension = "41-55-02-00 EXT. 9439", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1249, usrnombre = "Ana Delia", usrpaterno = "Adame", usrmaterno = "García", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "ana.adame", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ana.adame@sedatu.gob.mx", usrextension = "6820-9700 EXT. 13025", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1250, usrnombre = "Germán Emmanuel", usrpaterno = "Salinas", usrmaterno = "Valdés", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "german.salinas", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "german.salinas@sedesol.gob.mx", usrextension = "53-28-50-00 EXT .20404", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1251, usrnombre = "ELISA ESTELA", usrpaterno = "Almaguer", usrmaterno = "Suastegui", usrpuesto = "JEFE DE DEPARTEMENTO DE SEGUIMIENTO", usrcontraseña = "elisa.almaguer", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "elisa.almaguer@sedesol.gob.mx", usrextension = "53-28-50-00 Ext.51419", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1252, usrnombre = "Adriana Guadalupe", usrpaterno = "Ramirez", usrmaterno = "Reyes", usrpuesto = "COORDINADOR PROFESIONAL DICTAMINADOR", usrcontraseña = "adriana.ramirez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "adriana.ramirez@sedesol.gob.mx", usrextension = "53-28-50-00 Ext. 51457", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1253, usrnombre = "Julio", usrpaterno = "Espinosa", usrmaterno = "Torres", usrpuesto = "DIRECTOR DE QUEJAS Y DENUNCIAS", usrcontraseña = "julio.espinosat", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "julio.espinosat@economia.gob.mx", usrextension = "56-29-95-00 Ext. 21203", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1254, usrnombre = "Carolina", usrpaterno = "Delgadillo", usrmaterno = "Rojo", usrpuesto = "SUBDIRECTORA DEL ÁREA DE QUEJAS", usrcontraseña = "carolina.delgadillo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "carolina.delgadillo@economia.gob.mx", usrextension = "56-29-95-00 Ext. 21247", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1255, usrnombre = "Miguel Ángel", usrpaterno = "Ballesteros", usrmaterno = "Pérez", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "miguel.ballesteros", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "miguel.ballesteros@nube.sep.gob.mx", usrextension = "(55) 36-01-86-50  Ext.66141 3601-8667", usrtitulo = "Ing.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1256, usrnombre = "Jorge", usrpaterno = "García", usrmaterno = "León", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "jgarcia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jgarcia@energia.gob.mx", usrextension = "50-00-60-00 Ext. 1044", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1257, usrnombre = "Gabriela", usrpaterno = "Nacif", usrmaterno = "González", usrpuesto = "SUBDIRECTOR DE RESPOSABILIDADES", usrcontraseña = "gnacif", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gnacif@segob.gob.mx", usrextension = "51-28-00-00 Ext- 31347", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1258, usrnombre = "Rosaura", usrpaterno = "García", usrmaterno = "Palmeros", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "rosaura_garcia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rosaura_garcia@hacienda.gob.mx", usrextension = "36-88-97-35", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1259, usrnombre = "José Luis", usrpaterno = "Cruz", usrmaterno = "López", usrpuesto = "SUBTENIENTE OFICINISTA", usrcontraseña = "sedena_toic", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "sedena_toic@funcionpublica.gob.mx", usrextension = "21-22-88-00 Ext. 3042 y 3823", usrtitulo = "", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1260, usrnombre = "Alejandro", usrpaterno = "Morales", usrmaterno = "Juárez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES  Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "alejandro.morales", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "alejandro.morales@semarnat.gob.mx", usrextension = "54-90-21-84 54-90-21-00 EXT. 22184", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1261, usrnombre = "Juan José", usrpaterno = "Venancio", usrmaterno = "Domínguez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "jvenancio", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jvenancio@sre.gob.mx", usrextension = "36-86-51-00 EXT. 5966 DIR. 36-86-59-66", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1262, usrnombre = "Brenda", usrpaterno = "Valencia", usrmaterno = "Garnica", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA  PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "brenda.valencia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "brenda.valencia@salud.gob.mx", usrextension = "20-00-31-42 y 20-00-31-40 SARAI 2000-3152", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1263, usrnombre = "Claudio", usrpaterno = "García", usrmaterno = "Gómez", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS Y TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "cgarciag", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "cgarciag@sectur.gob.mx", usrextension = "30-03-16-00  Ext. 4069", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1264, usrnombre = "Carlos Alexis", usrpaterno = "Huerta", usrmaterno = "Gonzalez", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "carlos.huerta", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "carlos.huerta@stps.gob.mx", usrextension = "50-02-33-66", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1265, usrnombre = "Ernesto", usrpaterno = "Camarillo", usrmaterno = "Haro", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS Y TITULAR DEL ÁREA DE RESPONSABILIDADES", usrcontraseña = "ecamarillo", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ecamarillo@secretariadoejecutivo.gob.mx", usrextension = "50-01-36-50 Ext. 39272", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1266, usrnombre = "Ruth Consuelo", usrpaterno = "Navarro", usrmaterno = "Omaña", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESARROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "ruth.navarro", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "ruth.navarro@sat.gob.mx", usrextension = "58-02-04-56", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1267, usrnombre = "Aniceto", usrpaterno = "Del rio", usrmaterno = "Chico", usrpuesto = "TITULAR DEL ÓRGANO INTERNO DE CONTROL", usrcontraseña = "adelrio", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "adelrio@sae.gob.mx", usrextension = "17-19-16-00  ext. 1826", usrtitulo = "C.P.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1268, usrnombre = "Rosa del Carmen", usrpaterno = "Brizuela Amador", usrmaterno = "Pérez", usrpuesto = "TITULAR DEL ÁREA DE AUDITORÍA PARA DESRROLLO Y MEJORA DE LA GESTIÓN PÚBLICA", usrcontraseña = "rosa.brizuela", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "rosa.brizuela@cns.gob.mx", usrextension = "54-84-67-00 Ext. 68060", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1269, usrnombre = "Salvador", usrpaterno = "Frontana", usrmaterno = "Reyes", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "salvadorfrontana", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "salvadorfrontana@sgm.gob.mx", usrextension = "01-77-711-35-01    01-77-711-42-70 01-77-711-42-66 EXT. 1330", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1270, usrnombre = "Yone", usrpaterno = "Altamirano", usrmaterno = "Colín", usrpuesto = "SUBDIRECTORA DE QUEJAS Y DENUNCIAS", usrcontraseña = "yone.altamirano", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "yone.altamirano@senasica.gob.mx", usrextension = "38-71-83-85, 59-05-10-00Ext. 51648", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1271, usrnombre = "Genaro", usrpaterno = "Castro", usrmaterno = "Sánchez", usrpuesto = "TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "gecastro", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "gecastro@correosdemexico.gob.mx", usrextension = "53-40-33-00 Ext.25740", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1272, usrnombre = "Ana Lilia", usrpaterno = "Torres", usrmaterno = "Bonilla", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "quejasoicseneam", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "quejasoicseneam@sct.gob.mx", usrextension = "57-86-55-31", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1274, usrnombre = "Luis Enrique", usrpaterno = "Sarabia", usrmaterno = "Gallardo", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "luis.sarabia", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "luis.sarabia@dif.gob.mx", usrextension = "30-03-22-00 EXT. 7280", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1275, usrnombre = "María Lina", usrpaterno = "Vázquez", usrmaterno = "Rosas", usrpuesto = "SUBDIRECTOR DE QUEJAS", usrcontraseña = "mvazquez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "mvazquez@dif.gob.mx", usrextension = "30-03-22-00 Ext. 2522", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1276, usrnombre = "Sandra Anel", usrpaterno = "Villanueva", usrmaterno = "Leal", usrpuesto = "ANALISTA", usrcontraseña = "svillanueva", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "svillanueva@shf.gob.mx", usrextension = "52-63-45-00 EXT. 4483", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1277, usrnombre = "Nancy Yuliana", usrpaterno = "Romero", usrmaterno = "García", usrpuesto = "JEFE DE DEPARTAMENTO", usrcontraseña = "nancy.romero", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "nancy.romero@superissste.gob.mx", usrextension = "54-47-62-00 Ext. 97931", usrtitulo = "L.C.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1278, usrnombre = "Sonia", usrpaterno = "Martínez", usrmaterno = "González", usrpuesto = "ABOGADA", usrcontraseña = "sonia.martinezg", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "sonia.martinezg@tgm.com.mx", usrextension = "57-04-74-00  Ext. 235  57-04-74-25", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1279, usrnombre = "Miguel Ángel", usrpaterno = "Velasco", usrmaterno = "Velasco", usrpuesto = "JEFE DE DEPARTAMENTO DE RESPONSABILIDADES E INCONFORMIDADES", usrcontraseña = "miguel.velasco", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "miguel.velasco@telecomm.gob.mx", usrextension = "50-90-15-88", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1280, usrnombre = "José de Jesús", usrpaterno = "Alavez", usrmaterno = "Rosas", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "jose.alavez", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jose.alavez@canal22.org.mx", usrextension = "54-84-56-01", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1281, usrnombre = "Juan Carlos", usrpaterno = "Vega", usrmaterno = "García", usrpuesto = "TITULAR DEL ÁREA DE RESPONSABILIDADES Y TITULAR DEL ÁREA DE QUEJAS", usrcontraseña = "jcvega", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "jcvega@upn.mx", usrextension = "56-30-97-00  Ext. 1499", usrtitulo = "Mtro.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1282, usrnombre = "Liliana", usrpaterno = "Olvera", usrmaterno = "Cruz", usrpuesto = "DIRECTOR DE PROCEDIMIENTOS DE ACCESO A INFORMACION", usrcontraseña = "liolvera", usrfecbaja = new DateTime(2017, 07, 29) , usrcorreo = "liolvera@funcionpublica.gob.mx", usrextension = "1080, 4245", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 11), usractivo = new DateTime()  });

            // USUARIOS DEL AREA DE TRASNPARENCIA..
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1283, usrnombre = "Erika Anilu", usrpaterno = "Ortiz", usrmaterno = "García", usrpuesto = "DIRECTORA DE TRANSPARENCIA Y ASESORÍA", usrcontraseña = "eaortiz", usrfecbaja = new DateTime(), usrcorreo = "eaortiz@funcionpublica.gob.mx", usrextension = "Sin Extensión", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 27), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1284, usrnombre = "Alejandra", usrpaterno = "Perez", usrmaterno = "Aguilar", usrpuesto = "SUBDIRECTORA DE PROCEDIMIENTOS Y RECURSOS DE ACCESO A INFORMACION", usrcontraseña = "apereza", usrfecbaja = new DateTime(), usrcorreo = "apereza@funcionpublica.gob.mx", usrextension = "Sin Extensión", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 27), usractivo = new DateTime()  });
            lstDatos.Add(new SIT_ADM_USUARIO { usrclave = 1285, usrnombre = "Luz María", usrpaterno = "Martínez", usrmaterno = "Torres", usrpuesto = "JEFE(A) DE DEPARTAMENTO DE TRATAMIENTO DE DATOS PERSONALES", usrcontraseña = "liolvera", usrfecbaja = new DateTime(), usrcorreo = "lmmartinez@funcionpublica.gob.mx", usrextension = "Sin Extensión", usrtitulo = "Lic.", usrintentos = 0, usrbloquearfin = new DateTime(), usrdesignacion = " ", usrauxcorreo = " ", usrfecmod = new DateTime(2017, 07, 27), usractivo = new DateTime()  });



            for (int iIdxUsu = 0; iIdxUsu < lstDatos.Count; iIdxUsu++)
            {
                lstDatos[iIdxUsu].usrcontraseña = EncriptarUtil.ObtenerMD5Hash(lstDatos[iIdxUsu].usrcontraseña);
            }

            return lstDatos;
        }

        public List<SIT_ADM_USUARIOAREA> dmlSelectUsuarioArea( )
        {
            List<SIT_ADM_USUARIOAREA> lstDatos = new List<SIT_ADM_USUARIOAREA>();

            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 3, araclave = 300, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 92, araclave = 102, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 93, araclave = 102, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 101, araclave = 102, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 102, araclave = 302, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 103, araclave = 102, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 104, araclave = 102, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 105, araclave = 201, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 106, araclave = 502, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 107, araclave = 300, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 108, araclave = 500, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 109, araclave = 300, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 111, araclave = 300, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 112, araclave = 1, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 113, araclave = 102, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 114, araclave = 104, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 150, araclave = 102, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 200, araclave = 200, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 201, araclave = 200, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 240, araclave = 207, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 241, araclave = 207, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 246, araclave = 208, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 251, araclave = 206, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 261, araclave = 203, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 265, araclave = 209, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 266, araclave = 209, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 300, araclave = 300, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 310, araclave = 301, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 321, araclave = 308, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 350, araclave = 106, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 400, araclave = 400, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 402, araclave = 400, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 405, araclave = 207, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 407, araclave = 412, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 409, araclave = 412, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 410, araclave = 410, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 412, araclave = 409, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 416, araclave = 207, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 440, araclave = 404, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 500, araclave = 500, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 511, araclave = 503, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 520, araclave = 504, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 530, araclave = 501, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 630, araclave = 607, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 650, araclave = 603, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 660, araclave = 606, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 672, araclave = 891, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 673, araclave = 839, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 675, araclave = 875, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 684, araclave = 866, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 685, araclave = 885, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 686, araclave = 893, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 687, araclave = 894, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 688, araclave = 890, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 689, araclave = 875, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 692, araclave = 875, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 695, araclave = 755, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 696, araclave = 774, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 698, araclave = 917, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 699, araclave = 850, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 702, araclave = 918, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 703, araclave = 880, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 706, araclave = 708, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 708, araclave = 889, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 714, araclave = 907, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 716, araclave = 719, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 720, araclave = 779, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 722, araclave = 888, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 734, araclave = 784, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 735, araclave = 919, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 739, araclave = 836, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 742, araclave = 857, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 748, araclave = 875, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 749, araclave = 875, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 750, araclave = 761, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 753, araclave = 808, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 755, araclave = 875, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 810, araclave = 205, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 811, araclave = 205, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 815, araclave = 205, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 820, araclave = 105, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 826, araclave = 105, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 831, araclave = 108, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 840, araclave = 106, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 845, araclave = 106, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 855, araclave = 104, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 861, araclave = 501, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 865, araclave = 2, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 875, araclave = 103, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 911, araclave = 859, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 950, araclave = 109, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 999, araclave = 106, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1000, araclave = 502, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1001, araclave = 1, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1002, araclave = 102, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1003, araclave = 103, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1004, araclave = 104, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1005, araclave = 106, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1006, araclave = 108, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1007, araclave = 108, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1008, araclave = 305, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1009, araclave = 305, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1010, araclave = 305, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1011, araclave = 109, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1012, araclave = 500, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1014, araclave = 502, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1015, araclave = 503, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1016, araclave = 503, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1017, araclave = 505, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1018, araclave = 200, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1019, araclave = 209, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1020, araclave = 207, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1021, araclave = 206, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1022, araclave = 203, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1023, araclave = 208, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1024, araclave = 400, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1025, araclave = 404, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1026, araclave = 412, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1027, araclave = 412, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1028, araclave = 413, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1029, araclave = 413, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1030, araclave = 205, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1031, araclave = 409, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1032, araclave = 409, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1033, araclave = 300, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1034, araclave = 306, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1035, araclave = 306, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1037, araclave = 308, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1038, araclave = 202, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1039, araclave = 202, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1040, araclave = 202, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1041, araclave = 307, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1042, araclave = 307, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1043, araclave = 605, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1044, araclave = 605, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1045, araclave = 606, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1046, araclave = 606, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1047, araclave = 604, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1048, araclave = 604, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1049, araclave = 604, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1050, araclave = 603, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1051, araclave = 603, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1052, araclave = 602, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1053, araclave = 602, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1054, araclave = 602, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1055, araclave = 602, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1056, araclave = 602, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1057, araclave = 602, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1058, araclave = 701, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1059, araclave = 702, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1060, araclave = 703, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1061, araclave = 704, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1062, araclave = 705, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1063, araclave = 706, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1064, araclave = 707, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1065, araclave = 708, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1066, araclave = 709, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1067, araclave = 710, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1068, araclave = 711, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1069, araclave = 712, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1070, araclave = 713, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1071, araclave = 714, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1072, araclave = 715, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1073, araclave = 716, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1074, araclave = 717, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1075, araclave = 718, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1076, araclave = 719, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1077, araclave = 720, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1078, araclave = 721, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1079, araclave = 722, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1080, araclave = 723, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1081, araclave = 724, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1082, araclave = 725, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1083, araclave = 726, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1084, araclave = 727, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1085, araclave = 727, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1086, araclave = 728, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1087, araclave = 729, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1088, araclave = 730, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1089, araclave = 731, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1090, araclave = 732, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1091, araclave = 733, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1092, araclave = 734, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1093, araclave = 735, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1094, araclave = 736, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1095, araclave = 737, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1096, araclave = 738, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1097, araclave = 739, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1098, araclave = 740, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1099, araclave = 741, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1100, araclave = 742, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1101, araclave = 743, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1102, araclave = 744, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1103, araclave = 745, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1104, araclave = 746, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1105, araclave = 747, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1106, araclave = 748, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1107, araclave = 749, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1108, araclave = 750, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1109, araclave = 751, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1110, araclave = 752, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1111, araclave = 753, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1112, araclave = 754, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1113, araclave = 755, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1114, araclave = 756, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1115, araclave = 757, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1116, araclave = 758, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1117, araclave = 759, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1118, araclave = 760, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1119, araclave = 761, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1120, araclave = 762, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1121, araclave = 763, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1122, araclave = 764, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1123, araclave = 765, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1124, araclave = 766, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1125, araclave = 767, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1126, araclave = 768, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1127, araclave = 769, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1128, araclave = 770, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1129, araclave = 771, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1130, araclave = 772, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1131, araclave = 773, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1132, araclave = 774, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1133, araclave = 775, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1134, araclave = 776, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1135, araclave = 777, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1136, araclave = 778, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1137, araclave = 779, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1138, araclave = 781, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1139, araclave = 782, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1140, araclave = 783, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1141, araclave = 784, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1142, araclave = 784, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1143, araclave = 785, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1144, araclave = 786, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1145, araclave = 787, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1146, araclave = 787, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1147, araclave = 788, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1148, araclave = 789, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1149, araclave = 790, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1150, araclave = 791, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1151, araclave = 792, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1152, araclave = 793, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1153, araclave = 794, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1154, araclave = 795, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1155, araclave = 796, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1156, araclave = 797, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1157, araclave = 798, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1158, araclave = 799, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1159, araclave = 800, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1160, araclave = 801, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1161, araclave = 802, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1162, araclave = 803, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1163, araclave = 804, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1164, araclave = 805, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1165, araclave = 806, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1167, araclave = 808, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1168, araclave = 809, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1169, araclave = 810, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1170, araclave = 811, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1171, araclave = 812, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1172, araclave = 813, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1173, araclave = 814, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1174, araclave = 815, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1175, araclave = 816, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1176, araclave = 817, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1177, araclave = 818, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1178, araclave = 819, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1179, araclave = 820, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1180, araclave = 821, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1181, araclave = 822, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1182, araclave = 823, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1183, araclave = 824, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1184, araclave = 825, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1185, araclave = 826, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1186, araclave = 827, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1187, araclave = 828, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1188, araclave = 829, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1189, araclave = 830, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1191, araclave = 831, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1192, araclave = 831, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1193, araclave = 832, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1194, araclave = 832, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1195, araclave = 833, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1196, araclave = 834, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1197, araclave = 835, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1198, araclave = 836, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1199, araclave = 837, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1200, araclave = 838, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1201, araclave = 839, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1202, araclave = 840, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1203, araclave = 841, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1204, araclave = 842, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1205, araclave = 843, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1206, araclave = 844, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1207, araclave = 845, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1208, araclave = 846, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1209, araclave = 847, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1210, araclave = 848, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1211, araclave = 849, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1212, araclave = 850, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1213, araclave = 851, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1214, araclave = 852, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1215, araclave = 853, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1216, araclave = 854, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1217, araclave = 855, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1218, araclave = 856, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1219, araclave = 857, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1220, araclave = 858, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1221, araclave = 859, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1222, araclave = 860, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1223, araclave = 861, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1224, araclave = 862, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1225, araclave = 863, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1226, araclave = 864, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1227, araclave = 865, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1228, araclave = 866, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1229, araclave = 867, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1230, araclave = 868, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1231, araclave = 869, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1232, araclave = 870, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1233, araclave = 871, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1234, araclave = 872, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1235, araclave = 873, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1236, araclave = 874, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1237, araclave = 875, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1238, araclave = 876, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1239, araclave = 877, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1240, araclave = 878, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1241, araclave = 880, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1243, araclave = 882, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1244, araclave = 883, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1245, araclave = 884, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1246, araclave = 885, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1247, araclave = 886, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1248, araclave = 887, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1249, araclave = 888, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1250, araclave = 889, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1251, araclave = 889, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1252, araclave = 889, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1253, araclave = 890, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1254, araclave = 890, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1255, araclave = 891, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1256, araclave = 892, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1257, araclave = 893, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1258, araclave = 894, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1259, araclave = 895, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1260, araclave = 896, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1261, araclave = 897, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1262, araclave = 898, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1263, araclave = 899, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1264, araclave = 900, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1265, araclave = 901, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1266, araclave = 902, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1267, araclave = 903, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1268, araclave = 904, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1269, araclave = 905, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1270, araclave = 906, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1271, araclave = 907, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1272, araclave = 908, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1274, araclave = 910, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1275, araclave = 910, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1276, araclave = 911, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1277, araclave = 912, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1278, araclave = 913, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1279, araclave = 914, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1280, araclave = 915, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1281, araclave = 916, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1282, araclave = 102, uarorigen = 1 });

            // UNIDAD DE TRANSPARENCIA
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1283, araclave = 920, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1284, araclave = 921, uarorigen = 1 });
            lstDatos.Add(new SIT_ADM_USUARIOAREA { usrclave = 1285, araclave = 922, uarorigen = 1 });

            return lstDatos;
        }

        public List<SIT_ADM_USRPERFIL> dmlSelectUsuarioPerfil( )
        {

            List<SIT_ADM_USRPERFIL> lstDatos = new List<SIT_ADM_USRPERFIL>();

            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 3, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 92, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 93, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 101, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 102, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 103, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 104, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 105, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 106, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 107, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 108, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 109, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 111, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 112, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 113, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 114, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 150, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 200, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 201, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 240, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 241, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 246, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 251, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 261, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 265, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 266, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 300, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 310, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 321, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 350, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 400, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 402, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 405, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 407, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 409, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 410, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 412, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 416, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 440, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 500, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 511, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 520, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 530, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 630, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 650, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 660, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 672, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 673, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 675, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 684, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 685, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 686, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 687, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 688, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 689, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 692, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 695, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 696, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 698, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 699, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 702, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 703, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 706, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 708, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 714, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 716, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 720, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 722, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 734, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 735, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 739, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 742, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 748, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 749, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 750, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 753, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 755, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 810, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 811, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 815, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 820, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 826, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 831, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 840, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 845, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 855, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 861, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 865, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 875, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 911, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 950, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 999, perclave = 5 });

            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1001, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1002, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1003, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1004, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1005, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1006, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1007, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1008, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1009, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1010, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1011, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1012, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1014, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1015, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1016, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1017, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1018, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1019, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1020, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1021, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1022, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1023, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1024, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1025, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1026, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1027, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1028, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1029, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1030, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1031, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1032, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1033, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1034, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1035, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1037, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1038, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1039, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1040, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1041, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1042, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1043, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1044, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1045, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1046, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1047, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1048, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1049, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1050, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1051, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1052, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1053, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1054, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1055, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1056, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1057, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1058, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1059, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1060, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1061, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1062, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1063, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1064, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1065, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1066, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1067, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1068, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1069, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1070, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1071, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1072, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1073, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1074, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1075, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1076, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1077, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1078, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1079, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1080, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1081, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1082, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1083, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1084, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1085, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1086, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1087, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1088, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1089, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1090, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1091, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1092, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1093, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1094, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1095, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1096, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1097, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1098, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1099, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1100, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1101, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1102, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1103, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1104, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1105, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1106, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1107, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1108, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1109, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1110, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1111, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1112, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1113, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1114, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1115, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1116, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1117, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1118, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1119, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1120, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1121, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1122, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1123, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1124, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1125, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1126, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1127, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1128, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1129, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1130, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1131, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1132, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1133, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1134, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1135, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1136, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1137, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1138, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1139, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1140, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1141, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1142, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1143, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1144, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1145, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1146, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1147, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1148, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1149, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1150, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1151, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1152, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1153, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1154, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1155, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1156, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1157, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1158, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1159, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1160, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1161, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1162, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1163, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1164, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1165, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1167, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1168, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1169, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1170, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1171, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1172, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1173, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1174, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1175, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1176, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1177, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1178, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1179, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1180, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1181, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1182, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1183, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1184, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1185, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1186, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1187, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1188, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1189, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1191, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1192, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1193, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1194, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1195, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1196, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1197, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1198, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1199, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1200, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1201, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1202, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1203, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1204, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1205, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1206, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1207, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1208, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1209, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1210, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1211, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1212, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1213, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1214, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1215, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1216, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1217, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1218, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1219, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1220, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1221, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1222, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1223, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1224, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1225, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1226, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1227, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1228, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1229, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1230, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1231, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1232, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1233, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1234, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1235, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1236, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1237, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1238, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1239, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1240, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1241, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1243, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1244, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1245, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1246, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1247, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1248, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1249, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1250, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1251, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1252, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1253, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1254, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1255, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1256, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1257, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1258, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1259, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1260, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1261, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1262, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1263, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1264, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1265, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1266, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1267, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1268, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1269, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1270, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1271, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1272, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1274, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1275, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1276, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1277, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1278, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1279, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1280, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1281, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1282, perclave = 5 });

            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1000, perclave = 1 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1000, perclave = 2 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1000, perclave = 3 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1000, perclave = 4 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1000, perclave = 5 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1000, perclave = 6 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1000, perclave = 7 });

            // UNIDAD DE TRANSPARENCIA
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1283, perclave = 3 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1283, perclave = 4 });

            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1284, perclave = 4 });
            lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1285, perclave = 4 });

            /// lstDatos.Add(new SIT_ADM_USRPERFIL { usrclave = 1000, perclave = 6 });

            return lstDatos;
        }

        public List<SIT_ADM_MODULO> dmlSelectModulo( )
        {
            List<SIT_ADM_MODULO> lstDatos = new List<SIT_ADM_MODULO>();

            lstDatos.Add(new SIT_ADM_MODULO { modclave = 1, modpadre = 0, modconsecutivo = 1, moddescripcion = "Información", modcontrol = "Informacion", modmetodo = "Plazos", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 2, modpadre = 0, modconsecutivo = 2, moddescripcion = "Catálogos", modcontrol = "Catalogo", modmetodo = "Catalogo", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 3, modpadre = 0, modconsecutivo = 3, moddescripcion = "Administración", modcontrol = "Administracion", modmetodo = "Seleccion", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 4, modpadre = 0, modconsecutivo = 4, moddescripcion = "Importar", modcontrol = "Importar", modmetodo = "FormatoInfoMex", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 5, modpadre = 0, modconsecutivo = 5, moddescripcion = "Solicitudes", modcontrol = "Solicitud", modmetodo = "BandejaEntrada", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 6, modpadre = 0, modconsecutivo = 6, moddescripcion = "Seguridad", modcontrol = "Seguridad", modmetodo = "Perfil", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 7, modpadre = 0, modconsecutivo = 7, moddescripcion = "Tablero", modcontrol = "Tablero", modmetodo = "Estadistica", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 8, modpadre = 0, modconsecutivo = 8, moddescripcion = "Reportes", modcontrol = "Reporte", modmetodo = "Seleccion", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 9, modpadre = 0, modconsecutivo = 9, moddescripcion = "Recurso", modcontrol = "Recurso", modmetodo = "Revision", modfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_MODULO { modclave = 10, modpadre = 0, modconsecutivo = 10, moddescripcion = "Bitacora", modcontrol = "Seguridad", modmetodo = "Bitacora", modfecbaja = new DateTime() });
            return lstDatos;
        }

        public List<SIT_ADM_PERFILMOD> dmlSelectPerfilModulo( )
        {
            List<SIT_ADM_PERFILMOD> lstDatos = new List<SIT_ADM_PERFILMOD>();
            
            // 1, Sistema
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 1 }); //"Información"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 2 }); //"Catálogos"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 3 }); //"Administración"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 4 }); //"Importar"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 5 }); //"Solicitudes"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 6 }); //"Seguridad"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 7 }); //"Tablero"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 8 }); //"Reportes"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 9 }); // Recurso
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 1, modclave = 10 }); // Bitacora

            // 2, INAI
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 2, modclave = 4 }); //"Importar"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 2, modclave = 5 }); //"Solicitudes"


            // 3, Unidad de Transparencia
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 3, modclave = 1 }); //"Información"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 3, modclave = 3 }); //"Administración"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 3, modclave = 4 }); //"Importar"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 3, modclave = 5 }); //"Solicitudes"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 3, modclave = 6 }); //"Seguridad"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 3, modclave = 7 }); //"Tablero"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 3, modclave = 8 }); //"Reportes"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 3, modclave = 9 }); // Recurso

            // 4, Unidad de Trans.Auxiliar
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 4, modclave = 1 }); //"Información"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 4, modclave = 5 }); //"Solicitudes"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 4, modclave = 7 }); //"Tablero"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 4, modclave = 8 }); //"Reportes"

            // 5, Unidad Administrativa
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 5, modclave = 1 }); //"Información"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 5, modclave = 5 }); //"Solicitudes"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 5, modclave = 7 }); //"Tablero"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 5, modclave = 8 }); //"Reportes"

            // 6, Comite de Transparencia         
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 6, modclave = 1 }); //"Información"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 6, modclave = 5 }); //"Solicitudes"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 6, modclave = 7 }); //"Tablero"
            lstDatos.Add(new SIT_ADM_PERFILMOD { perclave = 6, modclave = 8 }); //"Reportes"

            return lstDatos;
        }

        public List<SIT_ADM_PERFILNODO> dmlSelectPerfilNodo( )
        {
            List<SIT_ADM_PERFILNODO> lstDatos = new List<SIT_ADM_PERFILNODO>();

            // 1, Sistema
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 1 }); //Crear soliciutd
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 2 });
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 3 });
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 4 });
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 5 });
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 6 });
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 7 });
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 8 });
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 9 });
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 1, nedclave = 10 });

            // 2, INAI
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 2, nedclave = 1 }); //Crear soliciutd
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 2, nedclave = 2 }); //Crear soliciutd

            // 3, Unidad de Transparencia UT
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 3, nedclave = 3 }); 

            // 4, PRUD
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 4, nedclave = 4}); // Recibir solicitud PRUD
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 4, nedclave = 5 }); // Recibir solicitud
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 4, nedclave = 6 }); // Recibir  aclaración


            // 5, Unidad Administrativa
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 5, nedclave = 7 });  // Recibir solicitud UA
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 5, nedclave = 8 });  // Modificar respuesta UA
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 5, nedclave = 9 }); // Generar RIA UA
            ////lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 5, nedclave = 13 }); // Leer mensaje

            // 6, Comite de Transparencia
            lstDatos.Add(new SIT_ADM_PERFILNODO { perclave = 6, nedclave = 10 }); // "Sesión CT"


            return lstDatos;
        }

        public List<SIT_ADM_CLASES> dmlSelectClases( )
        {
            List<SIT_ADM_CLASES> lstDatos = new List<SIT_ADM_CLASES>();
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 1, cladescripcion = "Áreas", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_AREADao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 2, cladescripcion = "Áreas gestión", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_AREAGESTIONDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 3, cladescripcion = "Áreas Historico", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_AREAHISTDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 4, cladescripcion = "Áreas nivel ", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_AREANIVELDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 5, cladescripcion = "Áreas Organización", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_AREAORGDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 6, cladescripcion = "Áreas Tipo", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_AREATIPODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 7, cladescripcion = "Clases", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_CLASESDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 8, cladescripcion = "Configuración", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_CONFIGURACIONDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 9, cladescripcion = "Día no laboral", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_DIANOLABORALDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 10, cladescripcion = "Módulo", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_MODULODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 11, cladescripcion = "Perfil", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_PERFILDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 12, cladescripcion = "Perfil clases", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_PERFILCLASESDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 13, cladescripcion = "Perfil módulo", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_PERFILMODDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 14, cladescripcion = "Perfil nodo", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_PERFILNODODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 15, cladescripcion = "Usuario perfil", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_USRPERFILDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 16, cladescripcion = "Usuario ", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_USUARIODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 17, cladescripcion = "Usuario área", clanombre = "SFP.SIT.SERV.Dao.ADM.SIT_ADM_USUARIOAREADao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 18, cladescripcion = "Arista comite", clanombre = "SFP.SIT.SERV.Dao.ARISTA.SIT_ARISTA_COMITEDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 19, cladescripcion = "Arista comite rubro", clanombre = "SFP.SIT.SERV.Dao.ARISTA.SIT_RESP_COMITERUBRODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 20, cladescripcion = "Arista resolución", clanombre = "SFP.SIT.SERV.Dao.ARISTA.SIT_ARISTA_RESOLUCIONDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 21, cladescripcion = "Arista recurso revisión", clanombre = "SFP.SIT.SERV.Dao.ARISTA.SIT_RESP_RREVISIONDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 22, cladescripcion = "Arista tipo", clanombre = "SFP.SIT.SERV.Dao.ARISTA.SIT_RESP_TIPODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 23, cladescripcion = "Arista tipo información", clanombre = "SFP.SIT.SERV.Dao.ARISTA.SIT_RESP_TIPOINFODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 24, cladescripcion = "Documento Arista", clanombre = "SFP.SIT.SERV.Dao.DOC.SIT_DOC_ARISTADao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 25, cladescripcion = "Documento", clanombre = "SFP.SIT.SERV.Dao.DOC.SIT_DOC_DOCUMENTODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 26, cladescripcion = "Documento Extension", clanombre = "SFP.SIT.SERV.Dao.DOC.SIT_DOC_EXTENSIONDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 27, cladescripcion = "AFD", clanombre = "SFP.SIT.SERV.Dao.RED.SIT_RED_AFDDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 28, cladescripcion = "AFD flujo", clanombre = "SFP.SIT.SERV.Dao.RED.SIT_RED_AFDFLUJODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 29, cladescripcion = "Red arista", clanombre = "SFP.SIT.SERV.Dao.RED.SIT_RED_ARISTADao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 30, cladescripcion = "Red nodo", clanombre = "SFP.SIT.SERV.Dao.RED.SIT_RED_NODODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 31, cladescripcion = "Red nodo estado", clanombre = "SFP.SIT.SERV.Dao.RED.SIT_RED_NODOESTADODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 32, cladescripcion = "Estado", clanombre = "SFP.SIT.SERV.Dao.SNT.SIT_SNT_ESTADODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 33, cladescripcion = "Municipio", clanombre = "SFP.SIT.SERV.Dao.SNT.SIT_SNT_MUNICIPIODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 34, cladescripcion = "Ocupación", clanombre = "SFP.SIT.SERV.Dao.SNT.SIT_SNT_OCUPACIONDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 35, cladescripcion = "Pais", clanombre = "SFP.SIT.SERV.Dao.SNT.SIT_SNT_PAISDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 36, cladescripcion = "Solicitante", clanombre = "SFP.SIT.SERV.Dao.SNT.SIT_SNT_SOLICITANTEDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 37, cladescripcion = "Solicitante tipo", clanombre = "SFP.SIT.SERV.Dao.SNT.SIT_SNT_SOLICITANTETIPODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 38, cladescripcion = "Medio entrada", clanombre = "SFP.SIT.SERV.Dao.SOL.SIT_SOL_MEDIOENTRADADao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 39, cladescripcion = "Medio entrega", clanombre = "SFP.SIT.SERV.Dao.SOL.SIT_SOL_MODOENTREGADao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 40, cladescripcion = "Proceso solicitud", clanombre = "SFP.SIT.SERV.Dao.SOL.SIT_SOL_PROCESODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 41, cladescripcion = "Processo plazos", clanombre = "SFP.SIT.SERV.Dao.SOL.SIT_SOL_PROCESOPLAZOSDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 42, cladescripcion = "Seguimiento", clanombre = "SFP.SIT.SERV.Dao.SOL.SIT_SOL_SEGUIMIENTODao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 43, cladescripcion = "Solicitud", clanombre = "SFP.SIT.SERV.Dao.SOL.SIT_SOL_SOLICITUDDao,SFP.SIT.SERV" });
            lstDatos.Add(new SIT_ADM_CLASES { claclave = 44, cladescripcion = "Solicitud tipo", clanombre = "SFP.SIT.SERV.Dao.SOL.SIT_SOL_SOLICITUDTIPODao,SFP.SIT.SERV" });

            return lstDatos;
        }

        public List<SIT_ADM_PERFILCLASES> dmlSelectPerfilClases( )
        {
            List<SIT_ADM_PERFILCLASES> lstDatos = new List<SIT_ADM_PERFILCLASES>();

            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 1 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 2 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 3 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 4 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 5 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 6 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 7 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 8 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 9 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 10 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 11 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 12 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 13 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 14 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 15 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 16 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 17 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 18 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 19 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 20 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 21 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 22 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 23 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 24 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 25 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 26 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 27 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 28 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 29 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 30 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 31 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 32 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 33 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 34 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 35 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 36 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 37 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 38 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 39 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 1, claclave = 40 });
      
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 1 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 3 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 4 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 8 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 9 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 10 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 11 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 12 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 13 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 16 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 17 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 18 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 19 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 20 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 21 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 22 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 24 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 25 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 26 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 27 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 28 });        
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 29 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 30 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 31 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 32 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 34 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 35 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 36 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 37 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 38 });
            lstDatos.Add(new SIT_ADM_PERFILCLASES { perclave = 3, claclave = 39 });
        
            return lstDatos;
        }

        public List<SIT_RED_AFD> dmlSelectRedAfd( )
        {
            List<SIT_RED_AFD> lstDatos = new List<SIT_RED_AFD>();
            lstDatos.Add(new SIT_RED_AFD {afdclave = 1, afddescripcion = "IMPORTACION", afdfecbaja = new DateTime(2015, 12, 31), afdprefijo = "Flujo1" });
            lstDatos.Add(new SIT_RED_AFD {afdclave = 2, afddescripcion = "NUEVA LEY", afdfecbaja = new DateTime(), afdprefijo = "Flujo2" });
            return lstDatos;
        }

        public List<SIT_RESP_CLASINFO> dmlSelectRespClasInfo()
        {
            List<SIT_RESP_CLASINFO> lstDatos = new List<SIT_RESP_CLASINFO>();
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 0, nfodescripcion = "No Aplica Tipo de Información" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 1, nfodescripcion = "Pública sin costo" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 2, nfodescripcion = "Pública con costo" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 3, nfodescripcion = "Pública Con Valor Comercial" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 4, nfodescripcion = "Reservada" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 5, nfodescripcion = "Confidencial" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 6, nfodescripcion = "Parcialmente reservada" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 7, nfodescripcion = "Parcialmente confidencial" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 8, nfodescripcion = "Aclaración" });
            lstDatos.Add(new SIT_RESP_CLASINFO { nfoclave = 9, nfodescripcion = "No competencia" });
            return lstDatos;
        }

        public List<SIT_RESP_TIPO> dmlSelectRespTipo( )
        {
            List<SIT_RESP_TIPO> lstDatos = new List<SIT_RESP_TIPO>();

            // Tipo: 1 IN           



            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 0, rtpdescripcion = "SIN RESPUESTA", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INTERNA,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 1, rtpdescripcion = "La solicitud no es de competencia de la unidad de enlace", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 2, rtpdescripcion = "No se dará trámite a la solicitud", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 3, rtpdescripcion = "La solicitud no corresponde al marco de la Ley Federal de Transparencia y Acceso a la Información Pública Gubernamental", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 4, rtpdescripcion = "Inexistencia de la información solicitada", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 5, rtpdescripcion = "La información está disponible públicamente", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 6, rtpdescripcion = "Se entrega información inmediata(informacion en medio electrónico)", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 7, rtpdescripcion = "Negativa por ser reservada o confidencial", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 8, rtpdescripcion = "Requerimiento de información adicional", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 9, rtpdescripcion = "Notificación de prórroga", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_NOTIFICAR,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 10, rtpdescripcion = "Notificación de disponibilidad de información pública", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_NOTIFICAR,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 11, rtpdescripcion = "Notificación de disponibilidad de información parcialmente reservada o confidencial", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_NOTIFICAR,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 12, rtpdescripcion = "Notificación de envío de información al solicitante", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_NOTIFICAR,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 13, rtpdescripcion = "Notificación de acceso de información al solicitante", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_NOTIFICAR,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 14, rtpdescripcion = "Informar la recepción de la información adicional", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_NOTIFICAR,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 15, rtpdescripcion = "Respuesta del solicitante a la notificación de la disponibilidad de información, seleccionando entrega sin costo", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_PAGO,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 16, rtpdescripcion = "Respuesta del solicitante a la notificación de la disponibilidad de información, seleccionando entrega con costo", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_PAGO,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 17, rtpdescripcion = "Notificación de pago", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INAI_PAGO,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 18, rtpdescripcion = "Enviar", rtpformato = sTextoDoc(0), rtpforma = "PVenviar", rtptipo = Constantes.RespuestaTipo.INTERNA,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 19, rtpdescripcion = "Responder", rtpformato = sTextoDoc(0), rtpforma = "PVresponder", rtptipo = Constantes.RespuestaTipo.INTERNA,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 20, rtpdescripcion = "Asignar", rtpformato = sTextoDoc(0), rtpforma = "PVasignar", rtptipo = Constantes.RespuestaTipo.INTERNA,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 21, rtpdescripcion = "Turnar", rtpformato = sTextoDoc(0), rtpforma = "PVturnar", rtptipo = Constantes.RespuestaTipo.INTERNA,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 22, rtpdescripcion = "Respuesta Múltiple", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = Constantes.RespuestaTipo.INTERNA_NOVISIBLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 23, rtpdescripcion = "Pública", rtpformato = sTextoDoc(0), rtpforma = "PVpublica", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });            
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 24, rtpdescripcion = "Pública a la vista", rtpformato = sTextoDoc(0), rtpforma = "PVpublica", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 25, rtpdescripcion = "Incompetencia parcial del área", rtpformato = sTextoDoc(0), rtpforma = "PVincompArea", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 26, rtpdescripcion = "Incompetencia total del área", rtpformato = sTextoDoc(0), rtpforma = "PVincompArea", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 27, rtpdescripcion = "Inexistencia de información en el área", rtpformato = sTextoDoc(0), rtpforma = "PVinexistencia", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 28, rtpdescripcion = "Información reservada", rtpformato = sTextoDoc(0), rtpforma = "PVreservada", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 29, rtpdescripcion = "Información reservada parcialmente", rtpformato = sTextoDoc(0), rtpforma = "PVreservadaParcial", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 30, rtpdescripcion = "Información confidencial", rtpformato = sTextoDoc(0), rtpforma = "PVconfidencial", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 31, rtpdescripcion = "Información confidencial parcialmente", rtpformato = sTextoDoc(0), rtpforma = "PVconfidencialParcial", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 32, rtpdescripcion = "Ampliación de plazo", rtpformato = sTextoDoc(0), rtpforma = "PVampliacionPlazo", rtptipo = Constantes.RespuestaTipo.INTERNA_MULTIPLE,rtpreporta = 0, rtpplazo = 3 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 33, rtpdescripcion = "Para sesión del Comité", rtpformato = sTextoDoc(0), rtpforma = "PVparaComite", rtptipo = Constantes.RespuestaTipo.INTERNA,rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 34, rtpdescripcion = "Corregir", rtpformato = sTextoDoc(0), rtpforma = "Pvcorregir", rtptipo = Constantes.RespuestaTipo.INTERNA, rtpreporta = 0, rtpplazo = 0 });
            lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 35, rtpdescripcion = "Requerimiento de Información Adicional", rtpformato = sTextoDoc(0), rtpforma = "PVria", rtptipo = Constantes.RespuestaTipo.INTERNA, rtpreporta = 0, rtpplazo = 2 });


            ////////////////////////////////////
            // SEGUNDA APROXIMACION
            ////////////////////////////////////
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 0, rtpdescripcion = "SIN RESPUESTA", rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = 2, rtpreporta=0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 1, rtpdescripcion = "Enviar", rtpformato = sTextoDoc(0), rtpforma = "PVenviar", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 2, rtpdescripcion = "Turnar", rtpformato = sTextoDoc(0), rtpforma = "PVturnar", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 3, rtpdescripcion = "Asignar", rtpformato = sTextoDoc(0), rtpforma = "PVasignar", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 4, rtpdescripcion = "Responder", rtpformato = sTextoDoc(0), rtpforma = "PVresponder", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 5, rtpdescripcion = "__", rtpformato = sTextoDoc(0), rtpforma = "PV__", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 6, rtpdescripcion = "Incompetencia", rtpformato = sTextoDoc(0), rtpforma = "PVincompetencia", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 7, rtpdescripcion = "Ampliación de plazo", rtpformato = sTextoDoc(0), rtpforma = "PVampliacionPlazo", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 8, rtpdescripcion = "Aclaración", rtpformato = sTextoDoc(0), rtpforma = "PVaclaracion", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 9, rtpdescripcion = "Corregir", rtpformato = sTextoDoc(0), rtpforma = "PVcorregir", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 10, rtpdescripcion = "Para el Comité", rtpformato = sTextoDoc(0), rtpforma = "Pvcomite", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 11, rtpdescripcion = "Respueta RIA", rtpformato = sTextoDoc(0), rtpforma = "PVrespuetaRIA", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 12, rtpdescripcion = "Solicitar RIA", rtpformato = sTextoDoc(0), rtpforma = "PVsolicitarRIA", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 13, rtpdescripcion = "Crear respuesta", rtpformato = sTextoDoc(0), rtpforma = "PVcrearRespuesta", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 14, rtpdescripcion = "Recurso de revision", rtpformato = sTextoDoc(0), rtpforma = "PVrecursoRevision", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 15, rtpdescripcion = "Datos de la aclaración", rtpformato = sTextoDoc(0), rtpforma = "PDdatosAclaración", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 16, rtpdescripcion = "Asignar Rec Rev", rtpformato = sTextoDoc(0), rtpforma = "PVasignarRR", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 17, rtpdescripcion = "Crear mensaje", rtpformato = sTextoDoc(0), rtpforma = "PVcrearMensaje", rtptipo = 2,rtpreporta = 0, rtpplazo = 0 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 18, rtpdescripcion = "La solicitud no es de competencia de la unidad de enlace.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4});
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 19, rtpdescripcion = "No se dará trámite a la solicitud.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 20, rtpdescripcion = "La solicitud no corresponde al marco de la Ley Federal de Transparencia y Acceso a la Información Pública Gubernamental.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 21, rtpdescripcion = "Inexistencia de la información solicitada.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 22, rtpdescripcion = "La información está disponible públicamente.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 23, rtpdescripcion = "Se entrega información inmediata (informacion en medio electrónico)", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 24, rtpdescripcion = "Negativa por ser reservada o confidencial", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 25, rtpdescripcion = "Requerimiento de información adicional.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 26, rtpdescripcion = "Notificación de prórroga.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 27, rtpdescripcion = "Notificación de disponibilidad de información pública.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 28, rtpdescripcion = "Notificación de disponibilidad de información parcialmente reservada o confidencial.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 29, rtpdescripcion = "Notificación de envío de información al solicitante.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 30, rtpdescripcion = "Notificación de acceso de información al solicitante. (Fecha y lugar de entrega)", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 31, rtpdescripcion = "Informar la recepción de la información adicional.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 32, rtpdescripcion = "Respuesta del solicitante a la notificación de la disponibilidad de información, seleccionando entrega sin costo.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 33, rtpdescripcion = "Respuesta del solicitante a la notificación de la disponibilidad de información, seleccionando entrega con costo.", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 34, rtpdescripcion = "Notificación de pago", rtpformato = sTextoDoc(0), rtpforma = "PVrespuesta", rtptipo = 1, rtpreporta = 4 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 35, rtpdescripcion = "Pública", rtpformato = sTextoDoc(0), rtpforma = "PVpublica", rtptipo = 3, rtpreporta = 5 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 36, rtpdescripcion = "Pública a la vista", rtpformato = sTextoDoc(0), rtpforma = "PVpublica", rtptipo = 3, rtpreporta = 5 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 37, rtpdescripcion = "Incompetencia del área", rtpformato = sTextoDoc(0), rtpforma = "PVincompArea", rtptipo = 3, rtpreporta = 5 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 38, rtpdescripcion = "Inexistencia de información en el área", rtpformato = sTextoDoc(0), rtpforma = "PVinexistencia", rtptipo = 3, rtpreporta = 5 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 39, rtpdescripcion = "Información reservada", rtpformato = sTextoDoc(0), rtpforma = "PVreservada", rtptipo = 3, rtpreporta = 5 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 40, rtpdescripcion = "Información confidencial", rtpformato = sTextoDoc(0), rtpforma = "PVconfidencial", rtptipo = 3, rtpreporta = 5 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 41, rtpdescripcion = "Parcialmente confidencial", rtpformato = sTextoDoc(0), rtpforma = "PVconfidencialParcial", rtptipo = 3, rtpreporta = 5 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 42, rtpdescripcion = "Parcialmente reservada", rtpformato = sTextoDoc(0), rtpforma = "PVreservadaParcial", rtptipo = 3, rtpreporta = 5 });
            //////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 43, rtpdescripcion = "Revisar PRUT", rtpformato = sTextoDoc(0), rtpforma = "PVREvisarPRUT", rtptipo = 3, rtpreporta = 5 });




            ////////////////////////////////////////////////
            // PRIMER APROXIMACION
            ////////////////////////////////////////////////
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 18, rtpdescripcion = "Turnar",   rtpformato = sTextoDoc(0),  rtpforma = "PVturnar", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 19, rtpdescripcion = "Pública",   rtpformato = sTextoDoc(0), rtpforma = "PVpublica", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 20, rtpdescripcion = "Pública a la vista",   rtpformato = sTextoDoc(0),  rtpforma = "PVpublica", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 21, rtpdescripcion = "Aclaración",   rtpformato = sTextoDoc(0),   rtpforma = "PVpublica", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 22, rtpdescripcion = "Incompetencia parcial",   rtpformato = sTextoDoc(0),  rtpforma = "PVpublica", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 23, rtpdescripcion = "Incompetencia total",   rtpformato = sTextoDoc(0), rtpforma = "PVpublica", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 24, rtpdescripcion = "Ampliación de plazo",   rtpformato = sTextoDoc(0),   rtpforma = "PVrespSimple", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 25, rtpdescripcion = "Inexistencia de información",   rtpformato = sTextoDoc(0), rtpforma = "PVinexistencia", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 26, rtpdescripcion = "Información reservada",   rtpformato = sTextoDoc(0),  rtpforma = "PVreservada", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 27, rtpdescripcion = "Información confidencial",   rtpformato = sTextoDoc(0), rtpforma = "PVconfidencial", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 28, rtpdescripcion = "Confirmar",   rtpformato = sTextoDoc(0),   rtpforma = "PVpublica", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 29, rtpdescripcion = "Modificar",   rtpformato = sTextoDoc(0),   rtpforma = "PVpublica", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 30, rtpdescripcion = "Revocar",   rtpformato = sTextoDoc(0),     rtpforma = "PVpublica", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 31, rtpdescripcion = "Requerimiento",   rtpformato = sTextoDoc(0),  rtpforma = "PVpublica", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 32, rtpdescripcion = "Notificacion de cambio de tipo de solicitud",   rtpformato = sTextoDoc(0), rtpforma = "PVpublica", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 33, rtpdescripcion = "Registrar y notificar en sistema",   rtpformato = sTextoDoc(0), rtpforma = "PVnotificar", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 34, rtpdescripcion = "Para sesión del comite",   rtpformato = sTextoDoc(0), rtpforma = "PVrespSimple", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 35, rtpdescripcion = "Resolución comite",   rtpformato = sTextoDoc(0),   rtpforma = "PVcomite", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 36, rtpdescripcion = "Contestar requerimiento",   rtpformato = sTextoDoc(0),   rtpforma = "PVrespSimple", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 37, rtpdescripcion = "Notificar resultado en la plataforma INAI-INFOMEX",   rtpformato = sTextoDoc(0),  rtpforma = "PVresultado", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 38, rtpdescripcion = "Notificar Recurso de Revision",   rtpformato = sTextoDoc(0),  rtpforma = "N/A", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 39, rtpdescripcion = "Parcialmente confidencial",   rtpformato = sTextoDoc(0),rtpforma = "PVreservadaParcial", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 40, rtpdescripcion = "Parcialmente reservada",   rtpformato = sTextoDoc(0),   rtpforma = "PVpublica", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 41, rtpdescripcion = "Conceder ampliación",   rtpformato = sTextoDoc(0),  rtpforma = "PVrespSimple", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 42, rtpdescripcion = "Negar ampliación",   rtpformato = sTextoDoc(0),   rtpforma = "PVrespSimple", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 43, rtpdescripcion = "Mensaje",   rtpformato = sTextoDoc(0),  rtpforma = "PVrespSimple", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 44, rtpdescripcion = "Returnar",   rtpformato = sTextoDoc(0),   rtpforma = "PVturnar", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 45, rtpdescripcion = "Cancelar",   rtpformato = sTextoDoc(0),   rtpforma = "PVturnar", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 46, rtpdescripcion = "Cierre automático",   rtpformato = sTextoDoc(0),    rtpforma = "PVturnar", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 47, rtpdescripcion = "Turnar al personal responsable de la Unidad de Transparencia",   rtpformato = sTextoDoc(0),   rtpforma = "PVasignar", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 48, rtpdescripcion = "Turnar áreas internas",   rtpformato = sTextoDoc(0),  rtpforma = "PVturnarSubAreas", rtptipo = 2 });

            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 101, rtpdescripcion = "Cancelar",   rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = 2 });
            ////lstDatos.Add(new SIT_RESP_TIPO { rtpclave = 102, rtpdescripcion = "Cierre automático",   rtpformato = sTextoDoc(0), rtpforma = "N/A", rtptipo = 2 });
            return lstDatos;
        }

        public List<SIT_RESP_TIPOINFO> dmlSelectRespTipoInfo()
        {
            List<SIT_RESP_TIPOINFO> lstDatos = new List<SIT_RESP_TIPOINFO>();
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 7, nfoclave = 4 }); // Negativa por ser confidencial o reservada - 4) Reservada
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 7, nfoclave = 5 }); // Negativa por ser confidencial o reservada - 5) Confidencial


            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 4, nfoclave = 0}); // Inexistencia de la información - 4) No aplica tipo de información
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 9, nfoclave = 0 }); // Notificación de Prorroga - 4) No aplica tipo de información

            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 11, nfoclave = 6 }); // Notificación de disponibilidad de información parcialmente reservada o confidencial - 6) Parcialmente reservada
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 11, nfoclave = 7 }); // Notificación de disponibilidad de información parcialmente reservada o confidencial - 7) Parcialemnte confidencial

            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 11, nfoclave = 1 }); // Notificación de disponibilidad de información pública - 1) Pública sin costo
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 11, nfoclave = 2 }); // Notificación de disponibilidad de información pública - 2) Pública con costo
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 11, nfoclave = 3 }); // Notificación de disponibilidad de información pública - 3) Pública Con Valor Comercial

            
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 2, nfoclave = 1 }); // No se dará trámite a la solicitud. - 1) Pública sin costo
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 3, nfoclave = 1 }); // La solicitud no corresponde al marco de la Ley Federal de Transparencia y Acceso - 1) Pública sin costo
            //////lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 5, nfoclave = 1 }); // La información está disponible públicamente. - 1) Pública sin costo
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 6, nfoclave = 1 }); // Se entrega información inmediata (informacion en medio electrónico) - 1) Pública sin costo


            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 1, nfoclave = 0 }); // Se entrega información inmediata (informacion en medio electrónico) - 0) No aplica tipo de información
            lstDatos.Add(new SIT_RESP_TIPOINFO { rtpclave = 21, nfoclave = 0 }); // Se entrega información inmediata (informacion en medio electrónico) - 0)No aplica tipo de información
            
            return lstDatos;
        }

        public List<SIT_RESP_ESTADO> dmlSelectRespEstado()
        {
            List<SIT_RESP_ESTADO> lstDatos = new List<SIT_RESP_ESTADO>();
            lstDatos.Add(new SIT_RESP_ESTADO { sdoclave = 1, sdodescripcion = "Tunar" });
            lstDatos.Add(new SIT_RESP_ESTADO { sdoclave = 2, sdodescripcion = "Turnado" }); 
            lstDatos.Add(new SIT_RESP_ESTADO { sdoclave = 3, sdodescripcion = "Propuesta" }); 
            lstDatos.Add(new SIT_RESP_ESTADO { sdoclave = 4, sdodescripcion = "Analizar" }); 
            lstDatos.Add(new SIT_RESP_ESTADO { sdoclave = 5, sdodescripcion = "RIA" }); 
            lstDatos.Add(new SIT_RESP_ESTADO { sdoclave = 6, sdodescripcion = "Responder" }); 
            lstDatos.Add(new SIT_RESP_ESTADO { sdoclave = 7, sdodescripcion = "Aceptada" });
            return lstDatos;
        }

        //LA SECCION DEL DOCUMENTO.,.......
        ////public List<SIT_RESP_CONTENIDOTIPO> dmlSelectRespContenidoTipo()
        ////{
        ////    List<SIT_RESP_CONTENIDOTIPO> lstDatos = new List<SIT_RESP_CONTENIDOTIPO>();
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 1, ctpdescripcion = "Instrucción", ctpclase = 1 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 2, ctpdescripcion = "Contenido", ctpclase = 2 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 3, ctpdescripcion = "Fundamento Legal", ctpclase = 3 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 4, ctpdescripcion = "Justificación", ctpclase = 3 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 5, ctpdescripcion = "Motivos", ctpclase = 3 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 6, ctpdescripcion = "Reserva Partes", ctpclase = 3 });

        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 7, ctpdescripcion = "Tiempo", ctpclase = 4 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 8, ctpdescripcion = "Modo", ctpclase = 4 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 9, ctpdescripcion = "Lugar", ctpclase = 4 });
            
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 10, ctpdescripcion = "Acta de Nacimiento", ctpclase = 5 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 11, ctpdescripcion = "Acta de Matrimonio", ctpclase = 5 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 12, ctpdescripcion = "Alias, seudónimos, nombre de usuario", ctpclase = 5 });
        ////    lstDatos.Add(new SIT_RESP_CONTENIDOTIPO { ctpclave = 13, ctpdescripcion = "Beneficiarios", ctpclase = 5 });
            

        ////    return lstDatos;
        ////}

        public List<SIT_RESP_MOMENTO> dmlSelectRespMomento()
        {
            List<SIT_RESP_MOMENTO> lstDatos = new List<SIT_RESP_MOMENTO>();
            lstDatos.Add(new SIT_RESP_MOMENTO { momclave = 1, momdescripcion = "I.Se reciba una solicitud de acceso a la información;" });
            lstDatos.Add(new SIT_RESP_MOMENTO { momclave = 2, momdescripcion = "II.Se determine mediante resolución de autoridad competente" });
            lstDatos.Add(new SIT_RESP_MOMENTO { momclave = 3, momdescripcion = "III.Se generen versiones públicas para dar cumplimiento a las obligaciones de transparencia previstas en la Ley General y en la Ley Federal" });
            return lstDatos;
        }
        public List<SIT_RESP_TEMA> dmlSelectRespTema()
        {
            List<SIT_RESP_TEMA> lstDatos = new List<SIT_RESP_TEMA>();
            lstDatos.Add(new SIT_RESP_TEMA { temclave = 1,  temdescripcion = "COMPRANET",  araclave = 502 });

            return lstDatos;
        }

        public List<SIT_RESP_REPRODUCCION> dmlSelectRespReproduccion()
        {
            List<SIT_RESP_REPRODUCCION> lstDatos = new List<SIT_RESP_REPRODUCCION>();
            lstDatos.Add(new SIT_RESP_REPRODUCCION { rccclave = 0, rccdescripcion = "Aplica costo" });
            lstDatos.Add(new SIT_RESP_REPRODUCCION { rccclave = 1, rccdescripcion = "Cuando implique la entrega de no más de veinte hojas simples." });
            lstDatos.Add(new SIT_RESP_REPRODUCCION { rccclave = 2, rccdescripcion = "Considerando la situación socioeconómica del solicitante. Autorizado por el CT." });
            lstDatos.Add(new SIT_RESP_REPRODUCCION { rccclave = 3, rccdescripcion = "Si la respuesta realizada fue posterior a los veinte días hábiles establecidos." });
            return lstDatos;
        }
        
        public List<SIT_RED_NODOESTADO> dmlSelectNodoEstado( )
        {
            List<SIT_RED_NODOESTADO> lstDatos = new List<SIT_RED_NODOESTADO>();

            //// REVISION PRIMERA FASE..
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 1, neddescripcion = "Crear solicitud", nedurl = "INAIsol", nedtipo = Constantes.NodoEstado.TIPO_INICIO });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 2, neddescripcion = "Respuesta solicitud", nedurl = "N/A", nedtipo = Constantes.NodoEstado.TIPO_TERMINAL });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 3, neddescripcion = "La solicitud requiere aclaración", nedurl = "N/A", nedtipo = Constantes.NodoEstado.TIPO_TERMINAL });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 4, neddescripcion = "La solicitud con incompetencia parcial", nedurl = "N/A", nedtipo = Constantes.NodoEstado.TIPO_TERMINAL });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 5, neddescripcion = "La solicitud requiere prorroga", nedurl = "N/A", nedtipo = Constantes.NodoEstado.TIPO_TERMINAL });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 6, neddescripcion = "Recibir solicitud", nedurl = "UTrecibirSol", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 7, neddescripcion = "Analizar respuesta", nedurl = "UTanalizar", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 8, neddescripcion = "Registrar respuesta en INAI", nedurl = "UTnotificar", nedtipo = Constantes.NodoEstado.TIPO_TERMINAL });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 9, neddescripcion = "Analizar solicitud", nedurl = "UAanalizar", nedtipo = Constantes.NodoEstado.TIPO_TERMINAL });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 10, neddescripcion = "Sesionar determinaciones", nedurl = "CTsesionar", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 11, neddescripcion = "Sesionar ampliación", nedurl = "CTampliacion", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 12, neddescripcion = "Solicitar requerimiento", nedurl = "N/A", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 13, neddescripcion = "Contestar requerimiento", nedurl = "UArequerir", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 14, neddescripcion = "Recibir resultado", nedurl = "UTresultado", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 15, neddescripcion = "Aclaracion solicitud", nedurl = "INAIacl", nedtipo = Constantes.NodoEstado.TIPO_INICIO });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 16, neddescripcion = "Recibir aclaración", nedurl = "UTrecibirAcl", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 17, neddescripcion = "Recibir recurso revisión", nedurl = "UTrecibirRev", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 18, neddescripcion = "Analizar recurso revisión", nedurl = "UArecrevision", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 19, neddescripcion = "Recibir notificacion Ampliación", nedurl = "NodoMensaje", nedtipo = Constantes.NodoEstado.TIPO_TERMINAL });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 20, neddescripcion = "Notificar respuesta final", nedurl = "NodoMsjFinal", nedtipo = Constantes.NodoEstado.TIPO_TERMINAL });
            ////lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 21, neddescripcion = "Registar ampliación de plazo en INAI", nedurl = "UTnotificarAmp", nedtipo = Constantes.NodoEstado.TIPO_TRANSICION });


            // REVISION DE LA SEGUNDA FASE
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 1, neddescripcion = "Crear solicitud", nedurl = "INAIsol", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 2, neddescripcion = "Recibir solicitud", nedurl = "UTrecibirSol", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 3, neddescripcion = "Asignar solicitud PRUD", nedurl = "PRUDasignar", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 4, neddescripcion = "Analizar solicitud UA", nedurl = "UAanalizar", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });            
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 5, neddescripcion = "Analizar respuesta UT", nedurl = "UTanalizarResp", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 6, neddescripcion = "Modificar respuesta UA", nedurl = "UAmodificarResp", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 7, neddescripcion = "Respuesta final", nedurl = "INAIrespuesta", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 8, neddescripcion = "Sesión CT", nedurl = "CTsesion", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 9, neddescripcion = "Reg info UT", nedurl = "UTreqInfo", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 10, neddescripcion = "Generar RIA UA", nedurl = "UAria", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 11, neddescripcion = "Revisar RIA", nedurl = "UTrevisarRIA", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 12, neddescripcion = "Respuesta del comité", nedurl = "UTrespCT", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 13, neddescripcion = "Leer mensaje", nedurl = "LeerMensaje", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 14, neddescripcion = "Recurso Revision", nedurl = "INAIrecrev", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });
            //lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 15, neddescripcion = "Analizar respuestas PRUD", nedurl = "PRUDanalizarResp", nedtipo = Constantes.NodoEstadoTipo.TIPO_INICIO });


            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 1, neddescripcion = "INAI_Crear solicitud", nedurl = "INAIsol", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 2, neddescripcion = "INAI_Respuesta solicitud", nedurl = "SIN_CLASE", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 3, neddescripcion = "UT_Recibir solicitud", nedurl = "UTrecibirSol", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 4, neddescripcion = "PRUD_Recibir solicitud", nedurl = "PRUDrecibirSol", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 5, neddescripcion = "PRUD_Revisar respuesta", nedurl = "PRUDrevisarResp", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 6, neddescripcion = "PRUD_Notificar", nedurl = "PRUDnotificar", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 7, neddescripcion = "UA_Analizar solicitud", nedurl = "UAanalizarSol", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });            
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 8, neddescripcion = "UA_Corregir respuesta", nedurl = "UAcorregirResp", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 9, neddescripcion = "UA_Información cómite", nedurl = "UAinfoComite", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            lstDatos.Add(new SIT_RED_NODOESTADO { nedclave = 10, neddescripcion = "CT_Sesión", nedurl = "CTsesion", nedtipo = Constantes.NodoEstadoTipo.NINGUNO });
            

            return lstDatos;
        }

        public List<SIT_RED_AFDFLUJO> dmlSelectRedAfdFlujo( )
        {
            List<SIT_RED_AFDFLUJO> lstDatos = new List<SIT_RED_AFDFLUJO>();


            /////* 1,"Crear solicitud", 1,"INAIsol"*/
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 1, rtpclave = 18, affdestino = 6, affplazo = 0 });  // turno -> Analizar solicitud (UT)

            /////* 6,"Recibir solicitud", UT */
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 6, rtpclave = 18, affdestino = 9, affplazo = 0 });  // turno -> Analizar solicitud (UA)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 6, rtpclave = 8, affdestino = 8, affplazo = 0 });  // Requerimiento de información adicional. -> Registrar en INAI (UT solnotificadoR)            
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 6, rtpclave = 1, affdestino = 2, affplazo = 0 });    //"Solicitud NO Competencia (UT) -> RESPUESTA SOLIICUTD (INAI) 
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 6, rtpclave = 24, affdestino = 11, affplazo = 0 });  //  Ampliacion de plazo -> Sesionar ampliación (CT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 6, rtpclave = 47, affdestino = 6, affplazo = 0 });  //  Ampliacion de plazo -> Sesionar ampliación (CT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 10, rtpclave = 35, affdestino = 14, affplazo = 0 });  //   Resolución comite -> Registrar para INAI (UT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 10, rtpclave = 28, affdestino = 8, affplazo = 0 });  //   Confirmar -> Registrar en INAI (UT solnotificadoR)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 10, rtpclave = 31, affdestino = 12, affplazo = 0 });  //   Requerimiento -> Solicitar requerimientO {afdclave=UT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 11, rtpclave = 41, affdestino = 21, affplazo = 0 });  //   Conceder prorroga ->  Registrar  INAI(UT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 11, rtpclave = 42, affdestino = 19, affplazo = 0 });  //   Negar prorroga ->   NOTIFICAR (UA, UT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 11, rtpclave = 43, affdestino = 19, affplazo = 0 });  //   Mensaje ->  NOTIFICAR (UA, UT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 19, rtpclave = 28, affdestino = 19, affplazo = 0 });  //   CONFIRMAR (LECTURA) ->  NOTIFICAR (UA, UT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 20, rtpclave = 33, affdestino = 20, affplazo = 0 });  //   CONFIRMAR (LECTURA) ->  Respuesta solicitud(INAI) 
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 21, rtpclave = 9, affdestino = 2, affplazo = 0 });       //Notificar prorroga -> Respuesta solicitud(INAI)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 1, affdestino = 8, affplazo = 0 });  //  No es competencia -> Respuesta solicitud(INAI)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 2, affdestino = 8, affplazo = 0 });  //  No se dara trámite -> Respuesta solicitud(INAI)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 3, affdestino = 8, affplazo = 0 });  //  No corresponde LFTAIPG -> Respuesta solicitud(INAI)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 5, affdestino = 8, affplazo = 0 });  //  No disponible -> Respuesta solicitud(INAI)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 6, affdestino = 8, affplazo = 0 });  //  Entrega inmediata -> Respuesta solicitud(INAI)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 8, affdestino = 8, affplazo = 0 });  //  Requerimiento adicional -> Respuesta solicitud(INAI)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 10, affdestino = 8, affplazo = 0 }); //  Notificación disponibilidad -> Respuesta solicitud(INAI)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 28, affdestino = 9, affplazo = 0 });  //   Confirmar -> analizar (UA)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 29, affdestino = 9, affplazo = 0 });  //   Modificar -> analizar (UA)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 44, affdestino = 9, affplazo = 0 });  //   Modificar -> analizar (UA)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 34, affdestino = 10, affplazo = 0 });  //   Sesion del comite ->  Sesionar determinaciones (CT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 12, rtpclave = 18, affdestino = 13, affplazo = 0 });  //   Turnar ->  Contestar requerimiento (UA)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 8, rtpclave = 33, affdestino = 2, affplazo = 0 });  //    Registrar en sistema ->    Respuesta solicitud(INAI) 
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 8, rtpclave = 43, affdestino = 20, affplazo = 0 });  //    Registrar en sistema ->    Respuesta solicitud(INAI) 
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 48, affdestino = 9, affplazo = 0 });  //    Turnar áreas internas->   Analizar solicitud(UA)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 19, affdestino = 7, affplazo = 0 });  //    Pública ->   Analizar respuesta UT
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 20, affdestino = 7, affplazo = 5 });  //    Pública la vista ->   Analizar respuesta UT
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 21, affdestino = 7, affplazo = 2 });  //    Aclaracion ->   Analizar respuesta UT
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 23, affdestino = 7, affplazo = 8 });  //    Incompentencia total ->   Analizar respuesta UT
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 24, affdestino = 11, affplazo = 3 });  //    Ampliacion de plazo ->   Analizar CT
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 25, affdestino = 7, affplazo = 8 });  //    Inexistenica ->   Analizar respuesta UT
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 26, affdestino = 7, affplazo = 0 });  //    Reservada ->   Analizar respuesta UT 
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 27, affdestino = 7, affplazo = 0 });  //    Confidencial ->  Analizar respuesta UT
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 9, rtpclave = 40, affdestino = 7, affplazo = 0 });  //    PARCIALMENTE CONFIDENICAL ->  Analizar respuesta UT
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 12, rtpclave = 31, affdestino = 13, affplazo = 0 });  //    Requerimiento ->   Contestar requerimientO {afdclave=UA)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 13, rtpclave = 36, affdestino = 10, affplazo = 0 });  //    Completar requerimiento ->   Sesionar requermientO {afdclave=CT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 14, rtpclave = 37, affdestino = 8, affplazo = 0 });  //   Confirmar ->   Registrar en INAI(UT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 15, rtpclave = 18, affdestino = 16, affplazo = 0 });  //    Turnar ->   Analizar solicitud(UA)

            ////RECIBIR ACLARCION
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 16, rtpclave = 18, affdestino = 9, affplazo = 0 });  //    Turnar ->   Analizar solicitud(UA)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 16, rtpclave = 8, affdestino = 8, affplazo = 0 });  // Requerimiento de información adicional. -> Registrar en INAI (UT solnotificadoR)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 16, rtpclave = 2, affdestino = 8, affplazo = 0 });  // Incompetencia parcial -> Registrar en INAI (UT solnotificadoR)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 16, rtpclave = 1, affdestino = 8, affplazo = 0 });  //"La solicitud no es de competencia de la unidad de enlace -> Registrar en INAI (UT solnotificadoR)       

            ////RECIBIR RECURSO DE ACLARACION
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 2, rtpclave = 38, affdestino = 17, affplazo = 0 });  // Notificar Rercurso de Revisión ->   Recibir Recurso de Revisión (UT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 17, rtpclave = 18, affdestino = 18, affplazo = 0 });  // Turnar ->   Analizar Recurso de Revisión
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 18, rtpclave = 19, affdestino = 10, affplazo = 0 });  // Publica ->   Sesionar determinaciones (CT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 18, rtpclave = 23, affdestino = 10, affplazo = 0 });  // Incompentencia total ->   Sesionar determinaciones (CT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 18, rtpclave = 25, affdestino = 10, affplazo = 0 });  // Inexistenica ->   Sesionar determinaciones (CT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 18, rtpclave = 26, affdestino = 10, affplazo = 0 });  // Reservada ->   Sesionar determinaciones (CT)
            ////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 18, rtpclave = 27, affdestino = 10, affplazo = 0 });  // Confidencial ->   Sesionar determinaciones (CT)




            // APROXIMACION DOS...
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 1, rtpclave = 1, affdestino = 2, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 2, rtpclave = 2, affdestino = 4, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 2, rtpclave = 3, affdestino = 3, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 2, rtpclave = 4, affdestino = 7, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 3, rtpclave = 2, affdestino = 4, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 3, rtpclave = 4, affdestino = 7, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 2, affdestino = 4, affplazo = 0 });  //  TURNAR
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 7, affdestino = 5, affplazo = 3 });  // Ampliación de plazo
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 8, affdestino = 5, affplazo = 2 });  // Aclaración
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 35, affdestino = 5, affplazo = 0 });  //Pública 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 37, affdestino = 5, affplazo = 0 });  // Incompetencia del área
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 38, affdestino = 5, affplazo = 8 });  // Inexistencia de información en el área
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 39, affdestino = 5, affplazo = 0 });  // Información reservada
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 40, affdestino = 5, affplazo = 0 });  // Información confidencial
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 41, affdestino = 5, affplazo = 0 });  // Parcialmente confidencial
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 42, affdestino = 5, affplazo = 0 });  // Parcialmente reservada
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 5, rtpclave = 2, affdestino = 4, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 5, rtpclave = 9, affdestino = 6, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 5, rtpclave = 10, affdestino = 8, affplazo = 0 });  // 
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 5, rtpclave = 4, affdestino = 7, affplazo = 0 });  // 
            //////// POR EL MOMNET OPAR QUE FUNCIONE SLA APLICACION..
            //////lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 43, affdestino = 15, affplazo = 0 });  // Parcialmente reservada



            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 1, rtpclave = 18, affdestino = 3 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 3, rtpclave = 19, affdestino = 2 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 3, rtpclave = 20, affdestino = 4 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 19, affdestino = 2 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 4, rtpclave = 21, affdestino = 7 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 21, affdestino = 7 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 22, affdestino = 5 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 7, rtpclave = 35, affdestino = 5 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 5, rtpclave = 19, affdestino = 2 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 5, rtpclave = 33, affdestino = 10 });
            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 5, rtpclave = 34, affdestino = 8 });

            lstDatos.Add(new SIT_RED_AFDFLUJO { afdclave = 2, afforigen = 8, rtpclave = 00, affdestino = 5 });

//INAI_Crear solicitud        Enviar                  UT_Recibir solicitud
//UT_Recibir solicitud        Responder               INAI_Respuesta
//UT_Recibir solicitud        Asignar                 PRUD_Recibir solicitud
//PRUD_Recibir solicitud      Responder               INAI_Respuesta
//PRUD_Recibir solicitud      Turnar                  UA_Analizar solicitud
//UA_Analizar solicitud       Turnar                  UA_Analizar solicitud
//UA_Analizar solicitud       Resp Multiple           PRUD_Revisar respuesta
//PRUD_Revisar respuesta      Responder               INAI_Respuesta
//PRUD_Revisar respuesta      Para sesión del Comité  CT_Sesión
//PRUD_Revisar respuesta      Corregir                UA_Corregir respuesta
//UA_Corregir respuesta       Resp Multiple           PRUD_Revisar respuesta


            return lstDatos;
        }

        public List<SIT_SNT_PAIS> dmlSelectPais( )
        {
            List<SIT_SNT_PAIS> lstDatos = new List<SIT_SNT_PAIS>();
            String sqlQuery = "SELECT IDPAIS, NOMBRE FROM PAIS ORDER BY IDPAIS";
            DataTable dtPais = ConsultaDML(sqlQuery);

            SIT_SNT_PAIS auxpaisMdl = new SIT_SNT_PAIS();
            auxpaisMdl.paiclave = 0;
            auxpaisMdl.paidescripcion = "N/A";
            lstDatos.Add(auxpaisMdl);

            Int32 iPais;
            bool parsed;

            foreach (DataRow drUsu in dtPais.Rows)
            {
                SIT_SNT_PAIS paisMdl = new SIT_SNT_PAIS();
                parsed = Int32.TryParse(drUsu["IDPAIS"].ToString(), out iPais);

                paisMdl.paiclave = iPais;
                paisMdl.paidescripcion = drUsu["NOMBRE"].ToString();
                // paisMdl.kpa_fecha_baja = NULL;
                lstDatos.Add(paisMdl);
            }
            return lstDatos;
        }

        public List<SIT_SNT_ESTADO> dmlSelectEstado( )
        {
            Int32 iArea;
            bool parsed;

            List<SIT_SNT_ESTADO> lstDatos = new List<SIT_SNT_ESTADO>();
            String sqlQuery = "SELECT CLAEST, NOMBRE FROM ESTADO";
            DataTable dtEstado = ConsultaDML(sqlQuery);

            SIT_SNT_ESTADO iniEstadoMdl = new SIT_SNT_ESTADO();
            iniEstadoMdl.edoclave = 0;
            iniEstadoMdl.paiclave = 1;
            iniEstadoMdl.edodescripcion = "N/A";
            lstDatos.Add(iniEstadoMdl);

            foreach (DataRow drUsu in dtEstado.Rows)
            {
                SIT_SNT_ESTADO estadoMdl = new SIT_SNT_ESTADO();
                parsed = Int32.TryParse(drUsu["CLAEST"].ToString(), out iArea);
                estadoMdl.edoclave = iArea;

                estadoMdl.paiclave = 131; // MEXICO
                estadoMdl.edodescripcion = drUsu["NOMBRE"].ToString();
                lstDatos.Add(estadoMdl);
            }

            return lstDatos;
        }

        public List<SIT_SNT_MUNICIPIO> dmlSelectMunicipio( )
        {
            List<SIT_SNT_MUNICIPIO> lstDatos = new List<SIT_SNT_MUNICIPIO>();
            String sqlQuery = "SELECT CLAMUN, NOMBRE FROM MUNICIPIO WHERE CLAMUN not in (0,33,33000, 99999)";
            DataTable dtMunicipio = ConsultaDML(sqlQuery);

            SIT_SNT_MUNICIPIO MunicipioMdl = new SIT_SNT_MUNICIPIO();
            MunicipioMdl.munclave = 0;
            MunicipioMdl.edoclave = 33;
            MunicipioMdl.paiclave = 131;
            MunicipioMdl.mundescripcion = "N/A";
            lstDatos.Add(MunicipioMdl);

            MunicipioMdl = new SIT_SNT_MUNICIPIO();
            MunicipioMdl.munclave = 999;
            MunicipioMdl.edoclave = 99;
            MunicipioMdl.paiclave = 131;
            MunicipioMdl.mundescripcion = "No especificado";
            lstDatos.Add(MunicipioMdl);

            for (int iIdx = 1; iIdx < 34; iIdx++)
            {
                MunicipioMdl = new SIT_SNT_MUNICIPIO();
                MunicipioMdl.munclave = iIdx * 1000;
                MunicipioMdl.edoclave = iIdx;
                MunicipioMdl.paiclave = 131;
                MunicipioMdl.mundescripcion = "No especificado";
                lstDatos.Add(MunicipioMdl);
            }

            MunicipioMdl = new SIT_SNT_MUNICIPIO();
            MunicipioMdl.munclave = 99000;
            MunicipioMdl.edoclave = 99;
            MunicipioMdl.paiclave = 131;
            MunicipioMdl.mundescripcion = "No especificado";
            lstDatos.Add(MunicipioMdl);

            //            SntMunicipioDto dtoDatos = new SntMunicipioDto( 0, 33 ,"EXTRANJERO",  null);
            Int32 iMunicipio;
            bool parsed;

            foreach (DataRow drMun in dtMunicipio.Rows)
            {
                MunicipioMdl = new SIT_SNT_MUNICIPIO();
                parsed = Int32.TryParse(drMun["CLAMUN"].ToString(), out iMunicipio);

                MunicipioMdl.munclave = iMunicipio;
                MunicipioMdl.edoclave = iMunicipio / 1000;
                MunicipioMdl.paiclave = 131;
                MunicipioMdl.mundescripcion = drMun["NOMBRE"].ToString();
                lstDatos.Add(MunicipioMdl);
            }
            return lstDatos;
        }

        public List<SIT_SOL_MEDIOENTRADA> dmlSelectMedioEntrada( )
        {
            Int32 iMedioEntrada;
            bool parsed;

            List<SIT_SOL_MEDIOENTRADA> lstDatos = new List<SIT_SOL_MEDIOENTRADA>();
            String sqlQuery = "SELECT IDMEDIOENTRADA, NOMBRE from MEDIOENTRADA";
            DataTable dtMedioEntrada = ConsultaDML(sqlQuery);

            foreach (DataRow drDatos in dtMedioEntrada.Rows)
            {
                parsed = Int32.TryParse(drDatos["IDMEDIOENTRADA"].ToString(), out iMedioEntrada);

                SIT_SOL_MEDIOENTRADA tipoMedioEntrada = new SIT_SOL_MEDIOENTRADA();
                tipoMedioEntrada.metclave = iMedioEntrada;
                tipoMedioEntrada.metdescripcion = drDatos["NOMBRE"].ToString();
                lstDatos.Add(tipoMedioEntrada);
            }
            return lstDatos;
        }

        //////private List<SolUEnlaceMdl> dmlSelectUnidadEnlace( )
        //////{
        //////    List<SolUEnlaceMdl> lstDatos = new List<SolUEnlaceMdl>();
        //////    String sqlQuery ="SELECT CLADEP, NOMBRE from DEPENDENCIA";
        //////    int iClaEnlace = 0;
        //////    bool parsed;

        //////    DataTable dtUniEnl = ConsultaDML(sqlQuery);
        //////    foreach (DataRow drDatos in dtUniEnl.Rows)
        //////    {
        //////        parsed = Int32.TryParse(drDatos["CLADEP"].ToString(), out iClaEnlace);

        //////        SolUEnlaceMdl uEnlaceMdl = new SolUEnlaceMdl( Convert.ToInt32(drDatos["CLADEP"].ToString()), drDatos["NOMBRE"].ToString());
        //////        lstDatos.Add(uEnlaceMdl);
        //////    }

        //////    lstDatos.Add(new SolUEnlaceMdl( 27001,"INSTITUTO DE ADMINISTRACIÓN Y AVALÚOS DE BIENES NACIONALES"));            
        //////    return lstDatos;
        //////}

        public List<SIT_SNT_OCUPACION> dmlSelectOcupacion( )
        {
            List<SIT_SNT_OCUPACION> lstDatos = new List<SIT_SNT_OCUPACION>();
            String sqlQuery = "SELECT IDOCUPACION, NOMBRE from OCUPACION";
            DataTable dtOcupacion = ConsultaDML(sqlQuery);

            int iOcupacion;
            bool parsed;

            foreach (DataRow drDatos in dtOcupacion.Rows)
            {
                SIT_SNT_OCUPACION ocupacionMdl = new SIT_SNT_OCUPACION();
                parsed = Int32.TryParse(drDatos["IDOCUPACION"].ToString(), out iOcupacion);
                ocupacionMdl.ocuclave = iOcupacion;
                ocupacionMdl.ocudescripcion = drDatos["NOMBRE"].ToString();
                lstDatos.Add(ocupacionMdl);
            }
            return lstDatos;
        }

        public List<SIT_SNT_SOLICITANTETIPO> dmlSelectTipoSolicitante( )
        {
            List<SIT_SNT_SOLICITANTETIPO> lstDatos = new List<SIT_SNT_SOLICITANTETIPO>();
            lstDatos.Add(new SIT_SNT_SOLICITANTETIPO { tslclave = 0, tsldescripcion = "Empresa" });
            lstDatos.Add(new SIT_SNT_SOLICITANTETIPO { tslclave = 1, tsldescripcion = "Ciudadano" });
            return lstDatos;
        }

        public List<SIT_SOL_SOLICITUDTIPO> dmlSelectTipoSolicitud( )
        {
            List<SIT_SOL_SOLICITUDTIPO> lstDatos = new List<SIT_SOL_SOLICITUDTIPO>();
            lstDatos.Add(new SIT_SOL_SOLICITUDTIPO { sotclave = 1, sotdescripcion = "INFORMACIÓN PÚBLICA" });
            lstDatos.Add(new SIT_SOL_SOLICITUDTIPO { sotclave = 2, sotdescripcion = "DATOS PERSONALES (ACCESO, RECTIFICACIÓN, CANCELACIÓN U OPOSICIÓN)" });
                        
            return lstDatos;
        }

        public List<SIT_SOL_PROCESO> dmlSelectProceso( )
        {
            List<SIT_SOL_PROCESO> lstDatos = new List<SIT_SOL_PROCESO>();
            lstDatos.Add(new SIT_SOL_PROCESO { prcclave = 1, prcdescripcion = "SOLICITUD" });
            lstDatos.Add(new SIT_SOL_PROCESO { prcclave = 2, prcdescripcion = "ACLARACION" });
            lstDatos.Add(new SIT_SOL_PROCESO { prcclave = 3, prcdescripcion = "RECURSO" });
            return lstDatos;
        }

        public List<SIT_SOL_PROCESOPLAZOS> dmlSelectProcesoPlazos(  )
        {
            List<SIT_SOL_PROCESOPLAZOS> lstDatos = new List<SIT_SOL_PROCESOPLAZOS>();
            // SOLICITUD (1) SOLICITUD  ,  

            //public SolProcesoPlazosMdl(int prcclave, int sotclave, int pczclave,
            //    int pczplazo, int pczverde, int pczamarillo)

            // SOLICITUD (1)  INFO
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 1, sotclave = 1, pczclave = 1, pczplazo = 20, pczverde = 9, pczamarillo = 15 });
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 1, sotclave = 1, pczclave = 2, pczplazo = 40, pczverde = 18, pczamarillo = 30 });

            // SOLICITUD (1)  DATOS PERSONALES
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 1, sotclave = 2, pczclave = 1, pczplazo = 10, pczverde = 2, pczamarillo = 3 });
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 1, sotclave = 2, pczclave = 2, pczplazo = 20, pczverde = 4, pczamarillo = 6 });

/*************************************************************/

            // ACLARACION (1) SOLICITUD  INFO
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 2, sotclave = 1, pczclave = 1, pczplazo = 20, pczverde = 9, pczamarillo = 15 });
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 2, sotclave = 1, pczclave = 2, pczplazo = 40, pczverde = 18, pczamarillo = 30 });

            // ACLARACION (2)  DATOS PERSONALES{ prcclave = 2)
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 2, sotclave = 2, pczclave = 1, pczplazo = 10, pczverde = 2, pczamarillo = 3 });
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 2, sotclave = 2, pczclave = 2, pczplazo = 20, pczverde = 4, pczamarillo = 6 });


/*************************************************************/

            // RECURSO (2) SOLICITUD ,  
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 3, sotclave = 1, pczclave = 1, pczplazo = 7, pczverde = 2, pczamarillo = 5 });
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 3, sotclave = 1, pczclave = 2, pczplazo = 14, pczverde = 4, pczamarillo = 10 });

            // RECURSO (2)  DATOS PERSONALES{ prcclave = 2)
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 3, sotclave = 2, pczclave = 1, pczplazo = 7, pczverde = 2, pczamarillo = 5 });
            lstDatos.Add(new SIT_SOL_PROCESOPLAZOS{ prcclave = 3, sotclave = 2, pczclave = 2, pczplazo = 14, pczverde = 4, pczamarillo = 10 });


            return lstDatos;
        }

        public List<SIT_SOL_MODOENTREGA> dmlSelectTipoModoEntrega( )
        {
            List<SIT_SOL_MODOENTREGA> lstDatos = new List<SIT_SOL_MODOENTREGA>();
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 0, megdescripcion = "No especificado", megmostrar = 1 });
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 1, megdescripcion = "Verbal", megmostrar = 0 });
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 2, megdescripcion = "Consulta directa", megmostrar =  0 });
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 3, megdescripcion = "Copia simple", megmostrar = 1 });
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 4, megdescripcion = "Copia certificada", megmostrar = 1 });
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 5, megdescripcion = "Archivo electrónico", megmostrar = 1 });
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 6, megdescripcion = "Otro medio", megmostrar = 1 });
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 7, megdescripcion = "Entrega por Internet en el INFOMEX", megmostrar = 1 });
            lstDatos.Add(new SIT_SOL_MODOENTREGA { megclave = 20, megdescripcion = "Ninguno", megmostrar = 1 });
            return lstDatos;
        }

        //////////private List<SolTipoRubroTematicoMdl> dmlSelectTipoRubroTematico( )
        //////////{
        //////////    List<SolTipoRubroTematicoMdl> lstDatos = new List<SolTipoRubroTematicoMdl>();

        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(0,"No asignado","","", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(1,"Auditoría","2 a 3 años","13 fracc. V y 14 fracc. VI", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(2,"Auditoría","1 a 2 años","14 fracc. VI", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(3,"Órganos Colegiados","1 año","14 fracc. VI", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(4,"Sanción a Proveedores","1 a 2 años","14 fracc. IV y VI", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(5,"Pliego de responsabilidades","1 a 2 años","14 fracc. V", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(6,"Inconformidades","1 a 2 años","14 fracc. IV y VI", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(7,"Procedimiento administrativo de responsabilidades","2 a 3 años ó 3 a 5 años **","13 fracc. V y 14 fracc. V", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(8,"Juicio de Amparo","1 a 2 años","14 fracc. IV", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(9,"Recurso de revisión","1 a 2 años","14 fracc. IV", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(10,"Recurso de revocación","1 a 2 años","14 fracc. IV", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(11,"Juicio de nulidad","3 a 5 años","14 fracc. IV", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(12,"Investigación de quejas y denuncias","1 a 2 años","14 fracc. IV y VI", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(13,"Coadyuvancia","6 a 8 años","13 fracc. V", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(14,"Conciliaciones","1 año","14 fracc. VI", new DateTime()));
        //////////    lstDatos.Add(new SolTipoRubroTematicoMdl(15,"Inconformidad en materia del Servicio Profesional de Carrera","1 año 14","fracc. IV y VI", new DateTime()));
        //////////    return lstDatos;
        //////////}

        //private List<DocTipoMdl> dmlSelectTipoDocumento( )
        //{
        //    List<DocTipoMdl> lstDatos = new List<DocTipoMdl>();
        //    lstDatos.Add(new DocTipoMdl(1,"Oficio"));
        //    lstDatos.Add(new DocTipoMdl(2,"Minuta"));
        //    return lstDatos;
        //}

        public List<SIT_ADM_DIANOLABORAL> dmlSelectDiaNoLaboral( )
        {
            List<SIT_ADM_DIANOLABORAL> lstDatos = new List<SIT_ADM_DIANOLABORAL>();
            SIT_ADM_DIANOLABORAL diaNoLaboralDto;
            DateTime calFechaInicial = new DateTime(2003, 1, 1);
            DateTime calFechaLimite = new DateTime(2025, 12, 31);
            DateTime calFechaIteracion;


            while (DateTime.Compare(calFechaInicial, calFechaLimite) < 0)
            {
                switch (calFechaInicial.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                        diaNoLaboralDto.diaclave = calFechaIteracion;
                        diaNoLaboralDto.diatipo ="S";
                        lstDatos.Add(diaNoLaboralDto);
                        break;

                    case DayOfWeek.Sunday:
                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                        diaNoLaboralDto.diaclave = calFechaIteracion;
                        diaNoLaboralDto.diatipo ="D";
                        lstDatos.Add(diaNoLaboralDto);
                        break;

                    default:

                        if (calFechaInicial.Year == 2014)
                        {
                            if (calFechaInicial.Month == 1)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                            else if (calFechaInicial.Month == 2)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 5:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                            else if (calFechaInicial.Month == 3)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 21:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                            else if (calFechaInicial.Month == 4)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 14:
                                    case 15:
                                    case 16:
                                    case 17:
                                    case 18:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                            else if (calFechaInicial.Month == 5)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 1:
                                    case 2:
                                    case 5:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                            else if (calFechaInicial.Month == 7)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 14:
                                    case 15:
                                    case 16:
                                    case 17:
                                    case 18:
                                    case 21:
                                    case 22:
                                    case 23:
                                    case 24:
                                    case 25:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                            else if (calFechaInicial.Month == 9)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 1:
                                    case 16:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                            else if (calFechaInicial.Month == 11)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 20:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                            else if (calFechaInicial.Month == 12)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 22:
                                    case 23:
                                    case 24:
                                    case 25:
                                    case 26:
                                    case 29:
                                    case 30:
                                    case 31:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                        }
                        else if (calFechaInicial.Year == 2015)
                        {
                            if (calFechaInicial.Month == 1)
                            {
                                switch (calFechaInicial.Day)
                                {
                                    case 1:
                                    case 2:
                                    case 5:
                                    case 6:
                                        diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                                        calFechaIteracion = new DateTime(calFechaInicial.Year, calFechaInicial.Month, calFechaInicial.Day);
                                        diaNoLaboralDto.diaclave = calFechaIteracion;
                                        diaNoLaboralDto.diatipo ="F";
                                        lstDatos.Add(diaNoLaboralDto);
                                        break;
                                }
                            }
                        }
                        break;
                }
                calFechaInicial = calFechaInicial.AddDays(1);
            }


            // aGREGAMOS LOS DIAS FESTIVOS DE DICEMBRE DE 2016
            for (int iIdx = 19; iIdx < 24; iIdx++)
            {
                diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                calFechaIteracion = new DateTime(2016, 12, iIdx);
                diaNoLaboralDto.diaclave = calFechaIteracion;
                diaNoLaboralDto.diatipo = "F";
                lstDatos.Add(diaNoLaboralDto);
            }

            // aGREGAMOS LOS DIAS FESTIVOS DE DICEMBRE DE 2016
            for (int iIdx = 26; iIdx < 31; iIdx++)
            {
                diaNoLaboralDto = new SIT_ADM_DIANOLABORAL();
                calFechaIteracion = new DateTime(2016, 12, iIdx);
                diaNoLaboralDto.diaclave = calFechaIteracion;
                diaNoLaboralDto.diatipo = "F";
                lstDatos.Add(diaNoLaboralDto);
            }
            // FALTA AGREGAR EL CALENDARIO DE DIAS FESTIVOS 2017
            return lstDatos;
        }

        public List<SIT_DOC_EXTENSION> dmlSelectTipoExtension( )
        {
            List<SIT_DOC_EXTENSION> lstDatos = new List<SIT_DOC_EXTENSION>();

            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 0, extterminacion = "NI", extmimetype = "No identificado" });
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 1, extterminacion = "RTF", extmimetype = "text/richtext" });
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 2, extterminacion = "ZIP", extmimetype = "application/zip"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 3, extterminacion = "JPG", extmimetype = "image/jpeg"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 4, extterminacion = "PDF", extmimetype = "application/pdf"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 5, extterminacion = "RAR", extmimetype = "application/octet-stream"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 6, extterminacion = "DOC", extmimetype = "application/msword"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 7, extterminacion = "DOT", extmimetype = "application/msword"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 8, extterminacion = "DOCX", extmimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 9, extterminacion = "DOTX", extmimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.template"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 10, extterminacion = "DOCM", extmimetype = "application/vnd.ms-word.document.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 11, extterminacion = "DOTM", extmimetype = "application/vnd.ms-word.template.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 12, extterminacion = "XLS", extmimetype = "application/vnd.ms-excel"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 13, extterminacion = "XLT", extmimetype = "application/vnd.ms-excel"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 14, extterminacion = "XLA", extmimetype = "application/vnd.ms-excel"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 15, extterminacion = "XLSX", extmimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 16, extterminacion = "XLTX", extmimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.template"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 17, extterminacion = "XLSM", extmimetype = "application/vnd.ms-excel.sheet.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 18, extterminacion = "XLTM", extmimetype = "application/vnd.ms-excel.template.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 19, extterminacion = "XLAM", extmimetype = "application/vnd.ms-excel.Addin.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 20, extterminacion = "XLSB", extmimetype = "application/vnd.ms-excel.sheet.binary.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 21, extterminacion = "PPT", extmimetype = "application/vnd.ms-powerpoint"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 22, extterminacion = "POT", extmimetype = "application/vnd.ms-powerpoint"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 23, extterminacion = "PPS", extmimetype = "application/vnd.ms-powerpoint"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 24, extterminacion = "PPA", extmimetype = "application / vnd.ms - powerpoint"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 25, extterminacion = "PPTX", extmimetype = "application/vnd.openxmlformats-officedocument.presentationml.presentation"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 26, extterminacion = "POTX", extmimetype = "application/vnd.openxmlformats-officedocument.presentationml.template"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 27, extterminacion = "PPSX", extmimetype = "application/vnd.openxmlformats-officedocument.presentationml.slideshow"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 28, extterminacion = "PPAM", extmimetype = "application/vnd.ms-powerpoint.Addin.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 29, extterminacion = "PPTM", extmimetype = "application/vnd.ms-powerpoint.presentation.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 30, extterminacion = "POTM", extmimetype = "application/vnd.ms-powerpoint.template.macroEnabled.12"});
            lstDatos.Add(new SIT_DOC_EXTENSION { extclave = 31, extterminacion = "PPSM", extmimetype = "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"});
            return lstDatos;
        }

        public String sTextoDoc(int iTipoArista)
        {
            StringBuilder sbTextoDoc = new StringBuilder();

            if (iTipoArista == 2 )
            {
                //Falta agregar el escudo nacional ???'
                sbTextoDoc.Append("<p><bold> LIC. ... </bold> </p>");
                sbTextoDoc.Append("<p><bold> Presidente del Comité de Información</bold ></p>");
                sbTextoDoc.Append("<p><bold> Presente.</bold></p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p style = 'text-align: left'> |Fecha|  </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> Me refiero a la solicitud de información No. |Folio|, turnada para su atención a esta unidad administrativa, misma que a continuación se transcribe: </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> |Solicitud| </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> Sobre el particular le informo que la solicitud d einfomración no es comptencenia de esta Unidad de Enlace.</p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p>[Al efecto, se acompaña copia simple de la constancia de baja documental respectiva.] </p>");
                sbTextoDoc.Append("<br />");
                sbTextoDoc.Append("<p> Sin otro en particular, quedo a sus órdenes. </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Atentamente, </p>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Cargo del Titular de la unidad Administrativa</p>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Nombre y Firma (autógrafa) </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> C.c.p.Titular de la Unidad de Enlace.- Presente.</p>");
                sbTextoDoc.Append("<p> Contralor Interno.- Presente </p>");
            }
            else if ( iTipoArista == 22 || iTipoArista == 23)
            {
                //Falta agregar el escudo nacional ???'
                sbTextoDoc.Append("<p><bold> LIC. ANTONIO CÁRDENAS ARROYO </bold> </p>");
                sbTextoDoc.Append("<p><bold> Presidente del Comité de Información</bold ></p>");
                sbTextoDoc.Append("<p><bold> Presente.</bold></p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p style = 'text-align: left'> |Fecha|  </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> Me refiero a la solicitud de información No. |Folio|, turnada para su atención a esta unidad administrativa, misma que a continuación se transcribe: </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> |Solicitud| </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> Sobre el particular le informo que una vez realizada la búsqueda de la información de cuenta en los archivos de esta unidad administrativa, se determinó que es inexistente, de conformidad con el artículo 46 de la Ley Federal de Transparencia y Acceso a la Información Pública Gubernamental.</p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p>[Al efecto, se acompaña copia simple de la constancia de baja documental respectiva.] </p>");
                sbTextoDoc.Append("<br />");
                sbTextoDoc.Append("<p> Sin otro en particular, quedo a sus órdenes. </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Atentamente, </p>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Cargo del Titular de la unidad Administrativa</p>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Nombre y Firma (autógrafa) </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> C.c.p.Titular de la Unidad de Enlace.- Presente.</p>");
                sbTextoDoc.Append("<p> Contralor Interno.- Presente </p>");
            }
            else if (iTipoArista == 27)
            {
                //@Titular se require genear una dato de ocnfiguracion apra que el sistem lo tome
                //@Fecha,@Folio,@Solicitud
                sbTextoDoc.Append("<p><bold> LIC.ANTONIO CÁRDENAS ARROYO </bold> </p>");
                sbTextoDoc.Append("<p><bold> Presidente del Comité de Información</bold ></p>");
                sbTextoDoc.Append("<p><bold> Presente.</bold></p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p style = 'text-align: left'> |Fecha|  </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> Me refiero a la solicitud de información No.|Folio|, turnada para su atención a esta unidad administrativa, misma que a continuación se transcribe: </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> |Solicitud| </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> Sobre el particular le informo que realizada la búsqueda de la información se localizó ésta, no obstante, la misma se encuentra reservada, por un plazo de 2 años(de 16 de octubre de 2013 al 15 de octubre de 2015), en términos de lo dispuesto en el artículo 14, fracción VI, de la Ley Federal de Transparencia y Acceso a la Información Pública Gubernamental, toda vez que se trata de un expediente que se encuentra en investigación, por lo que, la información solicitada está sujeta a un proceso deliberativo a cargo de los servidores públicos adscritos a esta área, en el que no se ha tomado una determinación definitiva, hacerla pública causaría detrimento de la propia investigación, conculcando las atribuciones para realizarla, por lo que no es posible otorgar el acceso solicitado.</p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> Sin otro en particular, quedo a sus órdenes. </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Atentamente, </p>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Cargo del Titular de la unidad Administrativa</p>");
                sbTextoDoc.Append("<p style = 'text-align: center'> Nombre y Firma (autógrafa) </p>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<br/>");
                sbTextoDoc.Append("<p> C.c.p.Titular de la Unidad de Enlace.- Presente.</p>");
                sbTextoDoc.Append("<p> Contralor Interno.- Presente </p>");                                            
            }
            return sbTextoDoc.ToString();
        }

        ////private List<RedComiteRespMdl> dmlSelectComiteResp( )
        ////{
        ////    List<RedComiteRespMdl> lstDatos = new List<RedComiteRespMdl>();
        ////    lstDatos.Add(new RedComiteRespMdl(0,"", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(1,"INFORMACION CONFIDENCIAL", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(2,"INFORMACION INEXISTENTE", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(3,"INFORMACION PARCIALMENTE CONFIDENCIAL", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(4,"INFORMACION RESERVADA", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(5,"MULTIPLE (PARCIALMENTE CONFIDENCIAL E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(6,"MULTIPLE (PARCIALMENTE CONFIDENCIAL Y RESERVADA)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(7,"MULTIPLE (PARCIALMENTE CONFIDENCIAL, RESERVADA E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(8,"MULTIPLE (PUBLICA E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(9,"MULTIPLE (PUBLICA Y PARCIALMENTE CONFIDENCIAL)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(10,"MULTIPLE (PUBLICA Y RESERVADA)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(11,"MULTIPLE (PUBLICA, PARCIALMENTE CONFIDENCIAL E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(12,"MULTIPLE (PUBLICA, PARCIALMENTE CONFIDENCIAL Y RESERVADA)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(13,"MULTIPLE (PUBLICA, PARCIALMENTE CONFIDENCIAL, RESERVADA E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(14,"MULTIPLE (PUBLICA, RESERVADA E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(15,"MULTIPLE (PUBLICA, RESERVADA, PARCIALMENTE CONFIDENCIAL E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(16,"MULTIPLE (PUBLICA,RESERVADA E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(17,"MULTIPLE (RESERVADA E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(18,"MULTIPLE (RESERVADA, PARCIALMENTE CONFIDENCIAL E INEXISTENTE)", new DateTime()));
        ////    lstDatos.Add(new RedComiteRespMdl(19,"PRORROGA", new DateTime()));
        ////    return lstDatos;
        ////}

        public List<SIT_RESP_COMITERUBRO> dmlSelectComiteRubro( )
        {
            List<SIT_RESP_COMITERUBRO> lstDatos = new List<SIT_RESP_COMITERUBRO>();

            lstDatos.Add(new SIT_RESP_COMITERUBRO { corclave = 0, cordescripcion = "", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =1, cordescripcion = "ACTA ENTREGA RECEPCIÓN", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =2, cordescripcion = "ACTUACIONES COMISARIOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =3, cordescripcion = "ACTUACIONES OIC", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =4, cordescripcion = "AGENDA SERVIDORES PUBLICOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =5, cordescripcion = "ARCHIVO HISTORICO", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =6, cordescripcion = "AUDITORIAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =7, cordescripcion = "AVALUOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =8, cordescripcion = "CAMPAÑA INFORME DE GOBIERNO", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =9, cordescripcion = "CAPACITACION", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =10, cordescripcion = "COMITES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =11, cordescripcion = "CONCILIACIONES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =12, cordescripcion = "CONTRATACIONES DE SERVIDORES PUBLICOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =13, cordescripcion = "CONTRATACIONES PUBLICAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =14, cordescripcion = "CONTRATOS DE ADQUISICIONES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =15, cordescripcion = "CONTRATOS DE ARRENDAMIENTO", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =16, cordescripcion = "CONTRATOS DE ARRENDAMIENTO AUTOMOVILES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =17, cordescripcion = "CONTRATOS DE SERVICIOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =18, cordescripcion = "CONVENIOS INTERNACIONALES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =19, cordescripcion = "CUENTAS BANCARIAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =20, cordescripcion = "DATOS DE SERVIDORES PUBLICOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =21, cordescripcion = "DATOS PERSONALES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =22, cordescripcion = "DECLARACIONES PATRIMONIALES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =23, cordescripcion = "DENUNCIAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =24, cordescripcion = "DENUNCIAS PENALES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =25, cordescripcion = "DIFUSION PAGINA DE INTERNET", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =26, cordescripcion = "DIVERSAS DISPOSICIONES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =27, cordescripcion = "DIVERSOS OFICIOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =28, cordescripcion = "ENCUESTAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =29, cordescripcion = "ESTRUCTURA ORGANICA", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =30, cordescripcion = "EVALUACION DEL DESEMPEÑO", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =31, cordescripcion = "EXPEDIENTE PERSONAL SERVIDORES PUBLICOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =32, cordescripcion = "FALLO DE LICITACIONES PUBLICAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =33, cordescripcion = "GASTOS DE ALIMENTACION", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =34, cordescripcion = "INCONFORMIDADES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =35, cordescripcion = "INFORMACION ESTADISTICA", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =36, cordescripcion = "INFORMACION ESTADISTICA DE TRAMITES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =37, cordescripcion = "INFORMACION ESTADISTICA SERVIDORES PUBLICOS DE LA APF", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =38, cordescripcion = "INMUEBLES FEDERALES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =39, cordescripcion = "INVESTIGACIONES QUEJAS Y DENUNCIAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =40, cordescripcion = "JUICIO DE NULIDAD", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =41, cordescripcion = "JUICIOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =42, cordescripcion = "LICITACIONES PUBLICAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =43, cordescripcion = "LINEA 12 DEL METRO", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =44, cordescripcion = "MANUALES DE ORGANIZACIÓN", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =45, cordescripcion = "NOM-026-STPS-2008", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =46, cordescripcion = "NOM-003-SEGOB-2002", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =47, cordescripcion = "OFICIOS DIVERSOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =48, cordescripcion = "PERFIL DE PUESTOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =49, cordescripcion = "PRESUPUESTOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =50, cordescripcion = "PROCEDIMIENTOS DE RESPONSABILIDAD ADMINISTRATIVA", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =51, cordescripcion = "PROGRAMAS DE LA SECRETARIA", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =52, cordescripcion = "PROGRAMAS FEDERALES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =53, cordescripcion = "PROVEEDORES Y CONTRATISTAS SANCIONADOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =54, cordescripcion = "PRUEBA DE DAÑO DE EXPEDIENTES CLASIFICADOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =55, cordescripcion = "RECURSOS DE REVOCACION", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =56, cordescripcion = "REFORMA ENERGÉTICA", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =57, cordescripcion = "RENUNCIA SERVIDORES PUBLICOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =58, cordescripcion = "RESPONSABILIDAD PATRIMONIAL", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =59, cordescripcion = "RESPONSABILIDADES ADMINISTRATIVAS SERVIDORES PUBLICOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =60, cordescripcion = "SERVICIO PROFESIONAL DE CARRERA", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =61, cordescripcion = "SERVICIOS DIGITALES", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =62, cordescripcion = "SISTEMAS INFORMATICOS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =63, cordescripcion = "VIAS FERREAS", corfecbaja = new DateTime() });
            lstDatos.Add(new SIT_RESP_COMITERUBRO{ corclave =64, cordescripcion = "VIATICOS Y PASAJES", corfecbaja = new DateTime() });

            return lstDatos;
        }

        public List<SIT_ADM_CONFIGURACION> dmlSelectConfiguracion( )
        {
            List<SIT_ADM_CONFIGURACION> lstDatos = new List<SIT_ADM_CONFIGURACION>();
            lstDatos.Add(new SIT_ADM_CONFIGURACION { cfgclave = 1, cfgsubclave = Constantes.CfgClavesRegistro.CLAVE_INST, cfgvalor = "27", cfgfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_CONFIGURACION { cfgclave = 2, cfgsubclave = Constantes.CfgClavesRegistro.FLUJO, cfgvalor = "2", cfgfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_CONFIGURACION { cfgclave = 3, cfgsubclave = Constantes.CfgClavesRegistro.HORA_PROCESO, cfgvalor = "3", cfgfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_CONFIGURACION { cfgclave = 4, cfgsubclave = Constantes.CfgClavesRegistro.INAI, cfgvalor = "700", cfgfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_CONFIGURACION { cfgclave = 5, cfgsubclave = Constantes.CfgClavesRegistro.UT, cfgvalor = "110", cfgfecbaja = new DateTime() });
            lstDatos.Add(new SIT_ADM_CONFIGURACION { cfgclave = 6, cfgsubclave = Constantes.CfgClavesRegistro.USR_TRANSPARENCIA, cfgvalor = "1000", cfgfecbaja = new DateTime() });
            return lstDatos;
        }


        /*  *****************************
              DATOS DEL SISTEMA ANTERIOR    
            ***************************** */

        public DataTable dmlSelectSolicitudes(object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>) oDatos;            
            int iAño = Convert.ToInt32(dicParametros[COL_AÑO_INI]);
            int iLimite = Convert.ToInt32(dicParametros[COL_AÑO_FIN]) +1;


            /* CONVERTIR TODOS LOS VALROE SUTILZIADOS A LONG DESDE LA BASE DE DATOS Y EVITAR CONVERSIONES */
            if (iAño > 0)
            {
                return ConsultaDML("SELECT NO_FOLIO from SOLICITUD "
                        +" WHERE fecha_recepcion_sisi > to_date('01-01-"+ iAño +"','dd-mm-yyyy') AND"
                        +" fecha_recepcion_sisi < to_date('01-01-"+ iLimite +"','dd-mm-yyyy') ORDER BY TO_NUMBER(NO_FOLIO)");
            }
            else
            {
                return ConsultaDML("SELECT NO_FOLIO from SOLICITUD  ORDER BY NO_FOLIO");
            }


            // return ConsultaDML("SELECT NO_FOLIO from  SOLICITUD WHERE NO_FOLIO in ('0002700009003')");
            // return  ConsultaDML("SELECT NO_FOLIO from  SOLICITUD WHERE NO_FOLIO in ('0002700000703', '0002700001103', '0002700002503', '0002700004503', '0002700006703', '0002700009003', '0002700009603', '0002700010503', '0002700012003', '0002700016603') ORDER BY NO_FOLIO");
            //return  ConsultaDML("SELECT NO_FOLIO from  SOLICITUD WHERE NO_FOLIO in ( '0002700010503', '0002700012003') ORDER BY NO_FOLIO");

            ///return  ConsultaDML("SELECT NO_FOLIO from  SOLICITUD WHERE NO_FOLIO in ('0002700008303')");
            //return  ConsultaDML("SELECT NO_FOLIO from  SOLICITUD WHERE NO_FOLIO in ('0002700027610')");
            //return ConsultaDML("SELECT NO_FOLIO from  SOLICITUD WHERE NO_FOLIO in ('0002700000103','0002700000203','0002700000303','0002700000403') ORDER BY NO_FOLIO");
            //return  ConsultaDML("SELECT NO_FOLIO from  SOLICITUD WHERE NO_FOLIO in ('0002700000103','0002700000203','0002700000303','0002700000104', '0002700000105', '0002700000106', '0002700000107') ORDER BY NO_FOLIO");
        }

        public SesaiSolicitudMdl dmlSelectSolicitud(object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;

            String sFolio = dicParametros[COL_FOLIO].ToString();
            SesaiSolicitudMdl dtoDatos = null;

            DataTable dtResultado = ConsultaDML("SELECT NO_FOLIO, metclave, FECHA_REG_SFP, FECHA_RECEPCION_SISI,"
                        +" FECHA_CAPTURA_SOLICITUD, INFO_BUSCAR, ARCH_DESC, OTRA_MODALIDAD, OTROS_DATOS, PLAZO,"
                        +" ID_MEDIO_ENTREGA, ARCH_ACLA, FECHA_ACLA, ACLARACION, TEXTO_ACLA, ID_TPO_SOLICITUD"
                        +" from  SOLICITUD WHERE NO_FOLIO = '"+ sFolio +"'");

            if (dtResultado != null)
            {
                dtoDatos = new SesaiSolicitudMdl();

                dtoDatos.no_folio = sFolio;
                dtoDatos.metclave = Convert.ToInt32( dtResultado.Rows[0]["metclave"]);
                dtoDatos.fecha_reg_sfp = ValidarFecha(dtResultado.Rows[0]["FECHA_REG_SFP"]);
                dtoDatos.fecha_recepcion_sisi = ValidarFecha(dtResultado.Rows[0]["FECHA_RECEPCION_SISI"]);
                dtoDatos.fecha_captura_solicitud = ValidarFecha(dtResultado.Rows[0]["FECHA_CAPTURA_SOLICITUD"]);
                dtoDatos.info_buscar = dtResultado.Rows[0]["INFO_BUSCAR"].ToString();
                dtoDatos.arch_desc = dtResultado.Rows[0]["ARCH_DESC"].ToString();
                dtoDatos.otra_modalidad = dtResultado.Rows[0]["OTRA_MODALIDAD"].ToString();
                dtoDatos.otros_datos = dtResultado.Rows[0]["OTROS_DATOS"].ToString();
                dtoDatos.plazo = Convert.ToInt32( dtResultado.Rows[0]["PLAZO"]);
                dtoDatos.id_medio_entrega = Convert.ToInt32( dtResultado.Rows[0]["ID_MEDIO_ENTREGA"]);
                dtoDatos.arch_acla = dtResultado.Rows[0]["ARCH_ACLA"].ToString();
                dtoDatos.fecha_acla = ValidarFecha(dtResultado.Rows[0]["FECHA_ACLA"]);
                dtoDatos.aclaracion = dtResultado.Rows[0]["ACLARACION"].ToString();
                dtoDatos.texto_acla = dtResultado.Rows[0]["TEXTO_ACLA"].ToString();
                dtoDatos.id_tpo_solicitud = Convert.ToInt32( dtResultado.Rows[0]["ID_TPO_SOLICITUD"]);
            }
            return dtoDatos;
        }

        public object dmlSelectDatos(object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            String sFolio = dicParametros[COL_FOLIO].ToString();

            SesaiDatosMdl datoMdl = null;

            DataTable dtResultado =   ConsultaDML("SELECT  NO_FOLIO, IDTIPO, CLADEP, IDPAIS, IDOCUPACION, CLAMUN, CLAEST,"
                            +" NOMBRE, RFC, APEPAT, APEMAT, CURP, CALLE, NUMEXT, NUMINT,"
                            +" COLONIA, TELEFONO, EMAIL, CODPOS, SEXO, FECHA_NAC, RAZONSOLCIAL, REPLEGAL"
                            +" from  DATOS_GENERALES WHERE NO_FOLIO = '"+ sFolio +"'");

            if (dtResultado != null) {
                datoMdl = new SesaiDatosMdl();

                datoMdl.no_folio = dtResultado.Rows[0]["NO_FOLIO"].ToString();
                datoMdl.idtipo = Convert.ToInt32(dtResultado.Rows[0]["IDTIPO"]);
                datoMdl.cladep = Convert.ToInt32(dtResultado.Rows[0]["CLADEP"]);
                datoMdl.idpais = Convert.ToInt32(dtResultado.Rows[0]["IDPAIS"]);
                datoMdl.idocupacion = Convert.ToInt32(dtResultado.Rows[0]["IDOCUPACION"]);
                datoMdl.clamun = Convert.ToInt32(dtResultado.Rows[0]["CLAMUN"]);
                datoMdl.claest = Convert.ToInt32(dtResultado.Rows[0]["CLAEST"]);
                datoMdl.nombre = dtResultado.Rows[0]["NOMBRE"].ToString();
                datoMdl.rfc = dtResultado.Rows[0]["RFC"].ToString();
                datoMdl.apepat = dtResultado.Rows[0]["APEPAT"].ToString();
                datoMdl.apemat = dtResultado.Rows[0]["APEMAT"].ToString();
                datoMdl.curp = dtResultado.Rows[0]["CURP"].ToString();
                datoMdl.calle = dtResultado.Rows[0]["CALLE"].ToString();
                datoMdl.numext = dtResultado.Rows[0]["NUMEXT"].ToString();
                datoMdl.numint = dtResultado.Rows[0]["NUMINT"].ToString();
                datoMdl.colonia = dtResultado.Rows[0]["COLONIA"].ToString();
                datoMdl.telefono = dtResultado.Rows[0]["TELEFONO"].ToString();
                datoMdl.email = dtResultado.Rows[0]["EMAIL"].ToString();
                datoMdl.codpos = dtResultado.Rows[0]["CODPOS"].ToString();
                datoMdl.sexo = dtResultado.Rows[0]["SEXO"].ToString();
                datoMdl.fecha_nac = ValidarFecha(dtResultado.Rows[0]["FECHA_NAC"]);
                datoMdl.razonsocial = dtResultado.Rows[0]["RAZONSOLCIAL"].ToString();
                datoMdl.replegal = dtResultado.Rows[0]["REPLEGAL"].ToString();
            }

            return datoMdl;
        }

        public Dictionary<int, SesaiTurnadaMdl> dmlSelectTurnada(object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            String sFolio = dicParametros[COL_FOLIO].ToString();

            Dictionary<int, SesaiTurnadaMdl> lstDatos = new Dictionary<int, SesaiTurnadaMdl>();

            int iRecorre = 0;
            long lTurnoAnt = 0;

            DataTable dtResultado = ConsultaDML("SELECT NO_FOLIO, ID_TURNADA, FECHATURNO, ID_USUARIO_ORIGEN, OBSERVACIONES, INF_COMPLETA, ESTADO,"
                        +" ID_AREA_DESTINO, ID_AREA_ORIGEN, CANALIZADA, COMPETENCIA, RES_CI, ID_TURNADAANT, DESFAZADA, FECHA_CANCELADA"
                        +" from  TURNADA WHERE NO_FOLIO = '"+ sFolio +"' ORDER BY ID_TURNADA");

            if (dtResultado != null)
            {
                foreach( DataRow drTurno in dtResultado.Rows)
                {

                    // verificar la conversion de lso datos.

                    if (drTurno["ID_TURNADAANT"].ToString().Length == 0)
                        lTurnoAnt = 0;
                    else
                        lTurnoAnt = Convert.ToInt64(drTurno["ID_TURNADAANT"]);

                    SesaiTurnadaMdl dtoDatos = new SesaiTurnadaMdl();

                    dtoDatos.no_folio = Convert.ToInt64(drTurno["NO_FOLIO"]);
                    dtoDatos.id_turnada = Convert.ToInt64(drTurno["ID_TURNADA"]);
                    dtoDatos.fechaturno = ValidarFecha(drTurno["FECHATURNO"]);
                    dtoDatos.id_usuario_origen = Convert.ToInt32(drTurno["ID_USUARIO_ORIGEN"]);
                    dtoDatos.observaciones = drTurno["OBSERVACIONES"].ToString();
                    dtoDatos.inf_completa = drTurno["INF_COMPLETA"].ToString();
                    dtoDatos.estado = drTurno["ESTADO"].ToString();
                    dtoDatos.id_area_destino = Convert.ToInt32(drTurno["ID_AREA_DESTINO"]);
                    dtoDatos.id_area_origen = Convert.ToInt32(drTurno["ID_AREA_ORIGEN"]);
                    dtoDatos.canalizada = drTurno["CANALIZADA"].ToString();
                    dtoDatos.competencia = drTurno["COMPETENCIA"].ToString();
                    dtoDatos.res_ci = drTurno["RES_CI"].ToString();
                    dtoDatos.id_turnadaant = lTurnoAnt;
                    dtoDatos.desfazada = drTurno["DESFAZADA"].ToString();
                    dtoDatos.fecha_cancelada = ValidarFecha(drTurno["FECHA_CANCELADA"]); 
                    dtoDatos.indice = iRecorre;
 
                    lstDatos.Add(iRecorre, dtoDatos);
                    iRecorre++;
                }
            }
            return lstDatos;
        }

        public Dictionary<int, SesaiResolucionMdl> dmlSelectResolucion(object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            String sFolio = dicParametros[COL_FOLIO].ToString();
            Dictionary<int, SesaiResolucionMdl> lstDatos = new Dictionary<int, SesaiResolucionMdl>();

            int iRecorre = 0;
            int iValor = 0;

            DataTable dtResultado = ConsultaDML("SELECT NO_FOLIO, ID_TURNADA, solrespclave, IDFORMADENTREGA, IDTIPOINFO, FECHA, UBICACION, TAM_CANT_DIR,"
                        +" TIPOINFO, RESOLUCION, SOL_AMP_TIEMPOS, ART7, NO_OFICIO, FECHA_OFICIO, FECHA_OF_COMITE"
                        +" from  RESOLUCION WHERE NO_FOLIO = '"+ sFolio +"' ORDER BY ID_TURNADA");

            if (dtResultado != null)
            {
                foreach(DataRow drResolucion in dtResultado.Rows)
                {
                    SesaiResolucionMdl dtoDatos = new SesaiResolucionMdl();

                    dtoDatos.no_folio = drResolucion["NO_FOLIO"].ToString();
                    dtoDatos.id_turnada = Convert.ToInt64(drResolucion["ID_TURNADA"]);


                    // verificar la conversion de lso datos.
                    if (Int32.TryParse(drResolucion["IDFORMADENTREGA"].ToString(), out iValor) == true)
                        dtoDatos.idformadentrega = iValor;
                    else
                        dtoDatos.idformadentrega = 0;


                    // verificar la conversion de los datos.
                    if (Int32.TryParse(drResolucion["solrespclave"].ToString(), out iValor) == true)
                        dtoDatos.solrespclave = iValor;
                    else
                        dtoDatos.solrespclave = 0;

                    // verificar la conversion de los datos.
                    if (Int32.TryParse(drResolucion["IDTIPOINFO"].ToString(), out iValor) == true)
                        dtoDatos.idtipoinfo = iValor;
                    else
                        dtoDatos.idtipoinfo = 0;

                    dtoDatos.fecha = ValidarFecha(drResolucion["FECHA"]);
                    dtoDatos.ubicacion = drResolucion["UBICACION"].ToString();
                    dtoDatos.tam_cant_dir = drResolucion["TAM_CANT_DIR"].ToString();
                    dtoDatos.tipoinfo = drResolucion["TIPOINFO"].ToString();
                    dtoDatos.resolucion = drResolucion["RESOLUCION"].ToString(); ;
                    dtoDatos.sol_amp_tiempos = drResolucion["SOL_AMP_TIEMPOS"].ToString(); ;
                    dtoDatos.art7 = drResolucion["ART7"].ToString(); ;
                    dtoDatos.no_oficio = drResolucion["NO_OFICIO"].ToString(); ;
                    dtoDatos.fecha_oficio = ValidarFecha(drResolucion["FECHA_OFICIO"]);
                    dtoDatos.fecha_comite = ValidarFecha(drResolucion["FECHA_OF_COMITE"]);

                    lstDatos.Add(iRecorre, dtoDatos);
                    iRecorre++;
                }
            }
            return lstDatos;
        }

        public Dictionary<int, SesaiDocMdl> dmlSelectDocumento(object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            String sFolio = dicParametros[COL_FOLIO].ToString();

            Dictionary<int, SesaiDocMdl> lstDatos = new Dictionary<int, SesaiDocMdl>();

            DataTable dtResultado = ConsultaDML("SELECT NAME, MIME_TYPE, doctamaño, DAD_CHARSET, LAST_UPDATED, CONTENT_TYPE,"
                    +"CONTENT, BLOB_CONTENT, NO_FOLIO, ID_TURNADA, TIPO, EXTENCION from DOCUMENTS WHERE NO_FOLIO = '"+  sFolio +"'");

            int iRecorre = 0;
            if (dtResultado != null){   
                
                foreach (DataRow drDoc in dtResultado.Rows)
                {
                    SesaiDocMdl dtoDatos = new SesaiDocMdl();
                    dtoDatos.name = drDoc["NAME"].ToString();
                    dtoDatos.mime_type = drDoc["MIME_TYPE"].ToString();
                    dtoDatos.doctamaño = Convert.ToInt32(drDoc["doctamaño"]);
                    dtoDatos.doc_charset = drDoc["DAD_CHARSET"].ToString();
                    dtoDatos.last_update = ValidarFecha(drDoc["LAST_UPDATED"]);
                    dtoDatos.content_type = drDoc["CONTENT_TYPE"].ToString();
                    if (drDoc["CONTENT"] != null)
                        dtoDatos.content = drDoc["CONTENT"].ToString();

                    if (drDoc["BLOB_CONTENT"] != null)
                        dtoDatos.blob_content = (Byte[])drDoc["BLOB_CONTENT"];

                    dtoDatos.no_folio = drDoc["NO_FOLIO"].ToString();
                    dtoDatos.id_turnada = Convert.ToInt64(drDoc["ID_TURNADA"]);
                    dtoDatos.tipo = drDoc["TIPO"].ToString();
                    dtoDatos.extencion = drDoc["EXTENCION"].ToString();

                    lstDatos.Add(iRecorre, dtoDatos);
                    iRecorre++;
                }
            }                                  

            return lstDatos;
        }

        public Dictionary<int, SesaiResComiteMdl> dmlSelectResComite(object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            String sFolio = dicParametros[COL_FOLIO].ToString();

            Dictionary<int, SesaiResComiteMdl> lstDatos = new Dictionary<int, SesaiResComiteMdl>();

            DataTable dtResultado = ConsultaDML("SELECT ID_TURNADA, NO_FOLIO, RESUELTO, FECHA, CONF_REV, OBSERVACION, MOTIVO,"
                        +"AUT_AMP_TIEMPO, NO_OFICIO, FECHA_OFICIO from  RES_COMITE WHERE NO_FOLIO = '"+ sFolio +"' ORDER BY ID_TURNADA");

            int iRecorre = 0;
            if (dtResultado != null)
            {

                foreach (DataRow drResComite in dtResultado.Rows)
                {
                    SesaiResComiteMdl dtoDatos = new SesaiResComiteMdl();

                    dtoDatos.id_turnada = Convert.ToInt64(drResComite["id_turnada"]);
                    dtoDatos.no_folio = drResComite["no_folio"].ToString();
                    dtoDatos.resuelto = drResComite["resuelto"].ToString();
                    dtoDatos.fecha = ValidarFecha(drResComite["fecha"]);
                    dtoDatos.conf_rev = drResComite["conf_rev"].ToString();
                    dtoDatos.observacion = drResComite["observacion"].ToString();
                    dtoDatos.motivo = drResComite["MOTIVO"].ToString();
                    dtoDatos.aut_amp_tiempo = drResComite["AUT_AMP_TIEMPO"].ToString();
                    dtoDatos.no_oficio = drResComite["NO_OFICIO"].ToString();
                    dtoDatos.fecha_oficio = ValidarFecha(drResComite["FECHA_OFICIO"]);
                    // Revisar este campo falta.. el archivo...
                    dtoDatos.archivo = drResComite["FECHA_OFICIO"].ToString();

                    lstDatos.Add(iRecorre, dtoDatos);
                    iRecorre++;
                }
            }
            return lstDatos;
        }

        public Dictionary<int, SesaiRevFormaMdl>  dmlSelectResComsoc(object oDatos)
        {
            Dictionary<string, object> dicParametros = (Dictionary<string, object>)oDatos;
            String sFolio = dicParametros[COL_FOLIO].ToString();

            Dictionary<int, SesaiRevFormaMdl> lstDatos = new Dictionary<int, SesaiRevFormaMdl>();

            DataTable drResultado = ConsultaDML("SELECT ID_TURNADA, NO_FOLIO, RESUELTO,"
                        +"FECHA, CONF_REV, OBSERVACION from  RES_COMSOC WHERE NO_FOLIO = '"+ sFolio +"' ORDER BY ID_TURNADA");

            int iRecorre = 0;

            if (drResultado != null)
            {
                foreach ( DataRow drResForma in drResultado.Rows)
                {
                    SesaiRevFormaMdl dtoDatos = new SesaiRevFormaMdl();

                    dtoDatos.id_turnada = Convert.ToInt64(drResForma["id_turnada"]);
                    dtoDatos.no_folio = drResForma["NO_FOLIO"].ToString();
                    dtoDatos.resuelto = drResForma["RESUELTO"].ToString();
                    dtoDatos.fecha = ValidarFecha( drResForma["FECHA"]);
                    dtoDatos.conf_rev = drResForma["CONF_REV"].ToString();
                    dtoDatos.observacion = drResForma["OBSERVACION"].ToString();
                    lstDatos.Add(iRecorre, dtoDatos);
                    iRecorre++;
                }
            }
            return lstDatos;
        }

        public object dmlUpdateSelectMultiple( )
        {
            return EjecutaDML("UPDATE INFO_SOL_SEGUIMIENTO SET segmultiple = 1"
                +"WHERE solclave IN ("
                +"WITH Sol_Multiple AS"
                +"( select solclave, nodorigen FROM  INFO_NODO_RED having count(nodorigen) > 1 group by solclave, nodorigen order by solclave )"
                +"SELECT solclave"
                +"FROM   Sol_Multiple"
                +"GROUP BY solclave)");
        }


        // -------------------------------------------------------------------------
        // -------------------------- BUSQUEDAS ------------------------------------
        // -------------------------------------------------------------------------

        //private DataTable _dtAreas;
        //private DataTable _dtUsuarios;

        ////private String buscarCorreoExtension(int piArea)
        ////{
        ////    Int32 iArea;
        ////    bool parsed;
        ////    String sCorreo ="";
        ////    foreach (DataRow drArea in _dtAreas.Rows)
        ////    {
        ////        parsed = Int32.TryParse(drArea["ID_AREA"].ToString(), out iArea);

        ////        if (iArea == piArea)
        ////        {
        ////            sCorreo = drArea["EMAIL1"] +"-"+ drArea["EXT1"];
        ////            return sCorreo;
        ////        }
        ////    }
        ////    return sCorreo;
        ////}

        ////private String buscarAreaTipo(int piArea)
        ////{
        ////    Int32 iArea;
        ////    bool parsed;
        ////    String sAreaTipo ="";

        ////    foreach (DataRow drArea in _dtAreas.Rows)
        ////    {
        ////        parsed = Int32.TryParse(drArea["ID_AREA"].ToString(), out iArea);
        ////        if (iArea == piArea)
        ////        {
        ////            sAreaTipo = drArea["TIPO"].ToString();
        ////            return sAreaTipo;
        ////        }
        ////    }
        ////    return sAreaTipo;
        ////}

        public DateTime ValidarFecha(object oValor) {

            if (oValor != null)
                if (oValor.GetType() == typeof(DateTime))
                    return (DateTime)oValor;
                else
                    return new DateTime();
            else
                return new DateTime();
        }


    }
}





//Dictionary<int, int> dicUsuEqui = new Dictionary<int, int>();

//dicUsuEqui.Add(0, 1000);
//            dicUsuEqui.Add(1, 103);
//            dicUsuEqui.Add(2, 103);
//            dicUsuEqui.Add(3, 3);
//            dicUsuEqui.Add(4, 103);
//            dicUsuEqui.Add(5, 103);
//            dicUsuEqui.Add(6, 103);
//            dicUsuEqui.Add(7, 103);
//            dicUsuEqui.Add(8, 103);
//            dicUsuEqui.Add(9, 103);
//            dicUsuEqui.Add(10, 103);
//            dicUsuEqui.Add(91, 92);
//            dicUsuEqui.Add(92, 92);
//            dicUsuEqui.Add(93, 93);
//            dicUsuEqui.Add(94, 92);
//            dicUsuEqui.Add(100, 103);
//            dicUsuEqui.Add(101, 101);
//            dicUsuEqui.Add(102, 102);
//            dicUsuEqui.Add(103, 103);
//            dicUsuEqui.Add(104, 104);
//            dicUsuEqui.Add(105, 105);
//            dicUsuEqui.Add(106, 106);
//            dicUsuEqui.Add(107, 107);
//            dicUsuEqui.Add(108, 108);
//            dicUsuEqui.Add(109, 109);
//            dicUsuEqui.Add(110, 103);
//            dicUsuEqui.Add(111, 111);
//            dicUsuEqui.Add(112, 112);
//            dicUsuEqui.Add(113, 113);
//            dicUsuEqui.Add(114, 1004);
//            dicUsuEqui.Add(150, 150);
//            dicUsuEqui.Add(200, 200);
//            dicUsuEqui.Add(201, 201);
//            dicUsuEqui.Add(240, 240);
//            dicUsuEqui.Add(241, 241);
//            dicUsuEqui.Add(245, 1023);
//            dicUsuEqui.Add(246, 246);
//            dicUsuEqui.Add(250, 250);
//            dicUsuEqui.Add(251, 251);
//            dicUsuEqui.Add(260, 260);
//            dicUsuEqui.Add(261, 261);
//            dicUsuEqui.Add(265, 265);
//            dicUsuEqui.Add(266, 266);
//            dicUsuEqui.Add(300, 300);
//            dicUsuEqui.Add(302, 300);
//            dicUsuEqui.Add(308, 308);
//            dicUsuEqui.Add(310, 310);
//            dicUsuEqui.Add(320, 320);
//            dicUsuEqui.Add(321, 321);
//            dicUsuEqui.Add(330, 330);
//            dicUsuEqui.Add(340, 340);
//            dicUsuEqui.Add(342, 340);
//            dicUsuEqui.Add(350, 350);
//            dicUsuEqui.Add(400, 400);
//            dicUsuEqui.Add(401, 1024);
//            dicUsuEqui.Add(402, 402);
//            dicUsuEqui.Add(405, 405);
//            dicUsuEqui.Add(407, 407);
//            dicUsuEqui.Add(409, 409);
//            dicUsuEqui.Add(410, 410);
//            dicUsuEqui.Add(411, 1031);
//            dicUsuEqui.Add(412, 412);
//            dicUsuEqui.Add(416, 416);
//            dicUsuEqui.Add(420, 412);
//            dicUsuEqui.Add(430, 103);
//            dicUsuEqui.Add(440, 440);
//            dicUsuEqui.Add(450, 103);
//            dicUsuEqui.Add(500, 500);
//            dicUsuEqui.Add(510, 511);
//            dicUsuEqui.Add(511, 511);
//            dicUsuEqui.Add(520, 520);
//            dicUsuEqui.Add(530, 530);
//            dicUsuEqui.Add(531, 530);
//            dicUsuEqui.Add(532, 530);
//            dicUsuEqui.Add(533, 530);
//            dicUsuEqui.Add(540, 1014);
//            dicUsuEqui.Add(550, 1008);
//            dicUsuEqui.Add(560, 1017);
//            dicUsuEqui.Add(600, 600);
//            dicUsuEqui.Add(610, 1052);
//            dicUsuEqui.Add(620, 1045);
//            dicUsuEqui.Add(630, 630);
//            dicUsuEqui.Add(640, 640);
//            dicUsuEqui.Add(650, 650);
//            dicUsuEqui.Add(660, 660);
//            dicUsuEqui.Add(666, 106);
//            dicUsuEqui.Add(667, 106);
//            dicUsuEqui.Add(670, 1056);
//            dicUsuEqui.Add(671, 1118);
//            dicUsuEqui.Add(672, 672);
//            dicUsuEqui.Add(673, 673);
//            dicUsuEqui.Add(674, 873);
//            dicUsuEqui.Add(675, 675);
//            dicUsuEqui.Add(676, 1262);
//            dicUsuEqui.Add(677, 1256);
//            dicUsuEqui.Add(678, 1266);
//            dicUsuEqui.Add(679, 1247);
//            dicUsuEqui.Add(680, 1084);
//            dicUsuEqui.Add(681, 896);
//            dicUsuEqui.Add(682, 1257);
//            dicUsuEqui.Add(683, 1117);
//            dicUsuEqui.Add(684, 684);
//            dicUsuEqui.Add(685, 685);
//            dicUsuEqui.Add(686, 686);
//            dicUsuEqui.Add(687, 1238);
//            dicUsuEqui.Add(688, 688);
//            dicUsuEqui.Add(689, 689);
//            dicUsuEqui.Add(690, 690);
//            dicUsuEqui.Add(691, 1261);
//            dicUsuEqui.Add(692, 692);
//            dicUsuEqui.Add(693, 1233);
//            dicUsuEqui.Add(695, 695);
//            dicUsuEqui.Add(696, 696);
//            dicUsuEqui.Add(697, 1202);
//            dicUsuEqui.Add(698, 698);
//            dicUsuEqui.Add(699, 699);
//            dicUsuEqui.Add(701, 1218);
//            dicUsuEqui.Add(702, 702);
//            dicUsuEqui.Add(703, 703);
//            dicUsuEqui.Add(704, 1259);
//            dicUsuEqui.Add(705, 1263);
//            dicUsuEqui.Add(706, 706);
//            dicUsuEqui.Add(708, 708);
//            dicUsuEqui.Add(710, 1264);
//            dicUsuEqui.Add(712, 1239);
//            dicUsuEqui.Add(713, 713);
//            dicUsuEqui.Add(714, 714);
//            dicUsuEqui.Add(715, 1267);
//            dicUsuEqui.Add(716, 716);
//            dicUsuEqui.Add(718, 1143);
//            dicUsuEqui.Add(720, 720);
//            dicUsuEqui.Add(722, 722);
//            dicUsuEqui.Add(725, 1077);
//            dicUsuEqui.Add(726, 1088);
//            dicUsuEqui.Add(727, 1110);
//            dicUsuEqui.Add(728, 1102);
//            dicUsuEqui.Add(729, 1115);
//            dicUsuEqui.Add(730, 1123);
//            dicUsuEqui.Add(731, 1126);
//            dicUsuEqui.Add(732, 1136);
//            dicUsuEqui.Add(733, 1138);
//            dicUsuEqui.Add(734, 734);
//            dicUsuEqui.Add(735, 735);
//            dicUsuEqui.Add(736, 735);
//            dicUsuEqui.Add(737, 1172);
//            dicUsuEqui.Add(738, 1173);
//            dicUsuEqui.Add(739, 739);
//            dicUsuEqui.Add(740, 1199);
//            dicUsuEqui.Add(741, 1207);
//            dicUsuEqui.Add(742, 742);
//            dicUsuEqui.Add(743, 1220);
//            dicUsuEqui.Add(744, 1222);
//            dicUsuEqui.Add(745, 1197);
//            dicUsuEqui.Add(746, 1233);
//            dicUsuEqui.Add(747, 735);
//            dicUsuEqui.Add(748, 748);
//            dicUsuEqui.Add(749, 749);
//            dicUsuEqui.Add(750, 750);
//            dicUsuEqui.Add(751, 776);
//            dicUsuEqui.Add(752, 752);
//            dicUsuEqui.Add(753, 753);
//            dicUsuEqui.Add(754, 1176);
//            dicUsuEqui.Add(755, 755);
//            dicUsuEqui.Add(756, 1210);
//            dicUsuEqui.Add(757, 1245);
//            dicUsuEqui.Add(758, 1274);
//            dicUsuEqui.Add(800, 93);
//            dicUsuEqui.Add(801, 1002);
//            dicUsuEqui.Add(805, 93);
//            dicUsuEqui.Add(810, 810);
//            dicUsuEqui.Add(811, 811);
//            dicUsuEqui.Add(815, 815);
//            dicUsuEqui.Add(816, 815);
//            dicUsuEqui.Add(820, 820);
//            dicUsuEqui.Add(825, 820);
//            dicUsuEqui.Add(826, 826);
//            dicUsuEqui.Add(827, 826);
//            dicUsuEqui.Add(830, 1006);
//            dicUsuEqui.Add(831, 831);
//            dicUsuEqui.Add(835, 1006);
//            dicUsuEqui.Add(836, 826);
//            dicUsuEqui.Add(840, 840);
//            dicUsuEqui.Add(845, 845);
//            dicUsuEqui.Add(850, 114);
//            dicUsuEqui.Add(855, 855);
//            dicUsuEqui.Add(856, 855);
//            dicUsuEqui.Add(857, 855);
//            dicUsuEqui.Add(858, 855);
//            dicUsuEqui.Add(860, 1001);
//            dicUsuEqui.Add(861, 861);
//            dicUsuEqui.Add(865, 865);
//            dicUsuEqui.Add(870, 1003);
//            dicUsuEqui.Add(875, 875);
//            dicUsuEqui.Add(880, 1047);
//            dicUsuEqui.Add(890, 1116);
//            dicUsuEqui.Add(900, 1083);
//            dicUsuEqui.Add(901, 102);
//            dicUsuEqui.Add(905, 1058);
//            dicUsuEqui.Add(906, 1099);
//            dicUsuEqui.Add(907, 1166);
//            dicUsuEqui.Add(908, 1148);
//            dicUsuEqui.Add(909, 1195);
//            dicUsuEqui.Add(910, 1200);
//            dicUsuEqui.Add(911, 911);
//            dicUsuEqui.Add(912, 1244);
//            dicUsuEqui.Add(914, 1235);
//            dicUsuEqui.Add(950, 950);
//            dicUsuEqui.Add(999, 999);