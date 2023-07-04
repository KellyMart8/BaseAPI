using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseAPI.Data
{
    public class Class2
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

        public int Id2CP { get; set; }
        [ForeignKey("Id2CP")]
        public ClasePrincipal ClasePrincipal { get; set; }
    }
}
