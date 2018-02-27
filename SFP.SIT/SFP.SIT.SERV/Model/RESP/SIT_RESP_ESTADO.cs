using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_ESTADO
	 {
	 	 public string sdodescripcion  { set; get; } 
	 	 public int sdoclave  { set; get; } 
 
	 	 public SIT_RESP_ESTADO () {} 
 
	 	 public SIT_RESP_ESTADO (
	 	  string sdodescripcion, int sdoclave
	 	 	 )
	 	 {
	 	 	 this.sdodescripcion = sdodescripcion;
	 	 	 this.sdoclave = sdoclave;
	 	 }
 
	 }
}
