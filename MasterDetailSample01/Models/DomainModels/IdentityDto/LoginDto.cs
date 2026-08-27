using System.ComponentModel.DataAnnotations;

namespace MasterDetailSample01.Models.DomainModels.IdentityDto
{
    public class LoginDto
    {
            [Required]
            public string? Username { get; set; }

            [Required]
            public string? Password { get; set; }
    }
}
