namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedNodoEstadoMdl
    {
        public const int TIPO_INICIO = 0;
        public const int TIPO_TRANSICION = 1;
        public const int TIPO_TERMINAL = 2;

        public int kne_clanodo_edo { get; set; }
        public string kne_descripcion { get; set; }
        public int kp_claperfil { get; set; }
        public string kne_url { get; set; }
        public int kne_tipo { get; set; }

        public RedNodoEstadoMdl() { }
        public RedNodoEstadoMdl( int kne_clanodo_edo, string kne_descripcion, int kp_claperfil, string kne_url, int kne_tipo)
        {
            this.kne_clanodo_edo = kne_clanodo_edo;
            this.kne_descripcion = kne_descripcion;
            this.kp_claperfil = kp_claperfil;
            this.kne_url = kne_url;
            this.kne_tipo = kne_tipo;
        }
    }
}
