using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Street Name")]
        public string streetName { get; set; }

        [Display(Name = "House Number")]
        public int houseNumber { get; set; }

        [Display(Name = "Zip Code")]
        public int zipCode { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }
        
    }
}
