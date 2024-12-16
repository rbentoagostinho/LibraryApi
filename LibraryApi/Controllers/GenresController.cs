using AutoMapper;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using LibraryApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreViewModel>>> GetAll()
        {
            var genres = await _genreService.GetAllAsync();
            var genreViewModels = _mapper.Map<IEnumerable<GenreViewModel>>(genres);
            return Ok(genreViewModels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreViewModel>> GetById(int id)
        {
            var genre = await _genreService.GetByIdAsync(id);
            if (genre == null) return NotFound();

            var genreViewModel = _mapper.Map<GenreViewModel>(genre);
            return Ok(genreViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Genre genre)
        {
            await _genreService.AddAsync(genre);

            var genreViewModel = _mapper.Map<GenreViewModel>(genre);
            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genreViewModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Genre genre)
        {
            if (id != genre.Id) return BadRequest();
            await _genreService.UpdateAsync(genre);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _genreService.DeleteAsync(id);
            return NoContent();
        }
    }
}
