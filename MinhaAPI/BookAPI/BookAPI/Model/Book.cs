
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Model
{
    public class Book
    {
        public int Id { get; set; }
        [Required]

        public string Author { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        

    }
}
