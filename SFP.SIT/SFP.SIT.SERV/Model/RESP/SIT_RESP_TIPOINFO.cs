using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_TIPOINFO
	 {
	 	 public int nfoclave  { set; get; } 
	 	 public int rtpclave  { set; get; } 
 
	 	 public SIT_RESP_TIPOINFO () {} 
 
	 	 public SIT_RESP_TIPOINFO (
	 	  int nfoclave, int rtpclave
	 	 	 )
	 	 {
	 	 	 this.nfoclave = nfoclave;
	 	 	 this.rtpclave = rtpclave;
	 	 }
 
	 }
}
