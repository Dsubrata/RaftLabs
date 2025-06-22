using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaftLabs.Enterprise.Domain.Models
{
    public class User
    {
        [Key]
        public virtual int id { get; set; }
        public virtual string email { get; set; }
        public virtual string first_name { get; set; }
        public virtual string last_name { get; set; }
        public virtual string avatar { get; set; }
    }

    public class GetUserApiResponse
    {
        public User data { get; set; }
        public support support { get; set; }    
    }

    public class GetAllUserApiResponse
    {
        public virtual int page { get; set; }
        public virtual int per_page { get; set; }
        public virtual int total { get; set; }
        public virtual int total_pages { get; set; }
        public List<User> data { get; set; }
        public support support { get; set; }
    }

    public class data 
    {
        public virtual int id { get; set; }
        public virtual string email { get; set; }
        public virtual string first_name { get; set; }
        public virtual string last_name { get; set; }
        public virtual string avatar { get; set; }
    }

    public class support
    {
        public virtual string url { get; set; }
        public virtual string text { get; set; }
    }


}
