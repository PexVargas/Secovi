using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPexIM.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "O login deve ser inserido.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida.")]
        public string Senha { get; set; }

        public string SiglaEstado { get; set; }
    }
}
