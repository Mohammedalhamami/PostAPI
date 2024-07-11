using Microsoft.EntityFrameworkCore;
using Posts.API.Data;
using Posts.API.Models.Domain;

namespace Posts.API.Repositories
{
	public class SQLPostRepository : IPostRepository
	{
		private readonly AppDbContext _dbContext;

		public SQLPostRepository(AppDbContext dbContext)
        {
			_dbContext = dbContext;
		}

		public async Task<List<Post>> ReadAllAsync()
		{
			return await _dbContext.Posts.ToListAsync();
		}
		public async Task<Post?> ReadByIdAsync(int id)
		{
			var postFromDb = await _dbContext.Posts.FirstOrDefaultAsync(post => post.Id == id);

			return postFromDb;
		}
		public async Task<Post> CreateAsync(Post post)
		{
			 await _dbContext.Posts.AddAsync(post);
			 await _dbContext.SaveChangesAsync();

			 return post;
		}
		public async Task<Post?> UpdateAsync(Post post, int id)
		{
			var postFromDb = await _dbContext.Posts.FirstOrDefaultAsync(postDb => postDb.Id == id);

			if(postFromDb is not null)
			{
				postFromDb.Content = post.Content;
				postFromDb.Publisher = post.Publisher;

				await _dbContext.SaveChangesAsync();

				return postFromDb;
			}

			return null;
		}
		public async Task<Post?> DeleteAsync(int id)
		{
			var postFromDb = await _dbContext.Posts.FirstOrDefaultAsync(post => post.Id == id);

			if (postFromDb is not null)
			{
				_dbContext.Posts.Remove(postFromDb);
				await _dbContext.SaveChangesAsync();
				return postFromDb;
			}

			return null;
		}


	}
}
