using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApplication.Models
{
    public class LoginDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
