using AutoMapper;
using AutoMapper.Configuration.Annotations;
using RaftLabs.Enterprise.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RaftLabs.Enterprise.Domain.DTOs
{
    [AutoMap(typeof(User))]
    public class UserDTO
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        [SourceMember("first_name")]
        public virtual string Firstname { get; set; }
        [SourceMember("last_name")]
        public virtual string Lastname { get; set; }
        public virtual string Avatar { get; set; }
    }
}
