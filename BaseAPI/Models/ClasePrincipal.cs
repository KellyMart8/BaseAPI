using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseAPI.Data
{
    public class ClasePrincipal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
