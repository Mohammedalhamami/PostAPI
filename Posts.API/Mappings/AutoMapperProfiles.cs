using AutoMapper;
using Posts.API.Models.Domain;
using Posts.API.Models.DTOs;

namespace Posts.API.Mappings
{
	public class AutoMapperProfiles : Profile
	{
        public AutoMapperProfiles()
        {

            CreateMap<Post, PostDto>();
            CreateMap<AddPostDto, Post>();
            
        }
    }
}
