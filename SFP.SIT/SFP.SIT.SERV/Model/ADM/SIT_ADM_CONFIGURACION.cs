using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_CONFIGURACION
	 {
	 	 public DateTime cfgfecbaja  { set; get; } 
	 	 public string cfgvalor  { set; get; } 
	 	 public string cfgsubclave  { set; get; } 
	 	 public int cfgclave  { set; get; } 
 
	 	 public SIT_ADM_CONFIGURACION () {} 
 
	 	 public SIT_ADM_CONFIGURACION (
	 	  DateTime cfgfecbaja, string cfgvalor, string cfgsubclave, int cfgclave
	 	 	 )
	 	 {
	 	 	 this.cfgfecbaja = cfgfecbaja;
	 	 	 this.cfgvalor = cfgvalor;
	 	 	 this.cfgsubclave = cfgsubclave;
	 	 	 this.cfgclave = cfgclave;
	 	 }
 
	 }
}
