using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingWebAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserFacade UserFacade;

        public UserController(IUserFacade userFacade)
        {
            UserFacade = userFacade;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegistrationDto user)
        {
            if (await UserFacade.UserExists(user.UserName))
            {
                return StatusCode(15);
            }

            await UserFacade.RegisterUser(user);

            var newUser = new UserDto
            {
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                BirthDate = DateTime.Parse(user.BirthDate)
            };

            await UserFacade.CreateUser(newUser);
            return Ok();
        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            await UserFacade.DeleteUser(userId);
            return Ok();
        }

        [HttpGet("get-user/{userId}")]
        public async Task<ActionResult<RideDto>> GetUserByID(int userId)
        {
            return Ok(await UserFacade.GetUserByID(userId));
        }

        [HttpGet("get-username/{userName}")]
        public async Task<ActionResult<RideDto>> GetUserByUsername(string userName)
        {
            var user = await UserFacade.GetUserByUserName(userName);
            
            if (user == null)
            {
                return StatusCode(10);
            }
            return Ok(user);
        }

        [HttpPut("edit-phone/{phoneNumber}")]
        public async Task<ActionResult> EditUserPhone([FromBody] UserEditDto user, string phoneNumber)
        {
            var modify = await UserFacade.GetUserByID(user.Id);
            
            if(phoneNumber == null)
            {
                phoneNumber = String.Empty;
            }
            
            if (modify != null)
            {
                modify.PhoneNumber = phoneNumber;
            }

            await UserFacade.UpdateUser(modify);
            return Ok();
        }

        [HttpPut("edit-fullname/{fullname}")]
        public async Task<ActionResult> EditFullName([FromBody] UserEditDto user, string fullname)
        {
            var modify = await UserFacade.GetUserByID(user.Id);
            var nameAndSurname = user.FullName.Split(' ');
            
            if (nameAndSurname.Length != 2)
            {
                return StatusCode(10);
            }

            modify.Name = nameAndSurname[0].Substring(0,1).ToUpper() + nameAndSurname[0][1..].ToLower();
            modify.Surname = nameAndSurname[1].Substring(0, 1).ToUpper() + nameAndSurname[1][1..].ToLower();
            await UserFacade.UpdateUser(modify);
            return Ok();
        }

        [HttpGet("check-username/{userName}")]
        public async Task<ActionResult<bool>> UserExists(string userName)
        {
            return Ok(await UserFacade.UserExists(userName));
        }

        [HttpGet("get-user/{name}/{surname}")]
        public async Task<ActionResult<ICollection<UserDto>>> GetUsersByNameAndSurname(string name, string surname)
        {
            return Ok(await UserFacade.GetUsersByNameAndSurname(name, surname));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLogin)
        {
            try
            {
                var user = await UserFacade.Login(userLogin);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(15);
            }
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return Ok(await UserFacade.GetAllUsers());
        }
    }
}
