using PV221Chat.Core.DataModels;
using PV221Chat.DTO;
using System.Security.Cryptography;
using System.Text;

namespace PV221Chat.Mapper
{
    public static class UserMapper
    {
        public static User ToModel(CreateUserDTO dto)
        {
            return new User
            {
                Nickname = dto.Nickname,
                Email = dto.Email,
                PasswordHash = CalculateHash(dto.Password, dto.Email),
                CreatedAt = DateTime.UtcNow
            };
        }
        public static UserDTO ToDTO(User model)
        {
            return new UserDTO
            {
                UserId = model.UserId,
                Nickname = model.Nickname,
                Email = model.Email,
                AvatarUrl = model.AvatarUrl,
                Hobbies = model.Hobbies,
                Skills = model.Skills,
                BirthDate = model.BirthDate,
                CreatedAt = model.CreatedAt
            };
        }
        public static void UpdateModel(UserDTO dto, User model)
        {
            if (!string.IsNullOrEmpty(dto.Nickname))
                model.Nickname = dto.Nickname;

            if (!string.IsNullOrEmpty(dto.Email))
                model.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.AvatarUrl))
                model.AvatarUrl = dto.AvatarUrl;

            if (!string.IsNullOrEmpty(dto.Hobbies))
                model.Hobbies = dto.Hobbies;

            if (!string.IsNullOrEmpty(dto.Skills))
                model.Skills = dto.Skills;

            if (dto.BirthDate.HasValue)
                model.BirthDate = dto.BirthDate.Value;
        }

        public static bool VerifPassword(string pass, string passHash, string salt)
        {
            return passHash.Equals(CalculateHash(pass,salt));
        }

        private static string CalculateHash(string clearTextPassword, string salt)
        {
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);

            HashAlgorithm algorithm = SHA256.Create();

            byte[] hash = algorithm.ComputeHash(saltedHashBytes);

            return Convert.ToBase64String(hash);
        }
    }
}