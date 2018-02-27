using System;

namespace SFP.SIT.WEB.Models
{
    public class FrmSegNodoMdl
    {
        public Int64 ID;
        public Int32 Ren;
        public Int32 Col;
        public String Sigla;
        public Int32 Tipo;
        public String Fecha;
        public Int32 Atendido;
        public String Estado;

        public FrmSegNodoMdl( Int64 ID, Int32 Ren, Int32 Col, String Sigla, Int32 Tipo, String  Fecha, Int32 Atendido, String Estado)
        {
            this.ID = ID;
            this.Ren = Ren;
            this.Col = Col;
            this.Sigla = Sigla;
            this.Tipo = Tipo;
            this.Fecha = Fecha;
            this.Atendido = Atendido;
            this.Estado = Estado;
        }

    }
}
