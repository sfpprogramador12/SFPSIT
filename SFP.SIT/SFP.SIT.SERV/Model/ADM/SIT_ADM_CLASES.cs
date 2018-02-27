using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_CLASES
	 {
	 	 public string clanombre  { set; get; } 
	 	 public string cladescripcion  { set; get; } 
	 	 public int claclave  { set; get; } 
 
	 	 public SIT_ADM_CLASES () {} 
 
	 	 public SIT_ADM_CLASES (
	 	  string clanombre, string cladescripcion, int claclave
	 	 	 )
	 	 {
	 	 	 this.clanombre = clanombre;
	 	 	 this.cladescripcion = cladescripcion;
	 	 	 this.claclave = claclave;
	 	 }
 
	 }
}
