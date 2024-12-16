using AutoMapper;
using LibraryApi.Models;
using LibraryApi.ViewModels;
using System.Linq;

namespace LibraryApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<CreateBookViewModel, Book>();
            CreateMap<UpdateBookViewModel, Book>();

            // Mapeamento para GenreViewModel
            CreateMap<Genre, GenreViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Title).ToList()));

            // Mapeamento para AuthorViewModel
            CreateMap<Author, AuthorViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Title).ToList()));

        }
    }
}

