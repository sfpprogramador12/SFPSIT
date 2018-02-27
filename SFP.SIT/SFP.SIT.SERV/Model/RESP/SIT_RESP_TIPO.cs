using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_TIPO
	 {
	 	 public int rtpplazo  { set; get; } 
	 	 public int rtpreporta  { set; get; } 
	 	 public int rtptipo  { set; get; } 
	 	 public string rtpformato  { set; get; } 
	 	 public string rtpforma  { set; get; } 
	 	 public string rtpdescripcion  { set; get; } 
	 	 public int rtpclave  { set; get; } 
 
	 	 public SIT_RESP_TIPO () {} 
 
	 	 public SIT_RESP_TIPO (
	 	  int rtpplazo, int rtpreporta, int rtptipo, string rtpformato, string rtpforma, string rtpdescripcion, int rtpclave
	 	 	 )
	 	 {
	 	 	 this.rtpplazo = rtpplazo;
	 	 	 this.rtpreporta = rtpreporta;
	 	 	 this.rtptipo = rtptipo;
	 	 	 this.rtpformato = rtpformato;
	 	 	 this.rtpforma = rtpforma;
	 	 	 this.rtpdescripcion = rtpdescripcion;
	 	 	 this.rtpclave = rtpclave;
	 	 }
 
	 }
}
