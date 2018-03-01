using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdmin.Domain
{
    public class YearGroup
    {
        [Key]
        [Column(Order = 1)]
        public int Level { get; set; }

        [Key]
        [Column(Order = 2)]
        public string StudentId { get; set; }

        public virtual Course Student { get; set; }
    }
}
