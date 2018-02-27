using SFP.SIT.SERVICES.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Negocio
{
    public class AdmCorreoNeg
    {
        CfgCorreoMdl _cfgCorreo = null;

        public AdmCorreoNeg(CfgCorreoMdl cfgCorreo)
        {
            _cfgCorreo = cfgCorreo;
        }

        public bool Enviar(string sCorreoDest, string sTitulo, string sMensaje)
        {
            bool bRespuesta = false;
            
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(_cfgCorreo.servidor, _cfgCorreo.puerto);
            System.Net.Mail.MailMessage MyMailMessage = new System.Net.Mail.MailMessage(_cfgCorreo.usuario, sCorreoDest, sTitulo, sMensaje);
            MyMailMessage.IsBodyHtml = true;

            //Proper Authentication Details need to be passed when sending email from gmail
            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(_cfgCorreo.usuario, _cfgCorreo.contraseña);

            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //mailClient.ServicePoint.Address;
            mailClient.EnableSsl = true;
            mailClient.UseDefaultCredentials = false;


            mailClient.Credentials = mailAuthentication;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                     delegate (object s
                         , X509Certificate certificate
                         , X509Chain chain
                         , SslPolicyErrors sslPolicyErrors)
                     { return true; };

                mailClient.Send(MyMailMessage);
                bRespuesta =  true;
            }
            catch (Exception exc)
            {
                System.Console.Out.WriteLine(exc.Message);
            }
            return bRespuesta;
        }

    }
}
