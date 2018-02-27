using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_MODULO
	 {
	 	 public string modmetodo  { set; get; } 
	 	 public int? modpadre  { set; get; } 
	 	 public DateTime modfecbaja  { set; get; } 
	 	 public int? modconsecutivo  { set; get; } 
	 	 public string modcontrol  { set; get; } 
	 	 public string moddescripcion  { set; get; } 
	 	 public int modclave  { set; get; } 
 
	 	 public SIT_ADM_MODULO () {} 
 
	 	 public SIT_ADM_MODULO (
	 	  string modmetodo, int? modpadre, DateTime modfecbaja, int? modconsecutivo, string modcontrol, string moddescripcion, int modclave
	 	 	 )
	 	 {
	 	 	 this.modmetodo = modmetodo;
	 	 	 this.modpadre = modpadre;
	 	 	 this.modfecbaja = modfecbaja;
	 	 	 this.modconsecutivo = modconsecutivo;
	 	 	 this.modcontrol = modcontrol;
	 	 	 this.moddescripcion = moddescripcion;
	 	 	 this.modclave = modclave;
	 	 }
 
	 }
}
