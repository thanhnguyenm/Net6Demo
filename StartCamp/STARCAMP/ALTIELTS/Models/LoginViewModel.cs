using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALTIELTS.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Passcode is required")]
        public string Passcode { get; set; }

        [Required(ErrorMessage = "Service is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Service is invalid")]
        public int? Service { get; set; }
    }
}
