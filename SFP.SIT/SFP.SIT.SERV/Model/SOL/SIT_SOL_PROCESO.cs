using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SOL
{
	 public class SIT_SOL_PROCESO
	 {
	 	 public string prcdescripcion  { set; get; } 
	 	 public int prcclave  { set; get; } 
 
	 	 public SIT_SOL_PROCESO () {} 
 
	 	 public SIT_SOL_PROCESO (
	 	  string prcdescripcion, int prcclave
	 	 	 )
	 	 {
	 	 	 this.prcdescripcion = prcdescripcion;
	 	 	 this.prcclave = prcclave;
	 	 }
 
	 }
}
