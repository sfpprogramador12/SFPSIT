using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SNT
{
	 public class SIT_SNT_OCUPACION
	 {
	 	 public DateTime ocufecbaja  { set; get; } 
	 	 public string ocudescripcion  { set; get; } 
	 	 public int ocuclave  { set; get; } 
 
	 	 public SIT_SNT_OCUPACION () {} 
 
	 	 public SIT_SNT_OCUPACION (
	 	  DateTime ocufecbaja, string ocudescripcion, int ocuclave
	 	 	 )
	 	 {
	 	 	 this.ocufecbaja = ocufecbaja;
	 	 	 this.ocudescripcion = ocudescripcion;
	 	 	 this.ocuclave = ocuclave;
	 	 }
 
	 }
}
