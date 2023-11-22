namespace TechPrimeLab.ViewModel
{
    public class ChangeProjectStatusVM
    {
        public Guid project_user_id { get; set; }
        public Guid project_id { get; set; }
        public string project_status { get; set;} = string.Empty;
    }
}
