using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Navigation Property
        public List<MovieGenre> MovieGenres { get; set; } // Bir tür birden fazla filmde kullanılabilir
    }
}
