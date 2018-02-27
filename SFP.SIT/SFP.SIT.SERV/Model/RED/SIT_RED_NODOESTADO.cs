using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RED
{
	 public class SIT_RED_NODOESTADO
	 {
	 	 public int nedtipo  { set; get; } 
	 	 public string nedurl  { set; get; } 
	 	 public string neddescripcion  { set; get; } 
	 	 public int nedclave  { set; get; } 
 
	 	 public SIT_RED_NODOESTADO () {} 
 
	 	 public SIT_RED_NODOESTADO (
	 	  int nedtipo, string nedurl, string neddescripcion, int nedclave
	 	 	 )
	 	 {
	 	 	 this.nedtipo = nedtipo;
	 	 	 this.nedurl = nedurl;
	 	 	 this.neddescripcion = neddescripcion;
	 	 	 this.nedclave = nedclave;
	 	 }
 
	 }
}
