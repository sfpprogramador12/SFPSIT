namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedTipoAristaMdl
    {
        public int kar_clatipoari { get; set; }
        public string kar_descripcion { get; set; }
        public int kar_aplicatipoinfo { get; set; }
        public string kar_formato { get; set; }
        public int kar_respuesta { get; set; }
        public int kar_tipo { get; set; }
        public string kar_sigla { get; set; }
        public int kar_subclasificar { get; set; }
        public int kar_mostrarsub { get; set; }         
        public int kar_nivel { get; set; }
        public string kar_forma { get; set; }

        public RedTipoAristaMdl() { }
        public RedTipoAristaMdl(int kar_clatipoari, string kar_descripcion, int kar_aplicatipoinfo, string kar_formato, 
            int kar_respuesta, int kar_tipo, string kar_sigla, int kar_subclasificar, int kar_mostrarsub, string kar_forma, int kar_nivel)
        {
            this.kar_clatipoari = kar_clatipoari;
            this.kar_descripcion = kar_descripcion;
            this.kar_aplicatipoinfo = kar_aplicatipoinfo;
            this.kar_formato = kar_formato;
            this.kar_tipo = kar_tipo;
            this.kar_sigla = kar_sigla;
            this.kar_subclasificar = kar_subclasificar;
            this.kar_mostrarsub = kar_mostrarsub;
            this.kar_forma = kar_forma;
            this.kar_respuesta = kar_respuesta;
            this.kar_nivel = kar_nivel;
        }
    }
}
