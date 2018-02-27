using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SOL
{
	 public class SIT_SOL_MEDIOENTRADA
	 {
	 	 public DateTime metfecbaja  { set; get; } 
	 	 public string metdescripcion  { set; get; } 
	 	 public int metclave  { set; get; } 
 
	 	 public SIT_SOL_MEDIOENTRADA () {} 
 
	 	 public SIT_SOL_MEDIOENTRADA (
	 	  DateTime metfecbaja, string metdescripcion, int metclave
	 	 	 )
	 	 {
	 	 	 this.metfecbaja = metfecbaja;
	 	 	 this.metdescripcion = metdescripcion;
	 	 	 this.metclave = metclave;
	 	 }
 
	 }
}
