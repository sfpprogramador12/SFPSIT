using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_RESERVA
	 {
	 	 public int? sdoclave  { set; get; } 
	 	 public int rsvdias  { set; get; } 
	 	 public int rsvmeses  { set; get; } 
	 	 public int rsvaños  { set; get; } 
	 	 public string rsvexpediente  { set; get; } 
	 	 public Int64 repclave  { set; get; } 
	 	 public int? momclave  { set; get; } 
	 	 public int? temclave  { set; get; } 
	 	 public int? araclave  { set; get; } 
	 	 public int rsvtipoclasif  { set; get; } 
	 	 public int rsvplazo  { set; get; } 
	 	 public DateTime rsvfecfin  { set; get; } 
	 	 public DateTime rsvfecini  { set; get; } 
	 	 public int rsvtiporeserva  { set; get; } 
 
	 	 public SIT_RESP_RESERVA () {} 
 
	 	 public SIT_RESP_RESERVA (
	 	  int? sdoclave, int rsvdias, int rsvmeses, int rsvaños, string rsvexpediente, Int64 repclave, int? momclave, int? temclave, int? araclave, int rsvtipoclasif, int rsvplazo, DateTime rsvfecfin, DateTime rsvfecini, int rsvtiporeserva
	 	 	 )
	 	 {
	 	 	 this.sdoclave = sdoclave;
	 	 	 this.rsvdias = rsvdias;
	 	 	 this.rsvmeses = rsvmeses;
	 	 	 this.rsvaños = rsvaños;
	 	 	 this.rsvexpediente = rsvexpediente;
	 	 	 this.repclave = repclave;
	 	 	 this.momclave = momclave;
	 	 	 this.temclave = temclave;
	 	 	 this.araclave = araclave;
	 	 	 this.rsvtipoclasif = rsvtipoclasif;
	 	 	 this.rsvplazo = rsvplazo;
	 	 	 this.rsvfecfin = rsvfecfin;
	 	 	 this.rsvfecini = rsvfecini;
	 	 	 this.rsvtiporeserva = rsvtiporeserva;
	 	 }
 
	 }
}
