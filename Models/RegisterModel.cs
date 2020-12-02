using System.ComponentModel.DataAnnotations;

namespace CarparkApi.Models {
    public class RegisterModel {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Contact { get; set; }

        [Required]
        public string Password { get; set; }
    }
}