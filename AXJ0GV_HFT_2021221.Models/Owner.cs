using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Models
{
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public string IdentityCardNumber { get; set; }
        [NotMapped]
        public virtual ICollection<Dog> Dogs{ get; set; }

        public Owner()
        {
            this.Dogs = new HashSet<Dog>();
        }
    }
}
