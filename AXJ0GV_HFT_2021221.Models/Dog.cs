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
        [Key] // ELSODLEGES KULCS
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required] // KOTELEZO
        public string Species { get; set; }
        public string Name{ get; set; }
        public Sex Sex{ get; set; }
        [NotMapped] // NAVIGATION PROPERTY
        public virtual Owner Owner { get; set; }
        public int OwnerID { get; set; }
    }
}
