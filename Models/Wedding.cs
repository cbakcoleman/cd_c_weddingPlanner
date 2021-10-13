using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cd_c_weddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId {get;set;}

        [Required(ErrorMessage = "First Spouse is required.")]
        [Display(Name = "First Spouse: ")]
        public string Spouse1 {get;set;}

        [Required(ErrorMessage = "Second Spouse is required.")]
        [Display(Name = "Second Spouse: ")]
        public string Spouse2 {get;set;}

        [Required(ErrorMessage = "Wedding Date is required.")]
        [Display(Name = "Wedding Date: ")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "MMMM dd, yyyy", ApplyFormatInEditMode = true)]
        public DateTime WeddingDate {get;set;}

        [Required(ErrorMessage ="Wedding Location is required.")]
        [Display(Name =  "Wedding Location: ")]
        public string Location {get;set;}

        public List<Guest> Users {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}