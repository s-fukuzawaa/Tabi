using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tabi.Models
{
    public class Survey
    {
        public int ID { get; set; }
        [Display(Name = "Your Age")]
        public int age { get; set; }
        [Display(Name = "Party Size")]
        public int size { get; set; }
        [Display(Name = "Your Budget")]
        public int budget { get; set; }
        [Display(Name = "Plane included?")]
        public Boolean planeTicket { get; set; }
        [Display(Name = "Duration")]
        public int duration { get; set; }
        [Display(Name = "Where")]
        public string region { get; set; }
        [Display(Name = "Nature or City?")]
        public string type { get; set; }

    }
}
