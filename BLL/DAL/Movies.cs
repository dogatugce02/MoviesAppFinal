using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace BLL.DAL
{
    public class Movies
    {
        public int Id {  get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public decimal TotalRevenue { get; set; }
        public int DirectorId { get; set; }
        public Director Directors { get; set; }
        //public List<Director> DirectorList { get; set; }
        //public List<PetOwner> PetOwners { get; set; } = new List<PetOwner>();
        public List<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>(); // navigational property




    }



}
