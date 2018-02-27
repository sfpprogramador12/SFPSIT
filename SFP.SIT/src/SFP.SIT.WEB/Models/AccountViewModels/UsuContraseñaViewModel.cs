
using System.ComponentModel.DataAnnotations;

namespace SFP.SIT.WEB.Models.AccountViewModels
{
    public class UsuContraseñaViewModel
    {
        public int codigo { get; set; }

        [Required]
        [EmailAddress]
        public string correo { get; set; }
        public string contraseña { get; set; }
        public string confirmar { get; set; }

        public string error { get; set; }
    }
}
