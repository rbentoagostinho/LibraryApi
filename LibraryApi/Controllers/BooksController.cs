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
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(books);
            return Ok(bookViewModels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();

            var bookViewModel = _mapper.Map<BookViewModel>(book);
            return Ok(bookViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBookViewModel model)
        {
            var book = _mapper.Map<Book>(model);
            await _bookService.AddAsync(book);
            
            book = await _bookService.GetByIdAsync(book.Id);
            var bookViewModel = _mapper.Map<BookViewModel>(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, bookViewModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateBookViewModel model)
        {
            if (id != model.Id) return BadRequest();

            var book = _mapper.Map<Book>(model);
            await _bookService.UpdateAsync(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
