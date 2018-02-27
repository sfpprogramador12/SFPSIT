using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RED
{
	 public class SIT_RED_AFD
	 {
	 	 public string afdprefijo  { set; get; } 
	 	 public DateTime afdfecbaja  { set; get; } 
	 	 public string afddescripcion  { set; get; } 
	 	 public int afdclave  { set; get; } 
 
	 	 public SIT_RED_AFD () {} 
 
	 	 public SIT_RED_AFD (
	 	  string afdprefijo, DateTime afdfecbaja, string afddescripcion, int afdclave
	 	 	 )
	 	 {
	 	 	 this.afdprefijo = afdprefijo;
	 	 	 this.afdfecbaja = afdfecbaja;
	 	 	 this.afddescripcion = afddescripcion;
	 	 	 this.afdclave = afdclave;
	 	 }
 
	 }
}
