using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Models
{
    public enum Sex
    {
        Male,
        Female
    }
    [Table("dogs")]
    public class Dog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Species { get; set; }
        public string Name{ get; set; }
        public Sex Sex{ get; set; }
        [NotMapped]
        public virtual Owner Owner { get; set; }
        public int OwnerID { get; set; }
        [NotMapped]
        public virtual ICollection<Injection> Injections { get; set; }
        public Dog()
        {
            this.Injections = new HashSet<Injection>();
        }
    }
}
