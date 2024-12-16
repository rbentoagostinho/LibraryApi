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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            var authorViewModels = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
            return Ok(authorViewModels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorViewModel>> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();

            var authorViewModel = _mapper.Map<AuthorViewModel>(author);
            return Ok(authorViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Author author)
        {
            await _authorService.AddAsync(author);

            var authorViewModel = _mapper.Map<AuthorViewModel>(author);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, authorViewModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Author author)
        {
            if (id != author.Id) return BadRequest();

            await _authorService.UpdateAsync(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
