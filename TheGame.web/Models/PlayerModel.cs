using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheGame.web.Models
{
    public class PlayerModel
    {
        [Required]
        [Display(Name = "Username")]
        public string PlayerName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Wiederholen")]
        public string Password2 { get; set; }

        public bool IsFreigeschalten { get; set; }
    }
}