using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_USRPERFIL
	 {
	 	 public int usrclave  { set; get; } 
	 	 public int perclave  { set; get; } 
 
	 	 public SIT_ADM_USRPERFIL () {} 
 
	 	 public SIT_ADM_USRPERFIL (
	 	  int usrclave, int perclave
	 	 	 )
	 	 {
	 	 	 this.usrclave = usrclave;
	 	 	 this.perclave = perclave;
	 	 }
 
	 }
}
