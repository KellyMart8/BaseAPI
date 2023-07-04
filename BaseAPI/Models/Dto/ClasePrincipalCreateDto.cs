using System.ComponentModel.DataAnnotations;

namespace BaseAPI.Models.Dto
{
    public class ClasePrincipalCreateDto
    {
        public int Id { get; set; }
        [Required]
        public int id2 { get; set; }
        [Required]
        [MaxLength(100)]
        public string Prop { get; set; }
        [Required]
        [MaxLength(100)]
        public string Myprop { get; set; }
    }
}
