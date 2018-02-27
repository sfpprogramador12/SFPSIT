using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_PERFILMOD
	 {
	 	 public int perclave  { set; get; } 
	 	 public int modclave  { set; get; } 
 
	 	 public SIT_ADM_PERFILMOD () {} 
 
	 	 public SIT_ADM_PERFILMOD (
	 	  int perclave, int modclave
	 	 	 )
	 	 {
	 	 	 this.perclave = perclave;
	 	 	 this.modclave = modclave;
	 	 }
 
	 }
}
