using App104.DTOS.Tag;
using App104.DTOS;
using App104.Entities;
using App104.Repositories.Interfaces;
using App104.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App104.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateTagDto tagDto)
        {
            Tag tag = new()
            {
                Name = tagDto.Name,
            };
            await _repository.AddAsync(tag);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            _repository.Delete(tag);

            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
        {
            var tags = await _repository
                .GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false)
                .ToListAsync();


            ICollection<GetTagDto> result = new List<GetTagDto>();

            foreach (Tag item in tags)
            {
                result.Add(new()
                {
                    Id = item.Id,

                    Name = item.Name,
                });
            }
            return result;
        }

        public async Task<GetTagDto> GetAsync(int id)
        {
            Tag tag = await _repository
                .GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            return new()
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }

        public async Task UpdateAsync(int id, UpdateTagDto tagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            tag.Name = tagDto.Name;

            _repository.Update(tag);

            await _repository.SaveChangesAsync();
        }
    }
}
