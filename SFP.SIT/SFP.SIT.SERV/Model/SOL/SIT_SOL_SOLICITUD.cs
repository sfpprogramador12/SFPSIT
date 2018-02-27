using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.SOL
{
	 public class SIT_SOL_SOLICITUD
	 {
	 	 public int? prcclave  { set; get; } 
	 	 public DateTime solfecrecrev  { set; get; } 
	 	 public DateTime solfecacl  { set; get; } 
	 	 public int? megclave  { set; get; } 
	 	 public Int64? sntclave  { set; get; } 
	 	 public int? sotclave  { set; get; } 
	 	 public Int64? solnotificado  { set; get; } 
	 	 public string solmotdesecha  { set; get; } 
	 	 public int? solrespclave  { set; get; } 
	 	 public int? metclave  { set; get; } 
	 	 public string solotroderacc  { set; get; } 
	 	 public DateTime solfecresp  { set; get; } 
	 	 public DateTime solfecenv  { set; get; } 
	 	 public DateTime solfecent  { set; get; } 
	 	 public DateTime solfecsol  { set; get; } 
	 	 public string soldat  { set; get; } 
	 	 public string soldes  { set; get; } 
	 	 public string solarcdes  { set; get; } 
	 	 public string solotromod  { set; get; } 
	 	 public DateTime solfecrec  { set; get; } 
	 	 public Int64 solclave  { set; get; } 
	 	 public string solfolio  { set; get; } 
 
	 	 public SIT_SOL_SOLICITUD () {} 
 
	 	 public SIT_SOL_SOLICITUD (
	 	  int? prcclave, DateTime solfecrecrev, DateTime solfecacl, int? megclave, Int64? sntclave, int? sotclave, Int64? solnotificado, string solmotdesecha, int? solrespclave, int? metclave, string solotroderacc, DateTime solfecresp, DateTime solfecenv, DateTime solfecent, DateTime solfecsol, string soldat, string soldes, string solarcdes, string solotromod, DateTime solfecrec, Int64 solclave, string solfolio
	 	 	 )
	 	 {
	 	 	 this.prcclave = prcclave;
	 	 	 this.solfecrecrev = solfecrecrev;
	 	 	 this.solfecacl = solfecacl;
	 	 	 this.megclave = megclave;
	 	 	 this.sntclave = sntclave;
	 	 	 this.sotclave = sotclave;
	 	 	 this.solnotificado = solnotificado;
	 	 	 this.solmotdesecha = solmotdesecha;
	 	 	 this.solrespclave = solrespclave;
	 	 	 this.metclave = metclave;
	 	 	 this.solotroderacc = solotroderacc;
	 	 	 this.solfecresp = solfecresp;
	 	 	 this.solfecenv = solfecenv;
	 	 	 this.solfecent = solfecent;
	 	 	 this.solfecsol = solfecsol;
	 	 	 this.soldat = soldat;
	 	 	 this.soldes = soldes;
	 	 	 this.solarcdes = solarcdes;
	 	 	 this.solotromod = solotromod;
	 	 	 this.solfecrec = solfecrec;
	 	 	 this.solclave = solclave;
	 	 	 this.solfolio = solfolio;
	 	 }
 
	 }
}
