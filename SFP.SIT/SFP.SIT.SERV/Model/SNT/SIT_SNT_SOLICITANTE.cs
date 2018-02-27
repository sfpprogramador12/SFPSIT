using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SNT
{
	 public class SIT_SNT_SOLICITANTE
	 {
	 	 public Int64? paiclave  { set; get; } 
	 	 public int? ocuclave  { set; get; } 
	 	 public int? tslclave  { set; get; } 
	 	 public string sntotroniveledu  { set; get; } 
	 	 public string sntotraocup  { set; get; } 
	 	 public string sntniveduc  { set; get; } 
	 	 public string sntrepleg  { set; get; } 
	 	 public Int64? munclave  { set; get; } 
	 	 public Int64? edoclave  { set; get; } 
	 	 public string sntusuario  { set; get; } 
	 	 public DateTime sntfecnac  { set; get; } 
	 	 public string sntsexo  { set; get; } 
	 	 public string sntciudadext  { set; get; } 
	 	 public string sntedoext  { set; get; } 
	 	 public string sntcorele  { set; get; } 
	 	 public string snttel  { set; get; } 
	 	 public string sntcodpos  { set; get; } 
	 	 public string sntcol  { set; get; } 
	 	 public string sntnumint  { set; get; } 
	 	 public string sntnumext  { set; get; } 
	 	 public string sntcalle  { set; get; } 
	 	 public string sntcurp  { set; get; } 
	 	 public string sntnombre  { set; get; } 
	 	 public string sntapemat  { set; get; } 
	 	 public string sntapepat  { set; get; } 
	 	 public string sntrfc  { set; get; } 
	 	 public Int64 sntclave  { set; get; } 
 
	 	 public SIT_SNT_SOLICITANTE () {} 
 
	 	 public SIT_SNT_SOLICITANTE (
	 	  Int64? paiclave, int? ocuclave, int? tslclave, string sntotroniveledu, string sntotraocup, string sntniveduc, string sntrepleg, Int64? munclave, Int64? edoclave, string sntusuario, DateTime sntfecnac, string sntsexo, string sntciudadext, string sntedoext, string sntcorele, string snttel, string sntcodpos, string sntcol, string sntnumint, string sntnumext, string sntcalle, string sntcurp, string sntnombre, string sntapemat, string sntapepat, string sntrfc, Int64 sntclave
	 	 	 )
	 	 {
	 	 	 this.paiclave = paiclave;
	 	 	 this.ocuclave = ocuclave;
	 	 	 this.tslclave = tslclave;
	 	 	 this.sntotroniveledu = sntotroniveledu;
	 	 	 this.sntotraocup = sntotraocup;
	 	 	 this.sntniveduc = sntniveduc;
	 	 	 this.sntrepleg = sntrepleg;
	 	 	 this.munclave = munclave;
	 	 	 this.edoclave = edoclave;
	 	 	 this.sntusuario = sntusuario;
	 	 	 this.sntfecnac = sntfecnac;
	 	 	 this.sntsexo = sntsexo;
	 	 	 this.sntciudadext = sntciudadext;
	 	 	 this.sntedoext = sntedoext;
	 	 	 this.sntcorele = sntcorele;
	 	 	 this.snttel = snttel;
	 	 	 this.sntcodpos = sntcodpos;
	 	 	 this.sntcol = sntcol;
	 	 	 this.sntnumint = sntnumint;
	 	 	 this.sntnumext = sntnumext;
	 	 	 this.sntcalle = sntcalle;
	 	 	 this.sntcurp = sntcurp;
	 	 	 this.sntnombre = sntnombre;
	 	 	 this.sntapemat = sntapemat;
	 	 	 this.sntapepat = sntapepat;
	 	 	 this.sntrfc = sntrfc;
	 	 	 this.sntclave = sntclave;
	 	 }
 
	 }
}
