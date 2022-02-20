using AutoMapper;
using BooksAPIsv2.Models.Dtos;
using BooksAPIsv2.Models;

namespace BooksApisv2.BooksMapper
{
    public class BooksMappers : Profile
    {
        public BooksMappers()
        {
            CreateMap<Category, DtoCategory>().ReverseMap();
            CreateMap<Book, BookDtos>().ReverseMap();
            CreateMap<Book, BookCreateDto>().ReverseMap();
            CreateMap<Book, BookUpdateDto>().ReverseMap();
        }
    }
}
