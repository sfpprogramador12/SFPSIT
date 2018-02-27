using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_AREATIPO
	 {
	 	 public string atpdescripcion  { set; get; } 
	 	 public int atpclave  { set; get; } 
 
	 	 public SIT_ADM_AREATIPO () {} 
 
	 	 public SIT_ADM_AREATIPO (
	 	  string atpdescripcion, int atpclave
	 	 	 )
	 	 {
	 	 	 this.atpdescripcion = atpdescripcion;
	 	 	 this.atpclave = atpclave;
	 	 }
 
	 }
}
