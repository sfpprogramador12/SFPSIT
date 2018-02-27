using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_PERFIL
	 {
	 	 public int permultiple  { set; get; } 
	 	 public string persigla  { set; get; } 
	 	 public string perdescripcion  { set; get; } 
	 	 public DateTime perfecbaja  { set; get; } 
	 	 public int perclave  { set; get; } 
 
	 	 public SIT_ADM_PERFIL () {} 
 
	 	 public SIT_ADM_PERFIL (
	 	  int permultiple, string persigla, string perdescripcion, DateTime perfecbaja, int perclave
	 	 	 )
	 	 {
	 	 	 this.permultiple = permultiple;
	 	 	 this.persigla = persigla;
	 	 	 this.perdescripcion = perdescripcion;
	 	 	 this.perfecbaja = perfecbaja;
	 	 	 this.perclave = perclave;
	 	 }
 
	 }
}
