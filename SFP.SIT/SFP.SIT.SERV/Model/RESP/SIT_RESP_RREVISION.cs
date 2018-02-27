using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_RREVISION
	 {
	 	 public Int64 repclave  { set; get; } 
 
	 	 public SIT_RESP_RREVISION () {} 
 
	 	 public SIT_RESP_RREVISION (
	 	  Int64 repclave
	 	 	 )
	 	 {
	 	 	 this.repclave = repclave;
	 	 }
 
	 }
}
