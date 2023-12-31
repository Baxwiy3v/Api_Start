﻿using App104.DTOS.Tag;
using App104.DTOS;

namespace App104.Services.Interfaces
{
    public interface ITagService
    {
        Task<ICollection<GetTagDto>> GetAllAsync(int page, int take);
        Task<GetTagDto> GetAsync(int id);
        Task CreateAsync(CreateTagDto tagDto);
        Task UpdateAsync(int id, UpdateTagDto tagDto);
        Task DeleteAsync(int id);
    }
}
