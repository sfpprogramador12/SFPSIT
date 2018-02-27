using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.DOC
{
	 public class SIT_DOC_DOCUMENTO
	 {
	 	 public string docmd5  { set; get; } 
	 	 public string docurl  { set; get; } 
	 	 public DateTime docfilesystem  { set; get; } 
	 	 public Int64 docclave  { set; get; } 
	 	 public int? extclave  { set; get; } 
	 	 public string docnombre  { set; get; } 
	 	 public string docfolio  { set; get; } 
	 	 public string docruta  { set; get; } 
	 	 public Int64 doctamaño  { set; get; } 
	 	 public DateTime docfecha  { set; get; } 
 
	 	 public SIT_DOC_DOCUMENTO () {} 
 
	 	 public SIT_DOC_DOCUMENTO (
	 	  string docmd5, string docurl, DateTime docfilesystem, Int64 docclave, int? extclave, string docnombre, string docfolio, string docruta, Int64 doctamaño, DateTime docfecha
	 	 	 )
	 	 {
	 	 	 this.docmd5 = docmd5;
	 	 	 this.docurl = docurl;
	 	 	 this.docfilesystem = docfilesystem;
	 	 	 this.docclave = docclave;
	 	 	 this.extclave = extclave;
	 	 	 this.docnombre = docnombre;
	 	 	 this.docfolio = docfolio;
	 	 	 this.docruta = docruta;
	 	 	 this.doctamaño = doctamaño;
	 	 	 this.docfecha = docfecha;
	 	 }
 
	 }
}
