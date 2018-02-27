using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_AREANIVEL
	 {
	 	 public string anldescripcion  { set; get; } 
	 	 public int anlclave  { set; get; } 
 
	 	 public SIT_ADM_AREANIVEL () {} 
 
	 	 public SIT_ADM_AREANIVEL (
	 	  string anldescripcion, int anlclave
	 	 	 )
	 	 {
	 	 	 this.anldescripcion = anldescripcion;
	 	 	 this.anlclave = anlclave;
	 	 }
 
	 }
}
