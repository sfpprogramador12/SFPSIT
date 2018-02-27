using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_PERFILCLASES
	 {
	 	 public int perclave  { set; get; } 
	 	 public int claclave  { set; get; } 
 
	 	 public SIT_ADM_PERFILCLASES () {} 
 
	 	 public SIT_ADM_PERFILCLASES (
	 	  int perclave, int claclave
	 	 	 )
	 	 {
	 	 	 this.perclave = perclave;
	 	 	 this.claclave = claclave;
	 	 }
 
	 }
}
