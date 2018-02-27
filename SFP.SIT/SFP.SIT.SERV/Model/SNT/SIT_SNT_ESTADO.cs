using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SNT
{
	 public class SIT_SNT_ESTADO
	 {
	 	 public Int64? paiclave  { set; get; } 
	 	 public DateTime edofecbaja  { set; get; } 
	 	 public string edodescripcion  { set; get; } 
	 	 public Int64 edoclave  { set; get; } 
 
	 	 public SIT_SNT_ESTADO () {} 
 
	 	 public SIT_SNT_ESTADO (
	 	  Int64? paiclave, DateTime edofecbaja, string edodescripcion, Int64 edoclave
	 	 	 )
	 	 {
	 	 	 this.paiclave = paiclave;
	 	 	 this.edofecbaja = edofecbaja;
	 	 	 this.edodescripcion = edodescripcion;
	 	 	 this.edoclave = edoclave;
	 	 }
 
	 }
}
