using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_SUBTIPO
	 {
	 	 public string rsbdescripcion  { set; get; } 
	 	 public int rsbclave  { set; get; } 
 
	 	 public SIT_RESP_SUBTIPO () {} 
 
	 	 public SIT_RESP_SUBTIPO (
	 	  string rsbdescripcion, int rsbclave
	 	 	 )
	 	 {
	 	 	 this.rsbdescripcion = rsbdescripcion;
	 	 	 this.rsbclave = rsbclave;
	 	 }
 
	 }
}
