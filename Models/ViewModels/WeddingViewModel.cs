using System.Collections.Generic;

namespace cd_c_weddingPlanner.Models
{
    public class WeddingView
    {
        public Wedding Wedding {get;set;}

        
        public int LoggedinuserId {get;set;}

        public List<Guest> Guests {get;set;}

        public WeddingView(Wedding wedding, int userId)
        {
            Wedding = wedding;
            LoggedinuserId = userId;
        }
    }
}