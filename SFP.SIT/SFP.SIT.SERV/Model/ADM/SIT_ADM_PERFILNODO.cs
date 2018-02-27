using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_PERFILNODO
	 {
	 	 public int perclave  { set; get; } 
	 	 public int nedclave  { set; get; } 
 
	 	 public SIT_ADM_PERFILNODO () {} 
 
	 	 public SIT_ADM_PERFILNODO (
	 	  int perclave, int nedclave
	 	 	 )
	 	 {
	 	 	 this.perclave = perclave;
	 	 	 this.nedclave = nedclave;
	 	 }
 
	 }
}
