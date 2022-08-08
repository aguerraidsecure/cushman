using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wr_anit_cushman_one.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Correo { get; set; }

        [Required]
        public string Password { get; set; }
    }
}