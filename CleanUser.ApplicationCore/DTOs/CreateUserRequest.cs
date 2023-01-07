using System.ComponentModel.DataAnnotations;

namespace CleanUser.ApplicationCore.DTOs
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public decimal Credit { get; set; }
    }


    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal Credit { get; set; }
    }
}
