using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RED
{
	 public class SIT_RED_AFDFLUJO
	 {
	 	 public int rtpclave  { set; get; } 
	 	 public int afforigen  { set; get; } 
	 	 public int afdclave  { set; get; } 
	 	 public int affdestino  { set; get; } 
 
	 	 public SIT_RED_AFDFLUJO () {} 
 
	 	 public SIT_RED_AFDFLUJO (
	 	  int rtpclave, int afforigen, int afdclave, int affdestino
	 	 	 )
	 	 {
	 	 	 this.rtpclave = rtpclave;
	 	 	 this.afforigen = afforigen;
	 	 	 this.afdclave = afdclave;
	 	 	 this.affdestino = affdestino;
	 	 }
 
	 }
}
