using Newtonsoft.Json;

namespace SFP.SIT.WEB.Models
{
    [JsonObject]
    public class FrmTurnarAreaMdl
    {
        
        [JsonProperty("recid")]
        public int recid { get; set; }

        [JsonProperty("AreasID")]
        public string areasID { get; set; }

        [JsonProperty("instruccion")]
        public string instruccion { get; set; }

        public FrmTurnarAreaMdl(string areasID, string instruccion)
        {
            this.areasID = areasID;
            this.instruccion = instruccion;
        }
    }
}


