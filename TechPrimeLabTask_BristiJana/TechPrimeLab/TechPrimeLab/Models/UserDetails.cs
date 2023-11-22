using System.ComponentModel.DataAnnotations;

namespace TechPrimeLab.Models
{
    public class UserDetails
    {
        [Key]
        public Guid user_id { get; set; }
        public string user_name { get; set; } 
        public string user_email { get; set; } 
        public string password_hash { get; set; }
        public string password_salt { get; set; }
        public bool is_active { get; set; }
        public DateTime? created_ts { get; set; }
        public DateTime? modified_ts { get;set; }
    }
}
