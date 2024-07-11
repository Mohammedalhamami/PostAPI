using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Posts.API.Mappings;
using Posts.API.Models.Domain;
using Posts.API.Models.DTOs;
using Posts.API.Repositories;

namespace Posts.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly IMapper mapper;
		private readonly IPostRepository postRepository;

		public PostsController(IMapper mapper, IPostRepository postRepository)
        {
			this.mapper = mapper;
			this.postRepository = postRepository;
		}


		//Post:/api/auth/register


		//Get: /api/posts
		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> GetAllPosts()
		{
			//from database to domain.
			var postsDomain = await postRepository.ReadAllAsync();

			if (postsDomain == null) { return NotFound(); }

			//mapping to dto.
			var postsDto =  mapper.Map<List<PostDto>>(postsDomain);

			return Ok(postsDto);

		}

	
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Roles = "Admin")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> GetPostById([FromRoute] int id)
		{
			var postDomain = await postRepository.ReadByIdAsync(id);

			if(postDomain is null) { return NotFound(); }

			//mapping domain to dto.
			return Ok(mapper.Map<PostDto>(postDomain));
		}

		
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> CreatePost([FromBody]AddPostDto postDto)
		{
			//map AddPostDto to domain
			var postDomain = mapper.Map<Post>(postDto);

			postDomain = await postRepository.CreateAsync(postDomain);

			//map domain to PostDto

			var post = mapper.Map<PostDto>(postDomain);

			return Ok(post);
		}


		[HttpPut]
		[Route("{id:int}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Roles = "Admin")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> UpdatePost([FromBody] AddPostDto postDto, [FromRoute] int id)
		{
			//mapping to domain

			var postDomain = mapper.Map<Post>(postDto);

			postDomain = await postRepository.UpdateAsync(postDomain, id);

			if(postDomain is null) { return NotFound(); }

			return Ok(mapper.Map<PostDto>(postDomain));
		}


		[HttpDelete]
		[Route("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeletePost([FromRoute]int id)
		{
			var postDomain = await postRepository.DeleteAsync(id);

			if (postDomain == null) { return NotFound(); }

			//mapping to postDto.

			return Ok(mapper.Map<PostDto>(postDomain));

		}

	}
}
