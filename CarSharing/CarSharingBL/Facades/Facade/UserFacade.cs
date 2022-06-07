using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using CarSharingBL.Services.IService;
using CarSharingInfrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.Facade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IUserService Service;
        private readonly IPictureService PictureService;

        public UserFacade(IUnitOfWork unitOfWork, IUserService service, IPictureService pictureService)
        {
            UnitOfWork = unitOfWork;
            Service = service;
            PictureService = pictureService;
        }

        public async Task CreateUser(UserDto user)
        {
            Service.CreateEntity(user);
            await UnitOfWork.CommitAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await Service.GetEntityByID(userId);
            
            if (user.Picture != null)
            {
                PictureService.DeleteEntity(user.Picture.Id);
            }
            
            Service.DeleteEntity(userId);
            await UnitOfWork.CommitAsync();
        }

        public async Task UpdateUser(UserDto user)
        {
            Service.UpdateEntity(user);
            await UnitOfWork.CommitAsync();
        }

        public async Task<UserDto> GetUserByID(int userId)
        {
            return await Service.GetEntityByID(userId);
        }

        public async Task<DriverDto> GetDriverById(int driverId)
        {
            return await Service.GetDriverById(driverId);
        }

        public async Task<UserDto> GetUserByUserName(string userName)
        {
            return await Service.GetUserByUserName(userName);
        }

        public async Task<bool> UserExists(string userName)
        {
            return await GetUserByUserName(userName) != null;
        }

        public async Task<UserDto> Login(UserLoginDto userLogin)
        {
            var user = await Service.AuthorizeUser(userLogin);
            
            if (user != null)
            {
                return user;
            }
            throw new UnauthorizedAccessException();
        }

        public async Task RegisterUser(UserRegistrationDto user)
        {
            Service.RegisterUser(user);
            await UnitOfWork.CommitAsync();
        }

        public async Task<ICollection<UserDto>> GetUsersByNameAndSurname(string name, string surname)
        {
            return await Service.GetUsersByNameAndSurname(name, surname);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return await Service.GetAllEntities();
        }
    }
}
