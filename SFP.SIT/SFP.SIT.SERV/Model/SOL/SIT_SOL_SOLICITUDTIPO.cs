using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SOL
{
	 public class SIT_SOL_SOLICITUDTIPO
	 {
	 	 public string sotdescripcion  { set; get; } 
	 	 public int sotclave  { set; get; } 
 
	 	 public SIT_SOL_SOLICITUDTIPO () {} 
 
	 	 public SIT_SOL_SOLICITUDTIPO (
	 	  string sotdescripcion, int sotclave
	 	 	 )
	 	 {
	 	 	 this.sotdescripcion = sotdescripcion;
	 	 	 this.sotclave = sotclave;
	 	 }
 
	 }
}
