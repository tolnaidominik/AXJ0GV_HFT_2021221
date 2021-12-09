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
    public enum InjectionName
    {
        Bordetella_Bronchiseptica,
        Canine_Distemper,
        Canine_Hepatitis,
        Canine_Parainfluenza,
        Heartworm,
        Leptospirosis,
        Parvovirus,
        Rabies
    }
    public enum Commonness
    {
        Once,
        Monthly,
        Half_year,
        Yearly
    }
    [Table("injections")]
    public class Injection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public InjectionName Name { get; set; }
        [Required]
        public int? Price { get; set; }
        public Commonness Commonness { get; set; }

        [NotMapped] // REVERSE NAVIGATION PROPERTY
        [JsonIgnore]
        public virtual ICollection<Dog> Dogs { get; set; }

        public Injection()
        {
            Dogs = new HashSet<Dog>();
        }

    }
}
