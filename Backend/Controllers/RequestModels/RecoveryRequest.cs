using System.ComponentModel.DataAnnotations;
namespace Backend.Controllers.RequestModels
{
    public class RecoveryRequest
    {
    [Required]
    [EmailAddress(ErrorMessage ="Bad email format")]
        public string Email { get; set; }
    }
}
