using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_GRAL
	 {
	 	 public string gracontenido  { set; get; } 
	 	 public int? rccclave  { set; get; } 
	 	 public Int64 repclave  { set; get; } 
 
	 	 public SIT_RESP_GRAL () {} 
 
	 	 public SIT_RESP_GRAL (
	 	  string gracontenido, int? rccclave, Int64 repclave
	 	 	 )
	 	 {
	 	 	 this.gracontenido = gracontenido;
	 	 	 this.rccclave = rccclave;
	 	 	 this.repclave = repclave;
	 	 }
 
	 }
}
