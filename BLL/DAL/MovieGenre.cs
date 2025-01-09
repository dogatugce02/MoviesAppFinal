using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class MovieGenre
    {
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int GenreId { get; set; }

        public Movies Movie { get; set; } // Bir kayıt bir filme aittir
        public Genre Genre { get; set; } // Bir kayıt bir türe aittir
    }
}
