using System;
using System.ComponentModel.DataAnnotations;
namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolBuscarMdl : BaseMdl
    {
        public Int32 IdConsulta { get; set; }
        public string Solicitud { get; set; }
        public long FolioIni { get; set; }        
        public long FolioFin { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaIni { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
        public DateTime FechaConclusion { get; set; }
        public Int32 Periodo { get; set; }
        public Int32 TipoProceso { get; set; }
        public Int32 SolicitudEstado { get; set; }
        public Int32 TipoSolicitud { get; set; }
        public Int32 RubroTematico { get; set; }
        public string Datos { get; set; }
        public Int32 TipoPerfil { get; set; }
        public Int32 Area { get; set; }

        public SolBuscarMdl() { }
    }
}
