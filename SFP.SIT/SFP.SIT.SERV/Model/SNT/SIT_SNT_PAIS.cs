using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SNT
{
	 public class SIT_SNT_PAIS
	 {
	 	 public DateTime paifecbaja  { set; get; } 
	 	 public string paidescripcion  { set; get; } 
	 	 public Int64 paiclave  { set; get; } 
 
	 	 public SIT_SNT_PAIS () {} 
 
	 	 public SIT_SNT_PAIS (
	 	  DateTime paifecbaja, string paidescripcion, Int64 paiclave
	 	 	 )
	 	 {
	 	 	 this.paifecbaja = paifecbaja;
	 	 	 this.paidescripcion = paidescripcion;
	 	 	 this.paiclave = paiclave;
	 	 }
 
	 }
}
