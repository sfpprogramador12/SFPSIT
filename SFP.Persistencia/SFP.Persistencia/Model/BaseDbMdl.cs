
namespace SFP.Persistencia.Model
{
    public class BaseDbMdl
    {
        public string connectionString { get; set; }
        public string objDbConnection { get; set; }
        public string objDbTransaccion { get; set; }
        public string objDbDataAdapter { get; set; }
        public BaseDbMdl() { }
    }
}
