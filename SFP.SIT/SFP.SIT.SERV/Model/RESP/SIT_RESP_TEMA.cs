using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_TEMA
	 {
	 	 public int? araclave  { set; get; } 
	 	 public string temdescripcion  { set; get; } 
	 	 public int temclave  { set; get; } 
 
	 	 public SIT_RESP_TEMA () {} 
 
	 	 public SIT_RESP_TEMA (
	 	  int? araclave, string temdescripcion, int temclave
	 	 	 )
	 	 {
	 	 	 this.araclave = araclave;
	 	 	 this.temdescripcion = temdescripcion;
	 	 	 this.temclave = temclave;
	 	 }
 
	 }
}
