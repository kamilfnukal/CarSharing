using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
using CarSharingBL.Services.IService;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CarSharingBL.Services.Service
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        private readonly IUserQueryObject QueryObject;

        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        public UserService(IRepository<User> repository, IMapper mapper, IUserQueryObject userQueryObject)
            : base(repository, mapper)
        {
            QueryObject = userQueryObject;
        }

        public async Task<DriverDto> GetDriverById(int driverId)
        {
            return Mapper.Map<DriverDto>(await Repository.GetEntityById(driverId));
        }

        public async Task<UserDto> GetUserByUserName(string userName) 
        {
            if (userName == null)
            {
                userName = string.Empty;
            }

            return await QueryObject.GetUserByUserName(userName);

        }

        public async Task<ICollection<UserDto>> GetUsersByNameAndSurname(string name, string surname)
        {
            if (name == null)
            {
                name = string.Empty;
            }

            if (surname == null)
            {
                surname = string.Empty;
            }

            return await QueryObject.GetUsersByNameAndSurname(name, surname);
        }

        public async Task<UserDto> AuthorizeUser(UserLoginDto login)
        {
            var userDto = await GetUserByUserName(login.UserName);
            
            if (userDto == null)
            {
                return null;
            }

            var user = await Repository.GetEntityById(userDto.Id);
            var (hash, salt) = user != null ? GetPassAndSalt(user.PasswordHash) : (string.Empty, string.Empty);
            var succ = user != null && VerifyHashedPassword(hash, salt, login.Password);

            return succ ? userDto : null;
        }

        public void RegisterUser(UserRegistrationDto user)
        {
            var (hash, salt) = CreateHash(user.Password);
            user.PasswordHash = string.Join(',', hash, salt);
        }

        private (string, string) GetPassAndSalt(string passwordHash)
        {
            var result = passwordHash.Split(',');
            
            if (result.Count() != 2)
            {
                return (string.Empty, string.Empty);
            }
            return (result[0], result[1]);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}
