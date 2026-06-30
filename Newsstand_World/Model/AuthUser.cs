using System.ComponentModel.DataAnnotations;

namespace Newsstand_World.Model
{
    public class AuthUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public string? AvatarPath { get; set; }
    }
}
