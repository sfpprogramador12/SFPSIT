using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SOL
{
	 public class SIT_SOL_SEGUIMIENTO
	 {
	 	 public Int64? repclave  { set; get; } 
	 	 public int? usrclave  { set; get; } 
	 	 public DateTime segfecestimada  { set; get; } 
	 	 public Int64 segultimonodo  { set; get; } 
	 	 public int? afdclave  { set; get; } 
	 	 public int segedoproceso  { set; get; } 
	 	 public int prcclave  { set; get; } 
	 	 public DateTime segfeccalculo  { set; get; } 
	 	 public int segdiasnolab  { set; get; } 
	 	 public int segmultiple  { set; get; } 
	 	 public DateTime segfecfin  { set; get; } 
	 	 public DateTime segfecamp  { set; get; } 
	 	 public int segsemaforocolor  { set; get; } 
	 	 public int segdiassemaforo  { set; get; } 
	 	 public DateTime segfecini  { set; get; } 
	 	 public Int64 solclave  { set; get; } 
 
	 	 public SIT_SOL_SEGUIMIENTO () {} 
 
	 	 public SIT_SOL_SEGUIMIENTO (
	 	  Int64? repclave, int? usrclave, DateTime segfecestimada, Int64 segultimonodo, int? afdclave, int segedoproceso, int prcclave, DateTime segfeccalculo, int segdiasnolab, int segmultiple, DateTime segfecfin, DateTime segfecamp, int segsemaforocolor, int segdiassemaforo, DateTime segfecini, Int64 solclave
	 	 	 )
	 	 {
	 	 	 this.repclave = repclave;
	 	 	 this.usrclave = usrclave;
	 	 	 this.segfecestimada = segfecestimada;
	 	 	 this.segultimonodo = segultimonodo;
	 	 	 this.afdclave = afdclave;
	 	 	 this.segedoproceso = segedoproceso;
	 	 	 this.prcclave = prcclave;
	 	 	 this.segfeccalculo = segfeccalculo;
	 	 	 this.segdiasnolab = segdiasnolab;
	 	 	 this.segmultiple = segmultiple;
	 	 	 this.segfecfin = segfecfin;
	 	 	 this.segfecamp = segfecamp;
	 	 	 this.segsemaforocolor = segsemaforocolor;
	 	 	 this.segdiassemaforo = segdiassemaforo;
	 	 	 this.segfecini = segfecini;
	 	 	 this.solclave = solclave;
	 	 }
 
	 }
}
