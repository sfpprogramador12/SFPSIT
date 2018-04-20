using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RED
{
	 public class SIT_RED_NODO
	 {
	 	 public int? perclave  { set; get; } 
	 	 public DateTime nodfeclectura  { set; get; } 
	 	 public int nodusrausencia  { set; get; } 
	 	 public int? usrclave  { set; get; } 
	 	 public int? prcclave  { set; get; } 
	 	 public Int64? solclave  { set; get; } 
	 	 public int? araclave  { set; get; } 
	 	 public int nodcapa  { set; get; } 
	 	 public int nodatendido  { set; get; } 
	 	 public int? nedclave  { set; get; } 
	 	 public DateTime nodfeccreacion  { set; get; } 
	 	 public Int64 nodclave  { set; get; }

        public Int64 nodregresar { set; get; }

        public SIT_RED_NODO () {} 
 
	 	 public SIT_RED_NODO (
	 	  int? perclave, DateTime nodfeclectura, int nodusrausencia, int? usrclave, int? prcclave, Int64? solclave, int? araclave, int nodcapa, int nodatendido, int? nedclave, DateTime nodfeccreacion, Int64 nodclave
	 	 	 )
	 	 {
	 	 	 this.perclave = perclave;
	 	 	 this.nodfeclectura = nodfeclectura;
	 	 	 this.nodusrausencia = nodusrausencia;
	 	 	 this.usrclave = usrclave;
	 	 	 this.prcclave = prcclave;
	 	 	 this.solclave = solclave;
	 	 	 this.araclave = araclave;
	 	 	 this.nodcapa = nodcapa;
	 	 	 this.nodatendido = nodatendido;
	 	 	 this.nedclave = nedclave;
	 	 	 this.nodfeccreacion = nodfeccreacion;
	 	 	 this.nodclave = nodclave;
	 	 }
 
	 }
}
