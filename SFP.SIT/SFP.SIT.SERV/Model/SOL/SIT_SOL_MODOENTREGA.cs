using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SOL
{
	 public class SIT_SOL_MODOENTREGA
	 {
	 	 public int megmostrar  { set; get; } 
	 	 public string megdescripcion  { set; get; } 
	 	 public int megclave  { set; get; } 
 
	 	 public SIT_SOL_MODOENTREGA () {} 
 
	 	 public SIT_SOL_MODOENTREGA (
	 	  int megmostrar, string megdescripcion, int megclave
	 	 	 )
	 	 {
	 	 	 this.megmostrar = megmostrar;
	 	 	 this.megdescripcion = megdescripcion;
	 	 	 this.megclave = megclave;
	 	 }
 
	 }
}
