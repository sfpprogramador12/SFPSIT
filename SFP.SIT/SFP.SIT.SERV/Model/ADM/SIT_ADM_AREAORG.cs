using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_AREAORG
	 {
	 	 public int agnclave  { set; get; } 
	 	 public int araclave  { set; get; } 
	 	 public int orgorden  { set; get; } 
	 	 public int orgclavereporta  { set; get; } 
 
	 	 public SIT_ADM_AREAORG () {} 
 
	 	 public SIT_ADM_AREAORG (
	 	  int agnclave, int araclave, int orgorden, int orgclavereporta
	 	 	 )
	 	 {
	 	 	 this.agnclave = agnclave;
	 	 	 this.araclave = araclave;
	 	 	 this.orgorden = orgorden;
	 	 	 this.orgclavereporta = orgclavereporta;
	 	 }
 
	 }
}
