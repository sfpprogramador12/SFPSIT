using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.DOC
{
	 public class SIT_DOC_EXTENSION
	 {
	 	 public string extterminacion  { set; get; } 
	 	 public string extmimetype  { set; get; } 
	 	 public int extclave  { set; get; } 
 
	 	 public SIT_DOC_EXTENSION () {} 
 
	 	 public SIT_DOC_EXTENSION (
	 	  string extterminacion, string extmimetype, int extclave
	 	 	 )
	 	 {
	 	 	 this.extterminacion = extterminacion;
	 	 	 this.extmimetype = extmimetype;
	 	 	 this.extclave = extclave;
	 	 }
 
	 }
}
