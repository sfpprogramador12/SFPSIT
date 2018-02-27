using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SNT
{
	 public class SIT_SNT_SOLICITANTETIPO
	 {
	 	 public string tsldescripcion  { set; get; } 
	 	 public int tslclave  { set; get; } 
 
	 	 public SIT_SNT_SOLICITANTETIPO () {} 
 
	 	 public SIT_SNT_SOLICITANTETIPO (
	 	  string tsldescripcion, int tslclave
	 	 	 )
	 	 {
	 	 	 this.tsldescripcion = tsldescripcion;
	 	 	 this.tslclave = tslclave;
	 	 }
 
	 }
}
