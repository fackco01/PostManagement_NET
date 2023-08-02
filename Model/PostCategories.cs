using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Model
{
    public class PostCategories
    {
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }
        [Required] public string CategoryName { get; set; }
        [Required] public string CategoryDescription { get; set; }
    }
}
