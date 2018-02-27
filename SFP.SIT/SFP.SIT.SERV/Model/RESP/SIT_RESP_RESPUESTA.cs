using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.RESP
{
	 public class SIT_RESP_RESPUESTA
	 {
	 	 public Int64 repcantidad  { set; get; } 
	 	 public int? megclave  { set; get; } 
	 	 public Int64? docclave  { set; get; } 
	 	 public string repoficio  { set; get; } 
	 	 public DateTime repedofec  { set; get; } 
	 	 public int? rtpclave  { set; get; } 
	 	 public Int64 repclave  { set; get; } 
 
	 	 public SIT_RESP_RESPUESTA () {} 
 
	 	 public SIT_RESP_RESPUESTA (
	 	  Int64 repcantidad, int? megclave, Int64? docclave, string repoficio, DateTime repedofec, int? rtpclave, Int64 repclave
	 	 	 )
	 	 {
	 	 	 this.repcantidad = repcantidad;
	 	 	 this.megclave = megclave;
	 	 	 this.docclave = docclave;
	 	 	 this.repoficio = repoficio;
	 	 	 this.repedofec = repedofec;
	 	 	 this.rtpclave = rtpclave;
	 	 	 this.repclave = repclave;
	 	 }
 
	 }
}
