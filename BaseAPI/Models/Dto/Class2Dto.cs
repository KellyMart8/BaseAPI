using System.ComponentModel.DataAnnotations;

namespace BaseAPI.Models.Dto
{
    public class Class2Dto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int Id2CP { get; set; }
    }
}
