using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_AREAGESTION
	 {
	 	 public string agndescripcion  { set; get; } 
	 	 public DateTime agnfecfin  { set; get; } 
	 	 public DateTime agnfecini  { set; get; } 
	 	 public int agnclave  { set; get; } 
 
	 	 public SIT_ADM_AREAGESTION () {} 
 
	 	 public SIT_ADM_AREAGESTION (
	 	  string agndescripcion, DateTime agnfecfin, DateTime agnfecini, int agnclave
	 	 	 )
	 	 {
	 	 	 this.agndescripcion = agndescripcion;
	 	 	 this.agnfecfin = agnfecfin;
	 	 	 this.agnfecini = agnfecini;
	 	 	 this.agnclave = agnclave;
	 	 }
 
	 }
}
