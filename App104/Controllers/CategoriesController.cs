﻿using App104.DAl;
using App104.DTOS;
using App104.Entities;
using App104.Repositories.Interfaces;
using App104.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App104.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 2)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);


            return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto categoryDto)
        {
            await _service.CreateAsync(categoryDto);

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDto categoryDto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            await _service.UpdateAsync(id, categoryDto);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
