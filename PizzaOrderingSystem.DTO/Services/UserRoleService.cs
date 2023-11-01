using AutoMapper;
using PizzaOrderingSystem.DataAccess.Configuration;
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
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserRoleService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserRoleDTO>> GetUserRolesAsync()
        {
            var userRoles = await _unitOfWork.UserRoleRepository.GetAll();
            return _mapper.Map<IEnumerable<UserRoleDTO>>(userRoles);
        }

        public UserRoleDTO GetRoleByUserId(int userId)
        {
            var userRole = _unitOfWork.UserRoleRepository.GetRoleByUserId(userId);
            return _mapper.Map<UserRoleDTO>(userRole);
        }

        public async Task<bool> InsertAsync(UserRoleDTO userRoleDTO)
        {
            var userRole = _mapper.Map<UserRole>(userRoleDTO);
            return await _unitOfWork.UserRoleRepository.Add(userRole);
        }
        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }

    }
}
