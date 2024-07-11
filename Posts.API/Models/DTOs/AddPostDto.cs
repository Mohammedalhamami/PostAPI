using System.ComponentModel.DataAnnotations;

namespace Posts.API.Models.DTOs
{
	public class AddPostDto
	{
       
        [Length(3, 250, ErrorMessage ="Content Length must be between 3 and 250"), Required]
        public string content { get; set; }

		[Required]
        public string Publisher { get; set; }
    }
}
