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
    public enum Sex
    {
        Male,
        Female
    }
    [Table("dogs")]
    public class Dog : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Species { get; set; }
        public string Name{ get; set; }
        public Sex Sex { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Owner Owner { get; set; }
        public int OwnerID { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Injection Injection { get; set; }
        public int InjectionID{ get; set; }
        
        
    }
}
