using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_MOMENTO
	 {
	 	 public string momdescripcion  { set; get; } 
	 	 public int momclave  { set; get; } 
 
	 	 public SIT_RESP_MOMENTO () {} 
 
	 	 public SIT_RESP_MOMENTO (
	 	  string momdescripcion, int momclave
	 	 	 )
	 	 {
	 	 	 this.momdescripcion = momdescripcion;
	 	 	 this.momclave = momclave;
	 	 }
 
	 }
}
