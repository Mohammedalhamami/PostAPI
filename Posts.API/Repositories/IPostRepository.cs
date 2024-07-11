using Posts.API.Models.Domain;

namespace Posts.API.Repositories
{
    public interface IPostRepository
	{
		Task<List<Post>> ReadAllAsync();
		Task<Post?> ReadByIdAsync(int id);
		Task<Post> CreateAsync(Post post);
		Task<Post?> UpdateAsync(Post post, int id);
		Task<Post?> DeleteAsync(int id);
	}
   
	
}
