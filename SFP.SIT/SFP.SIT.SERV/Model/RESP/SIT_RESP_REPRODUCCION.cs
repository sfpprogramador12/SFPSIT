using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_REPRODUCCION
	 {
	 	 public string rccdescripcion  { set; get; } 
	 	 public int rccclave  { set; get; } 
 
	 	 public SIT_RESP_REPRODUCCION () {} 
 
	 	 public SIT_RESP_REPRODUCCION (
	 	  string rccdescripcion, int rccclave
	 	 	 )
	 	 {
	 	 	 this.rccdescripcion = rccdescripcion;
	 	 	 this.rccclave = rccclave;
	 	 }
 
	 }
}
