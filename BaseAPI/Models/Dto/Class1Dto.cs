using System.ComponentModel.DataAnnotations;

namespace BaseAPI.Models.Dto
{
    public class Class1Dto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int IdCP { get; set; }
    }
}
