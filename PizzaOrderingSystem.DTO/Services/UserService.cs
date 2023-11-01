using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.DataAccess.Configuration;
using PizzaOrderingSystem.DataAccess.IRepositories;
using PizzaOrderingSystem.DataAccess.Models;
using PizzaOrderingSystem.DTO.IService;
using PizzaOrderingSystem.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DTO.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<bool> InsertAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            return await _unitOfWork.UserRepository.Add(user);
        }

        public UserDTO GetUserByIdPassword(string userName, string password)
        {
            var user = _unitOfWork.UserRepository.GetUserByIdPassword(userName, password);
            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO CheckUserAvailibility(string userName, string email)
        {
            var user = _unitOfWork.UserRepository.CheckUserAvailibility(userName, email);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }

    }
}
