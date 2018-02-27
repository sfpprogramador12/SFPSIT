namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedTipoInfoMdl
    {
        public int tpi_clatipo_info { get; set; }
        public string tpi_descripcion { get; set; }

        public RedTipoInfoMdl() { }
        public RedTipoInfoMdl(int tpi_clatipo_info, string tpi_descripcion) {
            this.tpi_clatipo_info = tpi_clatipo_info;
            this.tpi_descripcion = tpi_descripcion;
        }
    }
}
