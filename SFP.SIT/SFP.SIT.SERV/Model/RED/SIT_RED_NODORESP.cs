using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RED
{
	 public class SIT_RED_NODORESP
	 {
	 	 public int? sdoclave  { set; get; } 
	 	 public Int64 repclave  { set; get; } 
	 	 public Int64 nodclave  { set; get; } 
 
	 	 public SIT_RED_NODORESP () {} 
 
	 	 public SIT_RED_NODORESP (
	 	  int? sdoclave, Int64 repclave, Int64 nodclave
	 	 	 )
	 	 {
	 	 	 this.sdoclave = sdoclave;
	 	 	 this.repclave = repclave;
	 	 	 this.nodclave = nodclave;
	 	 }
 
	 }
}
