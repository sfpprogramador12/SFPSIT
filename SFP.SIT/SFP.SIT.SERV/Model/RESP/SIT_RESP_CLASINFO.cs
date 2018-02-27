using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_CLASINFO
	 {
	 	 public string nfodescripcion  { set; get; } 
	 	 public int nfoclave  { set; get; } 
 
	 	 public SIT_RESP_CLASINFO () {} 
 
	 	 public SIT_RESP_CLASINFO (
	 	  string nfodescripcion, int nfoclave
	 	 	 )
	 	 {
	 	 	 this.nfodescripcion = nfodescripcion;
	 	 	 this.nfoclave = nfoclave;
	 	 }
 
	 }
}
