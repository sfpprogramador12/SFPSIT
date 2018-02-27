using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_AREAHIST
	 {
	 	 public int? atpclave  { set; get; } 
	 	 public int? anlclave  { set; get; } 
	 	 public int araclave  { set; get; } 
	 	 public int arhreporta  { set; get; } 
	 	 public string arhsiglas  { set; get; } 
	 	 public string arhdescripcion  { set; get; } 
	 	 public string arhclaveua  { set; get; } 
	 	 public DateTime arhfecfin  { set; get; } 
	 	 public DateTime arhfecini  { set; get; } 
 
	 	 public SIT_ADM_AREAHIST () {} 
 
	 	 public SIT_ADM_AREAHIST (
	 	  int? atpclave, int? anlclave, int araclave, int arhreporta, string arhsiglas, string arhdescripcion, string arhclaveua, DateTime arhfecfin, DateTime arhfecini
	 	 	 )
	 	 {
	 	 	 this.atpclave = atpclave;
	 	 	 this.anlclave = anlclave;
	 	 	 this.araclave = araclave;
	 	 	 this.arhreporta = arhreporta;
	 	 	 this.arhsiglas = arhsiglas;
	 	 	 this.arhdescripcion = arhdescripcion;
	 	 	 this.arhclaveua = arhclaveua;
	 	 	 this.arhfecfin = arhfecfin;
	 	 	 this.arhfecini = arhfecini;
	 	 }
 
	 }
}
