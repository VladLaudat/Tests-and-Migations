using System.ComponentModel.DataAnnotations;

namespace Backend.DbContext
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        [StringLength(40)]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
