using AutoMapper;
using BooksAPIsv2.Models;
using BooksAPIsv2.Models.Dtos;
using BooksAPIsv2.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Route("api/Books")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class BooksControllers : Controller
    {
        private readonly IRepositoryBook _repo;
        private readonly IMapper _mapper;

        public BooksControllers(IRepositoryBook repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        /// <summary>
        /// No recibe ningun tipo de parametros y devuelve todas las libros que existan en la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookDtos>))]
        [ProducesResponseType(400)]
        public IActionResult GetBooks()
        {
            var ListCategories = _repo.GetBooks();
            var ListDto = new List<BookDtos>();

            foreach (var list in ListCategories)
            {
                ListDto.Add(_mapper.Map<BookDtos>(list));
            }

            return Ok(ListDto);
        }
        /// <summary>
        /// Recibe un entero, y devuelve una libro en especifico.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}", Name = "GetBook")]
        [ProducesResponseType(200, Type = typeof(List<BookDtos>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetBook(int Id)

        {
            var Book = _repo.GetBook(Id);

            if (Book == null)
            {
                return NotFound();
            }

            var bookDto = new BookDtos();
            {
                bookDto = _mapper.Map<BookDtos>(Book);
            }

            return Ok(bookDto);
        }
        /// <summary>
        /// Recibe un entero, y devuelve una libro en especifico.
        /// </summary>
        /// <param name="CatId"></param>
        /// <returns></returns>

        [HttpGet("{CatId:int}", Name = "GetBooksInCat")]
        [ProducesResponseType(200, Type = typeof(List<BookDtos>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetBooksInCat(int CatId)
        {
            var Book = _repo.GetBooksInCat(CatId);

            if (Book == null)
            {
                return NotFound();
            }

            var bookDto = new BookDtos();
            {
                bookDto = _mapper.Map<BookDtos>(Book);
            }

            return Ok(bookDto);
        }

        /// <summary>
        /// devuelve todos lo resultados de la palabra clave puesta en el buscador.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("find")]
        [ProducesResponseType(200, Type = typeof(List<BookDtos>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult FindBooks(string title)
        {
            var Ans = _repo.FindBook(title);

            if(Ans.Any())
            {
                return Ok(Ans);
            }
            return NotFound("No se encontraron resultado para esta busqueda.");
        }

        /// <summary>
        /// Necesita de un Json para proceder su operacion, esta crea una libro como su nombre lo indica.
        /// </summary>
        /// <param name="BookDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<BookDtos>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateBook([FromBody] BookDtos BookDto)
        {
            if(BookDto == null)
            {
                return BadRequest(ModelState);
            }
            
            if(_repo.existsBook(BookDto.Title))
            {
                ModelState.AddModelError("","La libro ya existe");
                return StatusCode(409, ModelState);
            }

            var book = _mapper.Map<Book>(BookDto);

            if(!_repo.CreateBook(book))
            {
                ModelState.AddModelError("", $"No se pudo llevar a cabo, la creacion de la libro {book.Title}.");
                return StatusCode(400, ModelState);

            }

            return CreatedAtRoute("GetBook", new { Id = book.Id },book);
        }
        /// <summary>
        /// Recibe por parametro un entero para identificar la libro que va actualizar.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="BookDto"></param>
        /// <returns></returns>
        [HttpPatch("{Id:int}", Name = "UpdateBook")]
        [ProducesResponseType(200, Type = typeof(List<BookDtos>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateBook(int Id,[FromBody] BookDtos BookDto)
        {
            if (BookDto == null || Id != BookDto.Id)
            {
                return NotFound(ModelState);
            }

            var book = _mapper.Map<Book>(BookDto);

            if (!_repo.UpdateBook(book))
            {
                ModelState.AddModelError("", $"No se pudo llevar a cabo, la actualizacion de la libro {book.Title}.");
                return StatusCode(500, ModelState);

            }

            return CreatedAtRoute("GetBook", new { Id = book.Id }, book);
        }
        /// <summary>
        /// Sirve para eliminar libro existente y recibe por parametro un entero.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}", Name = "DeleteBook")]
        [ProducesResponseType(200, Type = typeof(List<BookDtos>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteBook(int Id)
        {
           if(!_repo.existsBook(Id))
            {
                ModelState.AddModelError("", "La libro que ha selecionado no existe.");
                return StatusCode(404, ModelState);
            }

            var catergory = _repo.GetBook(Id);

           if(!_repo.DeleteBook(catergory))
            {
                ModelState.AddModelError("", "No se pudo llegar a cabo la eliminacion de la libro.");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

    }
}
