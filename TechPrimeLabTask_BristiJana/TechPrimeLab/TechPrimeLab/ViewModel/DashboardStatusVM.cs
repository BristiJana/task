namespace TechPrimeLab.ViewModel
{
    public class DashboardStatusVM
    {
        public int total_projects { get; set; }
        public int closed_projects { get; set; }
        public int running_projects { get; set; }
        public int closure_delay_projects { get; set; }
        public int canceled_projects { get; set; }
    }
}
