
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Model
{
    public class AppUsers
    {
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
