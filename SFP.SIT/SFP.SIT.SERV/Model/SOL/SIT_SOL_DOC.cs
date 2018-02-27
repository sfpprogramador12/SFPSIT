using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SOL
{
	 public class SIT_SOL_DOC
	 {
	 	 public Int64 docclave  { set; get; } 
	 	 public Int64 solclave  { set; get; } 
 
	 	 public SIT_SOL_DOC () {} 
 
	 	 public SIT_SOL_DOC (
	 	  Int64 docclave, Int64 solclave
	 	 	 )
	 	 {
	 	 	 this.docclave = docclave;
	 	 	 this.solclave = solclave;
	 	 }
 
	 }
}
