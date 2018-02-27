using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_DETALLE
	 {
	 	 public Int64? docclave  { set; get; } 
	 	 public Int64 repclave  { set; get; } 
	 	 public string detdescripcion  { set; get; } 
	 	 public string detclave  { set; get; } 
 
	 	 public SIT_RESP_DETALLE () {} 
 
	 	 public SIT_RESP_DETALLE (
	 	  Int64? docclave, Int64 repclave, string detdescripcion, string detclave
	 	 	 )
	 	 {
	 	 	 this.docclave = docclave;
	 	 	 this.repclave = repclave;
	 	 	 this.detdescripcion = detdescripcion;
	 	 	 this.detclave = detclave;
	 	 }
 
	 }
}
