using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_DIANOLABORAL
	 {
	 	 public string diatipo  { set; get; } 
	 	 public DateTime diaclave  { set; get; } 
 
	 	 public SIT_ADM_DIANOLABORAL () {} 
 
	 	 public SIT_ADM_DIANOLABORAL (
	 	  string diatipo, DateTime diaclave
	 	 	 )
	 	 {
	 	 	 this.diatipo = diatipo;
	 	 	 this.diaclave = diaclave;
	 	 }
 
	 }
}
