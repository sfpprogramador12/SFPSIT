using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.WEB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Util
{
    public static class JsonTransform
    {
        public const string NO_SESSION = "{ \"records\":[ {\"Session\":\"NO EXISTE\"} ] }";

        public const string NO_RECORDS = "{ \"records\":[]}";

        public static object convertJsonNoSession() {

            return JObject.Parse(NO_SESSION);
        }

        public static String convertJson(DataTable Datos)
        {
            string json;
            int iIdx = 1;

            if (Datos != null)
            {
                if (Datos.Columns.Contains("recid") == false)
                {
                    Datos.Columns.Add("recid", typeof(int));
                    foreach (DataRow row in Datos.Rows)
                    {
                        row["recid"] = iIdx;
                        iIdx++;
                    }
                }

                json = "{ \"records\":";
                json += JsonConvert.SerializeObject(Datos, Formatting.None, new JsonShortDateConverter() );
                json += "}";
                

                json = json.Replace("ID\"", "id\"").Replace("TEXT\"", "text\"");
            }
            else
            {
                json = NO_RECORDS;
            }

            return json;
        }
        public static String convertJsonNoRecID(DataTable Datos)
        {
            string json;

            if (Datos != null)
            {
                json = "{ \"records\":";
                json += JsonConvert.SerializeObject(Datos, Formatting.None, new JsonShortDateConverter());
                json += "}";

                json = json.Replace("RECID\"", "recid\"");
            }
            else
            {
                json = NO_RECORDS;
            }

            return json;
        }

        public static String convertJson(DataTable Datos, Int32 iLimiteInf)
        {
            DataTable table = new DataTable();
            int iIdx = 0;
            table.Columns.Add("recid", typeof(int));
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("text", typeof(string));

            if (Datos != null)
            {
                var rows = Datos.Select("id >" + iLimiteInf);
                foreach (var row in rows)
                {
                    iIdx++;
                    table.Rows.Add(iIdx, row[0], row[1]);
                }
            }

            string json = "{ \"records\":";
            json += JsonConvert.SerializeObject(table, Formatting.None, new JsonShortDateConverter() );
            json += "}";
            return json;
        }

        public static String convertJsonW2UI(DataTable Datos)
        {
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("text", typeof(string));

            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    table.Rows.Add(row[0], row[1]);
                }
            }
                         
             var jo = JObject.FromObject(table, new JsonSerializer() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            string json = "{ \"records\":";
            json += JsonConvert.SerializeObject(jo, Formatting.None, new JsonShortDateConverter());
            json += "}";
            return json;
        }

        public static String convertJson(List<Object> Datos)
        {
            string json = "{ \"records\":";
            json += JsonConvert.SerializeObject(Datos, Formatting.None, new JsonShortDateConverter() );
            json += "}";
            return json;
        }

        public static String convertJson(Object Datos)
        {
            string json = NO_RECORDS;

            if (Datos != null)
            {
                json = "{ \"records\":";
                json += JsonConvert.SerializeObject(Datos, Formatting.None, new JsonShortDateConverter());
                json += "}";

                json = json.Replace("ID", "id").Replace("TEXT", "text");
            }



            return json;
        }

        public static String convertJsonDicToTable(Object Datos)
        {
            string json;

            if (Datos != null)
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(int));
                table.Columns.Add("text", typeof(string));
                Dictionary<Int32, string> dicDatos = (Dictionary<Int32, string>)Datos;

                foreach (KeyValuePair<Int32, string> entry in dicDatos)
                {
                    table.Rows.Add(entry.Key, entry.Value);
                }

                json = "{ \"records\":";
                json += JsonConvert.SerializeObject(table, Formatting.None, new JsonShortDateConverter() );
                json += "}";
            }
            else
                json = NO_RECORDS;

            return json;
        }

        public static String convertJsonCatalogos(Object Datos)
        {
            DataTable table = new DataTable();
            table.Columns.Add("recid", typeof(int));
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("text", typeof(string));
            Dictionary<string, string> dicDatos = (Dictionary<string, string>)Datos;

            int iCuenta = 1;
            foreach (KeyValuePair<string, string> entry in dicDatos)
            {
                table.Rows.Add(iCuenta, entry.Key, entry.Value);
                iCuenta++;
            }

            string json = "{ \"records\":";
            json += JsonConvert.SerializeObject(table, Formatting.None, new JsonShortDateConverter() );
            json += "}";
            return json;
        }


        public static String convertJsonNoRecords(Object Datos)
        {
            return JsonConvert.SerializeObject(Datos, Formatting.None, new JsonShortDateConverter());
        }

        public static String convertJsonGantt(DataTable dtGantt)
        {
            if (dtGantt != null)
            {
                StringBuilder sbData = new StringBuilder();
                StringBuilder sbLink = new StringBuilder();
                DateTime fecIni;
                DateTime fecFin;
                int iIdx;

                sbData.Append(" \"data\" : [ ");
                sbLink.Append(" \"links\" : [ ");

                iIdx = 1;
                foreach (DataRow row in dtGantt.Rows)
                {
                    if (iIdx > 1)
                    {
                        sbData.Append(",");
                    }
                    sbData.Append(" { \"id\":");
                    sbData.Append(row["nodorigen"]);
                    sbData.Append(", \"text\":\"");
                    sbData.Append(row["ORIGEN"] + " " + row["rtpdescripcion"] + " " + row["DESTINO"]);
                    sbData.Append("\", \"start_date\":\"");
                    sbData.Append(row["NRE_FECINI"].ToString().Substring(8, 2) + "-" + row["NRE_FECINI"].ToString().Substring(5, 2) + "-" + row["NRE_FECINI"].ToString().Substring(0, 4));
                    sbData.Append("\", \"duration\":");

                    fecIni = new DateTime(
                            Convert.ToInt16(row["NRE_FECINI"].ToString().Substring(0, 4)),
                            Convert.ToInt16(row["NRE_FECINI"].ToString().Substring(5, 2)),
                            Convert.ToInt16(row["NRE_FECINI"].ToString().Substring(8, 2)));

                    if (row["NRE_FECFIN"] == null)
                        fecFin = new DateTime().Date;
                    else
                        fecFin = new DateTime(Convert.ToInt16(row["NRE_FECFIN"].ToString().Substring(0, 4)), Convert.ToInt16(row["NRE_FECFIN"].ToString().Substring(5, 2)), Convert.ToInt16(row["NRE_FECFIN"].ToString().Substring(8, 2)));

                    int result = DateTime.Compare(fecIni, fecFin);

                    if (result == 0)
                    {
                        sbData.Append("1");
                    }
                    else
                    {
                        sbData.Append((fecFin - fecIni).TotalDays);
                    }

                    sbData.Append("}");

                    if (iIdx > 1)
                    {
                        sbLink.Append(",");
                    }
                    sbLink.Append(" { \"id\":" + iIdx + ", \"source\":" + row["nodorigen"] + ", \"target\":" + row["noddestino"] + ", \"type\":\"1\" }");
                    iIdx++;
                }
                sbLink.Append(" ] ");
                sbData.Append(" ]");
                return " {" + sbData.ToString() + "," + sbLink.ToString() + "}";
            }
            else
            {
                return " ";
            }
        }

        public static String convertJsonAristaSeg(Dictionary<Int64, RedAristaSegMdl> dicSegArista, Dictionary<Int64, FrmSegNodoMdl> dicSegNodo, Int32[] CoordMaxXY)
        {
            if (dicSegArista.Count > 0)
            {
                StringBuilder sbSeguimiento = new StringBuilder();
                StringBuilder sbNodo = new StringBuilder();
                int iIdx;

                sbSeguimiento.Append(" \"Aristas\" : [ ");

                // PRIMERO LAS ARISTAS
                iIdx = 1;
                foreach (KeyValuePair<Int64, RedAristaSegMdl> entry in dicSegArista)
                {
                    if (iIdx > 1)
                        sbSeguimiento.Append(",");
                    iIdx++;

                    sbSeguimiento.Append(" { \"origen\":" + entry.Value.Origen + ", \"destino\":" + entry.Value.Destino + ", \"accion\":\"" + entry.Value.Accion +
                         "\", \"dias\":" + entry.Value.DiasLaborales + ", \"fecini\":\"" + entry.Value.FecIni.ToString("dd/MM/yyyy") + "\", \"fecfin\":\"" + entry.Value.FecFin.ToString("dd/MM/yyyy") +
                         "\", \"observacion\":\"" + entry.Value.Responsable.TrimEnd().Replace('\n', '\u0020').Replace('\r', '\u0020').Replace('\t', '\u0020').Replace('\"', '\u0020') +
                         "\", \"responsable\":\"" + entry.Value.Responsable + "\"}");
                    //////     "\"documento\":\"" + entry.Value.Documento + "\"}");


                }
                sbSeguimiento.Append(" ] ");


                // OBTENEMOS INFOMRACIÓN DE LOS NODOS
                iIdx = 1;
                sbNodo.Append(" \"Areas\" : [ ");
                foreach (KeyValuePair<Int64, FrmSegNodoMdl> entry in dicSegNodo)
                {
                    if (iIdx > 1)
                        sbNodo.Append(",");
                    iIdx++;
                    sbNodo.Append(" { \"id\":" + entry.Value.ID + ", \"ren\":" + entry.Value.Ren + ", \"col\":" + entry.Value.Col +
                         ", \"sigla\":\"" + entry.Value.Sigla + "\", \"tipo\":" + entry.Value.Tipo + ", \"fecha\":\"" + entry.Value.Fecha + "\", " +
                         " \"atendido\":" + entry.Value.Atendido + ", \"nodoEstado\":\"" + entry.Value.Estado + "\" }");
                }
                sbNodo.Append(" ]");

                CoordMaxXY[0]++;
                CoordMaxXY[1]++;

                String sCoordenadas = " \"Coordenadas\" : [ {\"x\":" + CoordMaxXY[0] + ",\"y\":" + CoordMaxXY[1] + "}]";

                return " {" + sCoordenadas + "," + sbSeguimiento.ToString() + "," + sbNodo.ToString() + "}";
            }
            else
                return "\"Aristas\" : [  ], \"Areas\" : [ ], \"Coordenadas\" : [] ";
        }


        // Regregasr un diccionario
        public static List<Tuple<int, string, int>> ValidarAreas(String sAreas)
        {
            List<Tuple<int, string, int>> lstAreasEnviar = new List<Tuple<int, string, int>>();
            List<FrmTurnarAreaMdl> lstTurnarArea = JsonConvert.DeserializeObject<List<FrmTurnarAreaMdl>>(sAreas);
            int iIdx = 0;

            foreach (FrmTurnarAreaMdl turAreaMdl in lstTurnarArea)
            {
                String[] asUsrPerfil = turAreaMdl.areasID.Split(',');
                foreach (String sUsrPer in asUsrPerfil)
                {
                    //Hay que obtener el área....
                    if (sUsrPer != "")
                    {
                        iIdx = sUsrPer.IndexOf("|");
                        lstAreasEnviar.Add(new Tuple<int, string, int>( Convert.ToInt32(sUsrPer.Substring(0, iIdx)), turAreaMdl.instruccion, Convert.ToInt32(sUsrPer.Substring(iIdx+1))) );
                    }                        

                }
            }

            return lstAreasEnviar;
        }

        public static string Serializar(object dtDatos)
        {
            return JsonConvert.SerializeObject(dtDatos);
            //return JsonConvert.SerializeObject(dtDatos, Formatting.Indented);
        }
    }
}
