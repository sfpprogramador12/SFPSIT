using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SOL
{
	 public class SIT_SOL_PROCESOPLAZOS
	 {
	 	 public int pczclave  { set; get; } 
	 	 public int sotclave  { set; get; } 
	 	 public int pczamarillo  { set; get; } 
	 	 public int pczverde  { set; get; } 
	 	 public int pczplazo  { set; get; } 
	 	 public int prcclave  { set; get; } 
 
	 	 public SIT_SOL_PROCESOPLAZOS () {} 
 
	 	 public SIT_SOL_PROCESOPLAZOS (
	 	  int pczclave, int sotclave, int pczamarillo, int pczverde, int pczplazo, int prcclave
	 	 	 )
	 	 {
	 	 	 this.pczclave = pczclave;
	 	 	 this.sotclave = sotclave;
	 	 	 this.pczamarillo = pczamarillo;
	 	 	 this.pczverde = pczverde;
	 	 	 this.pczplazo = pczplazo;
	 	 	 this.prcclave = prcclave;
	 	 }
 
	 }
}
