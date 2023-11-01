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
    public class PizzaInventoryService : IPizzaInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PizzaInventoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PizzaInventoryDTO>> GetPizzaInventoryAsync()
        {
            var pizzaInventory = await _unitOfWork.PizzaInventoryRepository.GetAll();
            return _mapper.Map<IEnumerable<PizzaInventoryDTO>>(pizzaInventory);
        }

        public async Task<bool> InsertAsync(PizzaInventoryDTO pizzaInventoryDTO)
        {
            var pizzaInventory = _mapper.Map<PizzaInventory>(pizzaInventoryDTO);
            return await _unitOfWork.PizzaInventoryRepository.Add(pizzaInventory);
        }
        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
