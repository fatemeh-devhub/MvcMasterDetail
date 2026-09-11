using System.ComponentModel.DataAnnotations;

namespace MasterDetailSample01.Models.DomainModels.IdentityDto
{
    public class Register
    {
        [Required]
        public string? Username { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
