using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RED
{
	 public class SIT_RED_ARISTA
	 {
	 	 public int? rtpclave  { set; get; } 
	 	 public int? prcclave  { set; get; } 
	 	 public Int64? solclave  { set; get; } 
	 	 public int arihito  { set; get; } 
	 	 public int aridiasnat  { set; get; } 
	 	 public int aridiaslab  { set; get; } 
	 	 public DateTime arifecenvio  { set; get; } 
	 	 public Int64 ariclave  { set; get; } 
	 	 public Int64? noddestino  { set; get; } 
	 	 public Int64? nodorigen  { set; get; } 
 
	 	 public SIT_RED_ARISTA () {} 
 
	 	 public SIT_RED_ARISTA (
	 	  int? rtpclave, int? prcclave, Int64? solclave, int arihito, int aridiasnat, int aridiaslab, DateTime arifecenvio, Int64 ariclave, Int64? noddestino, Int64? nodorigen
	 	 	 )
	 	 {
	 	 	 this.rtpclave = rtpclave;
	 	 	 this.prcclave = prcclave;
	 	 	 this.solclave = solclave;
	 	 	 this.arihito = arihito;
	 	 	 this.aridiasnat = aridiasnat;
	 	 	 this.aridiaslab = aridiaslab;
	 	 	 this.arifecenvio = arifecenvio;
	 	 	 this.ariclave = ariclave;
	 	 	 this.noddestino = noddestino;
	 	 	 this.nodorigen = nodorigen;
	 	 }
 
	 }
}
