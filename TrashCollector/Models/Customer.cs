using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class Customer
    {
        
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        
        [Display(Name = "Pick Up Day")]
        public DayOfWeek weeklyPickUpDay { get; set; }


        [Display(Name = "One Time Pick Ups")]
        public DateTime extraPickUps { get; set; }

        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime endDate { get; set; }

        [Display(Name = "Amount Owed")]
        public double amountOwed { get; set; }



        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; } 

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }


        public enum DayOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday, 
            Friday,
        }
    }
}
