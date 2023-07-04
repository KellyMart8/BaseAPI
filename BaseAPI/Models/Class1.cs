

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseAPI.Data
{
    public class Class1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int IdCP { get; set; }
        [ForeignKey("IdCP")]
        public ClasePrincipal ClasePrincipal { get; set; }
    }
}
