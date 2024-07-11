namespace Posts.API.Models.Domain
{
	public class Post
	{
        public int Id { get; set; }
        public string Content { get; set; }
        public string Publisher { get; set; }
        public DateTime DateTimePosted { get; set; }
    }
}
