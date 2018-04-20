using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Model.ADM
{
	 public class SIT_ADM_USUARIO
	 {
	 	 public string usractivo  { set; get; } 
	 	 public string usrauxcorreo  { set; get; } 
	 	 public DateTime usrfecmod  { set; get; } 
	 	 public string usrdesignacion  { set; get; } 
	 	 public string usrtitulo  { set; get; } 
	 	 public DateTime usrbloquearfin  { set; get; } 
	 	 public int usrintentos  { set; get; } 
	 	 public string usrextension  { set; get; } 
	 	 public string usrcorreo  { set; get; } 
	 	 public string usrcontraseña  { set; get; } 
	 	 public DateTime usrfecbaja  { set; get; } 
	 	 public string usrpuesto  { set; get; } 
	 	 public string usrmaterno  { set; get; } 
	 	 public string usrpaterno  { set; get; } 
	 	 public string usrnombre  { set; get; } 
	 	 public int usrclave  { set; get; } 
 
	 	 public SIT_ADM_USUARIO () {} 
 
	 	 public SIT_ADM_USUARIO (
<<<<<<< HEAD
           string usractivo, string usrauxcorreo, DateTime usrfecmod, string usrdesignacion, string usrtitulo, DateTime usrbloquearfin, int usrintentos, string usrextension, string usrcorreo, string usrcontraseña, DateTime usrfecbaja, string usrpuesto, string usrmaterno, string usrpaterno, string usrnombre, int usrclave
=======
	 	  string usractivo, string usrauxcorreo, DateTime usrfecmod, string usrdesignacion, string usrtitulo, DateTime usrbloquearfin, int usrintentos, string usrextension, string usrcorreo, string usrcontraseña, DateTime usrfecbaja, string usrpuesto, string usrmaterno, string usrpaterno, string usrnombre, int usrclave
>>>>>>> parent of 1d90231... Se agregan los cambios de Mak
	 	 	 )
	 	 {
	 	 	 this.usractivo = usractivo;
	 	 	 this.usrauxcorreo = usrauxcorreo;
	 	 	 this.usrfecmod = usrfecmod;
	 	 	 this.usrdesignacion = usrdesignacion;
	 	 	 this.usrtitulo = usrtitulo;
	 	 	 this.usrbloquearfin = usrbloquearfin;
	 	 	 this.usrintentos = usrintentos;
	 	 	 this.usrextension = usrextension;
	 	 	 this.usrcorreo = usrcorreo;
	 	 	 this.usrcontraseña = usrcontraseña;
	 	 	 this.usrfecbaja = usrfecbaja;
	 	 	 this.usrpuesto = usrpuesto;
	 	 	 this.usrmaterno = usrmaterno;
	 	 	 this.usrpaterno = usrpaterno;
	 	 	 this.usrnombre = usrnombre;
	 	 	 this.usrclave = usrclave;
	 	 }
 
	 }
}
