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
    [Route("api/Categories")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CategoriesControllers : Controller
    {
        private readonly IRepositoryCategory _crp;
        private readonly IMapper _mapper;

        public CategoriesControllers(IRepositoryCategory crp, IMapper mapper)
        {
            _crp = crp;
            _mapper = mapper;
        }
        /// <summary>
        /// No recibe ningun tipo de parametros y devuelve todas las categorias que existan en la base de datos.
        /// </summary>
        /// <returns></returns>
       [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoCategory>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories()
        {
            var ListCategories = _crp.GetCategories();
            var ListDto = new List<DtoCategory>();

            foreach(var list in ListCategories)
            {
                ListDto.Add(_mapper.Map<DtoCategory>(list));
            }

            return Ok(ListDto);
        }
        /// <summary>
        /// Recibe un entero, y devuelve una categoria en especifico.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet ("{Id:int}",Name = "GetCategory")]
        [ProducesResponseType(200, Type = typeof(List<DtoCategory>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetCategory(int Id)

        {
            var Category = _crp.GetCategory(Id);

            if(Category == null)
            {
                return NotFound();
            }

            var CatDto = new DtoCategory();
            {
                CatDto = _mapper.Map<DtoCategory>(Category);
            }

            return Ok(CatDto);
        }
        /// <summary>
        /// Necesita de un Json para proceder su operacion, esta crea una categoria como su nombre lo indica.
        /// </summary>
        /// <param name="CategoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<DtoCategory>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateCategory([FromBody] DtoCategory CategoryDto)
        {
            if(CategoryDto == null)
            {
                return BadRequest(ModelState);
            }
            
            if(_crp.existsCategory(CategoryDto.Name))
            {
                ModelState.AddModelError("","La categoria ya existe");
                return StatusCode(409, ModelState);
            }

            var category = _mapper.Map<Category>(CategoryDto);

            if(!_crp.CreateCategory(category))
            {
                ModelState.AddModelError("", $"No se pudo llevar a cabo, la creacion de la categoria {category.Name}.");
                return StatusCode(400, ModelState);

            }

            return CreatedAtRoute("GetCategory", new { Id = category.Id },category);
        }
        /// <summary>
        /// Recibe por parametro un entero para identificar la categoria que va actualizar.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CategoryDto"></param>
        /// <returns></returns>
        [HttpPatch("{Id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(200, Type = typeof(List<DtoCategory>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateCategory(int Id,[FromBody] DtoCategory CategoryDto)
        {
            if (CategoryDto == null || Id != CategoryDto.Id)
            {
                return NotFound(ModelState);
            }
          
            var category = _mapper.Map<Category>(CategoryDto);

            if (!_crp.UpdateCategory(category))
            {
                ModelState.AddModelError("", $"No se pudo llevar a cabo, la actualizacion de la categoria {category.Name}.");
                return StatusCode(500, ModelState);

            }

            return CreatedAtRoute("GetCategory", new { Id = category.Id }, category);
        }
        /// <summary>
        /// Sirve para eliminar categoria existente y recibe por parametro un entero.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}", Name = "DeleteCategory")]
        [ProducesResponseType(200, Type = typeof(List<DtoCategory>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteCategory(int Id)
        {
           if(!_crp.existsCategory(Id))
            {
                ModelState.AddModelError("", "La categoria que ha selecionado no existe.");
                return StatusCode(404, ModelState);
            }

            var catergory = _crp.GetCategory(Id);

           if(!_crp.DeleteCategory(catergory))
            {
                ModelState.AddModelError("", "No se pudo llegar a cabo la eliminacion de la categoria.");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

    }
}
