using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_INEXISTENCIA
	 {
	 	 public string inxcargo  { set; get; } 
	 	 public Int64 repclave  { set; get; } 
	 	 public string inxresponsable  { set; get; } 
 
	 	 public SIT_RESP_INEXISTENCIA () {} 
 
	 	 public SIT_RESP_INEXISTENCIA (
	 	  string inxcargo, Int64 repclave, string inxresponsable
	 	 	 )
	 	 {
	 	 	 this.inxcargo = inxcargo;
	 	 	 this.repclave = repclave;
	 	 	 this.inxresponsable = inxresponsable;
	 	 }
 
	 }
}
