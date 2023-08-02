using System.ComponentModel.DataAnnotations;

namespace Assignment3.Model
{
    public class Posts
    {
        [Key]
        public int PostID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set;}
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool PublishStatus { get; set; }
        [Required]
        public int CategoryID { get; set; }

        public AppUsers user { get; set; }
        public PostCategories category { get; set; }
    }
}
