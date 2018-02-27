using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_COMITERUBRO
	 {
	 	 public DateTime corfecbaja  { set; get; } 
	 	 public string cordescripcion  { set; get; } 
	 	 public int corclave  { set; get; } 
 
	 	 public SIT_RESP_COMITERUBRO () {} 
 
	 	 public SIT_RESP_COMITERUBRO (
	 	  DateTime corfecbaja, string cordescripcion, int corclave
	 	 	 )
	 	 {
	 	 	 this.corfecbaja = corfecbaja;
	 	 	 this.cordescripcion = cordescripcion;
	 	 	 this.corclave = corclave;
	 	 }
 
	 }
}
