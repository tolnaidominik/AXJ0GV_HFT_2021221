using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Models
{
    [Table("owners")]
    public class Owner : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public string IdentityCardNumber { get; set; }
        [NotMapped] // REVERSE NAVIGATION PROPERTY
        [JsonIgnore]
        public virtual ICollection<Dog> Dogs { get; set; }

        public Owner()
        {
            Dogs = new HashSet<Dog>();
        }
    }
}
