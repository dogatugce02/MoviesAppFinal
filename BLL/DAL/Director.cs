using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Director
    {
        public int Id {  get; set; }
        [Required]
        [StringLength(100)]

        [Display(Name = "Director Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
       
        public bool isRetired { get; set; }

        public List<Movies> Movies { get; set; } = new List<Movies>();
       
    }

    
}