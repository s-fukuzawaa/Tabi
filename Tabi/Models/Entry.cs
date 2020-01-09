using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tabi.Models
{
    public class Entry
    {
        public int ID { get; set; }
        public string city { get; set; }
        //hotel/food/ sightseeing
        public string placetype { get; set; }
        
        public string address { get; set; }
        public string imgUrl { get; set; }
        public string map { get; set; }
    }
}
