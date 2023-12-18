using App104.DTOS.Category;
using App104.DTOS;
using App104.Entities;
using App104.Repositories.Interfaces;
using App104.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App104.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name
            };
            await _repository.AddAsync(category);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category is null) throw new Exception("Not found");

            _repository.Delete(category);

            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            var categories = await _repository
                .GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false)
                .ToListAsync();

            ICollection<GetCategoryDto> result = new List<GetCategoryDto>();

            foreach (Category item in categories)
            {
                result.Add(new()
                {
                    Id = item.Id,

                    Name = item.Name,
                });
            }
            return result;
        }


        public async Task<GetCategoryDto> GetAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category is null) throw new Exception("Not found");

            return new GetCategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category is null) throw new Exception("Not found");

            category.Name = categoryDto.Name;

            _repository.Update(category);

            await _repository.SaveChangesAsync();
        }
    }
}
