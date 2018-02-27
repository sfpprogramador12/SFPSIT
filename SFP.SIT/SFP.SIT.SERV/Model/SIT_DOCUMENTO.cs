using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model
{
	 public class SIT_DOCUMENTO
	 {
	 	 public string doc_md5  { set; get; } 
	 	 public string doc_url  { set; get; } 
	 	 public DateTime doc_filesystem  { set; get; } 
	 	 public Int64 doc_cladoc  { set; get; } 
	 	 public int kte_claext  { set; get; } 
	 	 public string doc_nombre  { set; get; } 
	 	 public string doc_folio  { set; get; } 
	 	 public string doc_ruta  { set; get; } 
	 	 public Int64 doc_size  { set; get; } 
	 	 public DateTime doc_fecha  { set; get; } 
 
	 	 public SIT_DOCUMENTO () {} 
 
	 	 public SIT_DOCUMENTO (
	 	  string doc_md5, string doc_url, DateTime doc_filesystem, Int64 doc_cladoc, int kte_claext, string doc_nombre, string doc_folio, string doc_ruta, Int64 doc_size, DateTime doc_fecha
	 	 	 )
	 	 {
	 	 	 this.doc_md5 = doc_md5;
	 	 	 this.doc_url = doc_url;
	 	 	 this.doc_filesystem = doc_filesystem;
	 	 	 this.doc_cladoc = doc_cladoc;
	 	 	 this.kte_claext = kte_claext;
	 	 	 this.doc_nombre = doc_nombre;
	 	 	 this.doc_folio = doc_folio;
	 	 	 this.doc_ruta = doc_ruta;
	 	 	 this.doc_size = doc_size;
	 	 	 this.doc_fecha = doc_fecha;
	 	 }
	 }
}
