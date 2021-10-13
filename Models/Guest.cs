using System;
using System.ComponentModel.DataAnnotations;

namespace cd_c_weddingPlanner.Models
{
    public class Guest
    {
        [Key]
        public int GuestId {get;set;}

        public int UserId {get;set;}
        public User GuestUser {get;set;}

        public int WeddingId {get;set;}
        public Wedding GuestWedding {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}