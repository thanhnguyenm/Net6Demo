using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALTIELTS.Models
{
    public class RatingViewModel
    {
        [Required]
        [Range(1, double.MaxValue)]
        public int QuestionId{ get; set; }
        public string Question { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating{ get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
