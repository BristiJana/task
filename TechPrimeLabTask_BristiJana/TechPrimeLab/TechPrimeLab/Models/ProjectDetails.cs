using System.ComponentModel.DataAnnotations;

namespace TechPrimeLab.Models
{
    public class ProjectDetails
    {
        [Key]
        public Guid project_id { get; set; }
        public Guid project_user_id { get; set; }
        public string project_name { get; set; } = string.Empty;
        public string project_theme { get; set; } = string.Empty;
        public string project_reason { get; set; } = string.Empty;
        public string project_type { get; set; } = string.Empty;
        public string project_division { get; set; } = string.Empty;
        public string project_category { get; set; } = string.Empty;
        public string project_priority { get; set; } = string.Empty;
        public string project_department { get; set; } = string.Empty;
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string project_location { get; set; } = string.Empty;
        public string project_status { get; set; } = string.Empty;
    }
}
