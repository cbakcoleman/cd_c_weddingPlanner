using System.Collections.Generic;

namespace cd_c_weddingPlanner.Models
{
    public class DashboardView
    {
        public List<Wedding> Weddings {get;set;}
        public int LoggedinUserId {get;set;}

        // IS THE SECOND EMPTY ONE NEEDED???
        public DashboardView() { }
        public DashboardView(List<Wedding> weddings, int userId)
        {
            Weddings = weddings;
            LoggedinUserId = userId;
        }
    }
}
