using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SNT
{
	 public class SIT_SNT_MUNICIPIO
	 {
	 	 public Int64? edoclave  { set; get; } 
	 	 public Int64? paiclave  { set; get; } 
	 	 public DateTime munfecbaja  { set; get; } 
	 	 public string mundescripcion  { set; get; } 
	 	 public Int64 munclave  { set; get; } 
 
	 	 public SIT_SNT_MUNICIPIO () {} 
 
	 	 public SIT_SNT_MUNICIPIO (
	 	  Int64? edoclave, Int64? paiclave, DateTime munfecbaja, string mundescripcion, Int64 munclave
	 	 	 )
	 	 {
	 	 	 this.edoclave = edoclave;
	 	 	 this.paiclave = paiclave;
	 	 	 this.munfecbaja = munfecbaja;
	 	 	 this.mundescripcion = mundescripcion;
	 	 	 this.munclave = munclave;
	 	 }
 
	 }
}
