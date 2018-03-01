using System.ComponentModel.DataAnnotations;

namespace SchoolAdmin.Domain
{
    public class GPA
    {
        [Key]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int Value{ set; get; }
    }
}