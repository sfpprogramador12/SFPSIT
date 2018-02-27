using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_TURNAR
	 {
	 	 public int araclave  { set; get; } 
	 	 public int usrclave  { set; get; } 
	 	 public string turinstruccion  { set; get; } 
	 	 public Int64 repclave  { set; get; } 
 
	 	 public SIT_RESP_TURNAR () {} 
 
	 	 public SIT_RESP_TURNAR (
	 	  int araclave, int usrclave, string turinstruccion, Int64 repclave
	 	 	 )
	 	 {
	 	 	 this.araclave = araclave;
	 	 	 this.usrclave = usrclave;
	 	 	 this.turinstruccion = turinstruccion;
	 	 	 this.repclave = repclave;
	 	 }
 
	 }
}
