using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_USRCOMPARTIR
	 {
	 	 public int comusr  { set; get; } 
	 	 public int usrclave  { set; get; } 
 
	 	 public SIT_ADM_USRCOMPARTIR () {} 
 
	 	 public SIT_ADM_USRCOMPARTIR (
	 	  int comusr, int usrclave
	 	 	 )
	 	 {
	 	 	 this.comusr = comusr;
	 	 	 this.usrclave = usrclave;
	 	 }
 
	 }
}
