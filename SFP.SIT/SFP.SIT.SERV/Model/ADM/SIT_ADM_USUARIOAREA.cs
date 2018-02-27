using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_USUARIOAREA
	 {
	 	 public int usrclave  { set; get; } 
	 	 public int uarorigen  { set; get; } 
	 	 public int araclave  { set; get; } 
 
	 	 public SIT_ADM_USUARIOAREA () {} 
 
	 	 public SIT_ADM_USUARIOAREA (
	 	  int usrclave, int uarorigen, int araclave
	 	 	 )
	 	 {
	 	 	 this.usrclave = usrclave;
	 	 	 this.uarorigen = uarorigen;
	 	 	 this.araclave = araclave;
	 	 }
 
	 }
}
