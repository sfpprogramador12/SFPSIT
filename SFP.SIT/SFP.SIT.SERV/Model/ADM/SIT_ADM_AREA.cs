using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_AREA
	 {
	 	 public DateTime arafeccreacion  { set; get; } 
	 	 public int araclave  { set; get; } 
 
	 	 public SIT_ADM_AREA () {} 
 
	 	 public SIT_ADM_AREA (
	 	  DateTime arafeccreacion, int araclave
	 	 	 )
	 	 {
	 	 	 this.arafeccreacion = arafeccreacion;
	 	 	 this.araclave = araclave;
	 	 }
 
	 }
}
